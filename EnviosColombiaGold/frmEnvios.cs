using Entities.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace EnviosColombiaGold
{
    public partial class frmEnvios : Form
    {
        #region variables

        private int iSackos;
        private int iCont;
        private int iInterval;
        private clsDHSamples oDHSamp = new clsDHSamples();
        private clsLabSubmit oLabS = new clsLabSubmit();
        private clsLabsumitIn oLabSIn = new clsLabsumitIn();
        private clsLabSumitInterval oLabSInterval = new clsLabSumitInterval();
        private clsSampleofNature oSampNature = new clsSampleofNature();
        private clsSampShipment oSampShipment = new clsSampShipment();
        private clsShipment oShipment = new clsShipment();
        private clsAssay oAssay = new clsAssay();
        private clsRf oRf = new clsRf();
        public string SampleType = string.Empty;

        //int iModify = 0;
        private string sSelected = "";

        //Estados de los botones
        private bool bcmbFind = false;
        private bool bcmbModify = false;
        private bool bcmbReanalisis = false;
        private bool isRean = false;
        private bool isSelected = false;
        public int indexSelectCmbSam = -1;
        public bool modificate = false;
        #endregion

        #region CargarCombos y textocmbShipments

        public class Item
        {
            public string Name { get; set; }
            public string Value { get; set; }

            public Item(string name, string value)
            {
                Name = name;
                Value = value;
            }
            public override string ToString()
            {
                return Name;
            }
        }

        public void clear()
        {

            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            CmbSamTyp.SelectedIndex = CmbSamTyp.Items.Count - 1;
            CmbTypSub.SelectedIndex = CmbTypSub.Items.Count - 1;
            cmbPrepCode.SelectedIndex = cmbPrepCode.Items.Count - 1;
            CmbAnCod.SelectedIndex = CmbAnCod.Items.Count - 1;
            CmbNatSamp.SelectedIndex = CmbNatSamp.Items.Count - 1;
            cmbLab.SelectedIndex = cmbLab.Items.Count - 1;
            cmbPrepLab.SelectedIndex = cmbPrepLab.Items.Count - 1;
            cmbAnLab.SelectedIndex = cmbAnLab.Items.Count - 1;
            cmbDisp.SelectedIndex = cmbDisp.Items.Count - 1;

            txtMetAnCod.Text = string.Empty;
            TxtOtAnCod.Text = string.Empty;

            txtObserv.Text = string.Empty;
            txtElements.Text = string.Empty;

            cmbHoleId.SelectedIndex = -1;
            a.SelectedIndex = -1;
            CmbTS.SelectedIndex = -1;

            dgInterval.DataSource = null;
            dgDetalleInterval.DataSource = null;
        }
        public frmEnvios()
        {
            InitializeComponent();
            GetItemsPrimary();
            Load_Combos();
            initVbles();
            isSelected = true;
        }
        private void GetItemsPrimary()
        {
            List<Item> lista = new List<Item>();

            lista.Add(new Item("Small Minning Shipment", string.Concat("SMS18-0001", DateTime.Now.ToString("yy"), "-")));
            lista.Add(new Item("Mine geology Shipment", string.Concat("SMG18-0001", DateTime.Now.ToString("yy"), "-")));
            lista.Add(new Item("Exploration Geology Shipment", string.Concat("SEP18-0001", DateTime.Now.ToString("yy"), "-")));
            lista.Add(new Item("Others Shipment", string.Concat("SOT18-0001", DateTime.Now.ToString("yy"), "-")));
            lista.Add(new Item("Ore Stockyard Shipment", string.Concat("SOS18-0001", DateTime.Now.ToString("yy"), "-")));
            lista.Add(new Item("Select an option..", ""));
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Value";
            comboBox1.DataSource = lista;
            comboBox1.SelectedIndex = lista.Count() - 1;
        }

        private void initVbles()
        {
            try
            {
                iSackos = 1;
                iCont = 1;
                if (dgInterval.Rows.Count == 0)
                {
                    iInterval = 1;
                }
                else if (dgInterval.Rows.Count >= 1)
                {
                    iInterval = dgInterval.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void LoadCmb()
        {
            try
            {
                DataTable dtPrefix = new DataTable();
                dtPrefix = oRf.getRfPrefix();
                string sPrefix = "";
                if (dtPrefix.Rows.Count > 0)
                {
                    sPrefix = dtPrefix.Rows[0]["Description_Prefix"].ToString();
                }

                DataTable dtCmbShipm = new DataTable();
                dtCmbShipm = LoadLabSubmit("3", sPrefix);

                cmbShipment.DisplayMember = "Ssumit";
                cmbShipment.ValueMember = "Ssumit";
                cmbShipment.DataSource = dtCmbShipm;

                int iCon = 1;
                if (dtCmbShipm.Rows.Count > 0 && cmbShipment.SelectedValue.ToString().Contains(sPrefix))
                {
                    //código Anterior
                    //iCon = int.Parse(cmbShipment.Text.ToString().Substring(6, 4)) + 1;
                    string sYear = DateTime.Now.ToString("yy");

                    int yearValue = dtCmbShipm.Rows[0]["Ssumit"].ToString().IndexOf('-');
                    if (yearValue > 0)
                    {
                        iCon = int.Parse(dtCmbShipm.Rows[0]["Ssumit"].ToString().Substring(yearValue - 2, 2));
                    }

                    if (int.Parse(sYear) <= iCon)
                    {
                        yearValue = cmbShipment.Text.ToString().IndexOf('-');
                        if (yearValue > 0)
                        {
                            iCon = int.Parse(cmbShipment.Text.Substring(yearValue - 2, 2));
                        }
                    }
                }

                DataRow dr = dtCmbShipm.NewRow();
                dr[0] = sPrefix.ToString() + DateTime.Now.ToString("yy") + "-" + iCon.ToString().PadLeft(4, '0');
                dtCmbShipm.Rows.Add(dr);
                sSelected = sPrefix.ToString() + DateTime.Now.ToString("yy") + "-" + iCon.ToString().PadLeft(4, '0');
                cmbShipment.SelectedValue = sSelected.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void LoadCmb1()
        {
            try
            {
                if (!String.IsNullOrEmpty(comboBox1.SelectedValue.ToString()))
                {
                    string sPrefix = comboBox1.SelectedValue.ToString().Substring(0, 3);
                    var ent_dtCmbShipm = LoadLabSubmit1("3", sPrefix);
                    int yearValue = 0;
                    cmbShipment.DisplayMember = "Ssumit";
                    cmbShipment.ValueMember = "Ssumit";
                    var query = ent_dtCmbShipm.Select(c => c.Ssumit).ToList();
                    int iCon = 1;
                    if (ent_dtCmbShipm.ToList().Count > 0)
                    {
                        cmbShipment.Items.AddRange(query.ToArray());
                        cmbShipment.SelectedIndex = 0;

                        if (cmbShipment.SelectedItem.ToString().Contains(sPrefix))
                        {
                            //código Anterior
                            string sYear = DateTime.Now.ToString("yy");

                            yearValue = ent_dtCmbShipm.Select(c => c.Ssumit).FirstOrDefault().ToString().IndexOf('-');
                            if (yearValue > 0)
                            {
                                //iCon = int.Parse(ent_dtCmbShipm.Select(c => c.Ssumit).FirstOrDefault().ToString().Substring(yearValue - 2, 2));
                                iCon = int.Parse(ent_dtCmbShipm.Select(c => c.Ssumit).FirstOrDefault().ToString().Substring(yearValue + 1, 4));
                            }

                            if (int.Parse(sYear) <= iCon)
                            {
                                yearValue = cmbShipment.Text.ToString().IndexOf('-');
                                if (yearValue > 0)
                                {
                                    iCon = int.Parse(cmbShipment.Text.Substring(yearValue + 1, 4));
                                }
                            }
                        }
                    }
                    Ent_Prefix pre = new Ent_Prefix();

                    var valueDefaul = LoadLabSubmit1("3", sPrefix).Select(e => e.Ssumit).FirstOrDefault();
                    if (valueDefaul != null)
                    {
                        int valueIndexSeparator = valueDefaul.IndexOf('-');

                        iCon = int.Parse(valueDefaul.ToString().Substring(valueIndexSeparator + 1, 4)) + 1;

                        pre.Ssumit = sPrefix.ToString() + DateTime.Now.ToString("yy") + "-" + iCon.ToString().PadLeft(4, '0');
                        ent_dtCmbShipm.Insert(ent_dtCmbShipm.ToList().Count, pre);
                        sSelected = sPrefix.ToString() + DateTime.Now.ToString("yy") + "-" + iCon.ToString().PadLeft(4, '0');
                        query = ent_dtCmbShipm.Select(c => c.Ssumit).ToList();
                        cmbShipment.Items.Clear();
                        cmbShipment.Items.AddRange(query.ToArray());
                        cmbShipment.SelectedIndex = ent_dtCmbShipm.ToList().Count - 1;
                    }
                    else
                    {
                        pre.Ssumit = sPrefix.ToString() + DateTime.Now.ToString("yy") + "-" + iCon.ToString().PadLeft(4, '0');
                        ent_dtCmbShipm.Insert(ent_dtCmbShipm.ToList().Count, pre);
                        sSelected = sPrefix.ToString() + DateTime.Now.ToString("yy") + "-" + iCon.ToString().PadLeft(4, '0');
                        query = ent_dtCmbShipm.Select(c => c.Ssumit).ToList();
                        cmbShipment.Items.Clear();
                        cmbShipment.Items.AddRange(query.ToArray());
                        cmbShipment.SelectedIndex = ent_dtCmbShipm.ToList().Count - 1;
                    }
                }
                else
                {
                    cmbShipment.SelectedIndex = 0;
                    cmbShipment.Text = string.Empty;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void Load_Combos()
        {
            try
            {
                CmbNatSamp.DisplayMember = "description";
                CmbNatSamp.ValueMember = "Id";
                //CmbNatSamp.DataSource = LoadSampleofnatureAll();

                var query = LoadSampleofnatureAllEnt().Select(c => c.description).ToList();
                CmbNatSamp.Items.AddRange(query.ToArray());
                CmbNatSamp.SelectedIndex = 0;
                CmbNatSamp.SelectedIndex = query.ToList().Count - 1;

                CmbSamTyp.DisplayMember = "Description";
                CmbSamTyp.ValueMember = "Code";

                query = LoadSampShipmentAllEnt().Select(c => c.Description).ToList();
                CmbSamTyp.Items.AddRange(query.ToArray());
                CmbSamTyp.SelectedText = "-1";
                CmbSamTyp.SelectedIndex = query.ToList().Count - 1;

                CmbTypSub.DisplayMember = "Description";
                CmbTypSub.ValueMember = "Code";
                var query1 = LoadShipmentAllTypSubEntity();
                CmbTypSub.DataSource = query1.Select(c => c.Description).ToList(); ;
                CmbTypSub.SelectedIndex = query1.ToList().Count - 1;

                cmbDisp.DisplayMember = "Name";
                cmbDisp.ValueMember = "Identification";
                var query2 = LoadDispatchedbySubEntity();
                cmbDisp.DataSource = query2;
                cmbDisp.SelectedIndex = query2.ToList().Count - 1;

                cmbLab.DisplayMember = "Code";
                cmbLab.ValueMember = "Code";
                var query3 = GetRfCodeLabEntity();
                cmbLab.DataSource = query3.Select(c => c.Code).ToList();
                cmbLab.SelectedIndex = query3.ToList().Count - 1;

                cmbPrepCode.DisplayMember = "Code";
                cmbPrepCode.ValueMember = "Code";
                var query5 = GetuRfPreparationCodeEntity();
                cmbPrepCode.DataSource = query5.Select(c => c.Code).ToList();
                cmbPrepCode.SelectedIndex = query5.ToList().Count - 1;

                CmbAnCod.DisplayMember = "Code";
                CmbAnCod.ValueMember = "Code";
                var query6 = GetuRfAnalysisCodeLabEntity();
                CmbAnCod.DataSource = query6.Select(c => c.Code).ToList();
                CmbAnCod.SelectedIndex = query6.ToList().Count - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private DataTable LoadAuAnCode()
        {
            try
            {
                DataTable dtAuAnC = new DataTable();
                dtAuAnC = oRf.getRfAnalysisCodeLab();

                DataRow dr = dtAuAnC.NewRow();
                dr[0] = "Select an option..";
                dtAuAnC.Rows.Add(dr);
                return dtAuAnC;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable LoadPrepCode()
        {
            try
            {
                DataTable dtPrpCode = new DataTable();
                dtPrpCode = oRf.getRfPreparationCode();

                DataRow dr = dtPrpCode.NewRow();
                dr[0] = "Select an option..";
                dtPrpCode.Rows.Add(dr);

                return dtPrpCode;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable LoadCodeLab()
        {
            try
            {
                DataTable dtLabC = new DataTable();
                dtLabC = oRf.getRfCodeLab();
                return dtLabC;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private IList<Ent_Prefix> LoadDispatchedbySubEntity()
        {
            try
            {
                var dtShipment = oShipment.getLoadDispatchedbyEntity();

                Ent_Prefix pre = new Ent_Prefix();
                pre.Identification = -1;
                pre.Name = "Select an option..";
                dtShipment.Insert(dtShipment.ToList().Count, pre);

                return dtShipment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private IList<Ent_Prefix> GetuRfAnalysisCodeLabEntity()
        {
            try
            {
                var dtShipment = oShipment.getRfAnalysisCodeLabEntity();

                Ent_Prefix pre = new Ent_Prefix();
                pre.Identification = -1;
                pre.Code = "Select an option..";
                dtShipment.Insert(dtShipment.ToList().Count, pre);


                return dtShipment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private IList<Ent_Prefix> GetuRfPreparationCodeEntity()
        {
            try
            {
                var dtShipment = oShipment.getRfPreparationCodeEntity();

                Ent_Prefix pre = new Ent_Prefix();
                pre.Identification = -1;
                pre.Code = "Select an option..";
                dtShipment.Insert(dtShipment.ToList().Count, pre);


                return dtShipment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private IList<Ent_Prefix> GetRfCodeLabEntity()
        {
            try
            {
                var dtShipment = oShipment.getuRfCodeLabEntity();

                Ent_Prefix pre = new Ent_Prefix();
                pre.Identification = -1;
                pre.Code = "Select an option..";
                dtShipment.Insert(dtShipment.ToList().Count, pre);

                return dtShipment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable LoadDispatchedby()
        {
            try
            {
                DataTable dtDisp = new DataTable();
                dtDisp = oRf.getRfWorker();

                DataRow dr = dtDisp.NewRow();
                dr[0] = "-1";
                dr[2] = "Select an option..";
                dtDisp.Rows.Add(dr);

                return dtDisp;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private DataTable LoadLabSubmit(string _sOpcion, string _SSubmit)
        {
            try
            {
                DataTable dtLabS = new DataTable();
                oLabS.sOpcion = _sOpcion.ToString();
                oLabS.sSubmit = _SSubmit.ToString();
                dtLabS = oLabS.getLabSubmit();
                dgInterval.DataSource = dtLabS;

                return dtLabS;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private IList<Ent_Prefix> LoadLabSubmit1(string _sOpcion, string _SSubmit)
        {
            try
            {
                oLabS.sOpcion = _sOpcion.ToString();
                oLabS.sSubmit = _SSubmit.ToString();
                IList<Ent_Prefix> dtLabS = oLabS.getLabSubmitEntities();

                return dtLabS;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable LoadLabSubmitIn(string _sOpcion, string _SSubmit)
        {
            try
            {

                DataTable dtLabSIn = new DataTable();
                oLabSIn.sOpcion = _sOpcion.ToString();
                oLabSIn.sSubmit = _SSubmit;
                dtLabSIn = oLabSIn.getLabSubmitIn();
                return dtLabSIn;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable LoadLabSubmitInterval(string _sOpcion, string _SSubmit)
        {
            try
            {

                DataTable dtLabSInterval = new DataTable();
                oLabSInterval.sOpcion = _sOpcion.ToString();
                oLabSInterval.sSubmit = _SSubmit;
                dtLabSInterval = oLabSInterval.getLabSubmitInterval();
                return dtLabSInterval;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable LoadSampleofnature(int _iId)
        {
            try
            {

                DataTable dtSampNature = new DataTable();
                oSampNature.iID = _iId;
                dtSampNature = oSampNature.getSampleofNature();
                return dtSampNature;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable LoadSampShipment(string _sCode)
        {
            try
            {
                DataTable dtSampShipment = new DataTable();
                oSampShipment.sCode = _sCode;
                dtSampShipment = oSampShipment.getSampShipment();
                return dtSampShipment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable LoadShipment(string _sCode)
        {
            try
            {
                DataTable dtShipment = new DataTable();
                oShipment.sCode = _sCode;
                dtShipment = oShipment.getShipment();
                return dtShipment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadFromTo()
        {
            try
            {
                DataTable dtFrom = new DataTable();
                DataTable dtTo = new DataTable();
                oDHSamp.sOpcion = "2";

                if (cmbHoleId.SelectedValue != null)
                {
                    oDHSamp.sHoleID = cmbHoleId.SelectedValue.ToString();
                }

                dtFrom = oDHSamp.getDHSamples();
                dtTo = dtFrom.Copy();

                a.DisplayMember = "Sample";//"From";
                if (a.ValueMember == string.Empty)
                {
                    a.ValueMember = "SKDHSamples";
                }

                CmbTS.DisplayMember = "Sample"; //"To";
                if (CmbTS.ValueMember == string.Empty)
                {
                    CmbTS.ValueMember = "SKDHSamples";
                }

                a.DataSource = dtFrom; //dtFromR;
                CmbTS.DataSource = dtTo; //dtToR;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private IList<Ent_Prefix> getCHSamplesEntity(string val1, string val2)
        {
            try
            {
                IList<Ent_Prefix> dtShipment = oShipment.getCHSamplesEntity(val1, val2);

                Ent_Prefix pre = new Ent_Prefix();
                pre.Identification = -1;
                pre.Name = "Select an option..";
                dtShipment.Insert(dtShipment.ToList().Count, pre);

                return dtShipment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadFromToChannels()
        {
            try
            {
                clsCHSamples oCh = new clsCHSamples();
                oCh.sOpcion = "2";
                if (cmbHoleId.SelectedValue == null)
                {
                    oCh.sCHId = "";
                }
                else
                {
                    oCh.sCHId = cmbHoleId.SelectedValue.ToString();
                }

                IList<Ent_Prefix> cHSamplesEntity = getCHSamplesEntity(oCh.sOpcion, oCh.sCHId);

                Ent_Prefix[] value1 = cHSamplesEntity.ToArray();
                Ent_Prefix[] value2 = value1;

                a.DisplayMember = "Sample";
                a.ValueMember = "Sample";

                CmbTS.DisplayMember = "Sample";
                CmbTS.ValueMember = "Sample";

                a.DataSource = cHSamplesEntity;
                CmbTS.DataSource = value2;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadFromToRocks()
        {
            try
            {
                clsGCSamplesRock oRock = new clsGCSamplesRock();
                oRock.sOpcion = "1";
                DataTable dtFromToR = oRock.getGCSamplesRockAll();

                a.DisplayMember = "Sample";//"From";
                if (a.ValueMember == string.Empty)
                {
                    a.ValueMember = "Sample";
                }

                CmbTS.DisplayMember = "Sample"; //"To";
                if (CmbTS.ValueMember == string.Empty)
                {
                    CmbTS.ValueMember = "Sample";
                }

                a.DataSource = dtFromToR;
                CmbTS.DataSource = dtFromToR.Copy();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadCmbHoleId()
        {
            try
            {
                DataTable dtHoleId = new DataTable();
                //if (CmbSamTyp.SelectedValue != null)

                if (CmbSamTyp.SelectedIndex != 4)
                {
                    string code = LoadSampShipmentAllEnt()[CmbSamTyp.SelectedIndex].Code.ToString().Trim();

                    if (code == ConfigurationSettings.AppSettings["Drill"].ToString())
                    {
                        dtHoleId = LoadCmbHoleDril();
                    }
                    else if (code == ConfigurationSettings.AppSettings["Rocks"].ToString())
                    {

                        dtHoleId = new DataTable();
                        cmbHoleId.DataSource = dtHoleId;
                        cmbHoleId.Enabled = false;
                        LoadFromToRocks();

                    }
                    else if (code == ConfigurationSettings.AppSettings["Channels"].ToString())
                    {
                        dtHoleId = new DataTable();
                        cmbHoleId.DataSource = dtHoleId;
                        cmbHoleId.Enabled = false;
                        LoadFromToChannels();
                    }
                    else
                    {
                        dtHoleId = new DataTable();
                        this.cmbHoleId.DataSource = dtHoleId;
                        this.cmbHoleId.Enabled = false;
                        this.LoadFromToRocks();
                        this.LoadFromToChannels();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private DataTable LoadCmbHoleDril()
        {
            try
            {
                clsDHSamples clsDHSamples = new clsDHSamples();
                DataTable dHSamplesDistinct = clsDHSamples.getDHSamplesDistinct();
                for (int i = 0; i < 2; i++)
                {
                    try
                    {
                        cmbHoleId.ValueMember = "HoleID";
                    }
                    catch
                    {
                    }
                    cmbHoleId.DisplayMember = "Comb";
                    cmbHoleId.DataSource = dHSamplesDistinct;
                    cmbHoleId.Enabled = true;
                    cmbHoleId.SelectedItem = "";
                }
                return dHSamplesDistinct;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private DataTable LoadSampleofnatureAll()
        {
            try
            {
                DataTable dtSampNature = new DataTable();
                dtSampNature = oSampNature.getSampleofNatureAllDT();
                DataRow dr = dtSampNature.NewRow();

                dr[0] = "-1";
                dr[1] = "Select an option..";
                dtSampNature.Rows.Add(dr);
                return dtSampNature;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private IList<Ent_Prefix> LoadSampleofnatureAllEnt()
        {
            try
            {
                IList<Ent_Prefix> dtSampNature = oSampNature.getSampleofNatureAll();
                Ent_Prefix pre = new Ent_Prefix();
                pre.Id = -1;
                pre.description = "Select an option..";
                dtSampNature.Insert(dtSampNature.ToList().Count, pre);

                return dtSampNature;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private IList<Ent_Prefix> LoadSampShipmentAllEnt()
        {
            try
            {
                IList<Ent_Prefix> dtSampShipment = oSampShipment.getSampShipmentAllEnt();
                Ent_Prefix pre = new Ent_Prefix();
                pre.Code = "-1";
                pre.Description = "Select an option..";
                dtSampShipment.Insert(dtSampShipment.ToList().Count, pre);

                return dtSampShipment;
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
                DataTable dtSampShipment = new DataTable();
                dtSampShipment = oSampShipment.getSampShipmentAll();

                DataRow dr = dtSampShipment.NewRow();
                dr[1] = "-1";
                dr[2] = "Select an option..";
                dtSampShipment.Rows.Add(dr);

                return dtSampShipment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private IList<Ent_Prefix> LoadShipmentAllEntity()
        {
            try
            {
                IList<Ent_Prefix> dtSampShipment = oSampShipment.getSampShipmentAllEnt();
                Ent_Prefix pre = new Ent_Prefix();
                pre.Code = "-1";
                pre.Description = "Select an option..";
                dtSampShipment.Insert(dtSampShipment.ToList().Count, pre);
                return dtSampShipment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private IList<Ent_Prefix> LoadShipmentAllTypSubEntity()
        {
            try
            {
                IList<Ent_Prefix> dtShipment = oShipment.getShipmentAllEntity();

                Ent_Prefix pre = new Ent_Prefix();
                pre.Code = "-1";
                pre.Description = "Select an option..";
                dtShipment.Insert(dtShipment.ToList().Count, pre);
                return dtShipment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private DataTable LoadShipmentAll()
        {
            try
            {
                DataTable dtShipment = new DataTable();
                dtShipment = oShipment.getShipmentAll();
                DataRow dr = dtShipment.NewRow();
                dr[1] = "-1";
                dr[2] = "Select an option..";
                dtShipment.Rows.Add(dr);

                return dtShipment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /* Alvaro Araujo Arrieta */
        private void Load_FormData()
        {
            try
            {
                if (!String.IsNullOrEmpty(cmbShipment.Text.Trim()))
                {
                    oLabS.sSubmit = cmbShipment.Text.ToString();
                    oLabS.sOpcion = "1";
                    clsGCSamplesRock oRock = new clsGCSamplesRock();


                    DataTable dtCmbShipm = new DataTable();
                    //Metodo que recibe una entidad, ya se encuentra modificado

                    dtCmbShipm = LoadLabSubmit("1", cmbShipment.Text.ToString());

                    if (dtCmbShipm.Rows.Count > 0)
                    {
                        DateTime calculateDate = Convert.ToDateTime(dtCmbShipm.Rows[0]["DateShipment"]);
                        string fecha = String.Format("{0: dd/MM/yyyy}", calculateDate);

                        TimeSpan dTime = (DateTime.Today - Convert.ToDateTime(fecha));
                        if (dTime.Days > 20 && bcmbModify)
                        {
                            MessageBox.Show("It was created 20 days ago. Blocked");
                            bcmbModify = false;
                            return;
                        }


                        if (dtCmbShipm.Rows[0]["TypeSumit"].ToString() == "RE")
                        {
                            isRean = true;
                        }
                        else
                        {
                            isRean = false;
                        }

                        //txtWorkOrder.Text = dtCmbShipm.Rows[0]["WorkOrder"].ToString();
                        txtDate.Text = dtCmbShipm.Rows[0]["DateShipment"].ToString();
                        cmbLab.Text = dtCmbShipm.Rows[0]["LabCode"].ToString();
                        cmbPrepLab.Text = dtCmbShipm.Rows[0]["PreparationLab"].ToString();
                        cmbAnLab.Text = dtCmbShipm.Rows[0]["AnaliticalLab"].ToString();

                        cmbDisp.DisplayMember = "Name";
                        cmbDisp.ValueMember = "Identification";
                        oLabS.sDispatchedBy = dtCmbShipm.Rows[0]["DispatchedBy"].ToString();
                        cmbDisp.DataSource = oLabS.getLabSubmit_Dispatchedby();
                        cmbDisp.SelectedValue = dtCmbShipm.Rows[0]["DispatchedBy"].ToString();

                        cmbPrepCode.Text = dtCmbShipm.Rows[0]["PrepCode"].ToString();
                        CmbAnCod.Text = dtCmbShipm.Rows[0]["AnalisysCode"].ToString();
                        txtMetAnCod.Text = dtCmbShipm.Rows[0]["MetAnalisysCode"].ToString();
                        TxtOtAnCod.Text = dtCmbShipm.Rows[0]["OtherAnalisysCode"].ToString();
                        txtInstructions.Text = dtCmbShipm.Rows[0]["Instructions"].ToString();
                        //txtBags.Text = "";//dtCmbShipm.Rows[0]["NoBags"].ToString();
                        txtObserv.Text = dtCmbShipm.Rows[0]["Observations"].ToString();
                        txtElements.Text = dtCmbShipm.Rows[0]["Element"].ToString();
                        //cmbHoleId.Text = dtCmbShipm.Rows[0]["HoleID"].ToString();

                        CmbNatSamp.DisplayMember = "description";
                        CmbNatSamp.ValueMember = "Id";
                        CmbNatSamp.DataSource = LoadSampleofnatureAll();
                        if (dtCmbShipm.Rows[0]["SampleNature"].ToString() != "")
                        {
                            CmbNatSamp.SelectedValue = dtCmbShipm.Rows[0]["SampleNature"].ToString();
                        }

                        CmbSamTyp.DisplayMember = "Description";
                        CmbSamTyp.ValueMember = "Code";
                        if (CmbSamTyp.DataSource == null)
                        {
                            CmbSamTyp.DataSource = LoadSampShipmentAll();
                        }

                        if (dtCmbShipm.Rows[0]["SampleType"].ToString() != "")
                        {
                            CmbSamTyp.SelectedValue = dtCmbShipm.Rows[0]["SampleType"].ToString();
                        }

                        //LoadCmbHoleId();
                        //Implementacion David Ciro
                        //CmbTypSub.DisplayMember = "Description";
                        //CmbTypSub.ValueMember = "Code";
                        ////CmbTypSub.DataSource = LoadShipment(dtCmbShipm.Rows[0]["TypeSumit"].ToString());
                        //CmbTypSub.DataSource = LoadShipmentAll();


                        CmbTypSub.DisplayMember = "Description";
                        CmbTypSub.ValueMember = "Code";
                        var query1 = LoadShipmentAllTypSubEntity();
                        CmbTypSub.DataSource = query1.Select(c => c.Description).ToList();
                        CmbTypSub.SelectedIndex = query1.ToList().Count - 1;

                        if (dtCmbShipm.Rows[0]["TypeSumit"].ToString() != "")
                        {
                            int index = -1;
                            foreach (var item in query1)
                            {
                                index++;
                                if (item.Code.Contains(dtCmbShipm.Rows[0]["TypeSumit"].ToString()))
                                {
                                    break;
                                }
                            }
                            CmbTypSub.SelectedIndex = index;
                        }

                        LoadCmbHoleId();

                        if (CmbSamTyp.SelectedValue.ToString() == ConfigurationSettings.AppSettings["Drill"].ToString())
                        {
                            LoadFromTo();
                        }

                        if (CmbSamTyp.SelectedValue.ToString() == ConfigurationSettings.AppSettings["Rocks"].ToString())
                        {
                            LoadFromToRocks();
                        }

                        if (CmbSamTyp.SelectedValue.ToString() == ConfigurationSettings.AppSettings["Channels"].ToString())
                        {
                            LoadFromTo();
                            LoadFromToRocks();
                        }


                        DataTable dtInterval = new DataTable();
                        dtInterval = LoadLabSubmitInterval("4", cmbShipment.Text.ToString());
                        dgInterval.DataSource = dtInterval;
                        dgInterval.Columns[0].Visible = false;

                        //clsLabSumitInterval.dtIntervals = dtInterval;
                        clsLabSumitInterval.dtIntervals = new DataTable();
                        if (clsLabSumitInterval.dtIntervals.Columns.Count == 0)
                        {
                            clsLabSumitInterval.dtIntervals.Columns.Add("ID", typeof(String));
                            clsLabSumitInterval.dtIntervals.Columns.Add("SampleFrom", typeof(String));
                            clsLabSumitInterval.dtIntervals.Columns.Add("SampleTo", typeof(String));
                            clsLabSumitInterval.dtIntervals.Columns.Add("TotalSample", typeof(String));
                            clsLabSumitInterval.dtIntervals.Columns.Add("IdFrom", typeof(String));
                            clsLabSumitInterval.dtIntervals.Columns.Add("IdTo", typeof(String));
                        }

                        for (int i = 0; i < dtInterval.Rows.Count; i++)
                        {
                            DataRow dr = clsLabSumitInterval.dtIntervals.NewRow();
                            dr["ID"] = dtInterval.Rows[i]["ID"].ToString();
                            dr["SampleFrom"] = dtInterval.Rows[i]["SampleFrom"].ToString();
                            dr["SampleTo"] = dtInterval.Rows[i]["SampleTo"].ToString();
                            dr["TotalSample"] = dtInterval.Rows[i]["TotalSample"].ToString();

                            if (a.SelectedValue != null)
                            {
                                dr["IdFrom"] = a.SelectedValue.ToString();
                            }
                            if (CmbTS.SelectedValue != null)
                            {
                                dr["IdTo"] = CmbTS.SelectedValue.ToString();
                            }

                            clsLabSumitInterval.dtIntervals.Rows.Add(dr);
                        }

                        DataTable dtIn = new DataTable();
                        dtIn = LoadLabSubmitIn("4", cmbShipment.Text.ToString());
                        dgDetalleInterval.DataSource = dtIn;
                        //clsLabsumitIn.dtIn = dtIn;
                        clsLabsumitIn.dtIn = new DataTable();
                        if (clsLabsumitIn.dtIn.Columns.Count == 0)
                        {
                            DataColumn DCAutoInc = clsLabsumitIn.dtIn.Columns.Add("Con", typeof(Int32));
                            DCAutoInc.AutoIncrement = true;
                            DCAutoInc.AutoIncrementSeed = 1;
                            DCAutoInc.AutoIncrementStep = 1;
                            clsLabsumitIn.dtIn.Columns.Add("IDInterval", typeof(String));
                            clsLabsumitIn.dtIn.Columns.Add("Sample", typeof(String));
                            clsLabsumitIn.dtIn.Columns.Add("Sack", typeof(String));
                            clsLabsumitIn.dtIn.Columns.Add("IdAut", typeof(int));
                        }

                        string sCantSack = "0";

                        for (int i = 0; i < dtIn.Rows.Count; i++)
                        {
                            DataRow drDetail = clsLabsumitIn.dtIn.NewRow();
                            drDetail["Con"] = dtIn.Rows[i]["Con"].ToString();
                            drDetail["IDInterval"] = dtIn.Rows[i]["IDInterval"].ToString();
                            drDetail["Sample"] = dtIn.Rows[i]["Sample"].ToString();
                            drDetail["Sack"] = dtIn.Rows[i]["Sack"].ToString();
                            drDetail["IdAut"] = dtIn.Rows[i]["DHSamplesID"].ToString() == string.Empty ? "0" : dtIn.Rows[i]["DHSamplesID"].ToString();
                            sCantSack = dtIn.Rows[i]["Sack"].ToString();
                            clsLabsumitIn.dtIn.Rows.Add(drDetail);
                        }

                        if (clsLabsumitIn.dtIn.Rows.Count > 0)
                        {
                            dgDetalleInterval.Columns[1].Visible = false;
                            dgDetalleInterval.Columns[4].Visible = false;
                        }

                        txtBags.Text = sCantSack.ToString();
                    }
                    else
                    {
                        MessageBox.Show("No records to show", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ControlsClean();
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Load Form Data: " + ex.Message);
            }
        }

        #endregion

        public void EjecutaPasarDato(string Dato)
        {
            try
            {
                cmbShipment.Items.Clear();
                cmbShipment.Text = Dato;
                cmbShipment_Leave(null, null);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                frmSearchId FrmConsultas = new frmSearchId();
                FrmConsultas.Pasado += new frmSearchId.PasarDatoSeleccionado(EjecutaPasarDato);
                FrmConsultas.ShowDialog();
                modificate = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbShipment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbShipment.Text.ToString() == sSelected)
                {
                    bcmbFind = false;
                }
                else
                {
                    bcmbFind = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ControlsClean()
        {
            try
            {
                comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
                CmbSamTyp.SelectedIndex = CmbSamTyp.Items.Count - 1;
                CmbTypSub.SelectedIndex = CmbTypSub.Items.Count - 1;
                cmbPrepCode.SelectedIndex = cmbPrepCode.Items.Count - 1;
                CmbAnCod.SelectedIndex = CmbAnCod.Items.Count - 1;
                CmbNatSamp.SelectedIndex = CmbNatSamp.Items.Count - 1;
                cmbLab.SelectedIndex = cmbLab.Items.Count - 1;
                cmbPrepLab.SelectedIndex = cmbPrepLab.Items.Count - 1;
                cmbAnLab.SelectedIndex = cmbAnLab.Items.Count - 1;
                cmbDisp.SelectedIndex = cmbDisp.Items.Count - 1;

                GetItemsPrimary();

                cmbHoleId.Text = "";
                txtMetAnCod.Text = "";
                txtBags.Text = "";
                txtDate.Text = DateTime.Now.ToShortDateString();
                txtElements.Text = "";
                txtInstructions.Text = "";
                TxtOtAnCod.Text = "";
                txtObserv.Text = "";
                a.Text = ""; CmbTS.Text = "";

                clsLabsumitIn.dtIn = new DataTable();
                clsLabSumitInterval.dtIntervals = new DataTable();

                dgInterval.DataSource = clsLabsumitIn.dtIn;
                dgDetalleInterval.DataSource = clsLabSumitInterval.dtIntervals;

                bcmbFind = false;
                bcmbModify = false;
                bcmbReanalisis = false;
                isRean = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            try
            {
                ControlsClean();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void frmEnvios_Load(object sender, EventArgs e)
        {
            try
            {
                lblUser.Text = "Welcome " + clsRf.sUser;

                DataRow[] dato;
                dato = clsRf.dsPermisos.Tables[1].Select("nombre_Real_Form = 'frmEnvios' and Accion = 'Insertar'");
                if (dato.Length > 0)
                {
                    btnSave.Enabled = true;
                }
                else
                {
                    btnSave.Enabled = false;
                }

                dato = clsRf.dsPermisos.Tables[1].Select("nombre_Real_Form = 'frmEnvios' and Accion = 'Generar Reportes'");
                if (dato.Length > 0)
                {
                    btnReport.Enabled = true;
                }
                else
                {
                    btnReport.Enabled = false;
                }

                dato = clsRf.dsPermisos.Tables[1].Select("nombre_Real_Form = 'frmEnvios' and Accion = 'Modificar'");
                if (dato.Length > 0)
                {
                    btnModify.Enabled = true;
                }
                else
                {
                    btnModify.Enabled = false;
                }

                dato = clsRf.dsPermisos.Tables[1].Select("nombre_Real_Form = 'frmEnvios' and Accion = 'Consultar'");
                if (dato.Length > 0)
                {
                    btnFind.Enabled = true;
                }
                else
                {
                    btnFind.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Load Error" + ex.Message);
            }
        }

        private void CmbSamTyp_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                LoadCmbHoleId();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void Init_DtIn()
        {
            try
            {
                if (clsLabsumitIn.dtIn.Columns.Count == 0)
                {
                    DataColumn DCAutoInc = clsLabsumitIn.dtIn.Columns.Add("Con", typeof(Int32));
                    DCAutoInc.AutoIncrement = true;
                    DCAutoInc.AutoIncrementSeed = 1;
                    DCAutoInc.AutoIncrementStep = 1;
                    clsLabsumitIn.dtIn.Columns.Add("IDInterval", typeof(String));
                    clsLabsumitIn.dtIn.Columns.Add("Sample", typeof(String));
                    clsLabsumitIn.dtIn.Columns.Add("Sack", typeof(String));
                    clsLabsumitIn.dtIn.Columns.Add("IdAut", typeof(int));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                clsGCSamplesRock oRock = new clsGCSamplesRock();
                clsCHSamples oCh = new clsCHSamples();

                string sMens = "";

                for (int i = 0; i < dgInterval.Rows.Count - 1; i++)
                {
                    string code = LoadSampShipmentAllEnt()[CmbSamTyp.SelectedIndex].Code.ToString().Trim();
                    if (code == ConfigurationSettings.AppSettings["Drill"].ToString())
                    {
                        if (oDHSamp.getDHSamples_getRangeValid(dgInterval.Rows[i].Cells["SampleFrom"].Value.ToString(),
                            dgInterval.Rows[i].Cells["SampleTo"].Value.ToString(), a.SelectedValue.ToString()).Rows.Count > 0)
                        {
                            sMens = "NOK";
                            break;
                        }

                        if (oDHSamp.getDHSamples_getRangeValid(dgInterval.Rows[i].Cells["SampleFrom"].Value.ToString(),
                            dgInterval.Rows[i].Cells["SampleTo"].Value.ToString(), CmbTS.SelectedValue.ToString()).Rows.Count > 0)
                        {
                            sMens = "NOK";
                            break;
                        }
                    }
                    else if (code == ConfigurationSettings.AppSettings["Rocks"].ToString())
                    {
                        DataTable dtRF = oRock.getGC_SamplesRockBySample(a.SelectedValue.ToString());
                        if (oRock.getGC_SamplesRock_getRangeValid(dgInterval.Rows[i].Cells["SampleFrom"].Value.ToString(),
                            dgInterval.Rows[i].Cells["SampleTo"].Value.ToString(), dtRF.Rows[0]["SKSamplesRock"].ToString()).Rows.Count > 0)
                        {
                            sMens = "NOK";
                            break;
                        }

                        DataTable dtRT = oRock.getGC_SamplesRockBySample(a.SelectedValue.ToString());
                        if (oRock.getGC_SamplesRock_getRangeValid(dgInterval.Rows[i].Cells["SampleFrom"].Value.ToString(),
                            dgInterval.Rows[i].Cells["SampleTo"].Value.ToString(), dtRF.Rows[0]["SKSamplesRock"].ToString()).Rows.Count > 0)
                        {
                            sMens = "NOK";
                            break;
                        }
                    }
                    else if (code == ConfigurationSettings.AppSettings["Channels"].ToString())
                    {
                        if (oCh.getCHSamples_getRangeValid(dgInterval.Rows[i].Cells["SampleFrom"].Value.ToString(),
                           dgInterval.Rows[i].Cells["SampleTo"].Value.ToString(), lblsampTo.Text.Trim()).Rows.Count > 0)
                        {
                            sMens = "NOK";
                            break;
                        }

                        if (oCh.getCHSamples_getRangeValid(dgInterval.Rows[i].Cells["SampleFrom"].Value.ToString(),
                            dgInterval.Rows[i].Cells["SampleTo"].Value.ToString(), lblsampfrom.Text.Trim()).Rows.Count > 0)
                        {
                            sMens = "NOK";
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (sMens == "")
                {
                    string sSampTo = "";
                    string sSampFrom = "";

                    string code = LoadSampShipmentAllEnt()[CmbSamTyp.SelectedIndex].Code.ToString().Trim();

                    if (code == ConfigurationSettings.AppSettings["Drill"].ToString())
                    {
                        sSampTo = lblsampTo.Text.ToString();
                        sSampFrom = lblsampfrom.Text.ToString();
                    }
                    else if (code == ConfigurationSettings.AppSettings["Rocks"].ToString())
                    {
                        sSampTo = (double.Parse(lblsampTo.Text.ToString()) + 1).ToString();
                        sSampFrom = lblsampfrom.Text.ToString();
                    }
                    else if (code == ConfigurationSettings.AppSettings["Channels"].ToString())
                    {
                        sSampTo = lblsampTo.Text.ToString();
                        sSampFrom = lblsampfrom.Text.ToString();
                    }
                    else
                    {
                        sSampTo = lblsampTo.Text.ToString();
                        sSampFrom = lblsampfrom.Text.ToString();
                    }

                    if (clsLabSumitInterval.dtIntervals.Columns.Count == 0)
                    {
                        clsLabSumitInterval.dtIntervals.Columns.Add("ID", typeof(String));
                        clsLabSumitInterval.dtIntervals.Columns.Add("SampleFrom", typeof(String));
                        clsLabSumitInterval.dtIntervals.Columns.Add("SampleTo", typeof(String));
                        clsLabSumitInterval.dtIntervals.Columns.Add("TotalSample", typeof(String));
                        clsLabSumitInterval.dtIntervals.Columns.Add("IdFrom", typeof(String));
                        clsLabSumitInterval.dtIntervals.Columns.Add("IdTo", typeof(String));
                    }

                    DataRow dr = clsLabSumitInterval.dtIntervals.NewRow();
                    dr["ID"] = iInterval.ToString(); //Variable para identificar el registro en ambos grid

                    if (code == ConfigurationSettings.AppSettings["Drill"].ToString())
                    {

                        dr["SampleFrom"] = a.Text.ToString();
                        dr["SampleTo"] = CmbTS.Text.ToString();
                        dr["TotalSample"] = oDHSamp.getDHSamplesBySamp(a.Text.ToString(), CmbTS.Text.ToString()).Rows.Count;

                    }
                    else if (code == ConfigurationSettings.AppSettings["Rocks"].ToString())
                    {
                        dr["SampleFrom"] = a.SelectedValue.ToString();
                        dr["SampleTo"] = CmbTS.SelectedValue.ToString();
                        dr["TotalSample"] = oRock.getGC_SamplesRockById(a.SelectedValue.ToString(), CmbTS.SelectedValue.ToString()).Rows.Count;

                    }
                    else if (code == ConfigurationSettings.AppSettings["Channels"].ToString())
                    {
                        dr["SampleFrom"] = a.Text.ToString();
                        dr["SampleTo"] = CmbTS.Text.ToString();

                        if (cmbHoleId.SelectedValue == null)
                        {
                            oCh.sCHId = "";
                        }
                        else
                        {
                            oCh.sCHId = cmbHoleId.SelectedValue.ToString();
                        }
                        dr["TotalSample"] = oCh.getCHSamplesBySample(a.Text.ToString(), CmbTS.Text.ToString()).Rows.Count;

                    }
                    else
                    {
                        if (this.cmbHoleId.SelectedValue == null)
                        {
                            oCh.sCHId = "";
                        }
                        else
                        {
                            oCh.sCHId = this.cmbHoleId.SelectedValue.ToString();
                        }
                        int num = 0;
                        if (this.oDHSamp.sHoleID != null && this.oDHSamp.sHoleID != string.Empty)
                        {
                            num = this.oDHSamp.getDHSamplesBySamp(this.a.Text.ToString(), this.CmbTS.Text.ToString()).Rows.Count;
                        }
                        dr["SampleFrom"] = this.a.SelectedValue.ToString();
                        dr["SampleTo"] = this.CmbTS.SelectedValue.ToString();
                        dr["TotalSample"] = oCh.getCHSamplesBySample(this.a.Text.ToString(), this.CmbTS.Text.ToString()).Rows.Count + oRock.getGC_SamplesRockById(this.a.SelectedValue.ToString(), this.CmbTS.SelectedValue.ToString()).Rows.Count + num;

                    }

                    oDHSamp.sHoleID = cmbHoleId.Text.ToString();
                    dr["IdFrom"] = a.SelectedValue.ToString();
                    dr["IdTo"] = CmbTS.SelectedValue.ToString();

                    clsLabSumitInterval.dtIntervals.Rows.Add(dr);
                    dgInterval.DataSource = clsLabSumitInterval.dtIntervals;
                    if (clsLabSumitInterval.dtIntervals.Rows.Count > 0)
                    {
                        dgInterval.Columns[0].Visible = false;
                    }
                    //Intervals Detail 
                    if (clsLabsumitIn.dtIn.Columns.Count == 0)
                    {
                        DataColumn DCAutoInc = clsLabsumitIn.dtIn.Columns.Add("Con", typeof(Int32));
                        DCAutoInc.AutoIncrement = true;
                        DCAutoInc.AutoIncrementSeed = 1;
                        DCAutoInc.AutoIncrementStep = 1;
                        clsLabsumitIn.dtIn.Columns.Add("IDInterval", typeof(String));
                        clsLabsumitIn.dtIn.Columns.Add("Sample", typeof(String));
                        clsLabsumitIn.dtIn.Columns.Add("Sack", typeof(String));
                        clsLabsumitIn.dtIn.Columns.Add("IdAut", typeof(int));
                    }

                    string sCantSack = "0";

                    //Asigno los valores al grid de detalle intervalo
                    DataTable dtDetail = new DataTable();
                    if (code == ConfigurationSettings.AppSettings["Drill"].ToString())
                    {

                        dtDetail = oDHSamp.getDHSamplesBySamp(a.Text.ToString(), CmbTS.Text.ToString());
                        if (!SampleType.Contains("Drill"))
                        {
                            SampleType = string.Concat(SampleType, "Drill,");
                        }

                        for (int i = 0; i < dtDetail.Rows.Count; i++)
                        {

                            DataRow drDetail = clsLabsumitIn.dtIn.NewRow();
                            drDetail["IDInterval"] = iInterval.ToString();
                            drDetail["Sample"] = dtDetail.Rows[i]["Sample"].ToString();
                            drDetail["Sack"] = iSackos.ToString();
                            drDetail["IdAut"] = dtDetail.Rows[i]["SKDHSamples"].ToString();
                            sCantSack = iSackos.ToString();
                            clsLabsumitIn.dtIn.Rows.Add(drDetail);

                            if ((iCont % int.Parse(ConfigurationSettings.AppSettings["CantSackos"].ToString())) == 0)
                            {
                                iSackos += 1;
                            }

                            iCont += 1;

                        }

                    }
                    else if (code == ConfigurationSettings.AppSettings["Rocks"].ToString())
                    {
                        dtDetail = oRock.getGC_SamplesRockById(a.SelectedValue.ToString(), CmbTS.SelectedValue.ToString());

                        if (!SampleType.Contains("Rocks"))
                        {
                            SampleType = string.Concat(SampleType, "Rocks,");
                        }

                        for (int i = 0; i < dtDetail.Rows.Count; i++)
                        {

                            DataRow drDetail = clsLabsumitIn.dtIn.NewRow();
                            drDetail["IDInterval"] = iInterval.ToString();
                            drDetail["Sample"] = dtDetail.Rows[i]["Sample"].ToString();
                            drDetail["Sack"] = iSackos.ToString();
                            drDetail["IdAut"] = dtDetail.Rows[i]["SKSamplesRock"].ToString();
                            sCantSack = iSackos.ToString();
                            clsLabsumitIn.dtIn.Rows.Add(drDetail);

                            if ((iCont % int.Parse(ConfigurationSettings.AppSettings["CantSackos"].ToString())) == 0)
                            {
                                iSackos += 1;
                            }

                            iCont += 1;

                        }
                    }
                    else if (code == ConfigurationSettings.AppSettings["Channels"].ToString())
                    {
                        if (cmbHoleId.SelectedValue == null)
                        {
                            oCh.sCHId = "";
                        }
                        else
                        {
                            oCh.sCHId = cmbHoleId.SelectedValue.ToString();
                        }

                        dtDetail = oCh.getCHSamplesBySample(a.Text.ToString(), CmbTS.Text.ToString());

                        if (!SampleType.Contains("Channels"))
                        {
                            SampleType = string.Concat(SampleType, "Channels,");
                        }

                        for (int i = 0; i < dtDetail.Rows.Count; i++)
                        {

                            DataRow drDetail = clsLabsumitIn.dtIn.NewRow();
                            drDetail["IDInterval"] = iInterval.ToString();
                            drDetail["Sample"] = dtDetail.Rows[i]["Sample"].ToString();
                            drDetail["Sack"] = iSackos.ToString();
                            drDetail["IdAut"] = dtDetail.Rows[i]["SKCHSamples"].ToString();
                            sCantSack = iSackos.ToString();
                            clsLabsumitIn.dtIn.Rows.Add(drDetail);

                            if ((iCont % int.Parse(ConfigurationSettings.AppSettings["CantSackos"].ToString())) == 0)
                            {
                                iSackos += 1;
                            }

                            iCont += 1;

                        }

                    }

                    txtBags.Text = sCantSack.ToString();

                    dgDetalleInterval.DataSource = clsLabsumitIn.dtIn;
                    if (clsLabsumitIn.dtIn.Rows.Count > 0)
                    {
                        dgDetalleInterval.Columns[1].Visible = false;
                        dgDetalleInterval.Columns[4].Visible = false;
                    }
                    //Variable para identificar el registro en ambos grid
                    iInterval += 1;
                }
                else if (sMens == "NOK")
                {
                    MessageBox.Show("Range Invalid.", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void dgInterval_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgInterval.Rows.Count > 1)
                {
                    for (int i = dgDetalleInterval.Rows.Count - 2; i >= 0; i += -1)
                    {

                        if (clsLabsumitIn.dtIn.Rows[i][1].ToString() == dgInterval.Rows[e.RowIndex].Cells[0].Value.ToString())
                        {
                            clsLabsumitIn.dtIn.Rows[i].Delete();
                        }
                    }
                    dgDetalleInterval.DataSource = clsLabsumitIn.dtIn;
                    clsLabSumitInterval.dtIntervals.Rows[e.RowIndex].Delete();
                    dgInterval.DataSource = clsLabSumitInterval.dtIntervals;
                    clsLabsumitIn.dtIn = new DataTable();
                    Init_DtIn();
                    initVbles();

                    for (int i = 0; i < dgDetalleInterval.Rows.Count - 1; i++)
                    {
                        DataRow drDetail = clsLabsumitIn.dtIn.NewRow();
                        drDetail["IDInterval"] = dgDetalleInterval.Rows[i].Cells[1].Value.ToString();
                        drDetail["Sample"] = dgDetalleInterval.Rows[i].Cells[2].Value.ToString();
                        drDetail["Sack"] = iSackos.ToString();
                        clsLabsumitIn.dtIn.Rows.Add(drDetail);

                        if ((iCont % int.Parse(ConfigurationSettings.AppSettings["CantSackos"].ToString())) == 0)
                        {
                            iSackos += 1;
                        }

                        iCont += 1;
                    }

                    dgDetalleInterval.DataSource = clsLabsumitIn.dtIn;
                }
                else
                {
                    MessageBox.Show("Select a record", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete row error: " + ex.Message);
            }
        }

        private string Save_LabSubmit()
        {
            try
            {
                DateTime dDate;
                string result;

                if (bcmbReanalisis)
                {
                    //se llena la variable opcion para traer el consecutivo de reanalisis 
                    oLabS.sOpcion = "4";
                    oLabS.sSubmit = cmbShipment.Text.ToString();

                    //Se llenan las variables sSubmit de las tres clases para el envio 
                    DataTable labSubmit = this.oLabS.getLabSubmit();
                    if (!(labSubmit.Rows[0]["Submit"].ToString() != "NOK"))
                    {
                        MessageBox.Show("No Shipment samples for ReAssay", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        result = "No Shipment samples for ReAssay";
                        return result;
                    }
                    this.oLabS.sSubmit = labSubmit.Rows[0]["Submit"].ToString();
                    this.oLabSIn.sSubmit = labSubmit.Rows[0]["Submit"].ToString();
                    this.oLabSInterval.sSubmit = labSubmit.Rows[0]["Submit"].ToString();
                    this.oLabS.sOpcion = "1";
                }
                else
                {
                    oLabS.sSubmit = cmbShipment.Text.ToString();
                }

                var culturaCol = CultureInfo.GetCultureInfo("es-CO");
                dDate = Convert.ToDateTime(txtDate.Text.ToString(), culturaCol);
                //Para capturar la fecha en el formato correcto
                //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                //dDate = DateTime.Parse(txtDate.Text.ToString());
                string sDate = dDate.Year.ToString().PadLeft(4, '0') + dDate.Month.ToString().PadLeft(2, '0') +
                    dDate.Day.ToString().PadLeft(2, '0');

                if (cmbDisp.Text.Contains("Select"))
                {
                    MessageBox.Show("El valor del campo Dispatched by es requerido");
                    return "";
                }
                else
                {
                    if (cmbLab.Text.Contains("Select"))
                    {
                        MessageBox.Show("El valor del campo Laboratory es requerido");
                        return "";
                    }
                    else
                    {
                        if (cmbPrepLab.Text.Contains("Select"))
                        {
                            MessageBox.Show("El valor del campo Preparation Lab es requerido");
                            return "";
                        }
                        else
                        {
                            if (cmbPrepCode.Text.Contains("Select"))
                            {
                                MessageBox.Show("El valor del campo Preparation Code es requerido");
                                return "";
                            }
                            else
                            {
                                if (CmbAnCod.Text.Contains("Select"))
                                {
                                    MessageBox.Show("El valor del campo Au Analisis Code es requerido");
                                    return "";
                                }
                                else
                                {
                                    if (cmbAnLab.Text.Contains("Select"))
                                    {
                                        MessageBox.Show("El valor del campo Analitical Lab es requerido");
                                        return "";
                                    }
                                    else
                                    {
                                        if (CmbTypSub.SelectedIndex == 5)
                                        {
                                            MessageBox.Show("El valor del campo Shipment Type es requerido");
                                            return "";
                                        }
                                        else
                                        {
                                            if (CmbNatSamp.SelectedIndex == 4)
                                            {
                                                MessageBox.Show("El valor del campo Nature of Sample es requerido");
                                                return "";
                                            }
                                            else
                                            {
                                                oLabS.sDateShipment = sDate.ToString();
                                                oLabS.sLabCode = cmbLab.Text.ToString();
                                                oLabS.sPreparationLab = cmbPrepLab.Text.ToString();
                                                oLabS.sPrepCode = cmbPrepCode.SelectedValue.ToString();
                                                oLabS.sDispatchedBy = cmbDisp.SelectedValue.ToString();
                                                oLabS.sAnalisysCode = CmbAnCod.Text.ToString();
                                                oLabS.sInstructions = txtInstructions.Text.ToString();
                                                oLabS.sReturnResults = "";
                                                oLabS.sOtherAnalisysCode = TxtOtAnCod.Text.ToString();
                                                oLabS.sElement = txtElements.Text.ToString();
                                                oLabS.iNoBags = int.Parse(txtBags.Text.ToString());
                                                oLabS.sObservations = txtObserv.Text.ToString();
                                                oLabS.sMetAnalisysCode = txtMetAnCod.Text.ToString();
                                                oLabS.sAnaliticalLab = cmbAnLab.Text.ToString();

                                                string sSampleType = LoadSampShipmentAllEnt()[CmbSamTyp.SelectedIndex].Code.ToString().Trim();
                                                SampleType = SampleType.TrimEnd(',');
                                                string[] countType = SampleType.Split(',');

                                                if (countType.Count() > 1)
                                                {
                                                    oLabS.sSampleType = "CRD";//CmbSamTyp.SelectedValue.ToString()
                                                }
                                                else
                                                {
                                                    oLabS.sSampleType = sSampleType;
                                                }

                                                string code = LoadSampShipmentAllEnt()[CmbSamTyp.SelectedIndex].Code.ToString().Trim();
                                                string sSampleNature = LoadSampleofnatureAllEnt()[CmbNatSamp.SelectedIndex].Id.ToString().Trim();
                                                oLabS.sSampleNature = sSampleNature;//CmbNatSamp.SelectedValue.ToString();
                                                string sTypeSumit = LoadShipmentAllTypSubEntity()[CmbTypSub.SelectedIndex].Code.ToString().Trim();
                                                oLabS.sTypeSumit = sTypeSumit; //CmbTypSub.SelectedValue.ToString();
                                                oLabS.sHoleID = cmbHoleId.Text.ToString();
                                                oLabS.sObservations = txtObserv.Text.ToString();
                                                string sRes = oLabS.LabSubmit_Add();

                                                if (sRes.Contains("OK"))
                                                {
                                                    SampleType = string.Empty;
                                                }

                                                //Insertar el registro para el historial de transacciones por usuario
                                                string sTrans = "";
                                                if (bcmbModify == true)
                                                {
                                                    sTrans = "Update";
                                                }
                                                else
                                                {
                                                    sTrans = "Insert";
                                                }

                                                oRf.InsertTrans("LabSubmit", sTrans.ToString(), clsRf.sUser.ToString(),
                                                                " DateShipment = " + sDate.ToString() +
                                                                ". Type = " + sSampleType.ToString() +
                                                                ". TypeSbumit = " + sTypeSumit.ToString());


                                                return sRes;

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string Save_LabSubmitInterval()
        {
            try
            {
                string sRes = "";
                for (int i = 0; i < dgInterval.Rows.Count - 1; i++)
                {
                    oLabSInterval.sOpcion = "1";
                    if (!bcmbReanalisis)
                    {
                        oLabSInterval.sSubmit = cmbShipment.Text.ToString();
                    }
                    oLabSInterval.sSampleFrom = dgInterval.Rows[i].Cells[1].Value.ToString();
                    oLabSInterval.sSampleTo = dgInterval.Rows[i].Cells[2].Value.ToString();
                    oLabSInterval.lTotalSample = int.Parse(dgInterval.Rows[i].Cells[3].Value.ToString());
                    //oLabSInterval.sSampleType = CmbSamTyp.SelectedValue.ToString();
                    //oLabSInterval.sPrepCode = "";
                    //oLabSInterval.sAnalisysCode = "";
                    //oLabSInterval.sElement = "";
                    //oLabSInterval.sSampleNature = "";

                    sRes = oLabSInterval.LabSubmitInterval_Add();

                    string sTrans = "";
                    if (bcmbModify)
                    {
                        sTrans = "Update";
                    }
                    else
                    {
                        sTrans = "Insert";
                    }
                    //Insertar el registro para el historial de transacciones por usuario
                    oRf.InsertTrans("LabSumitInterval", sTrans, clsRf.sUser.ToString(),
                                    " Submit = " + cmbShipment.Text.ToString() +
                                    ". SampleFrom = " + dgInterval.Rows[i].Cells[1].Value.ToString() +
                                    ". SampleTo = " + dgInterval.Rows[i].Cells[2].Value.ToString());

                }

                return sRes.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string Save_LabSubmitIn()
        {
            try
            {
                string sRes = "";
                for (int i = 0; i < dgDetalleInterval.Rows.Count - 1; i++)
                {

                    oLabSIn.sOpcion = "1";
                    if (!bcmbReanalisis)
                    {
                        oLabSIn.sSubmit = cmbShipment.Text.ToString();
                    }
                    oLabSIn.iCon = int.Parse(dgDetalleInterval.Rows[i].Cells[0].Value.ToString());
                    oLabSIn.sSample = dgDetalleInterval.Rows[i].Cells[2].Value.ToString();
                    oLabSIn.iSack = int.Parse(dgDetalleInterval.Rows[i].Cells[3].Value.ToString());
                    oLabSIn.iWeight = 0;
                    oLabSIn.iId = int.Parse(dgDetalleInterval.Rows[i].Cells[4].Value.ToString());

                    sRes = oLabSIn.LabSubmitIn_Add();

                    //Actualizo assay cuando se reenvia una muestra
                    if (bcmbReanalisis)
                    {
                        oAssay.sSubmit = cmbShipment.Text.ToString();
                        oAssay.sSample = dgDetalleInterval.Rows[i].Cells["Sample"].Value.ToString();
                        oAssay.Assay_Edit();
                    }

                }
                return sRes.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void Unload_DtStatics()
        {
            try
            {
                clsLabsumitIn.dtIn = null;
                clsLabSumitInterval.dtIntervals = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void SaveShipment()
        {
            try
            {
                string sSaveL, sSaveLint, sSaveLin, sSaveAssay;

                if (bcmbModify == true)
                {
                    //Validar si tiene permiso para modificar 
                    DataRow[] dato = clsRf.dsPermisos.Tables[1].Select("nombre_Real_Form = 'frmEnvios' and Accion = 'Modificar'");
                    if (dato.Length > 0)
                    {
                        oLabSInterval.sSubmit = cmbShipment.Text.ToString();
                        oLabSInterval.LabSubmitInterval_Delete();
                        oLabSIn.sSubmit = cmbShipment.Text.ToString();
                        oLabSIn.LabSubmitIn_Delete();

                        oLabS.sOpcion = "2";
                    }
                    else
                    {
                        return;
                    }

                }
                else
                {
                    //Validar si tiene permiso para insertar 
                    DataRow[] dato = clsRf.dsPermisos.Tables[1].Select("nombre_Real_Form = 'frmEnvios' and Accion = 'Insertar'");
                    if (dato.Length > 0)
                    {
                        oLabS.sOpcion = "1";
                    }
                    else
                    {
                        return;
                    }
                }

                if (bcmbFind == false)
                {
                    if (dgInterval.Rows.Count > 1 && dgDetalleInterval.Rows.Count > 1)
                    {

                        sSaveL = Save_LabSubmit();
                        if (sSaveL == "OK")
                        {
                            sSaveLint = Save_LabSubmitInterval();
                            if (sSaveLint == "OK")
                            {
                                sSaveLin = Save_LabSubmitIn();

                                if (sSaveLin == "OK")
                                {
                                    MessageBox.Show("Suseccsfull shipment", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //Unload_DtStatics();
                                    //ControlsClean();
                                    //LoadCmb();
                                    //Load_Combos();
                                    UnloadObjects();
                                }

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No records in Interval and Sack and Samples", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (bcmbFind == true && bcmbReanalisis == true)
                {
                    sSaveL = Save_LabSubmit();
                    sSaveLint = Save_LabSubmitInterval();
                    sSaveLin = Save_LabSubmitIn();


                    sSaveAssay = oAssay.Assay_Edit();

                    if (sSaveL == "OK" && sSaveLint == "OK" && sSaveLin == "OK")
                    {
                        MessageBox.Show("Suseccsfull shipment reanalysis", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Unload_DtStatics();
                        //ControlsClean();
                        //LoadCmb();
                        //Load_Combos();
                        UnloadObjects();
                    }

                }
                else
                {
                    MessageBox.Show("Modify Click", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //Si activo o no el boton modificar y reanalisis, igual inactivo la bandera bcmbModify y bcmbReanalisis
                bcmbModify = false;
                bcmbReanalisis = false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void UnloadObjects()
        {
            try
            {
                Unload_DtStatics();
                ControlsClean();
                //LoadCmb();
                //Load_Combos();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isRean == false)
                {
                    string val = LoadShipmentAllTypSubEntity()[CmbTypSub.SelectedIndex].Code.ToString();

                    if (val == "RE")
                    {
                        bcmbReanalisis = true;
                    }

                    SaveShipment();
                    indexSelectCmbSam = -1;
                    modificate = false;
                }
                else
                {
                    MessageBox.Show("Shipment is reanalysis", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Save Error: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgInterval.Rows.Count > 1)
                {
                    for (int i = dgDetalleInterval.Rows.Count - 2; i >= 0; i += -1)
                    {
                        if (clsLabsumitIn.dtIn.Rows[i][1].ToString() == dgInterval.Rows[dgInterval.SelectedRows[0].Index].Cells[0].Value.ToString())
                        {
                            clsLabsumitIn.dtIn.Rows[i].Delete();
                        }
                    }
                    dgDetalleInterval.DataSource = clsLabsumitIn.dtIn;

                    clsLabSumitInterval.dtIntervals.Rows[dgInterval.SelectedRows[0].Index].Delete();

                    dgInterval.DataSource = clsLabSumitInterval.dtIntervals;
                    clsLabsumitIn.dtIn = new DataTable();

                    Init_DtIn();
                    initVbles();

                    for (int i = 0; i < dgDetalleInterval.Rows.Count - 1; i++)
                    {
                        DataRow drDetail = clsLabsumitIn.dtIn.NewRow();
                        drDetail["IDInterval"] = dgDetalleInterval.Rows[i].Cells[1].Value.ToString();
                        drDetail["Sample"] = dgDetalleInterval.Rows[i].Cells[2].Value.ToString();
                        drDetail["Sack"] = iSackos.ToString();
                        drDetail["IdAut"] = dgDetalleInterval.Rows[i].Cells[4].Value.ToString();
                        clsLabsumitIn.dtIn.Rows.Add(drDetail);

                        if ((iCont % int.Parse(ConfigurationSettings.AppSettings["CantSackos"].ToString())) == 0)
                        {
                            iSackos += 1;
                        }

                        iCont += 1;
                    }

                    dgDetalleInterval.DataSource = clsLabsumitIn.dtIn;
                }
                else
                {
                    MessageBox.Show("Select a record", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete row error: " + ex.Message);
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            //Validar si tiene permiso para generar reportes 
            DataRow[] dato = clsRf.dsPermisos.Tables[1].Select("nombre_Real_Form = 'frmEnvios' and Accion = 'Generar Reportes'");
            if (dato.Length > 0)
            {
                Excel.Application oXL;
                Excel._Workbook oWB;
                Excel._Worksheet oSheet;
                Excel.Range oRng;
                Excel._Worksheet oSheet2;
                Excel._Worksheet oSheet3;

                try
                {
                    if (cmbLab.SelectedValue.ToString() == "ACME")
                    {
                        #region report Acme

                        oXL = new Excel.Application();
                        //oXL.Visible = true;
                        oWB = oXL.Workbooks.Open(ConfigurationSettings.AppSettings["Ruta_ExcelAcme"].ToString(), 0, true, 5,
                        Type.Missing, Type.Missing, false, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, null, null);

                        oSheet = //(Excel._Worksheet)oWB.ActiveSheet;
                        (Excel._Worksheet)oWB.Sheets[1];

                        //oSheet.get_Range("M2", "N2").MergeCells = true;
                        //oSheet.get_Range("M2", "N2").Value2 = txtDate.Text.ToString();


                        int iTotalMuestras = 0;
                        int iInicial = 25;
                        for (int i = 0; i < dgInterval.Rows.Count - 1; i++)
                        {
                            iInicial += 1;

                            oSheet.get_Range("B" + iInicial.ToString(), "C" + iInicial.ToString()).MergeCells = true;
                            oSheet.get_Range("B" + iInicial.ToString(), "C" + iInicial.ToString()).Value2 = CmbSamTyp.SelectedValue.ToString();

                            oSheet.get_Range("D" + iInicial.ToString(), "E" + iInicial.ToString()).MergeCells = true;
                            oSheet.get_Range("D" + iInicial.ToString(), "E" + iInicial.ToString()).Value2 = dgInterval.Rows[i].Cells[3].Value.ToString();

                            oSheet.get_Range("F" + iInicial.ToString(), "K" + iInicial.ToString()).MergeCells = true;
                            oSheet.get_Range("F" + iInicial.ToString(), "K" + iInicial.ToString()).Value2 = dgInterval.Rows[i].Cells[1].Value.ToString() + "-"
                                + dgInterval.Rows[i].Cells[2].Value.ToString();

                            oSheet.get_Range("L" + iInicial.ToString(), "M" + iInicial.ToString()).MergeCells = true;
                            oSheet.get_Range("L" + iInicial.ToString(), "M" + iInicial.ToString()).Value2 = cmbPrepCode.Text.ToString();

                            oSheet.get_Range("N" + iInicial.ToString(), "T" + iInicial.ToString()).MergeCells = true;
                            oSheet.get_Range("N" + iInicial.ToString(), "T" + iInicial.ToString()).Value2 = txtElements.Text.ToString();

                            oSheet.get_Range("B" + iInicial.ToString(), "U" + iInicial.ToString()).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                            iTotalMuestras += int.Parse(dgInterval.Rows[i].Cells[3].Value.ToString());


                        }

                        //Borrar filas restantes
                        //int iDelete = 35 + dgInterval.Rows.Count;
                        //for (int i = 0; i < 15 - dgInterval.Rows.Count; i++)
                        //{
                        //    oSheet.get_Range("B" + iDelete.ToString(), "B" + iDelete.ToString()).EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        //}


                        //Escribir en la hoja 2
                        oSheet2 = (Excel._Worksheet)oWB.Sheets[2];
                        int iInicialH2 = 5;
                        for (int iHoja2 = 0; iHoja2 < dgDetalleInterval.Rows.Count - 1; iHoja2++)
                        {

                            iInicialH2 += 1;

                            oSheet2.Cells[iInicialH2, 1] = dgDetalleInterval.Rows[iHoja2].Cells["Sack"].Value.ToString();
                            oSheet2.Cells[iInicialH2, 2] = dgDetalleInterval.Rows[iHoja2].Cells["Con"].Value.ToString();
                            oSheet2.Cells[iInicialH2, 3] = dgDetalleInterval.Rows[iHoja2].Cells["Sample"].Value.ToString();


                        }

                        //oSheet2.Cells[6, 3] = "Muestra ....";
                        //FIN. Escribir en la hoja 2



                        /*
                      CmbSamTyp
                      DC	Diamond Drill Core
                      PR	Pulp
                      RO	Rocks
                      RR	Rejects
                      CH	Channels*/

                        if (CmbSamTyp.SelectedValue.ToString() == "DC" ||
                            CmbSamTyp.SelectedValue.ToString() == "CH")
                        {
                            oSheet.get_Range("B39", "U39").MergeCells = true;
                            oSheet.get_Range("B39", "U39").Value2 =
                                "Preparar dos pulpas de 250gr, enviar una a Chile y retener la otra en el laboratorio hasta entrega al cliente";

                            oSheet.get_Range("B40", "U40").MergeCells = true;
                            oSheet.get_Range("B40", "U40").Value2 =
                                "Todos los resultados de Au >10ppm reanalizarlos por Au-G612, hacer una repetición y reportar también el segundo resultado.";

                            oSheet.get_Range("B41", "U41").MergeCells = true;
                            oSheet.get_Range("B41", "U41").Value2 =
                                "Todos los resultados de Ag >1000ppm reanalizarlos por  Ag-G613, hacer una repetición y reportar también el segundo resultado.";

                        }
                        //else if (CmbSamTyp.SelectedValue.ToString() == "CH")
                        //{
                        //    oSheet.get_Range("B39", "U39").MergeCells = true;
                        //    oSheet.get_Range("B39", "U39").Value2 =
                        //        "Preparar dos pulpas de 250gr, enviar una a Chile y retener la otra en el laboratorio hasta entrega al cliente";

                        //    oSheet.get_Range("B40", "U40").MergeCells = true;
                        //    oSheet.get_Range("B40", "U40").Value2 =
                        //        "Todos los resultados de Au >10ppm reanalizarlos por Au-G612, hacer una repetición y reportar también el segundo resultado.";

                        //    oSheet.get_Range("B41", "U41").MergeCells = true;
                        //    oSheet.get_Range("B41", "U41").Value2 =
                        //        "Todos los resultados de Ag >1000ppm reanalizarlos por  Ag-G613, hacer una repetición y reportar también el segundo resultado.";
                        //}
                        else if (CmbSamTyp.SelectedValue.ToString() == "PR")
                        {
                            oSheet.get_Range("B39", "U39").MergeCells = true;
                            oSheet.get_Range("B39", "U39").Value2 =
                                "Rehomogenizar las pulpas";

                            oSheet.get_Range("B40", "U40").MergeCells = true;
                            oSheet.get_Range("B40", "U40").Value2 =
                                "Todos los resultados de Au >10ppm reanalizarlos por Au-G612, hacer una repetición y reportar también el segundo resultado.";

                            oSheet.get_Range("B41", "U41").MergeCells = true;
                            oSheet.get_Range("B41", "U41").Value2 =
                                "";
                        }
                        else if (CmbSamTyp.SelectedValue.ToString() == "RR")
                        {
                            oSheet.get_Range("B39", "U39").MergeCells = true;
                            oSheet.get_Range("B39", "U39").Value2 =
                                "Preparar nuevas pulpas a partir de los rechazos gruesos de las muestras";

                            oSheet.get_Range("B40", "U40").MergeCells = true;
                            oSheet.get_Range("B40", "U40").Value2 =
                                "Todos los resultados de Au >10ppm reanalizarlos por Au-G612, hacer una repetición y reportar también el segundo resultado.";

                            oSheet.get_Range("B41", "U41").MergeCells = true;
                            oSheet.get_Range("B41", "U41").Value2 =
                                "";

                        }


                        oXL.Visible = true;
                        oXL.UserControl = true;

                        #endregion report Acme
                    }
                    else if (cmbLab.SelectedValue.ToString() == "ALSCHEMEX")
                    {
                        #region report ALS

                        Word.Document oWordDoc = new Microsoft.Office.Interop.Word.Document();
                        Word.Application oWord = new Microsoft.Office.Interop.Word.Application();
                        object ostart = 0;
                        object oend = 0;
                        Word.Range oWordRange = oWordDoc.Range(ref ostart, ref oend);
                        //Word.Table oWordTable = null;
                        //Word.Section oWordSection;

                        object file = ConfigurationSettings.AppSettings["Ruta_WordALS"].ToString();
                        //"D:\\TemplateShipmentALScopia.doc"; //Ruta_WordALS
                        object nullobj = System.Reflection.Missing.Value;
                        oWordDoc = oWord.Documents.Open(
                             ref file, ref nullobj, ref nullobj, ref nullobj, ref nullobj,
                             ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj,
                             ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj
                            );

                        oWordDoc.Tables[1].Cell(2, 5).Range.Text = txtDate.Text.ToString();
                        oWordDoc.Tables[1].Cell(4, 3).Range.Text = (int.Parse(cmbShipment.Text.ToString().Substring(6, 4).ToString())).ToString();

                        int iTotalM = 0;
                        for (int i = 0; i < dgInterval.Rows.Count - 1; i++)
                        {
                            oWordDoc.Tables[2].Cell(3 + i, 1).Range.Text = dgInterval.Rows[i].Cells[1].Value.ToString();
                            oWordDoc.Tables[2].Cell(3 + i, 2).Range.Text = dgInterval.Rows[i].Cells[2].Value.ToString();
                            oWordDoc.Tables[2].Cell(3 + i, 3).Range.Text = dgInterval.Rows[i].Cells[3].Value.ToString();
                            oWordDoc.Tables[2].Cell(3 + i, 4).Range.Text = txtElements.Text.ToString();

                            iTotalM += int.Parse(dgInterval.Rows[i].Cells[3].Value.ToString());
                        }


                        oWordDoc.Tables[2].Cell(11, 3).Range.Text = iTotalM.ToString();

                        oWord.Visible = true;

                        //oWord.UserControl = true;

                        #endregion report ALS
                    }
                    else if (cmbLab.SelectedValue.ToString() == "SGS")
                    {
                        #region report SGS

                        oXL = new Excel.Application();
                        oXL.Visible = true;
                        //Get a new workbook.
                        //oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                        //oSheet = (Excel._Worksheet)oWB.ActiveSheet;
                        //oWB = oXL.Workbooks.Open(@"D:/Template_Shipment_Sgs.xls", 0, true, 5,
                        oWB = oXL.Workbooks.Open(ConfigurationSettings.AppSettings["Ruta_ExcelSGS"].ToString(), 0, true, 5,
                        Type.Missing, Type.Missing, false, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, null, null);

                        #region Sheet_1 REQUERIMIENTO DE ANÁLISIS

                        //oSheet = (Excel._Worksheet)oWB.ActiveSheet;
                        oSheet = (Excel._Worksheet)oWB.Sheets[1];

                        //oSheet.get_Range("M2", "N2").MergeCells = true;
                        //oSheet.get_Range("M2", "N2").Value2 = txtDate.Text.ToString();

                        oSheet.Cells[7, 19] = "FECHA: " + txtDate.Text.ToString();
                        oSheet.Cells[7, 14] = "ENVÍO: " + cmbShipment.Text.ToString();

                        int iTotalMuestras = 0;
                        int iInicial = 38;
                        for (int i = 0; i < dgInterval.Rows.Count - 1; i++)
                        {

                            iInicial += 1;

                            //No. Muestras
                            oSheet.Cells[iInicial, 2] = dgInterval.Rows[i].Cells[3].Value.ToString();

                            //Numeracion
                            oSheet.get_Range("C" + iInicial.ToString(), "H" + iInicial.ToString()).MergeCells = true;
                            oSheet.get_Range("C" + iInicial.ToString(), "H" + iInicial.ToString()).Value2 =
                            //oSheet.Cells[iInicial, 3] = 
                            dgInterval.Rows[i].Cells[1].Value.ToString() + "   -   "
                                + dgInterval.Rows[i].Cells[2].Value.ToString();

                            //Tipo de muestra
                            oSheet.get_Range("J" + iInicial.ToString(), "L" + iInicial.ToString()).MergeCells = true;
                            oSheet.get_Range("J" + iInicial.ToString(), "L" + iInicial.ToString()).Value2 = CmbSamTyp.Text.ToString();

                            //Naturaleza de la muestra
                            //oSheet.Cells[iInicial + i, 13] = CmbNatSamp.Text.ToString();

                            //Elementos
                            oSheet.get_Range("N" + iInicial.ToString(), "R" + iInicial.ToString()).MergeCells = true;
                            oSheet.get_Range("N" + iInicial.ToString(), "R" + iInicial.ToString()).Value2 = txtElements.Text.ToString();

                            //Naturaleza de la muestra
                            oSheet.get_Range("M" + iInicial.ToString(), "M" + iInicial.ToString()).MergeCells = true;
                            oSheet.get_Range("M" + iInicial.ToString(), "M" + iInicial.ToString()).Value2 = CmbNatSamp.Text.ToString();

                            //Codigo de Servicio
                            oSheet.get_Range("S" + iInicial.ToString(), "S" + iInicial.ToString()).MergeCells = true;
                            oSheet.get_Range("S" + iInicial.ToString(), "S" + iInicial.ToString()).Value2 = CmbAnCod.Text.ToString() + "-" + txtMetAnCod.Text.ToString();


                            oSheet.get_Range("B" + iInicial.ToString(), "S" + iInicial.ToString()).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                            iTotalMuestras += int.Parse(dgInterval.Rows[i].Cells[3].Value.ToString());


                        }

                        #endregion

                        #region Sheet_2 y Sheet_2( Listado Muestras, Listado_BD)

                        oSheet2 = (Excel._Worksheet)oWB.Sheets[2];
                        oSheet3 = (Excel._Worksheet)oWB.Sheets[3];

                        int iInicialH = 5;

                        for (int i = 0; i < dgDetalleInterval.Rows.Count - 1; i++)
                        {
                            iInicialH += 1;

                            oSheet2.Cells[iInicialH, 1] = dgDetalleInterval.Rows[i].Cells["Sack"].Value.ToString();
                            oSheet2.Cells[iInicialH, 2] = dgDetalleInterval.Rows[i].Cells["Con"].Value.ToString();
                            oSheet2.Cells[iInicialH, 3] = dgDetalleInterval.Rows[i].Cells["Sample"].Value.ToString();

                            oSheet3.Cells[iInicialH, 1] = dgDetalleInterval.Rows[i].Cells["Sack"].Value.ToString();
                            oSheet3.Cells[iInicialH, 2] = dgDetalleInterval.Rows[i].Cells["Con"].Value.ToString();
                            oSheet3.Cells[iInicialH, 3] = dgDetalleInterval.Rows[i].Cells["Sample"].Value.ToString();
                        }

                        #endregion


                        //Borrar filas restantes
                        //int iDelete = 39 + dgInterval.Rows.Count;
                        //for (int i = 0; i < 15 - dgInterval.Rows.Count; i++)
                        //{
                        //    oSheet.get_Range("B" + iDelete.ToString(), "B" + iDelete.ToString()).EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        //}

                        oXL.Visible = true;
                        oXL.UserControl = true;

                        #endregion report SGS
                    }
                }
                catch (Exception theException)
                {
                    String errorMessage;
                    errorMessage = "Error: ";
                    errorMessage = String.Concat(errorMessage, theException.Message);
                    errorMessage = String.Concat(errorMessage, " Line: ");
                    errorMessage = String.Concat(errorMessage, theException.Source);
                    MessageBox.Show(errorMessage, "Error");
                }

            }
            else
            {
                MessageBox.Show("Action is not allowed", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                bcmbModify = true;
                bcmbFind = false;
                Load_FormData();
                indexSelectCmbSam = -1;
                modificate = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void cmbLaboratory_Enter(object sender, EventArgs e)
        {

        }

        private void btnReanalisis_Click(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {
                MessageBox.Show("Reanalisis Error: " + ex.Message);
            }
        }

        private void CmbTypSub_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show(CmbTypSub.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void cmbPrepLab_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbAnLab.SelectedIndex = cmbPrepLab.SelectedIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void cmbLab_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!cmbLab.Text.Contains("Select"))
                {
                    var AnLab = GetRfAnalysisCodeLabEntity();
                    var dataAnLab = AnLab.Where(c => (c.CodeLab == null ? string.Empty : c.CodeLab) == cmbLab.Text).OrderBy(o => o.Code).ToList();
                    cmbAnLab.DisplayMember = "Code";
                    cmbAnLab.ValueMember = "Code";
                    cmbAnLab.DataSource = dataAnLab;
                    cmbAnLab.SelectedIndex = dataAnLab.ToList().Count - 1;

                    var PrepLab = GetRfPreparationCodeEntity();
                    var dataPrepLab = PrepLab.Where(c => (c.CodeLab == null ? string.Empty : c.CodeLab) == cmbLab.Text).OrderBy(o => o.Code).ToList();
                    cmbPrepLab.DisplayMember = "Code";
                    cmbPrepLab.ValueMember = "Code";
                    cmbPrepLab.DataSource = dataPrepLab;
                    cmbPrepLab.SelectedIndex = dataPrepLab.ToList().Count - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private IList<Ent_Prefix> GetRfAnalysisCodeLabEntity()
        {
            try
            {
                var dtShipment = oShipment.getRfAnalysisCodeLabEntity();

                Ent_Prefix pre = new Ent_Prefix();
                pre.Identification = -1;
                pre.Code = "Select an option..";
                dtShipment.Insert(dtShipment.ToList().Count, pre);

                return dtShipment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private IList<Ent_Prefix> GetRfPreparationCodeEntity()
        {
            try
            {
                var dtShipment = oShipment.getRfPreparationCodeEntity();

                Ent_Prefix pre = new Ent_Prefix();
                pre.Identification = -1;
                pre.Code = "Select an option..";
                dtShipment.Insert(dtShipment.ToList().Count, pre);

                return dtShipment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void frmEnvios_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }




        private void btnPruebas_Click(object sender, EventArgs e)
        {

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            DateTime dDate;
            dDate = DateTime.Parse(txtDate.Text.ToString());
            string sDate = dDate.Year.ToString().PadLeft(4, '0') + dDate.Month.ToString().PadLeft(2, '0') +
                dDate.Day.ToString().PadLeft(2, '0');

            MessageBox.Show(sDate.ToString());
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }



        private void CmbFS_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //try
            //{
            //    if (CmbFS.DataSource == null)
            //    {
            //        return;
            //    }
            //    DataTable dtIdF = oDHSamp.getDHSamplesById(CmbFS.SelectedValue.ToString(), CmbFS.SelectedValue.ToString());


            //    if (dtIdF.Rows.Count == 0)
            //    {
            //        return;
            //    }
            //    lblsampfrom.Text = dtIdF.Rows[0]["From"].ToString(); //CmbFS.SelectedValue.ToString();

            //    dtIdF.Clear();
            //    dtIdF.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void CmbTS_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //try
            //{
            //    if (CmbTS.DataSource == null)
            //    {
            //        return;
            //    }
            //    DataTable dtIdT = oDHSamp.getDHSamplesById(CmbTS.SelectedValue.ToString(), CmbTS.SelectedValue.ToString());

            //    if (dtIdT.Rows.Count == 0)
            //    {
            //        return;
            //    }
            //    lblsampTo.Text = dtIdT.Rows[0]["To"].ToString();//CmbTS.SelectedValue.ToString();

            //    dtIdT.Clear();
            //    dtIdT.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void cmbHoleId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = LoadSampShipmentAllEnt()[CmbSamTyp.SelectedIndex].Code.ToString().Trim();


                if (code == ConfigurationSettings.AppSettings["Drill"].ToString())
                {
                    LoadFromTo();
                }
                else
                {
                    if (code == ConfigurationSettings.AppSettings["Channels"].ToString())
                    {
                        LoadFromToChannels();
                    }
                    else
                    {
                        LoadFromTo();
                        LoadFromToChannels();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void CmbFS_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (a.DataSource == null)
                {
                    DataTable dtIdF = new DataTable();
                    string code = LoadSampShipmentAllEnt()[CmbSamTyp.SelectedIndex].Code.ToString().Trim();

                    if (code == ConfigurationSettings.AppSettings["Drill"].ToString())
                    {
                        dtIdF = oDHSamp.getDHSamplesById(a.SelectedValue.ToString(), a.SelectedValue.ToString());

                        if (dtIdF.Rows.Count == 0)
                        {
                            return;
                        }
                        lblsampfrom.Text = dtIdF.Rows[0]["From"].ToString(); //CmbFS.SelectedValue.ToString();
                    }
                    else
                    {
                        string sCadena = a.SelectedValue.ToString();
                        string sNumero = "";
                        string sLetras = "";
                        foreach (char val in sCadena.ToCharArray())
                        {
                            if (char.IsNumber(val))
                            {
                                sNumero = sNumero + val;
                            }
                            else
                            {
                                sLetras = sLetras + val;
                            }
                        }

                        lblsampfrom.Text = sNumero;//CmbFS.SelectedValue.ToString(); 
                    }

                    dtIdF.Clear();
                    dtIdF.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CmbTS_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CmbTS.DataSource != null)
                {
                    DataTable dtIdT = new DataTable();
                    string code = LoadSampShipmentAllEnt()[CmbSamTyp.SelectedIndex].Code.ToString().Trim();

                    if (code == ConfigurationSettings.AppSettings["Drill"].ToString())
                    {
                        dtIdT = oDHSamp.getDHSamplesById(CmbTS.SelectedValue.ToString(), CmbTS.SelectedValue.ToString());
                        if (dtIdT.Rows.Count == 0)
                        {
                            return;
                        }
                        lblsampTo.Text = dtIdT.Rows[0]["To"].ToString();//CmbTS.SelectedValue.ToString();

                    }
                    else
                    {
                        string sCadena = CmbTS.SelectedValue.ToString();
                        string sNumero = "";
                        string sLetras = "";
                        foreach (char val in sCadena.ToCharArray())
                        {
                            if (char.IsNumber(val))
                            {
                                sNumero = sNumero + val;
                            }
                            else
                            {
                                sLetras = sLetras + val;
                            }
                        }

                        lblsampTo.Text = sNumero;//CmbTS.SelectedValue.ToString();
                    }

                    dtIdT.Clear();
                    dtIdT.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CmbSamTyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (a.DataSource == null)
                {
                    a.DataSource = null;
                }

                CmbTS.DataSource = null;
                LoadCmbHoleId();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmEnvios_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnloadObjects();
            this.Dispose(true);
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isSelected)
            {
                LoadCmb1();
            }
        }

        private void cmbShipment_Leave(object sender, EventArgs e)
        {
            Load_FormData();

        }

        private void CmbSamTyp_RightToLeftChanged(object sender, EventArgs e)
        {

        }

        private void CmbSamTyp_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void CmbSamTyp_Leave(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void txtDate_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
