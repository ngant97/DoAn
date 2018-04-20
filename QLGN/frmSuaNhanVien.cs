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
using Redbus;
using QLGN.QLGNObj;
using log4net;

namespace QLGN
{
    
    public partial class frmSuaNhanVien : DevExpress.XtraBars.TabForm
    {
        private ILog lg = LogManager.GetLogger(typeof(frmSuaNhanVien));
        String maNv="";
        String matKhau="";
        String queQuan="";
        String SDT="";
        String tenNv="";
        DateTime DoB;
        bool checkQL;
        string ql;
        KetNoiSql k = new KetNoiSql();
        Ngant n = new Ngant();
        List<clsTaiKhoan> ListTaiKhoan = new List<clsTaiKhoan>(); 
        
        public frmSuaNhanVien(String _maNv,String _matKhau,String _queQuan, String _SDT, String _tenNv, DateTime _DoB, bool _checkQL)
        {
            maNv = _maNv;
            matKhau = _matKhau;
            queQuan = _queQuan;
            SDT = _SDT;
            tenNv = _tenNv;
            DoB = _DoB;
            checkQL = _checkQL;
            InitializeComponent();
        }
        private void frmSuaNhanVien_Load(object sender, EventArgs e)
        {

            txtMaNv.Text = maNv;
            txtMatKhau.Text = matKhau;
            txtQueQuan.Text = queQuan;
            txtSDT.Text = SDT;
            txtTenNv.Text = tenNv;
            deDoB.DateTime = DoB;
            cbQuanLy.Checked = checkQL;
        }
        private void btHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btNhapLai_Click(object sender, EventArgs e)
        {
            txtMaNv.ResetText();
            txtMatKhau.ResetText();
            txtQueQuan.ResetText();
            txtSDT.ResetText();
            txtTenNv.ResetText();
            deDoB.ResetText();
            cbQuanLy.Checked = false;
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            try
            {
                ListTaiKhoan = n.List_SelectTaiKhoan();
                maNv=txtMaNv.Text;
                matKhau = fmDangNhap.GetMD5(txtMatKhau.Text);

                queQuan =txtQueQuan.Text;
                SDT=txtSDT.Text;
                tenNv=txtTenNv.Text;
                DoB = DateTime.Parse( deDoB.Text);
                checkQL = cbQuanLy.Checked;
                n.UpdateTaiKhoan(maNv, checkQL);
                n.UpdateNhanVien(maNv, tenNv, queQuan, SDT, DoB);
                MessageBox.Show("Chỉnh sửa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fmMain.eventBus.Publish(new CheckFormClose());
                btNhapLai_Click(sender, e);
            }
            catch (Exception ex)
            {
                lg.Error(ex);
            }
        }
    }
}