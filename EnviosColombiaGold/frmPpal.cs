using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using Intercept_Intervals.UI;
using EnviosColombiaGold.Rol;
using EnviosColombiaGold.CommonUI;
using RN.RulesBussines;
using Entities.Person;
using EnviosColombiaGold.Topography;

namespace EnviosColombiaGold
{
    /// <summary>
    /// /Formulario encargado de orquestar los llamados de las opciones 
    /// </summary>
    public partial class FrmPpal : Form
    {
        #region variables
        clsRf oRf = new clsRf();
        public List<Rol_Permission> Permission;
        DataTable dtFormsAllowed = new DataTable();
        #endregion 

        #region Constructor
        public FrmPpal()
        {
            InitializeComponent();
            helloToolStripMenuItem.Text = string.Concat("Welcome: ", clsRf.sUser);
        }
        public FrmPpal(string User)
        {
            InitializeComponent();
            GlobalValue.Permissions = LoadLog.GetPermissiinRol("SPGet_RolesForUser", User, this.Name);
            this.Permission = GlobalValue.Permissions;
            ValidatePermission(this.Controls);
            helloToolStripMenuItem.Text = string.Concat("Welcome: ", clsRf.sUser);
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Metodo que se encarga de validar el control actual para saber si tiene un estado en particular (Visible -- enable)
        /// </summary>
        /// <param name="controlCollection">Nombre del control</param>
        private void ValidatePermission(Control.ControlCollection controlCollection)
        {
            foreach (Control c in controlCollection)
            {
                if (c.Controls.Count > 0)
                {
                    ValidatePermission(c.Controls);
                }
                if (c is MenuStrip)
                {
                    MenuStrip menuStrip = c as MenuStrip;
                    ShowToolStipItems(menuStrip.Items);
                }

                if (c is Button || c is ComboBox || c is TextBox ||
                    c is ListBox || c is DataGridView || c is RadioButton ||
                    c is RichTextBox || c is TabPage || c is TextBox || c is GroupBox)
                {

                    Rol_Permission valueFilter = Permission.Where(e => e.fkcontrolid == c.Name).FirstOrDefault();

                    if (valueFilter != null)
                    {
                        if (valueFilter.Invisible > 0)
                            c.Visible = false;
                        else
                            c.Visible = true;

                        if (valueFilter.Disabled > 0)
                            c.Enabled = false;
                        else
                            c.Enabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// Meotod encargado de verificar si el control es un item de un menù para verificar su estado
        /// </summary>
        /// <param name="toolStripItems">Nombre del item del menu</param>
        private void ShowToolStipItems(ToolStripItemCollection toolStripItems)
        {
            foreach (ToolStripMenuItem mi in toolStripItems)
            {
                mi.ToolTipText = mi.Name;

                if (mi.DropDownItems.Count > 0)
                {
                    ShowToolStipItems(mi.DropDownItems);
                }

                Rol_Permission valueFilter = Permission.Where(e => e.fkcontrolid == mi.Name).FirstOrDefault();

                if (valueFilter != null)
                {
                    if (valueFilter.Invisible > 0)
                        mi.Visible = false;
                    else
                        mi.Visible = true;

                    if (valueFilter.Disabled > 0)
                        mi.Enabled = false;
                    else
                        mi.Enabled = true;
                }
            }
        }
        #endregion 

        #region Eventos
        private void FrmPpal_Load(object sender, EventArgs e)
        {
            try
            {
                if (clsRf.sIdGrupo != null)
                {
                    if (clsRf.valueIntercpt)
                        graficasToolStripMenuItem.Visible = true;
                    else
                    {
                        if (clsRf.valueTopografish)
                            topographyToolStripMenuItem.Visible = true;
                        else
                        {
                            seguridadToolStripMenuItem.Visible = false;
                            graficasToolStripMenuItem.Visible = false;
                            topographyToolStripMenuItem.Visible = false;
                        }
                    }
                    dtFormsAllowed = oRf.getFormsByGrupo(clsRf.sIdGrupo, ConfigurationSettings.AppSettings["IDProject"].ToString());
                    clsRf.dsPermisos = oRf.getFormsByGrupoAll(clsRf.sIdGrupo);

                    MdiClient ctlMDI = default(MdiClient);
                    foreach (Control ctl in this.Controls)
                    {
                        try
                        {
                            ctlMDI = (MdiClient)ctl;
                            ctlMDI.BackColor = Color.White;
                        }
                        catch (InvalidCastException ex)
                        {
                            //throw new Exception(ex.Message);
                        }
                    }
                }
                else
                {
                    if (clsRf.valueIntercpt)
                    {
                        envioToolStripMenuItem.Visible = false;
                        graficasToolStripMenuItem.Visible = true;
                        topographyToolStripMenuItem.Visible = false;
                    }
                    else
                    {
                        if (clsRf.valueTopografish)
                        {
                            envioToolStripMenuItem.Visible = false;
                            graficasToolStripMenuItem.Visible = false;
                            topographyToolStripMenuItem.Visible = true;
                        }
                        else
                        {
                            graficasToolStripMenuItem.Visible = false;
                            topographyToolStripMenuItem.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void prepararEnvioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                var query = from dtnew in dtFormsAllowed.AsEnumerable()
                            where dtnew.Field<string>("nombre_Real_Form") == "FRMENVIOS"
                            select dtnew;

                if (query.Count() > 0)
                {
                    frmEnvios oEnvios = new frmEnvios();
                    //oEnvios.MdiParent = this;                 
                    //oEnvios.Close();
                    //oEnvios.Dispose();

                    oEnvios = new frmEnvios();
                    oEnvios.MdiParent = this;
                    oEnvios.Show();

                }
                else
                {
                    MessageBox.Show("Form is not allowed", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
            //this.Close();
        }


        private void selectDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDB oDB = new frmDB();
            oDB.MdiParent = this;
            oDB.Show();
        }

        private void FrmPpal_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsRf.dsPermisos = new DataSet();
            Application.Exit();
        }


        private void mnucontrolSampling_Click(object sender, EventArgs e)
        {
            frmControlSampling oCtrlA = new frmControlSampling();
            oCtrlA.MdiParent = this;
            oCtrlA.Show();
        }

        private void sGSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCargarResEnvio oCargEnv = new frmCargarResEnvio();
            oCargEnv.MdiParent = this;
            oCargEnv.Show();
        }

        private void aCTLABSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCargarLaboratorio oCargLab = new frmCargarLaboratorio();
            oCargLab.MdiParent = this;
            oCargLab.Show();
        }

        private void selectDBToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDB oDB = new frmDB();
            oDB.MdiParent = this;
            oDB.Show();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void interceptIntervalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Intercept_Intervals Intervals = new frm_Intercept_Intervals(clsRf.sUser);
            Intervals.MdiParent = this;
            Intervals.Show();
        }

        private void administradorDeRolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageRoles dlg = new ManageRoles();
            dlg.MdiParent = this;
            dlg.Show();
        }

        private void administradorPermisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManagePermissions dlg =
               new ManagePermissions();
            dlg.MdiParent = this;
            dlg.Show();
        }
        #endregion

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdminTopography frmTopog = new frmAdminTopography();
            frmTopog.MdiParent = this;
            frmTopog.Show();
            
        }

        private void laborToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void movementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMoventLabor frmMovent = new frmMoventLabor();
            frmMovent.MdiParent = this;
            frmMovent.Show();
        }

        private void loadCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmLoadArchive frmMovent = new frmLoadArchive();
            frmMovent.MdiParent = this;
            frmMovent.Show();
        }
    }
}
