using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EnviosColombiaGold
{
    partial class frmEnvios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEnvios));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnClean = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tbShipment = new System.Windows.Forms.TabPage();
            this.cmbLaboratory = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgDetalleInterval = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtDate = new System.Windows.Forms.DateTimePicker();
            this.TxtOtAnCod = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.CmbSamTyp = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.CmbTypSub = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.CmbNatSamp = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtElements = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtObserv = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtBags = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtInstructions = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMetAnCod = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.CmbAnCod = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbPrepCode = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbDisp = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbAnLab = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbPrepLab = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbLab = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmbShipment = new System.Windows.Forms.ComboBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbHoleId = new System.Windows.Forms.ComboBox();
            this.lblsampTo = new System.Windows.Forms.Label();
            this.lblsampfrom = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.CmbTS = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.a = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.dgInterval = new System.Windows.Forms.DataGridView();
            this.TabShipm = new System.Windows.Forms.TabControl();
            this.tbShipment.SuspendLayout();
            this.cmbLaboratory.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetalleInterval)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInterval)).BeginInit();
            this.TabShipm.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSave.Location = new System.Drawing.Point(10, 584);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(76, 31);
            this.btnSave.TabIndex = 24;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReport
            // 
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnReport.Location = new System.Drawing.Point(90, 584);
            this.btnReport.Margin = new System.Windows.Forms.Padding(2);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(76, 31);
            this.btnReport.TabIndex = 25;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnClean
            // 
            this.btnClean.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnClean.Location = new System.Drawing.Point(170, 584);
            this.btnClean.Margin = new System.Windows.Forms.Padding(2);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(74, 31);
            this.btnClean.TabIndex = 26;
            this.btnClean.Text = "Clean";
            this.btnClean.UseVisualStyleBackColor = true;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnModify
            // 
            this.btnModify.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnModify.Location = new System.Drawing.Point(249, 584);
            this.btnModify.Margin = new System.Windows.Forms.Padding(2);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(76, 31);
            this.btnModify.TabIndex = 27;
            this.btnModify.Text = "Modify";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnClose.Location = new System.Drawing.Point(329, 584);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(89, 31);
            this.btnClose.TabIndex = 29;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tbShipment
            // 
            this.tbShipment.Controls.Add(this.cmbLaboratory);
            this.tbShipment.Location = new System.Drawing.Point(4, 22);
            this.tbShipment.Name = "tbShipment";
            this.tbShipment.Size = new System.Drawing.Size(1265, 543);
            this.tbShipment.TabIndex = 0;
            this.tbShipment.UseVisualStyleBackColor = true;
            // 
            // cmbLaboratory
            // 
            this.cmbLaboratory.Controls.Add(this.groupBox1);
            this.cmbLaboratory.Controls.Add(this.groupBox3);
            this.cmbLaboratory.Controls.Add(this.lblUser);
            this.cmbLaboratory.Controls.Add(this.groupBox2);
            this.cmbLaboratory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cmbLaboratory.Location = new System.Drawing.Point(2, 2);
            this.cmbLaboratory.Margin = new System.Windows.Forms.Padding(2);
            this.cmbLaboratory.Name = "cmbLaboratory";
            this.cmbLaboratory.Padding = new System.Windows.Forms.Padding(2);
            this.cmbLaboratory.Size = new System.Drawing.Size(1270, 547);
            this.cmbLaboratory.TabIndex = 0;
            this.cmbLaboratory.TabStop = false;
            this.cmbLaboratory.Text = "Sending samples to the laboratory";
            this.cmbLaboratory.Enter += new System.EventHandler(this.cmbLaboratory_Enter);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgDetalleInterval);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox1.Location = new System.Drawing.Point(594, 248);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(641, 301);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sack and Samples";
            // 
            // dgDetalleInterval
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgDetalleInterval.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDetalleInterval.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetalleInterval.Location = new System.Drawing.Point(8, 20);
            this.dgDetalleInterval.Margin = new System.Windows.Forms.Padding(2);
            this.dgDetalleInterval.Name = "dgDetalleInterval";
            this.dgDetalleInterval.RowTemplate.Height = 24;
            this.dgDetalleInterval.Size = new System.Drawing.Size(609, 265);
            this.dgDetalleInterval.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.comboBox1);
            this.groupBox3.Controls.Add(this.txtDate);
            this.groupBox3.Controls.Add(this.TxtOtAnCod);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.CmbSamTyp);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.CmbTypSub);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.CmbNatSamp);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.txtElements);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.txtObserv);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.txtBags);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.txtInstructions);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txtMetAnCod);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.CmbAnCod);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.cmbPrepCode);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.cmbDisp);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cmbAnLab);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cmbPrepLab);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cmbLab);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btnFind);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Controls.Add(this.cmbShipment);
            this.groupBox3.Location = new System.Drawing.Point(16, 30);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(1219, 215);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 10F);
            this.label2.Location = new System.Drawing.Point(386, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 17);
            this.label2.TabIndex = 46;
            this.label2.Text = "Select group";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(389, 54);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(176, 24);
            this.comboBox1.TabIndex = 45;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // txtDate
            // 
            this.txtDate.CustomFormat = "yyyy/MM/dd";
            this.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDate.Location = new System.Drawing.Point(579, 145);
            this.txtDate.Margin = new System.Windows.Forms.Padding(2);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(197, 23);
            this.txtDate.TabIndex = 11;
            this.txtDate.ValueChanged += new System.EventHandler(this.txtDate_ValueChanged);
            // 
            // TxtOtAnCod
            // 
            this.TxtOtAnCod.Location = new System.Drawing.Point(210, 145);
            this.TxtOtAnCod.Margin = new System.Windows.Forms.Padding(2);
            this.TxtOtAnCod.Name = "TxtOtAnCod";
            this.TxtOtAnCod.Size = new System.Drawing.Size(175, 23);
            this.TxtOtAnCod.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 10F);
            this.label11.Location = new System.Drawing.Point(207, 126);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(146, 17);
            this.label11.TabIndex = 42;
            this.label11.Text = "Other Analisis Code";
            // 
            // CmbSamTyp
            // 
            this.CmbSamTyp.FormattingEnabled = true;
            this.CmbSamTyp.Location = new System.Drawing.Point(952, 50);
            this.CmbSamTyp.Margin = new System.Windows.Forms.Padding(2);
            this.CmbSamTyp.Name = "CmbSamTyp";
            this.CmbSamTyp.Size = new System.Drawing.Size(176, 24);
            this.CmbSamTyp.TabIndex = 1;
            this.CmbSamTyp.SelectedIndexChanged += new System.EventHandler(this.CmbSamTyp_SelectedIndexChanged);
            this.CmbSamTyp.RightToLeftChanged += new System.EventHandler(this.CmbSamTyp_RightToLeftChanged);
            this.CmbSamTyp.TabIndexChanged += new System.EventHandler(this.CmbSamTyp_TabIndexChanged);
            this.CmbSamTyp.Leave += new System.EventHandler(this.CmbSamTyp_Leave);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Verdana", 10F);
            this.label19.Location = new System.Drawing.Point(951, 31);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(97, 17);
            this.label19.TabIndex = 40;
            this.label19.Text = "Sample Type";
            // 
            // CmbTypSub
            // 
            this.CmbTypSub.FormattingEnabled = true;
            this.CmbTypSub.Location = new System.Drawing.Point(17, 100);
            this.CmbTypSub.Margin = new System.Windows.Forms.Padding(2);
            this.CmbTypSub.Name = "CmbTypSub";
            this.CmbTypSub.Size = new System.Drawing.Size(176, 24);
            this.CmbTypSub.TabIndex = 2;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Verdana", 10F);
            this.label18.Location = new System.Drawing.Point(16, 82);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(113, 17);
            this.label18.TabIndex = 38;
            this.label18.Text = "Shipment Type";
            // 
            // CmbNatSamp
            // 
            this.CmbNatSamp.FormattingEnabled = true;
            this.CmbNatSamp.Location = new System.Drawing.Point(393, 143);
            this.CmbNatSamp.Margin = new System.Windows.Forms.Padding(2);
            this.CmbNatSamp.Name = "CmbNatSamp";
            this.CmbNatSamp.Size = new System.Drawing.Size(176, 24);
            this.CmbNatSamp.TabIndex = 7;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Verdana", 10F);
            this.label17.Location = new System.Drawing.Point(394, 125);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(130, 17);
            this.label17.TabIndex = 36;
            this.label17.Text = "Nature of Sample";
            // 
            // txtElements
            // 
            this.txtElements.Location = new System.Drawing.Point(386, 185);
            this.txtElements.Margin = new System.Windows.Forms.Padding(2);
            this.txtElements.Name = "txtElements";
            this.txtElements.Size = new System.Drawing.Size(147, 23);
            this.txtElements.TabIndex = 16;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 10F);
            this.label15.Location = new System.Drawing.Point(386, 168);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(72, 17);
            this.label15.TabIndex = 34;
            this.label15.Text = "Elements";
            // 
            // txtObserv
            // 
            this.txtObserv.Location = new System.Drawing.Point(202, 185);
            this.txtObserv.Margin = new System.Windows.Forms.Padding(2);
            this.txtObserv.Name = "txtObserv";
            this.txtObserv.Size = new System.Drawing.Size(174, 23);
            this.txtObserv.TabIndex = 15;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 10F);
            this.label14.Location = new System.Drawing.Point(201, 168);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(101, 17);
            this.label14.TabIndex = 32;
            this.label14.Text = "Observations";
            // 
            // txtBags
            // 
            this.txtBags.Enabled = false;
            this.txtBags.Location = new System.Drawing.Point(795, 144);
            this.txtBags.Margin = new System.Windows.Forms.Padding(2);
            this.txtBags.Name = "txtBags";
            this.txtBags.Size = new System.Drawing.Size(176, 23);
            this.txtBags.TabIndex = 12;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 10F);
            this.label13.Location = new System.Drawing.Point(765, 126);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 17);
            this.label13.TabIndex = 30;
            this.label13.Text = "Bags No";
            // 
            // txtInstructions
            // 
            this.txtInstructions.Location = new System.Drawing.Point(15, 185);
            this.txtInstructions.Margin = new System.Windows.Forms.Padding(2);
            this.txtInstructions.Name = "txtInstructions";
            this.txtInstructions.Size = new System.Drawing.Size(176, 23);
            this.txtInstructions.TabIndex = 14;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 10F);
            this.label12.Location = new System.Drawing.Point(16, 169);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 17);
            this.label12.TabIndex = 28;
            this.label12.Text = "Instructions";
            // 
            // txtMetAnCod
            // 
            this.txtMetAnCod.Location = new System.Drawing.Point(17, 145);
            this.txtMetAnCod.Margin = new System.Windows.Forms.Padding(2);
            this.txtMetAnCod.Name = "txtMetAnCod";
            this.txtMetAnCod.Size = new System.Drawing.Size(176, 23);
            this.txtMetAnCod.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 10F);
            this.label10.Location = new System.Drawing.Point(17, 126);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(131, 17);
            this.label10.TabIndex = 24;
            this.label10.Text = "Met Analisis Code";
            // 
            // CmbAnCod
            // 
            this.CmbAnCod.FormattingEnabled = true;
            this.CmbAnCod.Location = new System.Drawing.Point(981, 100);
            this.CmbAnCod.Margin = new System.Windows.Forms.Padding(2);
            this.CmbAnCod.Name = "CmbAnCod";
            this.CmbAnCod.Size = new System.Drawing.Size(176, 24);
            this.CmbAnCod.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 10F);
            this.label9.Location = new System.Drawing.Point(979, 82);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 17);
            this.label9.TabIndex = 22;
            this.label9.Text = "Au Analisis Code";
            // 
            // cmbPrepCode
            // 
            this.cmbPrepCode.FormattingEnabled = true;
            this.cmbPrepCode.Location = new System.Drawing.Point(795, 101);
            this.cmbPrepCode.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPrepCode.Name = "cmbPrepCode";
            this.cmbPrepCode.Size = new System.Drawing.Size(176, 24);
            this.cmbPrepCode.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 10F);
            this.label8.Location = new System.Drawing.Point(793, 82);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(129, 17);
            this.label8.TabIndex = 20;
            this.label8.Text = "Preparation Code";
            // 
            // cmbDisp
            // 
            this.cmbDisp.FormattingEnabled = true;
            this.cmbDisp.Location = new System.Drawing.Point(984, 144);
            this.cmbDisp.Margin = new System.Windows.Forms.Padding(2);
            this.cmbDisp.Name = "cmbDisp";
            this.cmbDisp.Size = new System.Drawing.Size(176, 24);
            this.cmbDisp.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 10F);
            this.label7.Location = new System.Drawing.Point(956, 126);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 17);
            this.label7.TabIndex = 18;
            this.label7.Text = "Dispatched by";
            // 
            // cmbAnLab
            // 
            this.cmbAnLab.FormattingEnabled = true;
            this.cmbAnLab.Location = new System.Drawing.Point(579, 101);
            this.cmbAnLab.Margin = new System.Windows.Forms.Padding(2);
            this.cmbAnLab.Name = "cmbAnLab";
            this.cmbAnLab.Size = new System.Drawing.Size(197, 24);
            this.cmbAnLab.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 10F);
            this.label6.Location = new System.Drawing.Point(577, 83);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 17);
            this.label6.TabIndex = 16;
            this.label6.Text = "Analitical Lab";
            // 
            // cmbPrepLab
            // 
            this.cmbPrepLab.FormattingEnabled = true;
            this.cmbPrepLab.Location = new System.Drawing.Point(393, 100);
            this.cmbPrepLab.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPrepLab.Name = "cmbPrepLab";
            this.cmbPrepLab.Size = new System.Drawing.Size(176, 24);
            this.cmbPrepLab.TabIndex = 9;
            this.cmbPrepLab.SelectedIndexChanged += new System.EventHandler(this.cmbPrepLab_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 10F);
            this.label5.Location = new System.Drawing.Point(391, 83);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Preparation Lab";
            // 
            // cmbLab
            // 
            this.cmbLab.FormattingEnabled = true;
            this.cmbLab.Location = new System.Drawing.Point(209, 101);
            this.cmbLab.Margin = new System.Windows.Forms.Padding(2);
            this.cmbLab.Name = "cmbLab";
            this.cmbLab.Size = new System.Drawing.Size(176, 24);
            this.cmbLab.TabIndex = 8;
            this.cmbLab.SelectedIndexChanged += new System.EventHandler(this.cmbLab_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 10F);
            this.label4.Location = new System.Drawing.Point(208, 82);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "Laboratory";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 10F);
            this.label3.Location = new System.Drawing.Point(578, 127);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Date";
            // 
            // btnFind
            // 
            this.btnFind.Font = new System.Drawing.Font("Verdana", 10F);
            this.btnFind.Location = new System.Drawing.Point(864, 52);
            this.btnFind.Margin = new System.Windows.Forms.Padding(2);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(84, 23);
            this.btnFind.TabIndex = 0;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 10F);
            this.label1.Location = new System.Drawing.Point(570, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Shipment No";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::EnviosColombiaGold.Properties.Resources.GranColombiaGold;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(19, 26);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(201, 45);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // cmbShipment
            // 
            this.cmbShipment.FormattingEnabled = true;
            this.cmbShipment.Location = new System.Drawing.Point(670, 52);
            this.cmbShipment.Margin = new System.Windows.Forms.Padding(2);
            this.cmbShipment.Name = "cmbShipment";
            this.cmbShipment.Size = new System.Drawing.Size(176, 24);
            this.cmbShipment.TabIndex = 0;
            this.cmbShipment.SelectedIndexChanged += new System.EventHandler(this.cmbShipment_SelectedIndexChanged);
            this.cmbShipment.Leave += new System.EventHandler(this.cmbShipment_Leave);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Verdana", 10F);
            this.lblUser.Location = new System.Drawing.Point(1051, 14);
            this.lblUser.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(90, 17);
            this.lblUser.TabIndex = 43;
            this.lblUser.Text = "Welcome ...";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbHoleId);
            this.groupBox2.Controls.Add(this.lblsampTo);
            this.groupBox2.Controls.Add(this.lblsampfrom);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.CmbTS);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.a);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.dgInterval);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 10F);
            this.groupBox2.Location = new System.Drawing.Point(16, 247);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(569, 304);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Interval";
            // 
            // cmbHoleId
            // 
            this.cmbHoleId.FormattingEnabled = true;
            this.cmbHoleId.Location = new System.Drawing.Point(13, 36);
            this.cmbHoleId.Name = "cmbHoleId";
            this.cmbHoleId.Size = new System.Drawing.Size(93, 24);
            this.cmbHoleId.TabIndex = 18;
            this.cmbHoleId.SelectedIndexChanged += new System.EventHandler(this.cmbHoleId_SelectedIndexChanged);
            // 
            // lblsampTo
            // 
            this.lblsampTo.AutoSize = true;
            this.lblsampTo.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsampTo.ForeColor = System.Drawing.Color.Blue;
            this.lblsampTo.Location = new System.Drawing.Point(225, 62);
            this.lblsampTo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblsampTo.Name = "lblsampTo";
            this.lblsampTo.Size = new System.Drawing.Size(67, 13);
            this.lblsampTo.TabIndex = 43;
            this.lblsampTo.Text = "To Sample";
            // 
            // lblsampfrom
            // 
            this.lblsampfrom.AutoSize = true;
            this.lblsampfrom.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsampfrom.ForeColor = System.Drawing.Color.Blue;
            this.lblsampfrom.Location = new System.Drawing.Point(121, 62);
            this.lblsampfrom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblsampfrom.Name = "lblsampfrom";
            this.lblsampfrom.Size = new System.Drawing.Size(83, 13);
            this.lblsampfrom.TabIndex = 42;
            this.lblsampfrom.Text = "From Sample";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(434, 21);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 36);
            this.btnDelete.TabIndex = 23;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(351, 21);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(79, 36);
            this.btnAdd.TabIndex = 21;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Verdana", 10F);
            this.label21.Location = new System.Drawing.Point(223, 17);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(80, 17);
            this.label21.TabIndex = 41;
            this.label21.Text = "To Sample";
            // 
            // CmbTS
            // 
            this.CmbTS.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.CmbTS.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.CmbTS.FormattingEnabled = true;
            this.CmbTS.Location = new System.Drawing.Point(224, 36);
            this.CmbTS.Margin = new System.Windows.Forms.Padding(2);
            this.CmbTS.Name = "CmbTS";
            this.CmbTS.Size = new System.Drawing.Size(92, 24);
            this.CmbTS.TabIndex = 20;
            this.CmbTS.SelectedIndexChanged += new System.EventHandler(this.CmbTS_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Verdana", 10F);
            this.label20.Location = new System.Drawing.Point(119, 18);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(100, 17);
            this.label20.TabIndex = 39;
            this.label20.Text = "From Sample";
            // 
            // a
            // 
            this.a.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.a.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.a.FormattingEnabled = true;
            this.a.Location = new System.Drawing.Point(120, 36);
            this.a.Margin = new System.Windows.Forms.Padding(2);
            this.a.Name = "a";
            this.a.Size = new System.Drawing.Size(92, 24);
            this.a.TabIndex = 19;
            this.a.SelectedIndexChanged += new System.EventHandler(this.CmbFS_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 10F);
            this.label16.Location = new System.Drawing.Point(12, 17);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 17);
            this.label16.TabIndex = 37;
            this.label16.Text = "Hole ID";
            // 
            // dgInterval
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgInterval.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgInterval.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgInterval.Location = new System.Drawing.Point(15, 83);
            this.dgInterval.Margin = new System.Windows.Forms.Padding(2);
            this.dgInterval.Name = "dgInterval";
            this.dgInterval.RowTemplate.Height = 24;
            this.dgInterval.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgInterval.Size = new System.Drawing.Size(550, 203);
            this.dgInterval.TabIndex = 22;
            this.dgInterval.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgInterval_CellContentDoubleClick);
            // 
            // TabShipm
            // 
            this.TabShipm.Controls.Add(this.tbShipment);
            this.TabShipm.Location = new System.Drawing.Point(10, 3);
            this.TabShipm.Name = "TabShipm";
            this.TabShipm.SelectedIndex = 0;
            this.TabShipm.Size = new System.Drawing.Size(1273, 569);
            this.TabShipm.TabIndex = 30;
            this.TabShipm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabControl1_KeyDown);
            // 
            // frmEnvios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 619);
            this.Controls.Add(this.TabShipm);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEnvios";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Shipment";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEnvios_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmEnvios_FormClosed);
            this.Load += new System.EventHandler(this.frmEnvios_Load);
            this.tbShipment.ResumeLayout(false);
            this.cmbLaboratory.ResumeLayout(false);
            this.cmbLaboratory.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetalleInterval)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInterval)).EndInit();
            this.TabShipm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnClean;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabPage tbShipment;
        private System.Windows.Forms.GroupBox cmbLaboratory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgDetalleInterval;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.DateTimePicker txtDate;
        private System.Windows.Forms.TextBox TxtOtAnCod;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox CmbSamTyp;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox CmbTypSub;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox CmbNatSamp;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtElements;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtObserv;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtBags;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtInstructions;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtMetAnCod;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox CmbAnCod;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbPrepCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbDisp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbAnLab;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbPrepLab;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbLab;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbShipment;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbHoleId;
        private System.Windows.Forms.Label lblsampTo;
        private System.Windows.Forms.Label lblsampfrom;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox CmbTS;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox a;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DataGridView dgInterval;
        private System.Windows.Forms.TabControl TabShipm;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}