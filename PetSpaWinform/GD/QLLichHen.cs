using GD.DTOs;
using GD.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GD
{
    public partial class QLLichHen : Form
    {
        private readonly AppointmentService _appointmentService;
        private readonly ServiceService _serviceService;
        private List<AppointmentDto> _appointments;
        private List<ServiceDto> _mainServices;
        private bool _isEditMode = false;
        private AppointmentDto _selectedAppointment;
        private static Dictionary<int, double> _appointmentWeights = new Dictionary<int, double>();

        public QLLichHen()
        {
            InitializeComponent();
            _appointmentService = new AppointmentService();
            _serviceService = new ServiceService();

            SetupUI();
            LoadInitData();
        }

        private void SetupUI()
        {
            dgvLichHen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLichHen.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLichHen.ReadOnly = true;
            dgvLichHen.MultiSelect = false;
            dgvLichHen.RowHeadersVisible = false;
            dgvLichHen.BackgroundColor = Color.White;
            dgvLichHen.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

            dgvLichHen.EnableHeadersVisualStyles = false;
            dgvLichHen.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgvLichHen.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLichHen.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvLichHen.ColumnHeadersHeight = 40;

            dtpNgayHen.Format = DateTimePickerFormat.Custom;
            dtpNgayHen.CustomFormat = "dd/MM/yyyy";

            cmbLoaiThuCung.SelectedIndexChanged += CmbLoaiThuCung_SelectedIndexChanged;
            dtpNgayHen.ValueChanged += DtpNgayHen_ValueChanged;

            ToggleInputs(false);
            btnHuyBo.Visible = false;
        }

        private async void LoadInitData()
        {
            try
            {
                var allServices = await _serviceService.GetServices(isActive: true);
                _mainServices = allServices.Where(s => s.ServiceId == 5 || s.ServiceId == 6 || s.ServiceId == 7).ToList();

                if (cmbLoaiThuCung.Items.Count > 0) cmbLoaiThuCung.SelectedIndex = 0;

                await LoadAvailableSlots(dtpNgayHen.Value);
                await LoadAppointments();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khởi tạo dữ liệu: " + ex.Message); }
        }

        private async void DtpNgayHen_ValueChanged(object sender, EventArgs e) => await LoadAvailableSlots(dtpNgayHen.Value);

        private async Task LoadAvailableSlots(DateTime date)
        {
            try
            {
                if (date.Date < DateTime.Now.Date)
                {
                    cmbKhungGio.DataSource = new string[] { "N/A" }; return;
                }
                var slots = await _appointmentService.GetAvailableSlots(date);
                var availableSlots = slots.Where(s => s.IsAvailable).Select(s => s.TimeSlot).ToList();

                if (_selectedAppointment != null && _isEditMode && !availableSlots.Contains(_selectedAppointment.TimeSlot))
                    availableSlots.Insert(0, _selectedAppointment.TimeSlot);

                cmbKhungGio.DataSource = availableSlots.Count > 0 ? availableSlots : new List<string> { "Đã kín lịch" };
                cmbKhungGio.Enabled = availableSlots.Count > 0;
            }
            catch { cmbKhungGio.DataSource = new string[] { "09:00", "10:00", "14:00" }; }
        }

        private void CmbLoaiThuCung_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_mainServices == null) return;
            string type = cmbLoaiThuCung.SelectedItem?.ToString();
            var filtered = type == "Mèo" ? _mainServices.Where(s => s.ServiceId != 7).ToList() : _mainServices.ToList();

            cmbDichVu.DataSource = filtered;
            cmbDichVu.DisplayMember = "Name";
            cmbDichVu.ValueMember = "ServiceId";
        }

        private async Task LoadAppointments()
        {
            _appointments = await _appointmentService.GetAppointments();
            BindGrid(_appointments);
        }

        private void BindGrid(List<AppointmentDto> listData)
        {
            if (listData == null) return;
            var viewData = listData.Select(a => new
            {
                a.AppointmentId,
                Date = a.AppointmentDate.ToString("dd/MM/yyyy"),
                a.TimeSlot,
                Customer = a.CustomerName ?? "Khách vãng lai",
                Phone = a.CustomerPhone,
                Service = a.ServiceName,
                Pet = (a.PetType == "dog" ? "Chó" : "Mèo") + " (" + (a.PetInfo ?? "") + ")",
                Status = TranslateStatus(a.Status),
                Source = a.Source == "web" ? "Web" : "Tại quầy",
                RawStatus = a.Status
            }).OrderByDescending(x => x.AppointmentId).ToList();

            dgvLichHen.DataSource = viewData;

            dgvLichHen.Columns["AppointmentId"].HeaderText = "Mã";
            dgvLichHen.Columns["Date"].HeaderText = "Ngày hẹn";
            dgvLichHen.Columns["TimeSlot"].HeaderText = "Khung giờ";
            dgvLichHen.Columns["Customer"].HeaderText = "Khách hàng";
            dgvLichHen.Columns["Phone"].HeaderText = "SĐT";
            dgvLichHen.Columns["Service"].HeaderText = "Dịch vụ";
            dgvLichHen.Columns["Pet"].HeaderText = "Thú cưng";
            dgvLichHen.Columns["Status"].HeaderText = "Trạng thái";
            dgvLichHen.Columns["Source"].HeaderText = "Nguồn";

            if (dgvLichHen.Columns["RawStatus"] != null) dgvLichHen.Columns["RawStatus"].Visible = false;

            if (dgvLichHen.Rows.Count > 0)
            {
                dgvLichHen.ClearSelection();
                dgvLichHen.Rows[0].Selected = true;
                dgvLichHen.CurrentCell = dgvLichHen.Rows[0].Cells[0];
                dgvLichHen_CellClick(dgvLichHen, new DataGridViewCellEventArgs(0, 0));
            }
            else
            {
                btnTaoMoi_Click(null, null);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (_appointments == null) return;
            string k = txtTimKiem.Text.Trim().ToLower();
            BindGrid(string.IsNullOrEmpty(k) ? _appointments : _appointments.Where(a =>
                (a.CustomerName != null && a.CustomerName.ToLower().Contains(k)) ||
                (a.CustomerPhone != null && a.CustomerPhone.Contains(k))).ToList());
        }

        private void dgvLichHen_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvLichHen.Rows[e.RowIndex].Cells["AppointmentId"].Value == null) return;

            int id = (int)dgvLichHen.Rows[e.RowIndex].Cells["AppointmentId"].Value;
            _selectedAppointment = _appointments.FirstOrDefault(a => a.AppointmentId == id);

            if (_selectedAppointment != null)
            {
                FillForm(_selectedAppointment);
                UpdateActionButtons(_selectedAppointment.Status);
            }
        }

        private void FillForm(AppointmentDto apt)
        {
            txtTenKhach.Text = apt.CustomerName;
            txtSdt.Text = apt.CustomerPhone;
            cmbLoaiThuCung.SelectedItem = apt.PetType == "dog" ? "Chó" : "Mèo";
            cmbDichVu.SelectedValue = apt.ServiceId;
            dtpNgayHen.Value = apt.AppointmentDate;
            cmbKhungGio.Text = apt.TimeSlot;
            txtGhiChu.Text = apt.PetInfo;
        }

        private void UpdateActionButtons(string status)
        {
            btnXacNhan.Enabled = false;
            btnThanhToanPOS.Enabled = false;
            btnHuyLich.Enabled = false;

            string st = status?.ToLower();

            if (st == "pending")
            {
                btnXacNhan.Enabled = true;
                btnXacNhan.Text = "Xác nhận";
                btnHuyLich.Enabled = true;
            }
            else if (st == "confirmed")
            {
                btnXacNhan.Enabled = true;
                btnXacNhan.Text = "Đã xong";
                btnHuyLich.Enabled = true;
            }
            else if (st == "processing")
            {
                btnXacNhan.Text = "Đang làm...";
                btnThanhToanPOS.Enabled = true;
                btnHuyLich.Enabled = false;
            }
            else
            {
                btnXacNhan.Text = "Cập nhật TT";
            }
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            _isEditMode = false; _selectedAppointment = null;
            txtTenKhach.Clear(); txtSdt.Clear(); txtGhiChu.Clear(); cmbDichVu.SelectedIndex = -1;
            dtpNgayHen.Value = DateTime.Now; LoadAvailableSlots(DateTime.Now);
            ToggleInputs(true); btnHuyBo.Visible = true; btnTaoMoi.Enabled = false;
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenKhach.Text) || string.IsNullOrEmpty(txtSdt.Text)) { MessageBox.Show("Thiếu thông tin khách!"); return; }
            if (!Regex.IsMatch(txtSdt.Text.Trim(), @"^0\d{9}$")) { MessageBox.Show("SĐT sai định dạng!"); return; }
            if (cmbKhungGio.SelectedItem == null || cmbKhungGio.Text == "Đã kín lịch") { MessageBox.Show("Chọn lại giờ!"); return; }

            var dto = new CreateAppointmentDto
            {
                CustomerName = txtTenKhach.Text.Trim(),
                CustomerPhone = txtSdt.Text.Trim(),
                CustomerAddress = "Tại quầy",
                ServiceId = (int)cmbDichVu.SelectedValue,
                PetType = cmbLoaiThuCung.SelectedItem?.ToString() == "Chó" ? "dog" : "cat",
                PetInfo = txtGhiChu.Text,
                AppointmentDate = dtpNgayHen.Value,
                TimeSlot = cmbKhungGio.Text,
                Source = "store"
            };

            try
            {
                await _appointmentService.CreateAppointment(dto);
                MessageBox.Show("Tạo lịch thành công!");
                ToggleInputs(false); btnHuyBo.Visible = false; btnTaoMoi.Enabled = true;
                await LoadAppointments(); await LoadAvailableSlots(dtpNgayHen.Value);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private async void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (_selectedAppointment == null) return;
            string currentStatus = _selectedAppointment.Status.ToLower();

            try
            {
                if (currentStatus == "pending")
                {
                    await _appointmentService.UpdateStatus(_selectedAppointment.AppointmentId, "confirmed");
                    MessageBox.Show("Đã XÁC NHẬN lịch hẹn! Khách đã đến?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadAppointments();
                }
                else if (currentStatus == "confirmed")
                {
                    await _appointmentService.UpdateStatus(_selectedAppointment.AppointmentId, "processing");
                    MessageBox.Show("Đã cập nhật trạng thái: ĐÃ LÀM XONG!\nVui lòng bấm 'TẠO HÓA ĐƠN' để nhập cân nặng và tính tiền.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadAppointments();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi cập nhật: " + ex.Message); }
        }

        private void btnThanhToanPOS_Click(object sender, EventArgs e)
        {
            if (_selectedAppointment == null) return;

            if (_mainServices == null || _mainServices.Count == 0)
            {
                MessageBox.Show("Dữ liệu dịch vụ chưa được tải. Vui lòng thử lại!", "Lỗi");
                return;
            }

            var serviceDto = _mainServices.FirstOrDefault(s => s.ServiceId == _selectedAppointment.ServiceId);
            if (serviceDto == null)
            {
                MessageBox.Show($"Dịch vụ (ID: {_selectedAppointment.ServiceId}) không hợp lệ để thanh toán!", "Lỗi");
                return;
            }

            double weight = 0;
            FrmCheckIn frm = new FrmCheckIn();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                weight = frm.Weight;
                if (_appointmentWeights.ContainsKey(_selectedAppointment.AppointmentId))
                    _appointmentWeights[_selectedAppointment.AppointmentId] = weight;
                else
                    _appointmentWeights.Add(_selectedAppointment.AppointmentId, weight);
            }
            else
            {
                return;
            }

            double finalPrice = _serviceService.CalculateServicePrice(serviceDto, weight, _selectedAppointment.PetType);
            if (finalPrice == 0 && serviceDto.Price > 0) finalPrice = serviceDto.Price;

            var itemToSend = new CreateOrderItemDto
            {
                ServiceId = serviceDto.ServiceId,
                Quantity = 1,
                Price = finalPrice
            };

            try
            {
                QLDH formPOS = Application.OpenForms.OfType<QLDH>().FirstOrDefault();
                if (formPOS == null || formPOS.IsDisposed)
                {
                    formPOS = new QLDH();
                }
                else
                {
                    if (formPOS.WindowState == FormWindowState.Minimized) formPOS.WindowState = FormWindowState.Normal;
                    formPOS.Show();
                    formPOS.BringToFront();
                    formPOS.Focus();
                }

                formPOS.AddAppointmentToCart(
                    _selectedAppointment.AppointmentId,
                    _selectedAppointment.CustomerId,
                    _selectedAppointment.CustomerName,
                    _selectedAppointment.CustomerPhone,
                    itemToSend
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở Form Hóa Đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnHuyLich_Click(object sender, EventArgs e)
        {
            if (_selectedAppointment == null) return;
            if (_selectedAppointment.Status.ToLower() == "completed" || _selectedAppointment.Status.ToLower() == "processing")
            {
                MessageBox.Show("Không thể hủy lịch đã làm xong!", "Cảnh báo"); return;
            }
            if (MessageBox.Show("Hủy lịch này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                await _appointmentService.UpdateStatus(_selectedAppointment.AppointmentId, "cancelled");
                MessageBox.Show("Đã HỦY lịch hẹn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadAppointments(); await LoadAvailableSlots(dtpNgayHen.Value);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e) { ToggleInputs(false); btnHuyBo.Visible = false; btnTaoMoi.Enabled = true; _selectedAppointment = null; }
        private void ToggleInputs(bool enable) { txtTenKhach.Enabled = enable; txtSdt.Enabled = enable; txtGhiChu.Enabled = enable; cmbDichVu.Enabled = enable; cmbLoaiThuCung.Enabled = enable; dtpNgayHen.Enabled = enable; cmbKhungGio.Enabled = enable; btnLuu.Visible = enable; }

        private string TranslateStatus(string status)
        {
            return status?.ToLower() switch
            {
                "pending" => "Chờ xác nhận",
                "confirmed" => "Đã xác nhận",
                "processing" => "Đã làm xong",
                "completed" => "Hoàn thành",
                "cancelled" => "Đã hủy",
                _ => status
            };
        }
    }
}