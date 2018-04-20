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
    public partial class frmThemKhachHang : DevExpress.XtraBars.TabForm
    {
        Ngant n = new Ngant();
        List<clsKhachHang> ListKhachHang = new List<clsKhachHang>();
        List<clsKhachHang> ListNguoiNhan = new List<clsKhachHang>();
        List<clsKhachHang> ListNguoiGui = new List<clsKhachHang>();
        public frmThemKhachHang()
        {
            InitializeComponent();
            cbTrangThai.Items.Add("Người gửi");
            cbTrangThai.Items.Add("Người nhận");
            ListKhachHang = n.List_SelectKhachHang();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if(txtCMT.Text=="")
            {
                MessageBox.Show("Vui lòng nhập chứng minh thư nhân dân của khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(txtDiaChi.Text=="")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ của khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(txtHoTen.Text=="")
            {
                MessageBox.Show("Vui lòng nhập họ tên của khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(txtSoDienThoai.Text=="")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại của khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(cbTrangThai.SelectedIndex==-1)
            {
                MessageBox.Show("Vui lòng chọn khách hàng là người gửi hay người nhận", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string cmnd = txtCMT.Text.Trim();
                string hoTen = txtHoTen.Text.Trim();
                string diaChi = txtDiaChi.Text.Trim();
                string soDienThoai = txtSoDienThoai.Text.Trim();
                string trangthai = cbTrangThai.Text.Trim();
                int check = 1;
                int dem = 0;
                if (trangthai.Trim().Equals("Người nhận")) 
                {
                    ListNguoiNhan = n.List_SelectNguoiNhan();
                    for(int i=0;i<ListNguoiNhan.Count;i++)
                    {
                        if(ListNguoiNhan[i].CMNDKhachHang.Trim().Equals(cmnd))
                        {
                            dem++;
                            break;
                        }
                    }
                    if(dem == 0)
                    {
                        check = n.InsertNguoiNhan(cmnd, hoTen, diaChi, soDienThoai);
                    }
                    else
                    {
                        MessageBox.Show("Có khách hàng là người nhận có chứng minh thư này\r\nVui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if(trangthai.Trim().Equals("Người gửi"))
                {
                    ListNguoiGui = n.List_SelectNguoiGui();
                    for (int i = 0; i < ListNguoiGui.Count; i++)
                    {
                        if (ListNguoiGui[i].CMNDKhachHang.Trim().Equals(cmnd))
                        {
                            dem++;
                            break;
                        }
                    }
                    if (dem == 0)
                    {
                        check = n.InsertNguoiGui(cmnd, hoTen, diaChi, soDienThoai);
                    }
                    else
                    {
                        MessageBox.Show("Có khách hàng là người gửi có chứng minh thư này\r\nVui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    
                }
                if(check==0&&dem==0)
                {
                    MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fmMain.eventBus.Publish(new InputKhachHang());
                    simpleButton1_Click(sender, e);
                }
                else if(check!=0&&dem==0)
                {
                    MessageBox.Show("Thêm khách hàng thất bại vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            txtCMT.ResetText();
            txtDiaChi.ResetText();
            txtHoTen.ResetText();
            txtSoDienThoai.ResetText();
            cbTrangThai.ResetText();
        }
    }
}
