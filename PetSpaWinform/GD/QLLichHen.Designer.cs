namespace GD
{
    partial class QLLichHen
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
            gbThongTin = new GroupBox();
            btnLuu = new Button();
            btnHuyBo = new Button();
            dtpNgayHen = new DateTimePicker();
            cmbKhungGio = new ComboBox();
            cmbLoaiThuCung = new ComboBox();
            cmbDichVu = new ComboBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            txtGhiChu = new TextBox();
            labelGhiChu = new Label();
            txtSdt = new TextBox();
            label3 = new Label();
            txtTenKhach = new TextBox();
            label2 = new Label();
            dgvLichHen = new DataGridView();
            gbTacVu = new GroupBox();
            btnThanhToanPOS = new Button();
            btnXacNhan = new Button();
            btnHuyLich = new Button();
            btnTaoMoi = new Button();
            gbTimKiem = new GroupBox();
            txtTimKiem = new TextBox();
            labelTim = new Label();
            gbThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLichHen).BeginInit();
            gbTacVu.SuspendLayout();
            gbTimKiem.SuspendLayout();
            SuspendLayout();
            // 
            // gbThongTin
            // 
            gbThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbThongTin.BackColor = Color.Honeydew;
            gbThongTin.Controls.Add(btnLuu);
            gbThongTin.Controls.Add(btnHuyBo);
            gbThongTin.Controls.Add(dtpNgayHen);
            gbThongTin.Controls.Add(cmbKhungGio);
            gbThongTin.Controls.Add(cmbLoaiThuCung);
            gbThongTin.Controls.Add(cmbDichVu);
            gbThongTin.Controls.Add(label7);
            gbThongTin.Controls.Add(label6);
            gbThongTin.Controls.Add(label5);
            gbThongTin.Controls.Add(label4);
            gbThongTin.Controls.Add(txtGhiChu);
            gbThongTin.Controls.Add(labelGhiChu);
            gbThongTin.Controls.Add(txtSdt);
            gbThongTin.Controls.Add(label3);
            gbThongTin.Controls.Add(txtTenKhach);
            gbThongTin.Controls.Add(label2);
            gbThongTin.Font = new Font("Segoe UI", 10F);
            gbThongTin.Location = new Point(12, 13);
            gbThongTin.Margin = new Padding(3, 4, 3, 4);
            gbThongTin.Name = "gbThongTin";
            gbThongTin.Padding = new Padding(3, 4, 3, 4);
            gbThongTin.Size = new Size(1176, 200);
            gbThongTin.TabIndex = 1;
            gbThongTin.TabStop = false;
            gbThongTin.Text = "Thông tin đặt lịch";
            // 
            // btnLuu
            // 
            btnLuu.BackColor = Color.ForestGreen;
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLuu.ForeColor = Color.White;
            btnLuu.Location = new Point(1023, 38);
            btnLuu.Margin = new Padding(3, 4, 3, 4);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(120, 51);
            btnLuu.TabIndex = 15;
            btnLuu.Text = "LƯU LỊCH";
            btnLuu.UseVisualStyleBackColor = false;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnHuyBo
            // 
            btnHuyBo.BackColor = Color.Gray;
            btnHuyBo.FlatStyle = FlatStyle.Flat;
            btnHuyBo.ForeColor = Color.White;
            btnHuyBo.Location = new Point(1023, 100);
            btnHuyBo.Margin = new Padding(3, 4, 3, 4);
            btnHuyBo.Name = "btnHuyBo";
            btnHuyBo.Size = new Size(120, 51);
            btnHuyBo.TabIndex = 16;
            btnHuyBo.Text = "Hủy bỏ";
            btnHuyBo.UseVisualStyleBackColor = false;
            btnHuyBo.Click += btnHuyBo_Click;
            // 
            // dtpNgayHen
            // 
            dtpNgayHen.Format = DateTimePickerFormat.Short;
            dtpNgayHen.Location = new Point(820, 38);
            dtpNgayHen.Margin = new Padding(3, 4, 3, 4);
            dtpNgayHen.Name = "dtpNgayHen";
            dtpNgayHen.Size = new Size(150, 30);
            dtpNgayHen.TabIndex = 13;
            // 
            // cmbKhungGio
            // 
            cmbKhungGio.FormattingEnabled = true;
            cmbKhungGio.Location = new Point(820, 94);
            cmbKhungGio.Margin = new Padding(3, 4, 3, 4);
            cmbKhungGio.Name = "cmbKhungGio";
            cmbKhungGio.Size = new Size(150, 31);
            cmbKhungGio.TabIndex = 14;
            // 
            // cmbLoaiThuCung
            // 
            cmbLoaiThuCung.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLoaiThuCung.FormattingEnabled = true;
            cmbLoaiThuCung.Items.AddRange(new object[] { "Chó", "Mèo" });
            cmbLoaiThuCung.Location = new Point(480, 94);
            cmbLoaiThuCung.Margin = new Padding(3, 4, 3, 4);
            cmbLoaiThuCung.Name = "cmbLoaiThuCung";
            cmbLoaiThuCung.Size = new Size(200, 31);
            cmbLoaiThuCung.TabIndex = 11;
            // 
            // cmbDichVu
            // 
            cmbDichVu.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDichVu.FormattingEnabled = true;
            cmbDichVu.Location = new Point(480, 38);
            cmbDichVu.Margin = new Padding(3, 4, 3, 4);
            cmbDichVu.Name = "cmbDichVu";
            cmbDichVu.Size = new Size(200, 31);
            cmbDichVu.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(720, 100);
            label7.Name = "label7";
            label7.Size = new Size(93, 23);
            label7.TabIndex = 17;
            label7.Text = "Khung giờ:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(720, 44);
            label6.Name = "label6";
            label6.Size = new Size(88, 23);
            label6.TabIndex = 18;
            label6.Text = "Ngày hẹn:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(380, 100);
            label5.Name = "label5";
            label5.Size = new Size(86, 23);
            label5.TabIndex = 19;
            label5.Text = "Thú cưng:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(380, 44);
            label4.Name = "label4";
            label4.Size = new Size(71, 23);
            label4.TabIndex = 20;
            label4.Text = "Dịch vụ:";
            // 
            // txtGhiChu
            // 
            txtGhiChu.Location = new Point(120, 144);
            txtGhiChu.Margin = new Padding(3, 4, 3, 4);
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.Size = new Size(200, 30);
            txtGhiChu.TabIndex = 5;
            // 
            // labelGhiChu
            // 
            labelGhiChu.AutoSize = true;
            labelGhiChu.Location = new Point(20, 148);
            labelGhiChu.Name = "labelGhiChu";
            labelGhiChu.Size = new Size(60, 23);
            labelGhiChu.TabIndex = 21;
            labelGhiChu.Text = "Giống:";
            // 
            // txtSdt
            // 
            txtSdt.Location = new Point(120, 94);
            txtSdt.Margin = new Padding(3, 4, 3, 4);
            txtSdt.Name = "txtSdt";
            txtSdt.Size = new Size(200, 30);
            txtSdt.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 98);
            label3.Name = "label3";
            label3.Size = new Size(44, 23);
            label3.TabIndex = 22;
            label3.Text = "SĐT:";
            // 
            // txtTenKhach
            // 
            txtTenKhach.Location = new Point(120, 38);
            txtTenKhach.Margin = new Padding(3, 4, 3, 4);
            txtTenKhach.Name = "txtTenKhach";
            txtTenKhach.Size = new Size(200, 30);
            txtTenKhach.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 41);
            label2.Name = "label2";
            label2.Size = new Size(90, 23);
            label2.TabIndex = 23;
            label2.Text = "Tên khách:";
            // 
            // dgvLichHen
            // 
            dgvLichHen.AllowUserToAddRows = false;
            dgvLichHen.AllowUserToDeleteRows = false;
            dgvLichHen.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvLichHen.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLichHen.Location = new Point(12, 221);
            dgvLichHen.Margin = new Padding(3, 4, 3, 4);
            dgvLichHen.Name = "dgvLichHen";
            dgvLichHen.RowHeadersWidth = 51;
            dgvLichHen.RowTemplate.Height = 24;
            dgvLichHen.Size = new Size(1176, 531);
            dgvLichHen.TabIndex = 2;
            dgvLichHen.CellClick += dgvLichHen_CellClick;
            // 
            // gbTacVu
            // 
            gbTacVu.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            gbTacVu.BackColor = Color.LightGray;
            gbTacVu.Controls.Add(btnThanhToanPOS);
            gbTacVu.Controls.Add(btnXacNhan);
            gbTacVu.Controls.Add(btnHuyLich);
            gbTacVu.Controls.Add(btnTaoMoi);
            gbTacVu.Font = new Font("Segoe UI", 10F);
            gbTacVu.Location = new Point(575, 760);
            gbTacVu.Margin = new Padding(3, 4, 3, 4);
            gbTacVu.Name = "gbTacVu";
            gbTacVu.Padding = new Padding(3, 4, 3, 4);
            gbTacVu.Size = new Size(613, 100);
            gbTacVu.TabIndex = 3;
            gbTacVu.TabStop = false;
            gbTacVu.Text = "Quy trình xử lý / Công cụ";
            // 
            // btnThanhToanPOS
            // 
            btnThanhToanPOS.BackColor = Color.OrangeRed;
            btnThanhToanPOS.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnThanhToanPOS.ForeColor = Color.White;
            btnThanhToanPOS.Location = new Point(398, 31);
            btnThanhToanPOS.Margin = new Padding(3, 4, 3, 4);
            btnThanhToanPOS.Name = "btnThanhToanPOS";
            btnThanhToanPOS.Size = new Size(200, 50);
            btnThanhToanPOS.TabIndex = 4;
            btnThanhToanPOS.Text = "TẠO HÓA ĐƠN >>";
            btnThanhToanPOS.UseVisualStyleBackColor = false;
            btnThanhToanPOS.Click += btnThanhToanPOS_Click;
            // 
            // btnXacNhan
            // 
            btnXacNhan.BackColor = Color.White;
            btnXacNhan.Location = new Point(240, 31);
            btnXacNhan.Margin = new Padding(3, 4, 3, 4);
            btnXacNhan.Name = "btnXacNhan";
            btnXacNhan.Size = new Size(120, 50);
            btnXacNhan.TabIndex = 2;
            btnXacNhan.Text = "Cập nhật TT";
            btnXacNhan.UseVisualStyleBackColor = false;
            btnXacNhan.Click += btnXacNhan_Click;
            // 
            // btnHuyLich
            // 
            btnHuyLich.BackColor = Color.White;
            btnHuyLich.Location = new Point(120, 31);
            btnHuyLich.Margin = new Padding(3, 4, 3, 4);
            btnHuyLich.Name = "btnHuyLich";
            btnHuyLich.Size = new Size(100, 50);
            btnHuyLich.TabIndex = 1;
            btnHuyLich.Text = "Hủy lịch";
            btnHuyLich.UseVisualStyleBackColor = false;
            btnHuyLich.Click += btnHuyLich_Click;
            // 
            // btnTaoMoi
            // 
            btnTaoMoi.BackColor = Color.White;
            btnTaoMoi.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnTaoMoi.Location = new Point(15, 31);
            btnTaoMoi.Margin = new Padding(3, 4, 3, 4);
            btnTaoMoi.Name = "btnTaoMoi";
            btnTaoMoi.Size = new Size(80, 50);
            btnTaoMoi.TabIndex = 0;
            btnTaoMoi.Text = "+ Tạo";
            btnTaoMoi.UseVisualStyleBackColor = false;
            btnTaoMoi.Click += btnTaoMoi_Click;
            // 
            // gbTimKiem
            // 
            gbTimKiem.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            gbTimKiem.BackColor = Color.BurlyWood;
            gbTimKiem.Controls.Add(txtTimKiem);
            gbTimKiem.Controls.Add(labelTim);
            gbTimKiem.Font = new Font("Segoe UI", 10F);
            gbTimKiem.Location = new Point(12, 760);
            gbTimKiem.Margin = new Padding(3, 4, 3, 4);
            gbTimKiem.Name = "gbTimKiem";
            gbTimKiem.Padding = new Padding(3, 4, 3, 4);
            gbTimKiem.Size = new Size(350, 100);
            gbTimKiem.TabIndex = 4;
            gbTimKiem.TabStop = false;
            gbTimKiem.Text = "Tìm kiếm";
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(100, 38);
            txtTimKiem.Margin = new Padding(3, 4, 3, 4);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(230, 30);
            txtTimKiem.TabIndex = 1;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            // 
            // labelTim
            // 
            labelTim.AutoSize = true;
            labelTim.Location = new Point(15, 41);
            labelTim.Name = "labelTim";
            labelTim.Size = new Size(77, 23);
            labelTim.TabIndex = 0;
            labelTim.Text = "Tên/SĐT:";
            // 
            // QLLichHen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 875);
            Controls.Add(gbTimKiem);
            Controls.Add(gbTacVu);
            Controls.Add(dgvLichHen);
            Controls.Add(gbThongTin);
            Margin = new Padding(3, 4, 3, 4);
            Name = "QLLichHen";
            Text = "Quản lý lịch hẹn";
            gbThongTin.ResumeLayout(false);
            gbThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLichHen).EndInit();
            gbTacVu.ResumeLayout(false);
            gbTimKiem.ResumeLayout(false);
            gbTimKiem.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbThongTin;
        private System.Windows.Forms.TextBox txtTenKhach;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSdt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label labelGhiChu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbDichVu;
        private System.Windows.Forms.ComboBox cmbLoaiThuCung;
        private System.Windows.Forms.ComboBox cmbKhungGio;
        private System.Windows.Forms.DateTimePicker dtpNgayHen;
        private System.Windows.Forms.Button btnHuyBo;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.DataGridView dgvLichHen;
        private System.Windows.Forms.GroupBox gbTacVu;
        private System.Windows.Forms.Button btnTaoMoi;
        private System.Windows.Forms.Button btnHuyLich;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnThanhToanPOS;
        private System.Windows.Forms.GroupBox gbTimKiem;
        private System.Windows.Forms.Label labelTim;
        private System.Windows.Forms.TextBox txtTimKiem;
    }
}