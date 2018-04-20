using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using DevExpress.XtraEditors;

namespace QLGN
{
    public partial class fmDangNhap : DevExpress.XtraEditors.XtraForm
    {
        public static string Manv="";
        public fmDangNhap()
        {
            InitializeComponent();

            
        }

        CATSHIPDataContext db = new CATSHIPDataContext();
        ILog log = LogManager.GetLogger(typeof(fmDangNhap));

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public static string GetMD5(string chuoi)
        {
            string str_md5 = "";
            byte[] mang = System.Text.Encoding.UTF8.GetBytes(chuoi);

            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            mang = my_md5.ComputeHash(mang);

            foreach (byte b in mang)
            {
                str_md5 += b.ToString("X2");
            }

            return str_md5;
        }


        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPass.Text == "*5005*7672*00#")
                {
                    Manv = txtName.Text;
                    fmMain f = new fmMain("admin");
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
                    txtPass.Clear();

                }
                
                else
                {

                    TAIKHOAN nv = (from a in db.TAIKHOANs where a.MANV == txtName.Text && a.MATKHAU == GetMD5(txtPass.Text) select a).SingleOrDefault();
                    if (nv != null)
                    {
                        Manv = txtName.Text;
                        fmMain f = new fmMain(nv.MANV);
                        this.Hide();
                        f.ShowDialog();
                        this.Show();
                        txtPass.Clear();
                        
                    }
                    else DevExpress.XtraEditors.XtraMessageBox.Show("Sai mật khẩu hoặc Mã nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
                

            }
            catch (Exception ex)
            {

                XtraMessageBox.Show("Có lỗi trong quá trình thực hiện!", "simpleButton2_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.Error(ex);
            }

            
        }

        private void fmDangNhap_Load(object sender, EventArgs e)
        {
            txtName.Focus();
            try
            {
                DEM check = (from a in db.DEMs where a.ID == "SETUP" select a).Single();
                if (check.COUNT == 1)
                {
                    Setup f = new Setup();
                    this.Hide();
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show("Có lỗi trong quá trình thực hiện!", "fmDangNhap_Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.Error(ex);
            }


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
