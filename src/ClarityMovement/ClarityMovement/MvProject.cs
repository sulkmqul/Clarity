using System;
using System.Collections.Generic;
using System.Linq;
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


    }

    /// <summary>
    /// タグ情報編集基底
    /// </summary>
    public abstract class BaseEditTag : BaseMovementTag
    {
        public BaseEditTag(EMovementTagType type) : base(type)
        {
        }


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

    }
}

