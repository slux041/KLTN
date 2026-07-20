using GD.DTOs;
using GD.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GD
{
    public partial class QLSP : Form
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly SupplierService _supplierService;
        private readonly PurchaseOrderService _purchaseOrderService;
        private List<CategoryDto> _categories;
        private List<SupplierDto> _suppliers;
        private List<ProductDto> _allProductsForCombo;
        private int _currentPage = 1;
        private int _pageSize = 10;
        private int _totalPages = 0;
        private string _currentSearch = "";
        private List<CreatePurchaseOrderItemDto> _cartItems = new List<CreatePurchaseOrderItemDto>();
        private bool _isEditing = false;
        private string _selectedImagePath = null;

        public QLSP()
        {
            InitializeComponent();
            _productService = new ProductService();
            _categoryService = new CategoryService();
            _supplierService = new SupplierService();
            _purchaseOrderService = new PurchaseOrderService();

            SetupDataGridView();
            SetupDataGridViewNhapHang();
            LoadData();
            SetEnableControls(false);

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = false;
        }

        private void SetupDataGridView()
        {
            var dgv = dgvSanPham;
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 144, 255);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 11);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 230, 150);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.RowHeadersVisible = false;
        }

        private void SetupDataGridViewNhapHang()
        {
            var dgv = dgvNhapHang;
            dgv.BackgroundColor = Color.WhiteSmoke;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.RowHeadersVisible = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.SeaGreen;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }

        private void SetEnableControls(bool enable)
        {
            txtTenSP.Enabled = enable;
            txtLoai.Enabled = enable;
            txtGiaBan.Enabled = enable;
            txtDonVi.Enabled = enable;
            txtSL.Enabled = enable;
            txtThuongHieu.Enabled = enable;
            txtMoTa.Enabled = enable;
            cmbTrangThai.Enabled = enable;
            btnChonAnh.Enabled = enable;
        }

        private async void LoadData()
        {
            try
            {
                if (_categories == null)
                {
                    _categories = await _categoryService.GetCategories("product");
                    txtLoai.DataSource = _categories;
                    txtLoai.DisplayMember = "Name";
                    txtLoai.ValueMember = "CategoryId";
                    txtLoai.SelectedIndex = -1;
                }

                if (cmbTrangThai.Items.Count == 0)
                {
                    cmbTrangThai.Items.Add("bán");
                    cmbTrangThai.Items.Add("ngưng bán");
                }

                await LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private async void tabControlKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlKho.SelectedTab == tabDanhSach)
            {
                await LoadProducts();
            }
            else if (tabControlKho.SelectedTab == tabNhapHang)
            {
                await LoadSuppliers();
                if (_allProductsForCombo == null) await LoadProductsForComboBox();
            }
        }

        private async Task LoadProducts()
        {
            var paginationData = await _productService.GetProducts(_currentSearch, _currentPage, _pageSize);

            if (paginationData != null && paginationData.Items != null)
            {
                BindGrid(paginationData.Items);
                _totalPages = paginationData.TotalPages;
                lblSoTrang.Text = $"{_currentPage}/{_totalPages}";
                btnTruoc.Enabled = paginationData.HasPrevious;
                btnSau.Enabled = paginationData.HasNext;
            }
            else
            {
                dgvSanPham.DataSource = null;
                lblSoTrang.Text = "0/0";
                btnTruoc.Enabled = false;
                btnSau.Enabled = false;
            }
        }

        private void BindGrid(List<ProductDto> products)
        {
            var displayList = products.Select(p => new
            {
                product_id = p.ProductId,
                name = p.Name,
                description = p.Description,
                price = p.Price,
                unit = p.Unit,
                stock_quantity = p.StockQuantity,
                category_name = p.CategoryName,
                trang_thai = p.IsActive ? "bán" : "ngưng bán",
                brand = p.Brand,
                image_url = p.ImageUrl,
                category_id = p.CategoryId
            }).ToList();

            dgvSanPham.DataSource = displayList;

            if (dgvSanPham.Columns["product_id"] != null) dgvSanPham.Columns["product_id"].HeaderText = "Mã SP";
            if (dgvSanPham.Columns["name"] != null) dgvSanPham.Columns["name"].HeaderText = "Tên SP";
            if (dgvSanPham.Columns["description"] != null) dgvSanPham.Columns["description"].HeaderText = "Mô tả";
            if (dgvSanPham.Columns["price"] != null) dgvSanPham.Columns["price"].HeaderText = "Giá";
            if (dgvSanPham.Columns["unit"] != null) dgvSanPham.Columns["unit"].HeaderText = "Đơn vị";
            if (dgvSanPham.Columns["stock_quantity"] != null) dgvSanPham.Columns["stock_quantity"].HeaderText = "Tồn kho";
            if (dgvSanPham.Columns["category_name"] != null) dgvSanPham.Columns["category_name"].HeaderText = "Loại sản phẩm";
            if (dgvSanPham.Columns["trang_thai"] != null) dgvSanPham.Columns["trang_thai"].HeaderText = "Trạng thái";
            if (dgvSanPham.Columns["category_id"] != null) dgvSanPham.Columns["category_id"].Visible = false;
            if (dgvSanPham.Columns["image_url"] != null) dgvSanPham.Columns["image_url"].Visible = false;
        }

        private async void btnTruoc_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1) { _currentPage--; await LoadProducts(); }
        }

        private async void btnSau_Click(object sender, EventArgs e)
        {
            if (_currentPage < _totalPages) { _currentPage++; await LoadProducts(); }
        }

        private async void PerformSearch()
        {
            _currentSearch = txtTimKiemTen.Text.Trim();
            if (string.IsNullOrEmpty(_currentSearch)) _currentSearch = txtTimkiemMa.Text.Trim();
            _currentPage = 1;
            await LoadProducts();
        }

        private void txtTimkiemMa_TextChanged(object sender, EventArgs e) => PerformSearch();
        private void txtTimKiemTen_TextChanged(object sender, EventArgs e) => PerformSearch();

        private void dgvSanPham_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow != null)
            {
                var row = dgvSanPham.CurrentRow;
                txtMaSP.Text = row.Cells["product_id"].Value?.ToString();
                txtTenSP.Text = row.Cells["name"].Value?.ToString();
                txtMoTa.Text = row.Cells["description"].Value?.ToString();
                txtGiaBan.Text = row.Cells["price"].Value?.ToString();
                txtDonVi.Text = row.Cells["unit"].Value?.ToString();
                txtSL.Text = row.Cells["stock_quantity"].Value?.ToString();
                txtThuongHieu.Text = row.Cells["brand"].Value?.ToString();

                if (row.Cells["category_id"].Value != null) txtLoai.SelectedValue = row.Cells["category_id"].Value;
                string status = row.Cells["trang_thai"].Value?.ToString();
                cmbTrangThai.SelectedItem = status;

                string imageUrl = row.Cells["image_url"].Value?.ToString();
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    try
                    {
                        pictureBoxSanPham.LoadAsync(imageUrl);
                    }
                    catch { pictureBoxSanPham.Image = null; }
                }
                else
                {
                    pictureBoxSanPham.Image = null;
                }

                _isEditing = false;
                SetEnableControls(false);
                btnSua.Text = "Sửa";

                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            ClearFields();
            SetEnableControls(true);
            txtMaSP.Enabled = false;
            txtTenSP.Focus();

            _isEditing = false;
            btnSua.Text = "Sửa";

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (open.ShowDialog() == DialogResult.OK)
            {
                _selectedImagePath = open.FileName;
                pictureBoxSanPham.Image = Image.FromFile(_selectedImagePath);
                pictureBoxSanPham.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private bool ValidateProductInputs()
        {
            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSP.Focus();
                return false;
            }

            if (txtLoai.SelectedIndex == -1 || txtLoai.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLoai.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtGiaBan.Text) || !double.TryParse(txtGiaBan.Text, out double price) || price < 0)
            {
                MessageBox.Show("Giá bán phải là số và không được âm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaBan.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDonVi.Text))
            {
                MessageBox.Show("Vui lòng nhập đơn vị tính!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonVi.Focus();
                return false;
            }

            if (cmbTrangThai.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn trạng thái kinh doanh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTrangThai.Focus();
                return false;
            }

            return true;
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateProductInputs()) return;

            try
            {
                var product = GetProductFromForm();
                await _productService.CreateProduct(product, _selectedImagePath);
                MessageBox.Show("Thêm sản phẩm thành công!");
                _currentPage = 1;
                await LoadProducts();
                ClearFields();
                SetEnableControls(true);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSP.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!");
                return;
            }

            if (!_isEditing)
            {
                SetEnableControls(true);
                txtMaSP.Enabled = false;

                _isEditing = true;
                btnSua.Text = "Lưu";

                btnThem.Enabled = false;
                btnXoa.Enabled = false;
            }
            else
            {
                if (!ValidateProductInputs()) return;

                try
                {
                    int id = int.Parse(txtMaSP.Text);
                    var product = GetProductFromForm();
                    var updateDto = new UpdateProductDto
                    {
                        Name = product.Name,
                        CategoryId = product.CategoryId,
                        Price = product.Price,
                        Unit = product.Unit,
                        StockQuantity = product.StockQuantity,
                        Description = product.Description,
                        IsActive = product.IsActive,
                        Brand = product.Brand
                    };
                    await _productService.UpdateProduct(id, updateDto, _selectedImagePath);
                    MessageBox.Show("Cập nhật thành công!");

                    await LoadProducts();

                    _isEditing = false;
                    btnSua.Text = "Sửa";
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSP.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int id = int.Parse(txtMaSP.Text);
                    await _productService.DeleteProduct(id);
                    MessageBox.Show("Xóa thành công!");
                    await LoadProducts();
                    ClearFields();

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

        private CreateProductDto GetProductFromForm()
        {
            return new CreateProductDto
            {
                Name = txtTenSP.Text.Trim(),
                CategoryId = (int)txtLoai.SelectedValue,
                Price = double.Parse(txtGiaBan.Text),
                Unit = txtDonVi.Text.Trim(),
                Description = txtMoTa.Text.Trim(),
                IsActive = cmbTrangThai.SelectedItem?.ToString() == "bán",
                Brand = txtThuongHieu.Text.Trim()
            };
        }

        private void ClearFields()
        {
            txtMaSP.Clear(); txtTenSP.Clear(); txtMoTa.Clear(); txtDonVi.Clear();
            txtGiaBan.Clear(); txtThuongHieu.Clear();
            txtLoai.SelectedIndex = -1; cmbTrangThai.SelectedIndex = -1;
            pictureBoxSanPham.Image = null;
            _selectedImagePath = null;
        }

        private async Task LoadSuppliers()
        {
            if (_suppliers == null)
            {
                _suppliers = await _supplierService.GetSuppliers();
                cmbNhaCung.DataSource = _suppliers;
                cmbNhaCung.DisplayMember = "Name";
                cmbNhaCung.ValueMember = "SupplierId";
            }
        }

        private async Task LoadProductsForComboBox()
        {
            var result = await _productService.GetProducts("", 1, 1000);
            if (result != null && result.Items != null)
            {
                _allProductsForCombo = result.Items;
                cmbSanPham.DataSource = _allProductsForCombo;
                cmbSanPham.DisplayMember = "Name";
                cmbSanPham.ValueMember = "ProductId";
                cmbSanPham.SelectedIndex = -1;
            }
        }

        private void cmbSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSanPham.SelectedValue != null && cmbSanPham.SelectedIndex != -1)
            {
                var selected = cmbSanPham.SelectedItem as ProductDto;
                if (selected != null)
                {
                    txtGiaNhap.Text = selected.Price.ToString();
                    numSoLuongNhap.Focus();
                }
            }
        }

        private void btnThemSPNhap_Click(object sender, EventArgs e)
        {
            if (cmbSanPham.SelectedIndex == -1 || cmbSanPham.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtGiaNhap.Text) || !double.TryParse(txtGiaNhap.Text, out double importPrice) || importPrice < 0)
            {
                MessageBox.Show("Giá nhập không hợp lệ (phải là số >= 0)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaNhap.Focus();
                return;
            }

            if (numSoLuongNhap.Value <= 0)
            {
                MessageBox.Show("Số lượng nhập phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numSoLuongNhap.Focus();
                return;
            }

            var selectedProd = cmbSanPham.SelectedItem as ProductDto;
            int qty = (int)numSoLuongNhap.Value;

            var existing = _cartItems.FirstOrDefault(x => x.ProductId == selectedProd.ProductId);
            if (existing != null)
            {
                existing.Quantity += qty;
                existing.Price = importPrice;
            }
            else
            {
                _cartItems.Add(new CreatePurchaseOrderItemDto
                {
                    ProductId = selectedProd.ProductId,
                    ProductName = selectedProd.Name,
                    Quantity = qty,
                    Price = importPrice
                });
            }

            BindGridNhapHang();

            cmbSanPham.SelectedIndex = -1;
            txtGiaNhap.Clear();
            numSoLuongNhap.Value = 1;
            cmbSanPham.Focus();
        }

        private void btnXoaSPNhap_Click(object sender, EventArgs e)
        {
            if (dgvNhapHang.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!");
                return;
            }

            if (dgvNhapHang.CurrentRow.Cells["ProductId"].Value == null) return;
            int prodId = (int)dgvNhapHang.CurrentRow.Cells["ProductId"].Value;

            var item = _cartItems.FirstOrDefault(x => x.ProductId == prodId);
            if (item != null)
            {
                _cartItems.Remove(item);
                BindGridNhapHang();
            }
        }

        private void BindGridNhapHang()
        {
            dgvNhapHang.DataSource = null;
            dgvNhapHang.DataSource = _cartItems;

            dgvNhapHang.Columns["ProductId"].HeaderText = "Mã SP";
            dgvNhapHang.Columns["ProductName"].HeaderText = "Tên Sản Phẩm";
            dgvNhapHang.Columns["Quantity"].HeaderText = "Số Lượng";
            dgvNhapHang.Columns["Price"].HeaderText = "Giá Nhập";
            dgvNhapHang.Columns["Price"].DefaultCellStyle.Format = "N0";
            dgvNhapHang.Columns["Total"].HeaderText = "Thành Tiền";
            dgvNhapHang.Columns["Total"].DefaultCellStyle.Format = "N0";

            double total = _cartItems.Sum(x => x.Total);
            lblTongTien.Text = $"Tổng tiền: {total:N0} VNĐ";
        }

        private async void btnLuuPhieu_Click(object sender, EventArgs e)
        {
            if (_cartItems.Count == 0)
            {
                MessageBox.Show("Danh sách nhập hàng trống! Vui lòng thêm sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbNhaCung.SelectedIndex == -1 || cmbNhaCung.SelectedValue == null)
            {
                MessageBox.Show("Chưa chọn nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbNhaCung.Focus();
                return;
            }

            var dto = new CreatePurchaseOrderDto
            {
                SupplierId = (int)cmbNhaCung.SelectedValue,
                Items = _cartItems
            };

            try
            {
                await _purchaseOrderService.CreatePurchaseOrder(dto);
                MessageBox.Show("Lưu phiếu nhập thành công! Kho đã được cập nhật.");

                _cartItems.Clear();
                BindGridNhapHang();
                cmbSanPham.SelectedIndex = -1;

                await LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phiếu: " + ex.Message);
            }
        }

        private void txtSL_TextChanged(object sender, EventArgs e)
        {

        }
    }
}