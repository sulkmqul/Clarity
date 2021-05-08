using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace Clarity.File
{
    enum EClarityUserSettingDataCode
    {
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

    class ClarityUserSettingData
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
        /// 格納データ
        /// </summary>
        public object Data = null;
        /// <summary>
        /// データType
        /// </summary>
        public EClarityUserSettingDataCode DataCode = EClarityUserSettingDataCode.MAX;
        /// <summary>
        /// DataTypeがArrayの場合の格納タイプ
        /// </summary>
        public EClarityUserSettingDataCode SubDataCode = EClarityUserSettingDataCode.MAX;

    }

    /// <summary>
    /// 設定情報
    /// </summary>
    public class ClarityUserSetting
    {
        /// <summary>
        /// 管理データ
        /// </summary>
        private Dictionary<int, ClarityUserSettingData> DataDic = null;

        /// <summary>
        /// Codeの重複チェック
        /// </summary>
        /// <param name="datalist"></param>
        /// <returns></returns>
        private bool CheckCodeDuplicate(List<ClarityUserSettingData> datalist)
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
        /// 設定の読み込み
        /// </summary>
        /// <param name="filepath"></param>
        public void LoadSetting(string filepath)
        {
            //ファイルの読み込み
            ClarityUserSettingFile fp = new ClarityUserSettingFile();
            List<ClarityUserSettingData>  datalist = fp.ReadSetting(filepath);

            //コード重複チェック
            bool dcret = this.CheckCodeDuplicate(datalist);
            if (dcret == false)
            {
                throw new Exception("Code重複が存在します");

            }

            //データの作成
            this.DataDic = new Dictionary<int, ClarityUserSettingData>();
            datalist.ForEach(x =>
            {
                this.DataDic.Add(x.Id, x);
            });

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
        /// 設定値の取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        private T GetSetting<T>(int id)
        {
            ClarityUserSettingData data = this.DataDic[id];

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
            ClarityUserSettingData data = this.DataDic[id];
            object[] ovec = (object[])data.Data;

            T[] ans = ovec.Select(x => (T)x).ToArray();
            return ans;
        }


    }


    /// <summary>
    /// ユーザー設定の読み込み
    /// </summary>
    class ClarityUserSettingFile : BaseCsvFile
    {

        /// <summary>
        /// データ解析 data.Dataに値が入る。data.Dataは初めに必ず初期化を行うこと
        /// </summary>
        /// <param name="pos">解析位置</param>
        /// <param name="fline">データ一式</param>
        /// <returns></returns>
        private delegate void AnalyzeDataDelegate(ref int pos, string[] fline, ClarityUserSettingData data);


        /// <summary>
        /// ユーザー設定の読み込み
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public List<ClarityUserSettingData> ReadSetting(string filepath)
        {
            List<ClarityUserSettingData> anslist = new List<ClarityUserSettingData>();
            try
            {
                //CSVの読み込み
                List<string[]> datalist = this.ReadCsvFile(filepath);

                int id = 1;
                datalist.ForEach(line =>
                {
                    ClarityUserSettingData data = this.AnalyzeLine(line);
                    data.Id = id;
                    anslist.Add(data);
                    id++;

                });

            }
            catch (Exception e)
            {
                throw new Exception("ClarityUserSettingFile ReadSetting", e);
            }

            return anslist;
        }

        /// <summary>
        /// データTypeの割り出し
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public EClarityUserSettingDataCode IdentityDataCode(string s)
        {
            //有効なもの一式
            (string ck, EClarityUserSettingDataCode code)[] supportvec = {
                ("int", EClarityUserSettingDataCode.Int),
                ("float", EClarityUserSettingDataCode.Float),
                ("vec2", EClarityUserSettingDataCode.Vec2),
                ("vec3", EClarityUserSettingDataCode.Vec3),
                ("string", EClarityUserSettingDataCode.String),
                ("array", EClarityUserSettingDataCode.Array),
            };

            foreach (var supp in supportvec)
            {
                string src = s.ToUpper();
                string sp = supp.ck.ToUpper();

                if (src.Equals(sp) == true)
                {
                    return supp.code;
                }
            }

            return EClarityUserSettingDataCode.MAX;
        }


        /// <summary>
        /// 解析関数の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private AnalyzeDataDelegate GetAnalyzeProc(EClarityUserSettingDataCode code)
        {
            Dictionary<EClarityUserSettingDataCode, AnalyzeDataDelegate> adic = new Dictionary<EClarityUserSettingDataCode, AnalyzeDataDelegate>();
            adic.Add(EClarityUserSettingDataCode.Int, this.AnalyzeDataInt);
            adic.Add(EClarityUserSettingDataCode.Float, this.AnalyzeDataFloat);
            adic.Add(EClarityUserSettingDataCode.Vec2, this.AnalyzeDataVec2);
            adic.Add(EClarityUserSettingDataCode.Vec3, this.AnalyzeDataVec3);
            adic.Add(EClarityUserSettingDataCode.String, this.AnalyzeDataString);
            adic.Add(EClarityUserSettingDataCode.Array, this.AnalyzeDataArray);

            AnalyzeDataDelegate ans = adic[code];
            return ans;
        }

        /// <summary>
        /// 一行の解析
        /// </summary>
        /// <param name="fline"></param>
        /// <returns></returns>
        private ClarityUserSettingData AnalyzeLine(string[] fline)
        {
            ClarityUserSettingData ans = new ClarityUserSettingData();

            int pos = 0;

            //Code
            ans.Code = fline[pos];
            pos++;

            //設定データコード            
            string sdtype = fline[pos];
            ans.DataCode = this.IdentityDataCode(sdtype);
            pos++;
            if (ans.DataCode == EClarityUserSettingDataCode.MAX)
            {
                throw new Exception("無効な設定データコードです code=" + sdtype);
            }


            //解析関数の取得
            AnalyzeDataDelegate aproc = this.GetAnalyzeProc(ans.DataCode);

            try
            {
                aproc(ref pos, fline, ans);
            }
            catch (Exception e)
            {
                throw new Exception("データの解析が行えません code=" + ans.Code);
            }
            return ans;
        }



        /// <summary>
        /// 配列の解析
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="fline"></param>
        /// <param name="data"></param>
        private void AnalyzeDataArray(ref int pos, string[] fline, ClarityUserSettingData data)
        {

            //データ数の取得
            int count = Convert.ToInt32(fline[pos]);
            pos++;

            //SubTypeの解析
            string subtype = fline[pos];
            data.SubDataCode = this.IdentityDataCode(subtype);
            pos++;
            if (data.SubDataCode == EClarityUserSettingDataCode.MAX || data.SubDataCode == EClarityUserSettingDataCode.Array)
            {
                throw new Exception("無効な配列設定データコードです code=" + subtype);
            }

            

            //解析関数の取得
            AnalyzeDataDelegate aproc = this.GetAnalyzeProc(data.SubDataCode);


            List<object> vlist = new List<object>();            
            for (int i = 0; i < count; i++)
            {
                ClarityUserSettingData temp = new ClarityUserSettingData();

                try
                {                    
                    aproc(ref pos, fline, temp);
                }
                catch (IndexOutOfRangeException ioe)
                {
                    //これは範囲外なのでフォルト構成にするため、無視
                }

                //AnalyzeDataDelegateでは必ず初期化が入る構成にしているので、最低でも初期値が入っているはずである
                vlist.Add(temp.Data);

            }


            data.Data = vlist.ToArray();

            
        }




        /// <summary>
        /// DataCode Intの解析
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="fline"></param>
        /// <param name="data"></param>
        private void AnalyzeDataInt(ref int pos, string[] fline, ClarityUserSettingData data)
        {
            //値
            data.Data = 0;
            data.Data = Convert.ToInt32(fline[pos]);
            pos++;
        }


        /// <summary>
        /// DataCode Floatの解析
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="fline"></param>
        /// <param name="data"></param>
        private void AnalyzeDataFloat(ref int pos, string[] fline, ClarityUserSettingData data)
        {
            data.Data = 0.0f;
            data.Data = Convert.ToSingle(fline[pos]);
            pos++;
        }


        /// <summary>
        /// DataCode stringの解析
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="fline"></param>
        /// <param name="data"></param>
        private void AnalyzeDataString(ref int pos, string[] fline, ClarityUserSettingData data)
        {
            data.Data = "";
            data.Data = fline[pos];
            pos++;
        }



        /// <summary>
        /// Vector2の解析
        /// </summary>
        /// <param name="pos">解析位置</param>
        /// <param name="fline">解析元データ</param>
        /// <returns></returns>
        private Vector2 AnalayzeVec2(ref int pos, string[] fline)
        {
            Vector2 ans = new Vector2();

            try
            {
                ans.X = Convert.ToSingle(fline[pos]);
                pos++;
                ans.Y = Convert.ToSingle(fline[pos]);
                pos++;
            }
            catch (IndexOutOfRangeException ioe)
            {
                //IndexOutOfRangeExceptionはデフォルトとしたいので無視で良い
            }
            catch (Exception e)
            {
                throw e;
            }
            return ans;
        }

        /// <summary>
        /// Vector3の解析
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="fline"></param>
        /// <returns></returns>
        private Vector3 AnalayzeVec3(ref int pos, string[] fline)
        {
            Vector3 ans = new Vector3();

            try
            {
                ans.X = Convert.ToSingle(fline[pos]);
                pos++;
                ans.Y = Convert.ToSingle(fline[pos]);
                pos++;
                ans.Z = Convert.ToSingle(fline[pos]);
                pos++;
            }
            catch (IndexOutOfRangeException ioe)
            {
                //IndexOutOfRangeExceptionはデフォルトとしたいので無視で良い
            }
            catch (Exception e)
            {
                throw e;
            }
            return ans;
        }


        /// <summary>
        /// Vec2の解析
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="fline"></param>
        /// <param name="data"></param>
        private void AnalyzeDataVec2(ref int pos, string[] fline, ClarityUserSettingData data)
        {
            data.Data = new Vector2(0.0f);
            data.Data = this.AnalayzeVec2(ref pos, fline);
        }


        /// <summary>
        /// Vec3の解析
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="fline"></param>
        /// <param name="data"></param>
        private void AnalyzeDataVec3(ref int pos, string[] fline, ClarityUserSettingData data)
        {
            data.Data = new Vector3(0.0f);
            data.Data = this.AnalayzeVec3(ref pos, fline);
        }
    }
}
