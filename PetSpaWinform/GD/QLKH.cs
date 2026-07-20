using GD.DTOs;
using GD.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GD
{
    public partial class QLKH : Form
    {
        private readonly CustomerService _customerService;
        private List<CustomerDto> _customers;
        private bool _isEditing = false;
        private string _selectedImagePath = null;

        private const int SPECIAL_CUSTOMER_ID = 7;

        public QLKH()
        {
            InitializeComponent();
            _customerService = new CustomerService();

            SetupDataGridView();
            LoadComboBoxes();
            LoadKhachHangData();
            SetEnableControls(false);

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = false;
        }

        private void SetupDataGridView()
        {
            dgvKhachHang.BorderStyle = BorderStyle.None;
            dgvKhachHang.BackgroundColor = Color.White;
            dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKhachHang.EnableHeadersVisualStyles = false;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 144, 255);
            dgvKhachHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvKhachHang.ColumnHeadersHeight = 40;
            dgvKhachHang.DefaultCellStyle.Font = new Font("Segoe UI", 11);
            dgvKhachHang.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 230, 150);
            dgvKhachHang.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvKhachHang.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvKhachHang.ReadOnly = true;
            dgvKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhachHang.RowHeadersVisible = false;
        }

        private void SetEnableControls(bool enable)
        {
            txtMaKH.Enabled = false;
            txtHoTen.Enabled = enable;
            txtEmail.Enabled = enable;
            txtSDT.Enabled = enable;
            txtDiaChi.Enabled = enable;
            dtpNgayDK.Enabled = false;
            cmbGioiTinh.Enabled = enable;
            cmbTrangThai.Enabled = enable;
            btnChonAnh.Enabled = enable;
        }

        private void LoadComboBoxes()
        {
            if (cmbGioiTinh.Items.Count == 0)
            {
                cmbGioiTinh.Items.Add("Nam");
                cmbGioiTinh.Items.Add("Nữ");
                cmbGioiTinh.Items.Add("Khác");
            }
            if (cmbTrangThai.Items.Count == 0)
            {
                cmbTrangThai.Items.Add("active");
                cmbTrangThai.Items.Add("inactive");
            }
        }

        private async void LoadKhachHangData(string search = "")
        {
            try
            {
                var paginationData = await _customerService.GetCustomers(search, pageSize: 100);
                if (paginationData != null && paginationData.Items != null)
                {
                    _customers = paginationData.Items;
                    BindGrid(_customers);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void BindGrid(List<CustomerDto> customers)
        {
            var displayList = customers.Select(c => new
            {
                MaKH = c.CustomerId,
                HoTen = c.FullName,
                Email = c.Email,
                SoDienThoai = c.Phone,
                DiaChi = c.Address,
                GioiTinh = c.Gender == "male" ? "Nam" : (c.Gender == "female" ? "Nữ" : "Khác"),
                TrangThai = c.Status,
                NgayDangKy = c.CreatedAt,
                ImageUrl = c.ImageUrl
            }).ToList();

            dgvKhachHang.DataSource = displayList;

            if (dgvKhachHang.Columns["MaKH"] != null) dgvKhachHang.Columns["MaKH"].HeaderText = "Mã KH";
            if (dgvKhachHang.Columns["HoTen"] != null) dgvKhachHang.Columns["HoTen"].HeaderText = "Họ tên";
            if (dgvKhachHang.Columns["Email"] != null) dgvKhachHang.Columns["Email"].HeaderText = "Email";
            if (dgvKhachHang.Columns["SoDienThoai"] != null) dgvKhachHang.Columns["SoDienThoai"].HeaderText = "SĐT";
            if (dgvKhachHang.Columns["DiaChi"] != null) dgvKhachHang.Columns["DiaChi"].HeaderText = "Địa chỉ";
            if (dgvKhachHang.Columns["GioiTinh"] != null) dgvKhachHang.Columns["GioiTinh"].HeaderText = "Giới tính";
            if (dgvKhachHang.Columns["TrangThai"] != null) dgvKhachHang.Columns["TrangThai"].HeaderText = "Trạng thái";

            if (dgvKhachHang.Columns["NgayDangKy"] != null)
            {
                dgvKhachHang.Columns["NgayDangKy"].HeaderText = "Ngày đăng ký";
                dgvKhachHang.Columns["NgayDangKy"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvKhachHang.Columns["ImageUrl"] != null) dgvKhachHang.Columns["ImageUrl"].Visible = false;
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvKhachHang.CurrentRow != null)
            {
                var row = dgvKhachHang.CurrentRow;

                int customerId = 0;
                if (row.Cells["MaKH"].Value != null)
                    int.TryParse(row.Cells["MaKH"].Value.ToString(), out customerId);

                txtMaKH.Text = customerId.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                txtSDT.Text = row.Cells["SoDienThoai"].Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? "";

                if (DateTime.TryParse(row.Cells["NgayDangKy"].Value?.ToString(), out DateTime dt))
                    dtpNgayDK.Value = dt;

                string gt = row.Cells["GioiTinh"].Value?.ToString();
                cmbGioiTinh.Text = gt;
                cmbTrangThai.Text = row.Cells["TrangThai"].Value?.ToString();

                string imgUrl = row.Cells["ImageUrl"].Value?.ToString();
                if (!string.IsNullOrEmpty(imgUrl))
                {
                    try { ptbAvatar.LoadAsync(imgUrl); } catch { ptbAvatar.Image = null; }
                }
                else ptbAvatar.Image = null;

                _isEditing = false;
                SetEnableControls(false);
                btnSua.Text = "Sửa";

                btnThem.Enabled = false;

                if (customerId == SPECIAL_CUSTOMER_ID)
                {
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                }
                else
                {
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                }
            }
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
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text)) { MessageBox.Show("Nhập họ tên!"); txtHoTen.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtEmail.Text)) { MessageBox.Show("Nhập Email!"); txtEmail.Focus(); return false; }
            return true;
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                string gender = cmbGioiTinh.Text == "Nam" ? "male" : (cmbGioiTinh.Text == "Nữ" ? "female" : "other");

                var dto = new CreateCustomerDto
                {
                    FullName = txtHoTen.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtSDT.Text.Trim(),
                    Address = txtDiaChi.Text.Trim(),
                    Password = "123456",
                    Gender = gender,
                    DateOfBirth = DateTime.Now
                };

                await _customerService.CreateCustomer(dto, _selectedImagePath);
                MessageBox.Show("Thêm thành công! Mật khẩu mặc định: 123");

                LoadKhachHangData();
                ClearInput();
                SetEnableControls(false);
                btnThem.Enabled = false;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text)) return;

            if (txtMaKH.Text == SPECIAL_CUSTOMER_ID.ToString())
            {
                MessageBox.Show("Không thể sửa thông tin 'Khách lẻ' mặc định của hệ thống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
            }
            else
            {
                if (!ValidateInput()) return;

                try
                {
                    string gender = cmbGioiTinh.Text == "Nam" ? "male" : (cmbGioiTinh.Text == "Nữ" ? "female" : "other");
                    string status = cmbTrangThai.Text;

                    int id = int.Parse(txtMaKH.Text);
                    var dto = new UpdateCustomerDto
                    {
                        FullName = txtHoTen.Text.Trim(),
                        Phone = txtSDT.Text.Trim(),
                        Address = txtDiaChi.Text.Trim(),
                        Gender = gender,
                        Status = status,
                        DateOfBirth = DateTime.Now
                    };

                    await _customerService.UpdateCustomer(id, dto, _selectedImagePath);
                    MessageBox.Show("Cập nhật thành công!");

                    LoadKhachHangData(txtTimKiem.Text.Trim()); 

                    _isEditing = false;
                    btnSua.Text = "Sửa";
                    SetEnableControls(false);
                    btnThem.Enabled = false;
                    btnXoa.Enabled = true;
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text)) return;

            if (txtMaKH.Text == SPECIAL_CUSTOMER_ID.ToString())
            {
                MessageBox.Show("Không thể xóa 'Khách lẻ' mặc định của hệ thống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show("Xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    int id = int.Parse(txtMaKH.Text);
                    await _customerService.DeleteCustomer(id);
                    MessageBox.Show("Đã xóa!");

                    LoadKhachHangData(txtTimKiem.Text.Trim());
                    ClearInput();

                    _isEditing = false;
                    btnSua.Text = "Sửa";
                    SetEnableControls(false);
                    btnThem.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private async void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtTimKiem.Text.Trim();

            await System.Threading.Tasks.Task.Delay(300); 
            LoadKhachHangData(searchTerm);
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                _selectedImagePath = open.FileName;
                ptbAvatar.Image = Image.FromFile(_selectedImagePath);
                ptbAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void ClearInput()
        {
            txtMaKH.Clear(); txtHoTen.Clear(); txtEmail.Clear(); txtSDT.Clear(); txtDiaChi.Clear();
            _selectedImagePath = null;
            ptbAvatar.Image = null;
            cmbGioiTinh.SelectedIndex = 0;
            cmbTrangThai.SelectedIndex = 0;
        }
    }
}