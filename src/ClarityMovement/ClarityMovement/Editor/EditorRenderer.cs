using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClarityMovement.Editor
{
    

    /// <summary>
    /// 描画管理
    /// </summary>
    internal class EditorRenderer
    {
        public EditorRenderer()
        {
            //基準テーブルの作成
            this.BaseFrameAreaWidthTable = this.CreateBaseWidthTable();
            this.BaseFrameAreaWidthTableIndex = this.BaseFrameAreaWidthTable.IndexOf(BaseAreaWidth);
        }
        

        /// <summary>
        /// 基準幅
        /// </summary>
        private const float BaseAreaWidth = 100.0f;
               

        /// <summary>
        /// 現在の基準幅取得
        /// </summary>
        public float CurrentBaseAreaWidth
        {
            get
            {
                return this.BaseFrameAreaWidthTable[this.BaseFrameAreaWidthTableIndex];
            }
        }

        /// <summary>
        /// フレーム描画の基礎データ
        /// </summary>
        public List<FrameRenderingInfo> FrameAreaInfoList { get; init; } = new List<FrameRenderingInfo>();

        /// <summary>
        /// 全体描画サイズ
        /// </summary>
        public SizeF FrameRenderSize
        {
            get
            {
                //横はフレーム
                int count = this.FrameAreaInfoList.Count;
                float w = this.CurrentBaseAreaWidth * (count + 1);

                float h = this.FrameAreaInfoList.First().Area.Bottom;

                return new SizeF(w, h);

            }
        }


        /// <summary>
        /// 現在の選択エリア
        /// </summary>
        public RectangleF? SelectArea { get; set; } = null;

        #region private        
        /// <summary>
        /// 基準幅index
        /// </summary>
        private int BaseFrameAreaWidthTableIndex = 0;

        /// <summary>
        /// 基準幅の設定
        /// </summary>
        private List<float> BaseFrameAreaWidthTable;

        #endregion
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//        
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init(MvProject proj)
        {
            this.FrameAreaInfoList.Clear();

            //フレーム画像分のデータを作成する
            int findex = 0;
            proj.BaseImageList.ForEach(x =>
            {
                FrameRenderingInfo info = new FrameRenderingInfo(findex);
                this.FrameAreaInfoList.Add(info);
                findex += 1;
            });
        }

        /// <summary>
        /// 拡大率の変更
        /// </summary>
        /// <param name="f">true=拡大 false=縮小</param>
        public void ChangeZoom(bool f)
        {
            if (f == true)
            {
                this.BaseFrameAreaWidthTableIndex += 1;
            }
            else
            {
                this.BaseFrameAreaWidthTableIndex -= 1;
            }

            //範囲内に
            this.BaseFrameAreaWidthTableIndex = Math.Min(this.BaseFrameAreaWidthTable.Count - 1, this.BaseFrameAreaWidthTableIndex);
            this.BaseFrameAreaWidthTableIndex = Math.Max(0, this.BaseFrameAreaWidthTableIndex);

            //値の設定
            this.FrameAreaInfoList.ForEach(x => x.BaseWidth = this.CurrentBaseAreaWidth);

        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="gra"></param>
        public void Render(Control con, PointF offset, Graphics gra, MvProject proj)
        {
            gra.Clear(MvGlobal.Setting.Editor.ClearColor);
            

            //フレーム情報の描画
            RectangleF framearearect = new RectangleF(0, 0, con.Width, con.Height);
            this.FrameAreaInfoList.ForEach(x =>
            {
                x.CalclateArea(con, offset, proj);
                x.Render(framearearect, gra, con.Font);
            });

            //画像の描画
            this.RenderBaseImage(proj, gra);

            //タグの描画


            //選択情報などの描画
            this.RenderSelectArea(gra);
        }


        /// <summary>
        /// 座標がどのフレーム領域上にあるかを調べる
        /// </summary>
        /// <param name="cpos">control座標位置</param>
        /// <returns></returns>
        public FrameRenderingInfo? ColPointFrame(PointF cpos)
        {
            FrameRenderingInfo? ans = null;
            ans = this.FrameAreaInfoList.Where(x => x.Area.Contains(cpos)).FirstOrDefault();

            return ans;
        }

        /// <summary>
        /// 選択エリアの取得
        /// </summary>
        /// <param name="cpos"></param>
        public void ColSelectArea(PointF cpos)
        {
            this.SelectArea = null;
            var data = this.ColPointFrame(cpos);
            if(data == null)
            {
                return;
            }

            this.SelectArea = data.ColPointAreaRect(cpos);
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 拡縮テーブルの作成
        /// </summary>
        /// <returns></returns>
        private List<float> CreateBaseWidthTable()
        {
            List<float> anslist = new List<float>();

            //基準
            anslist.Add(BaseAreaWidth);

            //小さいほう
            float rate = BaseAreaWidth;
            for (int i = 0; i < 4; i++)
            {
                rate *= 0.9f;
                anslist.Add(rate);
            }

            //大きい方
            rate = BaseAreaWidth;
            for (int i = 0; i < 10; i++)
            {
                rate *= 1.1f;
                anslist.Add(rate);
            }

            //小さい順に
            var avec = anslist.OrderBy(x => x).ToList();
            return avec;
        }

        /// <summary>
        /// 基準画像の描画
        /// </summary>
        /// <param name="proj"></param>
        /// <param name="gra"></param>
        private void RenderBaseImage(MvProject proj, Graphics gra)
        {
            int index = 0;
            proj.BaseImageList.ForEach(bit =>
            {
                var finfo = this.FrameAreaInfoList[index];
                if (finfo.RenderEnabled == true)
                {                    
                    gra.DrawImage(bit, finfo.BaseImageArea);
                }

                index++;
            });

        }

        /// <summary>
        /// 選択エリアの描画
        /// </summary>
        /// <param name="gra"></param>
        private void RenderSelectArea(Graphics gra)
        {            
            if(this.SelectArea == null)
            {
                return;
            }
            RectangleF rc = this.SelectArea.Value;

            using (SolidBrush bru = new SolidBrush(Color.FromArgb(80, Color.White)))
            {
                gra.FillRectangle(bru, rc);
            }
        }
    }
}
