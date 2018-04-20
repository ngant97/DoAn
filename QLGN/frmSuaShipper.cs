using QLGN.QLGNObj;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLGN
{
    public partial class frmSuaShipper : DevExpress.XtraBars.TabForm
    {
        Ngant n = new Ngant();
        List<clsShipper> ListShipper = new List<clsShipper>();
        List<clsPhuongTien> ListPhuongTien = new List<clsPhuongTien>();
        string maShipper, tenShipper, bienSoXe, soDienThoai, maPhuongTien;

        private void btLuu_Click(object sender, EventArgs e)
        {
            if (txtTen.Text == "")
            {
                MessageBox.Show("Tên shipper không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtBienSoXe.Text == "")
            {
                MessageBox.Show("Biển số xe của shipper không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtSoDienThoai.Text == "")
            {
                MessageBox.Show("Số điện thoại của shipper không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (luedPhuongTien.EditValue == null)
            {
                MessageBox.Show("Vui lòng chọn phương tiện của shipper!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    string maShipper = txtMaShiper.Text;
                    string tenShipper = txtTen.Text;
                    string bienSoXe = txtBienSoXe.Text;
                    string soDienThoai = txtSoDienThoai.Text;
                    string maPhuongTien = luedPhuongTien.EditValue.ToString();
                    int check = n.UpdateShipper(maShipper, tenShipper, bienSoXe, soDienThoai, maPhuongTien);
                    if (check == 0)
                    {
                        MessageBox.Show("Sửa shipper thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fmMain.eventBus.Publish(new EditShipper());
                    }
                    else
                    {
                        MessageBox.Show("Sửa shipper thất bại,vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
               
            }
        }

        private void btNhapLai_Click(object sender, EventArgs e)
        {
            txtBienSoXe.ResetText();
            txtSoDienThoai.ResetText();
            txtTen.ResetText();
            luedPhuongTien.EditValue = null;
        }

        public frmSuaShipper(string MaShipper, string TenShipper, string BienSoXe, string SoDienThoai, string MaPhuongTien)
        {
            InitializeComponent();
            ListPhuongTien = n.List_SelectPhuongTien();
            maShipper = MaShipper;
            tenShipper = TenShipper;
            bienSoXe = BienSoXe;
            soDienThoai = SoDienThoai;
            maPhuongTien = MaPhuongTien;
        }
        private void LoadPhuongTien()
        {
            luedPhuongTien.Properties.DataSource = ListPhuongTien;
            luedPhuongTien.Properties.DisplayMember = "TenPhuongTien";
            luedPhuongTien.Properties.ValueMember = "MaPhuongTien";
        }
        private void LoadForm()
        {
            txtMaShiper.Text = maShipper;
            txtTen.Text = tenShipper;
            txtBienSoXe.Text = bienSoXe;
            txtSoDienThoai.Text = soDienThoai;
            luedPhuongTien.EditValue = maPhuongTien;
        }
        private void frmSuaShipper_Load(object sender, EventArgs e)
        {
            LoadPhuongTien();
            LoadForm();
        }
    }
}
