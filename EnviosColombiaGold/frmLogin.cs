using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Configuration;
using System.Diagnostics;
using RN.Entity;
using RN;
using EnviosColombiaGold.CommonUI;

namespace EnviosColombiaGold
{
    public partial class frmLogin : Form
    {

        Configuration conf = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
        clsRf oRf = new clsRf();
        bool bAct = false;

        public frmLogin()
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
                if (DirectorioActivo.Autenticar(GlobalValue.Dominio, txtUser.Text.ToString(), txtPwd.Text.ToString(), GlobalValue.path))
                {
                    #region Logica authorization
                    //string sPwd = GetSHA1(txtPwd.Text.ToString());
                    clsRf.cConnection = cmbDB.SelectedItem.ToString();
                    if (!String.IsNullOrEmpty(clsRf.cConnection))
                    {
                        var value = oRf.getUsersGeneric(txtUser.Text.ToString());
                        if (value.ToList().Count > 0)
                        {
                            if (value.Select(c => c.activo_User).Count()==0)
                            {
                                MessageBox.Show("Disabled User", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (value.Where(r => r.id_Grupo.Equals(3) && r.activo_User).Count() > 0)
                            {
                                if (value.Select(c => c.login_User.ToString().ToUpper()).FirstOrDefault().ToString().ToUpper() == txtUser.Text.ToString().ToUpper())
                                {
                                    clsRf.sUser = txtUser.Text.ToString();
                                    clsRf.sIdentification = value.Select(c => c.id_User).FirstOrDefault().ToString();
                                    clsRf.sIdGrupo = value.Select(c => c.idGrupo_User).FirstOrDefault().ToString();

                                    var intercept = value.Where(s => s.id_Grupo.Equals(22) && s.activo_User);

                                    if (intercept.Count() > 0)
                                        clsRf.valueIntercpt = true;

                                    frmSplash oSplash = new frmSplash();
                                    oSplash.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("user without privileges", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                if (value.Where(r => r.id_Grupo.Equals(22) && r.activo_User).Count() > 0)
                                {
                                    if (value.Where(r => r.id_Grupo.Equals(22)).Select(c => c.login_User.ToString().ToUpper()).FirstOrDefault().ToString().ToUpper() == txtUser.Text.ToString().ToUpper())
                                    {
                                        clsRf.valueIntercpt = true;
                                        clsRf.sUser = txtUser.Text.ToString();
                                        frmSplash oSplash = new frmSplash();
                                        oSplash.Show();
                                        this.Hide();
                                    }
                                    else
                                    {
                                        MessageBox.Show("user without privileges", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    if (value.Where(r => r.id_Grupo.Equals(7) && r.activo_User).Count() > 0)
                                    {
                                        if (value.Where(r => r.id_Grupo.Equals(7)).Select(c => c.login_User.ToString().ToUpper()).FirstOrDefault().ToString().ToUpper() == txtUser.Text.ToString().ToUpper())
                                        {
                                            clsRf.valueTopografish = true;
                                            clsRf.sUser = txtUser.Text.ToString();
                                            frmSplash oSplash = new frmSplash();
                                            oSplash.Show();
                                            this.Hide();
                                        }
                                    }

                                    else
                                        MessageBox.Show("user without privileges in group", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    
                    }
                    else
                    {
                        MessageBox.Show("Credentials failed", "connection required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("User not active in Active Directory", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public static string GetSHA1(String texto)
        {
            try
            {
                SHA1 sha1 = SHA1CryptoServiceProvider.Create();
                Byte[] textOriginal = ASCIIEncoding.Default.GetBytes(texto);
                Byte[] hash = sha1.ComputeHash(textOriginal);
                StringBuilder cadena = new StringBuilder();
                foreach (byte i in hash)
                {
                    cadena.AppendFormat("{0:x2}", i);
                }
                return cadena.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {

                //ConfigurationSettings.AppSettings["IDProject"].ToString()
                oRf.iIdProject = int.Parse(ConfigurationSettings.AppSettings["IDProject"].ToString());
                //var dtVers = oRf.getVersionProject();
                DataTable dtVers = oRf.getVersionProject1();

                //if (double.Parse(dtVers.Select(c => c.version).FirstOrDefault().ToString()) >
                //    double.Parse(ConfigurationSettings.AppSettings["Version"].ToString()))

                if (double.Parse(dtVers.Rows[0]["version"].ToString()) >
                   double.Parse(ConfigurationSettings.AppSettings["Version"].ToString()))
                {

                    bAct = true;

                    MessageBox.Show("Version Update");

                    this.Close();

                    Process[] _proceses = null;
                    _proceses = Process.GetProcessesByName("EnviosColombiaGold.exe");
                    foreach (Process proces in _proceses)
                    {
                        proces.Kill();
                    }


                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (bAct == true)
            {
                Process.Start("Actualizar.bat");
            }
        }

        private void pbtnChange_Click(object sender, EventArgs e)
        {
            try
            {
                conf.ConnectionStrings.ConnectionStrings["SqlProvider"].ConnectionString =
                    cmbDB.SelectedValue.ToString(); //ConfigurationSettings.AppSettings["Zancudo"].ToString();
                conf.Save();

                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
