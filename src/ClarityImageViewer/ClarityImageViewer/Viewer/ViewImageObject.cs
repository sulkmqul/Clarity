using Clarity.Engine;
using Clarity.Engine.Element;
using Clarity.Engine.Texture;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClarityImageViewer.Viewer
{


    /// <summary>
    /// 画面描画
    /// </summary>
    internal class ViewImageObject : ClarityObject
    {
        public ViewImageObject(DxControl parent, TextureAnimationInfo idata)
        {
            this.PCon = parent;
            this.IData = idata;
            this.TexAnimeBehavior = new TextureAnimeBehavior(this.IData);
        }

        
        /// <summary>
        /// 最大拡大率
        /// </summary>
        public float MaxScaleRate { get; set; } = 50.0f;

        /// <summary>
        /// 最小拡大率
        /// </summary>
        public float MinScaleRate
        {
            get
            {
                float brate = Math.Min(this.FitScaleRate, 1.0f);
                return brate * 0.5f;
            }
        }

        /// <summary>
        /// 拡大率
        /// </summary>
        public float ScaleRate
        {
            get
            {
                return this.TransSet.ScaleRate;
            }
            private set
            {
                this.TransSet.ScaleRate = value;
            }
        }

        /// <summary>
        /// 画像表示情報
        /// </summary>
        private TextureAnimationInfo IData;

        /// <summary>
        /// 親画面
        /// </summary>
        private DxControl PCon;

        /// <summary>
        /// 現在のFitScale(表示画像の実サイズが1.0とする)
        /// </summary>
        private float FitScaleRate = 1.0f;

        /// <summary>
        /// テクスチャアニメ所作
        /// </summary>
        private TextureAnimeBehavior TexAnimeBehavior;

        /// <summary>
        /// 諸々踏まえた画像サイズ
        /// </summary>
        private Vector2 ApplyImageSize = new Vector2(0.0f);
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 画面サイズと揃える
        /// </summary>
        /// <param name="cw">画面サイズW</param>
        /// <param name="ch">画面サイズH</param>
        public void FitImage(int cw, int ch)
        {
            //Fitsizeを計算する時回転を考慮する
            Matrix4x4 mat = Matrix4x4.CreateRotationZ(this.TransSet.RotZ);
            Vector4 v = Vector4.Transform(this.TransSet.Scale2D, mat);
            Vector2 csize = new Vector2(Math.Abs(v.X), Math.Abs(v.Y));

            //float wrate = (float)cw / (float)this.IData.ImageWidth;
            //float hrate = (float)ch / (float)this.IData.ImageHeight;
            float wrate = (float)cw / csize.X;
            float hrate = (float)ch / csize.Y;

            float rate = this.TransSet.ScaleRate = Math.Min(wrate, hrate);
            this.FitScaleRate = rate;
            this.ChangeScaling(cw, ch, rate);
        }

        /// <summary>
        /// スケールの変更
        /// </summary>
        /// <param name="width">画面サイズW</param>
        /// <param name="height">画面サイズH</param>
        /// <param name="mpos">マウス位置</param>
        /// <param name="f">true=拡大 false=縮小</param>
        public void ChangeScalingStep(float width, float height, Vector2 mpos, bool f)
        {            
            //適切な拡大率の計算
            float rate = this.CalcuNextScaleRate(f);
            rate = Math.Max(rate, this.MinScaleRate);
            rate = Math.Min(rate, this.MaxScaleRate);

            this.ChangeScaling(width, height, rate);
        }

        /// <summary>
        /// 拡大率の設定
        /// </summary>
        /// <param name="scale">等倍 1.0f</param>
        /// <param name="width">画面サイズW</param>
        /// <param name="height">画面サイズH</param>
        public void ChangeScaling(float width, float height, float scale)
        {
            this.ScaleRate = scale;

            //実画像サイズ
            {                

                Matrix4x4 mat = Matrix4x4.CreateRotationZ(this.TransSet.RotZ);
                Vector4 v = Vector4.Transform(this.TransSet.Size2D, mat);
                this.ApplyImageSize = new Vector2(Math.Abs(v.X), Math.Abs(v.Y));
                this.ApplyImageSize /= v.W;
            }
            

            this.LimitPosition(width, height);
        }


        /// <summary>
        /// 移動量計算
        /// </summary>
        /// <param name="width">画面W</param>
        /// <param name="heigt">画面H</param>
        /// <param name="vec">移動量</param>
        public void MoveImage(float width, float height, Vector2 vec)
        {
            this.TransSet.PosX -= vec.X;
            this.TransSet.PosY -= vec.Y;

            this.LimitPosition(width, height);
        }

        

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 初期化処理
        /// </summary>
        protected override void InitElement()
        {
            this.TransSet.WorldID = 1;

            //自身の最低限のデータを設定する
            this.TextureID = 1;
            this.VertexID = ClarityEngine.BuildInPolygonModelIndex.Rect;
            this.ShaderID = ClarityEngine.BuildInShaderIndex.Default;            
            this.TransSet.Pos = new Vector4(0.0f);
            this.TransSet.Scale3D = new Vector3(this.IData.ImageWidth, this.IData.ImageHeight, 1.0f);
                        
            this.AddProcBehavior(this.TexAnimeBehavior);
        }

        protected override void PostProcess()
        {
            
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 次の拡大率を計算する
        /// </summary>
        /// <param name="f">true=拡大</param>
        /// <returns></returns>
        private float CalcuNextScaleRate(bool f)
        {
            float nrate = this.ScaleRate;


            float ans = nrate;
            if (f == true)
            {
                if (nrate <= 1.0)
                {
                    ans += 0.1f;
                }
                else
                {
                    ans = nrate * 1.1f;
                }
            }
            else
            {
                if (nrate <= 1.0)
                {
                    ans -= 0.1f;
                }
                else
                {
                    ans = nrate * 0.9f;
                }
            }

            //ある程度の範囲に入ったら1.0倍にする
            if(0.95 < ans && ans <= 1.05)
            {
                ans = 1.0f;
            }
            

            return ans;
        }


        /// <summary>
        /// 移動制限を設ける
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void LimitPosition(float width, float height)
        {
            //移動制限
            Vector2 pos = this.TransSet.Pos2D;
            //Vector2 size = this.TransSet.Size2D;
            Vector2 size = this.ApplyImageSize;

            //画像が画面サイズに収まってる場合は移動しない            
            if (size.X <= width)
            {
                pos.X = 0;
            }
            if (size.Y <= height)
            {
                pos.Y = 0;
            }


            //幅より大きいなら処理
            if (size.X > width)
            {
                //端が出ないようにする
                float wh = width * 0.5f;
                float left = pos.X + (size.X * 0.5f);
                if (left < wh)
                {
                    float sa = wh - (size.X * 0.5f);
                    pos.X = sa;
                }


                float right = pos.X - (size.X * 0.5f);
                if (right > -wh)
                {
                    float sa = wh - (size.X * 0.5f);
                    pos.X = -sa;
                }
            }

            //高さより大きいなら処理
            if (size.Y > height)
            {
                
                float hh = height * 0.5f;
                float top = pos.Y + (size.Y * 0.5f);
                if (top < hh)
                {
                    float sa = hh - (size.Y * 0.5f);
                    pos.Y = sa;
                }

                float bottom = pos.Y - (size.Y * 0.5f);
                if (bottom > -hh)
                {
                    float sa = hh - (size.Y * 0.5f);
                    pos.Y = -sa;
                }
            }

            //反映
            this.TransSet.Pos2D = pos;
        }
    }


}
