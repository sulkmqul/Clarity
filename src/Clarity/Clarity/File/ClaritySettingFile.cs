using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Xml.Linq;
using System.IO;

namespace Clarity.File
{
    /// <summary>
    /// ユーザー設定の読み込み
    /// </summary>
    class ClaritySettingFile
    {
        const string PathDev = ".";


        /// <summary>
        /// 解析関数
        /// </summary>
        /// <param name="ele"></param>
        /// <param name="data"></param>
        private delegate object AnalyzeDataDelegate(XElement ele, EClaritySettingFileDataType type, EClaritySettingFileDataType subtype);


        /// <summary>
        /// ユーザー設定の読み込み
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public List<ClaritySettingData> ReadSetting(string filepath)
        {
            List<ClaritySettingData> anslist = new List<ClaritySettingData>();
            try
            {
                using (FileStream fp = new FileStream(filepath, FileMode.Open))
                {
                    anslist = this.ReadSetting(fp);
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClarityUserSettingFile ReadSetting", e);
            }

            return anslist;
        }

        /// <summary>
        /// ユーザー設定の読み込み
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public List<ClaritySettingData> ReadSetting(Stream st)
        {
            List<ClaritySettingData> anslist = new List<ClaritySettingData>();
            try
            {
                XElement xml = XElement.Load(st);
                anslist = this.ReadNodes(null, xml, false);
            }
            catch (Exception e)
            {
                throw new Exception("ClarityUserSettingFile ReadSetting", e);
            }

            return anslist;
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// XMLの読み込み
        /// </summary>
        /// <param name="epath">親パス nullで付与しない</param>
        /// <param name="ele">読み込みノード</param>
        /// <param name="adf">自身のパスを付与するか？ false=下にはepathnullで渡す(root用)</param>
        /// <returns></returns>
        private List<ClaritySettingData> ReadNodes(string? epath, XElement ele, bool adf)
        {
            List<ClaritySettingData> anslist = new List<ClaritySettingData>();

            //自身が解析できたか？
            {
                ClaritySettingData ans = this.AnalyzeNode(epath, ele);
                if (ans != null)
                {
                    anslist.Add(ans);
                    return anslist;
                }
            }

            //解析できない場合は、自身のパスを付与してもう一回
            var nodes = ele.Nodes();
            foreach (XNode node in nodes)
            {
                XElement em = node as XElement;
                if (em == null)
                {
                    continue;
                }

                string path = (epath != null) ? epath + PathDev + ele.Name : ele.Name.LocalName;
                if (adf == false) { path = null; }

                //子供を解析
                List<ClaritySettingData> tl = this.ReadNodes(path, em, true);
                anslist.AddRange(tl);
            }

            return anslist;
        }


        /// <summary>
        /// ノードの解析　失敗=null
        /// </summary>
        /// <param name="epath">親のパス</param>
        /// <param name="ele">解析対象</param>
        /// <returns></returns>
        private ClaritySettingData AnalyzeNode(string? epath, XElement ele)
        {
            ClaritySettingData ans = new ClaritySettingData();
            try
            {
                //Code
                ans.Code = this.CreateElementCode(epath, ele);

                //生のタグ名
                ans.TagName = ele.Name.LocalName;

                //データコード
                {
                    XAttribute atype = ele.Attribute("type");
                    if (atype == null)
                    {
                        return null;
                    }
                    ans.DataType = this.IdentityDataType(atype.Value);
                    if (ans.DataType == EClaritySettingFileDataType.MAX)
                    {
                        return null;
                    }
                }

                //サブコード
                {
                    if (ans.DataType == EClaritySettingFileDataType.Array)
                    {
                        XAttribute asubtype = ele.Attribute("subtype");
                        if (asubtype == null)
                        {
                            throw new FormatException($"tag={ans.Code} pelase set subtype");
                        }
                        ans.SubDataType = this.IdentityDataType(asubtype.Value);
                        if (ans.SubDataType == EClaritySettingFileDataType.Array || ans.SubDataType == EClaritySettingFileDataType.MAX)
                        {
                            throw new FormatException($"tag={ans.Code} invalid subtype");
                        }
                    }
                }


                //解析関数の取得
                AnalyzeDataDelegate aproc = this.GetAnalyzeProc(ans.DataType);
                ans.Data = aproc(ele, ans.DataType, ans.SubDataType);
            }
            catch (Exception ex)
            {
                throw new Exception($"AnalyzeNode name={ele.Name}", ex);
            }
            return ans;
        }

        /// <summary>
        /// Codeの作成
        /// </summary>
        /// <param name="epath">親名</param>
        /// <param name="ele">名前</param>
        /// <returns></returns>
        private string CreateElementCode(string? epath, XElement ele)
        {
            string ans = (epath != null) ? epath + PathDev + ele.Name : ele.Name.LocalName;
            return ans;
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// データTypeの割り出し
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public EClaritySettingFileDataType IdentityDataType(string s)
        {
            //有効なもの一式
            (string ck, EClaritySettingFileDataType code)[] supportvec = {
                ("bool", EClaritySettingFileDataType.Bool),
                ("int", EClaritySettingFileDataType.Int),
                ("float", EClaritySettingFileDataType.Float),
                ("vec2", EClaritySettingFileDataType.Vec2),
                ("vec3", EClaritySettingFileDataType.Vec3),
                ("string", EClaritySettingFileDataType.String),
                ("array", EClaritySettingFileDataType.Array),
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

            return EClaritySettingFileDataType.MAX;
        }


        /// <summary>
        /// 解析関数の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private AnalyzeDataDelegate GetAnalyzeProc(EClaritySettingFileDataType code)
        {
            Dictionary<EClaritySettingFileDataType, AnalyzeDataDelegate> adic = new Dictionary<EClaritySettingFileDataType, AnalyzeDataDelegate>();
            adic.Add(EClaritySettingFileDataType.Bool, this.AnalyzeDataBool);
            adic.Add(EClaritySettingFileDataType.Int, this.AnalyzeDataInt);
            adic.Add(EClaritySettingFileDataType.Float, this.AnalyzeDataFloat);
            adic.Add(EClaritySettingFileDataType.Vec2, this.AnalyzeDataVec2);
            adic.Add(EClaritySettingFileDataType.Vec3, this.AnalyzeDataVec3);
            adic.Add(EClaritySettingFileDataType.String, this.AnalyzeDataString);
            adic.Add(EClaritySettingFileDataType.Array, this.AnalyzeDataArray);

            AnalyzeDataDelegate ans = adic[code];
            return ans;
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 単体Boolの解析
        /// </summary>
        /// <param name="ele"></param>
        private object AnalyzeDataBool(XElement ele, EClaritySettingFileDataType type, EClaritySettingFileDataType subtype)
        {
            return Convert.ToBoolean(ele.Value);
        }
        /// <summary>
        /// 単体Intの解析
        /// </summary>
        /// <param name="ele"></param>
        private object AnalyzeDataInt(XElement ele, EClaritySettingFileDataType type, EClaritySettingFileDataType subtype)
        {
            return Convert.ToInt32(ele.Value);
        }
        /// <summary>
        /// Floatの解析
        /// </summary>
        /// <param name="ele"></param>
        private object AnalyzeDataFloat(XElement ele, EClaritySettingFileDataType type, EClaritySettingFileDataType subtype)
        {
            return Convert.ToSingle(ele.Value);
        }
        /// <summary>
        /// stringの解析
        /// </summary>
        /// <param name="ele"></param>
        private object AnalyzeDataString(XElement ele, EClaritySettingFileDataType type, EClaritySettingFileDataType subtype)
        {
            return ele.Value;
        }
        /// <summary>
        /// vec2の解析
        /// </summary>
        /// <param name="ele"></param>
        private object AnalyzeDataVec2(XElement ele, EClaritySettingFileDataType type, EClaritySettingFileDataType subtype)
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
        private object AnalyzeDataVec3(XElement ele, EClaritySettingFileDataType type, EClaritySettingFileDataType subtype)
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
        private object AnalyzeDataArray(XElement ele, EClaritySettingFileDataType type, EClaritySettingFileDataType subtype)
        {
            //配列の場合子ノードを変換する
            List<object> anslist = new List<object>();

            var nodes = ele.Nodes();
            foreach (XNode node in nodes)
            {   
                XElement em = node as XElement;
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
}
