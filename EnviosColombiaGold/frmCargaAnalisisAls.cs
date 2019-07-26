using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Spire.Xls;
using System.Configuration;
using System.Globalization;

namespace EnviosColombiaGold
{
    public partial class frmCargaAnalisisAls : Form
    {
        private DataTable dtXls = new DataTable();
        private DataTable dtElementos = new DataTable();
        private DataRow drElem;
        private DataSet dsCarga = new DataSet();
        private string sLab;
        private string sEnvio;
        private string sSample;
        private string sQaqc;
        private string sType;
        private string sNomArchivo;
        private string sDateRep;
        private DataTable dtQCConversion = new DataTable();
        private clsRf oRf = new clsRf();
        private clsLabSubmit oLabSub = new clsLabSubmit();
        private clsAssay oAssay = new clsAssay();

        #region Variables Globales
        private SqlConnection conn = new SqlConnection();
        public string codEnvio { get; set; }
        #endregion Variables Globales

        public frmCargaAnalisisAls()
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
            dsCarga = new DataSet();
            // Create new FileStream with which to read the schema.
            System.IO.FileStream fsReadXml = new System.IO.FileStream
                (myXMLfile, System.IO.FileMode.Open);
            try
            {
                dsCarga.ReadXml(fsReadXml);
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

        private void oDialog_FileOk(object sender, CancelEventArgs e)
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

        private void tMessage_Popup(object sender, PopupEventArgs e)
        {

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

                CargaExcelFormaSinProvider(result);
                label2.Visible = false;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private void CargaExcelFormaSinProvider(OpenFileDialog openFileDialog)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                label2.Visible = true;
                txtRuta.Text = openFileDialog.FileName;
                FileInfo oFi = new FileInfo(openFileDialog.FileName);
                string sExt = oFi.Extension.ToString();
                sNomArchivo = oFi.Name.Substring(0, oFi.Name.ToString().Length - sExt.ToString().Length);

                Excel.Application oExc = new Excel.Application();
                oExc.Workbooks.Open(txtRuta.Text, 0, true, 5, Type.Missing, Type.Missing, false, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, null, null);
                oExc.Quit();
            }
            try
            {
                DataTable dtXls = new DataTable();
                Workbook workbook = new Workbook();

                workbook.LoadFromFile(txtRuta.Text);
                Worksheet sheet = workbook.Worksheets[0];
                dtXls = sheet.ExportDataTable();
                dsCarga.Tables.Add(dtXls);
                dgXls.DataSource = dtXls;
                dgXls.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        public void limpiar()
        {
            dgXls.DataSource = null;
            txtRuta.Text = string.Empty;
        }

        private void pbtnUpdate_Click(object sender, EventArgs e)
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
                string envio = string.Empty;
                //variables de insercion
                string contextValue = string.Empty;

                string[] envioExtraido = dsCarga.Tables[1].Rows[0][0].ToString().Split('-');
                envio = envioExtraido[0].Trim();

                string[] fechaExtraida = dsCarga.Tables[1].Rows[3][0].ToString().Split(' ');
                DateTime fecha = Convert.ToDateTime(fechaExtraida[8]);

                string[] codEnvioExtraido = dsCarga.Tables[1].Rows[6][0].ToString().Split(' ');
                codEnvio = codEnvioExtraido[3];

                bool indicate = false;
                for (int numFila = 10; numFila <= dsCarga.Tables[1].Rows.Count -1; numFila++)
                {
                    string tenorAufaaa = string.Empty;
                    string tenorAgfa = string.Empty;
                    string tenorAufagr = string.Empty;
                    string aufAgfagr = string.Empty;
                    string weight = string.Empty;
                    string muestra = string.Empty;

                    if (dsCarga.Tables[1].Rows[numFila][0].ToString() != string.Empty)
                    {
                        muestra = dsCarga.Tables[1].Rows[numFila][0].ToString();
                    }

                    if (dsCarga.Tables[1].Rows[numFila][1].ToString() != string.Empty)
                    {
                        weight = dsCarga.Tables[1].Rows[numFila][1].ToString();
                    }

                    if (dsCarga.Tables[1].Rows[numFila][2].ToString() == "<0.2")
                    {
                        tenorAufaaa = "0.1";
                    }
                    else
                    {
                        if (dsCarga.Tables[1].Rows[numFila][2].ToString() == ">10"
                            || dsCarga.Tables[1].Rows[numFila][2].ToString() == ">10.0"
                            || dsCarga.Tables[1].Rows[numFila][2].ToString() == "˃10.00")
                        {
                            tenorAufaaa = "10.001";
                        }                       
                        else
                        {
                            tenorAufaaa = dsCarga.Tables[1].Rows[numFila][2].ToString();
                        }
                    }

                    if (dsCarga.Tables[1].Rows[numFila][4].ToString() == "<0.2")
                    {
                        tenorAgfa = "0.1";
                    }
                    else
                    {
                        if (dsCarga.Tables[1].Rows[numFila][4].ToString() == ">10"
                            || dsCarga.Tables[1].Rows[numFila][4].ToString() == ">10.0"
                            || dsCarga.Tables[1].Rows[numFila][4].ToString() == "˃10.00")
                        {
                            tenorAgfa = "10.001";
                        }
                        else if (dsCarga.Tables[1].Rows[numFila][4].ToString() == ">100")
                        {
                            tenorAgfa = "100.1";
                        }
                        else
                        {
                            tenorAgfa = dsCarga.Tables[1].Rows[numFila][4].ToString();
                        }
                    }

                    if (dsCarga.Tables[1].Rows[numFila][5].ToString() == "<0.2")
                    {
                        tenorAufagr = "0.1";
                    }
                    else
                    {
                        if (dsCarga.Tables[1].Rows[numFila][5].ToString() == ">10"
                            || dsCarga.Tables[1].Rows[numFila][5].ToString() == ">10.0"
                            || dsCarga.Tables[1].Rows[numFila][5].ToString() == "˃10.00")
                        {
                            tenorAufagr = "10.001";
                        }
                        else
                        {
                            tenorAufagr = dsCarga.Tables[1].Rows[numFila][5].ToString();
                        }
                    }


                    if (dsCarga.Tables[1].Rows[numFila][7].ToString() == "<0.2")
                    {
                        aufAgfagr = "0.1";
                    }
                    else
                    {
                        if (dsCarga.Tables[1].Rows[numFila][7].ToString() == ">10"
                            || dsCarga.Tables[1].Rows[numFila][7].ToString() == ">10.0"
                            || dsCarga.Tables[1].Rows[numFila][7].ToString() == "˃10.00")
                        {
                            aufAgfagr = "10.001";
                        }
                        else
                        {
                            aufAgfagr = dsCarga.Tables[1].Rows[numFila][7].ToString();
                        }
                    }

                    if (!indicate)
                    {
                        if (!ValidateLabsumitPather(codEnvio))
                        {
                            MessageBox.Show("EnvÍo no encontrado en base de datos VERIFICAR EXCEL!");
                            return;
                        }
                        else
                        {
                            indicate = true;
                        }
                    }

                    if (ValidateLabsumit(codEnvio, muestra))
                    {
                        if (ValidaSiExiste(envio, muestra))
                        {
                            contextValue = "UPDATE  Assay SET Aufaaa = '" + tenorAufaaa + "'  ,  Aufagr = '" + tenorAufagr
                                            + "' , Agfa = '" + tenorAgfa + "' , Agfagr = '" + aufAgfagr
                                            + "' , Weight = '" + weight  + ", qaqc = 1"
                                            + "' WHERE jobno= '" + codEnvio + "'  and sample= '" + muestra + "'   ";
                        }
                        else
                        {
                            contextValue = "INSERT INTO Assay (jobno, sample, Aufaaa, Aufagr, qaqc , Agfagr , Agfa,  Weight) VALUES('"
                                                             + codEnvio + "','"
                                                             + muestra + "' ,'"
                                                             + tenorAufaaa + "' ,'"
                                                             + tenorAufagr + "' " +
                                                             ", 1, '"
                                                             + aufAgfagr + "' ,'"
                                                             + tenorAgfa + "' ,'"
                                                             + weight + "' )";
                        }

                        oLabSub.alterdataBase(contextValue);
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(envioNoEncontrados) && !envioNoEncontrados.Contains("Blanco") && !envioNoEncontrados.Contains("STD") && !envioNoEncontrados.Contains("Dup") && !envioNoEncontrados.Contains("Duplic"))
                        {
                            envioNoEncontrados = string.Concat(envioNoEncontrados, muestra, ",");
                        }
                        else
                        {
                            envioNoEncontrados = string.Concat(muestra, ",", envioNoEncontrados);
                        }
                    }

                }

                cargarenlabresult(codEnvio, envio, fecha);
                Clear();

                if (String.IsNullOrEmpty(envioNoEncontrados))
                {
                    MessageBox.Show("Importacion Finalizada con exito");
                }
                else
                {
                    MessageBox.Show(string.Concat("Importacion Finalizada con Detalles!", Environment.NewLine, "Muestras no enviadas:", Environment.NewLine, envioNoEncontrados.TrimEnd(',')));
                }

                label2.Visible = false;
                QCReport(envio);
                pbtnUpdate.Enabled = false;
                limpiar();

            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool ValidaSiExiste(string jobno, string sample)
        {
            try
            {
                string sqlbuscar = String.Format("SELECT COUNT(*) FROM Assay  WHERE jobno = @jobno and sample = @sample");

                if (Convert.ToInt32(oLabSub.Exist_DB(sqlbuscar, jobno, sample, "@jobno", "@sample")) > 0)
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

        private bool ValidateLabsumit(string submit, string sample)
        {
            try
            {
                string sqlbuscar = String.Format("SELECT COUNT(*) FROM LabsumitIn  WHERE Submit= @submit and sample = @sample");
                if (Convert.ToInt32(oLabSub.Exist_DB(sqlbuscar, submit, sample, "@submit", "@sample")) > 0)
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


        public void Clear()
        {
            txtRuta.Text = string.Empty;
            dgXls.DataSource = null;
        }

        private void cmbJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sqlbuscar = String.Format("SELECT LabResult.Submit, Labsumit.SampleType FROM LabResult INNER JOIN Labsumit ON LabResult.Submit = Labsumit.Submit WHERE JOBNO = @jobs");
            DataAccess.ManagerDA cal = new DataAccess.ManagerDA();
            SqlParameter[] arr = cal.GetParameters(1);
            arr[0].ParameterName = "@jobs";
            arr[0].Value = cmbJobNo.SelectedValue;
            DataSet ds = new DataSet();
            ds = cal.ExecuteDataset(sqlbuscar, arr, CommandType.Text);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtSubmit.Text = ds.Tables[0].Rows[0][0].ToString().Trim();
                    comboBox1.Text = ds.Tables[0].Rows[0][1].ToString().Trim();
                }
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

                    oSheet.Cells[2, 9] = dtQcReport.Rows[0]["Lab"].ToString();
                    oSheet.Cells[2, 11] = dtQcReport.Rows[0]["Jobno"].ToString();

                    var culturaCol = CultureInfo.GetCultureInfo("es-CO");
                    DateTime dateReporte = Convert.ToDateTime(dtQcReport.Rows[0]["DateReport"].ToString(), culturaCol);

                    oSheet.Cells[3, 11] = dateReporte.ToString("dd/MM/yyyy HH:mm");
                    oSheet.Cells[4, 11] = dtQcReport.Rows[0]["DifDate"].ToString();

                    //int iTotalMuestras = 0;
                    int iInicial = 9;

                    if (!dtQcReport.Columns.Contains("DupDe"))
                    {
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

                        }
                    }
                    else
                    {
                        for (int i = 0; i < dtQcReport.Rows.Count; i++)
                        {
                            oSheet.Cells[iInicial, 1] = dtQcReport.Rows[i]["HoleID"].ToString();//Hole
                            oSheet.Cells[iInicial, 2] = dtQcReport.Rows[i]["From"].ToString();//From
                            oSheet.Cells[iInicial, 3] = dtQcReport.Rows[i]["To"].ToString();//To
                            oSheet.Cells[iInicial, 4] = dtQcReport.Rows[i]["Sample"].ToString();//Sample
                            oSheet.Cells[iInicial, 5] = dtQcReport.Rows[i]["SampleType"].ToString();//Type

                            if (dtQcReport.Columns.Contains("DupDe"))
                            {
                                oSheet.Cells[iInicial, 6] = dtQcReport.Rows[0]["DupDe"].ToString();
                            }

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
                }
                else if (dtQcReport.Rows.Count == 0)
                {
                    MessageBox.Show("There is no samples in DHSamples", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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


        private bool ValidateLabsumitPather(string submit)
        {
            try
            {
                var coneccion = ConfigurationManager.ConnectionStrings["SqlProvider"].ConnectionString;
                SqlConnection cnn = new SqlConnection(coneccion);
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
                string sqlbuscar = String.Format("SELECT COUNT(*) FROM LabResult WHERE  Submit = @Submit and jobno = @jobno");

                if (Convert.ToInt32(oLabSub.Exist_DB(sqlbuscar, jobno, Submit, "@jobno", "@Submit")) > 0)
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

        internal void cargarenlabresult(string envio, string ordentrabajo, DateTime fecha)
        {
            string valueCommandText = string.Empty;
            string valueDate = string.Concat(fecha.Day, "/0", fecha.Month, "/", fecha.Year, " ", "00:00:00");

            if ((ValidaSiExisteLabResult(ordentrabajo, envio)))
            {
                valueCommandText = "UPDATE  LabResult  SET   jobno ='" + @ordentrabajo + "', DateReport ='" + valueDate + "', lab='" + "ALS" + "'  WHERE submit='" + envio + "'";
            }
            else
            {
                valueCommandText = "INSERT INTO LabResult (Lab, jobno, submit, DateReport)VALUES('ALS','" + ordentrabajo + "','" + envio + "','" + valueDate + "'  )";
            }

            oLabSub.alterdataBase(valueCommandText);
        }


    }
}
