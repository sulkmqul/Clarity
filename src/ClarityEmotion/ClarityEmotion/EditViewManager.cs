using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Clarity.GUI;
using ClarityEmotion.Core;

namespace ClarityEmotion
{
    /// <summary>
    /// 編集メインの管理
    /// </summary>
    internal class EditViewManager
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="cv">管理Viewer</param>
        public EditViewManager(ClarityImageViewer cv)
        {
            this.Viewer = cv;
        }

        /// <summary>
        /// 管理View
        /// </summary>
        private ClarityImageViewer Viewer;

        EditViewRenderer Renderer = new EditViewRenderer();

        


        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            this.Renderer = new EditViewRenderer();           


            //描画フレームの変更
            CeGlobal.Event.FrameChange.Subscribe(x =>
            {
                this.Renderer.Frame = x.Frame;
                this.Viewer.Refresh();
            });

            //プロジェクト作成
            CeGlobal.Event.ValueChange.Where(x => (x.EventID & EEventID.CreateProject) == EEventID.CreateProject).Subscribe(x =>
            {
                this.Viewer.Init(new SizeF(CeGlobal.Project.BasicInfo.ImageWidth, CeGlobal.Project.BasicInfo.ImageHeight));
                this.Viewer.AddDisplayer(this.Renderer);
            });

            //値変更時
            CeGlobal.Event.ValueChange.Where(x => (x.EventID & EEventID.LayerUpdate) == EEventID.LayerUpdate).Subscribe(x =>
            {
                this.Viewer.Refresh();
            });

            //レイヤーの追加、削除
            CeGlobal.Event.ValueChange.Where(x => (x.EventID & EEventID.AddLayer) == EEventID.AddLayer || (x.EventID & EEventID.RemoveLayer) == EEventID.RemoveLayer).Subscribe(x =>
            {
                this.Renderer.Core.Init(CeGlobal.Project.Anime.LayerList);

            });

            CeGlobal.Event.ValueChange.Where(x => (x.EventID & EEventID.Player) == EEventID.Player).Subscribe(x =>
            {
                this.Renderer.EditFlag = !((bool)x.Data);
                this.Viewer.Refresh();
            });



        }
    }
}
