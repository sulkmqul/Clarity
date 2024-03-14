using Clarity.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Collider
{

    /// <summary>
    /// 衝突判定一時情報
    /// </summary>
    internal class ColliderTempInfo
    {
        /// <summary>
        /// テンプレートターゲットタイプ
        /// </summary>
        public int TargetColType = 0;

        /// <summary>
        /// これの当たり判定
        /// </summary>
        internal List<BaseCollider> TempColliderList = new List<BaseCollider>();

        /// <summary>
        /// 対象のTargetフラグを落とす
        /// </summary>
        /// <param name="flag"></param>
        public void DownTargetColTypeFlag(int flag)
        {
            this.TargetColType = (this.TargetColType ^ flag) & this.TargetColType;
        }
    }

    /// <summary>
    /// 衝突判定情報の一式
    /// </summary>
    public class ColliderInfo
    {
        public ColliderInfo(BaseObject p)
        {
            this.Parent = p;
            this.ColliderSerialCode = ColliderSerialCodeGenerator.GetNextSerial();
        }

        /// <summary>
        /// 当たり判定有効可否
        /// </summary>
        public bool Enabled = true;


        /// <summary>
        /// 自分の当たり判定タイプ bit演算が行えるようにシフトを利用した単一フラグであること
        /// </summary>
        public int ColType = 0;

        /// <summary>
        /// 対象の当たり判定タイプ &指定せよ
        /// </summary>
        public int TargetColType = 0;

        /// <summary>
        /// この当たり判定を一意に識別するコード
        /// </summary>
        public long ColliderSerialCode { get; private set; }

        /// <summary>
        /// これの判定後の無敵付与時間(ms)
        /// </summary>
        public long GrantInvincibleTime = long.MinValue;

        /// <summary>
        /// これの親
        /// </summary>
        public BaseObject Parent { get; private set; }
        
        /// <summary>
        /// 当たり判定領域一式（元ネタ）
        /// </summary>
        public List<BaseCollider> SrcColliderList = new List<BaseCollider>();


        /// <summary>
        /// フレームごとに更新される情報一式
        /// </summary>
        internal ColliderTempInfo TempInfo = new ColliderTempInfo();


        /// <summary>
        /// 当たり判定の更新 TempInfoの情報を初期化し書き換える。
        /// </summary>                
        internal void UpdateTempInfo()
        {
            //フラグの初期化
            this.TempInfo.TargetColType = this.TargetColType;

            //前回のデータをクリア
            this.TempInfo.TempColliderList.Clear();
            //if (this.TempInfo.TempColliderList.Count != this.SrcColliderList.Count)
            //{
            //    this.TempInfo.TempColliderList = new List<BaseCollider>(this.SrcColliderList.Count);
            //    this.SrcColliderList.ForEach(x => this.TempInfo.TempColliderList.Add(null));
            //}

            //判定領域の変形してADDする
            int i = 0;
            this.SrcColliderList.ForEach(x =>
            {
                BaseCollider c = (BaseCollider)x.Clone();                
                c.TransformCollider(this.Parent.TransSet);
                c.Proc(0, new FrameInfo(0, 0)); //FrameInfoは意味を持たないので仮
                c.HitTempFlag = false;
                this.TempInfo.TempColliderList.Add(c);
                //this.TempInfo.TempColliderList[i] = c;
                i++;

            });

            
            //[そのうちつくります]
            //ここで無敵時間関係の更新をせよ。
            //将来的にはColliderSerialCodeと当たった時間、無敵時間を一覧で覚えておいて
            //無敵リストに入れる。
            //そして対象のserialcodeの無敵時間中は判定をとらない、として多段ヒット的にする予定。
            //そして、ここで無敵時間リストの更新をするべし。
            //すなわち、時間をチェックして無敵時間が過ぎている物を無敵リストから外すべし


        }



    }
}
