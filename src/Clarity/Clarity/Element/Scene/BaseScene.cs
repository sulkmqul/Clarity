using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Element.Scene
{
    /// <summary>
    /// シーン基底
    /// </summary>
    public abstract class BaseScene
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="sno">シーン番号</param>
        public BaseScene(int sno)
        {
            this.SceneNo = sno;
        }

        /// <summary>
        /// シーン番号
        /// </summary>
        public int SceneNo { get; private set; } = 0;


        /// <summary>
        /// シーンの初期化
        /// </summary>
        protected abstract void InitScene();

        /// <summary>
        /// シーンの実行
        /// </summary>
        protected abstract void ProcScene();


        /// <summary>
        /// シーンの初期化
        /// </summary>
        internal virtual void Init()
        {
            //解放は下でやるべきかは検討が必要。
            //管理以外の解放
            //ElementManager.Mana.Clear();

            

            //リソースがきつい場合はリソース解放関係を入れる。必要なら汎用ロードシーンを作成せよ

            //初期化
            this.InitScene();
        }

        /// <summary>
        /// シーンの実行
        /// </summary>
        internal virtual void Proc()
        {
            this.ProcScene();
        }


        /// <summary>
        /// シーンの解放
        /// </summary>
        internal virtual void Release()
        {
        }


    }
}
