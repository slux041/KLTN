using GD.DTOs;
using GD.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GD
{
    public partial class QLLoaiSP : Form
    {
        private readonly CategoryService _categoryService;
        private List<CategoryDto> _categories;
        private bool _isEditing = false;

        public QLLoaiSP()
        {
            InitializeComponent();
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
            dgvLoaiSP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLoaiSP.ReadOnly = true;
            dgvLoaiSP.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLoaiSP.MultiSelect = false;
            dgvLoaiSP.RowHeadersVisible = false;
            dgvLoaiSP.BackgroundColor = Color.White;
            dgvLoaiSP.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 250, 255);

            dgvLoaiSP.EnableHeadersVisualStyles = false;
            dgvLoaiSP.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 144, 255);
            dgvLoaiSP.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLoaiSP.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvLoaiSP.ColumnHeadersHeight = 40;
        }

        private void SetEnableControls(bool enable)
        {
            txtTenLoai.Enabled = enable;
            txtMoTa.Enabled = enable;
            cmbType.Enabled = enable;
            chkIsActive.Enabled = enable;
        }

        private async void LoadData()
        {
            try
            {
                _categories = await _categoryService.GetCategories();
                BindGrid(_categories);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindGrid(List<CategoryDto> categories)
        {
            var displayList = categories.Select(c => new
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Type = c.Type == "product" ? "Sản phẩm" : "Dịch vụ",
                Description = c.Description,
                IsActive = c.IsActive
            }).ToList();

            dgvLoaiSP.DataSource = displayList;

            if (dgvLoaiSP.Columns["CategoryId"] != null) dgvLoaiSP.Columns["CategoryId"].HeaderText = "Mã";
            if (dgvLoaiSP.Columns["Name"] != null) dgvLoaiSP.Columns["Name"].HeaderText = "Tên danh mục";
            if (dgvLoaiSP.Columns["Type"] != null) dgvLoaiSP.Columns["Type"].HeaderText = "Phân loại";
            if (dgvLoaiSP.Columns["Description"] != null) dgvLoaiSP.Columns["Description"].HeaderText = "Mô tả";
            if (dgvLoaiSP.Columns["IsActive"] != null) dgvLoaiSP.Columns["IsActive"].HeaderText = "Kích hoạt";
        }

        private void dgvLoaiSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvLoaiSP.CurrentRow != null)
            {
                var row = dgvLoaiSP.CurrentRow;
                txtMaLoai.Text = row.Cells["CategoryId"].Value?.ToString();
                txtTenLoai.Text = row.Cells["Name"].Value?.ToString();
                txtMoTa.Text = row.Cells["Description"].Value?.ToString();

                string typeVal = row.Cells["Type"].Value?.ToString();
                cmbType.SelectedItem = typeVal == "Sản phẩm" ? "product" : "service";

                if (row.Cells["IsActive"].Value is bool active)
                {
                    chkIsActive.Checked = active;
                }

                _isEditing = false;
                SetEnableControls(false);
                btnSua.Text = "Sửa";

                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtTenLoai.Text))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLoai.Focus();
                return false;
            }
            if (cmbType.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại danh mục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbType.Focus();
                return false;
            }
            return true;
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            SetEnableControls(true);
            txtTenLoai.Focus();

            _isEditing = false;
            btnSua.Text = "Sửa";

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                var dto = new CreateCategoryDto
                {
                    Name = txtTenLoai.Text.Trim(),
                    Type = cmbType.SelectedItem?.ToString() ?? "product",
                    Description = txtMoTa.Text.Trim()
                };

                await _categoryService.CreateCategory(dto);
                MessageBox.Show("Thêm mới thành công!");
                LoadData();
                ClearForm();
                SetEnableControls(false);
                btnThem.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLoai.Text))
            {
                MessageBox.Show("Vui lòng chọn danh mục cần sửa!");
                return;
            }

            if (!_isEditing)
            {
                SetEnableControls(true);
                txtTenLoai.Focus();

                _isEditing = true;
                btnSua.Text = "Lưu";

                btnThem.Enabled = false;
                btnXoa.Enabled = false;
            }
            else
            {
                if (!ValidateInputs()) return;

                try
                {
                    int id = int.Parse(txtMaLoai.Text);
                    var dto = new UpdateCategoryDto
                    {
                        Name = txtTenLoai.Text.Trim(),
                        Description = txtMoTa.Text.Trim(),
                        IsActive = chkIsActive.Checked
                    };

                    await _categoryService.UpdateCategory(id, dto);
                    MessageBox.Show("Cập nhật thành công!");
                    LoadData();

                    _isEditing = false;
                    btnSua.Text = "Sửa";
                    SetEnableControls(false);
                    btnThem.Enabled = false;
                    btnXoa.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLoai.Text))
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    int id = int.Parse(txtMaLoai.Text);
                    await _categoryService.DeleteCategory(id);
                    MessageBox.Show("Xóa thành công!");
                    LoadData();
                    ClearForm();

                    _isEditing = false;
                    btnSua.Text = "Sửa";
                    SetEnableControls(false);
                    btnThem.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.ToLower().Trim();
            if (_categories == null) return;

            var filtered = _categories.Where(c =>
                c.Name.ToLower().Contains(keyword) ||
                (c.Description != null && c.Description.ToLower().Contains(keyword))
            ).ToList();

            BindGrid(filtered);
        }

        private void ClearForm()
        {
            txtMaLoai.Clear();
            txtTenLoai.Clear();
            txtMoTa.Clear();
            cmbType.SelectedIndex = 0;
            chkIsActive.Checked = true;
        }
    }
}