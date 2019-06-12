using Intercept_Intervals.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EnviosColombiaGold
{
    public partial class frmSplash : Form
    {

        public frmSplash()
        {
            InitializeComponent();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            frm_Intercept_Intervals oPpal = new frm_Intercept_Intervals();
            oPpal.Show();
            this.Hide();
            timer1.Enabled = false;
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;

        }
    }
}
