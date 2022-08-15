using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity.Util;
using Clarity.GUI;
namespace ClarityOrbit.EditView
{
    /// <summary>
    /// Minimapの処理者
    /// </summary>
    internal class MinimapDisplayer : BaseDisplayer
    {
        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="gra"></param>
        public override void Render(Graphics gra)
        {
            if (OrbitGlobal.Project == null)
            {
                return;
            }

            //サイズの取得
            OrbitProject pro = OrbitGlobal.Project;
            

            //データクリア
            gra.Clear(Color.Black);

            //描画範囲の表示
            this.RenderViewArea(gra);

            //レイヤー情報の描画
            this.RenderLayerInfo(gra, pro);
            
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// レイヤー情報の描画
        /// </summary>
        /// <param name="gra"></param>
        private void RenderLayerInfo(Graphics gra, OrbitProject pro)
        {
            //サイズの取得
            Size tcount = pro.BaseInfo.TileCount;

            //全レイヤーの描画
            pro.Layer.LayerList.ForEach((layer) =>
            {
                //非表示は描画しない
                if (layer.Visible == false)
                {
                    return;
                }

                //レイヤーごと、あるいは非表示に対応して色を変更するかもしれない
                using (SolidBrush bru = new SolidBrush(layer.LayerDisplayColor))
                {
                    for (int y = 0; y < tcount.Height; y++)
                    {
                        for (int x = 0; x < tcount.Width; x++)
                        {
                            //データ設定されている？
                            if (layer.TipMap[y][x].DataExists == false)
                            {
                                continue;
                            }


                            float l = this.SrcXToDispX(x - 1);
                            float t = this.SrcYToDispY(y - 1);
                            float r = this.SrcXToDispX(x);
                            float b = this.SrcYToDispY(y);

                            //位置に矩形を描画
                            gra.FillRectangle(bru, l, t, r - l, b - t);
                        }
                    }
                }

            });
        }


        /// <summary>
        /// 暫定描画範囲を表示
        /// </summary>
        /// <param name="gra"></param>
        private void RenderViewArea(Graphics gra)
        {
            //表示範囲を取得
            Rectangle rc = OrbitEditViewControl.TempInfo.ViewAreaIndexRect;

            float l = this.SrcXToDispX(rc.Left);
            float t = this.SrcYToDispY(rc.Top);
            float r = this.SrcXToDispX(rc.Right);
            float b = this.SrcYToDispY(rc.Bottom);

            //範囲描画
            using (Pen pe = new Pen(Color.Red, 2.0f))
            {
                gra.DrawRectangle(pe, l, t, r - l, b - t);
            }
        }
    }
}
