using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ClarityOrbit
{
    /// <summary>
    /// Tile編集処理本体
    /// </summary>
    internal class TileEditCore
    {
        private TileEditCore()
        {
            //処理テーブル作成
            this.EditProcDic = this.CreateEditProcDic();
        }

        private static TileEditCore core = new TileEditCore();

        private delegate bool EditProcDelegate(Rectangle sel);

        /// <summary>
        /// 描画処理関数テーブル
        /// </summary>
        private Dictionary<EOrbitMouseControlMode, EditProcDelegate> EditProcDic;

        /// <summary>
        /// tile編集処理
        /// </summary>
        /// <param name="selarea">EditView選択エリア</param>
        /// <returns>true=処理した</returns>
        public static bool Edit(TileEditInfo tei)
        {
            if (OrbitGlobal.Project == null)
            {
                return false;
            }

            Rectangle selarea = tei.SelectedRect;
            EOrbitMouseControlMode mode = tei.Mode;

            bool kef = core.EditProcDic.ContainsKey(mode);
            if (kef == false)
            {
                return false;
            }
            //処理
            bool ret = core.EditProcDic[mode](selarea);            
            return ret;
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 処理関数作成
        /// </summary>
        private Dictionary<EOrbitMouseControlMode, EditProcDelegate> CreateEditProcDic()
        {
            Dictionary<EOrbitMouseControlMode, EditProcDelegate> ans = new Dictionary<EOrbitMouseControlMode, EditProcDelegate>();

            ans.Add(EOrbitMouseControlMode.Draw, this.EditDraw);
            ans.Add(EOrbitMouseControlMode.Eraser, this.EditEraser);
            ans.Add(EOrbitMouseControlMode.Fill, this.EditFill);

            return ans;
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="sel"></param>
        /// <returns></returns>
        private bool EditDraw(Rectangle sel)
        {

            //編集レイヤの取得
            var layer = OrbitGlobal.Project?.Layer.SelectedLayer;
            if (layer == null)
            {
                return false;
            }
            //元領域が選択されていない
            if (OrbitGlobal.ControlInfo.SrcSelectedInfo == null)
            {
                return false;
            }
            Size tcount = OrbitGlobal.Project.BaseInfo.TileCount;


            //レイヤー情報の設定
            for (int y = 0; y < sel.Height; y++)
            {
                for (int x = 0; x < sel.Width; x++)
                {
                    TileBitInfo binfo = new TileBitInfo();
                    {
                        //選択情報が設定できない
                        binfo.TipImageInfo = OrbitGlobal.ControlInfo.SrcSelectedInfo.SrcInfo;
                        if (binfo.TipImageInfo == null)
                        {
                            return false;
                        }
                        binfo.SrcPosIndex = new Point(OrbitGlobal.ControlInfo.SrcSelectedInfo.SelectedIndexRect.X + x, OrbitGlobal.ControlInfo.SrcSelectedInfo.SelectedIndexRect.Y + y);
                    }

                    int tx = x + sel.X;
                    int ty = y + sel.Y;
                    //中チェック
                    if (tx >= tcount.Width || ty >= tcount.Height) {
                        continue;
                    }
                    layer.TipMap[ty][tx].SrcInfo = binfo;
                }
            }
        

            return true;
        }

        /// <summary>
        /// 消しゴム処理
        /// </summary>
        /// <param name="sel"></param>
        /// <returns></returns>
        private bool EditEraser(Rectangle sel)
        {
            return true;
        }

        /// <summary>
        /// 塗りつぶし処理
        /// </summary>
        /// <param name="sel"></param>
        /// <returns></returns>
        private bool EditFill(Rectangle sel)
        {
            return true;
        }
    }
}
