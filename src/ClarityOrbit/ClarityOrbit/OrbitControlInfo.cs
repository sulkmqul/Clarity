using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ClarityOrbit
{
    public enum EOrbitMouseControlMode
    {
        /// <summary>
        /// 移動モード 注視点とカメラを同期して動かす
        /// </summary>
        CameraMove,

        /// <summary>
        /// 注視点を変えずにカメラを動かす
        /// </summary>
        CameraMove3D,

        /// <summary>
        /// 通常描画
        /// </summary>
        Draw,

        /// <summary>
        /// 消しゴム
        /// </summary>
        Eraser,

        /// <summary>
        /// 塗りつぶし
        /// </summary>
        Fill,

    }



    /// <summary>
    /// 保存しないが全体で使用する情報
    /// 主にユーザー操作の予定
    /// </summary>
    internal class OrbitControlInfo
    {
        /// <summary>
        /// 編集処理モード
        /// </summary>
        public EOrbitMouseControlMode ControlMode { get; set; } = EOrbitMouseControlMode.Draw;

        /// <summary>
        /// 元画像情報選択可否
        /// </summary>
        public SrcImageSelectedInfo? SrcSelectedInfo = null;

    }

    /// <summary>
    /// 元画像情報選択
    /// </summary>
    internal class SrcImageSelectedInfo
    {
        /// <summary>
        /// 選択タイル元画像情報
        /// </summary>
        public TileImageSrcInfo? SrcInfo = null;

        /// <summary>
        /// 選択矩形(TileIndex)
        /// </summary>
        public Rectangle SelectedIndexRect;

        /// <summary>
        /// 範囲選択を行っているか否か
        /// </summary>
        public bool AreaSelect
        {
            get
            {
                if (this.SrcInfo == null)
                {
                    return false;
                }

                if (this.SelectedIndexRect.Width == 1 && this.SelectedIndexRect.Height == 1)
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 選択情報の作成
        /// </summary>
        /// <param name="si">選択開始点index情報</param>
        /// <param name="ei">選択終了点index情報</param>
        /// <returns></returns>
        public static SrcImageSelectedInfo? CreateSrcSelectedInfo(TileImageSrcInfo srcinfo, Point si, Point ei)
        {            
            Point st = new Point();
            Point ed = new Point();

            //index位置を正しくする
            st.X = Math.Min(si.X, ei.X);
            st.Y = Math.Min(si.Y, ei.Y);
            ed.X = Math.Max(si.X, ei.X);
            ed.Y = Math.Max(si.Y, ei.Y);

            //範囲外なら選択解除・・・するべきか？
            

            //矩形幅を出す
            int width = ed.X - st.X + 1;
            int height = ed.Y - st.Y + 1;

            SrcImageSelectedInfo ans = new SrcImageSelectedInfo();
            ans.SrcInfo = srcinfo;
            ans.SelectedIndexRect = new Rectangle(st, new Size(width, height));

            return ans;

        }

    }

    
}
