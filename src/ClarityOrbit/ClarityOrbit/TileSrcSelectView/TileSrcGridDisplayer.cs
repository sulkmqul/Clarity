using Clarity.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityOrbit.TileSrcSelectView
{
    /// <summary>
    /// タイル元画像表示補正
    /// </summary>
    internal class TileSrcGridDisplayer : Clarity.GUI.BaseDisplayer
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="sinfo"></param>
        public TileSrcGridDisplayer(TileSrcImageInfo sinfo)
        {
            this.SInfo = sinfo;
            
        }

        /// <summary>
        /// 管理データ
        /// </summary>
        private TileSrcImageInfo SInfo { get; init; }


        /// <summary>
        /// マウスオーバーtileindex
        /// </summary>
        private Point MouseOverTileIndex { get ; set; }


        /// <summary>
        /// マウス情報を保存する
        /// </summary>
        private MouseInfo? MouseInfo { get; set; }
        

        /// <summary>
        /// マウスが押された時
        /// </summary>
        /// <param name="minfo"></param>
        public override void MouseDown(MouseInfo minfo)
        {
            this.MouseInfo = minfo;            
            if (minfo.DownButton == MouseButtons.Left)
            {
                this.UpdateSelectArea(minfo);
            }
        }

        /// <summary>
        /// マウスが動いたとき
        /// </summary>
        /// <param name="minfo"></param>
        public override void MouseMove(MouseInfo minfo)
        {
            this.MouseInfo = minfo;

            //マウスがかかっているタイル位置を取得
            var dpos = this.DispPointToSrcPoint(minfo.NowPos);
            this.MouseOverTileIndex = this.SInfo.CalcuImagePosIndex((int)dpos.X, (int)dpos.Y);

            //マウスが押されているなら更新
            if (minfo.DownButton == MouseButtons.Left)
            {
                this.UpdateSelectArea(minfo);
            }
            
        }

        /// <summary>
        /// マウスが離された時
        /// </summary>
        /// <param name="minfo"></param>
        public override void MouseUp(MouseInfo minfo)
        {
            this.MouseInfo = minfo;
            if (minfo.DownButton == MouseButtons.Left)
            {
                this.UpdateSelectArea(minfo);
            }
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="gra"></param>
        public override void Render(Graphics gra)
        {
            //グリッドの描画
            this.RenderGrid(gra);

            //仮選択の描画
            if (this.MouseInfo?.DownFlag == false)
            {
                this.RenderMouseOverPos(gra);
            }
            

            //選択の描画
            this.RenderSelectArea(gra);
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 表示位置からTileIndexを特定する
        /// </summary>
        /// <param name="dp">表示位置</param>
        /// <returns>tile index</returns>
        private Point DispPosToTileIndex(PointF dp)
        {
            //開始indexの特定
            var pf = this.DispPointToSrcPoint(dp);
            Point ans = this.SInfo.CalcuImagePosIndex(Convert.ToInt32(pf.X), Convert.ToInt32(pf.Y));
            return ans;
        }

        /// <summary>
        /// 選択情報の更新
        /// </summary>
        /// <param name="minfo">マウス情報</param>
        private void UpdateSelectArea(MouseInfo minfo)
        {
            //選択ID設定
            OrbitGlobal.UserTemp.TileSrcSelectInfo.TileSrcImageID = this.SInfo.TileSrcImageID;

            //indexの特定
            Point dpos = this.DispPosToTileIndex(minfo.DownPos);
            Point tpos = this.DispPosToTileIndex(minfo.NowPos);


            //エリアindexの計算
            int left = Math.Min(dpos.X, tpos.X);
            int top = Math.Min(dpos.Y, tpos.Y);

            int right = Math.Max(dpos.X, tpos.X);
            int bottom = Math.Max(dpos.Y, tpos.Y);

            int w = right - left + 1;
            int h = bottom - top + 1;


            OrbitGlobal.UserTemp.TileSrcSelectInfo.Index = new Rectangle(left, top, w, h);

            //選択エリア情報変更
            OrbitGlobal.SendEvent(EOrbitEventID.TileSrcImageSelectAreaChanged);
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// グリッドの描画
        /// </summary>
        /// <param name="gra"></param>
        private void RenderGrid(Graphics gra)
        {
            //Gridペンの作成
            using (Pen pe = new Pen(OrbitGlobal.Settings.DisplaySetting.TileSrcImageGridColor, 1))
            {
                //スタイル設定
                pe.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;


                var tc = this.SInfo.TileCount;

                //縦
                for (int i = 0; i < tc.Width; i++)
                {
                    //x位置とy軸の特定
                    int x = this.SInfo.CalcuIndexToImageLT(i, 0).X;
                    int y = 0;
                    float h = this.SrcSize.Height;
                    
                    float px = this.SrcXToDispX(x);

                    float sty = this.SrcYToDispY(y);
                    float edy = this.SrcYToDispY(h);


                    gra.DrawLine(pe, px, sty, px, edy);
                }

                //横
                for (int i = 0; i < tc.Height; i++)
                {
                    //x位置とy軸の特定
                    int y = this.SInfo.CalcuIndexToImageLT(0, i).Y;
                    int x = 0;
                    float w = this.SrcSize.Width;


                    float stx = this.SrcXToDispX(x);
                    float edx = this.SrcXToDispX(w);

                    float py = this.SrcYToDispY(y);


                    gra.DrawLine(pe, stx, py, edx, py);
                }
            }
        }


        /// <summary>
        /// マウス位置の仮描画
        /// </summary>
        /// <param name="gra">描画場所</param>
        private void RenderMouseOverPos(Graphics gra)
        {
            //元画像のエリアを特定
            Rectangle srcarea = this.SInfo.CalcuIndexToImageArea(this.MouseOverTileIndex.X, this.MouseOverTileIndex.Y);

            
            using (Pen pe = new Pen(OrbitGlobal.Settings.DisplaySetting.TileSrcImageMouseOverColor, 2))
            {
                var st = this.SrcPointToDispPoint(new PointF(srcarea.Left, srcarea.Top));
                var ed = this.SrcPointToDispPoint(new PointF(srcarea.Right, srcarea.Bottom));


                gra.DrawRectangle(pe, st.X, st.Y, ed.X - st.X, ed.Y - st.Y);
            }
        }

        /// <summary>
        /// 選択エリアの描画
        /// </summary>
        /// <param name="gra"></param>
        private void RenderSelectArea(Graphics gra)
        {
            //自身の画像が選択されていない場合は描画しない
            if( this.SInfo.TileSrcImageID != OrbitGlobal.UserTemp.TileSrcSelectInfo.TileSrcImageID)
            {
                return;
            }

            Rectangle sarea = OrbitGlobal.UserTemp.TileSrcSelectInfo.Index;

            //元画像のエリアを特定
            Rectangle starea = this.SInfo.CalcuIndexToImageArea(sarea.Left, sarea.Top);
            Rectangle edarea = this.SInfo.CalcuIndexToImageArea(sarea.Right, sarea.Bottom);


            using (Pen pe = new Pen(OrbitGlobal.Settings.DisplaySetting.TileSrcImageSelectAreaColor, 4))
            {
                var st = this.SrcPointToDispPoint(new PointF(starea.Left, starea.Top));
                var ed = this.SrcPointToDispPoint(new PointF(edarea.Left, edarea.Top));


                gra.DrawRectangle(pe, st.X, st.Y, ed.X - st.X, ed.Y - st.Y);
            }
        }

    }
}
