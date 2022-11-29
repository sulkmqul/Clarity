using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using System.Numerics;
using Clarity.Engine;
using Clarity.GUI;
using Clarity.Engine.Element;
using Clarity.Collider;
using System.Drawing;

namespace ClarityOrbit.EditView
{
    /// <summary>
    /// マウス管理エレメント
    /// </summary>
    internal class MouseManageElement : ClarityObject
    {
        public MouseManageElement()
        {
        }

        /// <summary>
        /// これのマウス情報
        /// </summary>
        public MouseInfo MInfo = new MouseInfo();

        /// <summary>
        /// 選択エリア描画可否
        /// </summary>
        public bool SelectAreaRenderFlag = false;
        

        /// <summary>
        /// 初期化
        /// </summary>
        protected override void InitElement()
        {
            //マウスの処理
            this.TransSet.WorldID = OrbitGlobal.OrbitWorldID;

            //自身の色を規定
            this.Color = new Vortice.Mathematics.Color4(1.0f, 0.0f, 0.0f, 1.0f);

            //描画設定
            this.SetVertexCode(EVertexCode.VGrid);
            this.SetShaderCode(EShaderCode.System_NoTexture);            
            this.RenderBehavior = new GirdFrameRenderBehavior();

            //描画を初期化
            this.SelectAreaRenderFlag = false;

            //所作は当たり判定を更新する物体が必要
            //これはscale rateなどの既存変換とは一線を画す処理であるため
            this.AddProcBehavior(new ActionBehavior((x) =>
            {
                //描画を初期化
                this.SelectAreaRenderFlag = false;

                if (OrbitGlobal.Project == null)
                {
                    return;
                }

                //当たり判定位置を修正する
                //マウス位置のworld座標を取得
                Vector3 stpos = ClarityEngine.WindowToWorld(this.TransSet.WorldID, this.MInfo.NowPos.X, this.MInfo.NowPos.Y, 0.0f);
                Vector3 edpos = ClarityEngine.WindowToWorld(this.TransSet.WorldID, this.MInfo.NowPos.X, this.MInfo.NowPos.Y, 1.0f);                

                //当たり判定を作り直す
                Vector3 dir = edpos - stpos;

                //線の当たり位置取得
                Vector3? cposs =  Clarity.Mathematics.ClarityMath.CalcuCrossPointInfinityPlaneLine(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 1.0f),
                    stpos, dir);
                if (cposs == null)
                {
                    return;
                }
                
                //マウス辺り位置から選択gridを算出
                int ix = (int)Math.Round(-(cposs.Value.X + (OrbitGlobal.Project.BaseInfo.TileSize.Width * 0.5f)) / OrbitGlobal.Project.BaseInfo.TileSize.Width) + 1;
                int iy = (int)Math.Round(-(cposs.Value.Y + (OrbitGlobal.Project.BaseInfo.TileSize.Height * 0.5f)) / OrbitGlobal.Project.BaseInfo.TileSize.Height) + 1;


                int w = OrbitGlobal.ControlInfo.SrcSelectedInfo?.SelectedIndexRect.Width ?? 1;
                int h = OrbitGlobal.ControlInfo.SrcSelectedInfo?.SelectedIndexRect.Height ?? 1;

                //選択矩形情報を算出るう
                OrbitEditViewControl.TempInfo.SelectTileRect = new Rectangle(ix, iy, w, h);

                //描画を初期化
                this.SelectAreaRenderFlag = true;

            }));
        }

        

        /// <summary>
        /// 描画設定
        /// </summary>
        protected override void RenderElemenet()
        {
            if (this.SelectAreaRenderFlag == false)
            {
                return;
            }

            //気に食わないが例外的にここで描画位置の設定をする
            {
                if (OrbitGlobal.Project == null)
                {
                    return;
                }

                var rect = OrbitEditViewControl.TempInfo.SelectTileRect;
                Size tsize = OrbitGlobal.Project.BaseInfo.TileSize;

                float px = (rect.X-1) * -tsize.Width;
                float py = (rect.Y-1) * -tsize.Height;
                this.TransSet.Pos2D = new System.Numerics.Vector2(px, py);
                this.TransSet.Scale2D = new System.Numerics.Vector2(rect.Width * -tsize.Width, rect.Height * -tsize.Height);

                

            }

            base.RenderElemenet();
        }

    }
}
