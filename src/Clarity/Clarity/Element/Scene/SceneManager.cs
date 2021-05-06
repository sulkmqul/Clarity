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
        public void EventCallback(int eid, BaseElement data)
        {
            //削除処理でない
            if (eid != ClarityElementEventID.Destroy)
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
            
            
            this.CurrentScene.Release();

            this.SceneNo = this.NextRequestSceneNo;
            this.NextRequestSceneNo = ClarityEngine.INVALID_ID;

            //シーンの初期化
            this.CurrentScene.Init();

            //フェードインオブジェクトをADDせよ。
        }


        /// <summary>
        /// 次のシーン移行申請
        /// </summary>
        /// <param name="nsno"></param>
        public virtual void ChangeSceneRequest(int nsno)
        {
            this.NextRequestSceneNo = nsno;


            //フェードオブジェクトのADDとEvent通知設定処理
            
        }

        
    }
}
