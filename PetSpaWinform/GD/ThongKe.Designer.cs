namespace GD
{
    partial class FrmThongKe
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabDoanhThu = new System.Windows.Forms.TabPage();
            this.chartRevenue = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panelStats = new System.Windows.Forms.Panel();
            this.lblLoiNhuan = new System.Windows.Forms.Label();
            this.lblChiPhi = new System.Windows.Forms.Label();
            this.lblDoanhThu = new System.Windows.Forms.Label();
            this.btnReload = new System.Windows.Forms.Button();
            this.tabHieuQua = new System.Windows.Forms.TabPage();
            this.dgvTopService = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvTopProduct = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();

            this.tabControl1.SuspendLayout();
            this.tabDoanhThu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenue)).BeginInit();
            this.panelStats.SuspendLayout();
            this.tabHieuQua.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopService)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProduct)).BeginInit();
            this.SuspendLayout();

            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabDoanhThu);
            this.tabControl1.Controls.Add(this.tabHieuQua);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1183, 705);
            this.tabControl1.TabIndex = 0;

            this.tabDoanhThu.Text = "Biểu đồ doanh thu";
            this.tabDoanhThu.BackColor = System.Drawing.Color.White;
            this.tabDoanhThu.Controls.Add(this.chartRevenue);
            this.tabDoanhThu.Controls.Add(this.panelStats);

            this.panelStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStats.Height = 80;
            this.panelStats.BackColor = System.Drawing.Color.Honeydew;
            this.panelStats.Controls.Add(this.btnReload);
            this.panelStats.Controls.Add(this.lblLoiNhuan);
            this.panelStats.Controls.Add(this.lblChiPhi);
            this.panelStats.Controls.Add(this.lblDoanhThu);

            this.lblDoanhThu.Text = "Tổng thu: 0 đ"; this.lblDoanhThu.Location = new System.Drawing.Point(50, 25); this.lblDoanhThu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold); this.lblDoanhThu.ForeColor = System.Drawing.Color.Green; this.lblDoanhThu.AutoSize = true;
            this.lblChiPhi.Text = "Tổng chi: 0 đ"; this.lblChiPhi.Location = new System.Drawing.Point(350, 25); this.lblChiPhi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold); this.lblChiPhi.ForeColor = System.Drawing.Color.Red; this.lblChiPhi.AutoSize = true;
            this.lblLoiNhuan.Text = "Lợi nhuận: 0 đ"; this.lblLoiNhuan.Location = new System.Drawing.Point(650, 25); this.lblLoiNhuan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold); this.lblLoiNhuan.ForeColor = System.Drawing.Color.Blue; this.lblLoiNhuan.AutoSize = true;

            this.btnReload.Text = "Làm mới"; this.btnReload.Location = new System.Drawing.Point(950, 20); this.btnReload.Size = new System.Drawing.Size(100, 40); this.btnReload.BackColor = System.Drawing.Color.LightSkyBlue; this.btnReload.Click += new System.EventHandler(this.btnReload_Click);

            chartArea1.Name = "ChartArea1";
            this.chartRevenue.ChartAreas.Add(chartArea1);
            this.chartRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartRevenue.Legends.Add(legend1);
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "DoanhThu";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chartRevenue.Series.Add(series1);

            this.tabHieuQua.Text = "Top Sản phẩm/Dịch vụ";
            this.tabHieuQua.BackColor = System.Drawing.Color.White;
            this.tabHieuQua.Controls.Add(this.dgvTopService);
            this.tabHieuQua.Controls.Add(this.label2);
            this.tabHieuQua.Controls.Add(this.dgvTopProduct);
            this.tabHieuQua.Controls.Add(this.label1);

            this.label1.Text = "Top 5 Sản phẩm bán chạy"; this.label1.Location = new System.Drawing.Point(20, 20); this.label1.AutoSize = true; this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);

            this.dgvTopProduct.Location = new System.Drawing.Point(20, 50);
            this.dgvTopProduct.Size = new System.Drawing.Size(550, 500);
            this.dgvTopProduct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTopProduct.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(30, 144, 255);
            this.dgvTopProduct.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvTopProduct.EnableHeadersVisualStyles = false;

            this.label2.Text = "Top 5 Dịch vụ được đặt nhiều"; this.label2.Location = new System.Drawing.Point(600, 20); this.label2.AutoSize = true; this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);

            this.dgvTopService.Location = new System.Drawing.Point(600, 50);
            this.dgvTopService.Size = new System.Drawing.Size(550, 500);
            this.dgvTopService.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTopService.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(30, 144, 255);
            this.dgvTopService.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvTopService.EnableHeadersVisualStyles = false;

            this.ClientSize = new System.Drawing.Size(1183, 705);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmThongKe";
            this.Text = "Báo cáo thống kê";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            this.tabControl1.ResumeLayout(false);
            this.tabDoanhThu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenue)).EndInit();
            this.panelStats.ResumeLayout(false); this.panelStats.PerformLayout();
            this.tabHieuQua.ResumeLayout(false); this.tabHieuQua.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopService)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProduct)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabDoanhThu, tabHieuQua;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenue;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label lblLoiNhuan, lblChiPhi, lblDoanhThu;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.DataGridView dgvTopService, dgvTopProduct;
        private System.Windows.Forms.Label label1, label2;
    }
}