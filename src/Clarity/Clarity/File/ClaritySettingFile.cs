using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Xml.Linq;
using System.IO;

namespace Clarity
{
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

        //Array_String,保留中　やるなら""で囲むなど仕様を定義せよ


        //------------------------------------
        MAX,
    }

    /// <summary>
    /// ノード情報種別
    /// </summary>
    public enum EClaritySettingNodeType
    {
        Node,
        Data,

        None,
    }


    /// <summary>
    /// 解析クラス規定
    /// </summary>
    public class BaseClaritySetting
    {
        internal const string PathDev = ".";


        /// <summary>
        /// 解析関数
        /// </summary>
        /// <param name="ele"></param>
        /// <param name="data"></param>
        private delegate object AnalyzeDataDelegate(XElement ele, EClaritySettingDataType type, EClaritySettingDataType subtype);

        /// <summary>
        /// ノード一行の解析　失敗=null
        /// </summary>
        /// <param name="epath">親のパス</param>
        /// <param name="ele">解析対象</param>
        /// <returns></returns>
        protected ClaritySettingData AnalyzeNode(ClaritySettingData parent, XElement ele)
        {
            ClaritySettingData ans = new ClaritySettingData();
            try
            {
                //Nodeタイプの設定
                ans.NodeType = EClaritySettingNodeType.Data;

                //Code
                ans.Code = this.CreateElementCode(parent, ele);

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
                    if (ans.DataType == EClaritySettingDataType.MAX)
                    {
                        return null;
                    }
                }

                //サブコード
                {
                    if (ans.DataType == EClaritySettingDataType.Array)
                    {
                        XAttribute asubtype = ele.Attribute("subtype");
                        if (asubtype == null)
                        {
                            throw new FormatException($"tag={ans.Code} pelase set subtype");
                        }
                        ans.SubDataType = this.IdentityDataType(asubtype.Value);
                        if (ans.SubDataType == EClaritySettingDataType.Array || ans.SubDataType == EClaritySettingDataType.MAX)
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



        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// Codeの作成
        /// </summary>
        /// <param name="epath">親名</param>
        /// <param name="ele">名前</param>
        /// <returns></returns>
        private string CreateElementCode(ClaritySettingData parent, XElement ele)
        {
            string ans = (parent != null) ? parent.Code + PathDev + ele.Name : ele.Name.LocalName;
            return ans;
        }

        /// <summary>
        /// データTypeの割り出し
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private EClaritySettingDataType IdentityDataType(string s)
        {
            //有効なもの一式
            (string ck, EClaritySettingDataType code)[] supportvec = {
                ("bool", EClaritySettingDataType.Bool),
                ("int", EClaritySettingDataType.Int),
                ("float", EClaritySettingDataType.Float),
                ("vec2", EClaritySettingDataType.Vec2),
                ("vec3", EClaritySettingDataType.Vec3),
                ("string", EClaritySettingDataType.String),
                ("array", EClaritySettingDataType.Array),
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

            return EClaritySettingDataType.MAX;
        }


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

    /// <summary>
    /// Clarity設定情報管理
    /// </summary>
    public class ClaritySettingData
    {
        /// <summary>
        /// Nodeかデータか
        /// </summary>
        public EClaritySettingNodeType NodeType = EClaritySettingNodeType.Node;

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
        public EClaritySettingDataType DataType = EClaritySettingDataType.MAX;
        /// <summary>
        /// DataTypeがArrayの場合の格納タイプ
        /// </summary>
        public EClaritySettingDataType SubDataType = EClaritySettingDataType.MAX;


        /// <summary>
        /// 親ノード
        /// </summary>
        public ClaritySettingData Parent = null;
        /// <summary>
        /// 子供一式
        /// </summary>
        public List<ClaritySettingData> ChildNode = new List<ClaritySettingData>();

    }

    /// <summary>
    /// ユーザー設定の読み込み
    /// </summary>
    internal class ClaritySettingFile : BaseClaritySetting
    {

        /// <summary>
        /// ユーザー設定の読み込み
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public List<ClaritySettingData> ReadSetting(string filepath)
        {
            int rid = 0;
            string rname;
            return this.ReadSetting(filepath, out rid, out rname);
        }

        /// <summary>
        /// ユーザー設定の読み込み
        /// </summary>
        /// <param name="filepath">読み込みパス</param>
        /// <param name="rid">読み込みroot_id</param>
        /// <returns></returns>
        public List<ClaritySettingData> ReadSetting(string filepath, out int rid, out string name)
        {
            List<ClaritySettingData> anslist = new List<ClaritySettingData>();
            try
            {
                int root_id = 0;
                string rname = "";
                using (FileStream fp = new FileStream(filepath, FileMode.Open))
                {
                    anslist = this.ReadSetting(fp, out root_id, out rname);
                }
                rid = root_id;
                name = rname;

                anslist.ForEach(x =>
                {
                    x.Id = root_id++;
                    if (rname.Length > 0)
                    {
                        x.Code = rname + "_" + x.Code;
                    }
                });
                
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
        public List<ClaritySettingData> ReadSetting(Stream st, out int rid, out string name)
        {
            List<ClaritySettingData> anslist = new List<ClaritySettingData>();
            try
            {
                XElement xml = XElement.Load(st);
                anslist = this.ReadNodes(null, xml, false);

                string s = xml.Attribute("root_id")?.Value ?? "0";
                rid = Convert.ToInt32(s);

                name = xml.Attribute("name")?.Value ?? "";
                
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
        private List<ClaritySettingData> ReadNodes(ClaritySettingData parent, XElement ele, bool adp)
        {
            List<ClaritySettingData> anslist = new List<ClaritySettingData>();

            //自身が解析できたか？
            {
                ClaritySettingData ans = this.AnalyzeNode(parent, ele);                
                if (ans != null)
                {
                    ans.Parent = parent;
                    parent?.ChildNode.Add(ans);
                    anslist.Add(ans);
                    return anslist;
                }
            }


            ClaritySettingData cp = null;
            //親パス追加許可がある
            if (adp == true)
            {
                //解析できないnodeは親nodeとして新たに登録する
                cp = new ClaritySettingData() { NodeType = EClaritySettingNodeType.Node };
                cp.Code = (parent != null) ? parent.Code + PathDev + ele.Name : ele.Name.LocalName;
                cp.TagName = ele.Name.LocalName;
                cp.Parent = parent;
                parent?.ChildNode.Add(cp);
                anslist.Add(cp);
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
                                
                //子供を解析
                List<ClaritySettingData> tl = this.ReadNodes(cp, em, true);
                parent?.ChildNode.AddRange(tl);
                anslist.AddRange(tl);
            }

            return anslist;
        }
    }

    /// <summary>
    /// 検索関数を作成しておく
    /// </summary>
    public static class ClaritySettingFileExtra
    {
        /// <summary>
        /// 対象nodeの検索
        /// </summary>
        /// <param name="datalist"></param>
        /// <param name="code"></param>
        /// <param name="ntype"></param>
        /// <returns></returns>
        internal static List<ClaritySettingData> SearchNode(this List<ClaritySettingData> datalist, string code, EClaritySettingNodeType ntype)
        {
            List<ClaritySettingData> anslist = new List<ClaritySettingData>();
            if (ntype == EClaritySettingNodeType.None)
            {
                anslist = datalist.Where(x => x.Code.Contains(code)).ToList();
            }
            else
            {
                anslist = datalist.Where(x => x.Code.Contains(code) && x.NodeType == ntype).ToList();
            }

            return anslist;
        }

        /// <summary>
        /// 対象種別のnodeを取得
        /// </summary>
        /// <param name="datalist"></param>
        /// <param name="ntype">ノード種別(None指定不可)</param>
        /// <returns></returns>
        internal static List<ClaritySettingData> GetSelectTypeNode(this List<ClaritySettingData> datalist, EClaritySettingNodeType ntype)
        {
            return datalist.Where(x => x.NodeType == ntype).ToList();
        }



        /// <summary>
        /// 対象nodeの先頭を取得
        /// </summary>
        /// <param name="datalist"></param>
        /// <param name="code"></param>
        /// <param name="ntype"></param>
        /// <returns></returns>
        internal static ClaritySettingData SearchNodeFirst(this List<ClaritySettingData> datalist, string code, EClaritySettingNodeType ntype)
        {
            ClaritySettingData ans = datalist.SearchNode(code, ntype).First();
            return ans;
        }



        /// <summary>
        /// データノードの検索
        /// </summary>
        /// <param name="datalist"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static List<ClaritySettingData> SearchDataNode(this List<ClaritySettingData> datalist, string code)
        {
            return datalist.SearchNode(code, EClaritySettingNodeType.Data);
        }
        /// <summary>
        /// データノードの検索し、先頭の一つを取得
        /// </summary>
        /// <param name="datalist"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ClaritySettingData SearchDataNodeFirst(this List<ClaritySettingData> datalist, string code)
        {
            return datalist.SearchNodeFirst(code, EClaritySettingNodeType.Data);
        }

        /// <summary>
        /// 親ノードの検索
        /// </summary>
        /// <param name="datalist"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static List<ClaritySettingData> SearchParentNode(this List<ClaritySettingData> datalist, string code)
        {
            return datalist.SearchNode(code, EClaritySettingNodeType.Node);
        }
        /// <summary>
        /// 親ノードの検索
        /// </summary>
        /// <param name="datalist"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ClaritySettingData SearchParentNodeFirst(this List<ClaritySettingData> datalist, string code)
        {
            return datalist.SearchNodeFirst(code, EClaritySettingNodeType.Node);
        }

        /// <summary>
        /// 先頭nodeの検索
        /// </summary>
        /// <param name="datalist"></param>
        /// <returns></returns>
        public static List<ClaritySettingData> SearchRootNode(this List<ClaritySettingData> datalist)
        {
            return datalist.Where(x => x.Parent == null).ToList();
        }
    }
}

