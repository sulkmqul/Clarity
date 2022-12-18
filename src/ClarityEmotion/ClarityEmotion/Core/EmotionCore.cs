using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Clarity.GUI;

namespace ClarityEmotion.Core
{
    

    /// <summary>
    /// コア処理
    /// </summary>
    internal class EmotionCore
    {
        public EmotionCore()
        {
        }

        internal class CoreData
        {
            public Rectangle DisplayRect;
            public AnimeElement SrcData;
        }

        internal List<CoreData> CoreList { get; private set; } = new List<CoreData>();

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="srclist">元アニメーション</param>
        public void Init(List<AnimeElement> srclist)
        {
            this.CoreList = new List<CoreData>();
            srclist.ForEach(x =>
            {
                CoreData data = new CoreData(){ DisplayRect = new Rectangle(), SrcData = x };
                this.CoreList.Add(data);

            });
        }


        /// <summary>
        /// データ描画
        /// </summary>
        /// <param name="frame">対象フレーム</param>
        /// <param name="gra">描画場所</param>
        /// <param name="ivt">変換情報</param>
        /// <param name="editflag">編集状態か？</param>
        public void GenerateEmotion(int frame, Graphics gra, ImageViewerTranslator ivt, bool editflag = false, int selectlayno = -1)
        {
            this.CoreList.ForEach(x =>
            {
                this.RenderLayerImage(x, frame, gra, ivt);
                if (editflag == true)
                {
                    //編集状態なら必要な情報の表示
                    this.RenderRect(x, gra, Color.Blue);

                    //マウスオーバー
                    if (x.SrcData.TempData.MouseOverFlag == true)
                    {
                        this.RenderRect(x, gra, Color.Blue, 2.0f);
                    }
                    //選択
                    if (x.SrcData.LayerNo == selectlayno)
                    {
                        this.RenderRect(x, gra, Color.Yellow, 2.0f);
                    }



                }
            });
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 対象の描画
        /// </summary>
        /// <param name="data"></param>
        /// <param name="frame"></param>
        /// <param name="gra"></param>
        /// <param name="ivt"></param>        
        private void RenderLayerImage(CoreData data, int frame, Graphics gra, ImageViewerTranslator ivt)
        {
            //フレーム情報取得
            Bitmap? img = data.SrcData.GetFrameImage(frame);
            if (img == null)
            {
                return;
            }
            var edata = data.SrcData.EaData;
            img = this.FlipBitmap(edata.FlipType, img);

            PointF pos = ivt.SrcPointToDispPoint(edata.Pos2D);
            PointF size = ivt.SrcPointToViewPoint(new Point(edata.DispSize.Width, edata.DispSize.Height));
            data.DisplayRect = new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y);

            //透明設定
            ColorMatrix cmat = new ColorMatrix();

            cmat.Matrix00 = 1.0f;
            cmat.Matrix11 = 1.0f;
            cmat.Matrix22 = 1.0f;
            cmat.Matrix33 = edata.Alpha;
            cmat.Matrix44 = 1.0f;

            ImageAttributes iat = new ImageAttributes();
            iat.SetColorMatrix(cmat);

            //描画
            gra.DrawImage(img, data.DisplayRect,
                0, 0, img.Width, img.Height,
                GraphicsUnit.Pixel, iat);
        }

        /// <summary>
        /// 画像のFLip処理
        /// </summary>
        /// <param name="ftype"></param>
        /// <param name="bit"></param>
        /// <returns></returns>
        private Bitmap FlipBitmap(EFlipType ftype, Bitmap bit)
        {
            if ((ftype & EFlipType.XFlip) == EFlipType.XFlip)
            {
                bit.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            if ((ftype & EFlipType.YFlip) == EFlipType.YFlip)
            {
                bit.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
            return bit;
        }



        /// <summary>
        /// エリアの描画
        /// </summary>
        /// <param name="cdata"></param>
        /// <param name="gra"></param>
        /// <param name="col"></param>
        private void RenderRect(CoreData cdata, Graphics gra, Color col, float penwidth = 1.0f)
        {
            var rect = cdata.DisplayRect;

            using (Pen pe = new Pen(col, penwidth))
            {
                //矩形の描画
                gra.DrawRectangle(pe, rect);

                //中心線の描画
                int hw = rect.X + (rect.Width / 2);
                int hh = rect.Y + (rect.Height / 2);

                gra.DrawLine(pe, new Point(hw, rect.Top), new Point(hw, rect.Bottom));
                gra.DrawLine(pe, new Point(rect.Left, hh), new Point(rect.Right, hh));
            }
        }


    }
}
