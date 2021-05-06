using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Element.Collider
{


    /// <summary>
    /// 衝突、当たり判定管理  当たり判定に関する部分のみを抽出します。
    /// </summary>
    class ColliderManager
    {
        public ColliderManager()
        {
        }

        /// <summary>
        /// 当たり判定管理dic [ColType, Object]
        /// </summary>
        private Dictionary<int, List<ICollider>> ColliderDic = new Dictionary<int, List<ICollider>>();

        /// <summary>
        /// 当たり判定コア クラス
        /// </summary>
        private ColliderCore Core = new ColliderCore();


        /// <summary>
        /// 当たり判定の情報を更新する
        /// </summary>
        /// <param name="frametime">今回のフレーム処理時間</param>
        private void UpdateCollisonInfomation(long frametime)
        {
            foreach (int key in this.ColliderDic.Keys)
            {
                //ここが並列すべきかは微妙だが個々で完結する点、大量にある可能性がある点などを考慮して並列化。                
                //問題があるなら変更せよ
                List<ICollider> iclist = this.ColliderDic[key];
                Parallel.ForEach(iclist, ic =>
                {
                    ic.ColInfo.UpdateTempInfo(frametime);                    
                });
            }

        }


        /// <summary>
        /// 自分と対象の当たり判定 true=どこかに当たった
        /// </summary>
        /// <param name="coi">自分</param>
        /// <param name="target">対象</param>
        /// <returns></returns>
        private bool Collider(ICollider coi, ICollider target)
        {
            List<BaseCollider> colist = coi.ColInfo.TempInfo.TempColliderList;
            List<BaseCollider> tarlist = target.ColInfo.TempInfo.TempColliderList;


            foreach (BaseCollider co in colist)
            {
                foreach (BaseCollider tar in tarlist)
                {
                    //当たり判定を行う
                    bool ret = this.Core.DetectCollider(co, tar);
                    if (ret == false)
                    {
                        continue;
                    }

                    //判定色の変更
                    co.Color = ClarityEngine.Setting.Debug.ColliderContactColor;
                    tar.Color = ClarityEngine.Setting.Debug.ColliderContactColor;

                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// 対象のオブジェクトの当たり判定を取る
        /// </summary>
        /// <param name="frametime">処理基準時間</param>
        /// <param name="coi">今回の比較対象</param>
        /// <param name="lastflag">最後の場合、相手のフラグを落としたい true=対象のフラグもoffにする</param>
        private void ProcCollisionElement(long frametime, ICollider coi, bool lastflag)
        {
            ColliderInfo cinfo = coi.ColInfo;
            ColliderTempInfo temp = cinfo.TempInfo;

            foreach (int ptype in this.ColliderDic.Keys)
            {
                //対象の判定か？
                if ((temp.TargetColType & ptype) != ptype)
                {
                    continue;
                }

                //ここまで来たら判定対象であるため、一覧を取得
                List<ICollider> targetlist = this.ColliderDic[ptype];
                targetlist.ForEach((target) =>
                {

                    //coiとtargetの判定
                    bool cret = this.Collider(coi, target);
                    if (cret == true)
                    {
                        //必要ならここで無敵リストへADDせよ

                        //判定コールバック
                        coi.ColliderCallback(target);
                        target.ColliderCallback(coi);
                    }


                    //最後の場合、当たった先の当たり判定を下し、二重判定を阻止する。
                    if (lastflag == true)
                    {
                        target.ColInfo.TempInfo.DownTargetColTypeFlag(cinfo.ColType);
                    }
                });


                //自分のフラグを下げる。
                cinfo.TempInfo.DownTargetColTypeFlag(ptype);
            }
        }

        /// <summary>
        /// 当たり判定本体
        /// </summary>
        /// <param name="frametime">処理基準時間</param>
        private void ProcCollision(long frametime)
        {
            foreach (List<ICollider> coilist in this.ColliderDic.Values)
            {
                coilist.ForEach(coi =>
                {
                    //判定を取る必要がない
                    if (coi.ColInfo.TempInfo.TargetColType == 0)
                    {
                        return;
                    }

                    //個別判定
                    bool f = coilist.Last() == coi;
                    this.ProcCollisionElement(frametime, coi, f);
                });

                

            }
        }


        /* ************************************************************************************ */
        /* ************************************************************************************ */
        /// <summary>
        /// 衝突判定の実行
        /// </summary>
        /// <param name="fparam"></param>
        public void ExecuteCollision(FrameProcParam fparam)
        {   

            //当たり判定情報の変形
            this.UpdateCollisonInfomation(fparam.FrameTime);

            //判定処理
            this.ProcCollision(fparam.FrameTime);
        
        }



        /// <summary>
        /// 当たり領域の描画(デバッグ用)
        /// </summary>
        /// <param name="vindex">描画ViewIndex</param>
        public void RenderCollider(int vindex)
        {
            int rindex = 0;
            //登録判定をすべて描画する
            foreach (List<ICollider> collist in this.ColliderDic.Values)
            {
                collist.ForEach(co =>
                {
                    co.ColInfo.TempInfo.TempColliderList.ForEach(ele =>
                    {
                        FrameRenderParam frp = new FrameRenderParam() { ViewIndex = vindex, RenderIndex = rindex };
                        ele.Render(frp);
                    });
                });
            }
        }



        #region 登録削除関数       

        /// <summary>
        /// 当たり判定対象へ登録
        /// </summary>
        /// <param name="ic"></param>
        public void AddCollider(ICollider ic)
        {
            ColliderInfo info = ic.ColInfo;
            //未登録判定は登録しない
            if (info == null)
            {
                return;

            }
            //判定対象としての定義がない時、登録しない。
            if (info.ColType == 0)
            {
                return;
            }

            //無い場合は新たに作成する・・・あらかじめ分かっている場合は事前追加の方が良いが・・・
            if (this.ColliderDic.ContainsKey(info.ColType) == false)
            {
                this.ColliderDic.Add(info.ColType, new List<ICollider>());
            }

            this.ColliderDic[info.ColType].Add(ic);
        }

        /// <summary>
        /// 当たり判定対象から削除
        /// </summary>
        /// <param name="ic"></param>
        public void RemoveCollider(ICollider ic)
        {
            ColliderInfo info = ic.ColInfo;
            //未登録判定
            if (info == null)
            {
                return;

            }
            //判定対象無し
            if (info.ColType == 0)
            {
                return;
            }

            //削除
            this.ColliderDic[info.ColType].Remove(ic);
        }


        /// <summary>
        /// 当たり判定の全クリア
        /// </summary>
        public void ClearCollider()
        {
            this.ColliderDic.Clear();

        }
        #endregion


    }
}
