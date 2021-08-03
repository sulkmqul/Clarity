using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Element.Scene
{
    /// <summary>
    /// シーン管理基底
    /// </summary>
    public class SceneManager : BaseClarityAdminObject, IClarityElementEvent
    {

        /// <summary>
        /// Scene管理の本体
        /// </summary>
        internal static SceneManager Manager = null;


        /// <summary>
        /// フェードイン、アウトの速度
        /// </summary>
        private float FadeSpeed = 1.0f;

        #region メンバ変数

        /// <summary>
        /// 現在実行中のシーン番号
        /// </summary>
        protected int SceneNo = ClarityEngine.INVALID_ID;

        /// <summary>
        /// 次の要求シーンNo
        /// </summary>
        protected int NextRequestSceneNo = ClarityEngine.INVALID_ID;


        /// <summary>
        /// シーン管理Dic
        /// </summary>
        protected Dictionary<int, BaseScene> SceneDic = new Dictionary<int, BaseScene>();

        #endregion


        /// <summary>
        /// フェードのAdd
        /// </summary>
        /// <param name="f">true=フェードイン false=フェードアウト</param>
        /// <param name="addevent">Event追加可否</param>
        protected void AddFadeObject(bool f, bool addevent = false)
        {
            float fspeed = (f == true) ? -this.FadeSpeed : this.FadeSpeed;

            FadeObject fo = new FadeObject(fspeed);
            if (addevent == true)
            {
                fo.AddEventSenderList(this);
            }

            ElementManager.Mana.AddRequest(fo);
        }


        /// <summary>
        /// 現在の実行シーンの取得
        /// </summary>
        protected BaseScene CurrentScene
        {
            get
            {
                if (this.SceneNo == ClarityEngine.INVALID_ID)
                {
                    return null;
                }
                return this.SceneDic[this.SceneNo];
            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        protected sealed override void InitElement()
        {
            this.InitSceneManager();
        }


        /// <summary>
        /// 処理
        /// </summary>
        protected sealed override void ProcElement()
        {
            this.ProcSceneManager();
        }



        /// <summary>
        /// シーン管理の初期化
        /// </summary>
        protected virtual void InitSceneManager()
        {
            
        }

        /// <summary>
        /// シーンの実行
        /// </summary>
        protected virtual void ProcSceneManager()
        {
            //対象シーンの取得
            this.CurrentScene?.Proc();
        }


        /// <summary>
        /// イベント処理
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="data"></param>
        //public void EventCallback(int eid, BaseElement data)
        public override void EventCallback(int eid, BaseElement data)
        {
            //削除処理でない
            if (eid != ClarityElementEventID.Remove)
            {
                return;
            }

            //次のシーンが設定されていない
            if (this.NextRequestSceneNo == ClarityEngine.INVALID_ID)
            {
                return;
            }

            //ここで実際の切り替え処理を行う
            this.ChangeScene();
        }



        /// <summary>
        /// シーンの切り替え
        /// </summary>
        private void ChangeScene()
        {
            //必要ならここでLoadシーンの実行初期化を行い、遷移せよ。Load後、再構築される
            
            
            this.CurrentScene?.Release();

            this.SceneNo = this.NextRequestSceneNo;
            this.NextRequestSceneNo = ClarityEngine.INVALID_ID;

            //シーンの初期化
            this.CurrentScene.Init();

            //FadeのADD
            this.AddFadeObject(true);
            
        }


        /// <summary>
        /// 次のシーン移行申請
        /// </summary>
        /// <param name="nsno"></param>
        public virtual void ChangeSceneRequest(int nsno)
        {
            this.NextRequestSceneNo = nsno;


            //フェード処理
            this.AddFadeObject(false, true);
        }


        /// <summary>
        /// シーン管理へ追加
        /// </summary>
        /// <param name="sc"></param>
        public void AddScene(BaseScene sc)
        {
            this.SceneDic.Add(sc.SceneNo, sc);
        }


        /// <summary>
        /// シーン実行開始
        /// </summary>
        /// <param name="sno"></param>
        public virtual void ExecuteScene(int sno)
        {
            this.AddFadeObject(true);

            this.SceneNo = sno;
            this.CurrentScene.Init();
        }
        
    }
}
