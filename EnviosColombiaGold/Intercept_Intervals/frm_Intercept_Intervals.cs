using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Globalization;
using RN.RulesBussines;
using Entities.Person;
using EnviosColombiaGold.CommonUI;

namespace Intercept_Intervals.UI
{/// <summary>
 /// Formulario que permite la carga de pozos e identificar sus intervalos 
 /// </summary>
 /// 


    public partial class frm_Intercept_Intervals : Form
    {
        #region constructor
        public frm_Intercept_Intervals()
        {
            InitializeComponent(); FillCmb();
        }
        public frm_Intercept_Intervals(string User)
        {
            InitializeComponent(); FillCmb();

            this.Usuario = User.Trim();
            GlobalValue.Permissions = LoadLog.GetPermissiinRol("SPGet_RolesForUser", this.Usuario, this.Name);

            this.Permission = GlobalValue.Permissions;
            ValidatePermission(this.Controls);
        }




        #endregion constructor

        #region Propiedades - Variables
        DataTable generalData = new DataTable();
        public bool valueModificate = false;
        public bool valueFirstRecuperation = false;
        public bool containeRegister = false;

        public List<Rol_Permission> Permission;

        public string Usuario { get; set; }
        public string IpLocal { get; set; }
        public string IpPublica { get; set; }
        public string SerialHDD { get; set; }
        #endregion

        #region entidades de calculo
        public class matriz1
        {
            public decimal value { get; set; }
        }
        public class matriz2
        {
            public decimal value { get; set; }
        }
        public class matriz3
        {
            public decimal value { get; set; }
        }

        public class matrizResult
        {
            public decimal value { get; set; }
        }

        public class matrizTo
        {
            public string value { get; set; }
        }

        public class matrizMod
        {
            public string value { get; set; }
        }
        #endregion 

