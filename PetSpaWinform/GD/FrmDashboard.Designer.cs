namespace GD
{
    partial class FrmDashboard
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelTop = new System.Windows.Forms.TableLayoutPanel();
            this.panelCard1 = new System.Windows.Forms.Panel();
            this.lblValue1 = new System.Windows.Forms.Label();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.panelCard2 = new System.Windows.Forms.Panel();
            this.lblValue2 = new System.Windows.Forms.Label();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.panelCard3 = new System.Windows.Forms.Panel();
            this.lblValue3 = new System.Windows.Forms.Label();
            this.lblTitle3 = new System.Windows.Forms.Label();
            this.panelCard4 = new System.Windows.Forms.Panel();
            this.lblValue4 = new System.Windows.Forms.Label();
            this.lblTitle4 = new System.Windows.Forms.Label();
            this.tableLayoutPanelMiddle = new System.Windows.Forms.TableLayoutPanel();
            this.panelChart = new System.Windows.Forms.Panel();
            this.chartRevenue = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBoxAppointments = new System.Windows.Forms.GroupBox();
            this.dgvAppointments = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanelBottom = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnNewAppointment = new System.Windows.Forms.Button();
            this.btnNewOrder = new System.Windows.Forms.Button();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelTop.SuspendLayout();
            this.panelCard1.SuspendLayout();
            this.panelCard2.SuspendLayout();
            this.panelCard3.SuspendLayout();
            this.panelCard4.SuspendLayout();
            this.tableLayoutPanelMiddle.SuspendLayout();
            this.panelChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenue)).BeginInit();
            this.groupBoxAppointments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).BeginInit();
            this.flowLayoutPanelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelTop, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelMiddle, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.flowLayoutPanelBottom, 0, 2);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1200, 800);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // tableLayoutPanelTop
            // 
            this.tableLayoutPanelTop.ColumnCount = 4;
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelTop.Controls.Add(this.panelCard1, 0, 0);
            this.tableLayoutPanelTop.Controls.Add(this.panelCard2, 1, 0);
            this.tableLayoutPanelTop.Controls.Add(this.panelCard3, 2, 0);
            this.tableLayoutPanelTop.Controls.Add(this.panelCard4, 3, 0);
            this.tableLayoutPanelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelTop.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelTop.Name = "tableLayoutPanelTop";
            this.tableLayoutPanelTop.RowCount = 1;
            this.tableLayoutPanelTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTop.Size = new System.Drawing.Size(1194, 114);
            this.tableLayoutPanelTop.TabIndex = 0;
            // 
            // panelCard1
            // 
            this.panelCard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(178)))));
            this.panelCard1.Controls.Add(this.lblValue1);
            this.panelCard1.Controls.Add(this.lblTitle1);
            this.panelCard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCard1.Location = new System.Drawing.Point(10, 10);
            this.panelCard1.Margin = new System.Windows.Forms.Padding(10);
            this.panelCard1.Name = "panelCard1";
            this.panelCard1.Size = new System.Drawing.Size(278, 94);
            this.panelCard1.TabIndex = 0;
            // 
            // lblValue1
            // 
            this.lblValue1.AutoSize = true;
            this.lblValue1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblValue1.Location = new System.Drawing.Point(15, 45);
            this.lblValue1.Name = "lblValue1";
            this.lblValue1.Size = new System.Drawing.Size(161, 38);
            this.lblValue1.TabIndex = 1;
            this.lblValue1.Text = "5.000.000đ";
            // 
            // lblTitle1
            // 
            this.lblTitle1.AutoSize = true;
            this.lblTitle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitle1.Location = new System.Drawing.Point(15, 15);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(158, 23);
            this.lblTitle1.TabIndex = 0;
            this.lblTitle1.Text = "Doanh thu hôm nay";
            // 
            // panelCard2
            // 
            this.panelCard2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(245)))), ((int)(((byte)(254)))));
            this.panelCard2.Controls.Add(this.lblValue2);
            this.panelCard2.Controls.Add(this.lblTitle2);
            this.panelCard2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCard2.Location = new System.Drawing.Point(308, 10);
            this.panelCard2.Margin = new System.Windows.Forms.Padding(10);
            this.panelCard2.Name = "panelCard2";
            this.panelCard2.Size = new System.Drawing.Size(278, 94);
            this.panelCard2.TabIndex = 1;
            // 
            // lblValue2
            // 
            this.lblValue2.AutoSize = true;
            this.lblValue2.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblValue2.Location = new System.Drawing.Point(15, 45);
            this.lblValue2.Name = "lblValue2";
            this.lblValue2.Size = new System.Drawing.Size(49, 38);
            this.lblValue2.TabIndex = 1;
            this.lblValue2.Text = "12";
            // 
            // lblTitle2
            // 
            this.lblTitle2.AutoSize = true;
            this.lblTitle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitle2.Location = new System.Drawing.Point(15, 15);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(116, 23);
            this.lblTitle2.TabIndex = 0;
            this.lblTitle2.Text = "Đơn hàng mới";
            // 
            // panelCard3
            // 
            this.panelCard3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(233)))));
            this.panelCard3.Controls.Add(this.lblValue3);
            this.panelCard3.Controls.Add(this.lblTitle3);
            this.panelCard3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCard3.Location = new System.Drawing.Point(606, 10);
            this.panelCard3.Margin = new System.Windows.Forms.Padding(10);
            this.panelCard3.Name = "panelCard3";
            this.panelCard3.Size = new System.Drawing.Size(278, 94);
            this.panelCard3.TabIndex = 2;
            // 
            // lblValue3
            // 
            this.lblValue3.AutoSize = true;
            this.lblValue3.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblValue3.Location = new System.Drawing.Point(15, 45);
            this.lblValue3.Name = "lblValue3";
            this.lblValue3.Size = new System.Drawing.Size(33, 38);
            this.lblValue3.TabIndex = 1;
            this.lblValue3.Text = "8";
            // 
            // lblTitle3
            // 
            this.lblTitle3.AutoSize = true;
            this.lblTitle3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitle3.Location = new System.Drawing.Point(15, 15);
            this.lblTitle3.Name = "lblTitle3";
            this.lblTitle3.Size = new System.Drawing.Size(142, 23);
            this.lblTitle3.TabIndex = 0;
            this.lblTitle3.Text = "Lịch hẹn hôm nay";
            // 
            // panelCard4
            // 
            this.panelCard4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.panelCard4.Controls.Add(this.lblValue4);
            this.panelCard4.Controls.Add(this.lblTitle4);
            this.panelCard4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCard4.Location = new System.Drawing.Point(904, 10);
            this.panelCard4.Margin = new System.Windows.Forms.Padding(10);
            this.panelCard4.Name = "panelCard4";
            this.panelCard4.Size = new System.Drawing.Size(280, 94);
            this.panelCard4.TabIndex = 3;
            // 
            // lblValue4
            // 
            this.lblValue4.AutoSize = true;
            this.lblValue4.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblValue4.Location = new System.Drawing.Point(15, 45);
            this.lblValue4.Name = "lblValue4";
            this.lblValue4.Size = new System.Drawing.Size(33, 38);
            this.lblValue4.TabIndex = 1;
            this.lblValue4.Text = "3";
            // 
            // lblTitle4
            // 
            this.lblTitle4.AutoSize = true;
            this.lblTitle4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitle4.Location = new System.Drawing.Point(15, 15);
            this.lblTitle4.Name = "lblTitle4";
            this.lblTitle4.Size = new System.Drawing.Size(117, 23);
            this.lblTitle4.TabIndex = 0;
            this.lblTitle4.Text = "Sắp hết hàng";
            // 
            // tableLayoutPanelMiddle
            // 
            this.tableLayoutPanelMiddle.ColumnCount = 2;
            this.tableLayoutPanelMiddle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanelMiddle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanelMiddle.Controls.Add(this.panelChart, 0, 0);
            this.tableLayoutPanelMiddle.Controls.Add(this.groupBoxAppointments, 1, 0);
            this.tableLayoutPanelMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMiddle.Location = new System.Drawing.Point(3, 123);
            this.tableLayoutPanelMiddle.Name = "tableLayoutPanelMiddle";
            this.tableLayoutPanelMiddle.RowCount = 1;
            this.tableLayoutPanelMiddle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMiddle.Size = new System.Drawing.Size(1194, 594);
            this.tableLayoutPanelMiddle.TabIndex = 1;
            // 
            // panelChart
            // 
            this.panelChart.Controls.Add(this.chartRevenue);
            this.panelChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChart.Location = new System.Drawing.Point(3, 3);
            this.panelChart.Name = "panelChart";
            this.panelChart.Padding = new System.Windows.Forms.Padding(10);
            this.panelChart.Size = new System.Drawing.Size(710, 588);
            this.panelChart.TabIndex = 0;
            // 
            // chartRevenue
            // 
            chartArea1.Name = "MainArea";
            this.chartRevenue.ChartAreas.Add(chartArea1);
            this.chartRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartRevenue.Legends.Add(legend1);
            this.chartRevenue.Location = new System.Drawing.Point(10, 10);
            this.chartRevenue.Name = "chartRevenue";
            series1.ChartArea = "MainArea";
            series1.Legend = "Legend1";
            series1.Name = "Revenue";
            this.chartRevenue.Series.Add(series1);
            this.chartRevenue.Size = new System.Drawing.Size(690, 568);
            this.chartRevenue.TabIndex = 0;
            this.chartRevenue.Text = "chart1";
            title1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            title1.Name = "Title1";
            title1.Text = "Biểu đồ doanh thu 7 ngày";
            this.chartRevenue.Titles.Add(title1);
            // 
            // groupBoxAppointments
            // 
            this.groupBoxAppointments.Controls.Add(this.dgvAppointments);
            this.groupBoxAppointments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxAppointments.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxAppointments.Location = new System.Drawing.Point(719, 3);
            this.groupBoxAppointments.Name = "groupBoxAppointments";
            this.groupBoxAppointments.Size = new System.Drawing.Size(472, 588);
            this.groupBoxAppointments.TabIndex = 1;
            this.groupBoxAppointments.TabStop = false;
            this.groupBoxAppointments.Text = "Lịch hẹn hôm nay";
            // 
            // dgvAppointments
            // 
            this.dgvAppointments.AllowUserToAddRows = false;
            this.dgvAppointments.AllowUserToDeleteRows = false;
            this.dgvAppointments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAppointments.BackgroundColor = System.Drawing.Color.White;
            this.dgvAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppointments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAppointments.Location = new System.Drawing.Point(3, 26);
            this.dgvAppointments.Name = "dgvAppointments";
            this.dgvAppointments.ReadOnly = true;
            this.dgvAppointments.RowHeadersVisible = false;
            this.dgvAppointments.RowHeadersWidth = 51;
            this.dgvAppointments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAppointments.Size = new System.Drawing.Size(466, 559);
            this.dgvAppointments.TabIndex = 0;
            // 
            // flowLayoutPanelBottom
            // 
            this.flowLayoutPanelBottom.Controls.Add(this.btnRefresh);
            this.flowLayoutPanelBottom.Controls.Add(this.btnNewAppointment);
            this.flowLayoutPanelBottom.Controls.Add(this.btnNewOrder);
            this.flowLayoutPanelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelBottom.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelBottom.Location = new System.Drawing.Point(3, 723);
            this.flowLayoutPanelBottom.Name = "flowLayoutPanelBottom";
            this.flowLayoutPanelBottom.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanelBottom.Size = new System.Drawing.Size(1194, 74);
            this.flowLayoutPanelBottom.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRefresh.Location = new System.Drawing.Point(1051, 13);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 40);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Làm mới (F5)";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnNewAppointment
            // 
            this.btnNewAppointment.BackColor = System.Drawing.Color.White;
            this.btnNewAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewAppointment.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnNewAppointment.Location = new System.Drawing.Point(925, 13);
            this.btnNewAppointment.Name = "btnNewAppointment";
            this.btnNewAppointment.Size = new System.Drawing.Size(120, 40);
            this.btnNewAppointment.TabIndex = 1;
            this.btnNewAppointment.Text = "Đặt lịch mới (F2)";
            this.btnNewAppointment.UseVisualStyleBackColor = false;
            // 
            // btnNewOrder
            // 
            this.btnNewOrder.BackColor = System.Drawing.Color.Orange;
            this.btnNewOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewOrder.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNewOrder.ForeColor = System.Drawing.Color.White;
            this.btnNewOrder.Location = new System.Drawing.Point(799, 13);
            this.btnNewOrder.Name = "btnNewOrder";
            this.btnNewOrder.Size = new System.Drawing.Size(120, 40);
            this.btnNewOrder.TabIndex = 0;
            this.btnNewOrder.Text = "Tạo đơn hàng (F1)";
            this.btnNewOrder.UseVisualStyleBackColor = false;
            // 
            // FrmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmDashboard";
            this.Text = "FrmDashboard";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelTop.ResumeLayout(false);
            this.panelCard1.ResumeLayout(false);
            this.panelCard1.PerformLayout();
            this.panelCard2.ResumeLayout(false);
            this.panelCard2.PerformLayout();
            this.panelCard3.ResumeLayout(false);
            this.panelCard3.PerformLayout();
            this.panelCard4.ResumeLayout(false);
            this.panelCard4.PerformLayout();
            this.tableLayoutPanelMiddle.ResumeLayout(false);
            this.panelChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenue)).EndInit();
            this.groupBoxAppointments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).EndInit();
            this.flowLayoutPanelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTop;
        private System.Windows.Forms.Panel panelCard1;
        private System.Windows.Forms.Panel panelCard2;
        private System.Windows.Forms.Panel panelCard3;
        private System.Windows.Forms.Panel panelCard4;
        private System.Windows.Forms.Label lblValue1;
        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.Label lblValue2;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Label lblValue3;
        private System.Windows.Forms.Label lblTitle3;
        private System.Windows.Forms.Label lblValue4;
        private System.Windows.Forms.Label lblTitle4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMiddle;
        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenue;
        private System.Windows.Forms.GroupBox groupBoxAppointments;
        private System.Windows.Forms.DataGridView dgvAppointments;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBottom;
        private System.Windows.Forms.Button btnNewOrder;
        private System.Windows.Forms.Button btnNewAppointment;
        private System.Windows.Forms.Button btnRefresh;
    }
}
