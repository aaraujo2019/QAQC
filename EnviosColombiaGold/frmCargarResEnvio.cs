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
    public partial class frmCargarResEnvio : Form
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
        //private string sDateRep;
        private DataTable dtQCConversion = new DataTable();
        clsRf oRf = new clsRf();
        clsLabSubmit oLabSub = new clsLabSubmit();
        clsAssay oAssay = new clsAssay();

        public frmCargarResEnvio()
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
            FileStream fsReadXml = new System.IO.FileStream (myXMLfile, FileMode.Open);
            try
            {
                ds.ReadXml(fsReadXml);
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


        public DataTable getDataTableFromExcel(string path)
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets.Count;
                DataTable tbl = new DataTable();
                return tbl;
            }
        }


        private void btnAbrir_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string sCampos = "";
                string sCamposVal = "";
                string sSQL = "";
                var culturaCol = CultureInfo.GetCultureInfo("es-CO");
                sLab = dtXls.Columns[0].Caption.ToString();

                DateTime dateReport = Convert.ToDateTime(dtXls.Rows[7][1].ToString(), culturaCol);

                string valueDate = string.Concat(dateReport.Day, "/0", dateReport.Month, "/", dateReport.Year, " ", "00:00:00");
                sLab = dtXls.Columns[0].Caption.ToString();
                sEnvio = dtXls.Rows[8][1].ToString();
                sSample = dtXls.Rows[5][1].ToString().Trim();
                sQaqc = ConfigurationSettings.AppSettings["Qaqc"].ToString();

                string sInsertLabRes = "";
                sInsertLabRes += "Insert into LabResult (Submit, Jobno, CantSample, Lab, DateReport) Values ('" +
                    sEnvio + "','" + sNomArchivo + "'," + sSample.Trim() + ",'" + sLab + "','" + valueDate + "')";
                string sResp = oRf.ExecSQL(sInsertLabRes);

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
                {

                    for (int iElem = 0; iElem < dtElementos.Columns.Count; iElem++)
                    {
                        for (int iXml = 0; iXml < ds.Tables[0].Rows.Count; iXml++)
                        {
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

                                        case "AAS11B":
                                            sAg = "Agfagr";
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
                    oLIn.sSample = dtElementos.Rows[iRowElem][0].ToString().Trim();
                    DataTable dtLIn = oLIn.getLabSubmitInBySampleReAssay();
                    if (dtLIn.Rows.Count > 0)
                    {
                        sQaqc = "0";
                    }
                    if (!dtElementos.Rows[iRowElem][0].ToString().Trim().Contains("* DUP"))
                    {
                        sSQL = "Insert into Assay (JobNo, [Sample], Qaqc, [Type] " +
                            sCampos + ") Values ('" + sNomArchivo + "','" + dtElementos.Rows[iRowElem][0].ToString().Trim() +
                            "','" + sQaqc + "','" + sType + "'" + sCamposVal + ")";

                        string sRespAss = oRf.ExecSQL(sSQL);
                        MessageBox.Show(sRespAss);

                    }
                    sCampos = "";
                    sCamposVal = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string sCampos = "";
                string sCamposVal = "";
                string sSQL = "";
                var culturaCol = CultureInfo.GetCultureInfo("es-CO");

                sLab = dtXls.Columns[0].Caption.ToString();
                DateTime dateReport = Convert.ToDateTime(dtXls.Rows[7][1].ToString(), culturaCol);
                string valueDate = string.Concat(dateReport.Day, "/", dateReport.Month, "/", DateTime.Now.Year, " 00:00");


                sEnvio = dtXls.Rows[8][1].ToString();
                sSample = dtXls.Rows[5][1].ToString().Trim();
                sQaqc = ConfigurationSettings.AppSettings["Qaqc"].ToString();

                oAssay.sJob = sNomArchivo.ToString();
                oAssay.AssayLabresult_Delete();

                string sInsertLabRes = "";
                sInsertLabRes += "Insert into LabResult (Submit, Jobno, CantSample, Lab, DateReport) Values ('" +
                    sEnvio + "','" + sNomArchivo + "'," + sSample.Trim() + ",'" + sLab + "','" + valueDate + "')";


                DataTable dtLabSInterval = new DataTable();
                clsLabSumitInterval oLabSInterval = new clsLabSumitInterval();

                oLabSInterval.sOpcion = "1";
                oLabSInterval.sSubmit = sEnvio;
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
                //Insertar el registro para el historial de transacciones por usuario
                oRf.InsertTrans("LabResult", "Insert", clsRf.sUser.ToString(),
                                "Submit = " + sEnvio +
                                ". Jobno = " + sNomArchivo +
                                ". CantSample = " + sSample.Trim() +
                                ". Lab = " + sLab +
                                ". DateReport = " + valueDate);

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

                int iAg = 0, iAu = 0, iMn = 0, iFe = 0, iCa = 0, iZn = 0, iS = 0, iAs = 0, iPb = 0, iCu = 0;
                string stAg = "", stAu = "", stMn = "", stFe = "", stCa = "", stZn = "", stS = "", stAs = "", stPb = "", stCu = "";
                string textoAgfa = string.Empty;
                int valAgicp = 0;

                for (int iRowElem = 4; iRowElem < iPosicionFin; iRowElem++)
                {
                    iAg = 0; iAu = 0; iMn = 0; iFe = 0; iCa = 0; iZn = 0; iS = 0; iAs = 0; iPb = 0; iCu = 0;
                    stAg = ""; stAu = ""; stMn = ""; stFe = ""; stCa = ""; stZn = ""; stS = ""; stAs = ""; stPb = ""; stCu = "";

                    for (int iElem = 0; iElem < dtElementos.Columns.Count; iElem++)
                    {
                        string[] words = dtElementos.Columns[iElem].Caption.ToString().Split('*');
                        var dtrow = ds.Tables[0].Select("IdLab='" + words[0].ToString() + "'");
                        if (dtrow.Length > 0)
                        {
                            //Vble para determinar si se hizo una conversion sobre el dato
                            bool bConv = false;
                            string sVal = dtElementos.Rows[iRowElem][iElem].ToString();

                            DataRow[] dato = dtQCConversion.Select("Code = '" + dtElementos.Rows[iRowElem][iElem].ToString() + "' And Description like '%" + words[0].ToString() + "%'");
                            if (dato.Length > 0)
                            {
                                sVal = dato[0].ItemArray[2].ToString();


                                //El oro Au tiene un trato especial por que el resultado viene en ppb y no en ppm como los demas elementos
                                if (words[0].ToUpper() == "AU" &&
                                    dtElementos.Rows[0][iElem].ToString().ToUpper() == "PPB")
                                {

                                    if (dtElementos.Rows[1][iElem].ToString() == "FAA313" ||
                                        dtElementos.Rows[1][iElem].ToString() == "FAA515")
                                    {
                                        sVal = (double.Parse(sVal.ToString()) / 1000).ToString("########0.0");
                                    }
                                }
                                bConv = true;
                            }
                            else
                            {
                                DataRow[] dato1 = dtQCConversion.Select("Code = '" + dtElementos.Rows[iRowElem][iElem].ToString() + "'");
                                if (dato1.Length > 0)
                                {
                                    sVal = dato1[0].ItemArray[2].ToString();//dtQCConversion.Rows[0]["Conversion"].ToString();
                                    bConv = true;
                                }
                            }
                            if (words[0].ToUpper() == "AU")
                            {
                                string sAu = "Aufaaa";

                                iAu += 1;

                                if (iAu > 1) //Si esta mas de una vez el elemento
                                {
                                    sAu = "Aufagr"; //Se guarda el dato en el segundo campo en DB

                                }
                                stAu = dtElementos.Rows[0][iElem].ToString();


                                if (words[0].ToUpper() == "AU" &&
                                dtElementos.Rows[0][iElem].ToString().ToUpper() == "PPB")
                                {
                                    if (bConv == false)
                                    {
                                        if (dtElementos.Rows[iRowElem][iElem].ToString() != "")
                                        {
                                            sVal = (double.Parse(dtElementos.Rows[iRowElem][iElem].ToString()) /
                                                1000).ToString("########0.0");
                                        }
                                        else if (dtElementos.Rows[iRowElem][iElem].ToString() == "")
                                        {
                                            sVal = "5.555"; //Valor cuando el valor de laboratorio de Au viene con -- o vacio
                                        }
                                    }
                                }
                                sCampos += ",[" + sAu + "]";
                                sCamposVal += ",'" + sVal.ToString() + "'";
                            }
                            else if (words[0].ToUpper() == "AG")
                            {
                                string sAg = "Agfa";
                                iAg += 1;

                                if (iAg > 1) //Si esta mas de una vez el elemento
                                {
                                    sAg = "Agicm"; //Se guarda el dato en el segundo campo en DB
                                }

                                switch (dtElementos.Rows[1][iElem].ToString())
                                {
                                    case "AAS42C":
                                        sAg = "Agfa";
                                        break;
                                    case "ICP40B":
                                        sAg = "Agicp";
                                        break;
                                    case "AAS12C":
                                        sAg = "Agfa";
                                        break;
                                    case "AAS11B":
                                        sAg = "Agfagr";
                                        break;
                                    case "ICM40B (*)":
                                        sAg = "agicm";
                                        break;
                                    case "AAS41B (*)":
                                        sAg = "Agfagr";
                                        break;
                                    default:
                                        sAg = "Agfa";
                                        break;
                                }

                                if (sVal.ToString() == ">500")
                                {
                                    sVal = "5.001";
                                }

                                if (sVal.ToString() == ">4000")
                                {
                                    sVal = "4001";
                                }

                                stAg = dtElementos.Rows[0][iElem].ToString();
                                sCampos += ",[" + sAg + "]";
                                sCamposVal += ",'" + sVal.ToString() + "'";
                            }
                            else if (words[0].ToUpper() == "MN")
                            {
                                string sMn = "Mn";
                                iMn += 1;

                                if (iMn > 1) //Si esta mas de una vez el elemento
                                {
                                    sMn = "Mn2"; //Se guarda el dato en el segundo campo en DB
                                    if (stMn.ToString() != dtElementos.Rows[0][iElem].ToString()) //Si la unidad de medida es diferente
                                    {
                                        if (dtElementos.Rows[0][iElem].ToString() == "%") //Si la segunda unidad es porcentaje, se multiplica por 10000
                                        {
                                            if (bConv == false)
                                            {
                                                if (dtElementos.Rows[iRowElem][iElem].ToString() != "")
                                                {
                                                    sVal = (double.Parse(dtElementos.Rows[iRowElem][iElem].ToString()) *
                                                    10000).ToString("########0.000");
                                                }
                                            }
                                        }
                                    }
                                }
                                stMn = dtElementos.Rows[0][iElem].ToString();
                                sCampos += ",[" + sMn + "]";
                                sCamposVal += ",'" + sVal.ToString() + "'";
                            }

                            else if (words[0].ToUpper() == "CU")
                            {
                                string sCu = "Cu";
                                iCu += 1;

                                if (iCu > 1) //Si esta mas de una vez el elemento
                                {
                                    sCu = "Cu2"; //Se guarda el dato en el segundo campo en DB
                                    if (stCu.ToString() != dtElementos.Rows[0][iElem].ToString()) //Si la unidad de medida es diferente
                                    {
                                        if (dtElementos.Rows[0][iElem].ToString() == "%") //Si la segunda unidad es porcentaje, se multiplica por 10000
                                        {
                                            if (bConv == false)
                                            {
                                                if (dtElementos.Rows[iRowElem][iElem].ToString() != "")
                                                {
                                                    sVal = (double.Parse(dtElementos.Rows[iRowElem][iElem].ToString()) *
                                                    10000).ToString("########0.000");
                                                }
                                            }
                                        }
                                    }
                                }
                                stCu = dtElementos.Rows[0][iElem].ToString();
                                sCampos += ",[" + sCu + "]";
                                sCamposVal += ",'" + sVal.ToString() + "'";
                            }
                            else if (words[0].ToUpper() == "FE")
                            {
                                string sFe = "Fe";
                                iFe += 1;

                                if (iFe > 1) //Si esta mas de una vez el elemento
                                {
                                    sFe = "Fe2"; //Se guarda el dato en el segundo campo en DB
                                    if (stFe.ToString() != dtElementos.Rows[0][iElem].ToString()) //Si la unidad de medida es diferente
                                    {
                                        if (dtElementos.Rows[0][iElem].ToString() == "%") //Si la segunda unidad es porcentaje, se multiplica por 10000
                                        {
                                            if (bConv == false)
                                            {
                                                if (dtElementos.Rows[iRowElem][iElem].ToString() != "")
                                                {
                                                    sVal = (double.Parse(dtElementos.Rows[iRowElem][iElem].ToString()) *
                                                    10000).ToString("########0.000");
                                                }
                                            }
                                        }
                                    }
                                }
                                stFe = dtElementos.Rows[0][iElem].ToString();
                                sCampos += ",[" + sFe + "]";
                                sCamposVal += ",'" + sVal.ToString() + "'";
                            }
                            else if (words[0].ToUpper() == "AS")
                            {
                                string sAs = "As";
                                iAs += 1;

                                if (iAs > 1) //Si esta mas de una vez el elemento
                                {
                                    sAs = "As2"; //Se guarda el dato en el segundo campo en DB
                                    if (stAs.ToString() != dtElementos.Rows[0][iElem].ToString()) //Si la unidad de medida es diferente
                                    {
                                        if (dtElementos.Rows[0][iElem].ToString() == "%") //Si la segunda unidad es porcentaje, se multiplica por 10000
                                        {
                                            if (bConv == false)
                                            {
                                                if (dtElementos.Rows[iRowElem][iElem].ToString() != "")
                                                {
                                                    sVal = (double.Parse(dtElementos.Rows[iRowElem][iElem].ToString()) *
                                                    10000).ToString("########0.000");
                                                }
                                            }
                                        }
                                    }
                                }
                                stAs = dtElementos.Rows[0][iElem].ToString();
                                sCampos += ",[" + sAs + "]";
                                sCamposVal += ",'" + sVal.ToString() + "'";
                            }

                            else if (words[0].ToUpper() == "ZN")
                            {
                                string sZn = "Zn";
                                iZn += 1;

                                if (iZn > 1) //Si esta mas de una vez el elemento
                                {
                                    sZn = "Zn2"; //Se guarda el dato en el segundo campo en DB
                                    if (stZn.ToString() != dtElementos.Rows[0][iElem].ToString()) //Si la unidad de medida es diferente
                                    {
                                        if (dtElementos.Rows[0][iElem].ToString() == "%") //Si la segunda unidad es porcentaje, se multiplica por 10000
                                        {
                                            if (bConv == false)
                                            {
                                                if (dtElementos.Rows[iRowElem][iElem].ToString() != "")
                                                {
                                                    sVal = (double.Parse(dtElementos.Rows[iRowElem][iElem].ToString()) *
                                                    10000).ToString("########0.000");
                                                }
                                            }
                                        }
                                    }
                                }
                                stZn = dtElementos.Rows[0][iElem].ToString();
                                sCampos += ",[" + sZn + "]";
                                sCamposVal += ",'" + sVal.ToString() + "'";
                            }

                            /*(Fe Ok, Pb,Zn ok, Ca ok, S ok)*/
                            else if (words[0].ToUpper() == "PB")
                            {
                                string sPb = "Pb";
                                iPb += 1;

                                if (iPb > 1) //Si esta mas de una vez el elemento
                                {
                                    sPb = "Pb2"; //Se guarda el dato en el segundo campo en DB
                                    if (stPb.ToString() != dtElementos.Rows[0][iElem].ToString()) //Si la unidad de medida es diferente
                                    {
                                        if (dtElementos.Rows[0][iElem].ToString() == "%") //Si la segunda unidad es porcentaje, se multiplica por 10000
                                        {
                                            if (bConv == false)
                                            {
                                                if (dtElementos.Rows[iRowElem][iElem].ToString() != "")
                                                {
                                                    sVal = (double.Parse(dtElementos.Rows[iRowElem][iElem].ToString()) *
                                                    10000).ToString("########0.000");
                                                }
                                            }
                                        }
                                    }
                                }
                                stPb = dtElementos.Rows[0][iElem].ToString();
                                sCampos += ",[" + sPb + "]";
                                sCamposVal += ",'" + sVal.ToString() + "'";

                            }

                            else if (words[0].ToUpper() == "WEIGHT")
                            {
                                string sS = "Weight";
                                iS += 1;

                                if (iS > 1) //Si esta mas de una vez el elemento
                                {
                                    sS = "Weight"; //Se guarda el dato en el segundo campo en DB
                                    if (stS.ToString() != dtElementos.Rows[0][iElem].ToString()) //Si la unidad de medida es diferente
                                    {
                                        if (dtElementos.Rows[0][iElem].ToString() == "%") //Si la segunda unidad es porcentaje, se multiplica por 10000
                                        {
                                            if (bConv == false)
                                            {
                                                if (dtElementos.Rows[iRowElem][iElem].ToString() != "")
                                                {
                                                    sVal = (double.Parse(dtElementos.Rows[iRowElem][iElem].ToString()) *
                                                    10000).ToString("########0.000");
                                                }
                                            }
                                        }
                                    }
                                }
                                stS = dtElementos.Rows[0][iElem].ToString();

                                sCampos += ",[" + sS + "]";
                                sCamposVal += ",'" + sVal.ToString() + "'";
                            }

                            else if (words[0].ToUpper() == "CA")
                            {
                                string sCa = "Ca";
                                iCa += 1;

                                if (iCa > 1) //Si esta mas de una vez el elemento
                                {
                                    sCa = "Ca2"; //Se guarda el dato en el segundo campo en DB
                                    if (stCa.ToString() != dtElementos.Rows[0][iElem].ToString()) //Si la unidad de medida es diferente
                                    {
                                        if (dtElementos.Rows[0][iElem].ToString() == "%") //Si la segunda unidad es porcentaje, se multiplica por 10000
                                        {
                                            if (bConv == false)
                                            {
                                                if (dtElementos.Rows[iRowElem][iElem].ToString() != "")
                                                {
                                                    sVal = (double.Parse(dtElementos.Rows[iRowElem][iElem].ToString()) *
                                                    10000).ToString("########0.000");
                                                }
                                            }
                                        }
                                    }
                                }
                                stCa = dtElementos.Rows[0][iElem].ToString();
                                sCampos += ",[" + sCa + "]";
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
                    if (dtLIn.Rows.Count > 0)
                    {
                        //2013-01-30. Se comentarea esta linea por que independiente si es o no Reanalisis
                        //el campo Qaqc cuando se carga el resultado se actualiza a "1"
                        //sQaqc = "0";
                        //2013-01-30. FIN
                    }

                    if (!dtElementos.Rows[iRowElem][0].ToString().Trim().Contains("*DUP"))
                    {
                        sSQL = "Insert into Assay (JobNo, [Sample], Qaqc, [Type] " +
                            sCampos + ") Values ('" + sNomArchivo + "','" + dtElementos.Rows[iRowElem][0].ToString().Trim() +
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
                                        "JobNo = " + sNomArchivo +
                                        ". [Sample] = " + dtElementos.Rows[iRowElem][0].ToString().Trim() +
                                        ". Qaqc = " + sQaqc +
                                        ". [Type] = " + sType +
                                        ". Elementos = " + sCampos + ". Valores elementos:" + sCamposVal);

                    }
                    sCampos = "";
                    sCamposVal = "";

                }
                MessageBox.Show("Successful", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                QCReport(sNomArchivo);
                pbtnUpdate.Enabled = false;
                limpiar();
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
                    oSheet.Cells[2, 9] = dtQcReport.Rows[0]["lab"].ToString();
                    oSheet.Cells[2, 11] = dtQcReport.Rows[0]["jobno"].ToString();
                    oSheet.Cells[3, 11] = dtQcReport.Rows[0]["DateReport"].ToString();
                    oSheet.Cells[4, 11] = dtQcReport.Rows[0]["DifDate"].ToString();

                    //int iTotalMuestras = 0;
                    int iInicial = 9;
                    if (dtQcReport.Columns["DupDe"] != null || dtQcReport.Columns["DupOf"] != null)
                        for (int i = 0; i < dtQcReport.Rows.Count; i++)
                        {

                            oSheet.Cells[iInicial, 1] = dtQcReport.Rows[i]["HoleID"].ToString();//Hole
                            oSheet.Cells[iInicial, 2] = dtQcReport.Rows[i]["From"].ToString();//From
                            oSheet.Cells[iInicial, 3] = dtQcReport.Rows[i]["To"].ToString();//To
                            oSheet.Cells[iInicial, 4] = dtQcReport.Rows[i]["Sample"].ToString();//Sample
                            oSheet.Cells[iInicial, 5] = dtQcReport.Rows[i]["SampleType"].ToString();//Type

                            if (dtQcReport.Columns["DupDe"] != null)
                                oSheet.Cells[iInicial, 6] = dtQcReport.Rows[i]["DupDe"].ToString();

                            if (dtQcReport.Columns["DupOf"] != null)
                                oSheet.Cells[iInicial, 6] = dtQcReport.Rows[i]["DupOf"].ToString();

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
                    else
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
                    label2.Visible = true;
                    txtRuta.Text = result.FileName;
                    FileInfo oFi = new FileInfo(result.FileName);
                    string sExt = oFi.Extension.ToString();
                    sNomArchivo = oFi.Name.Substring(0, oFi.Name.ToString().Length - sExt.ToString().Length);

                    Excel.Application oExc = new Excel.Application();
                    //Excel.Worksheet oSheeds = new Excel.Worksheet();
                    oExc.Workbooks.Open(txtRuta.Text, 0, true, 5, Type.Missing, Type.Missing, false, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, null, null);
                    oExc.Quit();
                }
                try
                {
                    pbtnUpdate.Enabled = false;
                    dtXls = new DataTable();

                    Workbook workbook = new Workbook();

                    workbook.LoadFromFile(txtRuta.Text);
                    Worksheet sheet = workbook.Worksheets[0];

                    dtXls = sheet.ExportDataTable();
                    dgXls.DataSource = dtXls;
                    dtElementos = new DataTable();
                    //Capturo la posicion de la tabla donde debo empezar
                    int iPosicionElem = 0;
                    for (int a = 0; a < dtXls.Rows.Count; a++)
                    {
                        if (dtXls.Rows[a][0].ToString() == "Elemento")
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
            catch (Exception ex)
            {
                label2.Visible = false;
                MessageBox.Show(ex.Message);

            }
            label2.Visible = false;
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

                string sqlActualizar = String.Format("update Labsumit set Labsumit.SampleType=@value where Labsumit.Submit=@Submit");

                oLabSub.Exist_DB(sqlActualizar, Convert.ToString(comboBox1.SelectedItem), txtSubmit.Text, "@value", "@Submit");

                MessageBox.Show("Actualización realizada con Exito!");

                txtSubmit.Text = string.Empty;
                comboBox1.SelectedIndex = -1;
            }
        }
        public void EjecutaPasarDato(string Dato)
        {
            cmbJobNo.Text = Dato;
            cmbJobNo_Leave(null, null);

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            frmSearchId FrmConsultas = new frmSearchId(1);
            FrmConsultas.Pasado += new frmSearchId.PasarDatoSeleccionado(EjecutaPasarDato);
            FrmConsultas.ShowDialog();

        }

        private void cmbJobNo_Leave(object sender, EventArgs e)
        {

        }
    }
}
