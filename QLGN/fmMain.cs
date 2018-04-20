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
using log4net;

namespace QLGN
{
    
    public partial class fmMain : DevExpress.XtraBars.TabForm
    {

//START FORM --> NOT DONE

        CATSHIPDataContext db = new CATSHIPDataContext();
        SubscriptionToken tokenThemNV, tokenSuaNV,tokenThemShipper,tokenSuaShipper,tokenThemKhachHang,tokentSuaKhachHang;
        public static IEventBus eventBus = new EventBus();
        Ngant n = new Ngant();
        List<clsDonHang> ListDonHangThoiGianTuChon = new List<clsDonHang>();
        List<clsTongDonHang> TongDonHang = new List<clsTongDonHang>();
        List<clsTongPhi> TongDoanhThu = new List<clsTongPhi>();
        List<clsGiaoHangThanhCong> GiaoHangThanhCong = new List<clsGiaoHangThanhCong>();
        List<clsDaHuy> DaHuy = new List<clsDaHuy>();
        List<clsPhuongTien> ListPhuongTien = new List<clsPhuongTien>();
        List<clsKhachHang> ListToanBoKhachHang = new List<clsKhachHang>();
        private ILog lg = LogManager.GetLogger(typeof(fmMain));
        NHANVIEN maNV;

        public fmMain(string _maNV)
        {

            maNV = (from a in db.NHANVIENs where a.MANV == _maNV select a).Single();

            InitializeComponent();
            tabFormControl1.SelectedPage = tabTongQuan;
            
        }

       
       


        //HÀM LOAD
            void LoadTTDH()
            {
                var DHChoVanChuyen = from a in db.DONHANGs where a.TTHD == "Chờ vận chuyển" select a;
                var DHDangVanChuyen = from a in db.DONHANGs where a.TTHD == "Đang giao hàng" select a;
                var DHDangTriHoan = from a in db.DONHANGs where a.TTHD == "Đang trì hoãn" select a;
                var DHDangHoanTra = from a in db.DONHANGs where a.TTHD == "Đang hoàn trả" select a;

                slChoVanChuyen.Text = DHChoVanChuyen.Count().ToString();
                slDangGiaoHang.Text = DHDangVanChuyen.Count().ToString();
                slDangTriHoan.Text = DHDangTriHoan.Count().ToString();
                slDangHoanTra.Text = DHDangHoanTra.Count().ToString();
            }

            void LoadQLDH()
            {
                gridQLDH.DataSource = from d in db.DONHANGs
                                      select new
                                      {
                                          MADH = d.MADH,
                                          MANV = d.MANV,
                                          DIACHINN = d.NGUOINHAN.DIACHI,
                                          DIACHING = d.NGUOIGUI.DIACHI,
                                          NGAYGUI = d.NGAYGUI,
                                          PHUONGTIEN = d.PHIGH.PHUONGTIEN.TENPT,
                                          NOIDUNG = d.HANG.NOIDUNG,
                                          GHICHU = d.HANG.GHICHU,
                                          TONGTIEN = d.PHIGH.TONGPHI,
                                          TTDH = d.TTHD,
                                          TTPHI = d.PHIGH.TTPHI,
                                          TENSHIPPER = d.PHIGH.SHIPPER.TEN
                                      };
            }

            void LoadQLHH()
            {
                gridQLHH.DataSource = from a in db.DONHANGs
                                      select new
                                      {
                                          MAHANG = a.HANG.MAHANG,
                                          CMNDNG = a.CMNDNG,
                                          CMNDNN = a.CMNDNN,
                                          DIACHINN = a.NGUOINHAN.DIACHI,
                                          DIACHING = a.NGUOIGUI.DIACHI,
                                          NOIDUNG = a.HANG.NOIDUNG,
                                          TTDH = a.TTHD,
                                          GHICHU = a.HANG.GHICHU
                                      };
            }

            void LoadQLKH()
            {
                string query = "select * from NGUOIGUI union select * from NGUOINHAN";
                DataTable table = k.KetnoiCSDL_Load(query);
                gridQLNV.DataSource = table;
            }

            void LoadPhuongTien()
            {
                ListPhuongTien = n.List_SelectPhuongTien();
                gridPhuongTien.DataSource = ListPhuongTien;
            }

            void LoadToanBoKhachHang()
            {
                ListToanBoKhachHang = n.List_SelectKhachHang();
                gridQLKH.DataSource = ListToanBoKhachHang;

            }
        

        //QUYỀN HẠN NHÂN VIÊN
            void Lock(bool key)
            {
                if (key == true)
                {
                    panelThongBao.Enabled = false;
                    richTextThongBao.ReadOnly = true;
                    TQ_NV.Visible = false;
                    groupChinhSuaPVC.Enabled = false;
                    bBXoaDonHang.Enabled = false;
                    barButtonXoaKhachHang.Enabled = false;
                    tabQuanLyNhanVien.Visible = false;
                    lineShape8.Visible = false;
                    Luuthongbao.Enabled = false;
                    richTextThongBao.ReadOnly = true;
                }
            }
        

        //LOAD ỨNG DỤNG
            private void fmMain_Load(object sender, EventArgs e)
            {
                    
                    //Phần quyền
                    TAIKHOAN check = (from a in db.TAIKHOANs where a.MANV == maNV.MANV select a).SingleOrDefault();
                    if (check.ADM == false) Lock(true);

                    this.Text = "Giao hàng nhanh CATSHIP - Nhân viên: " + maNV.TEN;

                    //LOAD TỔNG QUÁT
                    THONGBAO tb = (from a in db.THONGBAOs select a).SingleOrDefault();
                    richTextThongBao.Text = tb.NOIDUNG;



                    //LOAD TÌNH TRẠNG ĐƠN HÀNG
                    LoadTTDH();


                    //LOAD QLDH
                    LoadQLDH();

                    //LOAD QLHH
                    LoadQLHH();

                    //LOAD QLKH
                    LoadToanBoKhachHang();

                    //LOAD QLNV
                    LoadNhanVien();
                    //Load phương tiện
                    LoadPhuongTien();
                    //Load Shipper
                    LoadShipper();

                    LoadCharDonHang();
            

           

            
            }
        
        //HÀM LOAD CHAR CỦA SHIPPER
            void LoadCharShipper(string masp)
            {
                DataThongKe dt = new DataThongKe();

                SHIPPER sp = (from a in db.SHIPPERs where a.MASHIPPER == masp select a).Single();
                groupControl7.Text = "THỐNG KÊ ĐƠN HÀNG ĐÃ NHẬN "+masp.ToUpper();
                for (int i = 1; i <= 31; i++)
                {
                    var dh = from a in db.DONHANGs where a.NGAYGUI.Day == i && a.NGAYGUI.Month == DateTime.Today.Month && a.PHIGH.MASHIPPER == sp.MASHIPPER select a;
                    dt.THONGKE.AddTHONGKERow(i, dh.Count());

                }

                chartControl1.DataSource = dt;

            }

