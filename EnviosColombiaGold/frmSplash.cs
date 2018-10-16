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
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (clsRf.valueIntercpt)
            {
                FrmPpal oPpal = new FrmPpal(clsRf.sUser);
                oPpal.Text = "Intercept_Interval";
                oPpal.Show();
                this.Hide();
                timer1.Enabled = false;
            }
            else
            {
                if (clsRf.valueTopografish)
                {
                    FrmPpal oPpal = new FrmPpal(clsRf.sUser);
                    oPpal.Text = "Topography";
                    oPpal.Show();
                    this.Hide();
                    timer1.Enabled = false;
                }
                else
                {


                    FrmPpal oPpal = new FrmPpal();
                    oPpal.Show();
                    this.Hide();
                    timer1.Enabled = false;
                }
            }
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;

        }
    }
}
