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
    public partial class frmThemShipper : DevExpress.XtraBars.TabForm
    {
        Ngant n = new Ngant();
        List<clsShipper> ListShipper = new List<clsShipper>();
        List<clsPhuongTien> ListPhuongTien = new List<clsPhuongTien>();
        public frmThemShipper()
        {
            InitializeComponent();
            ListPhuongTien = n.List_SelectPhuongTien();
            
        }
        private void btTaoMa_Click(object sender, EventArgs e)
        {
            ListShipper = n.List_SelectShipper();
            txtMaShiper.Text = "SP-" + BienVaHam.TaoMa();
        }
        private void LoadPhuongTien()
        {
            luedPhuongTien.Properties.DataSource = ListPhuongTien;
            luedPhuongTien.Properties.DisplayMember = "TenPhuongTien";
            luedPhuongTien.Properties.ValueMember = "MaPhuongTien";
        }

        private void frmThemShipper_Load(object sender, EventArgs e)
        {
            LoadPhuongTien();
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            ListShipper = n.List_SelectShipper();
            int dem=0;
            for(int i=0;i<ListShipper.Count;i++)
            {
                if(ListShipper[i].MaShipper.Trim().Equals(txtMaShiper.Text.Trim()))
                {
                    MessageBox.Show("Mã shipper đã tồn tại vui lòng chọn mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dem++;
                    break;
                }
            }
            if(dem==0)
            {
                if (txtMaShiper.Text == "")
                {
                    MessageBox.Show("Mã shipper không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtTen.Text == "")
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
                    string maShipper = txtMaShiper.Text;
                    string tenShipper = txtTen.Text;
                    string bienSoXe = txtBienSoXe.Text;
                    string soDienThoai = txtSoDienThoai.Text;
                    string maPhuongTien = luedPhuongTien.EditValue.ToString();
                    int check= n.InsertShipper(maShipper, tenShipper, bienSoXe, soDienThoai, maPhuongTien);
                    if(check==0)
                    {
                        MessageBox.Show("Thêm shipper thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fmMain.eventBus.Publish(new InputShipper());
                        btNhapLai_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Thêm shipper thất bại vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void btNhapLai_Click(object sender, EventArgs e)
        {
            txtBienSoXe.ResetText();
            txtMaShiper.ResetText();
            txtSoDienThoai.ResetText();
            txtTen.ResetText();
            luedPhuongTien.EditValue = null;
        }
    }
}
