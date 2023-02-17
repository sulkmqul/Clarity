using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Disposables;
using System.Drawing;
using Clarity;
using System.IO.Pipes;

namespace ClarityMovement.FrameEdit
{
    /// <summary>
    /// エディタのパラメータ
    /// </summary>
    public class FrameEditorParam
    {
        /// <summary>
        /// 1フレームの横サイズ
        /// </summary>
        public int FrameSize { get; set; } = 10;

        /// <summary>
        /// 計測目盛り表示領域(Col Header)
        /// </summary>
        public int ColHeaderSize { get; set; } = 20;

        /// <summary>
        /// Rowのヘッダーサイズ
        /// </summary>
        public int RowHeaderSize { get; set; } = 25;

        /// <summary>
        /// フレーム画像表示エリア縦サイズ
        /// </summary>
        public int ImageSize { get; set; } = 50;

        /// <summary>
        /// タグの高さ
        /// </summary>
        public int TagSize { get; set; } = 25;

        /// <summary>
        /// 最大タグ数 
        /// </summary>
        public int MaxTagCount { get; } = 10;


        /// <summary>
        /// 線の色
        /// </summary>
        public Color BaseLineColor { get; set; } = Color.Gray;

        /// <summary>
        /// ヘッダー色
        /// </summary>
        public Color HeaderColor { get; set; } = Color.FromArgb(220, 220, 220);
    }




    


    /// <summary>
    /// エディターの描画
    /// </summary>
    internal class FrameEditorPainter
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="c">管理画面</param>
        public FrameEditorPainter(FrameEditControl c)
        {
            this.Parent= c;
        }


        /// <summary>
        /// 親画面
        /// </summary>
        private FrameEditControl Parent;


        /// <summary>
        /// フレームの描画(colヘッダー領域)
        /// </summary>
        public Rectangle ColHeaderArea { get; private set; } = new Rectangle();

        /// <summary>
        /// タグ番号などの説明(rowヘッダー領域)
        /// </summary>
        public Rectangle RowHeaderArea { get; private set; } = new Rectangle();
        /// <summary>
        /// 設定映像の描画範囲
        /// </summary>
        public Rectangle ImageArea { get; private set; } = new Rectangle();
        /// <summary>
        /// タグの描画範囲
        /// </summary>
        public Rectangle TagArea { get; private set; } = new Rectangle();


        /// <summary>
        /// Rx管理
        /// </summary>
        private CompositeDisposable dispComp = new CompositeDisposable();

        

        /// <summary>
        /// 初期化されるとき
        /// </summary>        
        public void Init()
        {
            this.dispComp.Dispose();
            this.dispComp = new CompositeDisposable();

            
        }



        /// <summary>
        /// エリア領域の再計算
        /// </summary>
        /// <param name="maxframe">最大フレーム数</param>
        /// <param name="fe">各種サイズパラメータ</param>
        /// <returns>これを描画するための必要コントロールサイズ</returns>
        public Size ResizeArea(int maxframe, FrameEditorParam fe)
        {
            //全体横幅の計算
            int width = maxframe * fe.FrameSize;

            int right = fe.RowHeaderSize;
            int bottom = 0;
            //フレーム領域の計算
            {
                this.ColHeaderArea = new Rectangle(right, 0, width, fe.ColHeaderSize);
                bottom = this.ColHeaderArea.Bottom;
            }
            //フレーム画像領域の計算
            {
                this.ImageArea = new Rectangle(right, bottom, width, fe.ImageSize);
                bottom = this.ImageArea.Bottom;
            }
            //タグサイズ
            {
                //タグ領域の高さ計算
                int th = fe.TagSize * fe.MaxTagCount;
                this.TagArea = new Rectangle(right, bottom, width, th);
            }



            //Rowのヘッダーの割り出し
            this.RowHeaderArea = new Rectangle(0, this.ImageArea.Top, fe.RowHeaderSize, this.ImageArea.Height + this.TagArea.Height);



            //一番下の右下がサイズ
            Size ans = new Size(this.TagArea.Right, this.TagArea.Bottom);

            
            return ans;
        }


