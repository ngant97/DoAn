using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QuanLySinhVien;
using QLGN.QLGNObj;

namespace QLGN
{
    public partial class frmThemNhanVien : DevExpress.XtraBars.TabForm
    {
        public frmThemNhanVien()
        {
            InitializeComponent();
        }

        private void btHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        KetNoiSql k = new KetNoiSql();
        Ngant n = new Ngant();
        bool checkQuanLy;

        private void btNhapLai_Click(object sender, EventArgs e)
        {
            txtManv.ResetText();
            txtMatKhau.ResetText();
            txtQueQuan.ResetText();
            txtSDT.ResetText();
            txtTenNV.ResetText();
            dEDOB.ResetText();
            cbQuanLy.Checked = false;
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            string maNV = txtManv.Text;
            string tenNv = txtTenNV.Text;
            string matKhau = fmDangNhap.GetMD5(txtMatKhau.Text);
            string queQuan = txtQueQuan.Text;
            string sdt = txtSDT.Text;
            DateTime dateOfBirth = dEDOB.DateTime;



            if (cbQuanLy.Checked == true)
            {
                checkQuanLy = true;
            }
            if (maNV.Trim() == "" || txtMatKhau.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập đủ tài khoản và mật khẩu","Thông báo");
                if (maNV == "")
                {
                    txtManv.Focus();
                }
                else if (matKhau == "")
                {
                    txtMatKhau.Focus();
                }
            }
            else if (dEDOB.EditValue == null)
            {
                MessageBox.Show("Vui lòng nhập ngày sinh", "Thông báo");
            }
            else
            {
                try
                {

                    string querycheck = "select count(*) from TAIKHOAN where MANV =N'" + maNV.Trim() + "'";
                    int i = k.Check(querycheck);
                    if (i != 0)
                    {
                        MessageBox.Show("Tài khoản đã tồn tại vui lòng chọn tài khoản khác");
                    }
                    else 
                    {
                        n.InsertTaiKhoan(maNV, matKhau, checkQuanLy);
                        n.InsertNhanVien(maNV, tenNv, queQuan, sdt, dateOfBirth);
                        MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btNhapLai_Click(sender,e);
                        fmMain.eventBus.Publish(new InputNhanVien());
                        this.DialogResult = DialogResult.OK;
                    }

                }
                catch (Exception ex)
                {

                    throw ex;
                }
                
            }
        }
    }
}