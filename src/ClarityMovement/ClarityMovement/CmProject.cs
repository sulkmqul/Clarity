using Clarity;
using Clarity.Engine;
using ClarityMovement.MotionFile;
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
    internal class CmProject :  IDisposable
    {
        public CmProject()
        {
            
        }
        
        /// <summary>
        /// フレームレート
        /// </summary>
        public double FrameRate { get; set; } = 1000.0 / 60.0;
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
        public int MaxFrame { get; set; } = 600;


        /// <summary>
        /// タグ修飾一式
        /// </summary>
        public List<BaseFrameModifier> ModifierList { get; set; } = new List<BaseFrameModifier>();


        /// <summary>
        /// 対象のタグを一括削除する。
        /// </summary>
        /// <param name="type"></param>
        public void ClearSelectTypeFrameModifier(ETagType type)
        {
            //画像設定のクリア            
            this.ModifierList.RemoveAll(x => x.TagType == type);
        }


        /// <summary>
        /// 解放処理
        /// </summary>
        public void Dispose()
        {
            this.ImageDataMana.Dispose();
        }


        /// <summary>
        /// 含まれている画像タグの一覧を取得する
        /// </summary>
        /// <returns></returns>
        public List<FrameImageModifier> ImageTagList
        {
            get
            {
                return this.ModifierList.Where(x => x.TagType == ETagType.Image).Select(z => (FrameImageModifier)z).ToList();
            }
        }



        /// <summary>
        /// 含まれているタグの一覧を取得する
        /// </summary>
        /// <returns></returns>
        public List<FrameTagModifier> TagDataList
        {
            get
            {
                return this.ModifierList.Where(x => x.TagType == ETagType.Tag).Select(z => (FrameTagModifier)z).ToList();
            }
        }

    }


    /// <summary>
    /// エリア種別
    /// </summary>
    public enum ETagType
    {   
        Image,
        Tag,

        //--//--//--//--//--//--//--//--//--//--//
        None = 999,
    }



    /// <summary>
    /// フレーム修飾基底クラス
    /// </summary>
    public class BaseFrameModifier
    {
        public BaseFrameModifier(ETagType tt)
        {
            this.TagType = tt;
         
        }

        /// <summary>
        /// タグ種別
        /// </summary>
        public ETagType TagType { get; init; } = ETagType.None;

        /// <summary>
        /// 設定フレーム
        /// </summary>
        public int Frame { get; set; } = 0;


        /// <summary>
        /// 設定フレーム長
        /// </summary>
        public int FrameSpan { get; set; } = 1;

        /// <summary>
        /// 対象フレームに属しているかをチェックする
        /// </summary>
        /// <param name="f">フレーム番号</param>
        /// <returns></returns>
        public bool CheckFrame(int f)
        {
            int st = this.Frame;
            int ed = this.Frame + this.FrameSpan;

            if (st <= f && f < ed)
            {
                return true;
            }
            return false;

        }
    }


    /// <summary>
    /// 表示画像定義
    /// </summary>
    public class FrameImageModifier : BaseFrameModifier
    {
        public FrameImageModifier() : base(ETagType.Image)
        {
        }

        /// <summary>
        /// 表示対象画像ID
        /// </summary>
        public int ImageDataID { get; set; } = -1;

        
    }

    /// <summary>
    /// 修飾タグ
    /// </summary>
    public class FrameTagModifier : BaseFrameModifier
    {
        public FrameTagModifier() : base(ETagType.Tag)
        {
        }

        /// <summary>
        /// タグID
        /// </summary>
        public int TagId
        {
            get
            {
                return this.Data.Id;
            }
            set
            {
                this.Data.Id = value;
            }
        }

        /// <summary>
        /// タグのコード
        /// </summary>
        public string TagCode
        {
            get
            {
                return this.Data.TagName;
            }
            set
            {
                this.Data.TagName = value;
            }
        }

        /// <summary>
        /// これのデータ
        /// </summary>
        public ClarityMotionTag Data = new ClarityMotionTag();


    }


}
