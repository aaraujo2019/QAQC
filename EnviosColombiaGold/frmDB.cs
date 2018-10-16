using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Text;
using System.Windows.Forms;
using RN;
using RN.Entity;
using System.Linq;

namespace EnviosColombiaGold
{
    public partial class frmDB : Form
    {
        Configuration conf = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

        public frmDB()
        {
            InitializeComponent();
            LoadCmb();
        }
        private void LoadCmb()
        {
            try
            {
                List<DataConection> nameConection = Conexion.getConection();
                var query = nameConection.Select(c => c.Description).ToList();
                cmbDB.Items.AddRange(query.ToArray());
                cmbDB.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

  
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Selected Connection: " + cmbDB.Text.ToString(), "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                Application.Restart();

            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        private void frmDB_Load(object sender, EventArgs e)
        {
            string sString =
                conf.ConnectionStrings.ConnectionStrings["SqlProvider"].ConnectionString.ToString();
            string[] words = sString.Split(';');
            lblActually.Text = "Selected: " + words[1].ToString().Substring(16);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string val = string.Empty;
        }
    }
}