        #region Cargar datos combo de pozos
        /// <summary>
        /// Carga el objeto combo
        /// </summary>
        private void FillCmb()
        {
            try
            {
                if (string.IsNullOrEmpty(this.IpLocal))
                    this.IpLocal = Adress_IP.Local();

                if (string.IsNullOrEmpty(this.IpPublica))
                    this.IpPublica = Adress_IP.Publica();

                if (string.IsNullOrEmpty(this.SerialHDD))
                    this.SerialHDD = Adress_IP.SerialNumberDisk();

                if (string.IsNullOrEmpty(this.Usuario))
                    this.Usuario = Adress_IP.SerialNumberDisk();
                var culturaCol = CultureInfo.GetCultureInfo("es-CO");
                DateTime dateReporte = Convert.ToDateTime(DateTime.Now, culturaCol);


                LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, "Search HoleID", "Consulta");
                this.cmbHoleID.DropDownStyle = ComboBoxStyle.DropDownList;
                this.cboSoruce.DropDownStyle = ComboBoxStyle.DropDownList;
                DataTable dtCollars = RN.GetDHCollars.getDHCollars("");
                DataRow drC = dtCollars.NewRow();
                drC[0] = "Select an option..";
                dtCollars.Rows.Add(drC);
                cmbHoleID.DisplayMember = "HoleID";
                cmbHoleID.ValueMember = "HoleID";
                cmbHoleID.DataSource = dtCollars;
                cmbHoleID.SelectedValue = "Select an option..";
                generalData = dtCollars;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Metodos

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


        /// <summary>
        /// Limpia todos los objetos involucrados
        /// </summary>
        private void Clear()
        {
            cmbHoleID.DisplayMember = "HoleID";
            cmbHoleID.ValueMember = "HoleID";
            cmbHoleID.DataSource = generalData;
            cmbHoleID.SelectedValue = "Select an option..";
            dtgValueCalculate.Rows.Clear();
            txtSearchHoleId.Text = string.Empty;
            cboSoruce.SelectedIndex = 0;
            txtComent.Text = string.Empty;
            btnExport.Enabled = false;
        }
        /// <summary>
        /// Exporta objeto dataGrid a excel
        /// </summary>
        /// <param name="HoldId">Código del pozo</param>
        private void exportIntercept(string HoldId)
        {
            try
            {
                //QCReport
                Excel.Application oXL;
                Excel._Workbook oWB;
                Excel._Worksheet oSheet;

                oXL = new Excel.Application();
                string pathArchive = string.Concat(Application.StartupPath, "\\GCG_Intercept_Intervals.xlsx");
                oWB = oXL.Workbooks.Open(pathArchive, 0, true, 5,
                Type.Missing, Type.Missing, false, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, null, null);

                oSheet = (Excel._Worksheet)oWB.Sheets[1];

                if (cboSoruce.Text.Contains("GEX"))
                    oSheet.Cells[4, 7] = "Pozos Exploración";
                else
                    oSheet.Cells[4, 7] = "Pozos Geología Minas";

                if (dtgDetailHoleID.Rows.Count > 0)
                {
                    int iInicial = 9;
                    for (int i = 0; i < dtgDetailHoleID.Rows.Count; i++)
                    {
                        if (i != dtgDetailHoleID.Rows.Count - 2)
                        {
                            if (dtgDetailHoleID.Rows[i].Cells[0].Value != null)
                                if (Convert.ToBoolean(dtgDetailHoleID.Rows[i].Cells[0].Value))
                                {
                                    oSheet.Cells[iInicial, 1] = "*".ToString();//Hole
                                    var columnHeadingsRange1 = oSheet.Range[
                                       oSheet.Cells[iInicial, 1],
                                       oSheet.Cells[iInicial, 9]];

                                    columnHeadingsRange1.Interior.Color = Color.LightBlue;

                                    var columnHeadingsRange2 = oSheet.Range[
                                      oSheet.Cells[iInicial, 11],
                                      oSheet.Cells[iInicial, 11]];

                                    columnHeadingsRange2.Interior.Color = Color.LightBlue;

                                }
                            oSheet.Cells[iInicial, 2] = dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["jobno"].Index].Value.ToString();
                            oSheet.Cells[iInicial, 3] = dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["dhid"].Index].Value.ToString();
                            oSheet.Cells[iInicial, 4] = dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["from"].Index].Value.ToString();
                            oSheet.Cells[iInicial, 5] = dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["to"].Index].Value.ToString();
                            oSheet.Cells[iInicial, 6] = dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["length"].Index].Value.ToString();
                            oSheet.Cells[iInicial, 7] = dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["sampno"].Index].Value.ToString();
                            oSheet.Cells[iInicial, 8] = dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["lithology"].Index].Value.ToString();
                            oSheet.Cells[iInicial, 9] = dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["Vn_mod"].Index].Value.ToString();

                            var columnHeadingsRange = oSheet.Range[
     oSheet.Cells[iInicial, 10],
     oSheet.Cells[iInicial, 10]];

                            oSheet.Cells[iInicial, 10] = dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value.ToString();



                            if (Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) > 0 && Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) < 1.999M)
                                dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9D9D9");

                            if (Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) >= 2M && Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) < 2.999M)
                                dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#00FFFF");

                            if (Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) >= 3M && Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) < 3.999M)
                                dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#00FF00");

                            if (Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) >= 4M && Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) <= 7.999M)
                                dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF00");


                            if (Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) >= 8M && Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) <= 11.999M)
                                dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC000");

                            if (Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) >= 12M && Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) <= 19.999M)
                                dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");


                            if (Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) >= 20M)
                                dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF00FF");


                            if (dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value != null && !String.IsNullOrEmpty(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value.ToString().Trim()))
                            {
                                if (Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) > 0 && Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) < 3.999M)
                                    dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9D9D9");

                                if (Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) >= 4M && Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) < 5.999M)
                                    dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#00FFFF");

                                if (Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) >= 6M && Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) < 7.999M)
                                    dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#00FF00");

                                if (Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) >= 8M && Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) <= 15.999M)
                                    dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF00");


                                if (Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) >= 16M && Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) <= 23.999M)
                                    dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC000");

                                if (Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) >= 24M && Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) <= 39.999M)
                                    dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");


                                if (Convert.ToDecimal(dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) >= 40M)
                                    dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF00FF");
                            }

                            if (dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["lithology"].Index].Value.ToString().Trim().Contains("VBX") || dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["lithology"].Index].Value.ToString().Trim().Contains("VEN") || dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["lithology"].Index].Value.ToString().Trim().Contains("VNA"))
                                dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["lithology"].Index].Style.BackColor = Color.Red;

                            if (dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["lithology"].Index].Value.ToString().Trim().Contains("FLT") || dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["lithology"].Index].Value.ToString().Trim().Contains("FLG") ||
                                dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["lithology"].Index].Value.ToString().Trim().Contains("BHF") || dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["lithology"].Index].Value.ToString().Trim().Contains("BXF"))
                                dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["lithology"].Index].Style.BackColor = Color.Blue;

                            if (dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["lithology"].Index].Value != null && (dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["lithology"].Index].Value.ToString().Trim().Contains("HA")))
                        dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["lithology"].Index].Style.BackColor = Color.Green;



                            oSheet.Cells[iInicial, 11] = dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value.ToString();
                            oSheet.Cells[iInicial, 12] = dtgValueCalculate.Rows[i].Cells[2].Value.ToString();
                            oSheet.Cells[iInicial, 13] = dtgValueCalculate.Rows[i].Cells[3].Value.ToString();
                            oSheet.Cells[iInicial, 14] = dtgValueCalculate.Rows[i].Cells[4].Value.ToString();
                            oSheet.Cells[iInicial, 15] = dtgValueCalculate.Rows[i].Cells[5].Value.ToString();
                            oSheet.Cells[iInicial, 16] = dtgValueCalculate.Rows[i].Cells[6].Value.ToString();
                            iInicial++;
                        }
                        else
                            break;
                    }
                }
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                if (ex.Message == "No se puede encontrar la tabla 0.")
                {
                    MessageBox.Show("No hay datos que mostrar");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion

        #region Eventos
        private void button6_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void cmbHoleID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cmbHoleID.Text))
            {
                DataTable dtCollars = RN.GetDHCollars.getDHHoleID(cmbHoleID.Text);

                dtgDetailHoleID.DataSource = dtCollars;
                dtgDetailHoleID.AutoResizeColumns();
                dtgDetailHoleID.Columns[dtgDetailHoleID.Columns["SKDHSamples"].Index].Visible = false;
                containeRegister = false;

                var culturaCol = CultureInfo.GetCultureInfo("es-CO");
                DateTime dateReporte = Convert.ToDateTime(DateTime.Now, culturaCol);

                LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, "Return Hole Id for Code: " + cmbHoleID.Text, "View information");


                foreach (DataGridViewRow row in dtgDetailHoleID.Rows)
                {
                    if (Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) > 0 && Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) < 1.999M)
                        row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9D9D9");

                    if (Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) >= 2M && Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) < 2.999M)
                        row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#00FFFF");

                    if (Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) >= 3M && Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) < 3.999M)
                        row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#00FF00");

                    if (Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) >= 4M && Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) <= 7.999M)
                        row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF00");


                    if (Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) >= 8M && Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) <= 11.999M)
                        row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC000");

                    if (Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) >= 12M && Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) <= 19.999M)
                        row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");


                    if (Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value) >= 20M)
                        row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF00FF");

                    if (row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value != null && !String.IsNullOrEmpty(row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value.ToString().Trim()))
                    {
                        if (Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) > 0 && Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) < 3.999M)
                            row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9D9D9");

                        if (Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) >= 4M && Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) < 5.999M)
                            row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#00FFFF");

                        if (Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) >= 6M && Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) < 7.999M)
                            row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#00FF00");

                        if (Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) >= 8M && Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) <= 15.999M)
                            row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF00");


                        if (Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) >= 16M && Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) <= 23.999M)
                            row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC000");

                        if (Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) >= 24M && Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) <= 39.999M)
                            row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");


                        if (Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value) >= 40M)
                            row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF00FF");
                    }

                    if (row.Cells[dtgDetailHoleID.Columns["lithology"].Index].Value!= null && (row.Cells[dtgDetailHoleID.Columns["lithology"].Index].Value.ToString().Trim().Contains("VBX") || row.Cells[dtgDetailHoleID.Columns["lithology"].Index].Value.ToString().Trim().Contains("VEN") || row.Cells[dtgDetailHoleID.Columns["lithology"].Index].Value.ToString().Trim().Contains("VNA")))
                        row.Cells[dtgDetailHoleID.Columns["lithology"].Index].Style.BackColor = Color.Red;

                    if (row.Cells[dtgDetailHoleID.Columns["lithology"].Index].Value != null && (row.Cells[dtgDetailHoleID.Columns["lithology"].Index].Value.ToString().Trim().Contains("FLT") || row.Cells[dtgDetailHoleID.Columns["lithology"].Index].Value.ToString().Trim().Contains("FLG") ||
                        row.Cells[dtgDetailHoleID.Columns["lithology"].Index].Value.ToString().Trim().Contains("BHF") || row.Cells[dtgDetailHoleID.Columns["lithology"].Index].Value.ToString().Trim().Contains("BXF")))
                        row.Cells[dtgDetailHoleID.Columns["lithology"].Index].Style.BackColor = Color.Blue;


                    if (row.Cells[dtgDetailHoleID.Columns["lithology"].Index].Value != null && (row.Cells[dtgDetailHoleID.Columns["lithology"].Index].Value.ToString().Trim().Contains("HA")))
                        row.Cells[dtgDetailHoleID.Columns["lithology"].Index].Style.BackColor = Color.Green;



                    if (row.Cells[dtgDetailHoleID.Columns["vn_mod"].Index].Value != null && !String.IsNullOrEmpty(row.Cells[dtgDetailHoleID.Columns["vn_mod"].Index].Value.ToString()))
                    {
                        row.Cells[0].Value = true;
                        row.Cells[1].Value = row.Cells[dtgDetailHoleID.Columns["vn_mod"].Index].Value.ToString().Trim();
                        containeRegister = true;
                    }
                }

                if (!String.IsNullOrEmpty(txtSearchHoleId.Text))
                {
                    DataRow datarow;
                    datarow = dtCollars.NewRow(); //Con esto le indica que es una nueva fila.

                    datarow[0] = false;
                    datarow[1] = 0;
                    datarow[2] = 0;
                    datarow[3] = 0;
                    datarow[4] = 0;
                    datarow[5] = 0;
                    datarow[6] = 0;
                    datarow[7] = 0;
                    datarow[8] = 0;
                    datarow[9] = 0;
                    datarow[10] = 0;
                    datarow[11] = 0;
                    datarow[12] = 0;
                    datarow[13] = 0;
                    datarow[14] = 0;
                    dtCollars.Rows.Add(datarow);


                    if (dtgDetailHoleID.Rows.Count > 1)
                    {
                        dtgDetailHoleID.Rows[dtgDetailHoleID.RowCount - 1].Cells[0].Value = false;

                        if (containeRegister)
                        {
                            containeRegister = false;
                            dtgDetailHoleID.Columns[0].Visible = false;
                            checkBox1.Checked = false;
                            label6.Text = "Inactive";
                        }
                        DataGridViewCellEventArgs s = new DataGridViewCellEventArgs(0, dtgDetailHoleID.RowCount - 1);
                        dataGridView2_CellContentClick(sender, s);
                        valueFirstRecuperation = true;
                    }
                }
                else
                {
                    DataRow datarow;
                    datarow = dtCollars.NewRow(); //Con esto le indica que es una nueva fila.

                    datarow[0] = false;
                    datarow[1] = 0;
                    datarow[2] = 0;
                    datarow[3] = 0;
                    datarow[4] = 0;
                    datarow[5] = 0;
                    datarow[6] = 0;
                    datarow[7] = 0;
                    datarow[8] = 0;
                    datarow[9] = 0;
                    datarow[10] = 0;
                    datarow[11] = 0;
                    datarow[12] = 0;
                    datarow[13] = 0;
                    datarow[14] = 0;
                    dtCollars.Rows.Add(datarow);


                    if (dtgDetailHoleID.Rows.Count > 1)
                    {
                        dtgDetailHoleID.Rows[dtgDetailHoleID.RowCount - 1].Cells[0].Value = false;

                        dtgDetailHoleID.Columns[0].Visible = true;
                        checkBox1.Checked = true;
                        containeRegister = true;
                        label6.Text = "Active";
                    }
                    DataGridViewCellEventArgs s = new DataGridViewCellEventArgs(0, dtgDetailHoleID.RowCount - 1);
                    dataGridView2_CellContentClick(sender, s);
                    valueFirstRecuperation = true;
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgDetailHoleID.Rows.Count > 1)
                    if (e.ColumnIndex == 0)
                    {
                        int indexCurrent = -1;

                        int countRegisterCheck = -1;
                        decimal Length_Grade = 0;
                        decimal au_ppm = 0, ag_ppm = 0;
                        string Vn_mod = string.Empty;

                        string jobno = string.Empty, dhid = string.Empty, from = string.Empty, to = string.Empty;
                        dtgValueCalculate.Rows.Clear();
                        matriz1 matr1 = new matriz1();
                        List<matriz1> listmt1 = new List<matriz1>();

                        matriz2 matr2 = new matriz2();
                        List<matriz2> listmt2 = new List<matriz2>();

                        matriz3 matr3 = new matriz3();
                        List<matriz3> listmt3 = new List<matriz3>();

                        matrizTo matrTo = new matrizTo();
                        List<matrizTo> listmtTo = new List<matrizTo>();

                        matrizMod matrMod = new matrizMod();
                        List<matrizMod> listmtMod = new List<matrizMod>();

                        foreach (DataGridViewRow row in dtgDetailHoleID.Rows)
                        {
                            if (row.Index != dtgDetailHoleID.Rows.Count - 1)
                            {
                                indexCurrent++;

                                if (Convert.ToBoolean(dtgDetailHoleID.Rows[e.RowIndex].Cells[0].Value))
                                    valueModificate = false;
                                else
                                    valueModificate = true;

                                if (e.RowIndex == indexCurrent)
                                    row.Cells[0].Value = valueModificate;

                                if (Convert.ToBoolean(row.Cells[0].Value))
                                {
                                    if (row.Cells[1].Value == null || string.IsNullOrEmpty(row.Cells[1].Value.ToString()))
                                    {
                                        dtgDetailHoleID.EndEdit();
                                        MessageBox.Show("Value Vn_Mod is required");

                                        try
                                        {

                                            DataGridView dgv = (DataGridView)sender;

                                            dgv.Rows.OfType<DataGridViewRow>().ToList().
                                                   ForEach(x => x.Cells[e.ColumnIndex].Value = false);
                                            dtgValueCalculate.Rows.Clear();

                                        }
                                        catch
                                        {


                                        }
                                        //DataGridView dgv = (DataGridView)sender;
                                        //      dataGridView2.EndEdit();

                                        //      dgv.Rows[indexCurrent].Cells[0].Value = false;
                                        //      dgv.Rows[indexCurrent].Cells[1].Value = string.Empty;

                                        btnExport.Enabled = false;
                                        return;
                                    }

                                    for (int i = 0; i < dtgDetailHoleID.ColumnCount; i++)
                                    {
                                        if (i != 6 && i != 7 && i != 8)
                                            row.Cells[i].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#DCE6F1");
                                    }

                                    if (Length_Grade == 0)
                                    {
                                        Length_Grade = Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["length"].Index].Value);
                                        jobno = row.Cells[dtgDetailHoleID.Columns["jobno"].Index].Value.ToString();
                                        dhid = row.Cells[dtgDetailHoleID.Columns["dhid"].Index].Value.ToString();
                                        from = row.Cells[dtgDetailHoleID.Columns["from"].Index].Value.ToString();
                                        to = row.Cells[dtgDetailHoleID.Columns["to"].Index].Value.ToString();
                                        matrTo.value = to;
                                        listmtTo.Add(matrTo);

                                        if (row.Cells[1].Value != null)
                                        {
                                            Vn_mod = row.Cells[1].Value.ToString();
                                            matrMod.value = Vn_mod;
                                            listmtMod.Add(matrMod);
                                        }

                                        if (row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value != null)
                                            au_ppm = Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value);

                                        matr1.value = au_ppm;
                                        listmt1.Add(matr1);
                                        matr1 = new matriz1();

                                        if (row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value != null && !string.IsNullOrEmpty(row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value.ToString()))
                                            ag_ppm = Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value);

                                        matr2.value = ag_ppm;
                                        listmt2.Add(matr2);
                                        matr2 = new matriz2();

                                        matr3.value = Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["length"].Index].Value);
                                        listmt3.Add(matr3);
                                        matr3 = new matriz3();

                                        countRegisterCheck++;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            if (!listmtMod[listmtMod.Count - 1].value.Equals(row.Cells[1].Value.ToString()))
                                            {
                                                MessageBox.Show("The  final value 'vn_mod':" + listmtMod[listmtMod.Count - 1].value.ToString() + " does not match the actual initial value 'vn_mod':" + row.Cells[1].Value.ToString() + " in position number:" + (indexCurrent + 1).ToString());
                                                DataGridView dgv = (DataGridView)sender;
                                                dtgDetailHoleID.EndEdit();

                                                dgv.Rows[indexCurrent].Cells[0].Value = false;
                                                dgv.Rows[indexCurrent].Cells[1].Value = string.Empty;

                                                //dgv.Rows.OfType<DataGridViewRow>().ToList().
                                                //       ForEach(x => x.Cells[e.ColumnIndex].Value = true);
                                                //dataGridView4.Rows.Clear();
                                                btnExport.Enabled = false;
                                            }
                                            else
                                            if (listmtTo[listmtTo.Count - 1].value.Equals(row.Cells[dtgDetailHoleID.Columns["from"].Index].Value.ToString()))
                                            {
                                                to = row.Cells[dtgDetailHoleID.Columns["to"].Index].Value.ToString();
                                                Length_Grade = Length_Grade + Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["length"].Index].Value);

                                                matrTo.value = to;
                                                listmtTo.Add(matrTo);

                                                if (row.Cells[1].Value != null)
                                                    Vn_mod = row.Cells[1].Value.ToString();

                                                if (row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value != null)
                                                    au_ppm = Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value);

                                                matr1.value = au_ppm;
                                                listmt1.Add(matr1);
                                                matr1 = new matriz1();

                                                if (row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value != null && !string.IsNullOrEmpty(row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value.ToString()))
                                                    ag_ppm = Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value);

                                                matr2.value = ag_ppm;
                                                listmt2.Add(matr2);
                                                matr2 = new matriz2();

                                                matr3.value = Convert.ToDecimal(row.Cells[dtgDetailHoleID.Columns["length"].Index].Value);
                                                listmt3.Add(matr3);
                                                matr3 = new matriz3();

                                                countRegisterCheck++;
                                            }
                                            else
                                            {
                                                MessageBox.Show("The  final value 'TO':" + listmtTo[listmtTo.Count - 1].value.ToString() + " does not match the actual initial value 'from':" + row.Cells[dtgDetailHoleID.Columns["from"].Index].Value.ToString() + " in position number:" + (indexCurrent + 1).ToString());
                                                DataGridView dgv = (DataGridView)sender;
                                                dtgDetailHoleID.EndEdit();

                                                dgv.Rows[indexCurrent].Cells[0].Value = false;
                                                dgv.Rows[indexCurrent].Cells[1].Value = string.Empty;

                                                //dgv.Rows.OfType<DataGridViewRow>().ToList().
                                                //       ForEach(x => x.Cells[e.ColumnIndex].Value = true);
                                                //dataGridView4.Rows.Clear();
                                                btnExport.Enabled = false;
                                                //return;
                                            }
                                        }
                                        catch
                                        {

                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < dtgDetailHoleID.ColumnCount; i++)
                                    {
                                        if (i != 6 && i != 7 && i != 8)
                                            row.Cells[i].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#DCE6F1");
                                    }
                                    if (au_ppm > 0)
                                    {
                                        matrizResult mtrResult = new matrizResult();
                                        List<matrizResult> listmtrResult = new List<matrizResult>();

                                        for (int i = 0; i < listmt1.Count; i++)
                                        {
                                            mtrResult.value = listmt3[i].value * listmt1[i].value;
                                            listmtrResult.Add(mtrResult);
                                            mtrResult = new matrizResult();
                                        }

                                        au_ppm = 0;

                                        for (int i = 0; i < listmtrResult.Count; i++)
                                        {
                                            if (au_ppm == 0)
                                                au_ppm = listmtrResult[i].value;
                                            else
                                                au_ppm = au_ppm + listmtrResult[i].value;
                                        }
                                        mtrResult = new matrizResult();
                                        listmtrResult = new List<matrizResult>();

                                        for (int i = 0; i < listmt3.Count; i++)
                                        {
                                            mtrResult.value = listmt2[i].value * listmt3[i].value;
                                            listmtrResult.Add(mtrResult);
                                            mtrResult = new matrizResult();
                                        }

                                        ag_ppm = 0;

                                        for (int i = 0; i < listmtrResult.Count; i++)
                                        {
                                            if (ag_ppm == 0)
                                                ag_ppm = listmtrResult[i].value;
                                            else
                                                ag_ppm = ag_ppm + listmtrResult[i].value;
                                        }
                                        btnExport.Enabled = true;

                                        dtgValueCalculate.Rows.Insert(dtgValueCalculate.Rows.Count - 1, jobno, dhid, from, to, Length_Grade, (au_ppm / Length_Grade).ToString("##,0.000"), (ag_ppm / Length_Grade).ToString("##,0.000"), countRegisterCheck + 1, Vn_mod);
                                        dtgValueCalculate.AutoResizeColumns();
                                        int valueDiferencial = (dtgValueCalculate.Rows.Count) - indexCurrent;
                                        for (int i = 0; i < countRegisterCheck + 1; i++)
                                        {
                                            dtgValueCalculate.Rows.Insert(dtgValueCalculate.Rows.Count - 1, "", "", "", "", "", "", "", "");
                                        }
                                        countRegisterCheck = -1;
                                        to = string.Empty; jobno = string.Empty; dhid = string.Empty; from = string.Empty;
                                        ag_ppm = 0;
                                        au_ppm = 0;
                                        Length_Grade = 0;
                                    }
                                    else
                                        dtgValueCalculate.Rows.Insert(dtgValueCalculate.Rows.Count - 1, "", "", "", "", "", "", "", "");
                                }
                            }
                            else
                            {
                                int fila = row.Index;
                                if (!string.IsNullOrEmpty(txtSearchHoleId.Text.Trim()))
                                {
                                    CurrencyManager cm = (CurrencyManager)BindingContext[dtgDetailHoleID.DataSource];
                                    cm.SuspendBinding();
                                    dtgDetailHoleID.CurrentCell = null;
                                    dtgDetailHoleID.Rows[fila - 1].Visible = false;
                                }
                                //dataGridView2.Rows[fila - 1].Visible = false;
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dtgValueCalculate.RowCount > 1)
            {
                string contextValue = string.Empty;
                try
                {
                    foreach (DataGridViewRow row in dtgDetailHoleID.Rows)
                    {
                        if (row.Cells[0].Value != null && Convert.ToBoolean(row.Cells[0].Value) && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                        {
                            if (row.Cells[1].Value == null)
                            {
                                MessageBox.Show("Select all value Column Vn_mod this select");
                                return;
                            }
                        }
                    }
                    var culturaCol = CultureInfo.GetCultureInfo("es-CO");
                    DateTime dateReporte = Convert.ToDateTime(DateTime.Now, culturaCol);

                    foreach (DataGridViewRow row in dtgDetailHoleID.Rows)
                    {
                        if (row.Cells[0].Value != null && Convert.ToBoolean(row.Cells[0].Value) && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                        {
                            contextValue = "UPDATE [GZC].[dbo].[DH_Samples]   SET      Vn_mod = '" + row.Cells[1].Value.ToString() + "' WHERE SKDHSamples =" + Convert.ToInt32(row.Cells[dtgDetailHoleID.Columns["SKDHSamples"].Index].Value) + " and HoleID ='" + row.Cells[dtgDetailHoleID.Columns["dhid"].Index].Value.ToString() + "'";
                            LoadLog.alterdataBase(contextValue);

                            LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, contextValue, "update");
                        }
                        else
                        {
                            if (row.Cells[dtgDetailHoleID.Columns["Vn_mod"].Index].Value != null && !String.IsNullOrEmpty(row.Cells[dtgDetailHoleID.Columns["Vn_mod"].Index].Value.ToString()))
                            {
                                contextValue = "UPDATE [GZC].[dbo].[DH_Samples]   SET      Vn_mod = Null WHERE SKDHSamples =" + Convert.ToInt32(row.Cells[dtgDetailHoleID.Columns["SKDHSamples"].Index].Value) + " and HoleID ='" + row.Cells[dtgDetailHoleID.Columns["dhid"].Index].Value.ToString() + "'";
                                LoadLog.alterdataBase(contextValue);
                                LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, contextValue, "update");

                            }
                        }
                    }

                    foreach (DataGridViewRow row in dtgValueCalculate.Rows)
                    {
                        if (row.Cells[1].Value != null && !String.IsNullOrEmpty(row.Cells[1].Value.ToString()))
                        {
                            contextValue = String.Format("SELECT ToV FROM DH_IntercepInterval  WHERE HoleID = @HoleID and FromV= @FromV");
                            object valueTo = LoadLog.Exist_DB(contextValue, row.Cells[1].Value.ToString(), Convert.ToDecimal(row.Cells[2].Value));
                            LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, contextValue, "Search");

                            if (valueTo != null && valueTo.ToString().Length > 0)
                            {
                                string valueDescrioption = string.Empty;
                                if (!string.IsNullOrEmpty(txtComent.Text.Trim()))
                                {
                                    valueDescrioption = string.Concat(txtComent.Text, " Con intervale inicial de ", Convert.ToDecimal(row.Cells[2].Value), " hasta ", valueTo);
                                    contextValue = "update GZC.dbo.DH_IntercepInterval set Tov=" + Convert.ToDecimal(row.Cells[3].Value) + ",Length_Grade=" + Convert.ToDecimal(row.Cells[4].Value) + ", Au_Grade=" + Convert.ToDecimal(row.Cells[5].Value) + ",Ag_Grade= " + Convert.ToDecimal(row.Cells[6].Value) + ",Comments='" + valueDescrioption + "',TotalRegister=" + Convert.ToInt32(row.Cells[7].Value) + " , Date_Event ='" + dateReporte.ToString() + "' where HoleID ='" + row.Cells[1].Value.ToString() + "' and Fromv =" + Convert.ToDecimal(row.Cells[2].Value);
                                    LoadLog.alterdataBase(contextValue);
                                    LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, contextValue, "Update");

                                }
                                else
                                {
                                    MessageBox.Show("Comment is required for the rank update");
                                    return;
                                }
                            }
                            else
                            {
                                contextValue = "INSERT INTO GZC.dbo.DH_IntercepInterval(HoleID,Fromv,Tov,Length_Grade,Au_Grade,Ag_Grade,Comments,TotalRegister,Vn_mod,Date_Event)VALUES(" + "'" + row.Cells[1].Value.ToString() + "'," + Convert.ToDecimal(row.Cells[2].Value) + " ," + Convert.ToDecimal(row.Cells[3].Value) + "," + Convert.ToDecimal(row.Cells[4].Value) + " ," + Convert.ToDecimal(row.Cells[5].Value) + "," + Convert.ToDecimal(row.Cells[6].Value) + ",'" + txtComent.Text + "'," + Convert.ToInt32(row.Cells[7].Value) + ",'" + row.Cells[8].Value.ToString() + "','" + dateReporte + "')";
                                LoadLog.alterdataBase(contextValue);
                                LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, contextValue, "Update");

                            }
                        }
                    }


                    MessageBox.Show("Save full");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message);

                }
            }
        }

        private void txtBuscarSelloControl_Leave(object sender, EventArgs e)
        {
            DataTable dtCollars = generalData;
            string _sqlWhere = string.Empty;

            if (txtSearchHoleId.Text.Length > 1)// && !String.IsNullOrEmpty(cboSoruce.Text))
            {
                txtSearchHoleId.Text = txtSearchHoleId.Text.Replace("\r\n", "");

                if (!String.IsNullOrEmpty(cboSoruce.Text))

                    _sqlWhere = "HoleID  LIKE '%" + txtSearchHoleId.Text + "%' and Source='" + cboSoruce.Text + "'";
                else
                    _sqlWhere = "HoleID  LIKE '%" + txtSearchHoleId.Text + "%'";


                try
                {
                    if (dtCollars.Select(_sqlWhere).Count() > 0)
                    {

                        DataTable _newDataTable = dtCollars.Select(_sqlWhere).CopyToDataTable();
                        if (_newDataTable.Rows.Count == 1)
                        {

                            cmbHoleID.DisplayMember = "HoleID";
                            cmbHoleID.ValueMember = "HoleID";
                            cmbHoleID.DataSource = _newDataTable;
                            cmbHoleID.SelectedValue = "Select an option..";

                            if (!valueFirstRecuperation)
                            {
                                dtgValueCalculate.Rows.Clear();
                                valueFirstRecuperation = false;

                            }
                        }
                        else
                        {
                            if (_newDataTable.Rows.Count == 0)
                            {
                                cmbHoleID.DisplayMember = "HoleID";
                                cmbHoleID.ValueMember = "HoleID";
                                cmbHoleID.DataSource = generalData;
                                cmbHoleID.SelectedValue = "Select an option..";

                                MessageBox.Show("No record matches");
                            }
                            else
                            {
                                cmbHoleID.DisplayMember = "HoleID";
                                cmbHoleID.ValueMember = "HoleID";
                                cmbHoleID.DataSource = generalData;
                                cmbHoleID.SelectedValue = "Select an option..";

                                MessageBox.Show("Enter only the precise record");
                            }
                        }
                    }
                    else
                    {
                        cmbHoleID.DisplayMember = "HoleID";
                        cmbHoleID.ValueMember = "HoleID";
                        cmbHoleID.DataSource = generalData;
                        cmbHoleID.SelectedValue = "Select an option..";
                        MessageBox.Show("No record matches");
                    }
                }
                catch (Exception ex)
                {
                }

            }
            else
            {
                if (string.IsNullOrEmpty(cboSoruce.Text))
                    MessageBox.Show("Select to HoleID");
            }
        }

        private void txtBuscarSelloControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                DataTable dtCollars = generalData;

                if (txtSearchHoleId.Text.Length > 1 && !String.IsNullOrEmpty(cboSoruce.Text))
                {
                    txtSearchHoleId.Text = txtSearchHoleId.Text.Replace("\r\n", "");
                    string _sqlWhere = "HoleID  LIKE '%" + txtSearchHoleId.Text + "%'and Source='" + cboSoruce.Text + "'";

                    try
                    {
                        if (dtCollars.Select(_sqlWhere).Count() > 0)
                        {
                            DataTable _newDataTable = dtCollars.Select(_sqlWhere).CopyToDataTable();
                            if (_newDataTable.Rows.Count == 1)
                            {
                                cmbHoleID.DisplayMember = "HoleID";
                                cmbHoleID.ValueMember = "HoleID";
                                cmbHoleID.DataSource = _newDataTable;
                                cmbHoleID.SelectedValue = "Select an option..";
                                dtgValueCalculate.Rows.Clear();
                            }
                            else
                            {
                                if (_newDataTable.Rows.Count == 0)
                                {
                                    cmbHoleID.DisplayMember = "HoleID";
                                    cmbHoleID.ValueMember = "HoleID";
                                    cmbHoleID.DataSource = generalData;
                                    cmbHoleID.SelectedValue = "Select an option..";

                                    MessageBox.Show("No record matches");
                                }
                                else
                                {
                                    cmbHoleID.DisplayMember = "HoleID";
                                    cmbHoleID.ValueMember = "HoleID";
                                    cmbHoleID.DataSource = generalData;
                                    cmbHoleID.SelectedValue = "Select an option..";

                                    MessageBox.Show("Enter only the precise record");
                                }
                            }
                        }
                        else
                        {
                            cmbHoleID.DisplayMember = "HoleID";
                            cmbHoleID.ValueMember = "HoleID";
                            cmbHoleID.DataSource = generalData;
                            cmbHoleID.SelectedValue = "Select an option..";
                            MessageBox.Show("No record matches");
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(cboSoruce.Text))
                        MessageBox.Show("Select to Source");
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            exportIntercept(cboSoruce.Text);
            var culturaCol = CultureInfo.GetCultureInfo("es-CO");
            DateTime dateReporte = Convert.ToDateTime(DateTime.Now, culturaCol);

            LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, "Generate Report", "Report");
            Clear();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!containeRegister)
            {
                dtgDetailHoleID.Columns[0].Visible = true;
                checkBox1.Checked = true;
                containeRegister = true;
                label6.Text = "Active";
            }
            else
            {
                dtgDetailHoleID.Columns[0].Visible = false;
                checkBox1.Checked = false;
                containeRegister = false;
                label6.Text = "Inactive";
            }

        }
        #endregion
    }
}
