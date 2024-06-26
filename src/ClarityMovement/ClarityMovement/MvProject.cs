﻿using Clarity.Collider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClarityMovement
{
    /// <summary>
    /// プロジェクト情報
    /// </summary>
    internal class MvProject
    {
        /// <summary>
        /// 基準画像一覧
        /// </summary>
        public List<Bitmap> BaseImageList { get; set; } = new List<Bitmap>();

        /// <summary>
        /// 追加情報
        /// </summary>
        public List<BaseEditTag> TagList { get; private set; } = new List<BaseEditTag>();

        /// <summary>
        /// タグの追加
        /// </summary>
        /// <param name="tag"></param>
        public void AddTag(BaseEditTag tag)
        {
            this.TagList.Add(tag);
            //indexの再割り当て
            int index = 0;
            this.TagList.ForEach(x => x.Index = index++);

            MvGlobal.SendEventUI(EMovementUIEvent.TagAdd, tag);
        }
        /// <summary>
        /// タグの削除
        /// </summary>
        /// <param name="tag"></param>
        public void RemoveTag(BaseEditTag tag)
        {
            this.TagList.Remove(tag);
            //indexの再割り当て
            int index = 0;
            this.TagList.ForEach(x => x.Index = index++);

            MvGlobal.SendEventUI(EMovementUIEvent.TagRemove, tag);
        }
    }

    /// <summary>
    /// タグ情報編集基底
    /// </summary>
    public abstract class BaseEditTag : BaseMovementTag
    {
        public BaseEditTag(EMovementTagType type) : base(type)
        {
        }

        public int Index { get; set; } = 0;

        /// <summary>
        /// 開始フレーム
        /// </summary>
        public int StartFrame { get; set; } = 0;

        /// <summary>
        /// 終了フレーム
        /// </summary>
        public int EndFrame { get; set; } = 0;
    }


    public class EditTagCollison : BaseEditTag
    {
        public EditTagCollison() : base(EMovementTagType.Collision)
        {

        }

        /// <summary>
        /// 当たり判定モード
        /// </summary>
        public EColMode ColMode { get; set; }

        /// <summary>
        /// 円の時の半径
        /// </summary>
        public float Radius { get; set; } = 0;

        /// <summary>
        /// 矩形の時のサイズ
        /// </summary>
        public Vector2 RectSize { get; set; } = new Vector2();

    }
}

