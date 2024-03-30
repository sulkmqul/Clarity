using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing.Text;
using System.Security.AccessControl;
using System.Diagnostics.CodeAnalysis;
using System.Configuration;
using System.IO;

namespace Clarity
{

    /// <summary>
    /// 設定情報
    /// </summary>
    public class ClaritySetting : ClaritySettingCore
    {
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 管理データ AddManage関数を利用してADDする
        /// </summary>
        private Dictionary<int, ClaritySettingData> DataDic = new Dictionary<int, ClaritySettingData>();

        /// <summary>
        /// 管理データ(文字をそのまま DataDicと内容は同等、キーアクセスを早くするための処置) AddManage関数を利用してADDする
        /// </summary>
        private Dictionary<string, ClaritySettingData> DataDicKeyString = new Dictionary<string, ClaritySettingData>();
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 設定の読み込み
        /// </summary>
        /// <param name="filepath">読み込みファイルパス</param>       
        /// <param name="overwrite">追記上書き可否 true=追記する</param>
        public void Load(string filepath, bool overwrite = false)
        {
            using(FileStream fp = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                this.Load(fp, overwrite);
            }            

        }

        public void Load(Stream st, bool overwrite = false)
        {
            //ファイルの読み込み
            ClaritySettingFile fp = new ClaritySettingFile();
            List<ClaritySettingData> datalist = fp.ReadSetting(st);


            //コード重複チェック
            bool dcret = this.CheckCodeDuplicate(datalist);
            if (dcret == false)
            {
                throw new Exception("Code重複が存在します");

            }

            //データの作成
            this.DataDic = new Dictionary<int, ClaritySettingData>();
            this.DataDicKeyString = new Dictionary<string, ClaritySettingData>();

            int id = 1;
            datalist.ForEach(x =>
            {
                x.Id = id;
                this.AddManage(x, overwrite);
                id++;

            });
        }

        /// <summary>
        /// デフォルトデータの挿入
        /// </summary>
        /// <param name="datalist">挿入データ組リスト</param>
        internal void InitDefault(List<(string code, object data)> datalist)
        {
            //データの作成
            this.DataDic = new Dictionary<int, ClaritySettingData>();
            this.DataDicKeyString = new Dictionary<string, ClaritySettingData>();

            int id = 1;
            datalist.ForEach(x =>
            {
                ClaritySettingData data = new ClaritySettingData();
                {
                    data.Id = id;
                    data.Code = x.code;
                    data.Data = x.data;

                    Type t = x.data.GetType();
                    EClaritySettingDataType dt, sdt;
                    this.GetClaritySettingDataType(t, out dt, out sdt);
                    data.DataType = dt;
                    data.SubDataType = sdt;
                }

                //管理へ追加
                this.AddManage(data, true);
                id++;
            });
        }

        /// <summary>
        /// 管理しているkeyとcodeを取得する(Aid用)
        /// </summary>
        /// <returns></returns>
        internal List<(int key, string code)> GetManagedKeyCode()
        {
            return this.DataDic.Select(x => (x.Value.Id, x.Value.Code)).ToList();
        }


