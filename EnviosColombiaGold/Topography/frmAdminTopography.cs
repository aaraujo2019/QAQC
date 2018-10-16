using Entities.Topography;
using RN.Topography;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnviosColombiaGold.Topography
{
    public partial class frmAdminTopography : Form
    {
        IList<MasterTopography> Reader = new List<MasterTopography>();
        bool isSave = false;
        public frmAdminTopography()
        {
            InitializeComponent();

            //cmbMaterID.Items.Insert(-1, "--");
            cmbMaterID.Items.Insert(0, "Activity Type");
            cmbMaterID.Items.Insert(1, "Zone Activity Types");
            cmbMaterID.Items.Insert(2, "Offices");
            cmbMaterID.Items.Insert(3, "Mines Names");
            cmbMaterID.Items.Insert(4, "Topographers");
            cmbMaterID.Items.Insert(5, "Direction");
            cmbMaterID.Items.Insert(6, "Orientation");
            cmbMaterID.Items.Insert(7, "Zone");
            cmbMaterID.Items.Insert(8, "Phase");
            cmbMaterID.Items.Insert(9, "C Cost");
            cmbMaterID.Items.Insert(10, "Sector Type");
            cmbMaterID.Items.Insert(11, "ID selection");
            cmbMaterID.Items.Insert(12, "Labor Type");
            cmbMaterID.Items.Insert(13, "Vein");
            cmbMaterID.Items.Insert(14, "Activity Aditional");

            cmbMaterID.Items.Insert(15, "Topographers");
            

            cmbMaterID.Items.Insert(16, "--");

            this.cmbMaterID.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboTypeCondition.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboConditions.DropDownStyle = ComboBoxStyle.DropDownList;

            grpValueTypeCond.Visible = false;
            grbValueIdSelect.Visible = false;

            this.grpValuelSetings.Location = new System.Drawing.Point(5, 123);
        }



        private void cmbMaterID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int valueEvaluate = cmbMaterID.SelectedIndex;

            if (Reader.Count() == 0 || isSave == true)
                Reader = GetDataTopography.getMaterTopography("usp_TO_getMaterTopography");

            isSave = false;
            dtgDetailMasterID.Columns.Clear();
            dtgDetailMasterID.DataBindings.Clear();
            clearValue(1);
            switch (cmbMaterID.SelectedIndex)
            {
                case 0:
                    List<MasterTopography> zone = Reader.Where(r => r.CodeMaster.Contains("actitype")).ToList();
                    dtgDetailMasterID.DataSource = zone;

                    dtgDetailMasterID.AutoResizeColumns();
                    dtgDetailMasterID.Columns[2].Visible = false;
                    dtgDetailMasterID.Columns[4].Visible = false;
                    dtgDetailMasterID.Columns[5].Visible = false;
                    grbValueIdSelect.Visible = false;
                    grpValueTypeCond.Visible = true;

                    this.grpValuelSetings.Location = new System.Drawing.Point(5, 188);

                    var query = Reader.Where(c => c.CodeMaster.Contains("ZonActTyp")).Select(r => r.Description).ToArray();

                    cboTypeCondition.Items.Clear();



                    cboTypeCondition.Items.AddRange(query.ToArray());
                    cboTypeCondition.SelectedIndex = 0; grpEvent.Visible = true;

                    break;
                case 1:

                    List<MasterTopography> zonzct = Reader.Where(r => r.CodeMaster.Contains("ZonActTyp")).ToList();
                    dtgDetailMasterID.DataSource = zonzct;
                    dtgDetailMasterID.AutoResizeColumns();
                    dtgDetailMasterID.Columns[2].Visible = false;
                    dtgDetailMasterID.Columns[3].Visible = false;
                    dtgDetailMasterID.Columns[4].Visible = false;
                    dtgDetailMasterID.Columns[5].Visible = false;
                    grpValueTypeCond.Visible = false;
                    grbValueIdSelect.Visible = false;
                    this.grpValuelSetings.Location = new System.Drawing.Point(5, 123); grpEvent.Visible = true;
                    break;
                case 2:

                    List<MasterTopography> actoffice = Reader.Where(r => r.CodeMaster.Contains("actoffice")).ToList();
                    dtgDetailMasterID.DataSource = actoffice;
                    dtgDetailMasterID.AutoResizeColumns();
                    dtgDetailMasterID.Columns[2].Visible = false;
                    dtgDetailMasterID.Columns[3].Visible = false;
                    dtgDetailMasterID.Columns[4].Visible = false;
                    dtgDetailMasterID.Columns[5].Visible = false;
                    grpValueTypeCond.Visible = false;
                    grbValueIdSelect.Visible = false;

                    this.grpValuelSetings.Location = new System.Drawing.Point(5, 123); grpEvent.Visible = true;
                    break;
                case 3:
                    List<MasterTopography> actmina = Reader.Where(r => r.CodeMaster.Contains("actmina")).ToList();
                    dtgDetailMasterID.DataSource = actmina;
                    dtgDetailMasterID.AutoResizeColumns();
                    dtgDetailMasterID.Columns[2].Visible = false;
                    dtgDetailMasterID.Columns[3].Visible = false;
                    dtgDetailMasterID.Columns[4].Visible = false;
                    dtgDetailMasterID.Columns[5].Visible = false;
                    grbValueIdSelect.Visible = false;
                    grpValueTypeCond.Visible = false;

                    this.grpValuelSetings.Location = new System.Drawing.Point(5, 123); grpEvent.Visible = true;
                    break;

                ////case 4:
                ////    cmbMaterID.Items.Insert(4, "Topographers"); se deja para ponerlo en lparte de admini crearlo en otro lado
                ////    break;

                case 5:
                    List<MasterTopography> actdirect = Reader.Where(r => r.CodeMaster.Contains("actdirect")).ToList();
                    dtgDetailMasterID.DataSource = actdirect;
                    dtgDetailMasterID.AutoResizeColumns();
                    dtgDetailMasterID.Columns[2].Visible = false;
                    dtgDetailMasterID.Columns[3].Visible = false;
                    dtgDetailMasterID.Columns[4].Visible = false;
                    dtgDetailMasterID.Columns[5].Visible = false;
                    grpValueTypeCond.Visible = false;
                    grbValueIdSelect.Visible = false;

                    this.grpValuelSetings.Location = new System.Drawing.Point(5, 123); grpEvent.Visible = true;
                    break;
                case 6:
                    List<MasterTopography> actorient = Reader.Where(r => r.CodeMaster.Contains("actorient")).ToList();
                    dtgDetailMasterID.DataSource = actorient;
                    dtgDetailMasterID.AutoResizeColumns();
                    dtgDetailMasterID.Columns[2].Visible = false;
                    dtgDetailMasterID.Columns[3].Visible = false;
                    dtgDetailMasterID.Columns[4].Visible = false;
                    dtgDetailMasterID.Columns[5].Visible = false;
                    grpValueTypeCond.Visible = false;
                    grbValueIdSelect.Visible = false;

                    this.grpValuelSetings.Location = new System.Drawing.Point(5, 123); grpEvent.Visible = true;
                    break;
                case 7:
                    List<MasterTopography> actzone = Reader.Where(r => r.CodeMaster.Contains("actzone")).ToList();
                    dtgDetailMasterID.DataSource = actzone;
                    dtgDetailMasterID.AutoResizeColumns();
                    dtgDetailMasterID.Columns[2].Visible = false;
                    dtgDetailMasterID.Columns[3].Visible = false;
                    dtgDetailMasterID.Columns[4].Visible = false;
                    dtgDetailMasterID.Columns[5].Visible = false;
                    grpValueTypeCond.Visible = false;
                    grbValueIdSelect.Visible = false;

                    this.grpValuelSetings.Location = new System.Drawing.Point(5, 123); grpEvent.Visible = true;
                    break;

                case 8:
                    List<MasterTopography> actphase = Reader.Where(r => r.CodeMaster.Contains("actphase")).ToList();
                    dtgDetailMasterID.DataSource = actphase;
                    dtgDetailMasterID.AutoResizeColumns();

                    dtgDetailMasterID.Columns[2].Visible = false;
                    dtgDetailMasterID.Columns[3].Visible = false;
                    dtgDetailMasterID.Columns[4].Visible = false;
                    dtgDetailMasterID.Columns[5].Visible = false;
                    grpValueTypeCond.Visible = false;
                    grbValueIdSelect.Visible = false;
                    this.grpValuelSetings.Location = new System.Drawing.Point(5, 123); grpEvent.Visible = true;
                    break;
                case 9:
                    List<MasterTopography> actccosto = Reader.Where(r => r.CodeMaster.Contains("actccosto")).ToList();
                    dtgDetailMasterID.DataSource = actccosto;
                    dtgDetailMasterID.AutoResizeColumns();
                    dtgDetailMasterID.Columns[2].Visible = false;
                    dtgDetailMasterID.Columns[3].Visible = false;
                    dtgDetailMasterID.Columns[4].Visible = false;
                    dtgDetailMasterID.Columns[5].Visible = false;
                    grpValueTypeCond.Visible = false;
                    grbValueIdSelect.Visible = false;
                    this.grpValuelSetings.Location = new System.Drawing.Point(5, 123); grpEvent.Visible = true;
                    break;
                case 10:
                    List<MasterTopography> actitype = Reader.Where(r => r.CodeMaster.Contains("actsectype")).ToList();
                    dtgDetailMasterID.DataSource = actitype;
                    dtgDetailMasterID.AutoResizeColumns();
                    dtgDetailMasterID.Columns[2].Visible = false;
                    dtgDetailMasterID.Columns[3].Visible = false;
                    dtgDetailMasterID.Columns[4].Visible = false;
                    dtgDetailMasterID.Columns[5].Visible = false;
                    grpValueTypeCond.Visible = false;
                    grbValueIdSelect.Visible = false;
                    this.grpValuelSetings.Location = new System.Drawing.Point(5, 123); grpEvent.Visible = true;
                    break;
                case 11:
                    List<MasterTopography> actrefid = Reader.Where(r => r.CodeMaster.Contains("actrefid")).ToList();
                    dtgDetailMasterID.DataSource = actrefid;
                    dtgDetailMasterID.AutoResizeColumns();
                    dtgDetailMasterID.Columns[2].Visible = false;
                    dtgDetailMasterID.Columns[3].Visible = false;
                    grpValueTypeCond.Visible = false;
                    grbValueIdSelect.Visible = true;

                    this.grpValuelSetings.Location = new System.Drawing.Point(5, 188);

                    cboConditions.Items.Clear();

                    cboConditions.Items.Insert(0, "Is repeated");
                    cboConditions.Items.Insert(1, "Not repeated");
                    cboConditions.Items.Insert(2, "Conditioned");
                    cboConditions.Items.Insert(3, "--");
                    grpEvent.Visible = true;
                    break;
                case 12:
                    List<MasterTopography> actlabtype = Reader.Where(r => r.CodeMaster.Contains("actlabtype")).ToList();
                    dtgDetailMasterID.DataSource = actlabtype;
                    dtgDetailMasterID.AutoResizeColumns();
                    dtgDetailMasterID.Columns[2].Visible = false;
                    dtgDetailMasterID.Columns[3].Visible = false;
                    dtgDetailMasterID.Columns[4].Visible = false;
                    dtgDetailMasterID.Columns[5].Visible = false;
                    grpValueTypeCond.Visible = false;
                    grbValueIdSelect.Visible = false;
                    this.grpValuelSetings.Location = new System.Drawing.Point(5, 123); grpEvent.Visible = true;
                    break;
                case 13:
                    List<MasterTopography> actlabvein = Reader.Where(r => r.CodeMaster.Contains("actlabvein")).ToList();
                    dtgDetailMasterID.DataSource = actlabvein;
                    dtgDetailMasterID.AutoResizeColumns();
                    dtgDetailMasterID.Columns[2].Visible = false;
                    dtgDetailMasterID.Columns[3].Visible = false;
                    dtgDetailMasterID.Columns[4].Visible = false;
                    dtgDetailMasterID.Columns[5].Visible = false;
                    grpValueTypeCond.Visible = false;
                    grbValueIdSelect.Visible = false;
                    this.grpValuelSetings.Location = new System.Drawing.Point(5, 123); grpEvent.Visible = true;
                    break;

                case 14:
                    List<MasterTopography> actaditional = Reader.Where(r => r.CodeMaster.Contains("actadition")).ToList();
                    dtgDetailMasterID.DataSource = actaditional;
                    dtgDetailMasterID.AutoResizeColumns();
                    dtgDetailMasterID.Columns[2].Visible = false;
                    dtgDetailMasterID.Columns[3].Visible = false;
                    dtgDetailMasterID.Columns[4].Visible = false;
                    dtgDetailMasterID.Columns[5].Visible = false;
                    grpValueTypeCond.Visible = false;
                    grbValueIdSelect.Visible = false;
                    this.grpValuelSetings.Location = new System.Drawing.Point(5, 123); grpEvent.Visible = true;
                    break;

                case 15:
                    List<MasterTopography> acttopogr = Reader.Where(r => r.CodeMaster.Contains("acttopogr")).ToList();
                    dtgDetailMasterID.DataSource = acttopogr;

                    dtgDetailMasterID.AutoResizeColumns();
                    dtgDetailMasterID.Columns[2].Visible = false;
                    dtgDetailMasterID.Columns[4].Visible = false;
                    dtgDetailMasterID.Columns[5].Visible = false;
                    grbValueIdSelect.Visible = false;
                    grpValueTypeCond.Visible = true;

                    this.grpValuelSetings.Location = new System.Drawing.Point(5, 188);

                      cboTypeCondition.Items.Clear();

                  
               actoffice = Reader.Where(r => r.CodeMaster.Contains("actoffice")).ToList();


                     query = Reader.Where(c => c.CodeMaster.Contains("actoffice")).Select(r => r.Description).ToArray();

                    cboTypeCondition.Items.AddRange(query.ToArray());
                    cboTypeCondition.SelectedIndex = 0; grpEvent.Visible = true;

                    grpEvent.Visible = true;

                    break;
                case 16:
                    grpValueTypeCond.Visible = false;
                    grbValueIdSelect.Visible = false;
                    grpEvent.Visible = false;
                    break;

                default:
                    break;
            }

        }

        private bool validateValue(int indicate)
        {
            switch (indicate)
            {
                case 1:
                    if (string.IsNullOrEmpty(txtCodeId.Text) || string.IsNullOrEmpty(txtDescriptionId.Text))
                    {
                        MessageBox.Show("El código y descripción es requerida");
                        return false;
                    }

                    break;

                case 2:
                    if (string.IsNullOrEmpty(txtCodeId.Text) || string.IsNullOrEmpty(txtDescriptionId.Text) || string.IsNullOrEmpty(cboTypeCondition.Text))
                    {
                        MessageBox.Show("El código, descripción y el tipo de condición es requerida");
                        return false;
                    }

                    break;

                case 3:
                    if (string.IsNullOrEmpty(txtCodeId.Text) || string.IsNullOrEmpty(txtDescriptionId.Text) || string.IsNullOrEmpty(txtDescrption.Text) || string.IsNullOrEmpty(cboConditions.Text))
                    {
                        MessageBox.Show("El código, descripción, el comentario y el tipo de condición es requerida");
                        return false;
                    }

                    break;

                default:
                    break;
            }
            return true;
        }

        private void clearValue(int value)
        {
            txtCodeId.Text = string.Empty;
            txtDescriptionId.Text = string.Empty;
            txtDescrption.Text = string.Empty;
            btnSave.Enabled = true;

            cboConditions.SelectedIndex = cboConditions.Items.Count - 1;
            dtgDetailMasterID.DataSource = null;

            if (value == 0)
                cmbMaterID.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cmbMaterID.Text))
            {
                switch (cmbMaterID.SelectedIndex)
                {
                    case 0:

                        if (validateValue(2))
                        {
                            IList<MasterTopography> value = Reader.Where(r => r.CodeMaster.Contains("actitype")).ToList();
                            var orientation = value[cboTypeCondition.SelectedIndex].Code.ToString().Trim();

                            GetDataTopography.InsertMaster("USP_To_InsertMaster", txtCodeId.Text, txtDescriptionId.Text, "actitype", orientation, string.Empty, string.Empty);
                            isSave = true;
                        }
                        break;
                    case 1:

                        if (validateValue(1))
                        {
                            GetDataTopography.InsertMaster("USP_To_InsertMaster", txtCodeId.Text, txtDescriptionId.Text, "ZonActTyp", string.Empty, string.Empty, string.Empty);
                            isSave = true;
                        }
                        break;
                    case 2:
                        if (validateValue(1))
                        {
                            GetDataTopography.InsertMaster("USP_To_InsertMaster", txtCodeId.Text, txtDescriptionId.Text, "actoffice", string.Empty, string.Empty, string.Empty);
                            isSave = true;
                        }
                        break;
                    case 3:
                        if (validateValue(1))
                        {
                            GetDataTopography.InsertMaster("USP_To_InsertMaster", txtCodeId.Text, txtDescriptionId.Text, "actmina", string.Empty, string.Empty, string.Empty);
                            isSave = true;
                        }
                        break;

                    ////case 4:
                    ////    cmbMaterID.Items.Insert(4, "Topographers"); se deja para ponerlo en lparte de admini crearlo en otro lado
                    ////    break;

                    case 5:

                        if (validateValue(1))
                        {
                            GetDataTopography.InsertMaster("USP_To_InsertMaster", txtCodeId.Text, txtDescriptionId.Text, "actdirect", string.Empty, string.Empty, string.Empty);
                            isSave = true;
                        }
                        break;
                    case 6:

                        if (validateValue(1))
                        {
                            GetDataTopography.InsertMaster("USP_To_InsertMaster", txtCodeId.Text, txtDescriptionId.Text, "actorient", string.Empty, string.Empty, string.Empty);
                            isSave = true;
                        }
                        break;
                    case 7:
                        if (validateValue(1))
                        {
                            GetDataTopography.InsertMaster("USP_To_InsertMaster", txtCodeId.Text, txtDescriptionId.Text, "actzone", string.Empty, string.Empty, string.Empty);
                            isSave = true;
                        }
                        break;

                    case 8:
                        if (validateValue(1))
                        {
                            GetDataTopography.InsertMaster("USP_To_InsertMaster", txtCodeId.Text, txtDescriptionId.Text, "actphase", string.Empty, string.Empty, string.Empty);
                            isSave = true;
                        }
                        break;
                    case 9:
                        if (validateValue(1))
                        {
                            GetDataTopography.InsertMaster("USP_To_InsertMaster", txtCodeId.Text, txtDescriptionId.Text, "actccosto", string.Empty, string.Empty, string.Empty);
                            isSave = true;
                        }
                        break;
                    case 10:
                        if (validateValue(1))
                        {
                            GetDataTopography.InsertMaster("USP_To_InsertMaster", txtCodeId.Text, txtDescriptionId.Text, "actsectype", string.Empty, string.Empty, string.Empty);
                            isSave = true;
                        }
                        break;
                    case 11:
                        if (validateValue(3))
                        {
                            GetDataTopography.InsertMaster("USP_To_InsertMaster", txtCodeId.Text, txtDescriptionId.Text, "actrefid", string.Empty, cboConditions.Text, txtDescrption.Text);
                            isSave = true;
                        }

                        break;

                    case 12:
                        if (validateValue(1))
                        {
                            GetDataTopography.InsertMaster("USP_To_InsertMaster", txtCodeId.Text, txtDescriptionId.Text, "actlabtype", string.Empty, cboConditions.Text, txtDescrption.Text);
                            isSave = true;
                        }
                        break;

                    case 13:
                        if (validateValue(1))
                        {
                            GetDataTopography.InsertMaster("USP_To_InsertMaster", txtCodeId.Text, txtDescriptionId.Text, "actlabvein", string.Empty, cboConditions.Text, txtDescrption.Text);
                            isSave = true;
                        }
                        break;

                    case 14:
                        if (validateValue(1))
                        {
                            GetDataTopography.InsertMaster("USP_To_InsertMaster", txtCodeId.Text, txtDescriptionId.Text, "actadition", string.Empty, cboConditions.Text, txtDescrption.Text);
                            isSave = true;
                        }

                        break;
                    case 15:

                        if (validateValue(2))
                        {

                            IList<MasterTopography> value = Reader.Where(r => r.CodeMaster.Contains("actoffice")).ToList();
                           var orientation = value[cboTypeCondition.SelectedIndex].Code.ToString().Trim();
                            GetDataTopography.InsertMaster("USP_To_InsertMaster", txtCodeId.Text, txtDescriptionId.Text, "acttopogr", orientation, string.Empty, string.Empty);
                            isSave = true;
                        }
                       
                        break;

                    default:
                        break;
                }
                MessageBox.Show("successful engraving ");
                clearValue(0);
            }
        }

        private void dtgDetailMasterID_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Fill = 0;
            if (dtgDetailMasterID.CurrentCell != null)
            {

                string valiue = dtgDetailMasterID.Rows[e.RowIndex].Cells[2].Value.ToString();
                switch (valiue)
                {
                    case "ZonActTyp":
                    case "actoffice":
                    case "actmina":
                    case "actdirect":
                    case "actorient":
                    case "actzone":
                    case "actphase":
                    case "actccosto":
                    case "actsectype":
                    case "actlabtype":
                    case "actlabvein":
                    case "actadition":
                        

                        txtCodeId.Text = dtgDetailMasterID.Rows[e.RowIndex].Cells[0].Value.ToString();
                        txtCodeId.Enabled = false;
                        txtDescriptionId.Text = dtgDetailMasterID.Rows[e.RowIndex].Cells[1].Value.ToString();
                        btnSave.Enabled = false;
                        break;

                    case "actitype":
                    case "acttopogr":
                        
                        txtCodeId.Text = dtgDetailMasterID.Rows[e.RowIndex].Cells[0].Value.ToString();
                        txtCodeId.Enabled = false;
                        txtDescriptionId.Text = dtgDetailMasterID.Rows[e.RowIndex].Cells[1].Value.ToString();
                        cboTypeCondition.Text = dtgDetailMasterID.Rows[e.RowIndex].Cells[3].Value.ToString();
                        btnSave.Enabled = false;
                        break;

                    case "actrefid":
                        txtCodeId.Text = dtgDetailMasterID.Rows[e.RowIndex].Cells[0].Value.ToString();
                        txtCodeId.Enabled = false;
                        txtDescriptionId.Text = dtgDetailMasterID.Rows[e.RowIndex].Cells[1].Value.ToString();
                        cboConditions.Text = dtgDetailMasterID.Rows[e.RowIndex].Cells[4].Value.ToString();
                        txtDescrption.Text = dtgDetailMasterID.Rows[e.RowIndex].Cells[5].Value.ToString();
                        btnSave.Enabled = false;
                        break;

                    default:
                        break;
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //clearValue(0);

            if (!String.IsNullOrEmpty(cmbMaterID.Text))
            {
                //List<MasterTopography> valueMax= /*null*/;
                switch (cmbMaterID.SelectedIndex)
                {
                    case 0:

                        txtCodeId.Enabled = false;
                        //var value1 = Reader.Where(r => r.CodeMaster.Contains("actitype")).Max(p => p.Code.Trim());///.ToList().Max(p=>p.Code.Trim());
                        var q = Reader.Where(r => r.CodeMaster.Contains("actitype")).ToList().OrderByDescending(s => s.Code);
                        int v = 0;
                        foreach (var item in q)
                        {
                            if (v == 0)
                            { v = Convert.ToInt32(item.Code); }
                            else
                            {
                                if (Convert.ToInt32(item.Code) > v)
                                    v = Convert.ToInt32(item.Code);
                            }
                        }
                        txtCodeId.Text = (Convert.ToInt32(v+1)).ToString();
                        break;
                    case 1:

                        txtCodeId.Enabled = false;
                        var value2 = Reader.Where(r => r.CodeMaster.Contains("ZonActTyp")).ToList();
                         v = 0;
                        foreach (var item in value2)
                        {
                            if (v == 0)
                            { v = Convert.ToInt32(item.Code); }
                            else
                            {
                                if (Convert.ToInt32(item.Code) > v)
                                    v = Convert.ToInt32(item.Code);
                            }
                        }
                        txtCodeId.Text = (Convert.ToInt32(v + 1)).ToString();
                        //txtCodeId.Text = (Convert.ToInt32(value2.Max(c => c.Code)) + 1).ToString();
                        break;
                    case 2:
                        //actoffice
                        txtCodeId.Enabled = true;

                        break;
                    case 3:
                        //actmina
                        txtCodeId.Enabled = true;

                        break;

                    ////case 4:
                    ////    cmbMaterID.Items.Insert(4, "Topographers"); se deja para ponerlo en lparte de admini crearlo en otro lado
                    ////    break;

                    case 5:
                        txtCodeId.Enabled = false;
                        var value3 = Reader.Where(r => r.CodeMaster.Contains("actdirect")).ToList();
                        v = 0;
                        foreach (var item in value3)
                        {
                            if (v == 0)
                            { v = Convert.ToInt32(item.Code); }
                            else
                            {
                                if (Convert.ToInt32(item.Code) > v)
                                    v = Convert.ToInt32(item.Code);
                            }
                        }
                        txtCodeId.Text = (Convert.ToInt32(v + 1)).ToString();

                        break;
                    case 6:
                        //actorient
                        txtCodeId.Enabled = true;

                        break;
                    case 7:
                        txtCodeId.Enabled = false;
                        var value4 = Reader.Where(r => r.CodeMaster.Contains("actzone")).ToList();
                        v = 0;
                        foreach (var item in value4)
                        {
                            if (v == 0)
                            { v = Convert.ToInt32(item.Code); }
                            else
                            {
                                if (Convert.ToInt32(item.Code) > v)
                                    v = Convert.ToInt32(item.Code);
                            }
                        }
                        txtCodeId.Text = (Convert.ToInt32(v + 1)).ToString();

                        break;

                    case 8:

                        txtCodeId.Enabled = false;
                        var value5 = Reader.Where(r => r.CodeMaster.Contains("actphase")).ToList();
                        v = 0;
                        foreach (var item in value5)
                        {
                            if (v == 0)
                            { v = Convert.ToInt32(item.Code); }
                            else
                            {
                                if (Convert.ToInt32(item.Code) > v)
                                    v = Convert.ToInt32(item.Code);
                            }
                        }
                        txtCodeId.Text = (Convert.ToInt32(v + 1)).ToString();
                        break;
                    case 9:

                        txtCodeId.Enabled = false;
                        var value6 = Reader.Where(r => r.CodeMaster.Contains("actccosto")).ToList();
                        v = 0;
                        foreach (var item in value6)
                        {
                            if (v == 0)
                            { v = Convert.ToInt32(item.Code); }
                            else
                            {
                                if (Convert.ToInt32(item.Code) > v)
                                    v = Convert.ToInt32(item.Code);
                            }
                        }
                        txtCodeId.Text = (Convert.ToInt32(v + 1)).ToString();
                        break;
                    case 10:

                        txtCodeId.Enabled = false;
                        var value7 = Reader.Where(r => r.CodeMaster.Contains("actsectype")).ToList();
                        v = 0;
                        foreach (var item in value7)
                        {
                            if (v == 0)
                            { v = Convert.ToInt32(item.Code); }
                            else
                            {
                                if (Convert.ToInt32(item.Code) > v)
                                    v = Convert.ToInt32(item.Code);
                            }
                        }
                        txtCodeId.Text = (Convert.ToInt32(v + 1)).ToString();
                        break;
                    case 11:
                        //actrefid
                        txtCodeId.Enabled = true;

                        //if (validateValue(3))
                        //{
                        //    GetDataTopography.InsertMaster("USP_To_InsertMaster", txtCodeId.Text, txtDescriptionId.Text, "actrefid", string.Empty, cboConditions.Text, txtDescrption.Text);
                        //    isSave = true;
                        //}

                        break;

                    case 12:

                        //actlabtype
                        txtCodeId.Enabled = true;
                        break;

                    case 13:

                        txtCodeId.Enabled = false;
                        var value8 = Reader.Where(r => r.CodeMaster.Contains("actlabvein")).ToList().OrderBy(s => s.Code);
                        v = 0;
                        foreach (var item in value8)
                        {
                            if (v == 0)
                            { v = Convert.ToInt32(item.Code); }
                            else
                            {
                                if (Convert.ToInt32(item.Code) > v)
                                    v = Convert.ToInt32(item.Code);
                            }
                        }
                        txtCodeId.Text = (Convert.ToInt32(v + 1)).ToString();
                        break;

                    case 14:

                        txtCodeId.Enabled = false;
                        var value9 = Reader.Where(r => r.CodeMaster.Contains("actadition")).ToList().OrderBy(s => s.Code);
                        v = 0;
                        foreach (var item in value9)
                        {
                            if (v == 0)
                            { v = Convert.ToInt32(item.Code); }
                            else
                            {
                                if (Convert.ToInt32(item.Code) > v)
                                    v = Convert.ToInt32(item.Code);
                            }
                        }
                        txtCodeId.Text = (Convert.ToInt32(v + 1)).ToString();
                        break;
                    case 15:
                        //actlabtype
                        txtCodeId.Enabled = true;
                        break;
                        
                    default:
                        break;
                }
                //MessageBox.Show("successful engraving ");
                //clearValue(0);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dtgDetailMasterID.CurrentCell != null)
            {
                if (!String.IsNullOrEmpty(txtCodeId.Text.Trim()) && !txtCodeId.Enabled)
                {
                    string valueOperation = string.Empty;
                    string orientation = string.Empty;
                    if (dtgDetailMasterID.CurrentRow.Cells[2].Value.ToString().Trim().Contains("acttopogr"))
                    {
                        valueOperation = "actoffice";

                        IList<MasterTopography> value = Reader.Where(r => r.CodeMaster.Contains(valueOperation)).ToList();

                        orientation = value[cboTypeCondition.SelectedIndex].Code.ToString().Trim();
                    }//
                    GetDataTopography.InsertMaster("USP_To_UpdateMaster", txtCodeId.Text, txtDescriptionId.Text, dtgDetailMasterID.CurrentRow.Cells[2].Value.ToString(),orientation, cboConditions.Text, txtDescrption.Text);
                    clearValue(0);
                    isSave = true;
                    MessageBox.Show("successful update");

                }
                else
                    MessageBox.Show("insert existing value to be updated");

            }
        }
    }
}
