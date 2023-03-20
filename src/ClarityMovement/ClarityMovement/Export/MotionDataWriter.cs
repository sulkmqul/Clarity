using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityMovement.Export
{

    internal class MotionDataWriter
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="project"></param>
        public MotionDataWriter(CmProject project)
        {
            this.Project = project;
        }

        /// <summary>
        /// 元ネタ
        /// </summary>
        private CmProject Project { get; set; }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 出力処理
        /// </summary>
        /// <param name="filepath">出力ファイルパス</param>
        /// <param name="mcode">モーションコード</param>
        public void Export(string filepath, string mcode)
        {
            //使用画像の取得と画像連結
            


        }



    }
}
