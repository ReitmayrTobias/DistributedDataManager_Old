namespace DDM_Messwagen
{
    partial class MainWindow
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.statorvermessungLMI1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.baumerKameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ddmView1 = new DDM_Messwagen.DDMView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.baumerView2 = new DDM_Messwagen.BaumerView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lmiView1 = new DDM_Messwagen.LMIView();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(896, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statorvermessungLMI1ToolStripMenuItem,
            this.baumerKameraToolStripMenuItem});
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(98, 22);
            this.toolStripDropDownButton1.Text = "Measurements";
            // 
            // statorvermessungLMI1ToolStripMenuItem
            // 
            this.statorvermessungLMI1ToolStripMenuItem.Name = "statorvermessungLMI1ToolStripMenuItem";
            this.statorvermessungLMI1ToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.statorvermessungLMI1ToolStripMenuItem.Text = "Statorvermessung LMI 1";
            this.statorvermessungLMI1ToolStripMenuItem.Click += new System.EventHandler(this.statorvermessungLMIToolStripMenuItem_Click);
            // 
            // baumerKameraToolStripMenuItem
            // 
            this.baumerKameraToolStripMenuItem.Name = "baumerKameraToolStripMenuItem";
            this.baumerKameraToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.baumerKameraToolStripMenuItem.Text = "Baumer Kamera";
            this.baumerKameraToolStripMenuItem.Click += new System.EventHandler(this.baumerKameraToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(896, 455);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ddmView1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(888, 429);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "DDM";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ddmView1
            // 
            this.ddmView1.BackColor = System.Drawing.Color.White;
            this.ddmView1.Location = new System.Drawing.Point(-4, 3);
            this.ddmView1.Name = "ddmView1";
            this.ddmView1.Size = new System.Drawing.Size(900, 430);
            this.ddmView1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.baumerView2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(888, 429);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Baumer";
            // 
            // baumerView2
            // 
            this.baumerView2.Location = new System.Drawing.Point(-4, 0);
            this.baumerView2.Name = "baumerView2";
            this.baumerView2.Size = new System.Drawing.Size(900, 430);
            this.baumerView2.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lmiView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(888, 429);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "LMI Stator";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lmiView1
            // 
            this.lmiView1.Location = new System.Drawing.Point(-4, 3);
            this.lmiView1.Name = "lmiView1";
            this.lmiView1.Size = new System.Drawing.Size(900, 430);
            this.lmiView1.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 516);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainWindow";
            this.Text = "DistributedDataManager";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem statorvermessungLMI1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem baumerKameraToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private LMIView lmiView1;
        private BaumerView baumerView1;
        private DDMView ddmView1;
        private BaumerView baumerView2;
    }
}

