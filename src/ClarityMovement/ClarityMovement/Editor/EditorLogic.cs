using Clarity.GUI;
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
    internal class EditorLogic
    {
        public EditorLogic()
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
        /// 描画要素情報
        /// </summary>
        public List<BaseUiElement> ElementList { get; init; } = new List<BaseUiElement>();

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
        /// 現在の選択データ
        /// </summary>
        public BaseUiElement? SelectedElement { get; set; } = null;
        /// <summary>
        /// 現在のマウス被りデータ
        /// </summary>
        public BaseUiElement? MouseOverElement { get; set; } = null;

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
            this.ElementList.Clear();

            //フレーム分のデータを作成する
            int findex = 0;
            proj.BaseImageList.ForEach(x =>
            {
                FrameRenderingInfo info = new FrameRenderingInfo(findex);
                this.FrameAreaInfoList.Add(info);
                findex += 1;
            });

            //フレーム画像の作成
            this.CreateFrameImageElement();            
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
            
            ///描画情報の計算
            this.CalculateRendering(con, offset, proj);

            //フレーム情報の描画
            RectangleF framearearect = new RectangleF(0, 0, con.Width, con.Height);
            this.FrameAreaInfoList.ForEach(x =>
            {
                x.Render(framearearect, gra, con.Font);
            });


            //全要素の描画
            this.ElementList.ForEach(x => x.Render(proj, gra));


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
        /// 対象位置のエリアを取得
        /// </summary>
        /// <param name="cpos"></param>
        public BaseUiElement? ColSelectArea(PointF cpos)
        {
            //elementlistは描画順に入っている、ということは最後の方が上に描画される＝当たり判定は逆でとると見かけ通り
            return this.ElementList.Reverse<BaseUiElement>().Where(x => x.ColMouse(cpos)).FirstOrDefault();
            //return this.ElementList.Where(x => x.ColMouse(cpos)).FirstOrDefault();
        }

        
        /// <summary>
        /// タグ情報の作り直し
        /// </summary>
        public void CreateTagData()
        {
            this.RemoveUiElement(EUiElementType.Tag);
            this.RemoveUiElement(EUiElementType.TagStart);
            this.RemoveUiElement(EUiElementType.TagEnd);

            MvGlobal.Project.TagList.ForEach(x =>
            {
                this.AddTagUiElement(x);
            });
        }

        /// <summary>
        /// タグの追加
        /// </summary>
        /// <param name="tag"></param>
        public void AddTagUiElement(BaseEditTag tag)
        {
            this.ElementList.Add(new TagUiElement(tag));            
            this.ElementList.Add(new TagStartElement(tag));
            this.ElementList.Add(new TagEndElement(tag));
        }

        /// <summary>
        /// 対象のタグを削除
        /// </summary>
        /// <param name="tag"></param>
        public void RemoveTagUiElement(BaseEditTag tag)
        {
            this.ElementList.RemoveAll(x =>
            {
                var data = x as BaseTagUiElement;
                if(data == null)
                {
                    return false;
                }

                if( data.Data == tag)
                {
                    return true;
                }

                return false;
            });
            
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
        /// 選択エリアの描画
        /// </summary>
        /// <param name="gra"></param>
        private void RenderSelectArea(Graphics gra)
        {     
            if(this.MouseOverElement == null)
            {
                return;
            }
            RectangleF rc = this.MouseOverElement.Area;

            using (SolidBrush bru = new SolidBrush(Color.FromArgb(80, Color.White)))
            {
                gra.FillRectangle(bru, rc);
            }
        }

        /// <summary>
        /// 描画情報の計算
        /// </summary>
        private void CalculateRendering(Control con, PointF offset, MvProject proj)
        {
            //フレーム情報の計算
            this.FrameAreaInfoList.ForEach(x =>
            {
                x.CalclateArea(con, offset, proj);
            });

            this.ElementList.ForEach(x => { x.ReCalcuArea(this.FrameAreaInfoList); });
        }


        /// <summary>
        /// 対象elementの削除
        /// </summary>
        /// <param name="ut"></param>
        private void RemoveUiElement(EUiElementType ut)
        {
            int debug = this.ElementList.RemoveAll(x => x.Type == ut);
        }

        /// <summary>
        /// フレーム画像描画者の作成
        /// </summary>
        private void CreateFrameImageElement()
        {
            this.RemoveUiElement(EUiElementType.FrameImage);

            this.FrameAreaInfoList.ForEach((x) =>
            {
                FrameImageUiElement e = new FrameImageUiElement(x);
                this.ElementList.Add(e);
            });
        }
    }
}
