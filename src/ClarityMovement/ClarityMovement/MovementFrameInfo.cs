using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClarityMovement
{
    /// <summary>
    /// モーション情報 Project情報から変換して作るclarity使用形式
    /// </summary>
    public class MovementData
    {
        public List<MovementFrameInfo> FrameList { get; set; } = new List<MovementFrameInfo>();
    }

    /// <summary>
    /// フレーム情報
    /// </summary>
    public class MovementFrameInfo
    {
        public MovementFrameInfo(int fno, Bitmap image)
        {
            this.FrameNo = fno;
            this.BaseImage = image;
        }

        /// <summary>
        /// フレーム番号
        /// </summary>
        public int FrameNo { get; set; } = 0;

        /// <summary>
        /// 基準画像
        /// </summary>
        public Bitmap BaseImage { get; set; }


        /// <summary>
        /// このフレームのタグリスト
        /// </summary>
        public List<BaseMovementTag> TagList { get; set; } = new List<BaseMovementTag>();
    }

    /// <summary>
    /// タグのタイプ
    /// </summary>
    public enum EMovementTagType
    {
        Collision,
        Image,
        Info,
    }

    /// <summary>
    /// フレームタグ情報基底
    /// </summary>
    public abstract class BaseMovementTag
    {
        public BaseMovementTag(EMovementTagType mt)
        {
            this.Type = mt;
        }

        /// <summary>
        /// タイプ
        /// </summary>
        public EMovementTagType Type { get; set; }

        /// <summary>
        /// 基準画像からの相対位置
        /// </summary>
        public Vector4 RelativePos { get; set; } = new Vector4();


        /// <summary>
        /// タグ
        /// </summary>
        public object Tag { get; set; } = 0;
    }
}