        /// <summary>
        /// 指定のIDが登録されているかをチェックする。
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true=登録済み</returns>
        public bool CheckExists(int id)
        {
            //IDのから値を取得
            ClaritySettingData? sdata = null;
            bool f = this.DataDic.TryGetValue(id, out sdata);
            if (f == false)
            {
                return false;
            }
            
            //念のため双方に存在するかを確認
            f = this.DataDicKeyString.ContainsKey(sdata?.Code ?? "");
            if (f == false)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 指定のCodeが登録されているかをチェックする
        /// </summary>
        /// <param name="code"></param>
        /// <returns>true=登録済み</returns>
        public bool CheckExists(string code)
        {
            //IDのから値を取得
            ClaritySettingData? sdata = null;
            bool f = this.DataDicKeyString.TryGetValue(code, out sdata);
            if (f == false)
            {
                return false;
            }

            //念のため双方に存在するかを確認
            f = this.DataDic.ContainsKey(sdata?.Id ?? int.MinValue);
            if (f == false)
            {
                return false;
            }
            return true;
        }

        #region 設定値の取得

        #region 設定値の取得(ID)
        /// <summary>
        /// Bool設定の取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool GetBool(int id)
        {
            return this.GetSetting<bool>(id);
        }

        /// <summary>
        /// Integer設定の取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetInteger(int id)
        {
            return this.GetSetting<int>(id);
        }

        /// <summary>
        /// Float設定の取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public float GetFloat(int id)
        {
            return this.GetSetting<float>(id);
        }

        /// <summary>
        /// String設定の取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetString(int id)
        {
            return this.GetSetting<string>(id);
        }

        /// <summary>
        /// Vector2設定の取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vector2 GetVec2(int id)
        {
            return this.GetSetting<Vector2>(id);
        }

        /// <summary>
        /// Vector3設定の取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vector3 GetVec3(int id)
        {
            return this.GetSetting<Vector3>(id);
        }



        /// <summary>
        /// Integer設定配列の取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int[] GetIntegerArray(int id)
        {
            return this.GetSettingArray<int>(id);
        }

        /// <summary>
        /// Float設定配列の取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public float[] GetFloatArray(int id)
        {
            return this.GetSettingArray<float>(id);
        }

        /// <summary>
        /// String設定配列の取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string[] GetStringArray(int id)
        {
            return this.GetSettingArray<string>(id);
        }

        /// <summary>
        /// Vector2設定配列の取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vector2[] GetVec2Array(int id)
        {
            return this.GetSettingArray<Vector2>(id);
        }

        /// <summary>
        /// Vector3設定配列の取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vector3[] GetVec3Array(int id)
        {
            return this.GetSettingArray<Vector3>(id);
        }


        /// <summary>
        /// Enum値の取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetEnum<T>(int id) where T : Enum
        {
            string m = this.GetString(id);

            T ans = (T)Enum.Parse(typeof(T), m);
            return ans;
        }
        #endregion

        #region 設定値の取得(Code)

        /// <summary>
        /// boolの取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool GetBool(string code)
        {
            return this.GetSetting<bool>(code);
        }
        /// <summary>
        /// boolの取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool GetBool(string code, bool def)
        {
            return this.GetSetting<bool>(code, def);
        }

        /// <summary>
        /// Integer設定の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int GetInteger(string code)
        {
            return this.GetSetting<int>(code);
        }
        /// <summary>
        /// Integer設定の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int GetInteger(string code, int def)
        {
            return this.GetSetting<int>(code, def);
        }

        /// <summary>
        /// Float設定の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public float GetFloat(string code)
        {
            return this.GetSetting<float>(code);
        }
        /// <summary>
        /// Float設定の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public float GetFloat(string code, float def)
        {
            return this.GetSetting<float>(code, def);
        }

        /// <summary>
        /// String設定の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetString(string code)
        {
            return this.GetSetting<string>(code);
        }

        /// <summary>
        /// String設定の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetString(string code, string def)
        {
            return this.GetSetting<string>(code, def);
        }

        /// <summary>
        /// Vector2設定の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Vector2 GetVec2(string code)
        {
            return this.GetSetting<Vector2>(code);
        }

        /// <summary>
        /// Vector2設定の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Vector2 GetVec2(string code, Vector2 def)
        {
            return this.GetSetting<Vector2>(code, def);
        }

        /// <summary>
        /// Vector3設定の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Vector3 GetVec3(string code)
        {
            return this.GetSetting<Vector3>(code);
        }

        /// <summary>
        /// Vector3設定の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Vector3 GetVec3(string code, Vector3 def)
        {
            return this.GetSetting<Vector3>(code, def);
        }

        /// <summary>
        /// Vector4設定の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Vector4 GetVec4(string code)
        {
            return this.GetSetting<Vector4>(code);
        }

        /// <summary>
        /// Vector4設定の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Vector4 GetVec4(string code, Vector4 def)
        {
            return this.GetSetting<Vector4>(code, def);
        }

        /// <summary>
        /// Integer設定配列の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int[] GetIntegerArray(string code)
        {
            return this.GetSettingArray<int>(code);
        }

        /// <summary>
        /// Float設定配列の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public float[] GetFloatArray(string code)
        {
            return this.GetSettingArray<float>(code);
        }

        /// <summary>
        /// String設定配列の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string[] GetStringArray(string code)
        {
            return this.GetSettingArray<string>(code);
        }

        /// <summary>
        /// Vector2設定配列の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Vector2[] GetVec2Array(string code)
        {
            return this.GetSettingArray<Vector2>(code);
        }

        /// <summary>
        /// Vector3設定配列の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Vector3[] GetVec3Array(string code)
        {
            return this.GetSettingArray<Vector3>(code);
        }

        /// <summary>
        /// Enum値の取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetEnum<T>(string code) where T : Enum
        {
            string m = this.GetString(code);
            T ans = (T)Enum.Parse(typeof(T), m);
            return ans;
        }

        /// <summary>
        /// Enum値の取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public T GetEnum<T>(string code, T def) where T : Enum
        {
            try
            {
                return this.GetEnum<T>(code);
            }
            catch
            {
                return def;
            }
        }
        #endregion




        #endregion

        #region 設定値の設定
        /// <summary>
        /// 手動設定
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="code">Code</param>
        /// <param name="data">設定データ</param>
        /// <returns>設定成功可否 true=成功</returns>
        public bool SetData(int id, string code, object data)
        {
            ClaritySettingData cs = new ClaritySettingData();
            bool exid = this.DataDic.ContainsKey(id);
            bool excode = this.DataDicKeyString.ContainsKey(code);
            //片方だけにあるなら不正
            if (exid != excode)
            {
                return false;

            }
            if (exid == excode == true)
            {
                cs = this.DataDic[id];
                //キーが見つかったけど不正Codeだった
                if (cs.Code != code)
                {
                    return false;
                }
            }


            //値の設定
            cs.Id = id;
            cs.Code = code;

            //データtypeの割り出し
            Type t = data.GetType();
            EClaritySettingDataType dt, sdt;
            this.GetClaritySettingDataType(t, out dt, out sdt);
            cs.DataType = dt;
            cs.SubDataType = sdt;
            cs.Data = data;

            //追加
            this.AddManage(cs, true);

            return true;
        }


        /// <summary>
        /// IDを使用して値を設定する。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="data">設定値</param>
        /// <returns>設定成功可否 true=成功</returns>
        public bool SetData(int id, object data)
        {
            ClaritySettingData cs = new ClaritySettingData();
            //IDとCodeの設定
            cs.Id = id;
            cs.Code = id.ToString();

            //既存物がある場合上書き
            bool exid = this.DataDic.ContainsKey(id);                        
            if (exid == true)
            {
                cs = this.DataDic[id];                
            }

            

            //データtypeの割り出し
            Type t = data.GetType();
            EClaritySettingDataType dt, sdt;
            this.GetClaritySettingDataType(t, out dt, out sdt);
            cs.DataType = dt;
            cs.SubDataType = sdt;
            cs.Data = data;

            //追加
            this.AddManage(cs, true);

            return true;
        }

        /// <summary>
        /// Codeを使用して値を設定する
        /// </summary>
        /// <param name="code">Code</param>
        /// <param name="data">設定値</param>
        /// <returns>設定成功可否</returns>
        public bool SetData(string code, object data)
        {
            ClaritySettingData cs = new ClaritySettingData();
            cs.Id = this.DataDic.Count;
            cs.Code = code;

            //既存品がある場合上書き
            bool excode = this.DataDicKeyString.ContainsKey(code);
            if (excode == true)
            {
                cs = this.DataDicKeyString[code];
            }

            //データtypeの割り出し
            Type t = data.GetType();
            EClaritySettingDataType dt, sdt;
            this.GetClaritySettingDataType(t, out dt, out sdt);
            cs.DataType = dt;
            cs.SubDataType = sdt;
            cs.Data = data;

            //追加
            this.AddManage(cs, true);


            return true;
        }

        #endregion
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// Codeの重複チェック
        /// </summary>
        /// <param name="datalist"></param>
        /// <returns></returns>
        private bool CheckCodeDuplicate(List<ClaritySettingData> datalist)
        {
            //Codeだけを抜き出し
            var codelist = datalist.Select((x) => { return x.Code; });

            //一意にしたのち、数を確認
            var dclist = codelist.Distinct();


            int src_count = codelist.Count();
            int dclistcount = dclist.Count();

            if (src_count == dclistcount)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 設定値の取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        private T GetSetting<T>(int id)
        {
            ClaritySettingData data = this.DataDic[id];            
            return data.GetValue<T>();
        }

        /// <summary>
        /// 設定値配列の取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        private T[] GetSettingArray<T>(int id)
        {
            ClaritySettingData data = this.DataDic[id];
            object[] ovec = (object[])data.Data;

            T[] ans = ovec.Select(x => (T)x).ToArray();
            return ans;
        }


        /// <summary>
        /// 設定値の取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <returns></returns>
        private T GetSetting<T>(string code, T def)
        {
            try
            {
                return this.GetSetting<T>(code);
            }
            catch
            {
                return def;
            }

        }

        /// <summary>
        /// 設定値の取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <returns></returns>
        private T GetSetting<T>(string code)
        {            
            ClaritySettingData data = this.DataDicKeyString[code];
            T ans = (T)data.Data;
            return ans;
        }

        /// <summary>
        /// 設定値配列の取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <returns></returns>
        private T[] GetSettingArray<T>(string code)
        {
            ClaritySettingData data = this.DataDicKeyString[code];
            object[] ovec = (object[])data.Data;

            T[] ans = ovec.Select(x => (T)x).ToArray();
            return ans;
        }



        /// <summary>
        /// 管理へ追加
        /// </summary>
        /// <param name="cdata">追加データ</param>
        /// <param name="overwrite">既存のキーがあった場合の処理 true=上書き false=exception</param>
        private void AddManage(ClaritySettingData cdata, bool overwrite = false)
        {            
            //既存キーチェック
            bool ckret = this.DataDic.ContainsKey(cdata.Id);
            bool ckcoderet = this.DataDicKeyString.ContainsKey(cdata.Code);
            if ((ckret == true || ckcoderet == true) && overwrite == false)
            {
                throw new Exception($"AddManage Key exists id={cdata.Id} code={cdata.Code}");
            }

            //this.DataDic.Add(cdata.Id, cdata);
            //this.DataDicKeyString.Add(cdata.Code, cdata);
            this.DataDic[cdata.Id] = cdata;
            this.DataDicKeyString[cdata.Code] = cdata;
        }

        

        
    }


    
}
