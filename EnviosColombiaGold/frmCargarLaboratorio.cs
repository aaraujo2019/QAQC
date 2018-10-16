using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Xml.Serialization;
using System.Configuration;

using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Data.SqlClient;
using OfficeOpenXml;
using System.Linq;
using System.Globalization;

namespace EnviosColombiaGold
{
    public partial class frmCargarLaboratorio : Form
    {
        private DataTable dtXls = new DataTable();
        private DataTable dtElementos = new DataTable();
        private DataRow drElem;
        private DataSet ds = new DataSet();
        private string sLab;
        private string sEnvio;
        private string sSample;
        private string sQaqc;
        private string sType;
        private string sNomArchivo;
        private string sDateRep;
        private DataTable dtQCConversion = new DataTable();
        clsRf oRf = new clsRf();
        clsLabSubmit oLabSub = new clsLabSubmit();
        clsAssay oAssay = new clsAssay();


        #region Variables Globales
        SqlConnection conn = new SqlConnection();
        public string codEnvio { get; set; }
        #endregion Variables Globales


        public frmCargarLaboratorio()
        {
            InitializeComponent();
            LoadXML();
            dtQCConversion = oRf.getRfQCConversion();
            LoadCmb();
        }

        private void LoadCmb()
        {
            try
            {
                DataTable dtJobNo = new DataTable();
                dtJobNo = oRf.getLabResult("1", "");
                DataRow drJ = dtJobNo.NewRow();
                drJ[1] = "Select an option..";
                dtJobNo.Rows.Add(drJ);

                cmbJobNo.DisplayMember = "Jobno";
                cmbJobNo.ValueMember = "Jobno";

                cmbJobNo.DataSource = dtJobNo;

                cmbJobNo.SelectedValue = "Select an option..";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadXML()
        {
            string myXMLfile = Application.StartupPath.ToString() + @"\Datos.xml";
            ds = new DataSet();
            // Create new FileStream with which to read the schema.
            System.IO.FileStream fsReadXml = new System.IO.FileStream
                (myXMLfile, System.IO.FileMode.Open);
            try
            {
                ds.ReadXml(fsReadXml);
                //dgXML.DataSource = ds.Tables[0];
                //dgDatos.DataMember = "Cust";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                fsReadXml.Close();
            }
        }

        private void frmCargarResEnvio_Load(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                oDialog.Title = "Seleccionar Archivos";
                oDialog.Filter = "Todos los archivos (*.xls*)|*.xls*";
                oDialog.Multiselect = false;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
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
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string sCampos = "";
                string sCamposVal = "";
                string sSQL = "";
                //int iCantCamp = 0;
                //int[] iColumn = new int[dtElementos.Columns.Count];


                sLab = dtXls.Columns[0].Caption.ToString();
                sDateRep = dtXls.Rows[7][1].ToString();
                sEnvio = dtXls.Rows[8][1].ToString();
                sSample = dtXls.Rows[5][1].ToString();
                sQaqc = ConfigurationSettings.AppSettings["Qaqc"].ToString();


                string sInsertLabRes = "";
                sInsertLabRes += "Insert into LabResult (Submit, Jobno, CantSample, Lab, DateReport) Values ('" +
                    sEnvio + "','" + sNomArchivo + "'," + sSample + ",'" + sLab + "','" + sDateRep + "')";
                string sResp = oRf.ExecSQL(sInsertLabRes);
                MessageBox.Show(sResp);


                oLabSub.sOpcion = "1";
                oLabSub.sSubmit = sEnvio.ToString();
                DataTable dtType = oLabSub.getLabSubmit();
                if (dtType.Rows.Count > 0)
                {
                    sType = dtType.Rows[0]["SampleType"].ToString();
                }
                else { sType = ""; }

                //Capturo la posicion de la tabla donde debo empezar
                int iPosicionFin = 0;
                for (int a = 0; a < dtElementos.Rows.Count; a++)
                {
                    if (dtElementos.Rows[a][0].ToString() == "END/FIN")
                    {
                        iPosicionFin = a;
                    }
                }

                for (int iRowElem = 5; iRowElem < iPosicionFin; iRowElem++)
                //for (int iRowElem = 5; iRowElem < 8; iRowElem++)


                {

                    for (int iElem = 0; iElem < dtElementos.Columns.Count; iElem++)
                    {



                        for (int iXml = 0; iXml < ds.Tables[0].Rows.Count; iXml++)
                        {

                            //string sCampoElem;
                            string[] words = dtElementos.Columns[iElem].Caption.ToString().Split('*');


                            if (words[0].ToUpper() == ds.Tables[0].Rows[iXml][1].ToString().ToUpper())
                            {

                                if (words[0].ToUpper() == "AU")
                                {
                                    string sAu = "Au";

                                    switch (dtElementos.Rows[1][iElem].ToString())
                                    {
                                        case "FAG505":
                                            sAu = "Aufagr";
                                            break;
                                        case "FAG303":
                                            sAu = "Aufagr";
                                            break;
                                        case "FAA313":
                                            sAu = "Aufaaa1";
                                            break;
                                        case "FAA515":
                                            sAu = "Aufaaa";
                                            break;
                                        default:
                                            sAu = "Aufaaa";
                                            break;
                                    }

                                    sCampos += ",[" + sAu + "]";
                                    sCamposVal += ",'" + dtElementos.Rows[iRowElem][iElem].ToString() + "'";
                                }
                                else if (words[0].ToUpper() == "AG")
                                {
                                    string sAg = "Ag";

                                    switch (dtElementos.Rows[1][iElem].ToString())
                                    {
                                        case "AAS41B":
                                            sAg = "Agfagr";
                                            break;
                                        case "ICP41B":
                                            sAg = "Agfagr1";
                                            break;
                                        case "AAS42C":
                                            sAg = "Agfa1";
                                            break;
                                        case "ICP12B":
                                            sAg = "Agfa";
                                            break;
                                        default:
                                            sAg = "Agfa";
                                            break;
                                    }
                                    sCampos += ",[" + sAg + "]";
                                    sCamposVal += ",'" + dtElementos.Rows[iRowElem][iElem].ToString() + "'";
                                }
                                else
                                {
                                    sCampos += ",[" + ds.Tables[0].Rows[iXml][0].ToString() + "]";
                                    sCamposVal += ",'" + dtElementos.Rows[iRowElem][iElem].ToString() + "'";
                                }
                            }
                        }
                    }


                    clsLabsumitIn oLIn = new clsLabsumitIn();
                    oLIn.sSample = dtElementos.Rows[iRowElem][0].ToString();
                    DataTable dtLIn = oLIn.getLabSubmitInBySampleReAssay();
                    if (dtLIn.Rows.Count > 0)
                    {
                        sQaqc = "0";
                    }

                    sSQL = "Insert into Assay (JobNo, [Sample], Qaqc, [Type] " +
                        sCampos + ") Values ('" + sNomArchivo + "','" + dtElementos.Rows[iRowElem][0].ToString() +
                        "','" + sQaqc + "','" + sType + "'" + sCamposVal + ")";

                    string sRespAss = oRf.ExecSQL(sSQL);
                    MessageBox.Show(sRespAss);
                    sCampos = "";
                    sCamposVal = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public SqlConnection OpenConexion(string cnnString)
        {
            SqlConnection Conn = new SqlConnection();
            Conn.ConnectionString = cnnString;
            try
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
                else
                    Conn.Open();
                return Conn;
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("Error al Abrir Conexion " + Ex.Errors);
            }
            return Conn;
        }


        private void pbtnUpdate_Click(object sender, EventArgs e)
        {
            if (dgXls.DataSource == null)
            {
                MessageBox.Show("Cargue el archivo reporte de análisis químico.");
                return;
            }
            string contextValue = string.Empty;
            try
            {
                //variables de insercion
                using (ExcelPackage paquete = new ExcelPackage())
                {
                    // Creamos un flujo a partir del archivo de Excel, y lo cargamos en el paquete
                    using (FileStream flujo = File.OpenRead(txtRuta.Text))
                    {
                        paquete.Load(flujo);
                    }
                    // Obtenemos la primera hoja del documento
                    ExcelWorksheet hoja1 = paquete.Workbook.Worksheets.First();

                    string Ordentrabajo = hoja1.Cells[11, 2].Text.Trim();

                    codEnvio = hoja1.Cells[12, 2].Text.Trim();

                    DateTime fecha = Convert.ToDateTime((hoja1.Cells[16, 2].Text.Trim()));
                    // Empezamos a leer a partir de la segunda fila

                    for (int numFila = 46; numFila <= 200; numFila++)
                    {
                        string tenor = string.Empty;
                        string tenorufagr = string.Empty;
                        string tenorag = string.Empty;
                        string muestra = Convert.ToString(hoja1.Cells[numFila, 1].Text);

                        if (Convert.ToString(hoja1.Cells[numFila, 2].Text) == "<0.02")
                            tenor = "0.01";
                        else
                        {
                            if (Convert.ToString(hoja1.Cells[numFila, 2].Text) == ">10")
                                tenor = "10.001";
                            else
                                tenor = Convert.ToString(hoja1.Cells[numFila, 2].Text);
                        }

                        tenorufagr = Convert.ToString(hoja1.Cells[numFila, 2].Text);

                        if (!String.IsNullOrEmpty(hoja1.Cells[numFila, 1].Text))
                        {
                            if ((ValidaSiExiste(Ordentrabajo, muestra)))
                                contextValue = "UPDATE  Assay  SET Aufaaa = '" + tenor + "'  ,  Aglab = '" + tenorag + "' , Agfa = '" + tenorag + "' , aufagr = '" + tenorufagr + "' WHERE jobno= '" + Ordentrabajo + "'  and sample= '" + muestra + "'   ";
                            else
                                contextValue = "INSERT INTO Assay (jobno,sample,Aufaaa,aufagr, qaqc , Aglab , Agfa )VALUES('" + Ordentrabajo + "','" + muestra + "' ,'" + tenor + "' ,'" + tenorufagr + "' , 1 , '" + tenorag + "' , '" + tenorag + "' )";

                            oLabSub.alterdataBase(contextValue);
                        }
                    }
                    cargarenlabresult(Ordentrabajo, codEnvio, fecha);
                    Clear();
                }

                MessageBox.Show("Importacion Finalizada");
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }
        public void Clear()
        {
            txtRuta.Text = string.Empty;
            dgXls.DataSource = null;
        }
        private bool ValidaSiExiste(string jobno, string sample)
        {
            try
            {
                string sqlbuscar = String.Format("SELECT COUNT(*) FROM Assay  WHERE jobno = @jobno and sample = @sample");

                if (Convert.ToInt32(oLabSub.Exist_DB(sqlbuscar, jobno, sample, "@jobno", "@sample")) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Save error LabSubmitIn. " + ex.Message); ;
            }
        }

        private bool ValidateLabsumit(string submit, string sample)
        {
            try
            {
                string sqlbuscar = String.Format("SELECT COUNT(*) FROM LabsumitIn  WHERE Submit= @submit and sample = @sample");
                if (Convert.ToInt32(oLabSub.Exist_DB(sqlbuscar, submit, sample, "@submit", "@sample")) > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                throw new Exception("Save error LabSubmitIn. " + ex.Message); ;
            }
        }

        private bool ValidateLabsumitPather(string submit)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(@"Server=10.10.203.4\gcg;uid=sa;pwd=BdZandor123*;database=GZC");
                string sqlbuscar = String.Format("SELECT COUNT(*) FROM LabsumitIn  WHERE Submit= @submit");
                SqlCommand cmd = new SqlCommand(sqlbuscar, cnn);
                cmd.Parameters.AddWithValue("@submit ", submit);
                cnn.Open();
                int Count = Convert.ToInt32(cmd.ExecuteScalar());
                cnn.Close();
                if (Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Save error LabSubmitIn. " + ex.Message); ;
            }
        }

        private bool ValidaSiExisteLabResult(string jobno, string Submit)
        {
            try
            {
                string sqlbuscar = String.Format("SELECT COUNT(*) FROM LabResult WHERE  Submit = @Submit and jobno=@jobno");

                if (Convert.ToInt32(oLabSub.Exist_DB(sqlbuscar, jobno, Submit, "@jobno", "@Submit")) > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                throw new Exception("Save error LabSubmitIn. " + ex.Message); ;
            }
        }

        internal void cargarenlabresult(string ordentrabajo, string envio, DateTime fecha)
        {
            //SqlConnection objconexion = OpenConexion(@"Server=10.10.203.4\gcg;uid=sa;pwd=BdZandor123*;database=GZC");
            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

            string valueCommandText = string.Empty;

            string valueDate = string.Concat(fecha.Day, "/0", fecha.Month, "/", fecha.Year, " ", "00:00:00");

            if ((ValidaSiExisteLabResult(ordentrabajo, envio)))
                valueCommandText = "UPDATE  LabResult  SET   jobno ='" + @ordentrabajo + "', DateReport ='" + valueDate + "', lab='" + "GZC" + "'  WHERE submit='" + envio + "'";
            else
                valueCommandText = "INSERT INTO LabResult (lab, jobno,submit,DateReport)VALUES('" + "GZC" + "','" + ordentrabajo + "','" + envio + "','" + valueDate + "'  )";

            oLabSub.alterdataBase(valueCommandText);
        }

        private void QCReport(string _sJobNomber)
        {
            try
            {
                DataTable dtQcReport = oRf.getQCReport(_sJobNomber);
                //QCReport
                Excel.Application oXL;
                Excel._Workbook oWB;
                Excel._Worksheet oSheet;
                Excel.Range oRng;

                oXL = new Excel.Application();
                //oXL.Visible = true;
                oWB = oXL.Workbooks.Open(ConfigurationSettings.AppSettings["Ruta_QCReport"].ToString(), 0, true, 5,
                Type.Missing, Type.Missing, false, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, null, null);

                oSheet = (Excel._Worksheet)oWB.Sheets[1];

                if (dtQcReport.Rows.Count > 0)
                {
                    oSheet.Cells[2, 5] = dtQcReport.Rows[0]["submit"].ToString();
                    oSheet.Cells[3, 5] = dtQcReport.Rows[0]["DateShipment"].ToString();
                    oSheet.Cells[4, 5] = dtQcReport.Rows.Count.ToString();

                    oSheet.Cells[2, 9] = dtQcReport.Rows[0]["lab"].ToString();
                    oSheet.Cells[2, 11] = dtQcReport.Rows[0]["jobno"].ToString();

                    var culturaCol = CultureInfo.GetCultureInfo("es-CO");
                    DateTime dateReporte = Convert.ToDateTime(dtQcReport.Rows[0]["DateReport"].ToString(), culturaCol);

                    oSheet.Cells[3, 11] = dateReporte.ToString("dd/MM/yyyy HH:mm");
                    oSheet.Cells[4, 11] = dtQcReport.Rows[0]["DifDate"].ToString();

                    //int iTotalMuestras = 0;
                    int iInicial = 9;

                    if (!dtQcReport.Columns.Contains("DupDe"))
                        for (int i = 0; i < dtQcReport.Rows.Count; i++)
                        {

                            oSheet.Cells[iInicial, 1] = dtQcReport.Rows[i]["HoleID"].ToString();//Hole
                            oSheet.Cells[iInicial, 2] = dtQcReport.Rows[i]["From"].ToString();//From
                            oSheet.Cells[iInicial, 3] = dtQcReport.Rows[i]["To"].ToString();//To
                            oSheet.Cells[iInicial, 4] = dtQcReport.Rows[i]["Sample"].ToString();//Sample
                            oSheet.Cells[iInicial, 5] = dtQcReport.Rows[i]["SampleType"].ToString();//Type



                            oSheet.Cells[iInicial, 6] = dtQcReport.Rows[i]["aufaaa"].ToString();//Au ppm
                            oSheet.Cells[iInicial, 7] = dtQcReport.Rows[i]["aufagr"].ToString();//AuGr ppm
                            oSheet.Cells[iInicial, 8] = dtQcReport.Rows[i]["agfa"].ToString();//Ag ppm
                            oSheet.Cells[iInicial, 9] = dtQcReport.Rows[i]["agfagr"].ToString();//AgGr ppm
                            oSheet.Cells[iInicial, 10] = dtQcReport.Rows[i]["CertifiedValue_ppm"].ToString();//Qc ppm

                            if (dtQcReport.Rows[i]["+3st"].ToString() != "")
                            {
                                oSheet.Cells[iInicial, 11] = dtQcReport.Rows[i]["QCAu"].ToString();

                                oSheet.Cells[iInicial, 12] = dtQcReport.Rows[i]["AgCertifiedValue_ppm"].ToString();
                                oSheet.Cells[iInicial, 13] = dtQcReport.Rows[i]["QCAg"].ToString();

                            }

                            oSheet.Cells[iInicial, 14] = dtQcReport.Rows[i]["Comments"].ToString();
                            iInicial += 1;

                            //No apto para usuario Adriana
                            //if (dtQcReport.Columns.Contains("DupDe"))
                            //oSheet.Cells[iInicial, 6] = dtQcReport.Rows[0]["DupDe"].ToString();

                            //oSheet.Cells[iInicial, 7] = dtQcReport.Rows[i]["aufaaa"].ToString();//Au ppm
                            //oSheet.Cells[iInicial, 8] = dtQcReport.Rows[i]["aufagr"].ToString();//AuGr ppm
                            //oSheet.Cells[iInicial, 9] = dtQcReport.Rows[i]["agfa"].ToString();//Ag ppm
                            //oSheet.Cells[iInicial, 10] = dtQcReport.Rows[i]["agfagr"].ToString();//AgGr ppm
                            //oSheet.Cells[iInicial, 11] = dtQcReport.Rows[i]["CertifiedValue_ppm"].ToString();//Qc ppm

                            //if (dtQcReport.Rows[i]["+3st"].ToString() != "")
                            //{
                            //    oSheet.Cells[iInicial, 12] = dtQcReport.Rows[i]["QCAu"].ToString();

                            //    oSheet.Cells[iInicial, 13] = dtQcReport.Rows[i]["AgCertifiedValue_ppm"].ToString();
                            //    oSheet.Cells[iInicial, 14] = dtQcReport.Rows[i]["QCAg"].ToString();

                            //}

                            //oSheet.Cells[iInicial, 15] = dtQcReport.Rows[i]["Comments"].ToString();
                            //iInicial += 1;

                        }
                    else
                        for (int i = 0; i < dtQcReport.Rows.Count; i++)
                        {

                            oSheet.Cells[iInicial, 1] = dtQcReport.Rows[i]["HoleID"].ToString();//Hole
                            oSheet.Cells[iInicial, 2] = dtQcReport.Rows[i]["From"].ToString();//From
                            oSheet.Cells[iInicial, 3] = dtQcReport.Rows[i]["To"].ToString();//To
                            oSheet.Cells[iInicial, 4] = dtQcReport.Rows[i]["Sample"].ToString();//Sample
                            oSheet.Cells[iInicial, 5] = dtQcReport.Rows[i]["SampleType"].ToString();//Type

                            if (dtQcReport.Columns.Contains("DupDe"))
                                oSheet.Cells[iInicial, 6] = dtQcReport.Rows[0]["DupDe"].ToString();

                            oSheet.Cells[iInicial, 7] = dtQcReport.Rows[i]["aufaaa"].ToString();//Au ppm
                            oSheet.Cells[iInicial, 8] = dtQcReport.Rows[i]["aufagr"].ToString();//AuGr ppm
                            oSheet.Cells[iInicial, 9] = dtQcReport.Rows[i]["agfa"].ToString();//Ag ppm
                            oSheet.Cells[iInicial, 10] = dtQcReport.Rows[i]["agfagr"].ToString();//AgGr ppm
                            oSheet.Cells[iInicial, 11] = dtQcReport.Rows[i]["CertifiedValue_ppm"].ToString();//Qc ppm

                            if (dtQcReport.Rows[i]["+3st"].ToString() != "")
                            {
                                oSheet.Cells[iInicial, 12] = dtQcReport.Rows[i]["QCAu"].ToString();
                                oSheet.Cells[iInicial, 13] = dtQcReport.Rows[i]["AgCertifiedValue_ppm"].ToString();
                                oSheet.Cells[iInicial, 14] = dtQcReport.Rows[i]["QCAg"].ToString();
                            }

                            oSheet.Cells[iInicial, 15] = dtQcReport.Rows[i]["Comments"].ToString();
                            iInicial += 1;
                        }
                }
                else if (dtQcReport.Rows.Count == 0)
                {
                    MessageBox.Show("There is no sa4ples in DHSamples", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                oXL.Visible = true;
                oXL.UserControl = true;

            }
            catch (Exception ex)
            {
                if (ex.Message == "No se puede encontrar la tabla 0.")
                {
                    MessageBox.Show("No hay datos que mostrar en el QC");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }

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
            try
            {
                OpenFileDialog result = new OpenFileDialog();
                result.Title = "Seleccionar archivos";
                result.Filter = "Todos los archivos (*.xlsx)|*.xls;*.xlsx;*.csv;*.txt";
                result.Multiselect = false;
                result.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
                if (result.ShowDialog() == DialogResult.OK)
                {
                    txtRuta.Text = result.FileName;
                    label2.Enabled = true;
                }
                if (txtRuta.Text == "")
                {

                }
                else
                {
                    try
                    {
                        Cargar(dgXls, txtRuta.Text);
                        label2.Enabled = false;
                        FileInfo oFi = new FileInfo(result.FileName);

                        string sExt = oFi.Extension.ToString();
                        sNomArchivo = oFi.Name.Substring(0, oFi.Name.ToString().Length - sExt.ToString().Length);

                    }
                    catch (OleDbException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                QCReport(cmbJobNo.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                QCReport(cmbJobNo.Text.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (dgXls.DataSource == null)
            {
                MessageBox.Show("Cargue el archivo reporte de análisis químico.");
                return;
            }
            label2.Visible = true;

            try
            {
                string envioNoEncontrados = string.Empty;
                string codEnvio = string.Empty;
                //variables de insercion
                string contextValue = string.Empty;

                using (ExcelPackage paquete = new ExcelPackage())
                {
                    // Creamos un flujo a partir del archivo de Excel, y lo cargamos en el paquete
                    using (FileStream flujo = File.OpenRead(txtRuta.Text))
                    {
                        paquete.Load(flujo);
                    }
                    // Obtenemos la primera hoja del documento
                    ExcelWorksheet hoja1 = paquete.Workbook.Worksheets.First();

                    string envio = hoja1.Cells[12, 2].Text.Trim();
                    codEnvio = hoja1.Cells[11, 2].Text.Trim();

                    DateTime fecha = Convert.ToDateTime((hoja1.Cells[16, 2].Text.Trim()));

                    // Empezamos a leer a partir de la segunda fila
                    bool indicate = false;
                    for (int numFila = 46; numFila <= 200; numFila++)
                    {
                        string tenor = string.Empty;
                        string tenorufagr = string.Empty;
                        string tenorag = string.Empty;
                        string aufagr = string.Empty;

                        string muestra = Convert.ToString(hoja1.Cells[numFila, 1].Text);

                        if (!muestra.Trim().ToUpper().Contains("DUPLIC"))
                        {
                            if (Convert.ToString(hoja1.Cells[numFila, 2].Text) == "<0.02")
                                tenor = "0.01";
                            else
                            {
                                if (hoja1.Cells[numFila, 2].Text.Trim() == ">10" || hoja1.Cells[numFila, 2].Text.Trim() == ">10.0" || hoja1.Cells[numFila, 2].Text.ToString().Trim() == "˃10.00")

                                {
                                    tenor = "10.001";
                                    aufagr = Convert.ToString(hoja1.Cells[numFila, 3].Text);
                                }
                                else
                                    tenor = Convert.ToString(hoja1.Cells[numFila, 2].Text);
                            }

                            tenorufagr = Convert.ToString(hoja1.Cells[numFila, 3].Text);

                            if (!String.IsNullOrEmpty(hoja1.Cells[numFila, 1].Text))
                            {
                                if (!indicate)
                                    if (!ValidateLabsumitPather(envio))
                                    {
                                        MessageBox.Show("EnvÍo no encontrado en base de datos VERIFICAR EXCEL!");
                                        return;
                                    }
                                    else
                                        indicate = true;

                                if (ValidateLabsumit(envio, muestra))
                                {
                                    if (ValidaSiExiste(codEnvio, muestra))
                                    {
                                        contextValue = "UPDATE  Assay SET Aufaaa = '" + tenor + "'  ,  Aglab = '" + tenorag + "' , Agfa = '" + tenorag + "' , aufagr = '" + tenorufagr + "' WHERE jobno= '" + codEnvio + "'  and sample= '" + muestra + "'   ";
                                    }
                                    else
                                    {
                                        contextValue = "INSERT INTO Assay (jobno,sample,Aufaaa,aufagr, qaqc , Aglab , Agfa )VALUES('" + codEnvio + "','" + muestra + "' ,'" + tenor + "' ,'" + tenorufagr + "' , 1 , '" + tenorag + "' , '" + tenorag + "' )";
                                    }

                                    oLabSub.alterdataBase(contextValue);
                                }
                                else
                                {
                                    if (!String.IsNullOrEmpty(envioNoEncontrados) && !envioNoEncontrados.Contains("Blanco") && !envioNoEncontrados.Contains("STD") && !envioNoEncontrados.Contains("Dup") && !envioNoEncontrados.Contains("Duplic"))

                                        envioNoEncontrados = string.Concat(envioNoEncontrados, muestra, ",");
                                    else
                                        envioNoEncontrados = string.Concat(muestra, ",", envioNoEncontrados);
                                }
                            }
                        }
                    }

                    cargarenlabresult(codEnvio, envio, fecha);

                    Clear();
                }
                if (String.IsNullOrEmpty(envioNoEncontrados))
                    MessageBox.Show("Importacion Finalizada con exito");
                else
                    MessageBox.Show(string.Concat("Importacion Finalizada con Detalles!", Environment.NewLine, "Muestras no enviadas:", Environment.NewLine, envioNoEncontrados.TrimEnd(',')));

                label2.Visible = false;
                QCReport(codEnvio);
                pbtnUpdate.Enabled = false;
                limpiar();
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        public void limpiar()
        {
            dgXls.DataSource = null;
            txtRuta.Text = string.Empty;
        }

        private void cmbJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sqlbuscar = String.Format("SELECT LabResult.Submit,Labsumit.SampleType FROM LabResult INNER JOIN Labsumit ON LabResult.Submit = Labsumit.Submit WHERE JOBNO = @jobs");
            DataAccess.ManagerDA cal = new DataAccess.ManagerDA();
            SqlParameter[] arr = cal.GetParameters(1);
            arr[0].ParameterName = "@jobs";
            arr[0].Value = cmbJobNo.SelectedValue;
            DataSet ds = new DataSet();
            ds = cal.ExecuteDataset(sqlbuscar, arr, CommandType.Text);

            if (ds.Tables.Count > 0)

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtSubmit.Text = ds.Tables[0].Rows[0][0].ToString().Trim();
                    comboBox1.Text = ds.Tables[0].Rows[0][1].ToString().Trim();
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSubmit.Text.Trim()) && comboBox1.SelectedItem != null)
            {
                string sqlActualizar = String.Format("update Labsumit set Labsumit.SampleType='" + comboBox1.SelectedItem + "' where Labsumit.Submit='" + txtSubmit.Text + "'");

                oLabSub.alterdataBase(sqlActualizar);

                MessageBox.Show("Actualización realizada con Exito!");

                txtSubmit.Text = string.Empty;
                comboBox1.SelectedIndex = -1;
            }
        }

        public void EjecutaPasarDato(string Dato)
        {
            cmbJobNo.Text = Dato;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            frmSearchId FrmConsultas = new frmSearchId(1);
            FrmConsultas.Pasado += new frmSearchId.PasarDatoSeleccionado(EjecutaPasarDato);
            FrmConsultas.ShowDialog();
        }
    }
}
