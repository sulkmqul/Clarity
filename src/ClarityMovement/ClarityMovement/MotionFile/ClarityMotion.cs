using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using Clarity.Engine;
using ClarityMovement.MotionFile;

namespace ClarityMovement.MotionFile
{  

    /// <summary>
    /// モーション管理データ
    /// </summary>
    public class ClarityMotion
    {
        public ClarityMotion() 
        {
        }

        /// <summary>
        /// 管理ID
        /// </summary>
        public int MotionID { get; set; } = ClarityEngine.INVALID_ID;
        /// <summary>
        /// 管理コード
        /// </summary>
        public string MotionCode { get; set; } = "";

        /// <summary>
        /// 最大フレーム数
        /// </summary>
        public int MaxFrame
        {
            get
            {
                return this.FrameList.Count;
            }
        }


        /// <summary>
        /// これのフレーム情報一式(フレーム数分存在)
        /// </summary>
        public List<ClarityMotionFrame> FrameList { get; private set; } = new List<ClarityMotionFrame>();


    }

    /// <summary>
    /// モーションのタグ
    /// </summary>
    public class ClarityMotionTag : ClarityData
    {
        public int Id { get; set; } = 0;
    }


    /// <summary>
    /// 1フレームの情報
    /// </summary>
    public class ClarityMotionFrame
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="no">フレーム番号</param>
        public ClarityMotionFrame(int no)
        {
            this.FrameNo = no;
        }

        /// <summary>
        /// フレーム番号
        /// </summary>
        public int FrameNo { get; init; } = 0;

        /// <summary>
        /// このフレームのテクスチャ情報
        /// </summary>
        public ClarityMotionFrameImage TexInfo { get; set; } = new ClarityMotionFrameImage();
        //public string Code { get; set; } = "";


        /// <summary>
        /// タグ情報
        /// </summary>
        public List<ClarityMotionTag> TagList { get; set; } = new List<ClarityMotionTag>();
    }

    /// ファイルデータフレーム情報
    /// </summary>
    public class ClarityMotionFrameImage
    {
        /// <summary>
        /// テクスチャコード
        /// </summary>
        public string TextureCode = "";

        /// <summary>
        /// 表示indexX
        /// </summary>
        public int IndexX = 0;
        /// <summary>
        /// 表示indexY
        /// </summary>
        public int IndexY = 0;

        public override string ToString()
        {
            string ans = $"{this.TextureCode},{this.IndexX},{this.IndexY}";
            return ans;
        }
    }
}