        /// <summary>
        /// ピクセル位置から選択情報を割り出す
        /// </summary>
        /// <param name="pos">ピクセル位置</param>
        /// <returns></returns>
        public FrameEditorSelection? GetSelect(Point pos)
        {
            FrameEditorParam para = this.Parent.SizeParam;

            //画像選択領域に入っているか？
            bool icf = this.ImageArea.Contains(pos);
            if (icf == true)
            {
                //位置の割り出し
                FrameEditorSelection ans = new FrameEditorSelection();
                ans.Area = ETagType.Image;
                ans.FrameNo = this.PixelXToFrame(pos.X);

                int l = ans.FrameNo * para.FrameSize + para.RowHeaderSize;
                int t = this.ImageArea.Top;
                ans.SelectionFrameRect = new Rectangle(l, t, para.FrameSize, this.ImageArea.Height);
                return ans;
            }

            //tag領域に入っているか？
            bool tcf = this.TagArea.Contains(pos);
            if (tcf == true)
            {
                //割り出し
                FrameEditorSelection ans = new FrameEditorSelection();
                ans.Area = ETagType.Tag;
                ans.FrameNo = this.PixelXToFrame(pos.X);

                //タグ位置を出す
                int ti = this.PixelYToTagIndex(pos.Y);

                int l = ans.FrameNo * para.FrameSize + para.RowHeaderSize;
                int t = this.TagArea.Top + ti * para.TagSize;
                ans.SelectionFrameRect = new Rectangle(l, t, para.FrameSize, para.TagSize);
                return ans;
            }

            //ここまで来たら選択なし
            return null;
        }

        /// <summary>
        /// ピクセルXからフレーム位置を算出する
        /// </summary>
        /// <param name="px"></param>
        /// <returns></returns>
        public int PixelXToFrame(int px)
        {
            //フレーム位置はどれも同じなので代表としてimageareaを使用する
            int left = this.ImageArea.Left;
            int width = this.ImageArea.Width;

            //移動
            int p = px - left;
            int x = p / this.Parent.SizeParam.FrameSize;

            return x;
        }


        /// <summary>
        /// PixelYからTagindexを割り出す
        /// </summary>
        /// <param name="py">pixel y</param>
        /// <returns>tagindexを割り出す マイナス値=範囲外</returns>
        public int PixelYToTagIndex(int py)
        {
            //タグエリアに入っているか？
            if (this.TagArea.Top > py && py >= this.TagArea.Bottom)
            {
                return -1;
            }

            int moast = py - this.TagArea.Top;
            int ans = moast / this.Parent.SizeParam.TagSize;
            return ans;
        }


        /// <summary>
        /// 対象フレームの横位置を算出する
        /// </summary>
        /// <param name="frame">フレームindex</param>
        /// <returns>計算範囲</returns>
        public HorizontalSize FrameToPosX(int frame)
        {
            var para = this.Parent.SizeParam;

            int x = this.ColHeaderArea.Left + frame * para.FrameSize;

            HorizontalSize ans = new HorizontalSize(x, x + para.FrameSize);
            return ans;
        }

        /// <summary>
        /// Tagindexから縦の範囲を割り出す
        /// </summary>
        /// <param name="tag">tag index</param>
        /// <returns></returns>
        public VerticalSize TagIndexToPosY(int tag)
        {
            var para = this.Parent.SizeParam;
            int y = this.TagArea.Top + (para.TagSize * tag);

            VerticalSize ans = new VerticalSize(y, y + para.TagSize);
            return ans;
        }

        /// <summary>
        /// 対象フレームのImageAreaの反映を作成する
        /// </summary>
        /// <param name="frame">フレーム位置</param>
        /// <param name="span">フレーム幅</param>
        /// <returns></returns>
        public Rectangle FrameToImageArea(int frame, int span = 1)
        {
            var st = this.FrameToPosX(frame);
            var ed = this.FrameToPosX(frame + span - 1);

            int w = ed.Right - st.Left;

            Rectangle ans = new Rectangle(st.Left, this.ImageArea.Top, w, this.ImageArea.Height);
            return ans;
        }
        /// <summary>
        /// 対象フレームのタグ番号から範囲を作成する
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Rectangle FrameToTagArea(int frame, int tag)
        {
            //フレーム位置
            var fsize = this.FrameToPosX(frame);
            //タグ位置
            var tagh = this.TagIndexToPosY(tag);

            Rectangle ans = new Rectangle(fsize.Left, tagh.Top, fsize.Width, tagh.Height);
            return ans;
        }

