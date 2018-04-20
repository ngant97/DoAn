using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QuanLySinhVien;
using Redbus;
using Redbus.Interfaces;
using QLGN.QLGNObj;
using LHMContactCenter;
using System.Globalization;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using log4net;
using System.Text.RegularExpressions;

namespace QLGN
{
    public partial class fmThemDonHang : DevExpress.XtraBars.TabForm
    {
        NHANVIEN nv;
        public fmThemDonHang(NHANVIEN _nv)
        {
            nv = _nv;
            InitializeComponent();
        }

        CATSHIPDataContext db = new CATSHIPDataContext();
        ILog log = LogManager.GetLogger(typeof(fmThemDonHang));

        //Tọa mã hóa đơn 
        public void Code()
        {
            try
            {
                string count = (from a in db.DEMs where a.ID == "DH" select a.COUNT).SingleOrDefault().ToString();
                T2MaDonHang.Text = "DH-" + String.Format("{0:ddMMyyyy}", DateTime.Today) + count;
                btTaoMoi.Enabled = false;
                
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show("Có lỗi trong quá trình thực hiện!", "Code", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.Error(ex);
            }

            //Check
           
          
        }


        private void fmThemDonHang_Load(object sender, EventArgs e)
        {
            try
            {
                XemDonHang(false);
                Code();

                ngaylapdon.DateTime = DateTime.Today;
                T2NhanVien.Text = nv.TEN;
                T2CbLoaiHinhVanTai.DataSource = from a in db.PHUONGTIENs select a.TENPT;

            }
            catch (Exception ex)
            {

                XtraMessageBox.Show("Có lỗi trong quá trình thực hiện!", "fmThemDonHang_Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.Error(ex);
            }

            
        }

       

        //TAB THÊM ĐƠN HÀNG ----------------------------------------------------------------------------------

        


        //Kiểm tra text box
        private static bool KiemTraSo(string text)
        {
            if (!Regex.IsMatch(text, @"^[0-9]\d*\.?[0]*$")) return false;
            return true;
        }

        private void NGCMT_TextChanged(object sender, EventArgs e)
        {
            //Kiểm tra textbox
            if (NGCMT.Text != "" && !KiemTraSo(NGCMT.Text)) { ktNGCMT.Visible = true; btLuuDonHang.Enabled = false; }
            else
            {
                ktNGCMT.Visible = false;
                btLuuDonHang.Enabled = true;

                try
                {
                    NGUOIGUI ng = (from a in db.NGUOIGUIs where NGCMT.Text == a.CMND select a).SingleOrDefault();
                    if (ng == null)
                    {
                        NGTen.Clear();
                        NGDiaChi.Clear();
                        NGSDT.Clear();
                    }
                    else
                    {
                        NGTen.Text = ng.HOTEN;
                        NGDiaChi.Text = ng.DIACHI;
                        NGSDT.Text = ng.SDT;
                    }

                }
                catch (Exception ex)
                {

                    XtraMessageBox.Show("Có lỗi trong quá trình thực hiện!", "NGCMT_TextChanged", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error(ex);
                }


            }

        }

        private void NNCMT_TextChanged(object sender, EventArgs e)
        {
            //Kiểm tra textbox
            if (NNCMT.Text != "" && !KiemTraSo(NNCMT.Text)) { ktNNCMT.Visible = true; btLuuDonHang.Enabled = false; }
            else
            {
                ktNNCMT.Visible = false;
                btLuuDonHang.Enabled = true; 
                try
                {
                    NGUOINHAN ng = (from a in db.NGUOINHANs where NNCMT.Text == a.CMND select a).SingleOrDefault();
                    if (ng == null)
                    {
                        NNTen.Clear();
                        NNDiaChi.Clear();
                        NNSDT.Clear();
                    }
                    else
                    {
                        NNTen.Text = ng.HOTEN;
                        NNDiaChi.Text = ng.DIACHI;
                        NNSDT.Text = ng.SDT;
                    }

                }
                catch (Exception ex)
                {

                    XtraMessageBox.Show("Có lỗi trong quá trình thực hiện!", "NNCMT_TextChanged", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error(ex);
                }

            }
        }
        

        //Tính phí giao hàng
        public double TinhPhi()
        {
            double tl;
            double pp;
            if (T2TrongLuong.Text == "") tl = 0; else tl = double.Parse(T2TrongLuong.Text);
            if (T2PhuPhi.Text == "") pp = 0;else pp  = double.Parse(T2PhuPhi.Text);
            double sum = 0;
            try
            {
                PHUONGTIEN pt = (from a in db.PHUONGTIENs where a.TENPT == T2CbLoaiHinhVanTai.Text select a).Single();
               sum += tl * pt.PHIPT + pp;
                
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show("Có lỗi trong quá trình thực hiện!", "TinhPhi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.Error(ex);
            }


            return sum;
        }

        bool CheckNull()
        {
            if (NGCMT.Text == "" || NGSDT.Text=="" || NGTen.Text=="" ||NGDiaChi.Text=="")
            {
                XtraMessageBox.Show("Thiếu thông tin người gửi! Vui lòng kiểm tra lại.");
                return true;
            }

            if (NNCMT.Text == "" || NNSDT.Text == "" || NNTen.Text == "" || NNDiaChi.Text == "")
            {
                XtraMessageBox.Show("Thiếu thông tin người nhận! Vui lòng kiểm tra lại.");
                return true;
            }

            if (T2MaDonHang.Text=="")
            {
                XtraMessageBox.Show("Thiếu mã đơn hàng! Vui lòng kiểm tra lại.");
                return true;
            }

            if (T2Noidung.Text == "")
            {
                XtraMessageBox.Show("Thiếu nội dung hàng hóa! Vui lòng kiểm tra lại.");
                return true;
            }

            if (T2TrongLuong.Text == "")
            {
                XtraMessageBox.Show("Thiếu trọng lượng hàng hóa! Vui lòng kiểm tra lại.");
                return true;
            }
            if (T2CbLoaiHinhVanTai.Text == "")
            {
                XtraMessageBox.Show("Thiếu loại hình vận tải hàng hóa! Vui lòng kiểm tra lại.");
                return true;
            }
            if(T2Dai.Text=="" || T2Rong.Text=="" || T2Cao.Text=="")
            {
                XtraMessageBox.Show("Thiếu loại kích thước hàng hóa! Vui lòng kiểm tra lại.");
                return true;
            }
            return false;
        }

        //Chặn sửa
        void XemDonHang(bool kt)
        {
            if (kt == true)
            {
                simpleButton1.Enabled = true;
                NNCMT.Enabled = false;
                NNDiaChi.Enabled = false;
                NNSDT.Enabled = false;
                NNTen.Enabled = false;

                NGCMT.Enabled = false;
                NGDiaChi.Enabled = false;
                NGSDT.Enabled = false;
                NGTen.Enabled = false;

                T2Cao.Enabled = false;
                T2Dai.Enabled = false;
                T2Rong.Enabled = false;


                T2Noidung.Enabled = false;
                T2TrongLuong.Enabled = false;
                T2Ghichu.Enabled = false;

                T2PhuPhi.Enabled = false;
                T2CbLoaiHinhVanTai.Enabled = false;

                btLuuDonHang.Enabled = false;
                simpleButton1.Enabled = true;
                btTaoMoi.Enabled = true;
                xuly.Enabled = true;
            }
            else
            {
                xuly.Enabled = false;
                simpleButton1.Enabled = false;
                NNCMT.Enabled = true;
                NNDiaChi.Enabled = true;
                NNSDT.Enabled = true;
                NNTen.Enabled = true;

                NGCMT.Enabled = true;
                NGDiaChi.Enabled = true;
                NGSDT.Enabled = true;
                NGTen.Enabled = true;

                T2Cao.Enabled = true;
                T2Dai.Enabled = true;
                T2Rong.Enabled = true;


                T2Noidung.Enabled = true;
                T2TrongLuong.Enabled = true;
                T2Ghichu.Enabled = true;

                T2PhuPhi.Enabled = true;
                T2CbLoaiHinhVanTai.Enabled = true;

                btLuuDonHang.Enabled = true;
                btTaoMoi.Enabled = false;
            }
        }


        //Lưu đơn hàng
        private void btLuuDonHang_Click(object sender, EventArgs e)
        {
            if (CheckNull() == false)
            {
                //CẬP NHẬT NGƯỜI GỬI
                try
                {
                    NGUOIGUI checkNG = (from a in db.NGUOIGUIs where NGCMT.Text == a.CMND select a).SingleOrDefault();
                    if (checkNG == null)
                    {
                        NGUOIGUI NG = new NGUOIGUI();
                        NG.CMND = NGCMT.Text;
                        NG.DIACHI = NGDiaChi.Text;
                        NG.HOTEN = NGTen.Text;
                        NG.SDT = NGSDT.Text;

                        db.NGUOIGUIs.InsertOnSubmit(NG);
                        db.SubmitChanges();
                    }
                    else
                    {

                        NGUOIGUI NG = (from a in db.NGUOIGUIs where NGCMT.Text == a.CMND select a).SingleOrDefault();
                        NG.DIACHI = NGDiaChi.Text;
                        NG.HOTEN = NGTen.Text;
                        NG.SDT = NGSDT.Text;
                        db.SubmitChanges();

                    }





                    //CẬP NHẬT NGƯỜI NHẬN
                    NGUOINHAN checkNN = (from a in db.NGUOINHANs where NNCMT.Text == a.CMND select a).SingleOrDefault();
                    if (checkNN == null)
                    {
                        NGUOINHAN NN = new NGUOINHAN();
                        NN.CMND = NNCMT.Text;
                        NN.DIACHI = NNDiaChi.Text;
                        NN.HOTEN = NNTen.Text;
                        NN.SDT = NNSDT.Text;

                        db.NGUOINHANs.InsertOnSubmit(NN);
                        db.SubmitChanges();
                    }
                    else
                    {

                        NGUOINHAN NN = (from a in db.NGUOINHANs where NNCMT.Text == a.CMND select a).SingleOrDefault();
                        NN.DIACHI = NNDiaChi.Text;
                        NN.HOTEN = NNTen.Text;
                        NN.SDT = NNSDT.Text;
                        db.SubmitChanges();

                    }




                    //THÊM MẶT HÀNG

                    HANG mathang = new HANG();
                    mathang.MAHANG = T2MaDonHang.Text;
                    mathang.TRONGLUONG = Convert.ToDouble(T2TrongLuong.Text);
                    mathang.DAI = Convert.ToDouble(T2Dai.Text);
                    mathang.RONG = Convert.ToDouble(T2Rong.Text);
                    mathang.CAO = Convert.ToDouble(T2Cao.Text);
                    mathang.NOIDUNG = T2Noidung.Text;
                    mathang.GHICHU = T2Ghichu.Text;

                    db.HANGs.InsertOnSubmit(mathang);
                    db.SubmitChanges();





                    //THÊM PHÍ ĐƠN HÀNG


                    PHIGH phi = new PHIGH();
                    phi.MAPHI = T2MaDonHang.Text;
                    phi.MAPT = (from a in db.PHUONGTIENs where a.TENPT == T2CbLoaiHinhVanTai.Text select a.MAPT).Single();
                    if (T2PhuPhi.Text != "") phi.PHUPHI = double.Parse(T2PhuPhi.Text); else phi.PHUPHI = 0;
                    phi.TONGPHI = TinhPhi();
                    if (T2raNG.Checked) phi.TTPHI = "Đã thanh toán"; else phi.TTPHI = "Chưa thanh toán";

                    db.PHIGHs.InsertOnSubmit(phi);
                    db.SubmitChanges();






                    //THÊM HÓA ĐƠN

                    DONHANG newDonHang = new DONHANG();
                    newDonHang.MADH = T2MaDonHang.Text;
                    newDonHang.MANV = nv.MANV;
                    newDonHang.CMNDNG = NGCMT.Text;
                    newDonHang.CMNDNN = NNCMT.Text;
                    newDonHang.NGAYGUI = DateTime.Today;
                    newDonHang.MAHANG = T2MaDonHang.Text;
                    newDonHang.MAPHI = T2MaDonHang.Text;
                    newDonHang.TTHD = "Chờ vận chuyển";



                    db.DONHANGs.InsertOnSubmit(newDonHang);
                    db.SubmitChanges();


                    DEM dem = (from a in db.DEMs where a.ID == "DH" select a).Single();
                    dem.COUNT++;
                    db.SubmitChanges();

                    XtraMessageBox.Show("Đã thêm đơn hàng " + newDonHang.MADH + " !", "Thêm thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    XemDonHang(true);
                    
                }
                catch (Exception ex)
                {

                    XtraMessageBox.Show("Có lỗi trong quá trình thực hiện!", "btLuuDonHang_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error(ex);
                }

            }

        }

        



        private void btXoaToanBo_Click(object sender, EventArgs e)
        {
            Code();
            XemDonHang(false);

            T2PhuPhi.ResetText();

            NNCMT.ResetText();
            NNDiaChi.ResetText();
            NNSDT.ResetText();
            NNTen.Clear();

            NGCMT.Clear();
            NGDiaChi.Clear();
            NGSDT.Clear();
            NGTen.Clear();

            T2Cao.Clear();
            T2Dai.Clear();
            T2Rong.Clear();


            T2Noidung.Clear();
            T2TrongLuong.Clear();
            T2Ghichu.Clear();

        }



        private void T2CbLoaiHinhVanTai_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtT2PhiGiaoHang.Text = String.Format("{0:0,0 vnđ}",TinhPhi());
        }

        private void T2PhuPhi_TextChanged(object sender, EventArgs e)
        {
            //Kiểm tra textbox
            if (T2PhuPhi.Text != "" && !KiemTraSo(T2PhuPhi.Text)) { ktPhuPhi.Visible = true; btLuuDonHang.Enabled = false; }
            else
            {
                ktPhuPhi.Visible = false;
                txtT2PhiGiaoHang.Text = String.Format("{0:0,0 vnđ}",TinhPhi());
                btLuuDonHang.Enabled = true; 
            }
        }

        private void T2TrongLuong_TextChanged(object sender, EventArgs e)
        {
            //Kiểm tra textbox
            if (T2TrongLuong.Text != "" && !KiemTraSo(T2TrongLuong.Text)) { ktTL.Visible = true; btLuuDonHang.Enabled = false; }
            else
            {
                ktTL.Visible = false;
                txtT2PhiGiaoHang.Text = String.Format("{0:0,0 vnđ}",TinhPhi());
                btLuuDonHang.Enabled = true; 
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                KetNoiSql k = new KetNoiSql();
                string query = "select DONHANG.MADH, DONHANG.NGAYGUI, DONHANG.TTHD, HANG.TRONGLUONG, HANG.DAI, HANG.RONG, HANG.CAO, HANG.NOIDUNG, HANG.GHICHU,NGUOIGUI.CMND, NGUOIGUI.HOTEN, NGUOIGUI.DIACHI, NGUOIGUI.SDT, NGUOINHAN.CMND as NGUOINHAN_CMND, NGUOINHAN.HOTEN as NGUOINHAN_HOTEN, NGUOINHAN.DIACHI as NGUOINHAN_DIACHI, NGUOINHAN.SDT as NGUOINHAN_SDT, NHANVIEN.TEN, NHANVIEN.MANV, PHIGH.PHUPHI, PHIGH.TONGPHI, PHIGH.TTPHI, PHUONGTIEN.TENPT from ((((((dbo.DONHANG DONHANG inner join dbo.HANG HANG on (HANG.MAHANG = DONHANG.MAHANG)) inner join dbo.NGUOIGUI NGUOIGUI on (NGUOIGUI.CMND = DONHANG.CMNDNG)) inner join dbo.NGUOINHAN NGUOINHAN on (NGUOINHAN.CMND = DONHANG.CMNDNN)) inner join dbo.NHANVIEN NHANVIEN on (NHANVIEN.MANV = DONHANG.MANV)) inner join dbo.PHIGH PHIGH on (PHIGH.MAPHI = DONHANG.MAPHI)) inner join dbo.PHUONGTIEN PHUONGTIEN on (PHUONGTIEN.MAPT = PHIGH.MAPT)) where DONHANG.MADH = '" + T2MaDonHang.Text + "'";
                DataTable table = k.KetnoiCSDL_Load(query);

                ReportHoaDon report = new ReportHoaDon();
                report.DataSource = table;
                report.ShowRibbonPreviewDialog();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show("Có lỗi trong quá trình thực hiện!", "simpleButton1_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.Error(ex);
            }

            
        }

        private void NGSDT_TextChanged(object sender, EventArgs e)
        {
            //Kiểm tra textbox
            if (NGSDT.Text != "" && !KiemTraSo(NGCMT.Text)) { ktNGSDT.Visible = true; btLuuDonHang.Enabled = false; }
            else { ktNGSDT.Visible = false; btLuuDonHang.Enabled = true; }
        }

        private void NNSDT_TextChanged(object sender, EventArgs e)
        {
            //Kiểm tra textbox
            if (NNSDT.Text != "" && !KiemTraSo(NNSDT.Text)) { ktNNSDT.Visible = true; btLuuDonHang.Enabled = false; }
            else { ktNNSDT.Visible = false; btLuuDonHang.Enabled = true; }
        }

        private void T2Dai_TextChanged(object sender, EventArgs e)
        {
            //Kiểm tra textbox
            if (T2Dai.Text != "" && !KiemTraSo(T2Dai.Text)) { KTDRC.Visible = true; btLuuDonHang.Enabled = false; }
            else
            {
                KTDRC.Visible = false;
                btLuuDonHang.Enabled = true;
            }
        }

        private void T2Rong_TextChanged(object sender, EventArgs e)
        {
            //Kiểm tra textbox
            if (T2Rong.Text != "" && !KiemTraSo(T2Rong.Text)) { KTDRC.Visible = true; btLuuDonHang.Enabled = false; }
            else {
                KTDRC.Visible = false;
                btLuuDonHang.Enabled = true;
            }
        }

        private void T2Cao_TextChanged(object sender, EventArgs e)
        {
            //Kiểm tra textbox
            if (T2Cao.Text != "" && !KiemTraSo(T2Cao.Text)) { KTDRC.Visible = true; btLuuDonHang.Enabled = false; }
            else { KTDRC.Visible = false; btLuuDonHang.Enabled = true; }
        }

        private void groupControl8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void xuly_Click(object sender, EventArgs e)
        {
           fmXuLyDonHang f = new fmXuLyDonHang(T2MaDonHang.Text);
           f.ShowDialog();
        }

    }
}