using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using System.Numerics;
using Clarity.Engine;
using Clarity.GUI;

namespace ClarityOrbit.EditView
{
    internal class EditViewDefine
    {
        public const int MouseColType = 1 << 0;
        public const int GridColType = 1 << 2;
    }


    /// <summary>
    /// マウス管理エレメント
    /// </summary>
    internal class MouseManageElement : BaseElement
    {
        
        /// <summary>
        /// これのマウス情報
        /// </summary>
        public MouseInfo MInfo = new MouseInfo();

        /// <summary>
        /// 初期化
        /// </summary>
        protected override void InitElement()
        {
            //マウスの処理
            this.TransSet.WorldID = OrbitGlobal.OrbitWorldID;

            //当たり判定の追加
            this.ColInfo = new Clarity.Collider.ColliderInfo(this);
            this.ColInfo.ColType = EditViewDefine.MouseColType;
            this.ColInfo.TargetColType = EditViewDefine.GridColType;
            
            Clarity.Collider.ColliderLine line = new Clarity.Collider.ColliderLine(new Vector3(0.0f), new Vector3(0.0f));            
            this.ColInfo.SrcColliderList.Add(line);

            //所作は当たり判定を更新する物体が必要
            //これはscale rateなどの既存変換とは一線を画す処理であるため
            this.AddProcBehavior(new ActionBehavior((x) =>
            {
                //当たり判定位置を修正する
                Vector3 stpos = ClarityEngine.WindowToWorld(this.TransSet.WorldID, this.MInfo.NowPos.X, this.MInfo.NowPos.Y, 0.0f);
                Vector3 edpos = ClarityEngine.WindowToWorld(this.TransSet.WorldID, this.MInfo.NowPos.X, this.MInfo.NowPos.Y, 1.0f);
                //Vector3 stpos = ClarityEngine.WindowToWorld(this.TransSet.WorldID, this.MInfo.DownPos.X, this.MInfo.DownPos.Y, 0.0f);
                //Vector3 edpos = ClarityEngine.WindowToWorld(this.TransSet.WorldID, this.MInfo.DownPos.X, this.MInfo.DownPos.Y, 1.0f);

                Vector3 dir = edpos - stpos;
                //当たり判定を作り直す
                this.ColInfo.SrcColliderList.Clear();
                Clarity.Collider.ColliderLine line = new Clarity.Collider.ColliderLine(stpos, dir);
                line.ColiderTransposeMode = Clarity.Collider.EColiderTransposeMode.None;
                this.ColInfo.SrcColliderList.Add(line);
            })); 
            
        }
    }
}
