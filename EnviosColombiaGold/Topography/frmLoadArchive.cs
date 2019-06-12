using Entities.Topography;
using RN.Topography;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnviosColombiaGold.Topography
{
    public partial class frmLoadArchive : Form
    {
        IList<MasterTopography> Reader = new List<MasterTopography>();
        private string nameTopo { get; set; }
        private string nameFileGeneric { get; set; }

        public frmLoadArchive()
        {
            InitializeComponent();

            loadDate();


        }
        private void loadDate()
        {
            this.cboActivityAdd.DropDownStyle = ComboBoxStyle.DropDownList;

            if (Reader.Count() == 0)
                Reader = GetDataTopography.getMaterTopography("usp_TO_getMaterTopography");

            //cboActivityAdd.DisplayMember = "Description";
            //cboActivityAdd.ValueMember = "Code";
            //var value = Reader.Where(r => r.CodeMaster.Contains("actadition")).Select(c => c.Description).ToList();
            //value.Insert(value.ToList().Count, "Select an option..");

            //cboActivityAdd.DataSource = value;
            //cboActivityAdd.SelectedIndex = value.ToList().Count - 1;

        }
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            StreamReader sr = new StreamReader(strFilePath);
            string[] headers = sr.ReadLine().Split(';');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = sr.ReadLine().Split(';');
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        internal void Cargar(DataGridView dgView, string SLibro)
        {
            try
            {
                System.Data.OleDb.OleDbConnection MyConnection;
                System.Data.DataSet DtSet;
                System.Data.OleDb.OleDbDataAdapter MyCommand;
                MyConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SLibro + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'");
                MyConnection.Open();

                System.Data.DataTable dt = MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                string sheetName = string.Empty;
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["TABLE_NAME"].ToString().ToUpper().Trim().Contains("REPORTE"))

                        {
                            sheetName = dt.Rows[i]["TABLE_NAME"].ToString().ToUpper().Trim();
                            break;
                        }
                    }

                }

                MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [" + sheetName + "]", MyConnection);
                MyCommand.TableMappings.Add("Table", "TestTable");
                DtSet = new System.Data.DataSet();
                MyCommand.Fill(DtSet);
                dgView.DataSource = DtSet.Tables[0];
                dgView.AutoResizeColumns();
                MyConnection.Close();
            }
            catch (Exception oMsg)
            {
                MessageBox.Show(oMsg.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pbtnSearch_Click(object sender, EventArgs e)
        {
            String ruta = "";
            String str = "";
            try
            {
                OpenFileDialog openfile1 = new OpenFileDialog();
                openfile1.Filter = "CSV files (*.csv)|*.CSV";
                openfile1.Title = "Seleccione el archivo de Excel";
                if (openfile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openfile1.FileName.Equals("") == false)//Si la ruta no es vacia
                    {
                        ruta = openfile1.FileName;
                        str = Path.GetFileNameWithoutExtension(ruta);

                    }


                    string filepath = ruta;
                    DataTable res = ConvertCSVtoDataTable(filepath);
                    res.Columns.Add("Code_Lab");
                    res.Columns.Add("Detail_ID");
                    res.Columns.Add("Point_Type");
                    res.Columns.Add("Condition");
                    res.Columns.Add("Activity");
                    string pathArchive = string.Concat(Application.StartupPath, "\\CODIGO_LEVANTAMIENTO.XLSX");

                    Microsoft.Office.Interop.Excel.Application oExc = new Microsoft.Office.Interop.Excel.Application();
                    //Excel.Worksheet oSheeds = new Excel.Worksheet();
                    oExc.Workbooks.Open(pathArchive, 0, true, 5,
                                Type.Missing, Type.Missing, false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                "\t", false, false, 0, true, null, null);
                    oExc.Quit();

                    DataTable dtXls = new DataTable();

                    Workbook workbook = new Workbook();

                    workbook.LoadFromFile(pathArchive);
                    Worksheet sheet = workbook.Worksheets[0];

                    dtXls = sheet.ExportDataTable();

                    for (int i = 0; i < res.Rows.Count; i++)
                    {
                        res.Rows[i]["Code_Lab"] = res.Rows[i][0].ToString().Substring(0, 4);

                        string filter = string.Empty;

                        switch (Convert.ToInt32(res.Rows[i]["Code"]))
                        {
                            case 1:
                                filter = "11-19";
                                break;
                            case 2:
                                filter = "21-29";
                                break;
                            case 3:
                                filter = "31-39";
                                break;
                            case 4:
                                filter = "41-49";
                                break;
                            case 5:
                                filter = "51-59";
                                break;
                            default:
                                break;
                        }

                        if (String.IsNullOrEmpty(filter))
                        {

                            if (Convert.ToInt32(res.Rows[i]["Code"]) >= 11 && Convert.ToInt32(res.Rows[i]["Code"]) <= 19)
                                filter = "11-19";

                            else
                            {
                                if (Convert.ToInt32(res.Rows[i]["Code"]) >= 21 && Convert.ToInt32(res.Rows[i]["Code"]) <= 19)
                                    filter = "21-29";
                                else
                                {
                                    if (Convert.ToInt32(res.Rows[i]["Code"]) >= 31 && Convert.ToInt32(res.Rows[i]["Code"]) <= 39)
                                        filter = "31-39";

                                    else
                                    {
                                        if (Convert.ToInt32(res.Rows[i]["Code"]) >= 41 && Convert.ToInt32(res.Rows[i]["Code"]) <= 49)
                                            filter = "41-49";

                                        else
                                        {
                                            if (Convert.ToInt32(res.Rows[i]["Code"]) >= 51 && Convert.ToInt32(res.Rows[i]["Code"]) <= 59)
                                                filter = "51-59";
                                        }
                                    }
                                }
                            }

                            if (string.IsNullOrEmpty(filter))
                                filter = res.Rows[i]["Code"].ToString();

                        }

                        var dt = FiltrarDataTable(dtXls, String.Concat("Code like '%", filter, "%'"));

                        if (dt.Rows.Count > 0)
                        {
                            res.Rows[i]["Detail_ID"] = dt.Rows[0]["Detail_ID"].ToString().Trim();
                            res.Rows[i]["Point_Type"] = dt.Rows[0]["PointType"].ToString().Trim();
                            res.Rows[i]["Condition"] = dt.Rows[0]["Condition"].ToString().Trim();
                            res.Rows[i]["Activity"] = dt.Rows[0]["Activity"].ToString().Trim();
                        }
                    }

                    dtgDetailLaborTopo.DataSource = res;

                    dtgDetailLaborTopo.Columns[0].DisplayIndex = 10;
                    dtgDetailLaborTopo.Columns[0].HeaderText = "Activity_Add";
                    var value = Reader.Where(r => r.CodeMaster.Contains("actadition")).Select(c => c.Description).ToList();

                    DataGridViewComboBoxColumn CmbDetalle = dtgDetailLaborTopo.Columns[0] as DataGridViewComboBoxColumn;
                    value.Insert(value.ToList().Count, "--");

                    CmbDetalle.DisplayMember = "Description";
                    CmbDetalle.ValueMember = "Code";
                    CmbDetalle.DataSource = value;

                    string nameFile = Path.GetFileName(filepath);

                    txtDateIn.Text = string.Concat(nameFile.Substring(0, 2), "/", nameFile.Substring(2, 2), "/", nameFile.Substring(4, 4));
                    txtDateIn.Enabled = false;
                    nameTopo = nameFile.Substring(9, 2);
                    nameFileGeneric = nameFile;

                    txtNameTopo.Text = Reader.Where(r => r.CodeMaster.Contains("acttopogr")).Where(s => s.Code.Equals(nameFile.Substring(9, 2))).Select(p => p.Description).First().Trim();
                    txtNameTopo.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //String acttopogr =
            //GetDataTopography.InsertMoveTopograp("GetDataTopography",);
        }

        public DataTable FiltrarDataTable(DataTable _dt, string _filter)
        {
            try
            {
                DataTable table = new DataTable();
                DataRow[] foundRows;
                foundRows = _dt.Select(_filter);
                table.Clear();
                if (foundRows.Length > 0) table = foundRows.CopyToDataTable();
                return table;
            }
            catch (Exception ex) { throw (ex); }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                grpManual.Enabled = true;
                cboTypeCondition.DisplayMember = "Description";
                cboTypeCondition.ValueMember = "Code";

                var value = Reader.Where(r => r.CodeMaster.Contains("acttopogr")).Select(c => c.Description).ToList();
                value.Insert(value.ToList().Count, "Select an option..");

                cboTypeCondition.DataSource = value;
                cboTypeCondition.SelectedIndex = value.ToList().Count - 1;


                cboActivityAdd.DisplayMember = "Description";
                cboActivityAdd.ValueMember = "Code";

                value = Reader.Where(r => r.CodeMaster.Contains("actadition")).Select(c => c.Description).ToList();
                value.Insert(value.ToList().Count, "Select an option..");

                cboActivityAdd.DataSource = value;
                cboActivityAdd.SelectedIndex = value.ToList().Count - 1;

            }
            else
                grpManual.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                if (String.IsNullOrEmpty(validateEntry(txtID.Text.Trim())))
                {
                    string pathArchive = string.Concat(Application.StartupPath, "\\CODIGO_LEVANTAMIENTO.XLSX");

                    Microsoft.Office.Interop.Excel.Application oExc = new Microsoft.Office.Interop.Excel.Application();
                    //Excel.Worksheet oSheeds = new Excel.Worksheet();
                    oExc.Workbooks.Open(pathArchive, 0, true, 5,
                                Type.Missing, Type.Missing, false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                "\t", false, false, 0, true, null, null);
                    oExc.Quit();

                    DataTable dtXls = new DataTable();

                    Workbook workbook = new Workbook();

                    workbook.LoadFromFile(pathArchive);
                    Worksheet sheet = workbook.Worksheets[0];

                    dtXls = sheet.ExportDataTable();
                    var dt = FiltrarDataTable(dtXls, String.Concat("Code like '%", txtCCode.Text, "%'"));
                    string idTopo = string.Empty, typeLab = string.Empty, Detail_ID = string.Empty, Point_Type = string.Empty, Condition = string.Empty, Activity = string.Empty, Code_Lab = string.Empty;
                    Code_Lab = txtID.Text.ToString().Substring(0, 4);
                    if (dt.Rows.Count > 0)
                    {
                        Detail_ID = dt.Rows[0]["Detail_ID"].ToString().Trim();
                        Point_Type = dt.Rows[0]["PointType"].ToString().Trim();
                        Condition = dt.Rows[0]["Condition"].ToString().Trim();
                        Activity = dt.Rows[0]["Activity"].ToString().Trim();
                    }
                    IList<MasterTopography> value = Reader.Where(r => r.CodeMaster.Contains("actlabtype")).ToList();

                    if (!cboActivityAdd.Text.Contains("Select"))
                        typeLab = value[cboActivityAdd.SelectedIndex].Code.ToString().Trim();
                    else
                    {
                        MessageBox.Show("Select value in Aditional Activity Type");
                        return;
                    }

                    value = Reader.Where(r => r.CodeMaster.Contains("acttopogr")).ToList();

                    if (!cboTypeCondition.Text.Contains("Select"))
                        idTopo = value[cboTypeCondition.SelectedIndex].Code.ToString().Trim();
                    else
                    {
                        MessageBox.Show("Select value in Topography Type");
                        return;
                    }
                    GetDataTopography.InsertMoveTopograp("USP_To_InsertMove", txtID.Text.Trim(), Convert.ToInt32(txtEast.Text.Trim()), Convert.ToInt32(txtNoth.Text.Trim()), Convert.ToInt32(txtSouth.Text.Trim()), Convert.ToInt32(txtCCode.Text.Trim()), Code_Lab, Detail_ID, Point_Type, Condition, idTopo, string.Empty, DateTime.Now, typeLab);
                }
                else
                    return;
                //GetDataTopography.InsertMoveTopograp("GetDataTopography",);
            }
            else
            {
                if (dtgDetailLaborTopo.DataSource != null)
                {
                    string typeLab = string.Empty;
                    IList<MasterTopography> value = Reader.Where(r => r.CodeMaster.Contains("actadition")).ToList();


                    string idLabor = string.Empty;
                    foreach (DataGridViewRow row in dtgDetailLaborTopo.Rows)
                    {
                        if (idLabor == string.Empty)
                        {
                            idLabor = row.Cells[1].Value.ToString().Trim();
                            if (row.Cells[0].Value.ToString().Trim() != string.Empty)
                                typeLab = value.Where(p => p.Description.Contains(row.Cells[0].Value.ToString().Trim())).Select(p => p.Code).First().Trim();

                        }
                        else
                        {

                            if (!idLabor.Equals(row.Cells[1].Value.ToString().Trim()))
                            {
                                typeLab = value.Where(p => p.Description.Contains(row.Cells[0].Value.ToString().Trim())).Select(p => p.Code).First().Trim();
                                if (row.Cells[0].Value.ToString().Trim() != string.Empty)
                                    typeLab = value.Where(p => p.Description.Contains(row.Cells[0].Value.ToString().Trim())).Select(p => p.Code).First().Trim();


                            }
                        }

                        GetDataTopography.InsertMoveTopograp("USP_To_InsertMove", row.Cells[1].Value.ToString().Trim(), Convert.ToInt32(row.Cells[2].Value.ToString().Trim()), Convert.ToInt32(row.Cells[3].Value.ToString().Trim()), Convert.ToInt32(row.Cells[4].Value.ToString().Trim()), Convert.ToInt32(row.Cells[5].Value), row.Cells[6].Value.ToString(), row.Cells[7].Value.ToString(), row.Cells[8].Value.ToString(), row.Cells[9].Value.ToString().Trim(), nameTopo, nameFileGeneric, DateTime.Now, typeLab);
                    }

                    MessageBox.Show("Save full");
                }
                else
                {
                    MessageBox.Show("the values to be loaded are required");
                    return;
                }
            }

        }

        private string validateEntry(string Code_Lab)
        {
            if (string.IsNullOrEmpty(txtID.Text))
                return "El Campo ID  es requerido";

            if (string.IsNullOrEmpty(txtNoth.Text))
                return "El Campo Noth es requerido";

            if (string.IsNullOrEmpty(txtSouth.Text))
                return "El Campo South es requerido";

            if (string.IsNullOrEmpty(txtEast.Text))
                return "El Campo East es requerido";

            if (string.IsNullOrEmpty(txtCCode.Text))
                return "El Campo CCode es requerido";

            if (string.IsNullOrEmpty(Code_Lab) || Code_Lab.Length < 4)
                return "El Campo ID debe de tener 4 carateres es requerido";

            return string.Empty;
        }

        private void txtEast_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Only numbers are allowed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtNoth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Only numbers are allowed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtSouth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Only numbers are allowed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
    }
}
