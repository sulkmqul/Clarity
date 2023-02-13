using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClarityMovement
{
    /// <summary>
    /// ClarityMovementプロジェクトファイル
    /// </summary>
    internal class CmProject
    {
        public CmProject()
        {
            
        }
        
        /// <summary>
        /// フレームレート
        /// </summary>
        public double FrameRate { get; set; } = 1000.0 / 60.0;
        
        /// <summary>
        /// 元画像ファイルパス一覧(zip or image file)
        /// </summary>
        //public List<string> SrcImageFilePath = new List<string>();
        
        /// <summary>
        /// 画像データ管理
        /// </summary>
        public ImageDataManager ImageDataMana { get; set; } = new ImageDataManager();

        /// <summary>
        /// 描画サイズ
        /// </summary>
        public Vector2 RenderingSize { get; set; } = new Vector2(0, 0);


        /// <summary>
        /// 最大フレーム数
        /// </summary>
        public int Frame { get; set; } = 10;
        

    }




    public class IFrameModifier
    {
        public int Frame { get; set; } = 0;
    }


    /// <summary>
    /// 表示画像定義
    /// </summary>
    public class FrameImageModifier : IFrameModifier
    {
        /// <summary>
        /// 表示フレーム数
        /// </summary>
        public int FrameSpan { get; set; } = 1;

        /// <summary>
        /// 表示対象画像ID
        /// </summary>
        public int ImageDataID { get; set; } = -1;

        
    }

    /// <summary>
    /// 修飾タグ
    /// </summary>
    public class FrameTagModifier : IFrameModifier
    {
        public int TagId { get; set; } = 0;

        public string TagCode { get; set; } = "";


    }


}
