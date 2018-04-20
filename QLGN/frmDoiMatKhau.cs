using log4net;
using QuanLySinhVien;
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
    public partial class frmDoiMatKhau : Form
    {
        string taikhoan = "";
        KetNoiSql k = new KetNoiSql();
        private ILog lg = LogManager.GetLogger(typeof(fmMain));
        Ngant n = new Ngant();
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }
        public frmDoiMatKhau(string _taiKhoan)
        {
            taikhoan = _taiKhoan;
            InitializeComponent();
        }
        private void btDoiMatKhau_Click(object sender, EventArgs e)
        {
            string matKhauCu, matKhauMoi, nhapLaiMatKhauMoi, matKhauCuDB;
            matKhauMoi = txtMatKhauMoi.Text;
            nhapLaiMatKhauMoi = txtNhapLaiMatKhau.Text;
            matKhauCu = fmDangNhap.GetMD5(txtMatKhauCu.Text);
            try
            {
                string query = "select Matkhau from TAIKHOAN where MANV=N'" +taikhoan + "'";
                DataSet ds = k.KetnoiCSDL_LoadDataSet(query);
                matKhauCuDB = ds.Tables[0].Rows[0]["Matkhau"].ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            if ((txtMatKhauMoi.Text == "") || (txtNhapLaiMatKhau.Text == "") || (txtMatKhauCu.Text == ""))
            {
                MessageBox.Show("Vui lòng nhập đủ 3 trường tài khoản,mật khẩu và nhập lại mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (txtMatKhauCu.Text == "")
                {
                    txtMatKhauCu.Focus();
                }
                else if (txtMatKhauMoi.Text == "")
                {
                    txtMatKhauMoi.Focus();
                }
                else if (txtNhapLaiMatKhau.Text == "")
                {
                    txtNhapLaiMatKhau.Focus();
                }
            }
            else if (matKhauCu != matKhauCuDB)
            {
                lbLoi.Text = "Mật khẩu cũ không đúng vui lòng kiểm tra lại!";
            }
            else if (matKhauMoi != nhapLaiMatKhauMoi)
            {
                lbLoi.Text = "Mật khẩu nhập lại không khớp vui lòng kiểm tra lại!";
            }
            else if (txtMatKhauMoi.Text.Length < 8)
            {
                lbLoi.Text = "Mật khẩu phải có từ 8 kí tự trở lên!";
            }
            else
            {
                try
                {
                    int check=1;
                    check = n.UpdateMatKhau(fmDangNhap.GetMD5(txtMatKhauMoi.Text),taikhoan.Trim());
                    if (check == 0)
                    {
                        MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lbLoi.Text = "";
                        lbMatKhau.Text = "";
                        lbNhapLaiMK.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Đổi mật khẩu thất bại vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    Reset();
                }
                catch (Exception ex)
                {
                    lg.Error(ex);
                }

            }
        }
        private void txtMatKhauMoi_TextChanged(object sender, EventArgs e)
        {
            string matKhau, matKhauNhapLai;
            matKhau = txtMatKhauMoi.Text;
            matKhauNhapLai = txtNhapLaiMatKhau.Text;
            if (matKhau.Length < 8)
            {
                lbMatKhau.Text = "Mật khẩu ngắn thường dễ đoán. \r\nHãy thử mật khẩu có ít nhất 8 ký tự.";
            }
            else if (matKhau.Length >= 8)
            {
                lbMatKhau.Text = "";
            }
        }
        private void txtNhapLaiMatKhau_TextChanged(object sender, EventArgs e)
        {
            string matKhau, matKhauNhapLai;
            matKhau = txtMatKhauMoi.Text;
            matKhauNhapLai = txtNhapLaiMatKhau.Text;
            if (matKhau != matKhauNhapLai)
            {
                lbNhapLaiMK.Text = "Mật khẩu không khớp vui lòng kiểm tra lại!";
            }
            else if (matKhau.Equals(matKhauNhapLai))
            {
                lbNhapLaiMK.Text = "";
            }
        }
        public static string checkPhanQuyen(string _phanQuyen)
        {
            string phanQuyen = "";
            if (_phanQuyen == "nguoidung")
            {
                phanQuyen = "nguoidung";
            }
            if (_phanQuyen == "giaovien")
            {
                phanQuyen = "gv";
            }
            if (_phanQuyen == "truongkhoa")
            {
                phanQuyen = "tk";
            }
            if (_phanQuyen == "admin")
            {
                phanQuyen = "admin";
            }
            return phanQuyen;
        }

        private void btNhapLai_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            txtMatKhauCu.ResetText();
            txtMatKhauMoi.ResetText();
            txtNhapLaiMatKhau.ResetText();
        }
    }
}
