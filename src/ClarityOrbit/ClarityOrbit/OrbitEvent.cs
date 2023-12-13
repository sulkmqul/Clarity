using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityOrbit
{
    /// <summary>
    /// 画面イベントIDを定義する
    /// </summary>
    internal enum EOrbitEventID
    {
        //プロジェクトの作成
        CreateProject,


        //タイル元画像選択エリアの変更
        TileSrcImageSelectAreaChanged,

        //タイル元画像追加
        TileSrcImageAdd,
        //タイル元画像削除
        TileSrcImageRemove,
    }


    /// <summary>
    /// コントロールイベントデータ
    /// </summary>
    internal class OrbitEvent
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="ev">イベントID</param>
        public OrbitEvent(EOrbitEventID ev)
        {
            this.EventID = ev;
            this.Value = null;
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="ev">イベントID</param>
        /// <param name="val">設定値</param>
        public OrbitEvent(EOrbitEventID ev, object? val)
        {
            this.EventID = ev;
            this.Value = val;
        }

        /// <summary>
        /// イベント情報
        /// </summary>
        public EOrbitEventID EventID { get; init; }

        /// <summary>
        /// 追加情報(event idによる)
        /// </summary>
        public object? Value { get; init; } = null;

    }


    /// <summary>
    /// タイルイベントデータ
    /// </summary>
    internal class TileEditEvent
    {
        public TileEditEvent() 
        {
        }

        /// <summary>
        /// 編集対象レイヤーID
        /// </summary>
        public int LayerID = 0;
        /// <summary>
        /// 編集対象位置
        /// </summary>
        public Point Location = new Point(0, 0);

        /// <summary>
        /// 設定タイル元画像ID
        /// </summary>
        public int TileSrcImageID = 0;
        /// <summary>
        /// 設定タイルID
        /// </summary>
        public int TileID = 0;
    }


}
