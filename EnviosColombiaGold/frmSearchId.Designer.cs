namespace EnviosColombiaGold
{
    partial class frmSearchId
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchId));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBuscar = new System.Windows.Forms.Button();
            this.TxbConsulta = new System.Windows.Forms.TextBox();
            this.dataGridViewConsulta = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConsulta)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonBuscar);
            this.groupBox1.Controls.Add(this.TxbConsulta);
            this.groupBox1.Controls.Add(this.dataGridViewConsulta);
            this.groupBox1.Location = new System.Drawing.Point(10, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(302, 183);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // buttonBuscar
            // 
            this.buttonBuscar.BackColor = System.Drawing.Color.Transparent;
            this.buttonBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonBuscar.Image = ((System.Drawing.Image)(resources.GetObject("buttonBuscar.Image")));
            this.buttonBuscar.Location = new System.Drawing.Point(265, 28);
            this.buttonBuscar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonBuscar.Name = "buttonBuscar";
            this.buttonBuscar.Size = new System.Drawing.Size(30, 21);
            this.buttonBuscar.TabIndex = 33;
            this.buttonBuscar.UseVisualStyleBackColor = false;
            this.buttonBuscar.Click += new System.EventHandler(this.buttonBuscar_Click);
            // 
            // TxbConsulta
            // 
            this.TxbConsulta.Location = new System.Drawing.Point(5, 28);
            this.TxbConsulta.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxbConsulta.Name = "TxbConsulta";
            this.TxbConsulta.Size = new System.Drawing.Size(254, 20);
            this.TxbConsulta.TabIndex = 32;
            this.TxbConsulta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxbConsulta_KeyPress);
            // 
            // dataGridViewConsulta
            // 
            this.dataGridViewConsulta.AllowDrop = true;
            this.dataGridViewConsulta.AllowUserToAddRows = false;
            this.dataGridViewConsulta.AllowUserToDeleteRows = false;
            this.dataGridViewConsulta.AllowUserToOrderColumns = true;
            this.dataGridViewConsulta.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewConsulta.BackgroundColor = System.Drawing.Color.Lavender;
            this.dataGridViewConsulta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewConsulta.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewConsulta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.NullValue = "Vacio";
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewConsulta.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewConsulta.GridColor = System.Drawing.Color.Lavender;
            this.dataGridViewConsulta.Location = new System.Drawing.Point(5, 56);
            this.dataGridViewConsulta.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridViewConsulta.Name = "dataGridViewConsulta";
            this.dataGridViewConsulta.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewConsulta.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewConsulta.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridViewConsulta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewConsulta.Size = new System.Drawing.Size(290, 121);
            this.dataGridViewConsulta.TabIndex = 29;
            this.dataGridViewConsulta.TabStop = false;
            this.dataGridViewConsulta.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewConsulta_CellContentDoubleClick);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmSearchId
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(326, 202);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSearchId";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shipment Search";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLogin_FormClosed);
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConsulta)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridViewConsulta;
        private System.Windows.Forms.Button buttonBuscar;
        private System.Windows.Forms.TextBox TxbConsulta;
        private System.Windows.Forms.Timer timer1;
    }
}