using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarityMovement.Editor
{
    public partial class TagEditImageControl : UserControl, ITagEditControl
    {
        public TagEditImageControl()
        {
            InitializeComponent();
        }

        public event ITagEditControlEventDelegate? TagEditEvent = null;

        public BaseEditTag CreateTag(int sframe, int eframe, Vector3 rpos)
        {
            return new EditTagCollison();
        }
    }
}
