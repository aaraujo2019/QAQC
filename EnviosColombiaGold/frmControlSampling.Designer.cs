namespace EnviosColombiaGold
{
    partial class frmControlSampling
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmControlSampling));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbCtrlSamp = new System.Windows.Forms.TabControl();
            this.tbQualityControlSamples = new System.Windows.Forms.TabPage();
            this.dgAssignQControl = new System.Windows.Forms.DataGridView();
            this.btnCancelAQCS = new System.Windows.Forms.Button();
            this.btnAceptAQCS = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbSampleType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSamples = new System.Windows.Forms.ComboBox();
            this.tbSamplesAssign = new System.Windows.Forms.TabPage();
            this.dgSamplesAssign = new System.Windows.Forms.DataGridView();
            this.btnCancelASamp = new System.Windows.Forms.Button();
            this.btnAceptASamp = new System.Windows.Forms.Button();
            this.cmbToSamplesAssign = new System.Windows.Forms.ComboBox();
            this.cmbFromSamplesAssign = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtDateAssign = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.cmbGeologist = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLoadSamples = new System.Windows.Forms.TabPage();
            this.btnLoad = new System.Windows.Forms.Button();
            this.dgLoadSamples = new System.Windows.Forms.DataGridView();
            this.gbLoadSamples = new System.Windows.Forms.GroupBox();
            this.cmbSheed = new System.Windows.Forms.ComboBox();
            this.pbtnSearch = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.btnAbrir = new System.Windows.Forms.Button();
            this.tbSamplesPrint = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgExportXls = new System.Windows.Forms.DataGridView();
            this.btnAceptExport = new System.Windows.Forms.Button();
            this.cmbToExport = new System.Windows.Forms.ComboBox();
            this.btnExcelExport = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbFromExport = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.oDialog = new System.Windows.Forms.OpenFileDialog();
            this.tbCtrlSamp.SuspendLayout();
            this.tbQualityControlSamples.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAssignQControl)).BeginInit();
            this.tbSamplesAssign.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSamplesAssign)).BeginInit();
            this.tbLoadSamples.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLoadSamples)).BeginInit();
            this.gbLoadSamples.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbtnSearch)).BeginInit();
            this.tbSamplesPrint.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgExportXls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExcelExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbCtrlSamp
            // 
            this.tbCtrlSamp.Controls.Add(this.tbQualityControlSamples);
            this.tbCtrlSamp.Controls.Add(this.tbSamplesAssign);
            this.tbCtrlSamp.Controls.Add(this.tbLoadSamples);
            this.tbCtrlSamp.Controls.Add(this.tbSamplesPrint);
            this.tbCtrlSamp.Location = new System.Drawing.Point(12, 72);
            this.tbCtrlSamp.Name = "tbCtrlSamp";
            this.tbCtrlSamp.SelectedIndex = 0;
            this.tbCtrlSamp.Size = new System.Drawing.Size(1219, 577);
            this.tbCtrlSamp.TabIndex = 0;
            // 
            // tbQualityControlSamples
            // 
            this.tbQualityControlSamples.Controls.Add(this.dgAssignQControl);
            this.tbQualityControlSamples.Controls.Add(this.btnCancelAQCS);
            this.tbQualityControlSamples.Controls.Add(this.btnAceptAQCS);
            this.tbQualityControlSamples.Controls.Add(this.label5);
            this.tbQualityControlSamples.Controls.Add(this.cmbSampleType);
            this.tbQualityControlSamples.Controls.Add(this.label4);
            this.tbQualityControlSamples.Controls.Add(this.cmbSamples);
            this.tbQualityControlSamples.Location = new System.Drawing.Point(4, 22);
            this.tbQualityControlSamples.Name = "tbQualityControlSamples";
            this.tbQualityControlSamples.Padding = new System.Windows.Forms.Padding(3);
            this.tbQualityControlSamples.Size = new System.Drawing.Size(1211, 551);
            this.tbQualityControlSamples.TabIndex = 1;
            this.tbQualityControlSamples.Text = "Assign Quality Control Samples";
            this.tbQualityControlSamples.UseVisualStyleBackColor = true;
            // 
            // dgAssignQControl
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgAssignQControl.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgAssignQControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAssignQControl.Location = new System.Drawing.Point(26, 71);
            this.dgAssignQControl.Margin = new System.Windows.Forms.Padding(2);
            this.dgAssignQControl.Name = "dgAssignQControl";
            this.dgAssignQControl.RowTemplate.Height = 24;
            this.dgAssignQControl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgAssignQControl.Size = new System.Drawing.Size(1180, 475);
            this.dgAssignQControl.TabIndex = 162;
            this.dgAssignQControl.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAssignQControl_CellClick);
            // 
            // btnCancelAQCS
            // 
            this.btnCancelAQCS.Location = new System.Drawing.Point(464, 43);
            this.btnCancelAQCS.Name = "btnCancelAQCS";
            this.btnCancelAQCS.Size = new System.Drawing.Size(75, 23);
            this.btnCancelAQCS.TabIndex = 161;
            this.btnCancelAQCS.Text = "Cancel";
            this.btnCancelAQCS.UseVisualStyleBackColor = true;
            this.btnCancelAQCS.Click += new System.EventHandler(this.btnCancelAQCS_Click);
            // 
            // btnAceptAQCS
            // 
            this.btnAceptAQCS.Location = new System.Drawing.Point(383, 43);
            this.btnAceptAQCS.Name = "btnAceptAQCS";
            this.btnAceptAQCS.Size = new System.Drawing.Size(75, 23);
            this.btnAceptAQCS.TabIndex = 160;
            this.btnAceptAQCS.Text = "Acept";
            this.btnAceptAQCS.UseVisualStyleBackColor = true;
            this.btnAceptAQCS.Click += new System.EventHandler(this.btnAceptAQCS_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(203, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 159;
            this.label5.Text = "Sample Type: ";
            // 
            // cmbSampleType
            // 
            this.cmbSampleType.FormattingEnabled = true;
            this.cmbSampleType.Location = new System.Drawing.Point(206, 43);
            this.cmbSampleType.Name = "cmbSampleType";
            this.cmbSampleType.Size = new System.Drawing.Size(171, 21);
            this.cmbSampleType.TabIndex = 158;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 157;
            this.label4.Text = "Sample: ";
            // 
            // cmbSamples
            // 
            this.cmbSamples.FormattingEnabled = true;
            this.cmbSamples.Location = new System.Drawing.Point(26, 43);
            this.cmbSamples.Name = "cmbSamples";
            this.cmbSamples.Size = new System.Drawing.Size(171, 21);
            this.cmbSamples.TabIndex = 156;
            // 
            // tbSamplesAssign
            // 
            this.tbSamplesAssign.Controls.Add(this.dgSamplesAssign);
            this.tbSamplesAssign.Controls.Add(this.btnCancelASamp);
            this.tbSamplesAssign.Controls.Add(this.btnAceptASamp);
            this.tbSamplesAssign.Controls.Add(this.cmbToSamplesAssign);
            this.tbSamplesAssign.Controls.Add(this.cmbFromSamplesAssign);
            this.tbSamplesAssign.Controls.Add(this.label3);
            this.tbSamplesAssign.Controls.Add(this.label2);
            this.tbSamplesAssign.Controls.Add(this.dtDateAssign);
            this.tbSamplesAssign.Controls.Add(this.lblDate);
            this.tbSamplesAssign.Controls.Add(this.cmbGeologist);
            this.tbSamplesAssign.Controls.Add(this.label1);
            this.tbSamplesAssign.Location = new System.Drawing.Point(4, 22);
            this.tbSamplesAssign.Name = "tbSamplesAssign";
            this.tbSamplesAssign.Padding = new System.Windows.Forms.Padding(3);
            this.tbSamplesAssign.Size = new System.Drawing.Size(1211, 551);
            this.tbSamplesAssign.TabIndex = 0;
            this.tbSamplesAssign.Text = "Samples Assign ";
            this.tbSamplesAssign.UseVisualStyleBackColor = true;
            // 
            // dgSamplesAssign
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgSamplesAssign.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgSamplesAssign.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSamplesAssign.Location = new System.Drawing.Point(29, 120);
            this.dgSamplesAssign.Margin = new System.Windows.Forms.Padding(2);
            this.dgSamplesAssign.Name = "dgSamplesAssign";
            this.dgSamplesAssign.RowTemplate.Height = 24;
            this.dgSamplesAssign.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSamplesAssign.Size = new System.Drawing.Size(1177, 426);
            this.dgSamplesAssign.TabIndex = 164;
            this.dgSamplesAssign.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSamplesAssign_CellClick);
            // 
            // btnCancelASamp
            // 
            this.btnCancelASamp.Location = new System.Drawing.Point(465, 89);
            this.btnCancelASamp.Name = "btnCancelASamp";
            this.btnCancelASamp.Size = new System.Drawing.Size(75, 23);
            this.btnCancelASamp.TabIndex = 163;
            this.btnCancelASamp.Text = "Cancel";
            this.btnCancelASamp.UseVisualStyleBackColor = true;
            this.btnCancelASamp.Click += new System.EventHandler(this.btnCancelASamp_Click);
            // 
            // btnAceptASamp
            // 
            this.btnAceptASamp.Location = new System.Drawing.Point(384, 90);
            this.btnAceptASamp.Name = "btnAceptASamp";
            this.btnAceptASamp.Size = new System.Drawing.Size(75, 23);
            this.btnAceptASamp.TabIndex = 162;
            this.btnAceptASamp.Text = "Acept";
            this.btnAceptASamp.UseVisualStyleBackColor = true;
            this.btnAceptASamp.Click += new System.EventHandler(this.btnAceptASamp_Click);
            // 
            // cmbToSamplesAssign
            // 
            this.cmbToSamplesAssign.FormattingEnabled = true;
            this.cmbToSamplesAssign.Location = new System.Drawing.Point(206, 92);
            this.cmbToSamplesAssign.Name = "cmbToSamplesAssign";
            this.cmbToSamplesAssign.Size = new System.Drawing.Size(171, 21);
            this.cmbToSamplesAssign.TabIndex = 161;
            // 
            // cmbFromSamplesAssign
            // 
            this.cmbFromSamplesAssign.FormattingEnabled = true;
            this.cmbFromSamplesAssign.Location = new System.Drawing.Point(29, 92);
            this.cmbFromSamplesAssign.Name = "cmbFromSamplesAssign";
            this.cmbFromSamplesAssign.Size = new System.Drawing.Size(171, 21);
            this.cmbFromSamplesAssign.TabIndex = 160;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 159;
            this.label3.Text = "From Sample Assign: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 158;
            this.label2.Text = "To Sample Assign: ";
            // 
            // dtDateAssign
            // 
            this.dtDateAssign.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDateAssign.Location = new System.Drawing.Point(207, 44);
            this.dtDateAssign.Name = "dtDateAssign";
            this.dtDateAssign.Size = new System.Drawing.Size(119, 20);
            this.dtDateAssign.TabIndex = 157;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(204, 27);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(70, 13);
            this.lblDate.TabIndex = 156;
            this.lblDate.Text = "Date Assign: ";
            // 
            // cmbGeologist
            // 
            this.cmbGeologist.FormattingEnabled = true;
            this.cmbGeologist.Location = new System.Drawing.Point(29, 43);
            this.cmbGeologist.Name = "cmbGeologist";
            this.cmbGeologist.Size = new System.Drawing.Size(171, 21);
            this.cmbGeologist.TabIndex = 155;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Geologist Assign: ";
            // 
            // tbLoadSamples
            // 
            this.tbLoadSamples.Controls.Add(this.btnLoad);
            this.tbLoadSamples.Controls.Add(this.dgLoadSamples);
            this.tbLoadSamples.Controls.Add(this.gbLoadSamples);
            this.tbLoadSamples.Location = new System.Drawing.Point(4, 22);
            this.tbLoadSamples.Name = "tbLoadSamples";
            this.tbLoadSamples.Size = new System.Drawing.Size(1211, 551);
            this.tbLoadSamples.TabIndex = 2;
            this.tbLoadSamples.Text = "Load Samples";
            this.tbLoadSamples.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(1117, 516);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 10;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dgLoadSamples
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgLoadSamples.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgLoadSamples.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLoadSamples.Location = new System.Drawing.Point(10, 93);
            this.dgLoadSamples.Margin = new System.Windows.Forms.Padding(2);
            this.dgLoadSamples.Name = "dgLoadSamples";
            this.dgLoadSamples.RowTemplate.Height = 24;
            this.dgLoadSamples.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgLoadSamples.Size = new System.Drawing.Size(1182, 418);
            this.dgLoadSamples.TabIndex = 165;
            // 
            // gbLoadSamples
            // 
            this.gbLoadSamples.Controls.Add(this.cmbSheed);
            this.gbLoadSamples.Controls.Add(this.pbtnSearch);
            this.gbLoadSamples.Controls.Add(this.label6);
            this.gbLoadSamples.Controls.Add(this.label7);
            this.gbLoadSamples.Controls.Add(this.txtRuta);
            this.gbLoadSamples.Controls.Add(this.btnAbrir);
            this.gbLoadSamples.Location = new System.Drawing.Point(10, 3);
            this.gbLoadSamples.Name = "gbLoadSamples";
            this.gbLoadSamples.Size = new System.Drawing.Size(511, 85);
            this.gbLoadSamples.TabIndex = 1;
            this.gbLoadSamples.TabStop = false;
            // 
            // cmbSheed
            // 
            this.cmbSheed.FormattingEnabled = true;
            this.cmbSheed.Location = new System.Drawing.Point(92, 47);
            this.cmbSheed.Name = "cmbSheed";
            this.cmbSheed.Size = new System.Drawing.Size(149, 21);
            this.cmbSheed.TabIndex = 3;
            // 
            // pbtnSearch
            // 
            this.pbtnSearch.Image = ((System.Drawing.Image)(resources.GetObject("pbtnSearch.Image")));
            this.pbtnSearch.Location = new System.Drawing.Point(467, 19);
            this.pbtnSearch.Name = "pbtnSearch";
            this.pbtnSearch.Size = new System.Drawing.Size(26, 22);
            this.pbtnSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbtnSearch.TabIndex = 9;
            this.pbtnSearch.TabStop = false;
            this.pbtnSearch.Click += new System.EventHandler(this.pbtnSearch_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Sheed";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "File Path Xls";
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(92, 21);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(369, 20);
            this.txtRuta.TabIndex = 1;
            // 
            // btnAbrir
            // 
            this.btnAbrir.Location = new System.Drawing.Point(418, 51);
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(75, 23);
            this.btnAbrir.TabIndex = 0;
            this.btnAbrir.Text = "Open";
            this.btnAbrir.UseVisualStyleBackColor = true;
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // tbSamplesPrint
            // 
            this.tbSamplesPrint.Controls.Add(this.groupBox1);
            this.tbSamplesPrint.Location = new System.Drawing.Point(4, 22);
            this.tbSamplesPrint.Name = "tbSamplesPrint";
            this.tbSamplesPrint.Size = new System.Drawing.Size(1211, 551);
            this.tbSamplesPrint.TabIndex = 3;
            this.tbSamplesPrint.Text = "Samples Export Xls";
            this.tbSamplesPrint.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgExportXls);
            this.groupBox1.Controls.Add(this.btnAceptExport);
            this.groupBox1.Controls.Add(this.cmbToExport);
            this.groupBox1.Controls.Add(this.btnExcelExport);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cmbFromExport);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1193, 536);
            this.groupBox1.TabIndex = 167;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Export";
            // 
            // dgExportXls
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgExportXls.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgExportXls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgExportXls.Location = new System.Drawing.Point(12, 75);
            this.dgExportXls.Margin = new System.Windows.Forms.Padding(2);
            this.dgExportXls.Name = "dgExportXls";
            this.dgExportXls.RowTemplate.Height = 24;
            this.dgExportXls.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgExportXls.Size = new System.Drawing.Size(1176, 456);
            this.dgExportXls.TabIndex = 168;
            // 
            // btnAceptExport
            // 
            this.btnAceptExport.Location = new System.Drawing.Point(380, 42);
            this.btnAceptExport.Name = "btnAceptExport";
            this.btnAceptExport.Size = new System.Drawing.Size(75, 23);
            this.btnAceptExport.TabIndex = 167;
            this.btnAceptExport.Text = "Acept";
            this.btnAceptExport.UseVisualStyleBackColor = true;
            this.btnAceptExport.Click += new System.EventHandler(this.btnAceptExport_Click);
            // 
            // cmbToExport
            // 
            this.cmbToExport.FormattingEnabled = true;
            this.cmbToExport.Location = new System.Drawing.Point(193, 42);
            this.cmbToExport.Name = "cmbToExport";
            this.cmbToExport.Size = new System.Drawing.Size(171, 21);
            this.cmbToExport.TabIndex = 165;
            // 
            // btnExcelExport
            // 
            this.btnExcelExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExcelExport.Image")));
            this.btnExcelExport.InitialImage = null;
            this.btnExcelExport.Location = new System.Drawing.Point(471, 35);
            this.btnExcelExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnExcelExport.Name = "btnExcelExport";
            this.btnExcelExport.Size = new System.Drawing.Size(36, 36);
            this.btnExcelExport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnExcelExport.TabIndex = 166;
            this.btnExcelExport.TabStop = false;
            this.btnExcelExport.Click += new System.EventHandler(this.btnExcelExport_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(191, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 162;
            this.label9.Text = "To Sample: ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 163;
            this.label8.Text = "From Sample: ";
            // 
            // cmbFromExport
            // 
            this.cmbFromExport.FormattingEnabled = true;
            this.cmbFromExport.Location = new System.Drawing.Point(16, 42);
            this.cmbFromExport.Name = "cmbFromExport";
            this.cmbFromExport.Size = new System.Drawing.Size(171, 21);
            this.cmbFromExport.TabIndex = 164;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(12, 4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(180, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 
            // frmControlSampling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(1233, 661);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tbCtrlSamp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmControlSampling";
            this.ShowIcon = false;
            this.Text = "Control Sampling";
            this.tbCtrlSamp.ResumeLayout(false);
            this.tbQualityControlSamples.ResumeLayout(false);
            this.tbQualityControlSamples.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAssignQControl)).EndInit();
            this.tbSamplesAssign.ResumeLayout(false);
            this.tbSamplesAssign.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSamplesAssign)).EndInit();
            this.tbLoadSamples.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgLoadSamples)).EndInit();
            this.gbLoadSamples.ResumeLayout(false);
            this.gbLoadSamples.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbtnSearch)).EndInit();
            this.tbSamplesPrint.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgExportXls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExcelExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbCtrlSamp;
        private System.Windows.Forms.TabPage tbSamplesAssign;
        private System.Windows.Forms.TabPage tbQualityControlSamples;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbGeologist;
        private System.Windows.Forms.DateTimePicker dtDateAssign;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbToSamplesAssign;
        private System.Windows.Forms.ComboBox cmbFromSamplesAssign;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbSampleType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSamples;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCancelASamp;
        private System.Windows.Forms.Button btnAceptASamp;
        private System.Windows.Forms.Button btnCancelAQCS;
        private System.Windows.Forms.Button btnAceptAQCS;
        private System.Windows.Forms.DataGridView dgSamplesAssign;
        private System.Windows.Forms.DataGridView dgAssignQControl;
        private System.Windows.Forms.TabPage tbLoadSamples;
        private System.Windows.Forms.DataGridView dgLoadSamples;
        private System.Windows.Forms.GroupBox gbLoadSamples;
        private System.Windows.Forms.ComboBox cmbSheed;
        private System.Windows.Forms.PictureBox pbtnSearch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.Button btnAbrir;
        private System.Windows.Forms.OpenFileDialog oDialog;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TabPage tbSamplesPrint;
        private System.Windows.Forms.ComboBox cmbToExport;
        private System.Windows.Forms.ComboBox cmbFromExport;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox btnExcelExport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAceptExport;
        private System.Windows.Forms.DataGridView dgExportXls;
    }
}