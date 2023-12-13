using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarityOrbit.LayerView
{
    /// <summary>
    /// レイヤー編集画面
    /// </summary>
    public partial class LayerEditForm : Form
    {
        public LayerEditForm(OrbitLayer lData)
        {
            InitializeComponent();
            this.LData = lData;
        }

        /// <summary>
        /// レイヤーデータ
        /// </summary>
        private OrbitLayer LData;

        private void LayerEditForm_Load(object sender, EventArgs e)
        {

        }
    }
}
