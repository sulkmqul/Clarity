using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Clarity;
using Clarity.Engine;

namespace ClarityOrbit
{
    /// <summary>
    /// 一枚のレイヤー情報
    /// </summary>
    internal class LayerInfo
    {
        public LayerInfo(int layno)
        {
            this.LayerNo = layno;
        }

        /// <summary>
        /// レイヤー番号(表示順に等しい、IDではない)
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
        /// このレイヤーの表示固有色
        /// </summary>
        public Color LayerDisplayColor { get; set; } = Color.Yellow;

        /// <summary>
        /// 作業場一式 [y][x]
        /// </summary>
        public List<List<TipInfo>> TipMap = new List<List<TipInfo>>();

        /// <summary>
        /// このレイヤーのZ位置
        /// </summary>
        public float OffsetZ { get; set; } = 0.0f;


        /// <summary>
        /// 初期タイルマップの作成
        /// </summary>
        /// <param name="tcount">タイル数</param>
        /// <param name="tsize">タイルサイズ</param>        
        public void CraeteInitialTipMap(Size tcount, Size tsize)
        {
            List<List<TipInfo>> anslist = new List<List<TipInfo>>(tcount.Height);

            //タイル数分のobjectを作成
            for (int y = 0; y < tcount.Height; y++)
            {
                List<TipInfo> xlist = new List<TipInfo>(tcount.Width);
                for (int x = 0; x < tcount.Width; x++)
                {
                    TipInfo tinfo = new TipInfo(this, new System.Drawing.Point(x, y), tsize);
                    xlist.Add(tinfo);                    
                }
                anslist.Add(xlist);
            }

            this.TipMap = anslist;            
        }

        /// <summary>
        /// レイヤー情報の削除
        /// </summary>
        public void RemoveLayer()
        {
            foreach (var dlist in this.TipMap)
            {
                foreach (var data in dlist)
                {
                    ClarityEngine.RemoveManage(data);
                }
            }
        }
    }

    




    /// <summary>
    /// レイヤープロジェクト情報
    /// </summary>
    internal class OrbitProjectLayer : BaseOrbitProjectInfo
    {
        public OrbitProjectLayer(OrbitProject op) : base(op)
        {

        }

        /// <summary>
        /// レイヤー一覧
        /// </summary>
        public List<LayerInfo> LayerList { get; set; } = new List<LayerInfo>();

        /// <summary>
        /// 選択LayerIndex
        /// </summary>
        public int SelectedLayerIndex { get; set; } = -1;

        /// <summary>
        /// 選択レイヤー情報
        /// </summary>
        public LayerInfo? SelectedLayer
        {
            get
            {
                if (this.SelectedLayerIndex < 0)
                {
                    return null;
                }

                return this.LayerList[this.SelectedLayerIndex];
            }
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// Layerの追加
        /// </summary>
        /// <returns>追加したレイヤー情報</returns>
        public LayerInfo AddNewLayer()
        {
            LayerInfo info = new LayerInfo(this.LayerList.Count);

            //チップを作成
            info.CraeteInitialTipMap(this.Project.BaseInfo.TileCount, this.Project.BaseInfo.TileSize);
            this.LayerList.Add(info);

            return info;
        }

        /// <summary>
        /// 管理layerのリサイズ
        /// </summary>
        public void ResizeLayer()
        {
        }

        /// <summary>
        /// Layerの削除
        /// </summary>
        /// <param name="lindex">削除layerIndex</param>
        public void RemoveLayer(int lindex)
        {
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        


    }


    
}
