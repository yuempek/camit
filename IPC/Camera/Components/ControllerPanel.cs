using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IPC.Camera.Components
{
    public partial class ControllerPanel : Panel
    {
        public ControllerPanel()
        {
            InitializeComponent();
        }

        public ControllerPanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        { }

        public void Recreatehandle()
        {
            base.RecreateHandle();
        }

        const int WS_EX_TRANSPARENT = 0x00000020;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams p = base.CreateParams;
                p.ExStyle |= WS_EX_TRANSPARENT;
                return p;
            }
        }
    }
}
