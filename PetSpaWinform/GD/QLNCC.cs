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
    public partial class QLNCC : Form
    {
        private readonly SupplierService _supplierService;
        private List<SupplierDto> _suppliers;
        private bool _isEditing = false;

        public QLNCC()
        {
            InitializeComponent();
            _supplierService = new SupplierService();
            SetupDataGridView();
            LoadSuppliers();
            SetEnableControls(false);

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = false;
        }

        private void SetEnableControls(bool enable)
        {
            txtTenNCC.Enabled = enable;
            txtDiaChi.Enabled = enable;
            txtSDT.Enabled = enable;
            txtEmail.Enabled = enable;
            txtTKBank.Enabled = enable;
        }

        private async void LoadSuppliers()
        {
            try
            {
                _suppliers = await _supplierService.GetSuppliers();
                BindGrid(_suppliers);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindGrid(List<SupplierDto> suppliers)
        {
            if (suppliers == null) return;

            var displayList = suppliers.Select(s => new
            {
                MaNCC = s.SupplierId,
                TenNCC = s.Name,
                SDT = s.Phone,
                Email = s.Email,
                DiaChi = s.Address,
                TKBank = s.BankAccount
            }).ToList();

            dgvNCC.DataSource = displayList;

            if (dgvNCC.Columns["MaNCC"] != null) dgvNCC.Columns["MaNCC"].Visible = false;
            if (dgvNCC.Columns["TenNCC"] != null)
            {
                dgvNCC.Columns["TenNCC"].HeaderText = "Tên nhà cung cấp";
                dgvNCC.Columns["TenNCC"].Width = 200;
            }
            if (dgvNCC.Columns["SDT"] != null) dgvNCC.Columns["SDT"].HeaderText = "Số điện thoại";
            if (dgvNCC.Columns["Email"] != null) dgvNCC.Columns["Email"].HeaderText = "Email";
            if (dgvNCC.Columns["DiaChi"] != null)
            {
                dgvNCC.Columns["DiaChi"].HeaderText = "Địa chỉ";
                dgvNCC.Columns["DiaChi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dgvNCC.Columns["TKBank"] != null) dgvNCC.Columns["TKBank"].HeaderText = "Tài khoản ngân hàng";
        }

        private void SetupDataGridView()
        {
            dgvNCC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNCC.ReadOnly = true;
            dgvNCC.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNCC.MultiSelect = false;
            dgvNCC.RowHeadersVisible = false;
            dgvNCC.BackgroundColor = Color.White;
            dgvNCC.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 250, 255);

            dgvNCC.EnableHeadersVisualStyles = false;
            dgvNCC.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 144, 255);
            dgvNCC.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvNCC.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvNCC.ColumnHeadersHeight = 40;
            dgvNCC.AllowUserToResizeRows = false;
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvNCC.CurrentRow != null)
            {
                var row = dgvNCC.CurrentRow;
                txtMaNCC.Text = row.Cells["MaNCC"].Value?.ToString() ?? "";
                txtTenNCC.Text = row.Cells["TenNCC"].Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? "";
                txtSDT.Text = row.Cells["SDT"].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                txtTKBank.Text = row.Cells["TKBank"].Value?.ToString() ?? "";

                _isEditing = false;
                SetEnableControls(false);
                btnSua.Text = "Sửa";

                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();
            if (_suppliers == null) return;

            var filtered = _suppliers.Where(s =>
                (s.Name?.ToLower().Contains(keyword) ?? false) ||
                (s.Phone?.Contains(keyword) ?? false)
            ).ToList();

            BindGrid(filtered);
        }

        private void ClearForm()
        {
            txtMaNCC.Clear();
            txtTenNCC.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            txtTKBank.Clear();
        }

        private int? GetSelectedSupplierId()
        {
            if (dgvNCC.CurrentRow == null) return null;
            var row = dgvNCC.CurrentRow;
            if (row.Cells["MaNCC"] == null || row.Cells["MaNCC"].Value == null) return null;
            if (int.TryParse(row.Cells["MaNCC"].Value.ToString(), out int id))
                return id;
            return null;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtTenNCC.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNCC.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtSDT.Text, @"^\d+$"))
            {
                MessageBox.Show("Số điện thoại chỉ được chứa ký tự số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }
            return true;
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            SetEnableControls(true);
            txtTenNCC.Focus();

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
                var supplier = new CreateSupplierDto
                {
                    Name = txtTenNCC.Text.Trim(),
                    Phone = txtSDT.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Address = txtDiaChi.Text.Trim(),
                    BankAccount = txtTKBank.Text.Trim()
                };

                await _supplierService.CreateSupplier(supplier);
                MessageBox.Show("Thêm nhà cung cấp thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadSuppliers();
                ClearForm();
                SetEnableControls(true);
                txtTenNCC.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            int? selectedId = GetSelectedSupplierId();
            if (!selectedId.HasValue)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_isEditing)
            {
                SetEnableControls(true);
                txtTenNCC.Focus();

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
                    var supplier = new UpdateSupplierDto
                    {
                        Name = txtTenNCC.Text.Trim(),
                        Phone = txtSDT.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Address = txtDiaChi.Text.Trim(),
                        BankAccount = txtTKBank.Text.Trim()
                    };

                    await _supplierService.UpdateSupplier(selectedId.Value, supplier);
                    MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadSuppliers();

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
            int? selectedId = GetSelectedSupplierId();
            if (!selectedId.HasValue)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhà cung cấp này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    await _supplierService.DeleteSupplier(selectedId.Value);
                    MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadSuppliers();
                    ClearForm();
                    SetEnableControls(false);

                    _isEditing = false;
                    btnSua.Text = "Sửa";
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnThem.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa (có thể do ràng buộc dữ liệu)!\nChi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            txtTimKiem_TextChanged(sender, e);
        }
    }
}