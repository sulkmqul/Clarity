using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;

using System.Reflection;


namespace CIsl.DB
{
    /// <summary>
    /// DataBaseAccessClass基底
    /// </summary>
    public abstract class BaseDataBaseAccess : ICloneable
    {
        /// <summary>
        /// PKを返却するように
        /// </summary>
        public virtual int ID
        {
            get
            {
                return EVal;
            }
        }

        /// <summary>
        /// nullや問題があるデータのint置き換え値
        /// </summary>
        public const int EVal = -1;

        /// <summary>
        /// 問題がある場合のDateTimeの置き換え
        /// </summary>
        public static DateTime EDate = DateTime.MinValue;
        /////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 全メンバの取得
        /// <remarks>
        /// メンバ変数名とDBのフィールド名が一致すること。DB以外のフィールドを持たせないこと。持たせたいときはプロパティにすること。
        /// publicなメンバ変数以外の値が取得したいときなどはオーバーライドすること。
        /// </remarks>
        /// </summary>
        /// <param name="dr"></param>
        protected virtual void ReadAll(System.Data.Common.DbDataReader dr)
        {
            //public メンバフィールドの取得
            Type t = this.GetType();
            FieldInfo[] minfovec = t.GetFields(BindingFlags.Public | BindingFlags.Instance);

            //全フィールドの値を取得
            foreach (FieldInfo fi in minfovec)
            {
                //フィールド変数名取得
                string name = fi.Name;
                object? obj = null;

                try
                {

                    //取得タイプごとに分岐して取得
                    //int
                    if (fi.FieldType == typeof(int))
                    {
                        obj = GetSafe(dr[name], EVal);
                    }

                    //decimal
                    if (fi.FieldType == typeof(decimal))
                    {
                        obj = GetSafe(dr[name], (decimal)EVal);
                    }

                    //bool
                    if (fi.FieldType == typeof(bool))
                    {
                        obj = GetSafe(dr[name], false);
                    }

                    //string
                    if (fi.FieldType == typeof(string))
                    {
                        obj = GetSafe(dr[name], "");
                    }

                    //datetime
                    if (fi.FieldType == typeof(DateTime))
                    {
                        obj = GetSafe(dr[name], EDate);
                    }

                    //byte[]
                    if (fi.FieldType == typeof(byte[]))
                    {
                        obj = GetSafe(dr[name], null);
                    }

                    //取得値の設定
                    fi.SetValue(this, obj);
                }
                catch (IndexOutOfRangeException e)
                {
                    //SELECTするものは常にすべてとは限らない。
                    //よってフィールドが存在しないのは許容できるものです。                    
                    continue;
                }


            }
        }



        /// <summary>
        /// データチェックして取得 キャストし使用
        /// </summary>
        /// <param name="drdata">NpgsqlDataReaderから取得したデータ</param>
        /// <param name="def">デフォルト値</param>
        /// <returns></returns>
        protected static object? GetSafe(object drdata, object? def)
        {
            if (drdata == DBNull.Value)
            {
                return def;
            }

            return drdata;
        }



        /// <summary>
        /// コピーできるようにしておく
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {   
            object ans = this.MemberwiseClone();
            return ans;
        }
    }

  
    /// <summary>
    /// DAC基底クラス
    /// </summary>
    public abstract class BaseDac : BaseDataBaseAccess
    {
        /// <summary>
        /// 読み込みデータDelegate
        /// </summary>
        /// <remarks>while (dr.Read()){ } を実行し、好きに読み込みを行い返却すること </remarks>
        /// <param name="dr"></param>
        /// <returns></returns>
        protected delegate object ReadDataDelegate(NpgsqlDataReader dr);

        
        /////////////////////////////////////////////////////


