namespace DDM_Messwagen
{
    partial class LMIView
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LMIView));
            this.chartPie = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.txb_Pin1 = new System.Windows.Forms.TextBox();
            this.lb_Pin1 = new System.Windows.Forms.Label();
            this.lb_Pin2 = new System.Windows.Forms.Label();
            this.txb_Pin2 = new System.Windows.Forms.TextBox();
            this.lb_Pin3 = new System.Windows.Forms.Label();
            this.txb_Pin3 = new System.Windows.Forms.TextBox();
            this.lb_Pin4 = new System.Windows.Forms.Label();
            this.txb_Pin4 = new System.Windows.Forms.TextBox();
            this.btn_Continue = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartPie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // chartPie
            // 
            chartArea2.Name = "ChartArea1";
            this.chartPie.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartPie.Legends.Add(legend2);
            this.chartPie.Location = new System.Drawing.Point(557, 86);
            this.chartPie.Name = "chartPie";
            this.chartPie.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.LawnGreen,
        System.Drawing.Color.Black};
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            series2.YValuesPerPoint = 2;
            this.chartPie.Series.Add(series2);
            this.chartPie.Size = new System.Drawing.Size(315, 282);
            this.chartPie.TabIndex = 0;
            this.chartPie.Text = "chart1";
            // 
            // txb_Pin1
            // 
            this.txb_Pin1.Location = new System.Drawing.Point(26, 242);
            this.txb_Pin1.Name = "txb_Pin1";
            this.txb_Pin1.Size = new System.Drawing.Size(60, 20);
            this.txb_Pin1.TabIndex = 1;
            // 
            // lb_Pin1
            // 
            this.lb_Pin1.AutoSize = true;
            this.lb_Pin1.Location = new System.Drawing.Point(23, 211);
            this.lb_Pin1.Name = "lb_Pin1";
            this.lb_Pin1.Size = new System.Drawing.Size(34, 13);
            this.lb_Pin1.TabIndex = 2;
            this.lb_Pin1.Text = "Pin 1:";
            // 
            // lb_Pin2
            // 
            this.lb_Pin2.AutoSize = true;
            this.lb_Pin2.Location = new System.Drawing.Point(150, 211);
            this.lb_Pin2.Name = "lb_Pin2";
            this.lb_Pin2.Size = new System.Drawing.Size(34, 13);
            this.lb_Pin2.TabIndex = 4;
            this.lb_Pin2.Text = "Pin 2:";
            // 
            // txb_Pin2
            // 
            this.txb_Pin2.Location = new System.Drawing.Point(153, 242);
            this.txb_Pin2.Name = "txb_Pin2";
            this.txb_Pin2.Size = new System.Drawing.Size(60, 20);
            this.txb_Pin2.TabIndex = 3;
            // 
            // lb_Pin3
            // 
            this.lb_Pin3.AutoSize = true;
            this.lb_Pin3.Location = new System.Drawing.Point(274, 211);
            this.lb_Pin3.Name = "lb_Pin3";
            this.lb_Pin3.Size = new System.Drawing.Size(34, 13);
            this.lb_Pin3.TabIndex = 6;
            this.lb_Pin3.Text = "Pin 3:";
            // 
            // txb_Pin3
            // 
            this.txb_Pin3.Location = new System.Drawing.Point(277, 242);
            this.txb_Pin3.Name = "txb_Pin3";
            this.txb_Pin3.Size = new System.Drawing.Size(60, 20);
            this.txb_Pin3.TabIndex = 5;
            // 
            // lb_Pin4
            // 
            this.lb_Pin4.AutoSize = true;
            this.lb_Pin4.Location = new System.Drawing.Point(393, 211);
            this.lb_Pin4.Name = "lb_Pin4";
            this.lb_Pin4.Size = new System.Drawing.Size(34, 13);
            this.lb_Pin4.TabIndex = 8;
            this.lb_Pin4.Text = "Pin 4:";
            // 
            // txb_Pin4
            // 
            this.txb_Pin4.Location = new System.Drawing.Point(396, 242);
            this.txb_Pin4.Name = "txb_Pin4";
            this.txb_Pin4.Size = new System.Drawing.Size(60, 20);
            this.txb_Pin4.TabIndex = 7;
            // 
            // btn_Continue
            // 
            this.btn_Continue.Location = new System.Drawing.Point(119, 314);
            this.btn_Continue.Name = "btn_Continue";
            this.btn_Continue.Size = new System.Drawing.Size(75, 23);
            this.btn_Continue.TabIndex = 10;
            this.btn_Continue.Text = "Continue";
            this.btn_Continue.UseVisualStyleBackColor = true;
            this.btn_Continue.Click += new System.EventHandler(this.btn_Continue_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(26, 314);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "Pause";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(158, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 24);
            this.label3.TabIndex = 12;
            this.label3.Text = "Stator measurement";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(27, 24);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(90, 90);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // LMIView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btn_Continue);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lb_Pin4);
            this.Controls.Add(this.txb_Pin4);
            this.Controls.Add(this.lb_Pin3);
            this.Controls.Add(this.txb_Pin3);
            this.Controls.Add(this.lb_Pin2);
            this.Controls.Add(this.txb_Pin2);
            this.Controls.Add(this.lb_Pin1);
            this.Controls.Add(this.txb_Pin1);
            this.Controls.Add(this.chartPie);
            this.Name = "LMIView";
            this.Size = new System.Drawing.Size(900, 430);
            ((System.ComponentModel.ISupportInitialize)(this.chartPie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartPie;
        private System.Windows.Forms.TextBox txb_Pin1;
        private System.Windows.Forms.Label lb_Pin1;
        private System.Windows.Forms.Label lb_Pin2;
        private System.Windows.Forms.TextBox txb_Pin2;
        private System.Windows.Forms.Label lb_Pin3;
        private System.Windows.Forms.TextBox txb_Pin3;
        private System.Windows.Forms.Label lb_Pin4;
        private System.Windows.Forms.TextBox txb_Pin4;
        private System.Windows.Forms.Button btn_Continue;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}
