namespace Intercept_Intervals.UI
{
    partial class frm_Intercept_Intervals
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Intercept_Intervals));
            this.grpHoleID = new System.Windows.Forms.GroupBox();
            this.cmbHoleID = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.grpSearchHoleID = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboSoruce = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtSearchHoleId = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.grpExportWell = new System.Windows.Forms.GroupBox();
            this.dtgDetailHoleID = new System.Windows.Forms.DataGridView();
            this.chkSelection = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RfVeins = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.grpValueResult = new System.Windows.Forms.GroupBox();
            this.dtgValueCalculate = new System.Windows.Forms.DataGridView();
            this.jobno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dhid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Length_Grade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Au_Grade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ag_Grade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalRegister = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vn_mod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpComments = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtComent = new System.Windows.Forms.TextBox();
            this.grpOption = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnActive = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefresch = new System.Windows.Forms.Button();
            this.btnSabe = new System.Windows.Forms.Button();
            this.LblTitulos = new System.Windows.Forms.Label();
            this.grpHoleID.SuspendLayout();
            this.grpSearchHoleID.SuspendLayout();
            this.grpExportWell.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDetailHoleID)).BeginInit();
            this.grpValueResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgValueCalculate)).BeginInit();
            this.grpComments.SuspendLayout();
            this.grpOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpHoleID
            // 
            this.grpHoleID.Controls.Add(this.cmbHoleID);
            this.grpHoleID.Controls.Add(this.label11);
            this.grpHoleID.Font = new System.Drawing.Font("Rockwell", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpHoleID.ForeColor = System.Drawing.Color.Navy;
            this.grpHoleID.Location = new System.Drawing.Point(12, 60);
            this.grpHoleID.Name = "grpHoleID";
            this.grpHoleID.Size = new System.Drawing.Size(366, 72);
            this.grpHoleID.TabIndex = 111;
            this.grpHoleID.TabStop = false;
            // 
            // cmbHoleID
            // 
            this.cmbHoleID.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.cmbHoleID.FormattingEnabled = true;
            this.cmbHoleID.Location = new System.Drawing.Point(6, 28);
            this.cmbHoleID.Name = "cmbHoleID";
            this.cmbHoleID.Size = new System.Drawing.Size(347, 33);
            this.cmbHoleID.TabIndex = 110;
            this.cmbHoleID.SelectedIndexChanged += new System.EventHandler(this.cmbHoleID_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 14);
            this.label11.TabIndex = 109;
            this.label11.Text = "Hole ID";
            // 
            // grpSearchHoleID
            // 
            this.grpSearchHoleID.Controls.Add(this.label3);
            this.grpSearchHoleID.Controls.Add(this.cboSoruce);
            this.grpSearchHoleID.Controls.Add(this.label20);
            this.grpSearchHoleID.Controls.Add(this.label18);
            this.grpSearchHoleID.Controls.Add(this.txtSearchHoleId);
            this.grpSearchHoleID.Controls.Add(this.label19);
            this.grpSearchHoleID.Font = new System.Drawing.Font("Rockwell", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSearchHoleID.ForeColor = System.Drawing.Color.Navy;
            this.grpSearchHoleID.Location = new System.Drawing.Point(384, 60);
            this.grpSearchHoleID.Name = "grpSearchHoleID";
            this.grpSearchHoleID.Size = new System.Drawing.Size(305, 72);
            this.grpSearchHoleID.TabIndex = 139;
            this.grpSearchHoleID.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(105, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 14);
            this.label3.TabIndex = 139;
            this.label3.Text = "Source";
            // 
            // cboSoruce
            // 
            this.cboSoruce.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.cboSoruce.FormattingEnabled = true;
            this.cboSoruce.Items.AddRange(new object[] {
            "",
            "GEX",
            "GEM"});
            this.cboSoruce.Location = new System.Drawing.Point(85, 31);
            this.cboSoruce.Name = "cboSoruce";
            this.cboSoruce.Size = new System.Drawing.Size(83, 33);
            this.cboSoruce.TabIndex = 112;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(198, 14);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(69, 14);
            this.label20.TabIndex = 138;
            this.label20.Text = "For Hole ID";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(7, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(37, 14);
            this.label18.TabIndex = 109;
            this.label18.Text = "Filter";
            // 
            // txtSearchHoleId
            // 
            this.txtSearchHoleId.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtSearchHoleId.Location = new System.Drawing.Point(174, 31);
            this.txtSearchHoleId.Multiline = true;
            this.txtSearchHoleId.Name = "txtSearchHoleId";
            this.txtSearchHoleId.Size = new System.Drawing.Size(122, 34);
            this.txtSearchHoleId.TabIndex = 137;
            this.txtSearchHoleId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarSelloControl_KeyPress);
            this.txtSearchHoleId.Leave += new System.EventHandler(this.txtBuscarSelloControl_Leave);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Agency FB", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(6, 38);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(52, 24);
            this.label19.TabIndex = 108;
            this.label19.Text = "Search";
            // 
            // grpExportWell
            // 
            this.grpExportWell.Controls.Add(this.dtgDetailHoleID);
            this.grpExportWell.Font = new System.Drawing.Font("Arial", 10F);
            this.grpExportWell.ForeColor = System.Drawing.Color.Navy;
            this.grpExportWell.Location = new System.Drawing.Point(12, 138);
            this.grpExportWell.Name = "grpExportWell";
            this.grpExportWell.Size = new System.Drawing.Size(670, 521);
            this.grpExportWell.TabIndex = 141;
            this.grpExportWell.TabStop = false;
            this.grpExportWell.Text = "Exploration Wells - Sandra K Mine";
            // 
            // dtgDetailHoleID
            // 
            this.dtgDetailHoleID.AllowUserToDeleteRows = false;
            this.dtgDetailHoleID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgDetailHoleID.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkSelection,
            this.RfVeins});
            this.dtgDetailHoleID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgDetailHoleID.Location = new System.Drawing.Point(3, 19);
            this.dtgDetailHoleID.Name = "dtgDetailHoleID";
            this.dtgDetailHoleID.RowHeadersWidth = 30;
            this.dtgDetailHoleID.Size = new System.Drawing.Size(664, 499);
            this.dtgDetailHoleID.TabIndex = 112;
            this.dtgDetailHoleID.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // chkSelection
            // 
            this.chkSelection.HeaderText = "Selección";
            this.chkSelection.Name = "chkSelection";
            this.chkSelection.Width = 40;
            // 
            // RfVeins
            // 
            this.RfVeins.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.RfVeins.HeaderText = "Vn_mod ";
            this.RfVeins.Items.AddRange(new object[] {
            "NULL",
            "1040",
            "1050",
            "130",
            "1300",
            "1320",
            "133",
            "1330",
            "286",
            "3180",
            "3680",
            "3845",
            "3975",
            "4020",
            "4240",
            "432",
            "4320",
            "6640",
            "840",
            "860",
            "935",
            "980",
            "CA1",
            "CA2",
            "CEC",
            "CHN",
            "CHU",
            "CO1",
            "CO2",
            "CRI",
            "CUE",
            "ESD",
            "ESP",
            "ESU",
            "EUG",
            "FTN1",
            "FTN2",
            "GAN",
            "GIN",
            "HI1",
            "HI2",
            "JUL",
            "LAA",
            "LAN",
            "LCN3",
            "LGC",
            "LRN4",
            "LVG2",
            "LVG3",
            "MAN",
            "MAR",
            "NAL",
            "NEM",
            "PAL",
            "PAT",
            "PRD",
            "PRO",
            "PRU",
            "RUB",
            "SAL",
            "SAN",
            "SIL",
            "SKP",
            "SKT",
            "SNC",
            "SNO",
            "UNK",
            "VEG",
            "VEM",
            "VEP",
            "VER",
            "VET",
            "VIR",
            "VPN"});
            this.RfVeins.Name = "RfVeins";
            this.RfVeins.ToolTipText = "--";
            // 
            // grpValueResult
            // 
            this.grpValueResult.Controls.Add(this.dtgValueCalculate);
            this.grpValueResult.Font = new System.Drawing.Font("Arial", 10F);
            this.grpValueResult.ForeColor = System.Drawing.Color.Navy;
            this.grpValueResult.Location = new System.Drawing.Point(688, 138);
            this.grpValueResult.Name = "grpValueResult";
            this.grpValueResult.Size = new System.Drawing.Size(560, 518);
            this.grpValueResult.TabIndex = 142;
            this.grpValueResult.TabStop = false;
            this.grpValueResult.Text = "Calculated Results";
            // 
            // dtgValueCalculate
            // 
            this.dtgValueCalculate.AllowUserToDeleteRows = false;
            this.dtgValueCalculate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgValueCalculate.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.jobno,
            this.dhid,
            this.from,
            this.to,
            this.Length_Grade,
            this.Au_Grade,
            this.Ag_Grade,
            this.TotalRegister,
            this.Vn_mod});
            this.dtgValueCalculate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgValueCalculate.Location = new System.Drawing.Point(3, 19);
            this.dtgValueCalculate.Name = "dtgValueCalculate";
            this.dtgValueCalculate.ReadOnly = true;
            this.dtgValueCalculate.RowHeadersWidth = 30;
            this.dtgValueCalculate.Size = new System.Drawing.Size(554, 496);
            this.dtgValueCalculate.TabIndex = 112;
            // 
            // jobno
            // 
            this.jobno.HeaderText = "jobno";
            this.jobno.Name = "jobno";
            this.jobno.ReadOnly = true;
            // 
            // dhid
            // 
            this.dhid.HeaderText = "dhid";
            this.dhid.Name = "dhid";
            this.dhid.ReadOnly = true;
            this.dhid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // from
            // 
            this.from.HeaderText = "from";
            this.from.Name = "from";
            this.from.ReadOnly = true;
            // 
            // to
            // 
            this.to.HeaderText = "to";
            this.to.Name = "to";
            this.to.ReadOnly = true;
            // 
            // Length_Grade
            // 
            this.Length_Grade.HeaderText = "Length_Grade";
            this.Length_Grade.Name = "Length_Grade";
            this.Length_Grade.ReadOnly = true;
            // 
            // Au_Grade
            // 
            this.Au_Grade.HeaderText = "Au_Grade";
            this.Au_Grade.Name = "Au_Grade";
            this.Au_Grade.ReadOnly = true;
            // 
            // Ag_Grade
            // 
            this.Ag_Grade.HeaderText = "Ag_Grade";
            this.Ag_Grade.Name = "Ag_Grade";
            this.Ag_Grade.ReadOnly = true;
            // 
            // TotalRegister
            // 
            this.TotalRegister.HeaderText = "TotalRegister";
            this.TotalRegister.Name = "TotalRegister";
            this.TotalRegister.ReadOnly = true;
            // 
            // Vn_mod
            // 
            this.Vn_mod.HeaderText = "Vn_mod";
            this.Vn_mod.Name = "Vn_mod";
            this.Vn_mod.ReadOnly = true;
            // 
            // grpComments
            // 
            this.grpComments.Controls.Add(this.label4);
            this.grpComments.Controls.Add(this.txtComent);
            this.grpComments.Font = new System.Drawing.Font("Rockwell", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpComments.ForeColor = System.Drawing.Color.Navy;
            this.grpComments.Location = new System.Drawing.Point(938, 60);
            this.grpComments.Name = "grpComments";
            this.grpComments.Size = new System.Drawing.Size(312, 72);
            this.grpComments.TabIndex = 146;
            this.grpComments.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 14);
            this.label4.TabIndex = 109;
            this.label4.Text = "Comments";
            // 
            // txtComent
            // 
            this.txtComent.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtComent.Location = new System.Drawing.Point(8, 18);
            this.txtComent.Multiline = true;
            this.txtComent.Name = "txtComent";
            this.txtComent.Size = new System.Drawing.Size(295, 45);
            this.txtComent.TabIndex = 137;
            // 
            // grpOption
            // 
            this.grpOption.Controls.Add(this.checkBox1);
            this.grpOption.Controls.Add(this.label6);
            this.grpOption.Controls.Add(this.label5);
            this.grpOption.Controls.Add(this.btnExport);
            this.grpOption.Controls.Add(this.btnActive);
            this.grpOption.Controls.Add(this.label2);
            this.grpOption.Controls.Add(this.label1);
            this.grpOption.Controls.Add(this.btnRefresch);
            this.grpOption.Controls.Add(this.btnSabe);
            this.grpOption.Font = new System.Drawing.Font("Rockwell", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpOption.ForeColor = System.Drawing.Color.Navy;
            this.grpOption.Location = new System.Drawing.Point(695, 60);
            this.grpOption.Name = "grpOption";
            this.grpOption.Size = new System.Drawing.Size(239, 72);
            this.grpOption.TabIndex = 148;
            this.grpOption.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(195, 32);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 152;
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.checkBox1.ThreeState = true;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(184, -1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 14);
            this.label6.TabIndex = 151;
            this.label6.Text = "Active";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(128, -1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 14);
            this.label5.TabIndex = 150;
            this.label5.Text = "Export";
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnExport.Image = global::EnviosColombiaGold.Properties.Resources.excel;
            this.btnExport.Location = new System.Drawing.Point(119, 13);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(56, 53);
            this.btnExport.TabIndex = 148;
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnActive
            // 
            this.btnActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnActive.Location = new System.Drawing.Point(176, 13);
            this.btnActive.Name = "btnActive";
            this.btnActive.Size = new System.Drawing.Size(56, 53);
            this.btnActive.TabIndex = 149;
            this.btnActive.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnActive.UseVisualStyleBackColor = true;
            this.btnActive.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 14);
            this.label2.TabIndex = 147;
            this.label2.Text = "Save";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 14);
            this.label1.TabIndex = 109;
            this.label1.Text = "Refresh";
            // 
            // btnRefresch
            // 
            this.btnRefresch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnRefresch.Image = global::EnviosColombiaGold.Properties.Resources.Refresh_icon_Value;
            this.btnRefresch.Location = new System.Drawing.Point(5, 13);
            this.btnRefresch.Name = "btnRefresch";
            this.btnRefresch.Size = new System.Drawing.Size(56, 53);
            this.btnRefresch.TabIndex = 140;
            this.btnRefresch.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresch.UseVisualStyleBackColor = true;
            this.btnRefresch.Click += new System.EventHandler(this.button6_Click);
            // 
            // btnSabe
            // 
            this.btnSabe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnSabe.Image = global::EnviosColombiaGold.Properties.Resources.save_162;
            this.btnSabe.Location = new System.Drawing.Point(62, 13);
            this.btnSabe.Name = "btnSabe";
            this.btnSabe.Size = new System.Drawing.Size(56, 53);
            this.btnSabe.TabIndex = 145;
            this.btnSabe.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSabe.UseVisualStyleBackColor = true;
            this.btnSabe.Click += new System.EventHandler(this.button1_Click);
            // 
            // LblTitulos
            // 
            this.LblTitulos.BackColor = System.Drawing.Color.Transparent;
            this.LblTitulos.Font = new System.Drawing.Font("Britannic Bold", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitulos.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LblTitulos.Image = global::EnviosColombiaGold.Properties.Resources.BarrasL;
            this.LblTitulos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LblTitulos.Location = new System.Drawing.Point(0, -2);
            this.LblTitulos.Name = "LblTitulos";
            this.LblTitulos.Size = new System.Drawing.Size(1257, 72);
            this.LblTitulos.TabIndex = 90;
            this.LblTitulos.Text = "Intercept Intervals";
            this.LblTitulos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frm_Intercept_Intervals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 701);
            this.Controls.Add(this.grpOption);
            this.Controls.Add(this.grpComments);
            this.Controls.Add(this.grpValueResult);
            this.Controls.Add(this.grpExportWell);
            this.Controls.Add(this.grpSearchHoleID);
            this.Controls.Add(this.grpHoleID);
            this.Controls.Add(this.LblTitulos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Intercept_Intervals";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Intercept Intervals";
            this.grpHoleID.ResumeLayout(false);
            this.grpHoleID.PerformLayout();
            this.grpSearchHoleID.ResumeLayout(false);
            this.grpSearchHoleID.PerformLayout();
            this.grpExportWell.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgDetailHoleID)).EndInit();
            this.grpValueResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgValueCalculate)).EndInit();
            this.grpComments.ResumeLayout(false);
            this.grpComments.PerformLayout();
            this.grpOption.ResumeLayout(false);
            this.grpOption.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label LblTitulos;
        private System.Windows.Forms.GroupBox grpHoleID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbHoleID;
        private System.Windows.Forms.GroupBox grpSearchHoleID;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtSearchHoleId;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnRefresch;
        private System.Windows.Forms.GroupBox grpExportWell;
        private System.Windows.Forms.DataGridView dtgDetailHoleID;
        private System.Windows.Forms.GroupBox grpValueResult;
        private System.Windows.Forms.DataGridView dtgValueCalculate;
        private System.Windows.Forms.DataGridViewTextBoxColumn jobno;
        private System.Windows.Forms.DataGridViewTextBoxColumn dhid;
        private System.Windows.Forms.DataGridViewTextBoxColumn from;
        private System.Windows.Forms.DataGridViewTextBoxColumn to;
        private System.Windows.Forms.DataGridViewTextBoxColumn Length_Grade;
        private System.Windows.Forms.DataGridViewTextBoxColumn Au_Grade;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ag_Grade;
        private System.Windows.Forms.Button btnSabe;
        private System.Windows.Forms.GroupBox grpComments;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtComent;
        private System.Windows.Forms.GroupBox grpOption;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboSoruce;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalRegister;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vn_mod;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnActive;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkSelection;
        private System.Windows.Forms.DataGridViewComboBoxColumn RfVeins;
    }
}