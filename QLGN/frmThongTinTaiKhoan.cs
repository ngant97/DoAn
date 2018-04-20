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
using System.Data.SqlClient;
using log4net;

namespace QLGN
{
    public partial class frmThongTinTaiKhoan : DevExpress.XtraBars.TabForm
    {
        String maNv = "";
        KetNoiSql k = new KetNoiSql();
        public static SqlConnection cnn = new SqlConnection(QLGN.Properties.Settings.Default.CATSHIPConnectionString);
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        private ILog lg = LogManager.GetLogger(typeof(frmThongTinTaiKhoan));
        public frmThongTinTaiKhoan()
        {
            InitializeComponent();
        }
        public frmThongTinTaiKhoan(String _maNv)
        {
            maNv = _maNv;
            InitializeComponent();
        }
        public static void connect()
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
        }
        public static void disconnect()
        {
            if (cnn.State == ConnectionState.Open)
                cnn.Close();
        }
        private void frmThongTinTaiKhoan_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select NHANVIEN.MANV,NHANVIEN.TEN,NHANVIEN.NGAYSINH,NHANVIEN.QUEQUAN,NHANVIEN.SDTNV,TAIKHOAN.MATKHAU,TAIKHOAN.ADM from NHANVIEN left join TAIKHOAN on NHANVIEN.MANV= TAIKHOAN.MANV where NHANVIEN.MANV='"+maNv+"'";
                connect();
                txtMaNV.DataBindings.Clear();
                ds.Clear();
                da = new SqlDataAdapter(query, cnn);
                da.Fill(ds);
                txtMaNV.DataBindings.Add("Text", ds.Tables[0], "MANV");
                txtTenNV.DataBindings.Add("Text", ds.Tables[0], "TEN");
                deDoB.DataBindings.Add("DateTime", ds.Tables[0], "NGAYSINH");
                txtQueQuan.DataBindings.Add("Text", ds.Tables[0], "QUEQUAN");
                txtSDT.DataBindings.Add("Text", ds.Tables[0], "SDTNV");
                txtMatKhau.DataBindings.Add("Text", ds.Tables[0], "MATKHAU");
                cbQuanLy.DataBindings.Add("Checked", ds.Tables[0], "ADM");
                disconnect();
            }
            catch (Exception ex)
            {
                lg.Error(ex);
            }
           
        }
    }
}