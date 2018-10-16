namespace EnviosColombiaGold
{
    partial class FrmPpal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPpal));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.envioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnucontrolSampling = new System.Windows.Forms.ToolStripMenuItem();
            this.prepararEnvioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jobAssayLaboratoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sGSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aCTLABSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectDBToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graficasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.interceptIntervalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topographyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.movementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seguridadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administradorDeRolesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administradorPermisosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 580);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(799, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.envioToolStripMenuItem,
            this.graficasToolStripMenuItem,
            this.topographyToolStripMenuItem,
            this.seguridadToolStripMenuItem,
            this.helloToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(799, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // envioToolStripMenuItem
            // 
            this.envioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnucontrolSampling,
            this.prepararEnvioToolStripMenuItem,
            this.jobAssayLaboratoryToolStripMenuItem,
            this.selectDBToolStripMenuItem1,
            this.logOutToolStripMenuItem});
            this.envioToolStripMenuItem.Name = "envioToolStripMenuItem";
            this.envioToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.envioToolStripMenuItem.Text = "QAQC";
            // 
            // mnucontrolSampling
            // 
            this.mnucontrolSampling.Name = "mnucontrolSampling";
            this.mnucontrolSampling.Size = new System.Drawing.Size(185, 22);
            this.mnucontrolSampling.Text = "Control Sampling";
            this.mnucontrolSampling.Click += new System.EventHandler(this.mnucontrolSampling_Click);
            // 
            // prepararEnvioToolStripMenuItem
            // 
            this.prepararEnvioToolStripMenuItem.Name = "prepararEnvioToolStripMenuItem";
            this.prepararEnvioToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.prepararEnvioToolStripMenuItem.Text = "Shipment prepare";
            this.prepararEnvioToolStripMenuItem.Click += new System.EventHandler(this.prepararEnvioToolStripMenuItem_Click);
            // 
            // jobAssayLaboratoryToolStripMenuItem
            // 
            this.jobAssayLaboratoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sGSToolStripMenuItem,
            this.aCTLABSToolStripMenuItem});
            this.jobAssayLaboratoryToolStripMenuItem.Name = "jobAssayLaboratoryToolStripMenuItem";
            this.jobAssayLaboratoryToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.jobAssayLaboratoryToolStripMenuItem.Text = "Job Assay Laboratory";
            // 
            // sGSToolStripMenuItem
            // 
            this.sGSToolStripMenuItem.Name = "sGSToolStripMenuItem";
            this.sGSToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.sGSToolStripMenuItem.Text = "SGS";
            this.sGSToolStripMenuItem.Click += new System.EventHandler(this.sGSToolStripMenuItem_Click);
            // 
            // aCTLABSToolStripMenuItem
            // 
            this.aCTLABSToolStripMenuItem.Name = "aCTLABSToolStripMenuItem";
            this.aCTLABSToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.aCTLABSToolStripMenuItem.Text = "GZC";
            this.aCTLABSToolStripMenuItem.Click += new System.EventHandler(this.aCTLABSToolStripMenuItem_Click);
            // 
            // selectDBToolStripMenuItem1
            // 
            this.selectDBToolStripMenuItem1.Name = "selectDBToolStripMenuItem1";
            this.selectDBToolStripMenuItem1.Size = new System.Drawing.Size(185, 22);
            this.selectDBToolStripMenuItem1.Text = "Select DB";
            this.selectDBToolStripMenuItem1.Click += new System.EventHandler(this.selectDBToolStripMenuItem1_Click);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.logOutToolStripMenuItem.Text = "Log out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // graficasToolStripMenuItem
            // 
            this.graficasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.interceptIntervalsToolStripMenuItem});
            this.graficasToolStripMenuItem.Name = "graficasToolStripMenuItem";
            this.graficasToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.graficasToolStripMenuItem.Text = "Intercept";
            // 
            // interceptIntervalsToolStripMenuItem
            // 
            this.interceptIntervalsToolStripMenuItem.Name = "interceptIntervalsToolStripMenuItem";
            this.interceptIntervalsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.interceptIntervalsToolStripMenuItem.Text = "Intercept Intervals";
            this.interceptIntervalsToolStripMenuItem.Click += new System.EventHandler(this.interceptIntervalsToolStripMenuItem_Click);
            // 
            // topographyToolStripMenuItem
            // 
            this.topographyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adminToolStripMenuItem,
            this.movementToolStripMenuItem,
            this.loadCSVToolStripMenuItem});
            this.topographyToolStripMenuItem.Name = "topographyToolStripMenuItem";
            this.topographyToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.topographyToolStripMenuItem.Text = "Topography";
            // 
            // adminToolStripMenuItem
            // 
            this.adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            this.adminToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.adminToolStripMenuItem.Text = "Admin";
            this.adminToolStripMenuItem.Click += new System.EventHandler(this.adminToolStripMenuItem_Click);
            // 
            // movementToolStripMenuItem
            // 
            this.movementToolStripMenuItem.Name = "movementToolStripMenuItem";
            this.movementToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.movementToolStripMenuItem.Text = "Labor";
            this.movementToolStripMenuItem.Click += new System.EventHandler(this.movementToolStripMenuItem_Click);
            // 
            // seguridadToolStripMenuItem
            // 
            this.seguridadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.administradorDeRolesToolStripMenuItem,
            this.administradorPermisosToolStripMenuItem});
            this.seguridadToolStripMenuItem.Name = "seguridadToolStripMenuItem";
            this.seguridadToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.seguridadToolStripMenuItem.Text = "Security";
            // 
            // administradorDeRolesToolStripMenuItem
            // 
            this.administradorDeRolesToolStripMenuItem.Name = "administradorDeRolesToolStripMenuItem";
            this.administradorDeRolesToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.administradorDeRolesToolStripMenuItem.Text = "Administration Users - Roles";
            this.administradorDeRolesToolStripMenuItem.Click += new System.EventHandler(this.administradorDeRolesToolStripMenuItem_Click);
            // 
            // administradorPermisosToolStripMenuItem
            // 
            this.administradorPermisosToolStripMenuItem.Name = "administradorPermisosToolStripMenuItem";
            this.administradorPermisosToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.administradorPermisosToolStripMenuItem.Text = "Administration Permission";
            this.administradorPermisosToolStripMenuItem.Click += new System.EventHandler(this.administradorPermisosToolStripMenuItem_Click);
            // 
            // helloToolStripMenuItem
            // 
            this.helloToolStripMenuItem.Name = "helloToolStripMenuItem";
            this.helloToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.helloToolStripMenuItem.Text = "Hello";
            // 
            // loadCSVToolStripMenuItem
            // 
            this.loadCSVToolStripMenuItem.Name = "loadCSVToolStripMenuItem";
            this.loadCSVToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadCSVToolStripMenuItem.Text = "Load CSV";
            this.loadCSVToolStripMenuItem.Click += new System.EventHandler(this.loadCSVToolStripMenuItem_Click);
            // 
            // FrmPpal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(799, 602);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmPpal";
            this.Text = "QAQC";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmPpal_FormClosed);
            this.Load += new System.EventHandler(this.FrmPpal_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem envioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prepararEnvioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jobAssayLaboratoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnucontrolSampling;
        private System.Windows.Forms.ToolStripMenuItem sGSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aCTLABSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectDBToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graficasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem interceptIntervalsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seguridadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administradorDeRolesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administradorPermisosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topographyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helloToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem movementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadCSVToolStripMenuItem;
    }
}

