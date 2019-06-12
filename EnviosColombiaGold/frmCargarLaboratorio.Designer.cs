namespace EnviosColombiaGold
{
    partial class frmCargarLaboratorio
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCargarLaboratorio));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.dgXls = new System.Windows.Forms.DataGridView();
            this.oDialog = new System.Windows.Forms.OpenFileDialog();
            this.tMessage = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabJobAssay = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.LblTitulos = new System.Windows.Forms.Label();
            this.tabReimp = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtSubmit = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbJobNo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pbtnSearch = new System.Windows.Forms.PictureBox();
            this.pbtnPrint = new System.Windows.Forms.PictureBox();
            this.pbtnUpdate = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgXls)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabJobAssay.SuspendLayout();
            this.tabReimp.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtnSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtnPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtnUpdate)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.pbtnSearch);
            this.groupBox1.Controls.Add(this.LblTitulos);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtRuta);
            this.groupBox1.Location = new System.Drawing.Point(6, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1113, 91);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "File Path Xls";
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(86, 52);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(950, 20);
            this.txtRuta.TabIndex = 1;
            // 
            // dgXls
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgXls.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgXls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgXls.Location = new System.Drawing.Point(1, 111);
            this.dgXls.Name = "dgXls";
            this.dgXls.Size = new System.Drawing.Size(1213, 515);
            this.dgXls.TabIndex = 5;
            // 
            // oDialog
            // 
            this.oDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabJobAssay);
            this.tabControl1.Controls.Add(this.tabReimp);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1233, 661);
            this.tabControl1.TabIndex = 9;
            // 
            // tabJobAssay
            // 
            this.tabJobAssay.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.tabJobAssay.Controls.Add(this.label7);
            this.tabJobAssay.Controls.Add(this.label2);
            this.tabJobAssay.Controls.Add(this.pictureBox1);
            this.tabJobAssay.Controls.Add(this.groupBox1);
            this.tabJobAssay.Controls.Add(this.dgXls);
            this.tabJobAssay.Location = new System.Drawing.Point(4, 22);
            this.tabJobAssay.Name = "tabJobAssay";
            this.tabJobAssay.Padding = new System.Windows.Forms.Padding(3);
            this.tabJobAssay.Size = new System.Drawing.Size(1225, 635);
            this.tabJobAssay.TabIndex = 0;
            this.tabJobAssay.Text = "Job Assay Results";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Britannic Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(487, 323);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(231, 65);
            this.label2.TabIndex = 94;
            this.label2.Text = "loading...";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Visible = false;
            // 
            // LblTitulos
            // 
            this.LblTitulos.BackColor = System.Drawing.Color.Transparent;
            this.LblTitulos.Font = new System.Drawing.Font("Britannic Bold", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitulos.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LblTitulos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LblTitulos.Location = new System.Drawing.Point(388, 7);
            this.LblTitulos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblTitulos.Name = "LblTitulos";
            this.LblTitulos.Size = new System.Drawing.Size(455, 43);
            this.LblTitulos.TabIndex = 92;
            this.LblTitulos.Text = "Load assay GZC";
            this.LblTitulos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabReimp
            // 
            this.tabReimp.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.tabReimp.Controls.Add(this.label4);
            this.tabReimp.Controls.Add(this.groupBox2);
            this.tabReimp.Location = new System.Drawing.Point(4, 22);
            this.tabReimp.Name = "tabReimp";
            this.tabReimp.Padding = new System.Windows.Forms.Padding(3);
            this.tabReimp.Size = new System.Drawing.Size(1225, 635);
            this.tabReimp.TabIndex = 1;
            this.tabReimp.Text = "Reimp QC Report";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Britannic Bold", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Silver;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(375, 25);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(455, 45);
            this.label4.TabIndex = 93;
            this.label4.Text = "Load assay GZC";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnFind);
            this.groupBox2.Controls.Add(this.txtSubmit);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.pbtnPrint);
            this.groupBox2.Controls.Add(this.cmbJobNo);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(6, 70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(518, 144);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Reimp QC Report";
            // 
            // btnFind
            // 
            this.btnFind.Font = new System.Drawing.Font("Verdana", 10F);
            this.btnFind.Location = new System.Drawing.Point(327, 50);
            this.btnFind.Margin = new System.Windows.Forms.Padding(2);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(84, 23);
            this.btnFind.TabIndex = 21;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtSubmit
            // 
            this.txtSubmit.Enabled = false;
            this.txtSubmit.Location = new System.Drawing.Point(80, 77);
            this.txtSubmit.Name = "txtSubmit";
            this.txtSubmit.Size = new System.Drawing.Size(225, 20);
            this.txtSubmit.TabIndex = 20;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(204, 109);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Change Type";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "RO",
            "PR",
            "CH",
            "DCO",
            "CRD"});
            this.comboBox1.Location = new System.Drawing.Point(80, 111);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(106, 21);
            this.comboBox1.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Type";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Submit";
            // 
            // cmbJobNo
            // 
            this.cmbJobNo.FormattingEnabled = true;
            this.cmbJobNo.Location = new System.Drawing.Point(81, 50);
            this.cmbJobNo.Name = "cmbJobNo";
            this.cmbJobNo.Size = new System.Drawing.Size(225, 21);
            this.cmbJobNo.TabIndex = 6;
            this.cmbJobNo.SelectedIndexChanged += new System.EventHandler(this.cmbJobNo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Job No";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::EnviosColombiaGold.Properties.Resources.nut_1420234_640__2___2_;
            this.pictureBox1.Location = new System.Drawing.Point(1125, 55);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(62, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            this.tMessage.SetToolTip(this.pictureBox1, "Update");
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(1078, 50);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(26, 22);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 93;
            this.pictureBox2.TabStop = false;
            this.tMessage.SetToolTip(this.pictureBox2, "Limpiar");
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pbtnSearch
            // 
            this.pbtnSearch.Image = ((System.Drawing.Image)(resources.GetObject("pbtnSearch.Image")));
            this.pbtnSearch.Location = new System.Drawing.Point(1042, 50);
            this.pbtnSearch.Name = "pbtnSearch";
            this.pbtnSearch.Size = new System.Drawing.Size(26, 22);
            this.pbtnSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbtnSearch.TabIndex = 9;
            this.pbtnSearch.TabStop = false;
            this.tMessage.SetToolTip(this.pbtnSearch, "Search");
            this.pbtnSearch.Click += new System.EventHandler(this.pbtnSearch_Click);
            // 
            // pbtnPrint
            // 
            this.pbtnPrint.Image = ((System.Drawing.Image)(resources.GetObject("pbtnPrint.Image")));
            this.pbtnPrint.Location = new System.Drawing.Point(434, 47);
            this.pbtnPrint.Name = "pbtnPrint";
            this.pbtnPrint.Size = new System.Drawing.Size(62, 50);
            this.pbtnPrint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbtnPrint.TabIndex = 9;
            this.pbtnPrint.TabStop = false;
            this.tMessage.SetToolTip(this.pbtnPrint, "Print");
            this.pbtnPrint.Click += new System.EventHandler(this.pbtnPrint_Click);
            // 
            // pbtnUpdate
            // 
            this.pbtnUpdate.Enabled = false;
            this.pbtnUpdate.Image = ((System.Drawing.Image)(resources.GetObject("pbtnUpdate.Image")));
            this.pbtnUpdate.Location = new System.Drawing.Point(128, 419);
            this.pbtnUpdate.Name = "pbtnUpdate";
            this.pbtnUpdate.Size = new System.Drawing.Size(62, 50);
            this.pbtnUpdate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbtnUpdate.TabIndex = 8;
            this.pbtnUpdate.TabStop = false;
            this.tMessage.SetToolTip(this.pbtnUpdate, "Update");
            this.pbtnUpdate.Click += new System.EventHandler(this.pbtnUpdate_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Britannic Bold", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Location = new System.Drawing.Point(1120, 17);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 35);
            this.label7.TabIndex = 94;
            this.label7.Text = "Cargar";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmCargarLaboratorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(1233, 661);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pbtnUpdate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCargarLaboratorio";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Job Assay Results";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCargarResEnvio_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgXls)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabJobAssay.ResumeLayout(false);
            this.tabReimp.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtnSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtnPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtnUpdate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.DataGridView dgXls;
        private System.Windows.Forms.OpenFileDialog oDialog;
        private System.Windows.Forms.PictureBox pbtnUpdate;
        private System.Windows.Forms.ToolTip tMessage;
        private System.Windows.Forms.PictureBox pbtnSearch;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabJobAssay;
        private System.Windows.Forms.TabPage tabReimp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbJobNo;
        private System.Windows.Forms.PictureBox pbtnPrint;
        private System.Windows.Forms.Label LblTitulos;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSubmit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label label7;
    }
}