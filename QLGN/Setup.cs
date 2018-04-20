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
using System.Configuration;
using System.Data.SqlClient;
using log4net;
using System.Security.Cryptography;

namespace QLGN
{

    public partial class Setup : DevExpress.XtraEditors.XtraForm
    {
        CATSHIPDataContext db = new CATSHIPDataContext();
        ILog log = LogManager.GetLogger(typeof(Setup));


        public Setup()
        {
            InitializeComponent();
            try
            {
                PHUONGTIEN cXD = (from a in db.PHUONGTIENs where a.MAPT == "XD" select a).SingleOrDefault();
                if (cXD != null) xedap.CheckState = CheckState.Checked;

                PHUONGTIEN cXM = (from a in db.PHUONGTIENs where a.MAPT == "XM" select a).SingleOrDefault();
                if (cXM != null) xemay.CheckState = CheckState.Checked;

                PHUONGTIEN cXT = (from a in db.PHUONGTIENs where a.MAPT == "XT" select a).SingleOrDefault();
                if (cXT != null) xetai.CheckState = CheckState.Checked;

                PHUONGTIEN cTT = (from a in db.PHUONGTIENs where a.MAPT == "TT" select a).SingleOrDefault();
                if (cTT != null) tauthuy.CheckState = CheckState.Checked;

                PHUONGTIEN cMB = (from a in db.PHUONGTIENs where a.MAPT == "MB" select a).SingleOrDefault();
                if (cMB != null) maybay.CheckState = CheckState.Checked;

                LoadPhuongTien();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show("Có lỗi trong quá trình thực hiện!", "Setup_Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.Error(ex);
            }
        }


        private void Setup_Load(object sender, EventArgs e)
        {
            NHANVIEN qt = (from a in db.NHANVIENs where a.MANV == "admin" select a).Single();
            ht.Text = qt.TEN;
            ns.DateTime = qt.NGAYSINH;
            qq.Text = qt.QUEQUAN;
            sdt.Text = qt.SDTNV;
            
        }

        //
        //Trang 1
        //

            private void checkBox1_CheckedChanged(object sender, EventArgs e)
            {
                if (checkBox1.Checked) panel1.Enabled = true; else panel1.Enabled = false;
            }

            private void simpleButton3_Click(object sender, EventArgs e)
            {

                try
                {
                    marqueeProgressBarControl1.Visible = true;
                    ChangeConnectionString(Connection(sever.Text, "CATSHIP"));
                    SqlConnection con = new SqlConnection(QLGN.Properties.Settings.Default.CATSHIPConnectionString);
                    con.Open();
                    XtraMessageBox.Show("Kết nối thành công!");
                    con.Close();
                    PageCSDL.AllowNext = true;
                    wizardControl1.SelectedPage = pageTK;
                }
                catch (Exception)
                {
                    XtraMessageBox.Show("Lỗi kết nối!");
                    PageCSDL.AllowNext = false;

                }
                finally
                {
                    marqueeProgressBarControl1.Visible = false;
                }
            }


        //
        //Trang 2
        //
            private void simpleButton1_Click(object sender, EventArgs e)
            {
                frmThemNhanVien f = new frmThemNhanVien();
                DialogResult dia = f.ShowDialog();
                if (dia == DialogResult.OK)
                    PageTNV.AllowNext = true;
            }

        //
        //Trang 3
        //
            //
            //Thêm phương tiện giao hàng
            //
            public void LoadPhuongTien()
            {
                cbPhuongTien.DataSource = from a in db.PHUONGTIENs select a.TENPT;
            }

            private void xedap_CheckedChanged(object sender, EventArgs e)
            {
                try
                {
                    if (xedap.Checked)
                    {
                        PHUONGTIEN xd = new PHUONGTIEN();
                        xd.MAPT = "XD";
                        xd.TENPT = "Xe đạp";
                        xd.PHIPT = 0;
                        db.PHUONGTIENs.InsertOnSubmit(xd);
                        db.SubmitChanges();
                        LoadPhuongTien();
                    }
                    else
                    {
                        PHUONGTIEN check = (from a in db.PHUONGTIENs where a.MAPT == "XD" select a).SingleOrDefault();
                        var cPGH = from a in db.PHIGHs where a.MAPT == "XD" select a;
                        var cSP = from a in db.SHIPPERs where a.MAPT == "XD" select a;
                        if (check != null && cPGH.Count() == 0 && cSP.Count() == 0)
                        {
                            db.PHUONGTIENs.DeleteOnSubmit(check);
                            db.SubmitChanges();
                            LoadPhuongTien();
                        }
                        else XtraMessageBox.Show("Không thể xóa phương tiện vì phương tiện đang quản lý Shipper hoặc Đơn hàng nào đó!\nHãy chắc chắn rằng phương tiện này không sử dụng","Lỗi liên kết",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    }
                }
                catch (Exception ex)
                {

                    XtraMessageBox.Show("Có lỗi trong quá trình thực hiện!", "xedap_CheckedChanged", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error(ex);
                }
               
            }

            private void xemay_CheckedChanged(object sender, EventArgs e)
            {
                try
                {
                    if (xemay.Checked)
                    {
                        PHUONGTIEN pt = new PHUONGTIEN();
                        pt.MAPT = "XM";
                        pt.TENPT = "Xe máy";
                        pt.PHIPT = 0;
                        db.PHUONGTIENs.InsertOnSubmit(pt);
                        db.SubmitChanges();
                        LoadPhuongTien();
                    }
                    else
                    {
                        PHUONGTIEN check = (from a in db.PHUONGTIENs where a.MAPT == "XM" select a).SingleOrDefault();
                        var cPGH = from a in db.PHIGHs where a.MAPT == "XM" select a;
                        var cSP = from a in db.SHIPPERs where a.MAPT == "XM" select a;
                        if (check != null && cPGH.Count() == 0 && cSP.Count() == 0)
                        {
                            db.PHUONGTIENs.DeleteOnSubmit(check);
                            db.SubmitChanges();
                            LoadPhuongTien();
                        }
                        else
                        {
                            XtraMessageBox.Show("Không thể xóa phương tiện vì phương tiện đang quản lý Shipper hoặc Đơn hàng nào đó!\nHãy chắc chắn rằng phương tiện này không sử dụng", "Lỗi liên kết", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            xemay.Checked = true;
                        }
                    }
                }
                catch (Exception ex)
                {

                    XtraMessageBox.Show("Có lỗi trong quá trình thực hiện!", "xemay_CheckedChanged", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error(ex);
                }
                
            }

            private void xetai_CheckedChanged(object sender, EventArgs e)
            {
                try
                {
                    if (xetai.Checked)
                    {
                        PHUONGTIEN pt = new PHUONGTIEN();
                        pt.MAPT = "XT";
                        pt.TENPT = "Xe tải";
                        pt.PHIPT = 0;
                        db.PHUONGTIENs.InsertOnSubmit(pt);
                        db.SubmitChanges();
                        LoadPhuongTien();
                    }
                    else
                    {
                        PHUONGTIEN check = (from a in db.PHUONGTIENs where a.MAPT == "XT" select a).SingleOrDefault();
                        var cPGH = from a in db.PHIGHs where a.MAPT == "XT" select a;
                        var cSP = from a in db.SHIPPERs where a.MAPT == "XT" select a;
                        if (check != null && cPGH.Count() == 0 && cSP.Count() == 0)
                        {
                            db.PHUONGTIENs.DeleteOnSubmit(check);
                            db.SubmitChanges();
                            LoadPhuongTien();
                        }
                        else
                        {
                            XtraMessageBox.Show("Không thể xóa phương tiện vì phương tiện đang quản lý Shipper hoặc Đơn hàng nào đó!\nHãy chắc chắn rằng phương tiện này không sử dụng", "Lỗi liên kết", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            xetai.Checked = true;
                        }
                    }
                }
                catch (Exception ex)
                {

                    XtraMessageBox.Show("Có lỗi trong quá trình thực hiện!", "xetai_CheckedChanged", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error(ex);
                }
                
            }

            private void tauthuy_CheckedChanged(object sender, EventArgs e)
            {
                try
                {
                    if (tauthuy.Checked)
                    {
                        PHUONGTIEN pt = new PHUONGTIEN();
                        pt.MAPT = "TT";
                        pt.TENPT = "Tàu thủy";
                        pt.PHIPT = 0;
                        db.PHUONGTIENs.InsertOnSubmit(pt);
                        db.SubmitChanges();
                        LoadPhuongTien();
                    }
                    else
                    {
                        PHUONGTIEN check = (from a in db.PHUONGTIENs where a.MAPT == "TT" select a).SingleOrDefault();
                        var cPGH = from a in db.PHIGHs where a.MAPT == "TT" select a;
                        var cSP = from a in db.SHIPPERs where a.MAPT == "TT" select a;
                        if (check != null && cPGH.Count() == 0 && cSP.Count() == 0)
                        {
                            db.PHUONGTIENs.DeleteOnSubmit(check);
                            db.SubmitChanges();
                            LoadPhuongTien();
                        }
                        else
                        {
                            XtraMessageBox.Show("Không thể xóa phương tiện vì phương tiện đang quản lý Shipper hoặc Đơn hàng nào đó!\nHãy chắc chắn rằng phương tiện này không sử dụng", "Lỗi liên kết", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            tauthuy.Checked = true;
                        }
                    }
                }
                catch (Exception ex)
                {

                    XtraMessageBox.Show("Có lỗi trong quá trình thực hiện!", "tauthuy_CheckedChanged", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error(ex);
                }
                
            }

            private void maybay_CheckedChanged(object sender, EventArgs e)
            {
                try
                {
                    if (maybay.Checked)
                    {
                        PHUONGTIEN pt = new PHUONGTIEN();
                        pt.MAPT = "MB";
                        pt.TENPT = "Máy bay";
                        pt.PHIPT = 0;
                        db.PHUONGTIENs.InsertOnSubmit(pt);
                        db.SubmitChanges();
                        LoadPhuongTien();
                    }
                    else
                    {
                        PHUONGTIEN check = (from a in db.PHUONGTIENs where a.MAPT == "MB" select a).SingleOrDefault();
                        var cPGH = from a in db.PHIGHs where a.MAPT == "MB" select a;
                        var cSP = from a in db.SHIPPERs where a.MAPT == "MB" select a;
                        if (check != null && cPGH.Count() == 0 && cSP.Count() == 0)
                        {
                            db.PHUONGTIENs.DeleteOnSubmit(check);
                            db.SubmitChanges();
                            LoadPhuongTien();
                        }
                        else
                        {
                            XtraMessageBox.Show("Không thể xóa phương tiện vì phương tiện đang quản lý Shipper hoặc Đơn hàng nào đó!\nHãy chắc chắn rằng phương tiện này không sử dụng", "Lỗi liên kết", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            maybay.Checked = true;
                        }
                    }
                }
                catch (Exception ex)
                {

                    XtraMessageBox.Show("Có lỗi trong quá trình thực hiện!", "maybay_CheckedChanged", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error(ex);
                }
                
            }

            private void cbPhuongTien_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    PHUONGTIEN pt = (from a in db.PHUONGTIENs where a.TENPT == cbPhuongTien.Text select a).Single();
                    phigiaohang.Text = pt.PHIPT.ToString();
                }
                catch (Exception ex)
                {

                    XtraMessageBox.Show("Có lỗi trong quá trình thực hiện!", "cbPhuongTien_SelectedIndexChanged", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error(ex);
                }
                
            }



            private void luuphuongtien_Click(object sender, EventArgs e)
            {
                try
                {
                    PHUONGTIEN pt = (from a in db.PHUONGTIENs where a.TENPT == cbPhuongTien.Text select a).Single();
                    pt.PHIPT = float.Parse(phigiaohang.Text);
                    db.SubmitChanges();
                    XtraMessageBox.Show("Đã lưu phương tiện " + pt.TENPT + " thành công!");
                }
                catch (Exception ex)
                {

                    XtraMessageBox.Show("Có lỗi trong quá trình thực hiện!", "luuphuongtien_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error(ex);
                }

            }
            
        //
        //Trang 4
        //
            
 
            public string GetMD5(string chuoi)
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


            private void luunv_Click(object sender, EventArgs e)
            {
                try
                {
                    NHANVIEN qt = (from a in db.NHANVIENs where a.MANV == "admin" select a).Single();
                    qt.TAIKHOAN.MATKHAU = GetMD5(mk.Text);
                    qt.TEN = ht.Text;
                    qt.QUEQUAN = qq.Text;
                    qt.SDTNV = sdt.Text;
                    qt.NGAYSINH = ns.DateTime;

                    db.SubmitChanges();

                    wizardControl1.SelectedPage = PageTNV;
                }
                catch (Exception ex)
                {

                    XtraMessageBox.Show("Có lỗi trong quá trình thực hiện!", "luunv_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error(ex);
                }
            }

        //
        //Trang 5
        //
      

        


      
        private void wizardControl1_FinishClick(object sender, CancelEventArgs e)
        {

            try
            {
                DEM d = (from a in db.DEMs where a.ID == "SETUP" select a).Single();
                d.COUNT = 0;
                db.SubmitChanges();
            }
            catch (Exception er)
            {

                MessageBox.Show(er.ToString());
            }

            Application.Restart();
        }

        private void wizardControl1_CancelClick(object sender, CancelEventArgs e)
        {
            this.Close();
            Application.ExitThread();
        }


        //CHỈNH SỬA SQL TRONG FILE CONFIG

        public static string Connection(string datasource, string database)
        {
            //
            // Data Source=;Initial Catalog=CATSHIP;Integrated Security=True
            //
            string connString = "Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Integrated Security=True";

           return connString;


        }

        public static void ChangeConnectionString(string strConn)
        {
            Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //the full name of the connection string can be found in the app.config file
            // in the "name" attribute of the connection string
            _config.ConnectionStrings.ConnectionStrings["QLGN.Properties.Settings.CATSHIPConnectionString"].ConnectionString = strConn;

            //Save to file
            _config.Save(ConfigurationSaveMode.Modified);

            //force changes to take effect so that we can start using
            //this new connection string immediately
            ConfigurationManager.RefreshSection(_config.ConnectionStrings.SectionInformation.Name);
            Properties.Settings.Default.Reload();
        }



       


        




       

       
       
       

        

       

        

      
    }
}