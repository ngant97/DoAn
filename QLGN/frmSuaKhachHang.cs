using log4net;
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
    public partial class frmSuaKhachHang : DevExpress.XtraBars.TabForm
    {
        Ngant n = new Ngant();
        List<clsKhachHang> ListNguoiNhan = new List<clsKhachHang>();
        List<clsKhachHang> ListNguoiGui = new List<clsKhachHang>();
        private ILog lg = LogManager.GetLogger(typeof(fmMain));
        string cmnd, hoTen, diaChi, soDienThoai;
        public frmSuaKhachHang()
        {
            InitializeComponent();
        }
        public frmSuaKhachHang(string _cmnd, string _hoTen,string _diaChi,string _SDT)
        {
            InitializeComponent();
            cmnd = _cmnd;
            hoTen = _hoTen;
            diaChi = _diaChi;
            soDienThoai = _SDT;
            txtCMT.Text = cmnd;
            txtDiaChi.Text = diaChi;
            txtHoTen.Text = hoTen;
            txtSoDienThoai.Text = soDienThoai;
        }
        private void btNhapLai_Click(object sender, EventArgs e)
        {
            txtDiaChi.ResetText();
            txtHoTen.ResetText();
            txtSoDienThoai.ResetText();
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            if (txtCMT.Text == "")
            {
                MessageBox.Show("Vui lòng nhập chứng minh thư nhân dân của khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ của khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtHoTen.Text == "")
            {
                MessageBox.Show("Vui lòng nhập họ tên của khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtSoDienThoai.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại của khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string cmnd = txtCMT.Text.Trim();
                string hoTen = txtHoTen.Text.Trim();
                string diaChi = txtDiaChi.Text.Trim();
                string soDienThoai = txtSoDienThoai.Text.Trim();
                int checkNguoiNhan = 1, checkNguoiGui = 1;
                try
                {
                    ListNguoiNhan = n.List_SelectNguoiNhan();
                    for (int i = 0; i < ListNguoiNhan.Count; i++)
                    {
                        if (ListNguoiNhan[i].CMNDKhachHang.Trim().Equals(cmnd))
                        {
                            checkNguoiNhan = n.UpdateNguoiNhan(cmnd, hoTen, diaChi, soDienThoai);
                            break;
                        }
                    }
                    ListNguoiGui = n.List_SelectNguoiGui();
                    for (int i = 0; i < ListNguoiGui.Count; i++)
                    {
                        if (ListNguoiGui[i].CMNDKhachHang.Trim().Equals(cmnd))
                        {
                            checkNguoiGui = n.UpdateNguoiGui(cmnd, hoTen, diaChi, soDienThoai);
                            break;
                        }
                    }
                    if (checkNguoiGui == 0 || checkNguoiNhan == 0)
                    {
                        MessageBox.Show("Sửa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fmMain.eventBus.Publish(new EditKhachHang());
                    }
                    else
                    {
                        MessageBox.Show("Sửa khách hàng thất bại vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    lg.Error(ex);
                }
                
            }
        }
    }
}
