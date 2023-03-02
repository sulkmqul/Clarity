using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Xml.Linq;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Clarity
{

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
    /// Clarity設定情報管理
    /// </summary>
    public class ClaritySettingData : ClarityData
    {
        /// <summary>
        /// Nodeかデータか
        /// </summary>
        internal EClaritySettingNodeType NodeType = EClaritySettingNodeType.Node;

        /// <summary>
        /// データID
        /// </summary>
        public int Id { get; internal set; } = 0;

        /// <summary>
        /// データコード
        /// </summary>
        public string Code { get; internal set; } = "";

        /// <summary>
        /// 親ノード
        /// </summary>
        internal ClaritySettingData? Parent  = null;
        /// <summary>
        /// 子供一式
        /// </summary>
        internal List<ClaritySettingData> ChildNode  = new List<ClaritySettingData>();


        #region 取得関数


        /// <summary>
        /// 親ノードの検索
        /// </summary>        
        /// <param name="code"></param>
        /// <returns></returns>
        public List<ClaritySettingData> SearchParentNode(string code)
        {
            return this.SearchNode(code, EClaritySettingNodeType.Node);
        }
        /// <summary>
        /// 親ノードの検索
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ClaritySettingData SearchParentNodeFirst(string code)
        {
            return this.SearchNodeFirst(code, EClaritySettingNodeType.Node);
        }
        /// <summary>
        /// 親ノードの検索
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClaritySettingData SearchParentNodeFirst(int  id)
        {
            return this.SearchNodeFirst(id, EClaritySettingNodeType.Node);
        }

        /// <summary>
        /// データの取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<T> GetDataList<T>(string code)
        {
            var dlist = this.SearchDataNode(code);
            return dlist.Select(x => (T)x.Data).ToList();
        }

        /// <summary>
        /// データを先頭の一つ取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <returns></returns>
        public T GetDataFirst<T>(string code)
        {
            return (T)this.SearchDataNodeFirst(code)?.Data;

        }

        /// <summary>
        /// デフォルト値付きデータ取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public T GetDataFirst<T>(string code, T def)
        {
            var a = this.SearchDataNodeFirst(code);
            if (a == null)
            {
                return def;
            }
            return (T)a.Data;
        }

        /// <summary>
        /// データの取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetDataFirst<T>(int id)
        {
            return (T)this.SearchDataNodeFirst(id).Data;
        }

        /// <summary>
        /// デフォルト付きデータ取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public T GetDataFirst<T>(int id, T def)
        {
            var a = this.SearchDataNodeFirst(id);
            if (a == null)
            {
                return def;
            }
            return (T)a.Data;
        }

        /// <summary>
        /// Enum情報の取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public T GetDataEnum<T>(string code, T def) where T : Enum
        {
            string s = this.GetDataFirst<string>(code);

            //指定型に変換可能？
            object o;
            bool f = Enum.TryParse(typeof(T), s, out o);
            if (f == false)
            {
                return def;
            }

            T ans = (T)Enum.Parse(typeof(T), s);
            return ans;
        }

        /// <summary>
        /// Enum情報の取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public T GetDataEnum<T>(int id, T def) where T : Enum
        {
            string s = this.GetDataFirst<string>(id);

            //指定型に変換可能？
            object o;
            bool f = Enum.TryParse(typeof(T), s, out o);
            if (f == false)
            {
                return def;
            }

            T ans = (T)Enum.Parse(typeof(T), s);
            return ans;
        }

        #region Node検索各種        
        /// <summary>
        /// データノードの検索
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal List<ClaritySettingData> SearchDataNode(string code)
        {
            return this.SearchNode(code, EClaritySettingNodeType.Data);
        }
        /// <summary>
        /// データノードの検索し、先頭の一つを取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal ClaritySettingData SearchDataNodeFirst(string code)
        {
            return this.SearchNodeFirst(code, EClaritySettingNodeType.Data);
        }

        /// <summary>
        /// データノードの検索
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal ClaritySettingData SearchDataNodeFirst(int id)
        {
            return this.SearchNodeFirst(id, EClaritySettingNodeType.Data);
        }

        /// <summary>
        /// 対象nodeの先頭を取得
        /// </summary>
        /// <param name="code"></param>
        /// <param name="ntype"></param>
        /// <returns></returns>
        internal ClaritySettingData SearchNodeFirst(string code, EClaritySettingNodeType ntype)
        {
            ClaritySettingData ans = this.SearchNode(code, ntype).FirstOrDefault();
            return ans;
        }
        /// <summary>
        /// 対象Nodeの先頭を取得
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ntype"></param>
        /// <returns></returns>
        internal ClaritySettingData SearchNodeFirst(int id, EClaritySettingNodeType ntype)
        {
            ClaritySettingData ans = this.SearchNode(id, ntype).FirstOrDefault();
            return ans;
        }

        /// <summary>
        /// 対象nodeの検索
        /// </summary>        
        /// <param name="code">検索code</param>
        /// <param name="ntype">nodeタイプ</param>
        /// <returns></returns>
        internal List<ClaritySettingData> SearchNode(string code, EClaritySettingNodeType ntype)
        {
            List<ClaritySettingData> anslist = new List<ClaritySettingData>();
            if (ntype == EClaritySettingNodeType.None)
            {
                anslist = this.ChildNode.Where(x => x.Code.Equals(code)).ToList();
            }
            else
            {
                anslist = this.ChildNode.Where(x => x.Code.Equals(code) && x.NodeType == ntype).ToList();
            }

            return anslist;
        }

        /// <summary>
        /// 対象Nodeの検索(IDは被らない想定だが、統一感もあるし一応)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ntype"></param>
        /// <returns></returns>
        internal List<ClaritySettingData> SearchNode(int id, EClaritySettingNodeType ntype)
        {
            List<ClaritySettingData> anslist = new List<ClaritySettingData>();
            if (ntype == EClaritySettingNodeType.None)
            {
                anslist = this.ChildNode.Where(x => x.Id == id).ToList();
            }
            else
            {
                anslist = this.ChildNode.Where(x => x.Id == id && x.NodeType == ntype).ToList();
            }

            return anslist;
        }
        #endregion

        #endregion


    }


    /// <summary>
    /// Clarity設定ファイル読み込み基底
    /// </summary>
    public class BaseClaritySetting : ClaritySettingCore
    {
        internal const string PathDev = ".";


        /// <summary>
        /// ノード一行の解析　失敗=null
        /// </summary>
        /// <param name="epath">親のパス</param>
        /// <param name="ele">解析対象</param>
        /// <returns></returns>
        protected ClaritySettingData? AnalyzeNode(ClaritySettingData? parent, XElement ele)
        {
            ClaritySettingData? ans = null;

            try
            {
                ans = this.AnalyzeNodeCore<ClaritySettingData>(ele);
                if (ans == null)
                {
                    return null;
                }

                //Nodeタイプの設定
                ans.NodeType = EClaritySettingNodeType.Data;

                //Code
                ans.Code = this.CreateElementCode(parent, ans.TagName);


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
        /// <param name="tagname">名前</param>
        /// <returns></returns>
        private string CreateElementCode(ClaritySettingData? parent, string tagname)
        {
            string ans = (parent != null) ? parent.Code + PathDev + tagname : tagname;
            return ans;
        }

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

                //データIDの割り当て
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
                throw new Exception("ClaritySettingFile ReadSetting", e);
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
                throw new Exception("ClaritySettingFile ReadSetting", e);
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
        private List<ClaritySettingData> ReadNodes(ClaritySettingData? parent, XElement ele, bool adp)
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
                XElement? em = node as XElement;
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

