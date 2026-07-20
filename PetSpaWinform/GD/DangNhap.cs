using GD.Services;
using System;
using System.Windows.Forms;

namespace GD
{
    public partial class DangNhap : Form
    {
        private readonly AuthService _authService;

        public DangNhap()
        {
            InitializeComponent();
            _authService = new AuthService();
            txtMatKhau.UseSystemPasswordChar = true;
            pictureBoxEye.Visible = true;
            pictureBox5.Visible = false;
            this.ActiveControl = txtTenDN;
        }

        private async void btnDangNhap_Click(object sender, EventArgs e)
        {
            string userEmail = txtTenDN.Text.Trim();
            string pass = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập email và mật khẩu.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnDangNhap.Enabled = false;
                var loginResponse = await _authService.Login(userEmail, pass);
                string role = loginResponse.Role?.ToLower();

                if (role != "admin" && role != "staff")
                {
                    MessageBox.Show("Tài khoản này không có quyền truy cập hệ thống quản lý!\n(Chỉ dành cho Admin và Nhân viên)",
                        "Truy cập bị từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ApiClient.Instance.Logout();
                    return;
                }

                Session.UserId = loginResponse.UserId;
                Session.FullName = loginResponse.FullName;
                Session.Role = role;
                Session.Token = loginResponse.Token;

                MessageBox.Show($"Đăng nhập thành công!\nXin chào: {loginResponse.FullName}",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Home home = new Home();
                home.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đăng nhập thất bại:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnDangNhap.Enabled = true;
            }
        }

        private void pictureBoxEye_Click(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = false;
            pictureBoxEye.Visible = false;
            pictureBox5.Visible = true;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = true;
            pictureBoxEye.Visible = true;
            pictureBox5.Visible = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }
    }
}