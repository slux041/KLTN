using GD.DTOs;
using GD.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GD
{
    public partial class QLGiaoDich : Form
    {
        private readonly TransactionService _transactionService;
        private List<TransactionDto> _transactions;

        public QLGiaoDich()
        {
            InitializeComponent();
            _transactionService = new TransactionService();

            SetupForm();
            LoadTransactions();
        }

        private void SetupForm()
        {
            cmbLoaiGD.Items.Clear();
            cmbLoaiGD.Items.Add("Tất cả");
            cmbLoaiGD.Items.Add("Sale");
            cmbLoaiGD.Items.Add("Purchase");
            cmbLoaiGD.SelectedIndex = 0;

            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dgvGiaoDich.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGiaoDich.ReadOnly = true;
            dgvGiaoDich.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGiaoDich.BackgroundColor = Color.White;
            dgvGiaoDich.EnableHeadersVisualStyles = false;
            dgvGiaoDich.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 144, 255);
            dgvGiaoDich.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvGiaoDich.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvGiaoDich.ColumnHeadersHeight = 40;
        }

        private async void LoadTransactions()
        {
            try
            {
                string type = cmbLoaiGD.SelectedItem.ToString();
                if (type == "Tất cả") type = null;

                DateTime endDate = dtpDenNgay.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                _transactions = await _transactionService.GetTransactions(type, dtpTuNgay.Value, endDate);

                BindGrid(_transactions);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải giao dịch: " + ex.Message);
            }
        }

        private void BindGrid(List<TransactionDto> list)
        {
            if (list == null) list = new List<TransactionDto>();

            var displayList = list.Select(t => new
            {
                MaGD = t.ReferenceId,
                Loai = t.Type == "Sale" ? "Xuất (Bán)" : "Nhập (Mua)",
                DoiTac = t.Name,
                SoLuong = t.Quantity,
                TongTien = t.Total,
                Ngay = t.Date
            }).OrderByDescending(x => x.Ngay).ToList();

            dgvGiaoDich.DataSource = displayList;

            if (dgvGiaoDich.Columns["MaGD"] != null) dgvGiaoDich.Columns["MaGD"].HeaderText = "Mã GD";
            if (dgvGiaoDich.Columns["Loai"] != null) dgvGiaoDich.Columns["Loai"].HeaderText = "Loại GD";
            if (dgvGiaoDich.Columns["DoiTac"] != null) dgvGiaoDich.Columns["DoiTac"].HeaderText = "Khách hàng / NCC";
            if (dgvGiaoDich.Columns["SoLuong"] != null) dgvGiaoDich.Columns["SoLuong"].HeaderText = "Số lượng SP";

            if (dgvGiaoDich.Columns["TongTien"] != null)
            {
                dgvGiaoDich.Columns["TongTien"].HeaderText = "Tổng tiền";
                dgvGiaoDich.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                dgvGiaoDich.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvGiaoDich.Columns["Ngay"] != null)
            {
                dgvGiaoDich.Columns["Ngay"].HeaderText = "Ngày giao dịch";
                dgvGiaoDich.Columns["Ngay"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }

            lblSoLuongGD.Text = $"Tổng số giao dịch: {list.Count}";

            double totalAmount = list.Sum(x => x.Total);
            lblTongTien.Text = $"Tổng tiền: {totalAmount:N0} VNĐ";
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadTransactions();
        }
    }
}