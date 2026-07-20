using System;
using System.Windows.Forms;

namespace GD
{
    public partial class FrmCheckIn : Form
    {
        public double Weight { get; private set; }
        public string Note { get; private set; }
        public bool IsConfirmed { get; private set; } = false;

        public FrmCheckIn()
        {
            InitializeComponent();
            nudCanNang.Enter += (s, e) => nudCanNang.Select(0, nudCanNang.Text.Length);
            nudCanNang.MouseUp += (s, e) => nudCanNang.Select(0, nudCanNang.Text.Length);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (nudCanNang.Value <= 0)
            {
                MessageBox.Show("Vui lòng nhập cân nặng hợp lệ (> 0 kg)!",
                                "Cảnh báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                nudCanNang.Focus();
                return;
            }

            Weight = (double)nudCanNang.Value;
            Note = txtGhiChu.Text.Trim();
            IsConfirmed = true;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsConfirmed = false;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}