using Spire.Xls;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

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
                //DataTable dtXls = new DataTable();
                Workbook workbook = new Workbook();

                workbook.LoadFromFile(txtRuta.Text);
                Worksheet sheet = workbook.Worksheets[0];
                dtXls = sheet.ExportDataTable();
                dsCarga.Tables.Add(dtXls);
                dgXls.DataSource = dtXls;
                dgXls.AutoResizeColumns();

                dtElementos = new DataTable();
                //Capturo la posicion de la tabla donde debo empezar
                int iPosicionElem = 0;
                for (int a = 0; a < dtXls.Rows.Count; a++)
                {
                    if (dtXls.Rows[a][0].ToString() == "SAMPLE")
                    {
                        iPosicionElem = a;
                    }
                }

                //Genero las columnas del nuevo datatable
                for (int i = 0; i < dtXls.Columns.Count; i++)
                {
                    if (dtXls.Rows[iPosicionElem][i].ToString() != "")
                    {
                        dtElementos.Columns.Add(dtXls.Rows[iPosicionElem][i].ToString() + '*' + i.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Prepare the file before load", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                //Lleno los registros del nuevo datatable
                for (int a = iPosicionElem + 1; a < dtXls.Rows.Count; a++)
                {
                    drElem = dtElementos.NewRow();
                    int irElem = 0; //Variable para el manejo de poblar solo las columnas que tengan titulo
                    for (int iElem = 0; iElem < dtXls.Columns.Count; iElem++)
                    {
                        if (dtXls.Rows[iPosicionElem][iElem].ToString() != "")
                        {
                            string sCampo = "";
                            if (dtXls.Rows[a][iElem].ToString() != "--")
                            {
                                sCampo = dtXls.Rows[a][iElem].ToString();
                            }

                            drElem[dtElementos.Columns[irElem].Caption] = sCampo.ToString();
                            irElem += 1;
                        }
                    }

                    dtElementos.Rows.Add(drElem);
                    pbtnUpdate.Enabled = true;
                }
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
            LoadXML();
            dtXls = new DataTable();
            dtElementos = new DataTable();
            txtRuta.Text = string.Empty;
            label2.Visible = false;
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
                int countSamples = 0;

                string sCampos = "";
                string sCamposVal = "";
                string sSQL = "";
                var culturaCol = CultureInfo.GetCultureInfo("es-CO");

                string[] envioExtraido = dtXls.Columns[0].Caption.ToString().Split('-');
                envio = envioExtraido[0].Trim();

                string[] fechaExtraida;
                DateTime fecha;

                string[] codEnvioExtraido;
                string[] cantExtraido;

                fechaExtraida = dsCarga.Tables[1].Rows[2][0].ToString().Split(' ');
                fecha = Convert.ToDateTime(fechaExtraida[8]);

                codEnvioExtraido = dsCarga.Tables[1].Rows[5][0].ToString().Split(' ');
                codEnvio = codEnvioExtraido[3];

                cantExtraido = dsCarga.Tables[1].Rows[1][0].ToString().Split(' ');
                countSamples = Convert.ToInt32(cantExtraido[4].Trim());


                sLab = "ALS Global";
                DateTime dateReport = Convert.ToDateTime(fecha.ToString(), culturaCol);
                string valueDate = string.Concat(dateReport.Day, "/", dateReport.Month, "/", DateTime.Now.Year, " 00:00");


                sEnvio = envio.Trim();
                sSample = codEnvio.Trim();
                sQaqc = ConfigurationSettings.AppSettings["Qaqc"].ToString();

                oAssay.sJob = sEnvio;
                oAssay.AssayLabresult_Delete();

                string sInsertLabRes = "";
                sInsertLabRes += "Insert into LabResult (Submit, Jobno, CantSample, Lab, DateReport) Values ('" +
                    sSample + "','" + sEnvio + "'," + countSamples + ",'" + sLab + "','" + valueDate + "')";


                DataTable dtLabSInterval = new DataTable();
                clsLabSumitInterval oLabSInterval = new clsLabSumitInterval();

                oLabSInterval.sOpcion = "1";
                oLabSInterval.sSubmit = sSample;
                dtLabSInterval = oLabSInterval.getLabSubmitInterval();

                if (dtLabSInterval.Rows.Count == 0)
                {
                    MessageBox.Show("El envío '" + sEnvio + "' No existe en la tabla 'labsumit'");
                    return;
                }

                string sResp = oRf.ExecSQL(sInsertLabRes);

                if (sResp != "OK")
                {
                    MessageBox.Show(sResp.ToString());
                    return;
                }

                oRf.InsertTrans("LabResult", "Insert", clsRf.sUser.ToString(),
                                "Submit = " + sEnvio +
                                ". Jobno = " + sSample +
                                ". CantSample = " + countSamples +
                                ". Lab = " + sLab +
                                ". DateReport = " + valueDate);

                oLabSub.sOpcion = "1";
                oLabSub.sSubmit = sSample.ToString();
                DataTable dtType = oLabSub.getLabSubmit();
                if (dtType.Rows.Count > 0)
                {
                    sType = dtType.Rows[0]["SampleType"].ToString();
                }
                else { sType = ""; }

                int iPosicionFin = 0;
                iPosicionFin = dtElementos.Rows.Count;

                int iAg = 0, iAu = 0, iS = 0;
                string stAg = "", stAu = "", stS = "";
                string textoAgfa = string.Empty;

                for (int iRowElem = 1; iRowElem < iPosicionFin; iRowElem++)
                {
                    iAg = 0; iAu = 0; iS = 0;
                    stAg = ""; stAu = ""; stS = "";

                    for (int iElem = 0; iElem < dtElementos.Columns.Count; iElem++)
                    {
                        string[] words = dtElementos.Columns[iElem].Caption.ToString().Split('*');

                        if (words[0].ToString().Equals("Recvd Wt."))
                        {
                            words[0] = "WEIGHT";
                        }

                        var dtrow = dsCarga.Tables[0].Select("IdLab='" + words[0].ToString() + "'");
                        if (dtrow.Length > 0)
                        {
                            bool bConv = false;
                            string sVal = dtElementos.Rows[iRowElem][iElem].ToString();

                            if (words[0].ToUpper() == "AU")
                            {
                                string sAu = "Aufaaa";
                                iAu++;

                                if (iAu > 1)
                                {
                                    sAu = "Aufagr";
                                }

                                if (sVal.ToString() == ">10.0")
                                {
                                    sVal = "10.001";
                                }

                                if (sVal.ToString() == "<0.005")
                                {
                                    sVal = "0.003";
                                }


                                sCampos += ",[" + sAu + "]";
                                sCamposVal += ",'" + sVal.ToString() + "'";
                            }
                            else if (words[0].ToUpper() == "AG")
                            {
                                string sAg = "Agfa";
                                iAg++;

                                if (iAg > 1) //Si esta mas de una vez el elemento
                                {
                                    sAg = "Agfagr"; //Se guarda el dato en el segundo campo en DB
                                }

                                if (sVal.ToString() == "<0.2")
                                {
                                    sVal = "0.1";
                                }

                                if (sVal.ToString() == ">100")
                                {
                                    sVal = "101";
                                }

                                stAg = dtElementos.Rows[0][iElem].ToString();
                                sCampos += ",[" + sAg + "]";
                                sCamposVal += ",'" + sVal.ToString() + "'";
                            }

                            else if (words[0].ToUpper() == "WEIGHT")
                            {
                                string sS = "Weight";

                                if (sVal.ToString() == "<0.02")
                                {
                                    sVal = "0.01";
                                }

                                sCampos += ",[" + sS + "]";
                                sCamposVal += ",'" + sVal.ToString() + "'";
                            }

                            bConv = false;
                        }
                    }


                    /*  Implementar logica para actualizar el campo qaqc solo si la muestra 
                      no ha sido reenviada */

                    clsLabsumitIn oLIn = new clsLabsumitIn();
                    oLIn.sSample = dtElementos.Rows[iRowElem][0].ToString().Trim();
                    DataTable dtLIn = oLIn.getLabSubmitInBySampleReAssay();

                    if (!dtElementos.Rows[iRowElem][0].ToString().Trim().Contains("*DUP"))
                    {
                        sSQL = "Insert into Assay (JobNo, [Sample], Qaqc, [Type] " +
                            sCampos + ") Values ('" + sEnvio + "','" + dtElementos.Rows[iRowElem][0].ToString().Trim() +
                            "','" + sQaqc + "','" + sType + "'" + sCamposVal + ")";
                        string sRespAss = oRf.ExecSQL(sSQL);

                        if (sRespAss != "OK")
                        {
                            //Implemento una especie de rollback para el archivo que se intento cargar
                            sSQL = "Delete from Assay Where JobNo = '" + sNomArchivo.ToString() + "'";
                            oRf.ExecSQL(sSQL);
                            sSQL = "Delete from LabResult Where Jobno = '" + sNomArchivo.ToString() + "'";
                            oRf.ExecSQL(sSQL);

                            MessageBox.Show(sRespAss.ToString());
                            return;
                        }


                        //Insertar el registro para el historial de transacciones por usuario
                        oRf.InsertTrans("Assay", "Insert", clsRf.sUser.ToString(),
                                        "JobNo = " + sEnvio +
                                        ". [Sample] = " + dtElementos.Rows[iRowElem][0].ToString().Trim() +
                                        ". Qaqc = " + sQaqc +
                                        ". [Type] = " + sType +
                                        ". Elementos = " + sCampos + ". Valores elementos:" + sCamposVal);

                    }
                    sCampos = "";
                    sCamposVal = "";

                }
                MessageBox.Show("Successful", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                QCReport(sEnvio);
                pbtnUpdate.Enabled = false;
                limpiar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                label2.Visible = false;
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

        private void frmCargaAnalisisAls_Load(object sender, EventArgs e)
        {

        }
    }
}
