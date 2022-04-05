using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Clarity;

namespace Clarity.Engine.Element
{
    /// <summary>
    /// 構造Element
    /// </summary>
    internal class ClarityStructure : BaseElement
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="code">構造コード</param>
        /// <param name="oid">ID</param>
        public ClarityStructure(string code, long oid) : base(oid)
        {
            this.Code = code;
        }

        /// <summary>
        /// これのコード
        /// </summary>
        public string Code = "";
    }

    /// <summary>
    /// 構造ファイルの読み込み
    /// </summary>
    internal class ClarityStructureFile
    {
        const string NodeIdentitiy = "Node";

        /// <summary>
        /// 構造ファイルの読み込み
        /// </summary>
        /// <param name="filepath">読み込み構造ファイルパス</param>
        /// <param name="rootid">構造開始ID</param>
        /// <param name="parent">構造作成親 nullで新たに作成</param>
        /// <returns>読み込み構造を作成した先頭node</returns>
        public ClarityStructure ReadStructure(string filepath, ClarityStructure parent = null)
        {
            ClarityStructure ans = new ClarityStructure("root", 0);

            using (FileStream fp = new FileStream(filepath, FileMode.Open))
            {
                ans = this.ReadStructure(fp, parent);
            }

            return ans;
        }

        /// <summary>
        /// 構造の読み込み
        /// </summary>
        /// <param name="st">読み込みstream</param>
        /// <param name="rootid">構造開始ID</param>
        /// <param name="parent">構造作成親 nullで新規</param>
        /// <returns></returns>
        public ClarityStructure ReadStructure(Stream st, ClarityStructure parent = null)
        {
            ClarityStructure ans = parent;
            if (ans == null)
            {
                ans = new ClarityStructure("root", 0);
            }
            ans.SystemLink.ChildList.Clear();

            try
            {
                long rid = 1;
                XElement xml = XElement.Load(st);

                //読み込み                
                ans = this.ReadNodes(xml, ref rid, ans);

            }
            catch (Exception ex)
            {
                throw new Exception("ClarityStructureFile.ReadStructure()", ex);
            }


            return ans;
        }

        /// <summary>
        /// ノードの読み込み
        /// </summary>
        /// <param name="xml">NodesXML</param>
        /// <param name="parent">接続親 nullでない場合はrootであるため、nodeを読み込まない</param>
        /// <returns></returns>
        private ClarityStructure ReadNodes(XElement xml, ref long rid, ClarityStructure parent = null)
        {
            ClarityStructure data = parent;
            //自身のNodesを読み込み
            if (parent == null)
            {
                string code = xml.Attribute("code").Value;
                data = new ClarityStructure(code, rid);
                rid += 1;
            }

            var nodes = xml.Elements(NodeIdentitiy);
            foreach (XElement ne in nodes)
            {
                ClarityStructure child = this.ReadNodes(ne, ref rid);
                data.AddChild(child);
            }


            return data;
        }
    }

    /// <summary>
    /// Element構造管理クラス 簡単にいっちゃうと、ファイルに沿って基本樹形構造を作成し、主用ノードを覚えておくクラス
    /// </summary>
    internal class EngineStructureManager
    {
        public EngineStructureManager()
        {
        }

        /// <summary>
        /// 主用ノードを記憶する [oid, structelement]
        /// </summary>
        protected Dictionary<long, ClarityStructure> NodeDic = new Dictionary<long, ClarityStructure>();


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 構造管理の初期化
        /// </summary>
        /// <param name="data">管理構造</param>
        /// <param name="f">自信を管理に含めるか true=含める</param>
        public void Init(ClarityStructure data, bool f = false)
        {
            this.NodeDic = new Dictionary<long, ClarityStructure>();

            this.AddNodeDicAll(data, f);

        }

        /// <summary>
        /// 対象を管理に追加する
        /// </summary>
        /// <param name="data"></param>
        public void AddManage(ClarityStructure data)
        {
            this.AddNodeDicAll(data, true);
        }


        /// <summary>
        /// 管理構造の取得
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public ClarityStructure GetNode(long oid)
        {
            return this.NodeDic[oid];
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// Nodeを全て辞書に登録する
        /// </summary>
        /// <param name="data">登録親</param>
        /// <param name="f">自身を管理に含めるか</param>
        private void AddNodeDicAll(ClarityStructure data, bool f = false)
        {
            if (f == true)
            {
                this.NodeDic.Add(data.ID, data);
            }

            //子供まで含めてすべてを追加する
            foreach (BaseElement elem in data.SystemLink.ChildList)
            {
                ClarityStructure cs = elem as ClarityStructure;
                if (cs == null)
                {
                    throw new Exception("ClarityStructure以外のデータがADDされています");
                }
                this.AddNodeDicAll(cs, true);
            }
        }


    }

    /// <summary>
    /// システム管理用構造
    /// </summary>
    internal class EngineSystemStructureManager : EngineStructureManager
    {
        public ClarityStructure GetNode(ESystemStructureID eid)
        {
            return this.GetNode((long)eid);
        }
    }
        
}
