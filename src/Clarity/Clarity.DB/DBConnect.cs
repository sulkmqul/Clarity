using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;

namespace CIsl.DB
{
    /// <summary>
    /// npgsqlを使用したDB接続管理
    /// </summary>
    public class DBConnect : IDisposable
    {
        #region メンバ変数

        /// <summary>
        /// DB接続管理
        /// </summary>
        public NpgsqlConnection DBCone;

        /// <summary>
        /// トランザクション管理
        /// </summary>
        private NpgsqlTransaction? Tra = null;

        /// <summary>
        /// 接続文字列
        /// </summary>
        private string ConnectString = "";
        #endregion


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="sc">DB接続文字列</param>
        public DBConnect(string sc)
        {
            this.ConnectString = sc;
            this.DBCone = new NpgsqlConnection(sc);
            this.DBCone.Open();
        }

        /// <summary>
        /// トランザクション開始
        /// </summary>
        /// <returns>成功可否</returns>
        public bool BeginTransaction()
        {
            this.Tra = this.DBCone.BeginTransaction();            
            return true;
        }

        /// <summary>
        /// トランザクション　コミット
        /// </summary>
        /// <returns>成功可否</returns>
        public bool ComitTransaction()
        {
            if (this.Tra == null)
            {
                return false;
            }

            this.Tra.Commit();

            this.Tra.Dispose();
            this.Tra = null;
            return true;
        }

        /// <summary>
        /// トランザクションロールバック
        /// </summary>
        /// <returns></returns>
        public bool RollbackTransaction()
        {
            if(this.Tra == null)
            {
                return false;
            }

            this.Tra.Rollback();

            this.Tra.Dispose();
            this.Tra = null;
            return true;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 削除されたとき
        /// </summary>
        public void Dispose()
        {
            this.DBCone.Close();
        }
    }
}

