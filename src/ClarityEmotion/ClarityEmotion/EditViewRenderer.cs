using Clarity.GUI;
using ClarityEmotion.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClarityEmotion
{
    /// <summary>
    /// 編集Viewの描画者
    /// </summary>
    internal class EditViewRenderer : Clarity.GUI.BaseDisplayer
    {
        public int Frame { get; set; } = 0;

        /// <summary>
        /// 描画ｺｱ
        /// </summary>
        public EmotionCore Core { get; private set; } = new EmotionCore();


        /// <summary>
        /// 編集可否
        /// </summary>
        public bool EditFlag { get; set; } = true;
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 描画されるとき
        /// </summary>
        /// <param name="gra"></param>
        public override void Render(Graphics gra)
        {
            //base.Render(gra);
            this.Core.GenerateEmotion(this.Frame, gra, this, this.EditFlag, CeGlobal.Project.SelectLayerData?.LayerNo ?? -1);
        }


        /// <summary>
        /// マウスが押された時
        /// </summary>
        /// <param name="minfo"></param>
        public override void MouseDown(MouseInfo minfo)
        {
            //選択元の取得
            var seldata = this.DetectMouseData(minfo.DownPos);
            if (seldata == null)
            {
                //選択なし
                return;
            }

            //選択に変更があったら通知
            if (CeGlobal.Project.Info.SelectLayerNo != seldata.SrcData.LayerNo)
            {
                CeGlobal.Project.Info.SelectLayerNo = seldata.SrcData.LayerNo;
                CeGlobal.Event.SendValueChangeEvent(EEventID.LayerSelectedChanged, seldata.SrcData);
            }
            

            minfo.SetMemory(seldata.SrcData.EaData.Pos2D.X, 0);
            minfo.SetMemory(seldata.SrcData.EaData.Pos2D.Y, 1);

        }

        /// <summary>
        /// マスウ動いたとき
        /// </summary>
        /// <param name="minfo"></param>
        public override void MouseMove(MouseInfo minfo)
        {
            //選択なし
            if(CeGlobal.Project.SelectLayerData == null) { return; }

            //drag中
            if (minfo.DownFlag == true && minfo.DownButton == MouseButtons.Left)
            {                
                PointF p = minfo.DownLength;

                int px = minfo.GetMemory<int>(0);
                int py = minfo.GetMemory<int>(1);

                CeGlobal.Project.SelectLayerData.EaData.Pos2D.X = (int)p.X + px;
                CeGlobal.Project.SelectLayerData.EaData.Pos2D.Y = (int)p.Y + py;

                CeGlobal.Event.SendValueChangeEvent(EEventID.LayerUpdate, CeGlobal.Project.SelectLayerData);
                return;
            }

            var selsrclist = this.CreateSelectSrc();
            selsrclist.ForEach(x => x.SrcData.TempData.MouseOverFlag = false);
            var ae = this.DetectMouseData(minfo.NowPos);
            if (ae != null)
            {
                ae.SrcData.TempData.MouseOverFlag = true;
            }


        }

        /// <summary>
        /// マウスが離された時
        /// </summary>
        /// <param name="minfo"></param>
        public override void MouseUp(MouseInfo minfo)
        {
            if (CeGlobal.Project.SelectLayerData == null) { return; }

            if (minfo.DownFlag == true)
            {
                CeGlobal.Event.SendValueChangeEvent(EEventID.LayerUpdate, CeGlobal.Project.SelectLayerData);
                return;
            }
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// マウス位置対象のデータを取得する
        /// </summary>
        /// <param name="mpos"></param>
        /// <returns></returns>
        private EmotionCore.CoreData? DetectMouseData(Point mpos)
        {
            //選択元の対象を取得する
            var selsrclist = this.CreateSelectSrc();

            var ans = selsrclist.Where(x => x.DisplayRect.Contains(mpos)).FirstOrDefault();
            return ans;
        }



        /// <summary>
        /// 選択元となるデータを作成する。
        /// </summary>
        /// <returns></returns>
        private List<EmotionCore.CoreData> CreateSelectSrc()
        {
            List<EmotionCore.CoreData> anslist = new List<EmotionCore.CoreData>();

            int selno = CeGlobal.Project.Info.SelectLayerNo;

            //選択が一番初めに
            EmotionCore.CoreData? seldata = this.Core.CoreList.Where(x => x.SrcData.LayerNo == selno).FirstOrDefault();
            if (seldata != null)
            {
                anslist.Add(seldata);
            }

            //以後はlayer順
            var aa = this.Core.CoreList.OrderByDescending(x => x.SrcData.LayerNo).ToList();
            anslist.AddRange(aa);

            return anslist;
        }



    }
}