        /// <summary>
        /// データの表示エリアの再計算
        /// </summary>
        public void RecalcuTagDataArea()
        {
            List<FrameModifierPaintData> datalist = this.Parent.EData.PaintDataList;
            datalist.ForEach(x => x.CalcuArea(this));
        }

        /// <summary>
        /// 描画処理本体
        /// </summary>
        /// <param name="gra">描画物</param>        
        /// <param name="maxframe">最大フレーム数</param>
        public void Paint(Graphics gra, int maxframe, EditorData edata)
        {
            var param = this.Parent.SizeParam;
            gra.Clear(Color.White);

            #region 各領域の下塗り
            {

                //ヘッダーの描画
                using (SolidBrush bru = new SolidBrush(param.HeaderColor))
                {
                    gra.FillRectangle(bru, this.ColHeaderArea);
                    gra.FillRectangle(bru, this.RowHeaderArea);
                }

                using (SolidBrush bru = new SolidBrush(Color.FromArgb(210, 230, 255)))
                {
                    gra.FillRectangle(bru, this.ImageArea);
                }

                using (SolidBrush bru = new SolidBrush(Color.AntiqueWhite))
                {
                    gra.FillRectangle(bru, this.TagArea);
                }
            }
            #endregion

            //目盛りの描画
            this.PaintMeasureArea(gra, maxframe);

            //マスウの選択位置の描画
            if (edata.MouseCursor != null)
            {
                using (Pen pe = new Pen(Color.Black, 1.0f))
                {
                    gra.DrawRectangle(pe, edata.MouseCursor.SelectionFrameRect);
                }
            }

            //データの領域再計算
            this.RecalcuTagDataArea();
            //描画
            edata.PaintDataList.ForEach(x => x.Paint(gra));
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 目盛りの描画
        /// </summary>
        /// <param name="gra"></param>
        /// <param name="maxframe"></param>
        private void PaintMeasureArea(Graphics gra, int maxframe)
        {
            Rectangle area = this.ColHeaderArea;

            //線の描画
            var param = this.Parent.SizeParam;
            using (Pen pe = new Pen(param.BaseLineColor, 1))
            {
                using (SolidBrush bru = new SolidBrush(Color.Black))
                {
                    Font font = new Font("Tohoma", 10.0f);
                    Rectangle cell = new Rectangle();


                    #region RowHeaderの描画
                    {
                        //全体を囲む
                        gra.DrawRectangle(pe, this.RowHeaderArea);

                        //タグのヘッダー部
                        for (int tc = 0; tc < param.MaxTagCount; tc++)
                        {
                            //一つをセル矩形
                            int top = this.TagArea.Top + (param.TagSize * tc);
                            cell = new Rectangle(0, top, param.RowHeaderSize, param.TagSize);
                            gra.DrawRectangle(pe, cell);


                            //tag番号
                            string text = (tc + 1).ToString();
                            SizeF fs = gra.MeasureString(text, font);
                            gra.DrawString(text, font, bru, cell.Right - fs.Width, cell.Bottom - fs.Height);
                        }
                    }
                    #endregion


                    //各フレーム枠の表示
                    for (int i = 0; i < maxframe; i++)
                    {
                        int left = i * param.FrameSize + area.X;


                        #region フレーム目盛り ColumnsHeader
                        cell = new Rectangle(left, area.Top, param.FrameSize, param.ColHeaderSize);
                        gra.DrawRectangle(pe, cell);


                        //フレーム番号の表示
                        string text = (i + 1).ToString();
                        SizeF fs = gra.MeasureString(text, font);
                        Rectangle iarea = this.ImageArea;
                        if (fs.Width > param.FrameSize)
                        {
                            text = "..";
                        }
                        gra.DrawString(text, font, bru, cell.Left, cell.Bottom - fs.Height);
                        #endregion


                        #region 画像
                        {
                            cell = this.FrameToImageArea(i);
                            gra.DrawRectangle(pe, cell);
                        }
                        #endregion


                        #region tag
                        {
                            Rectangle tarea = this.TagArea;

                            for (int tc = 0; tc < param.MaxTagCount; tc++)
                            {
                                cell = this.FrameToTagArea(i, tc);
                                gra.DrawRectangle(pe, cell);

                            }
                        }
                        #endregion
                    }
                }
            }

        }
    }
}
