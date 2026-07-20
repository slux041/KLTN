using GD.DTOs;
using GD.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace GD
{
    public partial class QLNV : Form
    {
        private readonly UserService _userService;
        private List<UserDto> _staffs;
        private bool _isEditing = false;

        public QLNV()
        {
            InitializeComponent();
            _userService = new UserService();

            SetupDataGridView();
            LoadComboBoxes();
            LoadNhanVien();
            SetEnableControls(false);

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnResetPass.Enabled = false;
            btnThem.Enabled = false;
        }

        private void SetupDataGridView()
        {
            var dgv = dataGridView1;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.RowHeadersVisible = false;
            dgv.BackgroundColor = Color.White;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 144, 255);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;
        }

        private void SetEnableControls(bool enable)
        {
            txtMaNV.Enabled = false;
            txtHoTen.Enabled = enable;
            txtSDT.Enabled = enable;
            txtEmail.Enabled = enable;
            dtpNgaysinh.Enabled = enable;
            cmbGioiTinh.Enabled = enable;
            cmbChucVu.Enabled = enable;
            cmbTrangThai.Enabled = enable;
        }

        private void LoadComboBoxes()
        {
            if (cmbGioiTinh.Items.Count == 0)
            {
                cmbGioiTinh.Items.Add("Nam");
                cmbGioiTinh.Items.Add("Nữ");
                cmbGioiTinh.Items.Add("Khác");
            }
            if (cmbChucVu.Items.Count == 0)
            {
                cmbChucVu.Items.Add("admin");
                cmbChucVu.Items.Add("staff");
                cmbChucVu.Items.Add("customer");
            }
            if (cmbTrangThai.Items.Count == 0)
            {
                cmbTrangThai.Items.Add("active");
                cmbTrangThai.Items.Add("inactive");
            }
        }

        private async void LoadNhanVien(string keyword = "")
        {
            try
            {
                _staffs = await _userService.GetEmployees(keyword);
                BindGrid(_staffs);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void BindGrid(List<UserDto> staffs)
        {
            if (staffs == null) return;

            var displayList = staffs.Select(s => new
            {
                MaNV = s.UserId,
                HoTen = s.FullName,
                NgaySinh = s.DateOfBirth.HasValue ? s.DateOfBirth.Value.ToString("dd/MM/yyyy") : "",
                ChucVu = s.Role,
                SoDienThoai = s.Phone,
                Email = s.Email,
                GioiTinh = s.Gender == "male" ? "Nam" : (s.Gender == "female" ? "Nữ" : "Khác"),
                TrangThai = s.Status
            }).ToList();

            dataGridView1.DataSource = displayList;

            dataGridView1.Columns["MaNV"].HeaderText = "Mã"; dataGridView1.Columns["MaNV"].Width = 50;
            dataGridView1.Columns["HoTen"].HeaderText = "Họ tên";
            dataGridView1.Columns["NgaySinh"].HeaderText = "Ngày sinh";
            dataGridView1.Columns["ChucVu"].HeaderText = "Chức vụ";
            dataGridView1.Columns["SoDienThoai"].HeaderText = "SĐT";
            dataGridView1.Columns["Email"].HeaderText = "Email";
            dataGridView1.Columns["GioiTinh"].HeaderText = "Giới tính";
            dataGridView1.Columns["TrangThai"].HeaderText = "Trạng thái";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dataGridView1.CurrentRow == null) return;
            var row = dataGridView1.CurrentRow;

            txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
            txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
            txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();
            cmbChucVu.Text = row.Cells["ChucVu"].Value.ToString();
            cmbTrangThai.Text = row.Cells["TrangThai"].Value.ToString();

            string gt = row.Cells["GioiTinh"].Value.ToString();
            cmbGioiTinh.SelectedItem = gt;

            if (DateTime.TryParseExact(row.Cells["NgaySinh"].Value.ToString(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dt))
                dtpNgaysinh.Value = dt;

            _isEditing = false;
            SetEnableControls(false);
            btnSua.Text = "Sửa";

            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnResetPass.Enabled = true;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Vui lòng nhập Email hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            if (cmbChucVu.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn chức vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbChucVu.Focus();
                return false;
            }

            // Ràng buộc tuổi từ 18 đến 60
            int age = DateTime.Now.Year - dtpNgaysinh.Value.Year;
            if (dtpNgaysinh.Value > DateTime.Now.AddYears(-age)) age--;

            if (age < 18 || age > 60)
            {
                MessageBox.Show($"Tuổi nhân viên phải từ 18 đến 60 tuổi!\n(Tuổi hiện tại: {age})", "Ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaysinh.Focus();
                return false;
            }

            return true;
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            ClearInput();
            SetEnableControls(true);
            txtHoTen.Focus();

            _isEditing = false;
            btnSua.Text = "Sửa";

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnResetPass.Enabled = false;
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                string defaultPass = "123456";

                var user = new CreateUserDto
                {
                    FullName = txtHoTen.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtSDT.Text.Trim(),
                    Password = defaultPass,
                    Role = cmbChucVu.Text,
                    DateOfBirth = dtpNgaysinh.Value,
                    Gender = cmbGioiTinh.Text == "Nam" ? "male" : (cmbGioiTinh.Text == "Nữ" ? "female" : "other")
                };

                await _userService.CreateUser(user);
                MessageBox.Show($"Thêm nhân viên thành công!\nMật khẩu mặc định: {defaultPass}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadNhanVien();
                ClearInput();
                SetEnableControls(false);
                btnThem.Enabled = false;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!");
                return;
            }

            if (!_isEditing)
            {
                SetEnableControls(true);
                txtEmail.Enabled = false;
                txtHoTen.Focus();

                _isEditing = true;
                btnSua.Text = "Lưu";

                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                btnResetPass.Enabled = false;
            }
            else
            {
                if (!ValidateInputs()) return;

                try
                {
                    int id = int.Parse(txtMaNV.Text);
                    var user = new UpdateUserDto
                    {
                        FullName = txtHoTen.Text.Trim(),
                        Phone = txtSDT.Text.Trim(),
                        DateOfBirth = dtpNgaysinh.Value,
                        Gender = cmbGioiTinh.Text == "Nam" ? "male" : (cmbGioiTinh.Text == "Nữ" ? "female" : "other")
                    };
                    await _userService.UpdateUser(id, user);

                    await _userService.ChangeStatus(id, cmbTrangThai.Text);

                    MessageBox.Show("Cập nhật thành công!");
                    LoadNhanVien();

                    _isEditing = false;
                    btnSua.Text = "Sửa";
                    SetEnableControls(false);
                    btnThem.Enabled = false;
                    btnXoa.Enabled = true;
                    btnResetPass.Enabled = true;
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    int id = int.Parse(txtMaNV.Text);
                    await _userService.DeleteUser(id);
                    MessageBox.Show("Đã xóa!");
                    LoadNhanVien();
                    ClearInput();

                    _isEditing = false;
                    btnSua.Text = "Sửa";
                    SetEnableControls(false);
                    btnThem.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnResetPass.Enabled = false;
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private async void btnResetPass_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNV.Text)) return;
            string newPass = Interaction.InputBox("Nhập mật khẩu mới cho nhân viên:", "Đặt lại mật khẩu", "");

            if (!string.IsNullOrEmpty(newPass))
            {
                if (newPass.Length < 6)
                {
                    MessageBox.Show("Mật khẩu phải từ 6 ký tự trở lên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    int id = int.Parse(txtMaNV.Text);
                    await _userService.ResetPassword(id, newPass);
                    MessageBox.Show("Đổi mật khẩu thành công!");
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string k = txtTimKiem.Text.ToLower().Trim();
            if (_staffs == null) return;
            var f = _staffs.Where(s => s.FullName.ToLower().Contains(k) || s.Email.ToLower().Contains(k)).ToList();
            BindGrid(f);
        }

        private void ClearInput()
        {
            txtMaNV.Clear();
            txtHoTen.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            dtpNgaysinh.Value = DateTime.Now.AddYears(-18);

            if (cmbGioiTinh.Items.Count > 0) cmbGioiTinh.SelectedIndex = 0;
            if (cmbChucVu.Items.Count > 0) cmbChucVu.SelectedIndex = 1;
            if (cmbTrangThai.Items.Count > 0) cmbTrangThai.SelectedIndex = 0;
        }
    }
}