        /// <summary>
        /// 対象をリストで取得 ReadAllで読み込み。try catchせよ
        /// </summary>
        /// <typeparam name="T">取得DAC</typeparam>
        /// <param name="cone">コネクション</param>
        /// <param name="sql">SQL文</param>
        /// <param name="paramlist">設定パラメータ</param>
        /// <returns></returns>
        protected static List<T> GetRecordsList<T>(DBConnect cone, string sql, List<SqlParamData>? paramlist = null) where T : BaseDac, new()
        {
            List<T> anslist = new List<T>();

            try
            {
                //コマンド発行
                using (NpgsqlCommand com = new NpgsqlCommand(sql, cone.DBCone))
                {
                    #region パラメータADD
                    if (paramlist != null)
                    {
                        foreach (SqlParamData data in paramlist)
                        {
                            com.Parameters.AddWithValue(data.Name, data.Value);
                        }
                    }
                    #endregion

                    //実行
                    using (NpgsqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            //読み込み
                            T ans = new T();
                            ans.ReadAll(dr);


                            anslist.Add(ans);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return anslist;
        }

        /// <summary>
        /// 対象を一つ取得　ReadAllで読み込み。
        /// </summary>
        /// <typeparam name="T">取得対象</typeparam>
        /// <param name="cone">db接続</param>
        /// <param name="sql">取得SQL</param>
        /// <param name="paramlist">設定パラメータ</param>
        /// <returns>対象</returns>
        protected static T GetRecordData<T>(DBConnect cone, string sql, List<SqlParamData>? paramlist = null) where T : BaseDac, new()
        {
            T ans = new T();

            try
            {
                //コマンド発行
                using (NpgsqlCommand com = new NpgsqlCommand(sql, cone.DBCone))
                {
                    #region パラメータADD
                    if (paramlist != null)
                    {
                        foreach (SqlParamData data in paramlist)
                        {
                            com.Parameters.AddWithValue(data.Name, data.Value);
                        }
                    }
                    #endregion

                    //実行
                    using (NpgsqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            //読み込み
                            ans.ReadAll(dr);

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ans;
        }


        /// <summary>
        /// 対象を任意で読み込み、DACクラスに頼らない読み込みが可能  deleprocの返り値がそのまま返却されます。
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="sql">実行SQL</param>
        /// <param name="deleproc">読み込みDelegate</param>
        /// <param name="paramlist">実行パラメータ</param>
        /// <returns></returns>
        protected static object GetRecordObject(DBConnect cone, string sql, ReadDataDelegate deleproc, List<SqlParamData>? paramlist = null)
        {
            object ans = null;

            try
            {
                //コマンド発行
                using (NpgsqlCommand com = new NpgsqlCommand(sql, cone.DBCone))
                {
                    #region パラメータADD
                    if (paramlist != null)
                    {
                        foreach (SqlParamData data in paramlist)
                        {
                            com.Parameters.AddWithValue(data.Name, data.Value);
                        }
                    }
                    #endregion

                    //実行
                    using (NpgsqlDataReader dr = com.ExecuteReader())
                    {
                        ans = deleproc(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ans;
        }


        /// <summary>
        /// 返り値付の実行 主にinsertで使用
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="sql">実行SQL</param>
        /// <param name="paramlist">パラメータリスト</param>
        /// <returns></returns>
        protected object? ExecuteScalar(DBConnect cone, string sql, List<SqlParamData>? paramlist = null)
        {
            object? ans = null;

            try
            {
                //コマンド発行
                using (NpgsqlCommand com = new NpgsqlCommand(sql, cone.DBCone))
                {
                    #region パラメータADD
                    if (paramlist != null)
                    {
                        foreach (SqlParamData data in paramlist)
                        {
                            com.Parameters.AddWithValue(data.Name, data.Value);
                        }
                    }
                    #endregion

                    //実行
                    ans = com.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return ans;
        }

        /// <summary>
        /// SQL実行　主にupdateでの使用を想定
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="sql"></param>
        /// <param name="paramlist"></param>
        /// <returns></returns>
        protected bool ExecuteNonQuery(DBConnect cone, string sql, List<SqlParamData>? paramlist = null)
        {
            try
            {
                //コマンド発行
                using (NpgsqlCommand com = new NpgsqlCommand(sql, cone.DBCone))
                {
                    #region パラメータADD
                    if (paramlist != null)
                    {
                        foreach (SqlParamData data in paramlist)
                        {
                            com.Parameters.AddWithValue(data.Name, data.Value);
                        }
                    }
                    #endregion

                    //実行
                    int n = com.ExecuteNonQuery();
                    if (n <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return true;
        }
                

        

        
    }

    

    


    

    /// <summary>
    /// パラメータ設定情報
    /// </summary>
    public class SqlParamData
    {
        /// <summary>
        /// パラメータ名
        /// </summary>
        public string Name = "";

        /// <summary>
        /// パラメータ値
        /// </summary>
        public object Value = null;
    }
}
