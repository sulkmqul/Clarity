using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Clarity
{
    /// <summary>
    /// clairty settingの一行だけを表すデータ
    /// </summary>
    public class ClarityData
    {

        /// <summary>
        /// これのタグ名
        /// </summary>
        public string TagName { get; set; } = "";
        /// <summary>
        /// 格納データ
        /// </summary>
        public object Data = 0;

        /// <summary>
        /// データType
        /// </summary>
        public EClaritySettingDataType DataType { get; internal set; }  = EClaritySettingDataType.MAX;
        /// <summary>
        /// DataTypeがArrayの場合の格納タイプ
        /// </summary>
        public EClaritySettingDataType SubDataType { get; internal set; } = EClaritySettingDataType.MAX;


        /// <summary>
        /// 設定値の取得
        /// </summary>
        /// <typeparam name="T"></typeparam>        
        /// <returns></returns>
        public T GetValue<T>()
        {
            T ans = (T)this.Data;
            return ans;
        }
    }

    /// <summary>
    /// ClaritySettingのコア処理、構文の一行を解析する。
    /// </summary>
    public class ClaritySettingCore
    {
        /// <summary>
        /// 有効なDataType一式 type文字列←→EClaritySettingDataType
        /// </summary>
        internal static readonly (string ck, EClaritySettingDataType code)[] SupportDataTypeList = {
                ("bool", EClaritySettingDataType.Bool),
                ("int", EClaritySettingDataType.Int),
                ("float", EClaritySettingDataType.Float),
                ("vec2", EClaritySettingDataType.Vec2),
                ("vec3", EClaritySettingDataType.Vec3),
                ("string", EClaritySettingDataType.String),
                ("array", EClaritySettingDataType.Array),
        };

        /// <summary>
        /// 型とTypeの一式
        /// </summary>
        internal static readonly (Type type, EClaritySettingDataType code)[] DataTypeMap = {
                (typeof(bool), EClaritySettingDataType.Bool),
                (typeof(int), EClaritySettingDataType.Int),
                (typeof(float), EClaritySettingDataType.Float),
                (typeof(Vector2), EClaritySettingDataType.Vec2),
                (typeof(Vector3), EClaritySettingDataType.Vec3),
                (typeof(string), EClaritySettingDataType.String),                
        };



        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// Nodeの解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ele"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        protected T? AnalyzeNodeCore<T>(XElement ele) where T : ClarityData, new()
        {
            T ans = new T();

            //生のタグ名
            ans.TagName = ele.Name.LocalName;

            //データコード
            {

                XAttribute? atype = ele.Attribute("type");
                if (atype == null)
                {
                    return null;
                }
                ans.DataType = this.IdentityDataType(atype.Value);
                if (ans.DataType == EClaritySettingDataType.MAX)
                {
                    return null;
                }
            }

            //サブコード
            {
                if (ans.DataType == EClaritySettingDataType.Array)
                {
                    XAttribute? asubtype = ele.Attribute("subtype");
                    if (asubtype == null)
                    {
                        throw new FormatException($"tag={ans.TagName} pelase set subtype");
                    }
                    ans.SubDataType = this.IdentityDataType(asubtype.Value);
                    if (ans.SubDataType == EClaritySettingDataType.Array || ans.SubDataType == EClaritySettingDataType.MAX)
                    {
                        throw new FormatException($"tag={ans.TagName} invalid subtype");
                    }
                }
            }


            //解析関数の取得
            AnalyzeDataDelegate aproc = this.GetAnalyzeProc(ans.DataType);
            ans.Data = aproc(ele, ans.DataType, ans.SubDataType);

            return ans;
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// EClaritySettingFileDataTypeの割り出し
        /// </summary>
        /// <param name="t"></param>
        /// <param name="type"></param>
        /// <param name="subtype"></param>
        internal void GetClaritySettingDataType(Type t, out EClaritySettingDataType type, out EClaritySettingDataType subtype)
        {
            type = EClaritySettingDataType.MAX;
            subtype = EClaritySettingDataType.MAX;

            //メインタイプ取得
            type = this.GetClaritySettingDataType(t);

            //配列の場合はsubtypeの取得
            if (type == EClaritySettingDataType.Array)
            {
                Type? subt = t.GetElementType();
                if (subt == null)
                {
                    return;
                }
                subtype = this.GetClaritySettingDataType(subt);
            }
        }

        /// <summary>
        /// EClaritySettingFileDataTypeの割り出し
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        internal EClaritySettingDataType GetClaritySettingDataType(Type t)
        {
            if (t.IsArray == true)
            {
                return EClaritySettingDataType.Array;
            }

            //対象の検索
            var a = from f in DataTypeMap where f.type == t select f.code;
            if (a.Count() <= 0)
            {
                throw new Exception("no supported clarity types.");
            }

            return a.First();
        }


        /// <summary>
        /// 解析関数
        /// </summary>
        /// <param name="ele"></param>
        /// <param name="data"></param>
        internal delegate object AnalyzeDataDelegate(XElement ele, EClaritySettingDataType type, EClaritySettingDataType subtype);

        /// <summary>
        /// DataTypeからTag用文字列の割り出し
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected string IdentityDataType(EClaritySettingDataType type)
        {
            //有効なもの一式
            var supportvec = SupportDataTypeList;

            foreach (var supp in supportvec)
            {
                if (supp.code == type)
                {
                    return supp.ck;
                }
            }

            return "";
        }

        /// <summary>
        /// データTypeの割り出し
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        protected EClaritySettingDataType IdentityDataType(string s)
        {
            //有効なもの一式
            var supportvec = SupportDataTypeList;

            foreach (var supp in supportvec)
            {
                string src = s.ToUpper();
                string sp = supp.ck.ToUpper();

                if (src.Equals(sp) == true)
                {
                    return supp.code;
                }
            }

            return EClaritySettingDataType.MAX;
        }



        /// <summary>
        /// データのケアをする
        /// </summary>
        /// <param name="data">tostring死体データ</param>
        /// <returns></returns>
        /// <remarks>
        /// numeric.vectorをtoStringすると＜＞という余計な文字列が入るので取り除く
        /// </remarks>
        protected string ToSafeString(object data)
        {
            string a = data.ToString() ?? "";
            return a.Replace("<", "").Replace(">", "");
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 解析関数の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private AnalyzeDataDelegate GetAnalyzeProc(EClaritySettingDataType code)
        {
            Dictionary<EClaritySettingDataType, AnalyzeDataDelegate> adic = new Dictionary<EClaritySettingDataType, AnalyzeDataDelegate>();
            adic.Add(EClaritySettingDataType.Bool, this.AnalyzeDataBool);
            adic.Add(EClaritySettingDataType.Int, this.AnalyzeDataInt);
            adic.Add(EClaritySettingDataType.Float, this.AnalyzeDataFloat);
            adic.Add(EClaritySettingDataType.Vec2, this.AnalyzeDataVec2);
            adic.Add(EClaritySettingDataType.Vec3, this.AnalyzeDataVec3);
            adic.Add(EClaritySettingDataType.String, this.AnalyzeDataString);
            adic.Add(EClaritySettingDataType.Array, this.AnalyzeDataArray);

            AnalyzeDataDelegate ans = adic[code];
            return ans;
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 単体Boolの解析
        /// </summary>
        /// <param name="ele"></param>
        private object AnalyzeDataBool(XElement ele, EClaritySettingDataType type, EClaritySettingDataType subtype)
        {
            return Convert.ToBoolean(ele.Value);
        }
        /// <summary>
        /// 単体Intの解析
        /// </summary>
        /// <param name="ele"></param>
        private object AnalyzeDataInt(XElement ele, EClaritySettingDataType type, EClaritySettingDataType subtype)
        {
            return Convert.ToInt32(ele.Value);
        }
        /// <summary>
        /// Floatの解析
        /// </summary>
        /// <param name="ele"></param>
        private object AnalyzeDataFloat(XElement ele, EClaritySettingDataType type, EClaritySettingDataType subtype)
        {
            return Convert.ToSingle(ele.Value);
        }
        /// <summary>
        /// stringの解析
        /// </summary>
        /// <param name="ele"></param>
        private object AnalyzeDataString(XElement ele, EClaritySettingDataType type, EClaritySettingDataType subtype)
        {
            return ele.Value;
        }
        /// <summary>
        /// vec2の解析
        /// </summary>
        /// <param name="ele"></param>
        private object AnalyzeDataVec2(XElement ele, EClaritySettingDataType type, EClaritySettingDataType subtype)
        {
            Vector2 v = new Vector2(0.0f);

            string[] svec = ele.Value.Split(",");
            v.X = Convert.ToSingle(svec[0]);
            v.Y = Convert.ToSingle(svec[1]);
            return v;
        }
        /// <summary>
        /// vec2の解析
        /// </summary>
        /// <param name="ele"></param>
        private object AnalyzeDataVec3(XElement ele, EClaritySettingDataType type, EClaritySettingDataType subtype)
        {
            Vector3 v = new Vector3(0.0f);


            string[] svec = ele.Value.Split(",");
            v.X = Convert.ToSingle(svec[0]);
            v.Y = Convert.ToSingle(svec[1]);
            v.Z = Convert.ToSingle(svec[2]);

            return v;
        }


        /// <summary>
        /// 配列の解析
        /// </summary>
        /// <param name="ele"></param>
        /// <param name="type"></param>
        /// <param name="subtype"></param>
        /// <returns></returns>
        private object AnalyzeDataArray(XElement ele, EClaritySettingDataType type, EClaritySettingDataType subtype)
        {
            //配列の場合子ノードを変換する
            List<object> anslist = new List<object>();

            var nodes = ele.Nodes();
            foreach (XNode node in nodes)
            {
                XElement? em = node as XElement;
                if (em == null)
                {
                    continue;
                }

                //サブタイプで解析を行う
                AnalyzeDataDelegate aproc = this.GetAnalyzeProc(subtype);
                object ans = aproc(em, subtype, subtype);
                anslist.Add(ans);
            }

            return anslist.ToArray();
        }
    }

    /// <summary>
    /// データコード
    /// </summary>
    public enum EClaritySettingDataType
    {
        Bool,
        Int,
        Float,
        Vec2,
        Vec3,
        String,
        Array,

        //------------------------------------
        MAX,
    }

    
    /// <summary>
    /// ClaritySetting形式の一行を読み込むもの
    /// </summary>
    public class ClaritySettingCoreReader : ClaritySettingCore
    {
        /// <summary>
        /// 一行の解析
        /// </summary>
        /// <param name="ele">解析行</param>
        /// <returns></returns>
        public ClarityData? Analyze(XElement ele)
        {
            return this.Analyze<ClarityData>(ele);
        }

        /// <summary>
        /// xml形式文字列の解析
        /// </summary>
        /// <param name="estring">読み込み対象xml形式文字列</param>
        /// <returns></returns>
        public ClarityData? Analyze(string estring)
        {
            return this.Analyze<ClarityData>(estring);
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ele"></param>
        /// <returns></returns>
        public T? Analyze<T>(XElement ele) where T : ClarityData, new()
        {
            return this.AnalyzeNodeCore<T>(ele);
        }

        /// <summary>
        /// xml形式の文字列から解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="estring"></param>
        /// <returns></returns>
        public T? Analyze<T>(string estring) where T : ClarityData, new()
        {
            XElement ele = XElement.Parse(estring);
            return this.Analyze<T>(ele);
        }

        /// <summary>
        /// データタイプの値の文字列からデータを取得する(array以外)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt">データタイプ</param>
        /// <param name="valuestring">値</param>
        /// <param name="tagname">タグ名(optional)</param>
        /// <returns></returns>
        /// <remarks>
        /// dt=vec2
        /// valuestring=12,15
        /// ので渡すとvec2のデータ入りを作成する
        /// </remarks>
        public T? Analyze<T>(EClaritySettingDataType dt, string valuestring, string tagname="tag") where T : ClarityData, new()
        {
            //疑似的にxmlを作成する
            XElement ele = new XElement(tagname);
            string dname = this.IdentityDataType(dt);
            ele.Add(new XAttribute("type", dname));
            ele.Value = valuestring;            
            
            //解析させる、
            return this.Analyze<T>(ele);
        }

    }


    /// <summary>
    /// 一行の書き込みの作成
    /// </summary>
    public class ClaritySettingCoreWriter : ClaritySettingCore
    {
        /// <summary>
        /// 書き込み用一行データの作成
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public XElement CreateElement(ClarityData data)
        {
            //タグの書き込み
            XElement ans = new XElement(data.TagName);

            //自身のデータtypeの割り出し
            Type datattpe = data.Data.GetType();
            EClaritySettingDataType ctype, subtype;
            this.GetClaritySettingDataType(datattpe, out ctype, out subtype);

            //typeから書き込みtype文字列の割り出し
            string wtype = this.IdentityDataType(ctype);
            string wsubtype = this.IdentityDataType(subtype);

            //type
            ans.Add(new XAttribute("type", wtype));

            //配列でないなら値を設定して終了
            if (ctype != EClaritySettingDataType.Array)
            {
                ans.Value = this.ToSafeString(data.Data);
                return ans;
            }

            //以下配列の場合------------------------------------------------------------------------
            //subtypeの書き込み
            ans.Add(new XAttribute("subtype", wsubtype));

            //配列なら必ずあるであろうGetEnumeratorを起動する。
            MethodInfo? getemufunc = datattpe.GetMethod("GetEnumerator");
            var dataenume = (IEnumerator?)getemufunc?.Invoke(data.Data, new object[] { });
            if (dataenume == null)
            {
                throw new Exception("array enumerator is not invoked");
            }
            //取得したenumeratorを回して配列要素を書き込む
            while (dataenume.MoveNext())
            {
                XElement c = new XElement("Value");
                c.Value = this.ToSafeString(dataenume.Current);
                ans.Add(c);
            }

            return ans;
        }


    }

}
