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
    public partial class frmMoventLabor : Form
    {
        IList<MasterTopography> Reader = new List<MasterTopography>();

        public frmMoventLabor()
        {
            InitializeComponent();

            loadDate();


        }

        private void clear()
        {
            this.cboLaborType.SelectedIndex = cboLaborType.Items.Count - 1;
            this.cboDirection.SelectedIndex = cboDirection.Items.Count - 1;
            this.cboZone.SelectedIndex = cboZone.Items.Count - 1;
            this.cboVein.SelectedIndex = cboVein.Items.Count - 1;
            this.cboCost.SelectedIndex = cboCost.Items.Count - 1;
            this.cboMine.SelectedIndex = cboMine.Items.Count - 1;
            this.cboPhase.SelectedIndex = cboPhase.Items.Count - 1;
            this.cboSectType.SelectedIndex = cboSectType.Items.Count - 1;
            cboOrientation.SelectedIndex = cboOrientation.Items.Count - 1;

            txtCodeId.Text = string.Empty;
            txtDescriptionId.Text = string.Empty;
            txtNomClat.Text = string.Empty;
            txtLevel.Text = string.Empty;
            txtLaborAB.Text = string.Empty;
            txtCodeId.Enabled = true;
        }

        private void loadDate()
        {

            this.cboLaborType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboOrientation.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboDirection.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboZone.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboVein.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboCost.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboMine.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPhase.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboSectType.DropDownStyle = ComboBoxStyle.DropDownList;

            cboLaborType.DisplayMember = "Description";
            cboLaborType.ValueMember = "Code";

            if (Reader.Count() == 0)
                Reader = GetDataTopography.getMaterTopography("usp_TO_getMaterTopography");

            var value = Reader.Where(r => r.CodeMaster.Contains("actlabtype")).Select(c => c.Description).ToList();
            value.Insert(value.ToList().Count, "Select an option..");

            cboLaborType.DataSource = value;
            cboLaborType.SelectedIndex = value.ToList().Count - 1;


            cboOrientation.DisplayMember = "Description";
            cboOrientation.ValueMember = "Code";

            value = Reader.Where(r => r.CodeMaster.Contains("actorient")).Select(c => c.Description).ToList();
            value.Insert(value.ToList().Count, "Select an option..");

            cboOrientation.DataSource = value;
            cboOrientation.SelectedIndex = value.ToList().Count - 1;


            cboDirection.DisplayMember = "Description";
            cboDirection.ValueMember = "Code";

            value = Reader.Where(r => r.CodeMaster.Contains("actdirect")).Select(c => c.Description).ToList();
            value.Insert(value.ToList().Count, "Select an option..");

            cboDirection.DataSource = value;
            cboDirection.SelectedIndex = value.ToList().Count - 1;

            cboZone.DisplayMember = "Description";
            cboZone.ValueMember = "Code";

            value = Reader.Where(r => r.CodeMaster.Contains("actzone")).Select(c => c.Description).ToList();
            value.Insert(value.ToList().Count, "Select an option..");

            cboZone.DataSource = value;
            cboZone.SelectedIndex = value.ToList().Count - 1;

            cboVein.DisplayMember = "Description";
            cboVein.ValueMember = "Code";

            value = Reader.Where(r => r.CodeMaster.Contains("actlabvein")).Select(c => c.Description).ToList();
            value.Insert(value.ToList().Count, "Select an option..");

            cboVein.DataSource = value;
            cboVein.SelectedIndex = value.ToList().Count - 1;

            cboPhase.DisplayMember = "Description";
            cboPhase.ValueMember = "Code";

            value = Reader.Where(r => r.CodeMaster.Contains("actphase")).Select(c => c.Description).ToList();
            value.Insert(value.ToList().Count, "Select an option..");

            cboPhase.DataSource = value;
            cboPhase.SelectedIndex = value.ToList().Count - 1;


            cboCost.DisplayMember = "Description";
            cboCost.ValueMember = "Code";

            value = Reader.Where(r => r.CodeMaster.Contains("actccosto")).Select(c => c.Description).ToList();
            value.Insert(value.ToList().Count, "Select an option..");

            cboCost.DataSource = value;
            cboCost.SelectedIndex = value.ToList().Count - 1;


            cboMine.DisplayMember = "Description";
            cboMine.ValueMember = "Code";

            value = Reader.Where(r => r.CodeMaster.Contains("actmina")).Select(c => c.Description).ToList();
            value.Insert(value.ToList().Count, "Select an option..");

            cboMine.DataSource = value;
            cboMine.SelectedIndex = value.ToList().Count - 1;

            cboSectType.DisplayMember = "Description";
            cboSectType.ValueMember = "Code";

            value = Reader.Where(r => r.CodeMaster.Contains("actsectype")).Select(c => c.Description).ToList();
            value.Insert(value.ToList().Count, "Select an option..");

            cboSectType.DataSource = value;
            cboSectType.SelectedIndex = value.ToList().Count - 1;



        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            string typeLab = string.Empty;
            string orientation = string.Empty;
            string direction = string.Empty;
            string zone = string.Empty;
            string vein = string.Empty;
            string phase = string.Empty;
            string cost = string.Empty;
            string mine = string.Empty;
            string sectType = string.Empty;

            IList<MasterTopography> value = Reader.Where(r => r.CodeMaster.Contains("actlabtype")).ToList();


            if (String.IsNullOrEmpty(txtCodeId.Text))
            {
                MessageBox.Show("Select value in Code");
                return;
            }

            if (!cboLaborType.Text.Contains("Select"))
                typeLab = value[cboLaborType.SelectedIndex].Code.ToString().Trim();
            else
            {
                MessageBox.Show("Select value in Labor Type");
                return;
            }

            value = Reader.Where(r => r.CodeMaster.Contains("actorient")).ToList();
            if (!cboOrientation.Text.Contains("Select"))
                orientation = value[cboOrientation.SelectedIndex].Code.ToString().Trim();
            else
            {
                MessageBox.Show("Select value in Orientation"); return;
            }
            value = Reader.Where(r => r.CodeMaster.Contains("actdirect")).ToList();
            if (!cboDirection.Text.Contains("Select"))
                direction = value[cboDirection.SelectedIndex].Code.ToString().Trim();
            else
            {
                MessageBox.Show("Select value in Direction"); return;
            }

            value = Reader.Where(r => r.CodeMaster.Contains("actzone")).ToList();
            if (!cboZone.Text.Contains("Select"))
                zone = value[cboZone.SelectedIndex].Code.ToString().Trim();
            else
            {
                MessageBox.Show("Select value in Zone"); return;
            }
            value = Reader.Where(r => r.CodeMaster.Contains("actlabvein")).ToList();
            if (!cboVein.Text.Contains("Select"))
                vein = value[cboVein.SelectedIndex].Code.ToString().Trim();
            else
            {
                MessageBox.Show("Select value in Vein"); return;
            }

            value = Reader.Where(r => r.CodeMaster.Contains("actphase")).ToList();
            if (!cboPhase.Text.Contains("Select"))
                phase = value[cboPhase.SelectedIndex].Code.ToString().Trim();
            else
            {
                MessageBox.Show("Select value in Phase"); return;
            }

            value = Reader.Where(r => r.CodeMaster.Contains("actccosto")).ToList();
            if (!cboCost.Text.Contains("Select"))
                cost = value[cboCost.SelectedIndex].Code.ToString().Trim();
            else
            {
                MessageBox.Show("Select value in CCosto"); return;
            }

            value = Reader.Where(r => r.CodeMaster.Contains("actmina")).ToList();
            if (!cboMine.Text.Contains("Select"))
                mine = value[cboMine.SelectedIndex].Code.ToString().Trim();
            else
            {
                MessageBox.Show("Select value in Mine"); return;
            }

            value = Reader.Where(r => r.CodeMaster.Contains("actsectype")).ToList();
            if (!cboSectType.Text.Contains("Select"))
                sectType = value[cboSectType.SelectedIndex].Code.ToString().Trim();
            else
            {
                MessageBox.Show("Select value in Sector Type"); return;
            }

            if (GetDataTopography.InsertLaborTopography("TO_Update_Topography_Labor", txtCodeId.Text, txtDescriptionId.Text, txtLevel.Text, typeLab, txtLaborAB.Text, orientation, txtNomClat.Text, direction, zone, vein, phase, cost, mine, sectType) != 0)
            {
                MessageBox.Show("successful engraving ");
                clear();

                dtgDetailLaborTopo.DataBindings.Clear();
                var getValue = GetDataTopography.getLaborTopography("TO_Get_Labor");
                dtgDetailLaborTopo.DataSource = getValue;
            }
            else
            {
                MessageBox.Show("Error engraving ");
                clear();
            }
        }

        private void dtgDetailLaborTopo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgDetailLaborTopo.CurrentCell != null)
            {
                if (e.RowIndex >= 0)
                {

                    txtCodeId.Text = dtgDetailLaborTopo.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    txtDescriptionId.Text = dtgDetailLaborTopo.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
                    txtNomClat.Text = dtgDetailLaborTopo.Rows[e.RowIndex].Cells[6].Value.ToString().Trim();
                    txtLevel.Text = dtgDetailLaborTopo.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
                    this.cboLaborType.Text = dtgDetailLaborTopo.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();
                    txtLaborAB.Text = dtgDetailLaborTopo.Rows[e.RowIndex].Cells[4].Value.ToString().Trim();
                    cboOrientation.Text = dtgDetailLaborTopo.Rows[e.RowIndex].Cells[5].Value.ToString().Trim();
                    this.cboDirection.Text = dtgDetailLaborTopo.Rows[e.RowIndex].Cells[7].Value.ToString().Trim();
                    this.cboZone.Text = dtgDetailLaborTopo.Rows[e.RowIndex].Cells[8].Value.ToString().Trim();
                    this.cboVein.Text = dtgDetailLaborTopo.Rows[e.RowIndex].Cells[9].Value.ToString().Trim();
                    this.cboPhase.Text = dtgDetailLaborTopo.Rows[e.RowIndex].Cells[10].Value.ToString().Trim();
                    this.cboCost.Text = dtgDetailLaborTopo.Rows[e.RowIndex].Cells[11].Value.ToString().Trim();
                    this.cboMine.Text = dtgDetailLaborTopo.Rows[e.RowIndex].Cells[12].Value.ToString().Trim();
                    this.cboSectType.Text = dtgDetailLaborTopo.Rows[e.RowIndex].Cells[13].Value.ToString().Trim();
                    txtCodeId.Enabled = false;
                }

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string typeLab = string.Empty;
            string orientation = string.Empty;
            string direction = string.Empty;
            string zone = string.Empty;
            string vein = string.Empty;
            string phase = string.Empty;
            string cost = string.Empty;
            string mine = string.Empty;
            string sectType = string.Empty;

            IList<MasterTopography> value = Reader.Where(r => r.CodeMaster.Contains("actlabtype")).ToList();


            if (!cboLaborType.Text.Contains("Select"))
                typeLab = value[cboLaborType.SelectedIndex].Code.ToString().Trim();
            else
            {
                MessageBox.Show("Select value in Labor Type");
                return;
            }

            value = Reader.Where(r => r.CodeMaster.Contains("actorient")).ToList();
            if (!cboOrientation.Text.Contains("Select"))
                orientation = value[cboOrientation.SelectedIndex].Code.ToString().Trim();
            else
            {
                MessageBox.Show("Select value in Orientation"); return;
            }
            value = Reader.Where(r => r.CodeMaster.Contains("actdirect")).ToList();
            if (!cboDirection.Text.Contains("Select"))
                direction = value[cboDirection.SelectedIndex].Code.ToString().Trim();
            else
            {
                MessageBox.Show("Select value in Direction"); return;
            }

            value = Reader.Where(r => r.CodeMaster.Contains("actzone")).ToList();
            if (!cboZone.Text.Contains("Select"))
                zone = value[cboZone.SelectedIndex].Code.ToString().Trim();
            else
            {
                MessageBox.Show("Select value in Zone"); return;
            }
            value = Reader.Where(r => r.CodeMaster.Contains("actlabvein")).ToList();
            if (!cboVein.Text.Contains("Select"))
                vein = value[cboVein.SelectedIndex].Code.ToString().Trim();
            else
            {
                MessageBox.Show("Select value in Vein"); return;
            }

            value = Reader.Where(r => r.CodeMaster.Contains("actphase")).ToList();
            if (!cboPhase.Text.Contains("Select"))
                phase = value[cboPhase.SelectedIndex].Code.ToString().Trim();
            else
            {
                MessageBox.Show("Select value in Phase"); return;
            }

            value = Reader.Where(r => r.CodeMaster.Contains("actccosto")).ToList();
            if (!cboCost.Text.Contains("Select"))
                cost = value[cboCost.SelectedIndex].Code.ToString().Trim();
            else
            {
                MessageBox.Show("Select value in CCosto"); return;
            }

            value = Reader.Where(r => r.CodeMaster.Contains("actmina")).ToList();
            if (!cboMine.Text.Contains("Select"))
                mine = value[cboMine.SelectedIndex].Code.ToString().Trim();
            else
            {
                MessageBox.Show("Select value in Mine"); return;
            }

            value = Reader.Where(r => r.CodeMaster.Contains("actsectype")).ToList();
            if (!cboSectType.Text.Contains("Select"))
                sectType = value[cboSectType.SelectedIndex].Code.ToString().Trim();
            else
            {
                MessageBox.Show("Select value in Sector Type"); return;
            }

            if (GetDataTopography.InsertLaborTopography("TO_Update_Topography_Labor", txtCodeId.Text, txtDescriptionId.Text, txtLevel.Text, typeLab, txtLaborAB.Text, orientation, txtNomClat.Text, direction, zone, vein, phase, cost, mine, sectType) != 0)
            {
                MessageBox.Show("successful update ");
                clear();

                dtgDetailLaborTopo.DataBindings.Clear();
                var getValue = GetDataTopography.getLaborTopography("TO_Get_Labor");
                dtgDetailLaborTopo.DataSource = getValue;
            }
            else
            {
                MessageBox.Show("Error update ");
                clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtCodeId.Text))
            {
                MessageBox.Show("Select value in Code");
                return;
            }

            if (GetDataTopography.DeleteLaborTopography("TO_Delete_Topography_Labor", txtCodeId.Text) != 0)
            {
                MessageBox.Show("successful Delete ");
                clear();

                dtgDetailLaborTopo.DataBindings.Clear();
                var getValue = GetDataTopography.getLaborTopography("TO_Get_Labor");
                dtgDetailLaborTopo.DataSource = getValue;
            }
            else
            {
                MessageBox.Show("Error Delete ");
                clear();
            }

        }

        private void cboMine_SelectedIndexChanged(object sender, EventArgs e)
        {
            //www.viatrix.co  

            var getValue = GetDataTopography.getLaborTopography("TO_Get_Labor").Where(r => r.Mina.Contains(cboMine.SelectedItem.ToString())).ToList();
            dtgDetailLaborTopo.DataSource = getValue;

            //dtgDetailLaborTopo.AutoResizeColumns();
        }

        private void cboLaborType_SelectedIndexChanged(object sender, EventArgs e)
        {
            IList<MasterTopography> value = Reader.Where(r => r.CodeMaster.Contains("actlabtype")).ToList();
            if (cboLaborType.SelectedIndex != cboLaborType.Items.Count - 1)
                txtLaborAB.Text = value[cboLaborType.SelectedIndex].Code.ToString().Trim();

        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                var getValue = GetDataTopography.getLaborTopography("TO_Get_Labor");
                dtgDetailLaborTopo.DataSource = getValue;
            }
            else
                dtgDetailLaborTopo.DataSource = null;

        }
    }
}
