namespace EnviosColombiaGold.Topography
{
    partial class frmAdminTopography
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
            this.grpMater = new System.Windows.Forms.GroupBox();
            this.cmbMaterID = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.grpCodeID = new System.Windows.Forms.GroupBox();
            this.txtCodeId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtDescriptionId = new System.Windows.Forms.TextBox();
            this.grpValuelSetings = new System.Windows.Forms.GroupBox();
            this.dtgDetailMasterID = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.grpValueTypeCond = new System.Windows.Forms.GroupBox();
            this.cboTypeCondition = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescrption = new System.Windows.Forms.TextBox();
            this.grbValueIdSelect = new System.Windows.Forms.GroupBox();
            this.cboConditions = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.grpEvent = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.LblTitulos = new System.Windows.Forms.Label();
            this.grpMater.SuspendLayout();
            this.grpCodeID.SuspendLayout();
            this.grpValuelSetings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDetailMasterID)).BeginInit();
            this.grpValueTypeCond.SuspendLayout();
            this.grbValueIdSelect.SuspendLayout();
            this.grpEvent.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMater
            // 
            this.grpMater.Controls.Add(this.cmbMaterID);
            this.grpMater.Controls.Add(this.label11);
            this.grpMater.Font = new System.Drawing.Font("Rockwell", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpMater.ForeColor = System.Drawing.Color.Navy;
            this.grpMater.Location = new System.Drawing.Point(5, 57);
            this.grpMater.Name = "grpMater";
            this.grpMater.Size = new System.Drawing.Size(366, 65);
            this.grpMater.TabIndex = 129;
            this.grpMater.TabStop = false;
            // 
            // cmbMaterID
            // 
            this.cmbMaterID.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.cmbMaterID.FormattingEnabled = true;
            this.cmbMaterID.Location = new System.Drawing.Point(6, 28);
            this.cmbMaterID.Name = "cmbMaterID";
            this.cmbMaterID.Size = new System.Drawing.Size(347, 33);
            this.cmbMaterID.TabIndex = 110;
            this.cmbMaterID.SelectedIndexChanged += new System.EventHandler(this.cmbMaterID_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 14);
            this.label11.TabIndex = 109;
            this.label11.Text = "Master";
            // 
            // grpCodeID
            // 
            this.grpCodeID.Controls.Add(this.txtCodeId);
            this.grpCodeID.Controls.Add(this.label3);
            this.grpCodeID.Controls.Add(this.label20);
            this.grpCodeID.Controls.Add(this.label18);
            this.grpCodeID.Controls.Add(this.txtDescriptionId);
            this.grpCodeID.Font = new System.Drawing.Font("Rockwell", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCodeID.ForeColor = System.Drawing.Color.Navy;
            this.grpCodeID.Location = new System.Drawing.Point(377, 57);
            this.grpCodeID.Name = "grpCodeID";
            this.grpCodeID.Size = new System.Drawing.Size(482, 65);
            this.grpCodeID.TabIndex = 140;
            this.grpCodeID.TabStop = false;
            // 
            // txtCodeId
            // 
            this.txtCodeId.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtCodeId.Location = new System.Drawing.Point(10, 27);
            this.txtCodeId.Multiline = true;
            this.txtCodeId.Name = "txtCodeId";
            this.txtCodeId.Size = new System.Drawing.Size(75, 34);
            this.txtCodeId.TabIndex = 140;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 14);
            this.label3.TabIndex = 139;
            this.label3.Text = "Code";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(91, 13);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(71, 14);
            this.label20.TabIndex = 138;
            this.label20.Text = "Description";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(7, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(78, 14);
            this.label18.TabIndex = 109;
            this.label18.Text = "Code - Detail";
            // 
            // txtDescriptionId
            // 
            this.txtDescriptionId.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtDescriptionId.Location = new System.Drawing.Point(91, 27);
            this.txtDescriptionId.Multiline = true;
            this.txtDescriptionId.Name = "txtDescriptionId";
            this.txtDescriptionId.Size = new System.Drawing.Size(385, 34);
            this.txtDescriptionId.TabIndex = 137;
            // 
            // grpValuelSetings
            // 
            this.grpValuelSetings.Controls.Add(this.dtgDetailMasterID);
            this.grpValuelSetings.Controls.Add(this.label4);
            this.grpValuelSetings.Font = new System.Drawing.Font("Arial", 10F);
            this.grpValuelSetings.ForeColor = System.Drawing.Color.Navy;
            this.grpValuelSetings.Location = new System.Drawing.Point(5, 188);
            this.grpValuelSetings.Name = "grpValuelSetings";
            this.grpValuelSetings.Size = new System.Drawing.Size(854, 480);
            this.grpValuelSetings.TabIndex = 142;
            this.grpValuelSetings.TabStop = false;
            this.grpValuelSetings.Text = "configured values";
            // 
            // dtgDetailMasterID
            // 
            this.dtgDetailMasterID.AllowUserToAddRows = false;
            this.dtgDetailMasterID.AllowUserToDeleteRows = false;
            this.dtgDetailMasterID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgDetailMasterID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgDetailMasterID.Location = new System.Drawing.Point(3, 19);
            this.dtgDetailMasterID.Name = "dtgDetailMasterID";
            this.dtgDetailMasterID.ReadOnly = true;
            this.dtgDetailMasterID.RowHeadersWidth = 30;
            this.dtgDetailMasterID.Size = new System.Drawing.Size(848, 458);
            this.dtgDetailMasterID.TabIndex = 112;
            this.dtgDetailMasterID.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgDetailMasterID_CellClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(615, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 16);
            this.label4.TabIndex = 109;
            this.label4.Text = "Value Adtional";
            // 
            // grpValueTypeCond
            // 
            this.grpValueTypeCond.Controls.Add(this.cboTypeCondition);
            this.grpValueTypeCond.Controls.Add(this.label1);
            this.grpValueTypeCond.Font = new System.Drawing.Font("Rockwell", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpValueTypeCond.ForeColor = System.Drawing.Color.Navy;
            this.grpValueTypeCond.Location = new System.Drawing.Point(5, 125);
            this.grpValueTypeCond.Name = "grpValueTypeCond";
            this.grpValueTypeCond.Size = new System.Drawing.Size(311, 66);
            this.grpValueTypeCond.TabIndex = 144;
            this.grpValueTypeCond.TabStop = false;
            // 
            // cboTypeCondition
            // 
            this.cboTypeCondition.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.cboTypeCondition.FormattingEnabled = true;
            this.cboTypeCondition.Location = new System.Drawing.Point(10, 26);
            this.cboTypeCondition.Name = "cboTypeCondition";
            this.cboTypeCondition.Size = new System.Drawing.Size(295, 33);
            this.cboTypeCondition.TabIndex = 140;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(113, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 14);
            this.label1.TabIndex = 139;
            this.label1.Text = "Type - Conditions";
            // 
            // txtDescrption
            // 
            this.txtDescrption.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txtDescrption.Location = new System.Drawing.Point(4, 26);
            this.txtDescrption.Multiline = true;
            this.txtDescrption.Name = "txtDescrption";
            this.txtDescrption.Size = new System.Drawing.Size(393, 34);
            this.txtDescrption.TabIndex = 137;
            // 
            // grbValueIdSelect
            // 
            this.grbValueIdSelect.Controls.Add(this.cboConditions);
            this.grbValueIdSelect.Controls.Add(this.label8);
            this.grbValueIdSelect.Controls.Add(this.label6);
            this.grbValueIdSelect.Controls.Add(this.txtDescrption);
            this.grbValueIdSelect.Font = new System.Drawing.Font("Rockwell", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbValueIdSelect.ForeColor = System.Drawing.Color.Navy;
            this.grbValueIdSelect.Location = new System.Drawing.Point(322, 123);
            this.grbValueIdSelect.Name = "grbValueIdSelect";
            this.grbValueIdSelect.Size = new System.Drawing.Size(536, 66);
            this.grbValueIdSelect.TabIndex = 145;
            this.grbValueIdSelect.TabStop = false;
            // 
            // cboConditions
            // 
            this.cboConditions.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.cboConditions.FormattingEnabled = true;
            this.cboConditions.Location = new System.Drawing.Point(402, 26);
            this.cboConditions.Name = "cboConditions";
            this.cboConditions.Size = new System.Drawing.Size(128, 33);
            this.cboConditions.TabIndex = 142;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(417, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 14);
            this.label8.TabIndex = 141;
            this.label8.Text = "Type - Conditions";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(172, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 14);
            this.label6.TabIndex = 138;
            this.label6.Text = "Comments";
            // 
            // grpEvent
            // 
            this.grpEvent.Controls.Add(this.label5);
            this.grpEvent.Controls.Add(this.btnNew);
            this.grpEvent.Controls.Add(this.label9);
            this.grpEvent.Controls.Add(this.btnUpdate);
            this.grpEvent.Controls.Add(this.label7);
            this.grpEvent.Controls.Add(this.btnSave);
            this.grpEvent.Font = new System.Drawing.Font("Rockwell", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpEvent.ForeColor = System.Drawing.Color.Navy;
            this.grpEvent.Location = new System.Drawing.Point(678, -1);
            this.grpEvent.Name = "grpEvent";
            this.grpEvent.Size = new System.Drawing.Size(183, 72);
            this.grpEvent.TabIndex = 149;
            this.grpEvent.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(134, -1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 14);
            this.label5.TabIndex = 153;
            this.label5.Text = "New";
            // 
            // btnNew
            // 
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnNew.Image = global::EnviosColombiaGold.Properties.Resources.new_162;
            this.btnNew.Location = new System.Drawing.Point(119, 14);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(56, 53);
            this.btnNew.TabIndex = 152;
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(69, -1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 14);
            this.label9.TabIndex = 114;
            this.label9.Text = "Update";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnUpdate.Image = global::EnviosColombiaGold.Properties.Resources.Refresh_icon_Value;
            this.btnUpdate.Location = new System.Drawing.Point(62, 14);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(56, 53);
            this.btnUpdate.TabIndex = 148;
            this.btnUpdate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 14);
            this.label7.TabIndex = 147;
            this.label7.Text = "Save";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnSave.Image = global::EnviosColombiaGold.Properties.Resources.save_162;
            this.btnSave.Location = new System.Drawing.Point(5, 14);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 53);
            this.btnSave.TabIndex = 145;
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.button1_Click);
            // 
            // LblTitulos
            // 
            this.LblTitulos.BackColor = System.Drawing.Color.Transparent;
            this.LblTitulos.Font = new System.Drawing.Font("Britannic Bold", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitulos.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LblTitulos.Image = global::EnviosColombiaGold.Properties.Resources.BarrasL;
            this.LblTitulos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LblTitulos.Location = new System.Drawing.Point(-2, -1);
            this.LblTitulos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblTitulos.Name = "LblTitulos";
            this.LblTitulos.Size = new System.Drawing.Size(860, 72);
            this.LblTitulos.TabIndex = 128;
            this.LblTitulos.Text = "Admin Master Topography";
            this.LblTitulos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmAdminTopography
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 673);
            this.Controls.Add(this.grpEvent);
            this.Controls.Add(this.grpValueTypeCond);
            this.Controls.Add(this.grpValuelSetings);
            this.Controls.Add(this.grbValueIdSelect);
            this.Controls.Add(this.grpCodeID);
            this.Controls.Add(this.grpMater);
            this.Controls.Add(this.LblTitulos);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdminTopography";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Topography";
            this.grpMater.ResumeLayout(false);
            this.grpMater.PerformLayout();
            this.grpCodeID.ResumeLayout(false);
            this.grpCodeID.PerformLayout();
            this.grpValuelSetings.ResumeLayout(false);
            this.grpValuelSetings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDetailMasterID)).EndInit();
            this.grpValueTypeCond.ResumeLayout(false);
            this.grpValueTypeCond.PerformLayout();
            this.grbValueIdSelect.ResumeLayout(false);
            this.grbValueIdSelect.PerformLayout();
            this.grpEvent.ResumeLayout(false);
            this.grpEvent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LblTitulos;
        private System.Windows.Forms.GroupBox grpMater;
        private System.Windows.Forms.ComboBox cmbMaterID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox grpCodeID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtDescriptionId;
        private System.Windows.Forms.TextBox txtCodeId;
        private System.Windows.Forms.GroupBox grpValuelSetings;
        private System.Windows.Forms.DataGridView dtgDetailMasterID;
        private System.Windows.Forms.GroupBox grpValueTypeCond;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDescrption;
        private System.Windows.Forms.ComboBox cboTypeCondition;
        private System.Windows.Forms.GroupBox grbValueIdSelect;
        private System.Windows.Forms.ComboBox cboConditions;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox grpEvent;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnNew;
    }
}