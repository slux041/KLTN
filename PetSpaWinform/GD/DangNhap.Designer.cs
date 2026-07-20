namespace GD
{
    partial class DangNhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DangNhap));
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            txtTenDN = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtMatKhau = new TextBox();
            btnDangNhap = new Button();
            pictureBox5 = new PictureBox();
            pictureBoxEye = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEye).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-3, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(356, 544);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(450, 35);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(200, 170);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // txtTenDN
            // 
            txtTenDN.Location = new Point(516, 236);
            txtTenDN.Name = "txtTenDN";
            txtTenDN.Size = new Size(192, 27);
            txtTenDN.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.ForeColor = SystemColors.HotTrack;
            label1.Location = new Point(373, 239);
            label1.Name = "label1";
            label1.Size = new Size(117, 20);
            label1.TabIndex = 3;
            label1.Text = "Tên Đăng Nhập";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = SystemColors.HotTrack;
            label2.Location = new Point(373, 292);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 5;
            label2.Text = "Mật Khẩu";
            // 
            // txtMatKhau
            // 
            txtMatKhau.Location = new Point(516, 289);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(192, 27);
            txtMatKhau.TabIndex = 1;
            txtMatKhau.UseSystemPasswordChar = true;
            // 
            // btnDangNhap
            // 
            btnDangNhap.BackColor = SystemColors.MenuHighlight;
            btnDangNhap.ForeColor = SystemColors.ButtonHighlight;
            btnDangNhap.Location = new Point(485, 367);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(118, 40);
            btnDangNhap.TabIndex = 2;
            btnDangNhap.Text = "Đăng Nhập";
            btnDangNhap.UseVisualStyleBackColor = false;
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // pictureBox5
            // 
            pictureBox5.Location = new Point(714, 289);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(32, 27);
            pictureBox5.TabIndex = 34;
            pictureBox5.TabStop = false;
            pictureBox5.Click += pictureBox5_Click;
            // 
            // pictureBoxEye
            // 
            pictureBoxEye.Image = (Image)resources.GetObject("pictureBoxEye.Image");
            pictureBoxEye.Location = new Point(714, 289);
            pictureBoxEye.Name = "pictureBoxEye";
            pictureBoxEye.Size = new Size(32, 27);
            pictureBoxEye.TabIndex = 7;
            pictureBoxEye.TabStop = false;
            pictureBoxEye.Click += pictureBoxEye_Click;
            // 
            // DangNhap
            // 
            AcceptButton = btnDangNhap;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(744, 489);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBoxEye);
            Controls.Add(btnDangNhap);
            Controls.Add(label2);
            Controls.Add(txtMatKhau);
            Controls.Add(label1);
            Controls.Add(txtTenDN);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Name = "DangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng Nhập Hệ Thống";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEye).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txtTenDN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBoxEye;
    }
}