using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.Image.PNG;
using Clarity.GUI;

namespace ClarityMovement
{
    internal class MainFormLogic
    {

        /// <summary>
        /// プロジェクトの作成
        /// </summary>
        /// <param name="apngfilepath">apngのパス</param>
        /// <returns></returns>
        public async Task CreateNewProject(string apngfilepath)
        {
            //apngの読込
            APngFile ap = new APngFile();
            await ap.Load(apngfilepath);

            //プロジェクトの作成
            var bilist = ap.FrameList.Select(x => x.CreateBitmapImage()).ToList();
            MvGlobal.Mana.CraeteProject(bilist);

            //通知
            MvGlobal.SendEventUI(EMovementUIEvent.NewProject);
        }



    }
}