        //HÀM LOAD CHAR DƠN HÀNG
            void LoadCharDonHang()
            {
                DataThongKe dt = new DataThongKe();


                for (int i = 1; i <= 31; i++)
                {
                    var dh = from a in db.DONHANGs where a.NGAYGUI.Day == i && a.NGAYGUI.Month == DateTime.Today.Month select a;
                    dt.THONGKE.AddTHONGKERow(i, dh.Count());

                }

                chartControl2.DataSource = dt;

            }
    






//TAB TỔNG QUAN -----------------------------------------------------------------------------



            private void home_ItemClick(object sender, ItemClickEventArgs e)
            {
                tabFormControl1.SelectedPage = tabTongQuan;
            }

        
            private void TQ_NG_Click(object sender, EventArgs e)
            {
            
                tabFormControl1.SelectedPage = tabKhachHang;
            }

            private void TQ_HH_Click(object sender, EventArgs e)
            {
                tabFormControl1.SelectedPage = tabQuanLyHang;
            }

            private void TQ_NV_Click(object sender, EventArgs e)
            {
                tabFormControl1.SelectedPage = tabQuanLyNhanVien;
            }

            private void TQ_TaoDH_Click(object sender, EventArgs e)
            {
                fmThemDonHang f = new fmThemDonHang(maNV);
                f.ShowDialog();
                LoadQLDH();
            }

            private void TQ_ThongKE_Click(object sender, EventArgs e)
            {
                tabFormControl1.SelectedPage = tabThongKe;
            }

            private void TQ_QLDH_Click(object sender, EventArgs e)
            {
                tabFormControl1.SelectedPage = tabQuanLyDH;
            }

            private void TQ_PhiVC_Click(object sender, EventArgs e)
            {
                tabFormControl1.SelectedPage = tabPhiVanChuyen;
            }

            private void TQ_NN_Click(object sender, EventArgs e)
            {
                tabFormControl1.SelectedPage = tabKhachHang;
            }






            //PANNEL THÔNG BÁO
            private void Luuthongbao_Click(object sender, EventArgs e)
            {
                THONGBAO tb = (from a in db.THONGBAOs select a).SingleOrDefault();
                tb.NOIDUNG = richTextThongBao.Text;
                db.SubmitChanges();
                XtraMessageBox.Show("Lưu thành công!", "Thông báo");
            }


            private void btHoanTac_Click(object sender, EventArgs e)
            {
                THONGBAO tb = (from a in db.THONGBAOs select a).SingleOrDefault();
                richTextThongBao.Text = tb.NOIDUNG;
            }

            private void btXoaThongBao_Click(object sender, EventArgs e)
            {
                richTextThongBao.Text = "";
            }

    



//TAB THỐNG KÊ --------------------------------------------------------------------------------------- 
// --> NOT DONE
       

        //TÌNH TRẠNG ĐƠN HÀNG --------------------------------------------------------------------- 
            void gridQLDHdata(string tt)
            {
                try
                {
                    gridQLDH.DataSource = from d in db.DONHANGs
                                          where d.TTHD == tt
                                          select new
                                          {
                                              MADH = d.MADH,
                                              MANV = d.MANV,
                                              DIACHINN = d.NGUOINHAN.DIACHI,
                                              DIACHING = d.NGUOIGUI.DIACHI,
                                              NGAYGUI = d.NGAYGUI,
                                              PHUONGTIEN = d.PHIGH.PHUONGTIEN.TENPT,
                                              NOIDUNG = d.HANG.NOIDUNG,
                                              GHICHU = d.HANG.GHICHU,
                                              TONGTIEN = d.PHIGH.TONGPHI,
                                              TTDH = d.TTHD,
                                              TTPHI = d.PHIGH.TTPHI,
                                              TENSHIPPER = d.PHIGH.SHIPPER.TEN
                                          };
                }
                catch (Exception)
                {
                    
                    throw;
                }
                
            }
            //1. BT ĐANG HOÀN TRẢ --------------------------------------------------------------------- 
            private void btDangHoanTra_Click(object sender, EventArgs e)
            {
                gridQLDHdata("Đang hoàn trả");
                tabFormControl1.SelectedPage = tabQuanLyDH;
            }

       
            //2. BT ĐANG TRI HOÃN ---------------------------------------------------------------------
            private void btDangTriHoan_Click(object sender, EventArgs e)
            {
                gridQLDHdata("Đang trì hoãn");
                tabFormControl1.SelectedPage = tabQuanLyDH;
            }
    
        


            //3. BT ĐANG GIAO HÀNG --------------------------------------------------------------------
            private void btDangGiaoHang_Click(object sender, EventArgs e)
            {
                gridQLDHdata("Đang giao hàng");
                tabFormControl1.SelectedPage = tabQuanLyDH;
            }

       

            //4. BT CHỜ VẬN CHUYỂN -------------------------------------------------------------------- 
            private void btChoVanChuyen_Click(object sender, EventArgs e)
            {
                gridQLDHdata("Chờ vận chuyển");
                tabFormControl1.SelectedPage = tabQuanLyDH;
            }



        //THỐNG KÊ ĐƠN HÀNG------------------------------------------------------

            private void radioNgayHomNay_CheckedChanged(object sender, EventArgs e)
            {
                panelChonThoiGian.Enabled = false;
                #region 
                //var TKDH = from a in db.DONHANGs where a.NGAYGUI == DateTime.Today select a;


                //    var DHGiaoHangThanhCong = from a in db.DONHANGs 
                //                              from b in db.PHIGHs
                //                              where a.MAPHI == b.MAPT 
                //                              &&    a.NGAYGUI == DateTime.Today 
                //                              &&    a.TTHD == "Giao hàng thành công"
                //                              select b;


                //    var DHDaHuy = from a in db.DONHANGs 
                //                              where a.NGAYGUI== DateTime.Today && a.TTHD == "Đã hủy"
                //                              select a;




                //    panelChonThoiGian.Enabled = false;

                //    gridThongKeDoanhThu.DataSource = TKDH;

                //    txtTongDonHang.Text = TKDH.Count().ToString();
                //    try
                //    {
                //        txtTongDoanhThu.Text = DHGiaoHangThanhCong.Sum(o => o.TONGPHI).ToString();
                //    }
                //    catch (Exception er)
                //    {
                //        MessageBox.Show(er.Message);  
                //    }

                //    txtSLGiaHangThanhCong.Text = DHGiaoHangThanhCong.Count().ToString();
                //    txtSLDaHuy.Text = DHDaHuy.Count().ToString();

                #endregion
                if (radioNgayHomNay.Checked==true)
                {
                    try
                    {
                        DateTime TuNgay = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"));
                        DateTime DenNgay = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"));
                        ListDonHangThoiGianTuChon = n.List_SelectDonHang(TuNgay, DenNgay);
                        gridThongKeDoanhThu.DataSource = ListDonHangThoiGianTuChon;
                        TongDonHang = n.SelectTongDonHang(TuNgay, DenNgay);
                        txtTongDonHang.Text = TongDonHang[0].TongDonHang.ToString();
                        TongDoanhThu = n.SelectTongDoanhThu(TuNgay, DenNgay);
                        if (TongDoanhThu[0].TongDoanhThu.ToString().Equals(""))
                        {
                            txtTongDoanhThu.Text = "0";
                        }
                        else
                        {
                            int number = Int32.Parse(TongDoanhThu[0].TongDoanhThu.ToString());
                            string whatYouWant = number.ToString("#,##0");
                            txtTongDoanhThu.Text = whatYouWant;
                        }
                         GiaoHangThanhCong = n.SelectGiaoHangThanhCong(TuNgay, DenNgay);
                        txtSLGiaHangThanhCong.Text = GiaoHangThanhCong[0].GiaoHangThanhCong.ToString();
                        DaHuy = n.SelectDaHuy(TuNgay, DenNgay);
                        txtSLDaHuy.Text = DaHuy[0].DaHuy.ToString();
                }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                    
                }
           

        }

