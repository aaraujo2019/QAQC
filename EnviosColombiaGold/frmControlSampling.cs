using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.IO;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Configuration;
using Entities.Config;
using Entities.Person;

namespace EnviosColombiaGold
{
    public partial class frmControlSampling : Form
    {

        public clsInvSamples oInv = new clsInvSamples();
        clsSampShipment oSampShipment = new clsSampShipment();
        clsRf oRf = new clsRf();
        static string sEditAssign = "0";
        private string sNomArchivo;


        public frmControlSampling()
        {
            try
            {
                InitializeComponent();
                LoadSamplesAssign();
                FillCmb();
                LoadAssignQualityControls();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void CargarCombosSamplesEntity(IList<Entities.Person.SampleControl> list, ComboBox _cbox)
        {
            try
            {
                if (list.Count > 0)
                {
                    _cbox.Items.AddRange(list.ToArray());
                    _cbox.SelectedText = "-1";
                    _cbox.SelectedIndex = list.ToList().Count - 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void CargarCombosSamples(DataTable _dt, ComboBox _cbox)
        {
            try
            {
                if (_dt.Rows.Count > 0)
                {
                    _cbox.DataSource = _dt.Copy();
                    _cbox.ValueMember = _dt.Columns[0].ToString();
                    _cbox.DisplayMember = _dt.Columns[1].ToString();
                    _cbox.SelectedValue = "-1";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void FillCmb()
        {
            try
            {
                //DataTable dtInvSamp = new DataTable();
                //dtInvSamp = LoadInvSamples("1", 1);
                //DataRow dr = dtInvSamp.NewRow();
                //dr[0] = "-1";
                //dr[1] = "Select an option..";
                //dtInvSamp.Rows.Add(dr);
                //CargarCombosSamples(dtInvSamp, cmbSamples);
                //CargarCombosSamples(dtInvSamp, cmbFromSamplesAssign);
                //CargarCombosSamples(dtInvSamp, cmbToSamplesAssign);
                //CargarCombosSamples(dtInvSamp, cmbFromExport);
                //CargarCombosSamples(dtInvSamp, cmbToExport);

                //Implementación David Ciro
                cmbSamples.DisplayMember = "Sample";
                cmbSamples.ValueMember = "SKInvSamples";
                cmbToExport.DisplayMember = "Sample";
                cmbToExport.ValueMember = "SKInvSamples";
                cmbFromExport.DisplayMember = "Sample";
                cmbFromExport.ValueMember = "SKInvSamples";
                cmbFromSamplesAssign.DisplayMember = "Sample";
                cmbFromSamplesAssign.ValueMember = "SKInvSamples";
                cmbToSamplesAssign.DisplayMember = "Sample";
                cmbToSamplesAssign.ValueMember = "SKInvSamples";

                IList<Entities.Person.SampleControl> value = LoadInvSamplesEntity("1", 1).ToList();

                CargarCombosSamplesEntity(value, cmbSamples);
                CargarCombosSamplesEntity(value, cmbFromSamplesAssign);
                CargarCombosSamplesEntity(value, cmbToSamplesAssign);
                CargarCombosSamplesEntity(value, cmbFromExport);
                CargarCombosSamplesEntity(value, cmbToExport);

                var RfTypeSample = oRf.getRfTypeSampleQaqcEntity();


                Ent_Prefix getRfTypeSample = new Ent_Prefix();
                getRfTypeSample.Code = "-1";
                getRfTypeSample.Description = "Select an option..";
                RfTypeSample.Insert(RfTypeSample.ToList().Count, getRfTypeSample);


                //DataTable dtSampleT = new DataTable();
                //dtSampleT = oRf.getRfTypeSampleQaqc();
                //DataRow dr1 = dtSampleT.NewRow();
                //dr1[0] = "-1";
                //dr1[1] = "Select an option..";
                //dtSampleT.Rows.Add(dr1);

                cmbSampleType.DisplayMember = "Comb";
                cmbSampleType.ValueMember = "Code";
                //cmbSampleType.DataSource = dtSampleT;
                //cmbSampleType.SelectedValue = -1;
                cmbSampleType.DataSource = RfTypeSample;
                cmbSampleType.SelectedValue = value.ToList().Count - 1;

                var getUsers = oRf.getUsersEntity();


                Ent_User getUsersValue = new Ent_User();
                getUsersValue.code = "-1";
                getUsersValue.Project = "Select an option..";
                getUsers.Insert(getUsers.ToList().Count, getUsersValue);




                //DataTable dtUsers = new DataTable();
                //dtUsers = oRf.getUsers("");

                //DataRow[] datoU = dtUsers.Select("grupo in ('Administradores','Perforacion')");
                //DataTable dtToR = dtUsers.Clone();

                //foreach (DataRow r in datoU)
                //{
                //    dtToR.ImportRow(r);
                //}


                //DataRow dr2 = dtToR.NewRow();
                //dr2[0] = "-1";
                //dr2[7] = "Select an option..";
                //dtToR.Rows.Add(dr2);

                cmbGeologist.DisplayMember = "Project";
                cmbGeologist.ValueMember = "code";
                //cmbGeologist.DataSource = dtToR;
                cmbGeologist.DataSource = getUsers;
                //cmbGeologist.SelectedValue = -1;
                cmbGeologist.SelectedValue = getUsers.ToList().Count - 1;



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadSamplesAssign()
        {
            try
            {
                DataTable dtSampAssign = new DataTable();
                oInv.sOpcion = "1";
                oInv.iId_User = 1;
                //dtSampAssign = oInv.getInvSamples_List().Tables[0];
                //dgSamplesAssign.DataSource = dtSampAssign;

                //Implemtacion David Ciro
                var value = oInv.getSamplesAssign();
                dgSamplesAssign.DataSource = value;

                //SKInvSamples
                dgSamplesAssign.Columns["SKInvSamplesControls"].Visible = false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadAssignExport()
        {
            try
            {
                DataTable dtAssignQuality = new DataTable();

                dtAssignQuality = oInv.getInvSamples_ListRange(Int64.Parse(cmbFromExport.SelectedValue.ToString()),
                    Int64.Parse(cmbToExport.SelectedValue.ToString()));

                dgExportXls.DataSource = dtAssignQuality;
                dgExportXls.Columns["SKInvSamples"].Visible = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadAssignQualityControls()
        {
            try
            {
                DataTable dtAssignQuality = new DataTable();

                //Implementación David Ciro
                dtAssignQuality = LoadInvSamples("1", 1);
                dgAssignQControl.DataSource = dtAssignQuality;
                dgAssignQControl.Columns["SKInvSamples"].Visible = false;

                //var samplesEntity = LoadInvSamplesEntity("1", 1);
                //dgAssignQControl.DataSource = samplesEntity;
                //dgAssignQControl.Columns["SKInvSamples"].Visible = false;



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable LoadInvSamples(string _sOpcion, int _iIdUser)
        {
            try
            {
                DataTable dtInv = new DataTable();

                oInv.sOpcion = "3";
                oInv.iId_User = 1;
                dtInv = oInv.getInvSamples_List().Tables[0];
                dgSamplesAssign.DataSource = dtInv;

                //Implementación David.ciro
                var value = oInv.getSamplesAssign();
                //dgSamplesAssign.DataSource = value;

                return dtInv;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private IList<Entities.Person.SampleControl> LoadInvSamplesEntity(string _sOpcion, int _iIdUser)
        {
            try
            {
                oInv.sOpcion = "3";
                oInv.iId_User = 1;
                //dtInv = oInv.getInvSamples_List().Tables[1];

                //Implementación David.ciro
                var value = oInv.getSamplesAssign();

                Entities.Person.SampleControl pre = new Entities.Person.SampleControl();
                pre.Code = "-1";
                pre.Description = "Select an option..";
                value.Insert(value.ToList().Count, pre);

                dgSamplesAssign.DataSource = value;

                return value;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable LoadSampShipmentAll()
        {
            try
            {
                //clsSampShipment oSampShipment = new clsSampShipment();
                DataTable dtSampShipment = new DataTable();
                dtSampShipment = oSampShipment.getSampShipmentAll();

                return dtSampShipment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool Keypress(KeyPressEventArgs e)
        {

            if (Char.IsNumber(e.KeyChar))
            {
                return false;
            }
            if (Char.IsLetter(e.KeyChar))
            {
                return true;
            }

            return false;
        }


        private void btnAceptAQCS_Click(object sender, EventArgs e)
        {
            try
            {

                oInv.sType = cmbSampleType.SelectedValue.ToString();
                oInv.iSKInvSamples = int.Parse(cmbSamples.SelectedValue.ToString());
                string sResp = oInv.InvSamples_Update();
                if (sResp == "OK")
                {
                    cmbSampleType.SelectedValue = -1;
                    cmbSamples.SelectedValue = -1;
                    cmbSamples.Focus();
                    LoadAssignQualityControls();
                }
                else
                {
                    MessageBox.Show("Assign Error ", "Assign Quality Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Control Sampling", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgAssignQControl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cmbSamples.SelectedValue = dgAssignQControl.Rows[e.RowIndex].Cells["SKInvSamples"].Value.ToString();
                cmbSampleType.SelectedValue = dgAssignQControl.Rows[e.RowIndex].Cells["Type"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelAQCS_Click(object sender, EventArgs e)
        {
            try
            {
                string val = string.Empty;
                cmbSampleType.SelectedValue = -1;
                cmbSamples.SelectedValue = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Control Sampling", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CleanControlsAssign()
        {
            try
            {
                cmbGeologist.SelectedValue = "-1";
                cmbFromSamplesAssign.SelectedValue = "-1";
                cmbToSamplesAssign.SelectedValue = "-1";
                dtDateAssign.Text = DateTime.Now.ToShortDateString();
                sEditAssign = "0";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnAceptASamp_Click(object sender, EventArgs e)
        {
            try
            {

                if (sEditAssign == "0")
                {
                    oInv.sOpcion = "1";
                    oInv.iSKInvSamplesControls = 0;
                }
                else if (sEditAssign == "1")
                {
                    oInv.sOpcion = "2";
                }


                oInv.iId_User = int.Parse(cmbGeologist.SelectedValue.ToString());

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                DateTime dDate = DateTime.Parse(dtDateAssign.Text.ToString());
                string sDate = dDate.Year.ToString().PadLeft(4, '0') + dDate.Month.ToString().PadLeft(2, '0') +
                    dDate.Day.ToString().PadLeft(2, '0');

                oInv.sDateAssign = sDate.ToString();
                oInv.sFromAssign = cmbFromSamplesAssign.SelectedValue.ToString();
                oInv.sToAssign = cmbToSamplesAssign.SelectedValue.ToString();

                string sResp = oInv.InvSamplesControl_Add();
                if (sResp != "OK")
                {
                    MessageBox.Show("Save error: " + sResp);
                }
                else
                {
                    CleanControlsAssign();
                    cmbGeologist.Focus();
                    LoadSamplesAssign();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Control Sampling", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelASamp_Click(object sender, EventArgs e)
        {
            try
            {
                CleanControlsAssign();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Control Sampling", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgSamplesAssign_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                sEditAssign = "1";
                oInv.iSKInvSamplesControls = int.Parse(dgSamplesAssign.Rows[e.RowIndex].Cells["SKInvSamplesControls"].Value.ToString());
                cmbGeologist.SelectedValue = dgSamplesAssign.Rows[e.RowIndex].Cells["Id_User"].Value.ToString();
                cmbFromSamplesAssign.SelectedValue = dgSamplesAssign.Rows[e.RowIndex].Cells["FromAssign"].Value.ToString();
                cmbToSamplesAssign.SelectedValue = dgSamplesAssign.Rows[e.RowIndex].Cells["ToAssign"].Value.ToString();
                dtDateAssign.Text = dgSamplesAssign.Rows[e.RowIndex].Cells["Date_Assign"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Control Sampling", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtXls = new DataTable();
                FileInfo oFi = new FileInfo(oDialog.FileName);
                string sExt = oFi.Extension.ToString();
                sNomArchivo = oFi.Name.Substring(0, oFi.Name.ToString().Length - sExt.ToString().Length);

                string sCon;
                sCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtRuta.Text + ";Extended Properties=Excel 12.0;";
                OleDbConnection oCon = new OleDbConnection(sCon);
                OleDbDataAdapter oAdap = new OleDbDataAdapter("Select * from [" + cmbSheed.Text.ToString() + "$]", oCon);
                oCon.Open();
                oAdap.Fill(dtXls);
                oCon.Close();
                dgLoadSamples.DataSource = dtXls;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void pbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = oDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtRuta.Text = oDialog.FileName;
                    /*Dim fi As New System.IO.FileInfo(fileName) 
                   Return fi.Name 

                   MessageBox.Show(GetOnlyFileTitle(dlg.FileName));*/

                    FileInfo oFi = new FileInfo(oDialog.FileName);
                    string sExt = oFi.Extension.ToString();
                    sNomArchivo = oFi.Name.Substring(0, oFi.Name.ToString().Length - sExt.ToString().Length);

                    Excel.Application oExc = new Excel.Application();
                    //Excel.Worksheet oSheeds = new Excel.Worksheet();
                    oExc.Workbooks.Open(txtRuta.Text, 0, true, 5,
                                Type.Missing, Type.Missing, false, Excel.XlPlatform.xlWindows,
                                "\t", false, false, 0, true, null, null);
                    cmbSheed.Items.Clear();
                    foreach (Excel.Worksheet oSh in oExc.Sheets)
                    {
                        cmbSheed.Items.Add(oSh.Name);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {

                string sResp = oInv.Inv_Samples_Mass("Inv_Samples", (DataTable)dgLoadSamples.DataSource);
                if (sResp == "OK")
                {
                    MessageBox.Show("Load successful", "Load Samples", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {


            try
            {
                Excel.Application oXL;
                Excel._Workbook oWB;
                Excel._Worksheet oSheet;
                Excel.Range oRng;

                oXL = new Excel.Application();
                oXL.Visible = true;


                oWB = oXL.Workbooks.Open(ConfigurationSettings.AppSettings["Ruta_InvSamples"].ToString(),
                    0, false, 5,
                Type.Missing, Type.Missing, false, Type.Missing, Type.Missing, true, false,
                Type.Missing, false, false, false);

                oSheet = (Excel._Worksheet)oWB.Sheets[2];

                oSheet.Cells[9, 1] = dgExportXls.Rows[0].Cells["nombre_User"].Value.ToString();

                int iInicial = 12;
                int iColumnInicial = 1;
                for (int i = 0; i < dgExportXls.Rows.Count - 1; i++)
                {
                    if (i != 0)
                    {
                        if (i < 100)
                        {
                            if (i % 20 == 0)
                            {
                                iColumnInicial = iColumnInicial + 2;
                                iInicial = 12;
                            }
                        }
                        else if (i >= 100)
                        {
                            if (i % 20 == 0)
                            {
                                iColumnInicial = iColumnInicial + 2;
                                iInicial = 34;
                            }
                        }
                    }

                    if (i == 100)
                    {
                        iColumnInicial = 1;
                    }

                    oSheet.Cells[iInicial, iColumnInicial] = dgExportXls.Rows[i].Cells["Sample"].Value.ToString();
                    oSheet.Cells[iInicial, iColumnInicial + 1] = dgExportXls.Rows[i].Cells["Type"].Value.ToString();

                    iInicial += 1;
                }

                //Hoja2
                oSheet = (Excel._Worksheet)oWB.Sheets[1];

                int iInicial2 = 2;
                int iColumnInicial2 = 1;
                for (int i = 0; i < dgExportXls.Rows.Count - 1; i++)
                {
                    oSheet.Cells[iInicial2, iColumnInicial2] = dgExportXls.Rows[i].Cells["Sample"].Value.ToString();
                    oSheet.Cells[iInicial2, iColumnInicial2 + 1] = dgExportXls.Rows[i].Cells["Type"].Value.ToString();
                    iInicial2 += 1;
                }
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAceptExport_Click(object sender, EventArgs e)
        {
            try
            {
                LoadAssignExport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




    }
}
