using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Clarity
{

    /// <summary>
    /// データコード
    /// </summary>
    public enum EClaritySettingFileDataType
    {
        Bool,
        Int,
        Float,
        Vec2,
        Vec3,
        String,
        Array,
        
        //Array_String,保留中　やるなら""で囲むなど仕様を定義せよ


        //------------------------------------
        MAX,
    }

    /// <summary>
    /// Clarity設定情報管理
    /// </summary>
    public class ClaritySettingData
    {
        /// <summary>
        /// データID
        /// </summary>
        public int Id = 0;
        /// <summary>
        /// データコード
        /// </summary>
        public string Code = "";

        /// <summary>
        /// これのタグ名
        /// </summary>
        public string TagName = "";

        /// <summary>
        /// 格納データ
        /// </summary>
        public object Data = null;
        /// <summary>
        /// データType
        /// </summary>
        public EClaritySettingFileDataType DataType = EClaritySettingFileDataType.MAX;
        /// <summary>
        /// DataTypeがArrayの場合の格納タイプ
        /// </summary>
        public EClaritySettingFileDataType SubDataType = EClaritySettingFileDataType.MAX;

    }


    /// <summary>
    /// 設定情報
    /// </summary>
    public class ClaritySetting
    {
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 管理データ AddManage関数を利用してADDする
        /// </summary>
        private Dictionary<int, ClaritySettingData> DataDic = null;

        /// <summary>
        /// 管理データ(文字をそのまま DataDicと内容は同等、キーアクセスを早くするための処置) AddManage関数を利用してADDする
        /// </summary>
        private Dictionary<string, ClaritySettingData> DataDicKeyString = null;
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//


        /// <summary>
        /// 設定の読み込み
        /// </summary>
        /// <param name="filepath">読み込みファイルパス</param>        
        public void Read(string filepath)
        {
            //ファイルの読み込み
            File.ClaritySettingFile fp = new File.ClaritySettingFile();
            List<ClaritySettingData> datalist = fp.ReadSetting(filepath);

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
                this.AddManage(x);
                id++;

            });

        }

        /// <summary>
        /// デフォルトデータの挿入
        /// </summary>
        /// <param name="datalist"></param>
        internal void InitDefault(List<(string code, object data)> datalist, bool overwrite)
        {
            //データの作成
            this.DataDic = new Dictionary<int, ClaritySettingData>();
            this.DataDicKeyString = new Dictionary<string, ClaritySettingData>();

            int id = 1;
            datalist.ForEach(x => {
                ClaritySettingData data = new ClaritySettingData();
                {
                    data.Id = id;
                    data.Code = x.code;
                    data.Data = x.data;

                    Type t = x.data.GetType();
                    this.GetClaritySettingFileDataType(t, out data.DataType, out data.SubDataType);
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
        public bool GetBool(string code, bool def)
        {
            return this.GetSetting<bool>(code, def);
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
        public float GetFloat(string code, float def)
        {
            return this.GetSetting<float>(code, def);
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
        public Vector2 GetVec2(string code, Vector2 def)
        {
            return this.GetSetting<Vector2>(code, def);
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
        public T GetEnum<T>(string code, T def) where T : Enum
        {
            string m = this.GetString(code, def.ToString());

            T ans = (T)Enum.Parse(typeof(T), m);
            return ans;
        }

        #endregion



        
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

            T ans = (T)data.Data;
            return ans;
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
                ClaritySettingData data = this.DataDicKeyString[code];

                T ans = (T)data.Data;
                return ans;
            }
            catch
            {
                return def;
            }

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

        

        /// <summary>
        /// EClaritySettingFileDataTypeの割り出し
        /// </summary>
        /// <param name="t"></param>
        /// <param name="type"></param>
        /// <param name="subtype"></param>
        private void GetClaritySettingFileDataType(Type t, out EClaritySettingFileDataType type, out EClaritySettingFileDataType subtype)
        {
            type = EClaritySettingFileDataType.MAX;
            subtype = EClaritySettingFileDataType.MAX;

            //メインタイプ取得
            type = this.GetClaritySettingFileDataType(t);

            //配列の場合はsubtype
            if (type == EClaritySettingFileDataType.Array)
            {
                Type subt = t.GetElementType();
                subtype = this.GetClaritySettingFileDataType(subt);
            }
        }

        /// <summary>
        /// EClaritySettingFileDataTypeの割り出し
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private EClaritySettingFileDataType GetClaritySettingFileDataType(Type t)
        {
            if (t.IsArray == true)
            {
                return EClaritySettingFileDataType.Array;
            }

            Dictionary<Type, EClaritySettingFileDataType> datadic = new Dictionary<Type, EClaritySettingFileDataType>();
            {
                datadic.Add(typeof(int), EClaritySettingFileDataType.Int);
                datadic.Add(typeof(float), EClaritySettingFileDataType.Float);
                datadic.Add(typeof(string), EClaritySettingFileDataType.String);
                datadic.Add(typeof(Vector2), EClaritySettingFileDataType.Vec2);
                datadic.Add(typeof(Vector3), EClaritySettingFileDataType.Vec3);
            }

            return datadic[t];
        }
    }


    
}
