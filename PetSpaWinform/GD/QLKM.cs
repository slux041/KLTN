using GD.DTOs;
using GD.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GD
{
    public partial class QLKM : Form
    {
        private readonly PromotionService _promotionService;
        private List<PromotionDto> _promotions;
        private bool _isEditing = false;

        public QLKM()
        {
            InitializeComponent();
            _promotionService = new PromotionService();

            SetupDataGridView();
            LoadComboBoxStatus();
            LoadPromotions();
            SetEnableControls(false);

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = false;
        }

        private void SetupDataGridView()
        {
            dgvKhuyenMai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKhuyenMai.ReadOnly = true;
            dgvKhuyenMai.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhuyenMai.MultiSelect = false;
            dgvKhuyenMai.RowHeadersVisible = false;
            dgvKhuyenMai.BackgroundColor = Color.White;
            dgvKhuyenMai.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 250, 255);
            dgvKhuyenMai.EnableHeadersVisualStyles = false;
            dgvKhuyenMai.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 144, 255);
            dgvKhuyenMai.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvKhuyenMai.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvKhuyenMai.ColumnHeadersHeight = 40;
        }

        private void SetEnableControls(bool enable)
        {
            txtCode.Enabled = enable;
            txtMoTa.Enabled = enable;
            txtGTGiam.Enabled = enable;
            dtbBatDau.Enabled = enable;
            dtpKetThuc.Enabled = enable;
            cmbTrangThai.Enabled = enable;
        }

        private void LoadComboBoxStatus()
        {
            cmbTrangThai.Items.Clear();
            cmbTrangThai.Items.Add("Hoạt động");
            cmbTrangThai.Items.Add("Tạm dừng");
        }

        private async void LoadPromotions()
        {
            try
            {
                _promotions = await _promotionService.GetPromotions();
                BindGrid(_promotions);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void BindGrid(List<PromotionDto> promotions)
        {
            var displayList = promotions.Select(p => new
            {
                MaKM = p.PromotionId,
                Code = p.Code,
                MoTa = p.Description,
                GiamGia = p.DiscountPercent,
                NgayBD = p.StartDate.ToString("dd/MM/yyyy"),
                NgayKT = p.EndDate.ToString("dd/MM/yyyy"),
                TrangThai = p.IsActive ? "Hoạt động" : "Tạm dừng"
            }).ToList();

            dgvKhuyenMai.DataSource = displayList;

            dgvKhuyenMai.Columns["MaKM"].Visible = false;
            dgvKhuyenMai.Columns["Code"].HeaderText = "Mã Code";
            dgvKhuyenMai.Columns["MoTa"].HeaderText = "Mô tả";
            dgvKhuyenMai.Columns["GiamGia"].HeaderText = "Giảm (%)";
            dgvKhuyenMai.Columns["NgayBD"].HeaderText = "Bắt đầu";
            dgvKhuyenMai.Columns["NgayKT"].HeaderText = "Kết thúc";
            dgvKhuyenMai.Columns["TrangThai"].HeaderText = "Trạng thái";
        }

        private void dgvKhuyenMai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvKhuyenMai.CurrentRow == null) return;
            var row = dgvKhuyenMai.CurrentRow;

            txtMaKM.Text = row.Cells["MaKM"].Value.ToString();
            txtCode.Text = row.Cells["Code"].Value.ToString();
            txtMoTa.Text = row.Cells["MoTa"].Value?.ToString();
            txtGTGiam.Text = row.Cells["GiamGia"].Value.ToString();
            cmbTrangThai.Text = row.Cells["TrangThai"].Value.ToString();

            if (DateTime.TryParseExact(row.Cells["NgayBD"].Value.ToString(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime d1))
                dtbBatDau.Value = d1;
            if (DateTime.TryParseExact(row.Cells["NgayKT"].Value.ToString(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime d2))
                dtpKetThuc.Value = d2;

            _isEditing = false;
            SetEnableControls(false);
            btnSua.Text = "Sửa";

            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã khuyến mãi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCode.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtGTGiam.Text) || !double.TryParse(txtGTGiam.Text, out double percent) || percent < 0 || percent > 100)
            {
                MessageBox.Show("Phần trăm giảm phải là số từ 0 đến 100!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGTGiam.Focus();
                return false;
            }
            if (dtpKetThuc.Value < dtbBatDau.Value)
            {
                MessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpKetThuc.Focus();
                return false;
            }
            return true;
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            SetEnableControls(true);
            txtCode.Focus();

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
                var dto = new CreatePromotionDto
                {
                    Code = txtCode.Text.ToUpper().Trim(),
                    Description = txtMoTa.Text.Trim(),
                    DiscountPercent = double.Parse(txtGTGiam.Text),
                    StartDate = dtbBatDau.Value,
                    EndDate = dtpKetThuc.Value,
                    IsActive = cmbTrangThai.Text == "Hoạt động"
                };

                await _promotionService.CreatePromotion(dto);
                MessageBox.Show("Thêm thành công!");
                LoadPromotions();
                ClearForm();
                SetEnableControls(false);
                btnThem.Enabled = false;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKM.Text))
            {
                MessageBox.Show("Vui lòng chọn khuyến mãi cần sửa!");
                return;
            }

            if (!_isEditing)
            {
                SetEnableControls(true);
                txtCode.Enabled = false;
                txtMoTa.Focus();

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
                    int id = int.Parse(txtMaKM.Text);
                    var dto = new UpdatePromotionDto
                    {
                        Description = txtMoTa.Text.Trim(),
                        DiscountPercent = double.Parse(txtGTGiam.Text),
                        StartDate = dtbBatDau.Value,
                        EndDate = dtpKetThuc.Value,
                        IsActive = cmbTrangThai.Text == "Hoạt động"
                    };

                    await _promotionService.UpdatePromotion(id, dto);
                    MessageBox.Show("Cập nhật thành công!");
                    LoadPromotions();

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
            if (string.IsNullOrEmpty(txtMaKM.Text))
            {
                MessageBox.Show("Vui lòng chọn khuyến mãi cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa mã này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    int id = int.Parse(txtMaKM.Text);
                    await _promotionService.DeletePromotion(id);
                    MessageBox.Show("Đã xóa!");
                    LoadPromotions();
                    ClearForm();

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

        private void txtTKKM_TextChanged(object sender, EventArgs e)
        {
            string key = txtTKKM.Text.ToUpper().Trim();
            if (_promotions == null) return;
            var filtered = _promotions.Where(p => p.Code.Contains(key)).ToList();
            BindGrid(filtered);
        }

        private void ClearForm()
        {
            txtMaKM.Clear(); txtCode.Clear(); txtMoTa.Clear(); txtGTGiam.Clear();
            dtbBatDau.Value = DateTime.Now; dtpKetThuc.Value = DateTime.Now.AddDays(7);
            cmbTrangThai.SelectedIndex = 0;
        }
    }
}