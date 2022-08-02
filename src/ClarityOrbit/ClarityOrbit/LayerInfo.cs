using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
namespace ClarityOrbit
{
    /// <summary>
    /// レイヤープロジェクト情報
    /// </summary>
    internal class OrbitProjectBaseLayer : BaseOrbitProjectInfo
    {
        public OrbitProjectBaseLayer(OrbitProject op) : base(op)
        {

        }

        /// <summary>
        /// レイヤー一覧
        /// </summary>
        public List<LayerInfo> LayerList = new List<LayerInfo>();
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        public void AddNewLayer()
        {

        }

        public void ResizeLayer()
        {
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        

    }


    /// <summary>
    /// 一枚のレイヤー情報
    /// </summary>
    internal class LayerInfo
    {
        /// <summary>
        /// レイヤー番号
        /// </summary>
        public int LayerNo { get; set; } = -1;
        /// <summary>
        /// レイヤー名
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 表示可否
        /// </summary>
        public bool Visible { get; set; } = true;
        /// <summary>
        /// 編集可否
        /// </summary>
        public bool Enable { get; set; } = true;

        /// <summary>
        /// 作業場一式 [y][x]
        /// </summary>
        public List<List<TipInfo>> TipMap = new List<List<TipInfo>>();

        /// <summary>
        /// レイヤー構造描画上の親
        /// </summary>
        public LayerStructureElement StructureElement = null;


    }

    /// <summary>
    /// レイヤー構造定義element
    /// </summary>
    internal class LayerStructureElement : BaseElement
    {
        public LayerStructureElement()
        {
            this.RenderBehavior = null;
        }
    }
}
