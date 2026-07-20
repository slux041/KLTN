using System;
using System.Drawing;
using System.Windows.Forms;

namespace GD
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            this.Load += Home_Load;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1920, 1080);

            ShowForm(new FrmDashboard());
            label1.Text = "TRANG CHỦ (DASHBOARD)";

            if (Session.Role != null && Session.Role.ToLower() == "staff")
            {
                DisableButton(button2);
                DisableButton(button3);
                DisableButton(button4);
                DisableButton(button6);
                DisableButton(button8);
                DisableButton(button9);
                DisableButton(button10);
                DisableButton(button11);
                DisableButton(button12);
            }
        }

        private void DisableButton(Button btn)
        {
            btn.Enabled = false;
            btn.BackColor = Color.Gray;
            btn.ForeColor = Color.LightGray;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                DangNhap login = new DangNhap();
                login.ShowDialog();
                this.Close();
            }
        }

        private void ShowForm(Form childForm)
        {
            panelContent.Controls.Clear();

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelContent.Controls.Add(childForm);
            panelContent.Tag = childForm;
            childForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowForm(new FrmDashboard());
            label1.Text = "TRANG CHỦ (DASHBOARD)";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowForm(new QLNCC());
            label1.Text = "QUẢN LÍ NHÀ CUNG CẤP";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowForm(new QLSP());
            label1.Text = "QUẢN LÍ KHO";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowForm(new QLKH());
            label1.Text = "QUẢN LÍ KHÁCH HÀNG";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ShowForm(new QLLichHen());
            label1.Text = "QUẢN LÍ LỊCH HẸN";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ShowForm(new QLNV());
            label1.Text = "QUẢN LÍ NHÂN VIÊN";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ShowForm(new QLDH());
            label1.Text = "QUẢN LÍ ĐƠN HÀNG";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ShowForm(new FrmThongKe());
            label1.Text = "BÁO CÁO THỐNG KÊ";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ShowForm(new QLDV());
            label1.Text = "QUẢN LÍ DỊCH VỤ";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ShowForm(new QLKM());
            label1.Text = "QUẢN LÍ KHUYẾN MÃI";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ShowForm(new QLLoaiSP());
            label1.Text = "QUẢN LÍ DANH MỤC";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ShowForm(new QLGiaoDich());
            label1.Text = "QUẢN LÍ GIAO DỊCH";
        }
    }
}