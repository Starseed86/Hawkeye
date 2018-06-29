using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace DCS_Hawkeye
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        #region " --- Global Variables --- "
        private FormMap formMap;
        Stopwatch swUpTime = new Stopwatch();
        Timer upTimeTimer = new Timer();
        #endregion

        private void FormMain_Load(object sender, EventArgs e)
        {
            int formWidth = this.Width;
            mainPictBoxR.Width = formWidth;
            pnlMain.Width = this.Width;
            pnlMain.Height = this.Height - 40;
            IsMdiContainer = true;
            swUpTime.Start();
            InitializeTimer();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            int formWidth = this.Width;
            mainPictBoxR.Width = formWidth;
            pnlMain.Width = this.Width;
            pnlMain.Height = this.Height - 40;
        }

        private void btnPPI_Click(object sender, EventArgs e)
        {
            if (formMap == null)
            {
                formMap = new FormMap
                {
                    TopLevel = false,
                    MdiParent = this
                };
                formMap.FormClosed += formMap_FormClosed;
                int rangeScale = 108;
                string txtFormMapTitle = "Primary PPI  Range Scale: " + rangeScale + "nm";
                formMap.Text = txtFormMapTitle;
                this.pnlMain.Controls.Add(formMap);
                formMap.Show();
            }
            else
            {
                formMap.Activate();
            }
        }

        private void formMap_FormClosed(object sender, EventArgs e)
        {
            formMap = null;
        }

        private void InitializeTimer()
        {
            upTimeTimer.Interval = 500;
            upTimeTimer.Tick += new EventHandler(upTimeTimer_Tick);
            upTimeTimer.Enabled = true;
        }

        //timer update loop
        private void upTimeTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = swUpTime.Elapsed;
            lblUpTime.Text = String.Format("Up  {0:D2}:{1:D2}:{2:D2}:{3:D3}", ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds);
        }
    }
}
