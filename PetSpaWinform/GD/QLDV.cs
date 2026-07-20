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
    public partial class QLDV : Form
    {
        private readonly ServiceService _serviceService;
        private readonly CategoryService _categoryService;
        private List<ServiceDto> _services;
        private List<CategoryDto> _categories;
        private bool _isEditing = false;

        public QLDV()
        {
            InitializeComponent();
            _serviceService = new ServiceService();
            _categoryService = new CategoryService();

            SetupDataGridView();
            LoadData();
            SetEnableControls(false);

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = false;
        }

        private void SetupDataGridView()
        {
            dgvdichvu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvdichvu.ReadOnly = true;
            dgvdichvu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvdichvu.MultiSelect = false;
            dgvdichvu.RowHeadersVisible = false;
            dgvdichvu.BackgroundColor = Color.White;
            dgvdichvu.EnableHeadersVisualStyles = false;
            dgvdichvu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 144, 255);
            dgvdichvu.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvdichvu.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvdichvu.ColumnHeadersHeight = 40;

            dgvGiaCanNang.BackgroundColor = Color.WhiteSmoke;
            dgvGiaCanNang.RowHeadersVisible = false;
            dgvGiaCanNang.AllowUserToAddRows = true;
        }

        private void SetEnableControls(bool enable)
        {
            txtTenDV.Enabled = enable;
            txtMoTa.Enabled = enable;
            txtGia.Enabled = enable;
            cmbThoiGian.Enabled = enable;
            cmbTrangThai.Enabled = enable;
            cmbDanhMuc.Enabled = enable;
            dgvGiaCanNang.Enabled = enable;
        }

        private async void LoadData()
        {
            try
            {
                LoadComboBoxData();
                await LoadCategories();
                await LoadServices();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void LoadComboBoxData()
        {
            cmbTrangThai.Items.Clear();
            cmbTrangThai.Items.Add("Hoạt động");
            cmbTrangThai.Items.Add("Ngưng hoạt động");

            cmbThoiGian.Items.Clear();
            cmbThoiGian.Items.Add("15"); cmbThoiGian.Items.Add("30"); cmbThoiGian.Items.Add("45");
            cmbThoiGian.Items.Add("60"); cmbThoiGian.Items.Add("90"); cmbThoiGian.Items.Add("120");
        }

        private async System.Threading.Tasks.Task LoadCategories()
        {
            _categories = await _categoryService.GetCategories("service");
            cmbDanhMuc.DataSource = _categories;
            cmbDanhMuc.DisplayMember = "Name";
            cmbDanhMuc.ValueMember = "CategoryId";
            cmbDanhMuc.SelectedIndex = -1;
        }

        private async System.Threading.Tasks.Task LoadServices()
        {
            _services = await _serviceService.GetServices();
            BindGrid(_services);
        }

        private void BindGrid(List<ServiceDto> services)
        {
            var displayList = services.OrderBy(s => s.ServiceId).Select(s => new
            {
                service_id = s.ServiceId,
                name = s.Name,
                description = s.Description,
                price = s.Price,
                duration_minutes = s.DurationMinutes,
                category_name = s.CategoryName,
                is_active = s.IsActive ? "Hoạt động" : "Ngưng",
                category_id = s.CategoryId
            }).ToList();

            dgvdichvu.DataSource = displayList;

            dgvdichvu.Columns["service_id"].HeaderText = "Mã";
            dgvdichvu.Columns["name"].HeaderText = "Tên dịch vụ";
            dgvdichvu.Columns["description"].HeaderText = "Mô tả";
            dgvdichvu.Columns["price"].HeaderText = "Giá gốc";
            dgvdichvu.Columns["price"].DefaultCellStyle.Format = "N0";
            dgvdichvu.Columns["duration_minutes"].HeaderText = "Thời gian (phút)";
            dgvdichvu.Columns["category_name"].HeaderText = "Danh mục";
            dgvdichvu.Columns["is_active"].HeaderText = "Trạng thái";
            dgvdichvu.Columns["category_id"].Visible = false;
        }

        private async void dgvDichvu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvdichvu.CurrentRow == null) return;
            var row = dgvdichvu.CurrentRow;

            txtMaDV.Text = row.Cells["service_id"].Value?.ToString();
            txtTenDV.Text = row.Cells["name"].Value?.ToString();
            txtMoTa.Text = row.Cells["description"].Value?.ToString();
            txtGia.Text = row.Cells["price"].Value?.ToString();
            cmbThoiGian.Text = row.Cells["duration_minutes"].Value?.ToString();
            cmbTrangThai.Text = row.Cells["is_active"].Value?.ToString();

            if (row.Cells["category_id"].Value != null)
                cmbDanhMuc.SelectedValue = row.Cells["category_id"].Value;

            try
            {
                int serviceId = int.Parse(txtMaDV.Text);
                var serviceDetail = await _serviceService.GetService(serviceId);

                dgvGiaCanNang.Rows.Clear();
                if (serviceDetail.ServicePrices != null)
                {
                    foreach (var p in serviceDetail.ServicePrices)
                    {
                        dgvGiaCanNang.Rows.Add(p.MinWeight, p.MaxWeight, p.Price, p.PetType);
                    }
                }
            }
            catch { }

            _isEditing = false;
            SetEnableControls(false);
            btnSua.Text = "Sửa";

            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            ClearInput();
            SetEnableControls(true);
            txtTenDV.Focus();

            _isEditing = false;
            btnSua.Text = "Sửa";

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                var prices = new List<ServicePriceDto>();
                foreach (DataGridViewRow row in dgvGiaCanNang.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells[0].Value != null)
                    {
                        prices.Add(new ServicePriceDto
                        {
                            MinWeight = Convert.ToDouble(row.Cells[0].Value),
                            MaxWeight = Convert.ToDouble(row.Cells[1].Value),
                            Price = Convert.ToDouble(row.Cells[2].Value),
                            PetType = row.Cells[3].Value?.ToString() ?? "dog"
                        });
                    }
                }

                var service = new CreateServiceDto
                {
                    Name = txtTenDV.Text.Trim(),
                    Description = txtMoTa.Text.Trim(),
                    Price = double.Parse(txtGia.Text),
                    DurationMinutes = int.Parse(cmbThoiGian.Text),
                    CategoryId = (int)cmbDanhMuc.SelectedValue,
                    PricingMethod = prices.Count > 0 ? "weight_based" : "fixed",
                    ServicePrices = prices
                };
                await _serviceService.CreateService(service);
                MessageBox.Show("Thêm thành công!");

                await LoadServices();
                ClearInput();
                SetEnableControls(false);
                btnThem.Enabled = false;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDV.Text))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần sửa!");
                return;
            }

            if (!_isEditing)
            {
                SetEnableControls(true);
                txtTenDV.Focus();

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
                    var prices = new List<ServicePriceDto>();
                    foreach (DataGridViewRow row in dgvGiaCanNang.Rows)
                    {
                        if (row.IsNewRow) continue;
                        if (row.Cells[0].Value != null)
                        {
                            prices.Add(new ServicePriceDto
                            {
                                MinWeight = Convert.ToDouble(row.Cells[0].Value),
                                MaxWeight = Convert.ToDouble(row.Cells[1].Value),
                                Price = Convert.ToDouble(row.Cells[2].Value),
                                PetType = row.Cells[3].Value?.ToString() ?? "dog"
                            });
                        }
                    }

                    int id = int.Parse(txtMaDV.Text);
                    var service = new UpdateServiceDto
                    {
                        Name = txtTenDV.Text.Trim(),
                        Description = txtMoTa.Text.Trim(),
                        Price = double.Parse(txtGia.Text),
                        DurationMinutes = int.Parse(cmbThoiGian.Text),
                        CategoryId = (int)cmbDanhMuc.SelectedValue,
                        IsActive = cmbTrangThai.Text == "Hoạt động",
                        PricingMethod = prices.Count > 0 ? "weight_based" : "fixed",
                        ServicePrices = prices
                    };
                    await _serviceService.UpdateService(id, service);
                    MessageBox.Show("Cập nhật thành công!");

                    await LoadServices();

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
            if (string.IsNullOrEmpty(txtMaDV.Text))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    int id = int.Parse(txtMaDV.Text);
                    await _serviceService.DeleteService(id);
                    MessageBox.Show("Đã xóa!");
                    await LoadServices();
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

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string k = txtTimKiem.Text.ToLower();
            if (_services == null) return;
            var f = _services.Where(s => s.Name.ToLower().Contains(k)).ToList();
            BindGrid(f);
        }

        private void ClearInput()
        {
            txtMaDV.Clear(); txtTenDV.Clear(); txtMoTa.Clear(); txtGia.Clear();
            cmbThoiGian.SelectedIndex = 0; cmbTrangThai.SelectedIndex = 0; cmbDanhMuc.SelectedIndex = -1;
            dgvGiaCanNang.Rows.Clear();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTenDV.Text)) { MessageBox.Show("Nhập tên DV!"); txtTenDV.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtGia.Text) || !double.TryParse(txtGia.Text, out _)) { MessageBox.Show("Giá không hợp lệ!"); txtGia.Focus(); return false; }
            if (cmbDanhMuc.SelectedValue == null) { MessageBox.Show("Chọn danh mục!"); cmbDanhMuc.Focus(); return false; }
            return true;
        }
    }
}