using Clarity.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarityMovement.Viwer
{
    public partial class MovementEditViewerControl : BaseCeControl
    {
        public MovementEditViewerControl() : base()
        {
            InitializeComponent();
        }


        protected override void InitCE()
        {
            //背景色の設定            
            ClarityEngine.EngineSetting.SetEngineParam(EClarityEngineSettingKeys.ViewDisplay_ClearColor, new System.Numerics.Vector4(0.4f, 0.4f, 0.4f, 1.0f));
        }


        protected override WorldData CreateWorld()
        {
            return base.CreateWorld();
        }

        private void MovementEditViewerControl_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void MovementEditViewerControl_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void MovementEditViewerControl_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