            private void radioThangNay_CheckedChanged(object sender, EventArgs e)
            {
                panelChonThoiGian.Enabled = false;
                #region
                //var TKDH = from a in db.DONHANGs where a.NGAYGUI.Month == DateTime.Today.Month select a;

                //panelChonThoiGian.Enabled = false;

                //var DHGiaoHangThanhCong = from a in db.DONHANGs
                //                          from b in db.PHIGHs

                //                          where a.MAPHI == b.MAPT && a.NGAYGUI.Month == DateTime.Today.Month && a.TTHD == "Giao hàng thành công"
                //                          select b;

                //var DHDaHuy = from a in db.DONHANGs
                //              where a.NGAYGUI.Month == DateTime.Today.Month && a.TTHD == "Đã hủy"
                //              select a;


                //panelChonThoiGian.Enabled = false;

                //gridThongKeDoanhThu.DataSource = TKDH;
                //gridView1.RefreshData();
                //txtTongDonHang.Text = TKDH.Count().ToString();
                //txtTongDoanhThu.Text = DHGiaoHangThanhCong.Sum(o => o.TONGPHI).ToString();
                //txtSLGiaHangThanhCong.Text = DHGiaoHangThanhCong.Count().ToString();
                //txtSLDaHuy.Text = DHDaHuy.Count().ToString();
                #endregion
                if (radioThangNay.Checked == true)
                {
                    try
                    {
                        DateTime TuNgay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        DateTime DenNgay = TuNgay.AddMonths(1).AddDays(-1);
                        ListDonHangThoiGianTuChon = n.List_SelectDonHang(TuNgay, DenNgay);
                        gridThongKeDoanhThu.DataSource = ListDonHangThoiGianTuChon;
                        TongDonHang = n.SelectTongDonHang(TuNgay, DenNgay);
                        txtTongDonHang.Text = TongDonHang[0].TongDonHang.ToString();
                        TongDoanhThu = n.SelectTongDoanhThu(TuNgay, DenNgay);
                        if (TongDoanhThu[0].TongDoanhThu.ToString().Equals(""))
                        {
                            txtTongDoanhThu.Text = "0";
                        }
                        else
                        {
                            int number = Int32.Parse(TongDoanhThu[0].TongDoanhThu.ToString());
                            string whatYouWant = number.ToString("#,##0");
                            txtTongDoanhThu.Text = whatYouWant;
                        }
                        GiaoHangThanhCong = n.SelectGiaoHangThanhCong(TuNgay, DenNgay);
                        txtSLGiaHangThanhCong.Text = GiaoHangThanhCong[0].GiaoHangThanhCong.ToString();
                        DaHuy = n.SelectDaHuy(TuNgay, DenNgay);
                        txtSLDaHuy.Text = DaHuy[0].DaHuy.ToString();
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                
                }
            }

            private void radioNamNay_CheckedChanged(object sender, EventArgs e)
                {
                panelChonThoiGian.Enabled = false;
                #region
                //var TKDH = from a in db.DONHANGs where a.NGAYGUI.Year == DateTime.Today.Year select a;

                //    panelChonThoiGian.Enabled = false;

                //    var DHGiaoHangThanhCong = from a in db.DONHANGs
                //                              where a.NGAYGUI.Year == DateTime.Today.Year && a.TTHD == "Giao hàng thành công"
                //                              select a;

                //    var DHDaHuy = from a in db.DONHANGs
                //                  where a.NGAYGUI.Year == DateTime.Today.Year && a.TTHD == "Đã hủy"
                //                  select a;


                //    

                //    gridThongKeDoanhThu.DataSource = TKDH;

                //    txtTongDonHang.Text = TKDH.Count().ToString();
                //    //Lỗi
                //    //txtTongDoanhThu.Text = TKDH.Sum(PHIGH).ToString();
                //    txtSLGiaHangThanhCong.Text = DHGiaoHangThanhCong.Count().ToString();
                //    txtSLDaHuy.Text = DHDaHuy.Count().ToString();
                #endregion
                if (radioNamNay.Checked == true)
                {
                    try
                    {
                        DateTime TuNgay = new DateTime(DateTime.Now.Year, 1, 1);
                        DateTime DenNgay = new DateTime(DateTime.Now.Year, 12, 31);
                        ListDonHangThoiGianTuChon = n.List_SelectDonHang(TuNgay, DenNgay);
                        gridThongKeDoanhThu.DataSource = ListDonHangThoiGianTuChon;
                        TongDonHang = n.SelectTongDonHang(TuNgay, DenNgay);
                        txtTongDonHang.Text = TongDonHang[0].TongDonHang.ToString();
                        TongDoanhThu = n.SelectTongDoanhThu(TuNgay, DenNgay);
                        if (TongDoanhThu[0].TongDoanhThu.ToString().Equals(""))
                        {
                            txtTongDoanhThu.Text = "0";
                        }
                        else
                        {
                            int number = Int32.Parse(TongDoanhThu[0].TongDoanhThu.ToString());
                            string whatYouWant = number.ToString("#,##0");
                            txtTongDoanhThu.Text = whatYouWant;
                        }
                        GiaoHangThanhCong = n.SelectGiaoHangThanhCong(TuNgay, DenNgay);
                        txtSLGiaHangThanhCong.Text = GiaoHangThanhCong[0].GiaoHangThanhCong.ToString();
                        DaHuy = n.SelectDaHuy(TuNgay, DenNgay);
                        txtSLDaHuy.Text = DaHuy[0].DaHuy.ToString();
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
            }

            private void radioTuyChon_CheckedChanged(object sender, EventArgs e)
                {
                    panelChonThoiGian.Enabled = true;
                }

            private void btnXem_Click(object sender, EventArgs e)
            {

                    if ((dateTu.Text == "")|| (dateDen.Text == ""))
                    {
                    MessageBox.Show("Bạn vui lòng chọn ngày để xem!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        DateTime TuNgay = dateTu.DateTime;
                        DateTime DenNgay = dateDen.DateTime;
                        ListDonHangThoiGianTuChon = n.List_SelectDonHang(TuNgay, DenNgay);
                        gridThongKeDoanhThu.DataSource = ListDonHangThoiGianTuChon;
                        TongDonHang = n.SelectTongDonHang(TuNgay, DenNgay);
                        txtTongDonHang.Text = TongDonHang[0].TongDonHang.ToString();
                        TongDoanhThu = n.SelectTongDoanhThu(TuNgay, DenNgay);
                        if (TongDoanhThu[0].TongDoanhThu.ToString().Equals(""))
                        {
                            txtTongDoanhThu.Text = "0";
                        }
                        else
                        {
                            int number = Int32.Parse(TongDoanhThu[0].TongDoanhThu.ToString());
                            string whatYouWant = number.ToString("#,##0");
                            txtTongDoanhThu.Text = whatYouWant;
                        }
                        GiaoHangThanhCong = n.SelectGiaoHangThanhCong(TuNgay, DenNgay);
                        txtSLGiaHangThanhCong.Text = GiaoHangThanhCong[0].GiaoHangThanhCong.ToString();
                        DaHuy = n.SelectDaHuy(TuNgay, DenNgay);
                        txtSLDaHuy.Text = DaHuy[0].DaHuy.ToString();
                    }
                
            }




        //MENU THỐNG KÊ
            private void toolStripMenuItem1_Click(object sender, EventArgs e)
            {
                InDonHang(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString());
            }

            private void toolStripMenuItem2_Click(object sender, EventArgs e)
            {
                fmXuLyDonHang f = new fmXuLyDonHang(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString());
                f.ShowDialog();
                LoadQLDH();
            }

            private void toolStripMenuItem3_Click(object sender, EventArgs e)
            {
                DONHANG dh = (from a in db.DONHANGs
                              where a.MADH == gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0])
                              select a).SingleOrDefault();

                fmChinhSuaDonHang f = new fmChinhSuaDonHang(dh.MADH);
                f.ShowDialog();
                LoadQLDH();
            }

        







//TAB QUẢN LÝ ĐƠN HÀNG ------------------------------------------------------------------------------------------

        //Sự kiện
            private void gridQLDH_DoubleClick(object sender, EventArgs e)
            {
                DONHANG dh = (from a in db.DONHANGs
                              where a.MADH == gridViewQLDH.GetRowCellValue(gridViewQLDH.FocusedRowHandle, gridViewQLDH.Columns[0])
                              select a).SingleOrDefault();

                fmChinhSuaDonHang f = new fmChinhSuaDonHang(dh.MADH);
                f.ShowDialog();
                LoadQLDH();
            }

        //TAB TÌNH TRANG DƠN HÀNG. --> DONE!

            private void bBToanBoDonHang_ItemClick(object sender, ItemClickEventArgs e)
            {
                LoadQLDH();
            }

            private void bBĐHChoVanChuyen_ItemClick(object sender, ItemClickEventArgs e)
            {
                gridQLDHdata("Chờ vận chuyển");
            }

            private void bBDangTriHoan_ItemClick(object sender, ItemClickEventArgs e)
            {
                gridQLDHdata("Đang trì hoãn");
            }

            private void bBĐHDangGiaoHang_ItemClick(object sender, ItemClickEventArgs e)
            {
                gridQLDHdata("Đang giao hàng");
            }

            private void bBĐHHoanTra_ItemClick(object sender, ItemClickEventArgs e)
            {
                gridQLDHdata("Đang hoàn trả");
            }

            private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
            {
                gridQLDHdata("Giao hàng thành công");
            }

            private void bBĐHĐaHuy_ItemClick(object sender, ItemClickEventArgs e)
            {
                gridQLDHdata("Đã hủy");
            }

            private void bBChoNhanHang_ItemClick(object sender, ItemClickEventArgs e)
            {
                gridQLDHdata("Chờ nhận hàng");
            }

     




        //TAB QUẢN LÝ ĐƠN HÀNG

            private void bBTaoDonHang_ItemClick(object sender, ItemClickEventArgs e)
            {
                fmThemDonHang f = new fmThemDonHang(maNV);
                f.ShowDialog();
                LoadQLDH();

            }

            private void bBChinhSuaDonHang_ItemClick(object sender, ItemClickEventArgs e)
            {
                gridQLDH_DoubleClick(sender, e);
                
            }

            private void bBChiTietDonHang_ItemClick(object sender, ItemClickEventArgs e)
            {
                fmXuLyDonHang f = new fmXuLyDonHang(gridViewQLDH.GetRowCellValue(gridViewQLDH.FocusedRowHandle, gridViewQLDH.Columns[0]).ToString());
                f.ShowDialog();
                LoadQLDH();
            }

            private void bBXoaDonHang_ItemClick(object sender, ItemClickEventArgs e)
            {
                DONHANG dh = (from a in db.DONHANGs
                              where a.MADH == gridViewQLDH.GetRowCellValue(gridViewQLDH.FocusedRowHandle, gridViewQLDH.Columns[0])
                              select a).SingleOrDefault();
                DialogResult dia = DevExpress.XtraEditors.XtraMessageBox.Show("Bạn có muốn xóa đơn hàng " + dh.MADH + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                HANG hang = (from a in db.HANGs where a.MAHANG == dh.MADH select a).SingleOrDefault();

                PHIGH phi = (from a in db.PHIGHs where a.MAPHI == dh.MAPHI select a).SingleOrDefault();


                if (dia == DialogResult.Yes)
                {
                    db.DONHANGs.DeleteOnSubmit(dh);
                    db.HANGs.DeleteOnSubmit(hang);
                    db.PHIGHs.DeleteOnSubmit(phi);
                    db.SubmitChanges();
                    DevExpress.XtraEditors.XtraMessageBox.Show("Xóa " + dh.MADH + " thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                
                LoadQLDH();
            }


            //MENU QLDH
            private void inĐơnHàngToolStripMenuItem_Click(object sender, EventArgs e)
            {
                InDonHang(gridViewQLDH.GetRowCellValue(gridViewQLDH.FocusedRowHandle, gridViewQLDH.Columns[0]).ToString());

            }

            private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
            {
                InDonHang(gridViewQLDH.GetRowCellValue(gridViewQLDH.FocusedRowHandle, gridViewQLDH.Columns[0]).ToString());
            }

            private void bBChiTietDonHang_ItemClick(object sender, EventArgs e)
            {
                fmXuLyDonHang f = new fmXuLyDonHang(gridViewQLDH.GetRowCellValue(gridViewQLDH.FocusedRowHandle, gridViewQLDH.Columns[0]).ToString());
                f.ShowDialog();
                LoadQLDH();
            }

            private void chỉnhSửaToolStripMenuItem_Click(object sender, EventArgs e)
            {
                gridQLDH_DoubleClick(sender, e);
            }

            private void xóaĐơnHàngToolStripMenuItem_Click(object sender, EventArgs e)
            {
                DONHANG dh = (from a in db.DONHANGs
                              where a.MADH == gridViewQLDH.GetRowCellValue(gridViewQLDH.FocusedRowHandle, gridViewQLDH.Columns[0])
                              select a).SingleOrDefault();
                DialogResult dia = DevExpress.XtraEditors.XtraMessageBox.Show("Bạn có muốn xóa đơn hàng " + dh.MADH + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                HANG hang = (from a in db.HANGs where a.MAHANG == dh.MADH select a).SingleOrDefault();

                PHIGH phi = (from a in db.PHIGHs where a.MAPHI == dh.MAPHI select a).SingleOrDefault();


                if (dia == DialogResult.Yes)
                {
                    db.DONHANGs.DeleteOnSubmit(dh);
                    db.HANGs.DeleteOnSubmit(hang);
                    db.PHIGHs.DeleteOnSubmit(phi);
                    db.SubmitChanges();
                    DevExpress.XtraEditors.XtraMessageBox.Show("Xóa " + dh.MADH + " thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


                LoadQLDH();
            }
     
   






        //TAB LƯU TRỮ ĐƠN HÀNG 

            private void bBDHXuatEX_ItemClick(object sender, ItemClickEventArgs e)
            {
                try
                {
                    SaveFileDialog save = new SaveFileDialog();
                    save.Filter = "File Excel|*.xlsx";
                    save.Title = "Xuất ra Excel";
                    save.InitialDirectory = @"C:/";
                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        gridQLDH.ExportToXlsx(save.FileName);
                    }
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show("Lỗi! \n" + err.ToString());
                }
            }



            private void bbDHXuatPDF_ItemClick(object sender, ItemClickEventArgs e)
            {
                try
                {
                    SaveFileDialog save = new SaveFileDialog();
                    save.Filter = "File PDF|*.pdf";
                    save.Title = "Xuất ra PDF";
                    save.InitialDirectory = @"C:/";
                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        gridQLDH.ExportToPdf(save.FileName);
                    }
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show("Lỗi! \n" + err.ToString());
                }
                
               

                
            }

          






//TAB HÀNG HÓA ------------------------------------------------------------------------------------------



        //TAB TÌNH TRẠNG HÀNG HÓA

            private void bBHHToanBo_ItemClick(object sender, ItemClickEventArgs e)
            {
                LoadQLDH();
            }
            
            private void bBHHGiaoHangTT_ItemClick(object sender, ItemClickEventArgs e)
            {
                //gridQLHH.DataSource = from a in db.HANGs
                //                      from b in db.DONHANGs
                //                      where a.MAHANG == b.MAHANG && b.TTHD == "Giao hàng thành công"
                                      
                //                      select new
                //                      {
                //                          MAHANG = a.MAHANG,
                //                          CMNDNG = b.CMNDNG,
                //                          CMNDNN = b.CMNDNN,
                //                          DIACHINN = b.DIACHINN,
                //                          DIACHING = b.DIACHING,
                //                          NOIDUNG = a.NOIDUNG,
                //                          TTDH = b.TTHD,
                //                          GHICHU = a.GHICHU

                //                      };
            }

            private void bbHHChoVanChuye_ItemClick(object sender, ItemClickEventArgs e)
            {
                //gridQLHH.DataSource = from a in db.HANGs
                //                      from b in db.DONHANGs
                //                      where a.MAHANG == b.MAHANG && b.TTHD == "Chờ vận chuyển"

                //                      select new
                //                      {
                //                          MAHANG = a.MAHANG,
                //                          CMNDNG = b.CMNDNG,
                //                          CMNDNN = b.CMNDNN,
                //                          DIACHINN = b.DIACHINN,
                //                          DIACHING = b.DIACHING,
                //                          NOIDUNG = a.NOIDUNG,
                //                          TTDH = b.TTHD,
                //                          GHICHU = a.GHICHU

                //                      };
            }

            private void bBDHHDangGiaoHang_ItemClick(object sender, ItemClickEventArgs e)
            {
            //    gridQLHH.DataSource = from a in db.HANGs
            //                          from b in db.DONHANGs
            //                          where a.MAHANG == b.MAHANG && b.TTHD == "Đang giao hàng"

            //                          select new
            //                          {
            //                              MAHANG = a.MAHANG,
            //                              CMNDNG = b.CMNDNG,
            //                              CMNDNN = b.CMNDNN,
            //                              DIACHINN = b.DIACHINN,
            //                              DIACHING = b.DIACHING,
            //                              NOIDUNG = a.NOIDUNG,
            //                              TTDH = b.TTHD,
            //                              GHICHU = a.GHICHU

            //                          };
            }

            private void bBHHHoanTra_ItemClick(object sender, ItemClickEventArgs e)
            {
                //gridQLHH.DataSource = from a in db.HANGs
                //                      from b in db.DONHANGs
                //                      where a.MAHANG == b.MAHANG && b.TTHD == "Đang hoàn trả"

                //                      select new
                //                      {
                //                          MAHANG = a.MAHANG,
                //                          CMNDNG = b.CMNDNG,
                //                          CMNDNN = b.CMNDNN,
                //                          DIACHINN = b.DIACHINN,
                //                          DIACHING = b.DIACHING,
                //                          NOIDUNG = a.NOIDUNG,
                //                          TTDH = b.TTHD,
                //                          GHICHU = a.GHICHU

                //                      };
            }

            private void bBHHDaHuy_ItemClick(object sender, ItemClickEventArgs e)
            {
                //gridQLHH.DataSource = from a in db.HANGs
                //                      from b in db.DONHANGs
                //                      where a.MAHANG == b.MAHANG && b.TTHD == "Đã hủy"

                //                      select new
                //                      {
                //                          MAHANG = a.MAHANG,
                //                          CMNDNG = b.CMNDNG,
                //                          CMNDNN = b.CMNDNN,
                //                          DIACHINN = b.DIACHINN,
                //                          DIACHING = b.DIACHING,
                //                          NOIDUNG = a.NOIDUNG,
                //                          TTDH = b.TTHD,
                //                          GHICHU = a.GHICHU

                //                      };
            }
            


        //TAB QUẢN LÝ HÀNG HÓA

            //Chưa làm
            private void bBHHChinhSua_ItemClick(object sender, ItemClickEventArgs e)
            {

            }

            //Chưa làm
            private void bBHHXemChiTiet_ItemClick(object sender, ItemClickEventArgs e)
            {

            }


        //TAB LƯU TRỮ

            private void bBXuatRaEx_ItemClick(object sender, ItemClickEventArgs e)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "File Excel|*.xlsx";
                save.Title = "Xuất ra Excel";
                save.InitialDirectory = @"C:/";
                save.ShowDialog();

                gridQLHH.ExportToXlsx(save.FileName);
            }

            private void bBXuatRaPDF_ItemClick(object sender, ItemClickEventArgs e)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "File PDF|*.pdf";
                save.Title = "Xuất ra PDF";
                save.InitialDirectory = @"C:/";
                save.ShowDialog();

                gridQLHH.ExportToPdf(save.FileName);
            }

            //Chưa làm
            private void bbXuatTuyChon_ItemClick(object sender, ItemClickEventArgs e)
            {

            }



        //TAB PHƯƠNG TIỆN

        private void btThemPhuongTien_Click(object sender, EventArgs e)
        {
            string MaPhuongTien = txtMaPhuongTien.Text;
            string TenPhuongTien = txtTenPhuongTien.Text;
            float PhiPhuongTien = float.Parse(txtPhiGiaoHang.Text);
            try
            {
                string querycheck = "select count(*) from PHUONGTIEN  where MAPT ='" + MaPhuongTien.Trim() + "'";
                int i = k.Check(querycheck);
                if (i != 0)
                {
                    MessageBox.Show("Mã phương tiện đã tồn tại vui lòng chọn mã khác khác");
                }
                else
                {
                    n.InsertPhuongTien(MaPhuongTien, TenPhuongTien, PhiPhuongTien);
                    MessageBox.Show("Thêm phương tiện thành công!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    LoadPhuongTien();
                    btHuy_Click(sender, e);
                }
                
            }
            catch (Exception ex ) 
            {

                throw ex ;
            }
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            string MaPhuongTien = txtMaPhuongTienCS.Text;
            string TenPhuongTien = txtTenPhuongTienCS.Text;
            float PhiPhuongTien = float.Parse(txtPhiGiaoHangCS.Text);
            n.UpdatePhuongTien(MaPhuongTien, TenPhuongTien, PhiPhuongTien);
            MessageBox.Show("Chỉnh sửa phương tiện thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadPhuongTien();
            btHuy1_Click(sender, e);
        }

        private void btHuy1_Click(object sender, EventArgs e)
        {
            txtMaPhuongTienCS.ResetText();
            txtTenPhuongTienCS.ResetText();
            txtPhiGiaoHangCS.ResetText();
        }

        List<clsPhiPhuongTien> ListPhiGiaoHang = new List<clsPhiPhuongTien>();
        private void btXoaPT_Click(object sender, EventArgs e)
        {
            try
            {
                int dem = 0;
                string MaPhuongTien = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "MaPhuongTien").ToString();
                ListShipper = n.List_SelectShipper();
                ListPhiGiaoHang = n.List_SelectPhiGiaoHang();
                for (int i = 0; i < ListShipper.Count; i++)
                {
                    if (ListShipper[i].MaPhuongTien.Trim().Equals(MaPhuongTien.Trim()))
                    {
                        MessageBox.Show("Đang có shipper sử dụng phương tiện này không thể xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dem++;
                        break;
                    }
                }
                for(int i=0;i<ListPhiGiaoHang.Count;i++)
                {
                    if(MaPhuongTien.Trim().Equals(ListPhiGiaoHang[i].MaPhuongTien.Trim()))
                    {
                        MessageBox.Show("Đang có đơn hàng sử dụng phương tiện này không thể xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dem++;
                        break;
                    }
                }
                if(dem==0)
                {
                    DialogResult dl;
                    dl = MessageBox.Show("Bạn có chắc chắn muốn xóa phương tiện này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dl == DialogResult.Yes)
                    {

                        int check= n.DeletePhuongTien(MaPhuongTien);
                        if(check==0)
                        {
                            MessageBox.Show("Xóa phương tiện thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadPhuongTien();
                            btHuy1_Click(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("Xóa phương tiện thất bại vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                lg.Error(ex);
            }
           
        }
        private void gridView2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string MaPhuongTien = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "MaPhuongTien").ToString();
            string TenPhuongTien = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TenPhuongTien").ToString();
            float PhiPhuongTien = float.Parse(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "PhiPhuongTien").ToString());
            txtMaPhuongTienCS.Text = MaPhuongTien;
            txtTenPhuongTienCS.Text = TenPhuongTien;
            txtPhiGiaoHangCS.Text = PhiPhuongTien.ToString();

        }
        //TAB NHÂN VIÊN 



        //Tab thêm nhân viên
        KetNoiSql k = new KetNoiSql();
        private void LoadNhanVien()
        {
            Ngant n = new Ngant();
            List<clsNhanVien> ListNhanVien = new List<clsNhanVien>();
            ListNhanVien = n.List_SelectAllNhanVien();
            gridQLNV.DataSource = ListNhanVien;
        }

            private void btXoa_Click(object sender, EventArgs e)
            {
            string maNv = gvQLNV.GetRowCellValue(gvQLNV.FocusedRowHandle, "maNV").ToString();
            DialogResult dl;
            if(maNv.Equals("admin"))
            {
                MessageBox.Show("Đây là tài khoản admin không xóa được!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dl = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dl == DialogResult.Yes)
                try
                {
                    n.DeleteNhanVien(maNv);
                    n.DeleteTaiKhoan(maNv);
                        MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadNhanVien();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            tokenThemNV = fmMain.eventBus.Subscribe<InputNhanVien>(s =>
            {
                LoadNhanVien();
            });
            frmThemNhanVien f = new frmThemNhanVien();
            f.ShowDialog();
        }

        private void btThongTinTaiKhoan_ItemClick(object sender, ItemClickEventArgs e)
        {
            string manv = fmDangNhap.Manv;
            frmThongTinTaiKhoan f = new frmThongTinTaiKhoan(manv);
            f.Show();
        }

        private void gvQLNV_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gvQLNV_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle != gvQLNV.FocusedRowHandle && (e.RowHandle % 2 == 0))
                e.Appearance.BackColor = GiaTriV2.RowColor();
        }

        private void gridViewQLDH_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gridViewQLDH_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle != gridViewQLDH.FocusedRowHandle && (e.RowHandle % 2 == 0))
                e.Appearance.BackColor = GiaTriV2.RowColor();
        }

        private void gridView5_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gridView5_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle != gridView5.FocusedRowHandle && (e.RowHandle % 2 == 0))
                e.Appearance.BackColor = GiaTriV2.RowColor();
        }

        private void gridView3_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gridView3_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle != gridView3.FocusedRowHandle && (e.RowHandle % 2 == 0))
                e.Appearance.BackColor = GiaTriV2.RowColor();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle != gridView1.FocusedRowHandle && (e.RowHandle % 2 == 0))
                e.Appearance.BackColor = GiaTriV2.RowColor();
        }
        private void gvShipper_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gvShipper_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle != gvShipper.FocusedRowHandle && (e.RowHandle % 2 == 0))
                e.Appearance.BackColor = GiaTriV2.RowColor();

        }
        private void barButtonItem28_ItemClick(object sender, ItemClickEventArgs e)
        {
            btSua_Click(sender, e);
        }

        private void barButtonItem29_ItemClick(object sender, ItemClickEventArgs e)
        {
            btXoa_Click(sender, e);
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void groupControl5_DoubleClick(object sender, EventArgs e)
        {
            txtMaPhuongTien.Enabled = true;
            txtTenPhuongTien.Enabled = true;
            txtPhiGiaoHang.Enabled = true;
            btThemPhuongTien.Enabled = true;
            btHuy.Enabled = true;
        }

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gridView2_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle != gridView2.FocusedRowHandle && (e.RowHandle % 2 == 0))
                e.Appearance.BackColor = GiaTriV2.RowColor();
        }

        private void T_ItemClick(object sender, ItemClickEventArgs e)
        {
            ListToanBoKhachHang = n.List_SelectNguoiGui();
            gridQLKH.DataSource = ListToanBoKhachHang;
        }

        private void barButtonItem37_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadToanBoKhachHang();
        }

        private void barButtonItem39_ItemClick(object sender, ItemClickEventArgs e)
        {
            ListToanBoKhachHang = n.List_SelectKHDK("Đang giao hàng");
            gridQLKH.DataSource = ListToanBoKhachHang;
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            ListToanBoKhachHang = n.List_SelectNguoiNhan();
            gridQLKH.DataSource = ListToanBoKhachHang;
        }

        private void barButtonItem41_ItemClick(object sender, ItemClickEventArgs e)
        {
            ListToanBoKhachHang = n.List_SelectKHChuaTT("Chưa thanh toán");
            gridQLKH.DataSource = ListToanBoKhachHang;
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            ListToanBoKhachHang = n.List_SelectKHDK("Đang hoàn trả");
            gridQLKH.DataSource = ListToanBoKhachHang;
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            ListToanBoKhachHang = n.List_SelectKHDK("Chờ lấy hàng");
            gridQLKH.DataSource = ListToanBoKhachHang;
        }

        private void barButtonItem42_ItemClick(object sender, ItemClickEventArgs e)
        {
            ListToanBoKhachHang = n.List_SelectKHDK("Đã hủy");
            gridQLKH.DataSource = ListToanBoKhachHang;
        }

        private void btSua_Click(object sender, EventArgs e)
            {
            string maNv = gvQLNV.GetRowCellValue(gvQLNV.FocusedRowHandle, "maNV").ToString();
            string tenNv = gvQLNV.GetRowCellValue(gvQLNV.FocusedRowHandle, "tenNV").ToString();
            DateTime DoB = DateTime.Parse( gvQLNV.GetRowCellValue(gvQLNV.FocusedRowHandle, "dateOfBirth").ToString());
            string queQuan = gvQLNV.GetRowCellValue(gvQLNV.FocusedRowHandle, "queQuan").ToString();
            string sdt = gvQLNV.GetRowCellValue(gvQLNV.FocusedRowHandle, "sdt").ToString();
            bool checkQl = bool.Parse(gvQLNV.GetRowCellValue(gvQLNV.FocusedRowHandle, "checkQuanLy").ToString());
            string matKhau = gvQLNV.GetRowCellValue(gvQLNV.FocusedRowHandle, "matKhau").ToString();
            frmSuaNhanVien f = new frmSuaNhanVien(maNv, matKhau, queQuan, sdt, tenNv, DoB, checkQl);
            f.Show();
            tokenSuaNV = fmMain.eventBus.Subscribe<CheckFormClose>(s =>
            {
                LoadNhanVien();
            });
        }
        List<clsShipper> ListShipper = new List<clsShipper>();
        private void LoadShipper()
        {
            ListShipper = n.List_SelectShipper();
            gcShipper.DataSource = ListShipper;
        }
        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridQLDH_Click(object sender, EventArgs e)
        {

        }

        private void btTKXemChiTiet_Click(object sender, EventArgs e)
        {
            fmXuLyDonHang f = new fmXuLyDonHang(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString());
            f.ShowDialog();
        }

        private void gridThongKeDoanhThu_DoubleClick(object sender, EventArgs e)
        {
            btTKXemChiTiet_Click(sender, e);
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            tokenThemKhachHang = fmMain.eventBus.Subscribe<InputKhachHang>(s =>
            {
                LoadToanBoKhachHang();
            });
            frmThemKhachHang frm = new frmThemKhachHang();
            frm.Show();
        }
        private void btResetMatKhau_Click(object sender, EventArgs e)
        {
            string maNv = gvQLNV.GetRowCellValue(gvQLNV.FocusedRowHandle, "maNV").ToString();
            DialogResult dl;
            dl=MessageBox.Show("Bạn có chắc chắn muốn reset mật khẩu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dl==DialogResult.Yes)
            {
                try
                {
                    n.UpdateTaiKhoanV2(maNv, fmDangNhap.GetMD5("123456"));
                    MessageBox.Show("Reset mật khẩu thành công!\r\nMật khẩu mới của tài khoản là 123456","Thông bao",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    LoadNhanVien();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra liên hệ quản trị viên", "Thông bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lg.Error(ex);
                }
               
            }
        }

        void InDonHang(string madh)
        {
            KetNoiSql k = new KetNoiSql();
            string query = "select DONHANG.MADH, DONHANG.NGAYGUI, DONHANG.TTHD, HANG.TRONGLUONG, HANG.DAI, HANG.RONG, HANG.CAO, HANG.NOIDUNG, HANG.GHICHU,NGUOIGUI.CMND, NGUOIGUI.HOTEN, NGUOIGUI.DIACHI, NGUOIGUI.SDT, NGUOINHAN.CMND as NGUOINHAN_CMND, NGUOINHAN.HOTEN as NGUOINHAN_HOTEN, NGUOINHAN.DIACHI as NGUOINHAN_DIACHI, NGUOINHAN.SDT as NGUOINHAN_SDT, NHANVIEN.TEN, NHANVIEN.MANV, PHIGH.PHUPHI, PHIGH.TONGPHI, PHIGH.TTPHI, PHUONGTIEN.TENPT from ((((((dbo.DONHANG DONHANG inner join dbo.HANG HANG on (HANG.MAHANG = DONHANG.MAHANG)) inner join dbo.NGUOIGUI NGUOIGUI on (NGUOIGUI.CMND = DONHANG.CMNDNG)) inner join dbo.NGUOINHAN NGUOINHAN on (NGUOINHAN.CMND = DONHANG.CMNDNN)) inner join dbo.NHANVIEN NHANVIEN on (NHANVIEN.MANV = DONHANG.MANV)) inner join dbo.PHIGH PHIGH on (PHIGH.MAPHI = DONHANG.MAPHI)) inner join dbo.PHUONGTIEN PHUONGTIEN on (PHUONGTIEN.MAPT = PHIGH.MAPT)) where DONHANG.MADH = '" +madh+ "'";
            DataTable table = k.KetnoiCSDL_Load(query);
            ReportHoaDon report = new ReportHoaDon();
            report.DataSource = table;
            report.ShowRibbonPreviewDialog();

        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {

        }
        List<clsKhachHang> ListNguoiNhan = new List<clsKhachHang>();
        List<clsKhachHang> ListNguoiGui = new List<clsKhachHang>();
        private void btXoaShipper_Click(object sender, EventArgs e)
        {
            try
            {
                string mashipper = gvShipper.GetRowCellValue(gvShipper.FocusedRowHandle, "MaShipper").ToString();
                DialogResult dl;
                dl = MessageBox.Show("Bạn có chắc chắn muốn xóa Shipper này không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dl == DialogResult.Yes)
                    try
                    {
                        int check = n.DeleteShipper(mashipper);
                        if (check == 0)
                        {
                            MessageBox.Show("Xóa shipper thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadToanBoKhachHang();
                        }
                        else
                        {
                            MessageBox.Show("Xóa shipper thất bại vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }
                    catch (Exception ex)
                    {
                        lg.Error(ex);
                    }
            }
            catch (Exception ex)
            {
                lg.Error(ex);
            }
            
        }

        private void btSuaKH_Click(object sender, EventArgs e)
        {
            try
            {
                string cmnd = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "CMNDKhachHang").ToString();
                string hoTen = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "HoTen").ToString();
                string diaChi = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "DiaChi").ToString();
                string soDienThoai = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "SDT").ToString();
                frmSuaKhachHang f = new frmSuaKhachHang(cmnd, hoTen, diaChi, soDienThoai);
                f.Show();
                tokenSuaShipper = fmMain.eventBus.Subscribe<EditKhachHang>(s =>
                {
                    LoadToanBoKhachHang();
                });
            }
            catch (Exception ex)
            {
                lg.Error(ex);
            }
        }

        private void btXoaKH_Click(object sender, EventArgs e)
        {
            try
            {
                string cmnd = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "CMNDKhachHang").ToString();
                DialogResult dl;
                dl = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dl == DialogResult.Yes)
                    try
                    {
                        int checkNguoiNhan = 1, checkNguoiGui = 1;
                        ListNguoiNhan = n.List_SelectNguoiNhan();
                        for (int i = 0; i < ListNguoiNhan.Count; i++)
                        {
                            if (ListNguoiNhan[i].CMNDKhachHang.Trim().Equals(cmnd))
                            {
                                checkNguoiNhan = n.DeleteNguoiNhan(cmnd);
                                break;
                            }
                        }
                        ListNguoiGui = n.List_SelectNguoiGui();
                        for (int i = 0; i < ListNguoiGui.Count; i++)
                        {
                            if (ListNguoiGui[i].CMNDKhachHang.Trim().Equals(cmnd))
                            {
                                checkNguoiGui = n.DeleteNguoiGui(cmnd);
                                break;
                            }
                        }
                        if (checkNguoiGui == 0 || checkNguoiNhan == 0)
                        {
                            MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadToanBoKhachHang();
                        }
                        else
                        {
                            MessageBox.Show("Xóa khách hàng thất bại vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }
                    catch (Exception ex)
                    {
                        lg.Error(ex);
                    }
            }
            catch (Exception ex)
            {
                lg.Error(ex);
            }
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            btSuaKH_Click(sender, e);
        }

        private void barButtonXoaKhachHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            btXoaKH_Click(sender, e);
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmDoiMatKhau frm = new frmDoiMatKhau(fmDangNhap.Manv);
            frm.Show();
        }

        private void barButtonItem43_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                string maShipper = gvShipper.GetRowCellValue(gvShipper.FocusedRowHandle, "MaShipper").ToString();
                string tenShipper = gvShipper.GetRowCellValue(gvShipper.FocusedRowHandle, "Ten").ToString();
                string bienSoXe = gvShipper.GetRowCellValue(gvShipper.FocusedRowHandle, "BienSoXe").ToString();
                string soDienThoai = gvShipper.GetRowCellValue(gvShipper.FocusedRowHandle, "SoDienThoai").ToString();
                string maPhuongTien = gvShipper.GetRowCellValue(gvShipper.FocusedRowHandle, "MaPhuongTien").ToString();
                frmSuaShipper f = new frmSuaShipper(maShipper, tenShipper, bienSoXe, soDienThoai, maPhuongTien);
                f.Show();
                tokenSuaShipper = fmMain.eventBus.Subscribe<EditShipper>(s =>
                {
                    LoadShipper();
                });
            }
            catch (Exception ex)
            {
                lg.Error(ex);
            }
            
        }

        private void barButtonItem44_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                string maShipper = gvShipper.GetRowCellValue(gvShipper.FocusedRowHandle, "MaShipper").ToString();
                DialogResult dl;
                dl = MessageBox.Show("Bạn có chắc chắn muốn xóa shipper này không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dl == DialogResult.Yes)
                    try
                    {
                       int check= n.DeleteShipper(maShipper);
                        if(check==0)
                        {
                            MessageBox.Show("Xóa Shipper thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadShipper();
                        }
                        else
                        {
                            MessageBox.Show("Xóa Shipper thất bại vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        lg.Error(ex);
                    }
            }
            catch (Exception ex)
            {
                lg.Error(ex);
            }
            
        }

        private void gvShipper_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            LoadCharShipper(gvShipper.GetRowCellValue(gvShipper.FocusedRowHandle, gvShipper.Columns[0]).ToString());
        }

        private void xóaĐơnHàngToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DONHANG dh = (from a in db.DONHANGs
                          where a.MADH == gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0])
                          select a).SingleOrDefault();
            DialogResult dia = DevExpress.XtraEditors.XtraMessageBox.Show("Bạn có muốn xóa đơn hàng " + dh.MADH + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            HANG hang = (from a in db.HANGs where a.MAHANG == dh.MADH select a).SingleOrDefault();

            PHIGH phi = (from a in db.PHIGHs where a.MAPHI == dh.MAPHI select a).SingleOrDefault();


            if (dia == DialogResult.Yes)
            {
                db.DONHANGs.DeleteOnSubmit(dh);
                db.HANGs.DeleteOnSubmit(hang);
                db.PHIGHs.DeleteOnSubmit(phi);
                db.SubmitChanges();
                DevExpress.XtraEditors.XtraMessageBox.Show("Xóa " + dh.MADH + " thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


            LoadQLDH();
        }

        private void barButtonItem47_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            tokenThemShipper = fmMain.eventBus.Subscribe<InputShipper>(s =>
            {
                LoadShipper();
            });
            frmThemShipper frm = new frmThemShipper();
            frm.Show();
        }

        private void btHuy_Click(object sender, EventArgs e)
        {

        }


        //Load char shipper
      

 














    }
}
