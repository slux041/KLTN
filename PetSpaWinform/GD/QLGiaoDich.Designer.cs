namespace GD
{
    partial class QLGiaoDich
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnLoc = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbLoaiGD = new System.Windows.Forms.ComboBox();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvGiaoDich = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.lblSoLuongGD = new System.Windows.Forms.Label();

            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiaoDich)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();

            //
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.Honeydew;
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1159, 100);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Bộ lọc tìm kiếm";
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular);

            this.label1.AutoSize = true; this.label1.Location = new System.Drawing.Point(50, 40); this.label1.Text = "Từ ngày:";
            this.dtpTuNgay.Location = new System.Drawing.Point(130, 35); this.dtpTuNgay.Size = new System.Drawing.Size(150, 30); this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.label2.AutoSize = true; this.label2.Location = new System.Drawing.Point(320, 40); this.label2.Text = "Đến ngày:";
            this.dtpDenNgay.Location = new System.Drawing.Point(410, 35); this.dtpDenNgay.Size = new System.Drawing.Size(150, 30); this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.label3.AutoSize = true; this.label3.Location = new System.Drawing.Point(600, 40); this.label3.Text = "Loại GD:";
            this.cmbLoaiGD.Location = new System.Drawing.Point(680, 35); this.cmbLoaiGD.Size = new System.Drawing.Size(150, 31);

            this.btnLoc.Text = "Lọc dữ liệu";
            this.btnLoc.Location = new System.Drawing.Point(880, 30); this.btnLoc.Size = new System.Drawing.Size(150, 40);
            this.btnLoc.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);

            this.groupBox3.Controls.Add(this.label1); this.groupBox3.Controls.Add(this.dtpTuNgay);
            this.groupBox3.Controls.Add(this.label2); this.groupBox3.Controls.Add(this.dtpDenNgay);
            this.groupBox3.Controls.Add(this.label3); this.groupBox3.Controls.Add(this.cmbLoaiGD);
            this.groupBox3.Controls.Add(this.btnLoc);

            // 
            // dgvGiaoDich
            // 
            this.dgvGiaoDich.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvGiaoDich.Location = new System.Drawing.Point(12, 120);
            this.dgvGiaoDich.Name = "dgvGiaoDich";
            this.dgvGiaoDich.Size = new System.Drawing.Size(1159, 510);
            this.dgvGiaoDich.TabIndex = 2;
            this.dgvGiaoDich.BackgroundColor = System.Drawing.Color.White;
            this.dgvGiaoDich.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGiaoDich.ReadOnly = true;
            this.dgvGiaoDich.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGiaoDich.RowHeadersVisible = false;
            this.dgvGiaoDich.EnableHeadersVisualStyles = false;
            this.dgvGiaoDich.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(30, 144, 255);
            this.dgvGiaoDich.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvGiaoDich.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvGiaoDich.ColumnHeadersHeight = 40;

            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.BackColor = System.Drawing.Color.LightGray;
            this.groupBox5.Location = new System.Drawing.Point(12, 640);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1159, 60);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;

            this.lblSoLuongGD.Text = "Tổng số giao dịch: 0";
            this.lblSoLuongGD.Location = new System.Drawing.Point(20, 20);
            this.lblSoLuongGD.AutoSize = true;
            this.lblSoLuongGD.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);

            this.lblTongTien.Text = "Tổng tiền: 0 VNĐ";
            this.lblTongTien.Location = new System.Drawing.Point(800, 20);
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.ForeColor = System.Drawing.Color.Red;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);

            this.groupBox5.Controls.Add(this.lblSoLuongGD);
            this.groupBox5.Controls.Add(this.lblTongTien);

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 710);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.dgvGiaoDich);
            this.Controls.Add(this.groupBox3);
            this.Name = "QLGiaoDich";
            this.Text = "Quản lý giao dịch";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            this.groupBox3.ResumeLayout(false); this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiaoDich)).EndInit();
            this.groupBox5.ResumeLayout(false); this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Label label3, label2, label1;
        private System.Windows.Forms.ComboBox cmbLoaiGD;
        private System.Windows.Forms.DateTimePicker dtpDenNgay, dtpTuNgay;
        private System.Windows.Forms.DataGridView dgvGiaoDich;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblTongTien, lblSoLuongGD;
    }
}