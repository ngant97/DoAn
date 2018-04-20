using QLGN.QLGNObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLGN
{
    public class Ngant
    {
        SqlDataHelper dh = new SqlDataHelper();
        DataTable dt = new DataTable();
        #region Insert
        public int InsertNguoiGui(string cmnd, string hoTen, string diaChi, string sdt)
        {
            int r = 0;
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@CMND", cmnd),
                    new SqlParameter("@HOTEN",hoTen),
                    new SqlParameter("@DIACHI", diaChi),
                    new SqlParameter("@SDT", sdt),

                };
                dh.ExecuteNonQuery("usp_NguoiGui_Insert", pa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return r;
        }
        public int InsertNguoiNhan(string cmnd, string hoTen, string diaChi, string sdt)
        {
            int r = 0;
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@CMND", cmnd),
                    new SqlParameter("@HOTEN",hoTen),
                    new SqlParameter("@DIACHI", diaChi),
                    new SqlParameter("@SDT", sdt),

                };
                dh.ExecuteNonQuery("usp_NguoiNhan_Insert", pa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return r;
        }
        public int InsertNhanVien(string maNV, string tennv, string queQuan, string sdt, DateTime dateOfBirth)
        {
            int r = 0;
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@MaNV", maNV),
                    new SqlParameter("@TEN",tennv),
                    new SqlParameter("@QUEQUAN", queQuan),
                    new SqlParameter("@SDTNV", sdt),
                    new SqlParameter("@NGAYSINH", dateOfBirth),
                  
                };
                dh.ExecuteNonQuery("usp_NhanVien_Insert", pa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return r;
        }
        public int InsertShipper(string MaShipper, string TenShipper, string BienSoXe, string SoDienThoai, string MaPhuongTien)
        {
            int r = 0;
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@MASHIPPER", MaShipper),
                    new SqlParameter("@TEN",TenShipper),
                    new SqlParameter("@BSX", BienSoXe),
                    new SqlParameter("@SDT", SoDienThoai),
                    new SqlParameter("@MAPT", MaPhuongTien),

                };
                dh.ExecuteNonQuery("usp_Shipper_Insert", pa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return r;
        }
        public int InsertTaiKhoan(string maNV, string matKhau, bool checkQuanLy)
        {
            int r = 0;
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@MANV", maNV),
                    new SqlParameter("@MATKHAU",matKhau),
                    new SqlParameter("@ADM", checkQuanLy),

                };
                dh.ExecuteNonQuery("usp_TAIKHOAN_Insert", pa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return r;
        }
        public int InsertPhuongTien(string maPhuongTien, string tenPhuongTien, float Phi)
        {
            int r = 0;
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@MAPT", maPhuongTien),
                    new SqlParameter("@TENPT",tenPhuongTien),
                    new SqlParameter("@PHIPT", Phi),

                };
                dh.ExecuteNonQuery("usp_PHUONGTIEN_Insert", pa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return r;
        }
        #endregion
        #region Select 
        public List<clsNhanVien> List_SelectAllNhanVien()

        {
            List<clsNhanVien> list = new List<clsNhanVien>();
            try
            {
                dt = dh.ExecuteDataSet("usp_LoadNhanVien", new SqlParameter[0]).Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        clsNhanVien us = new clsNhanVien
                        {
                            maNV = dr["MANV"] == DBNull.Value ? "" : dr["MANV"].ToString(),
                            tenNV = dr["TEN"] == DBNull.Value ? "" : dr["TEN"].ToString(),
                            dateOfBirth = dr["NGAYSINH"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(dr["NGAYSINH"].ToString()),
                            queQuan = dr["QUEQUAN"] == DBNull.Value ? "" : dr["QUEQUAN"].ToString(),
                            sdt = dr["SDTNV"] == DBNull.Value ? "" : dr["SDTNV"].ToString(),
                            matKhau = dr["MATKHAU"] == DBNull.Value ? "" : dr["MATKHAU"].ToString(),
                            checkQuanLy = dr["ADM"] == DBNull.Value ? false :bool.Parse(dr["ADM"].ToString()),
                        };
                        list.Add(us);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public List<clsPhiPhuongTien> List_SelectPhiGiaoHang()

        {
            List<clsPhiPhuongTien> list = new List<clsPhiPhuongTien>();
            try
            {
                dt = dh.ExecuteDataSet("usp_PhiGH_Select", new SqlParameter[0]).Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        clsPhiPhuongTien us = new clsPhiPhuongTien
                        {
                            MaPhi = dr["MAPHI"] == DBNull.Value ? "" : dr["MAPHI"].ToString(),
                            MaShipper = dr["MASHIPPER"] == DBNull.Value ? "" : dr["MASHIPPER"].ToString(),
                            PhuPhi = dr["PHUPHI"] == DBNull.Value ? 0 : float.Parse(dr["PHUPHI"].ToString()),
                            TongPhi = dr["TONGPHI"] == DBNull.Value ? 0 : float.Parse(dr["TONGPHI"].ToString()),
                            TrangThaiPhi = dr["TTPHI"] == DBNull.Value ? "" : dr["TTPHI"].ToString(),
                            MaPhuongTien = dr["MAPT"] == DBNull.Value ? "" : dr["MAPT"].ToString(),
                        };
                        list.Add(us);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public List<clsShipper> List_SelectShipper()

        {
            List<clsShipper> list = new List<clsShipper>();
            try
            {
                dt = dh.ExecuteDataSet("usp_Shipper_select", new SqlParameter[0]).Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        clsShipper us = new clsShipper
                        {
                            MaShipper = dr["MASHIPPER"] == DBNull.Value ? "" : dr["MASHIPPER"].ToString(),
                            Ten = dr["TEN"] == DBNull.Value ? "" : dr["TEN"].ToString(),
                            BienSoXe = dr["BSX"] == DBNull.Value ? "" : dr["BSX"].ToString(),
                            SoDienThoai = dr["SDT"] == DBNull.Value ? "" : dr["SDT"].ToString(),
                            PhuongTien = dr["TENPT"] == DBNull.Value ? "" : dr["TENPT"].ToString(),
                            MaPhuongTien = dr["MAPT"] == DBNull.Value ? "" : dr["MAPT"].ToString(),
                        };
                        list.Add(us);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public List<clsKhachHang> List_SelectThongTinKhachHang(string CMND)

        {
            List<clsKhachHang> list = new List<clsKhachHang>();
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@CMND", CMND),
                };
                dt = dh.ExecuteDataSet("usp_KhachHang_SelectV2", new SqlParameter[0]).Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        clsKhachHang us = new clsKhachHang
                        {
                            CMNDKhachHang = dr["CMND"] == DBNull.Value ? "" : dr["CMND"].ToString(),
                            HoTen = dr["HOTEN"] == DBNull.Value ? "" : dr["HOTEN"].ToString(),
                            DiaChi = dr["DIACHI"] == DBNull.Value ? "" : dr["DIACHI"].ToString(),
                            SDT = dr["SDT"] == DBNull.Value ? "" : dr["SDT"].ToString(),
                        };
                        list.Add(us);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public List<clsTaiKhoan> List_SelectTaiKhoan()

        {
            List<clsTaiKhoan> list = new List<clsTaiKhoan>();
            try
            {
                dt = dh.ExecuteDataSet("usp_TAIKHOAN_Select", new SqlParameter[0]).Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        clsTaiKhoan us = new clsTaiKhoan
                        {
                            MaNV = dr["MANV"] == DBNull.Value ? "" : dr["MANV"].ToString(),
                            MatKhau = dr["MATKHAU"] == DBNull.Value ? "" : dr["MATKHAU"].ToString(),
                            QuanLy = dr["ADM"] == DBNull.Value ? false : bool.Parse(dr["ADM"].ToString()),
                        };
                        list.Add(us);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public List<clsPhuongTien> List_SelectPhuongTien()

        {
            List<clsPhuongTien> list = new List<clsPhuongTien>();
            try
            {
                dt = dh.ExecuteDataSet("usp_PHUONGTIEN_select", new SqlParameter[0]).Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        clsPhuongTien us = new clsPhuongTien
                        {
                            MaPhuongTien = dr["MAPT"] == DBNull.Value ? "" : dr["MAPT"].ToString(),
                            TenPhuongTien = dr["TENPT"] == DBNull.Value ? "" : dr["TENPT"].ToString(),
                            PhiPhuongTien = dr["PHIPT"] == DBNull.Value ? 0 : float.Parse(dr["PHIPT"].ToString()),
                            SoLuongShipper = dr["SoLuongShipper"] == DBNull.Value ? 0 : Int32.Parse(dr["SoLuongShipper"].ToString()),
                        };
                        list.Add(us);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public List<clsKhachHang> List_SelectKhachHang()

        {
            List<clsKhachHang> list = new List<clsKhachHang>();
            try
            {
                dt = dh.ExecuteDataSet("usp_KhachHang_select", new SqlParameter[0]).Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        clsKhachHang us = new clsKhachHang
                        {
                            CMNDKhachHang = dr["CMND"] == DBNull.Value ? "" : dr["CMND"].ToString(),
                            HoTen = dr["HOTEN"] == DBNull.Value ? "" : dr["HOTEN"].ToString(),
                            DiaChi = dr["DIACHI"] == DBNull.Value ? "" : dr["DIACHI"].ToString(),
                            SDT= dr["SDT"] == DBNull.Value ? "" : dr["SDT"].ToString(),
                        };
                        list.Add(us);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public List<clsKhachHang> List_SelectNguoiGui()

        {
            List<clsKhachHang> list = new List<clsKhachHang>();
            try
            {
                dt = dh.ExecuteDataSet("usp_KhachHang_NguoiGui_select", new SqlParameter[0]).Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        clsKhachHang us = new clsKhachHang
                        {
                            CMNDKhachHang = dr["CMND"] == DBNull.Value ? "" : dr["CMND"].ToString(),
                            HoTen = dr["HOTEN"] == DBNull.Value ? "" : dr["HOTEN"].ToString(),
                            DiaChi = dr["DIACHI"] == DBNull.Value ? "" : dr["DIACHI"].ToString(),
                            SDT = dr["SDT"] == DBNull.Value ? "" : dr["SDT"].ToString(),
                        };
                        list.Add(us);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public List<clsKhachHang> List_SelectNguoiNhan()

        {
            List<clsKhachHang> list = new List<clsKhachHang>();
            try
            {
                dt = dh.ExecuteDataSet("usp_KhachHang_NguoiNhan_select", new SqlParameter[0]).Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        clsKhachHang us = new clsKhachHang
                        {
                            CMNDKhachHang = dr["CMND"] == DBNull.Value ? "" : dr["CMND"].ToString(),
                            HoTen = dr["HOTEN"] == DBNull.Value ? "" : dr["HOTEN"].ToString(),
                            DiaChi = dr["DIACHI"] == DBNull.Value ? "" : dr["DIACHI"].ToString(),
                            SDT = dr["SDT"] == DBNull.Value ? "" : dr["SDT"].ToString(),
                        };
                        list.Add(us);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public List<clsKhachHang> List_SelectKHDK(string TTHD)

        {
            List<clsKhachHang> list = new List<clsKhachHang>();
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@TTDH", TTHD),
                };
                dt = dh.ExecuteDataSet("usp_KhachHangDK_select", pa).Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        clsKhachHang us = new clsKhachHang
                        {
                            CMNDKhachHang = dr["CMND"] == DBNull.Value ? "" : dr["CMND"].ToString(),
                            HoTen = dr["HOTEN"] == DBNull.Value ? "" : dr["HOTEN"].ToString(),
                            DiaChi = dr["DIACHI"] == DBNull.Value ? "" : dr["DIACHI"].ToString(),
                            SDT = dr["SDT"] == DBNull.Value ? "" : dr["SDT"].ToString(),
                        };
                        list.Add(us);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public List<clsKhachHang> List_SelectKHChuaTT(string TTPHI)

        {
            List<clsKhachHang> list = new List<clsKhachHang>();
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@TTPHI", TTPHI),
                };
                dt = dh.ExecuteDataSet("usp_KhachHangChuaThanhToan_select", pa).Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        clsKhachHang us = new clsKhachHang
                        {
                            CMNDKhachHang = dr["CMND"] == DBNull.Value ? "" : dr["CMND"].ToString(),
                            HoTen = dr["HOTEN"] == DBNull.Value ? "" : dr["HOTEN"].ToString(),
                            DiaChi = dr["DIACHI"] == DBNull.Value ? "" : dr["DIACHI"].ToString(),
                            SDT = dr["SDT"] == DBNull.Value ? "" : dr["SDT"].ToString(),
                        };
                        list.Add(us);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public List<clsDonHang> List_SelectDonHang(DateTime TuNgay, DateTime DenNgay)

        {
            List<clsDonHang> list = new List<clsDonHang>();
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@TuNgay", TuNgay.ToString("yyyy/MM/dd") + " 00:00:00"),
                    new SqlParameter("@DenNgay", DenNgay.ToString("yyyy/MM/dd") + " 23:59:59"),
                };
                dt = dh.ExecuteDataSet("usp_DanhSachDonHangTheoNgay_Select", pa).Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        clsDonHang donhang = new clsDonHang
                        {
                            MaDonHang = dr["MADH"] == DBNull.Value ? "" : dr["MADH"].ToString(),
                            TenNhanVien = dr["TEN"] == DBNull.Value ? "" : dr["TEN"].ToString(),
                            NgayGui = dr["NGAYGUI"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(dr["NGAYGUI"].ToString()),
                            NguoiGui = dr["NGUOIGUI"] == DBNull.Value ? "" : dr["NGUOIGUI"].ToString(),
                            NguoiNhan = dr["NGUOINHAN"] == DBNull.Value ? "" : dr["NGUOINHAN"].ToString(),
                            PhuPhi = dr["PHUPHI"] == DBNull.Value ? 0 : float.Parse(dr["PHUPHI"].ToString()),
                            TongPhi = dr["TONGPHI"] == DBNull.Value ? 0 : float.Parse(dr["TONGPHI"].ToString()),
                            PhuongTien = dr["TENPT"] == DBNull.Value ? "" : dr["TENPT"].ToString(),
                            TinhTrangPhi = dr["TTPHI"] == DBNull.Value ? "" : dr["TTPHI"].ToString(),
                            TinhTrangDonHang = dr["TTHD"] == DBNull.Value ? "" : dr["TTHD"].ToString(),
                        };
                        list.Add(donhang);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public List<clsTongDonHang> SelectTongDonHang(DateTime TuNgay, DateTime DenNgay)

        {
            List<clsTongDonHang> list = new List<clsTongDonHang>();
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@TuNgay", TuNgay.ToString("yyyy/MM/dd") + " 00:00:00"),
                    new SqlParameter("@DenNgay", DenNgay.ToString("yyyy/MM/dd") + " 23:59:59"),
                };
                dt = dh.ExecuteDataSet("usp_TongDonHang_Select", pa).Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        clsTongDonHang a = new clsTongDonHang
                        {
                            TongDonHang = dr["TongDonHang"] == DBNull.Value ? "" : dr["TongDonHang"].ToString()
                        };
                        list.Add(a);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public List<clsTongPhi> SelectTongDoanhThu(DateTime TuNgay, DateTime DenNgay)

        {
            List<clsTongPhi> list = new List<clsTongPhi>();
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@TuNgay", TuNgay.ToString("yyyy/MM/dd") + " 00:00:00"),
                    new SqlParameter("@DenNgay", DenNgay.ToString("yyyy/MM/dd") + " 23:59:59"),
                };
                dt = dh.ExecuteDataSet("usp_TongPhi_Select", pa).Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        clsTongPhi a = new clsTongPhi
                        {
                            TongDoanhThu = dr["TONGPHI"] == DBNull.Value ? "" : dr["TONGPHI"].ToString()
                        };
                        list.Add(a);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public List<clsGiaoHangThanhCong> SelectGiaoHangThanhCong(DateTime TuNgay, DateTime DenNgay)

        {
            List<clsGiaoHangThanhCong> list = new List<clsGiaoHangThanhCong>();
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@TuNgay", TuNgay.ToString("yyyy/MM/dd") + " 00:00:00"),
                    new SqlParameter("@DenNgay", DenNgay.ToString("yyyy/MM/dd") + " 23:59:59"),
                };
                dt = dh.ExecuteDataSet("usp_GiaoHangThanhCong_Select", pa).Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        clsGiaoHangThanhCong a = new clsGiaoHangThanhCong
                        {
                            GiaoHangThanhCong = dr["GIAOHANGTHANHCONG"] == DBNull.Value ? "" : dr["GIAOHANGTHANHCONG"].ToString()
                        };
                        list.Add(a);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public List<clsDaHuy> SelectDaHuy(DateTime TuNgay, DateTime DenNgay)

        {
            List<clsDaHuy> list = new List<clsDaHuy>();
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@TuNgay", TuNgay.ToString("yyyy/MM/dd") + " 00:00:00"),
                    new SqlParameter("@DenNgay", DenNgay.ToString("yyyy/MM/dd") + " 23:59:59"),
                };
                dt = dh.ExecuteDataSet("usp_DaHuy_Select", pa).Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        clsDaHuy a = new clsDaHuy
                        {
                            DaHuy = dr["DAHUY"] == DBNull.Value ? "" : dr["DAHUY"].ToString()
                        };
                        list.Add(a);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        #endregion
        #region Update
        public int UpdateTaiKhoan(string maNV, bool checkQuanLy)
        {
            int r = 0;
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@MANV", maNV),
                    new SqlParameter("@ADM", checkQuanLy),

                };
                dh.ExecuteNonQuery("usp_TAIKHOAN_UpdateV2", pa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return r;
        }
        public int UpdateMatKhau(string matKhau,string maNV)
        {
            int r = 0;
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@MATKHAU", matKhau),
                    new SqlParameter("@MANV", maNV),

                };
                dh.ExecuteNonQuery("usp_TAIKHOAN_UpdateMatKhau", pa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return r;
        }
        public int UpdateShipper(string MaShipper, string TenShipper, string BienSoXe, string SoDienThoai, string MaPhuongTien)
        {
            int r = 0;
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@MASHIPPER", MaShipper),
                    new SqlParameter("@TEN",TenShipper),
                    new SqlParameter("@BSX", BienSoXe),
                    new SqlParameter("@SDT", SoDienThoai),
                    new SqlParameter("@MAPT", MaPhuongTien),

                };
                dh.ExecuteNonQuery("usp_Shipper_Update", pa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return r;
        }
        public int UpdateNguoiGui(string cmnd, string hoten, string diachi, string SoDienThoai)
        {
            int r = 0;
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@CMND", cmnd),
                    new SqlParameter("@HOTEN",hoten),
                    new SqlParameter("@DIACHI", diachi),
                    new SqlParameter("@SDT", SoDienThoai)

                };
                dh.ExecuteNonQuery("usp_NguoiGui_Update", pa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return r;
        }
        public int UpdateNguoiNhan(string cmnd, string hoten, string diachi, string SoDienThoai)
        {
            int r = 0;
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@CMND", cmnd),
                    new SqlParameter("@HOTEN",hoten),
                    new SqlParameter("@DIACHI", diachi),
                    new SqlParameter("@SDT", SoDienThoai)

                };
                dh.ExecuteNonQuery("usp_NguoiNhan_Update", pa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return r;
        }
        public int UpdateTaiKhoanV2(string maNV, string matkhau)
        {
            int r = 0;
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@MANV", maNV),
                    new SqlParameter("@MatKhau", matkhau),

                };
                dh.ExecuteNonQuery("usp_TAIKHOAN_UpdateV3", pa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return r;
        }
        public int UpdatePhuongTien (string MaPhuongTien, string TenPhuongTien, float PhiPhuongTien)
        {
            int r = 0;
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@MAPT", MaPhuongTien),
                    new SqlParameter("@TENPT",TenPhuongTien),
                    new SqlParameter("@PHIPT", PhiPhuongTien),

                };
                dh.ExecuteNonQuery("usp_PHUONGTIEN_UPDATE", pa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return r;
        }
        public int UpdateNhanVien(string maNV, string tennv, string queQuan, string sdt, DateTime dateOfBirth)
        {
            int r = 0;
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@MaNV", maNV),
                    new SqlParameter("@TEN",tennv),
                    new SqlParameter("@QUEQUAN", queQuan),
                    new SqlParameter("@SDTNV", sdt),
                    new SqlParameter("@NGAYSINH", dateOfBirth),

                };
                dh.ExecuteNonQuery("usp_NhanVien_Update", pa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return r;
        }
        #endregion
        #region Delete
        public int DeleteTaiKhoan(string maNV)
        {
            int r = 0;
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@MANV", maNV),

                };
                dh.ExecuteNonQuery("usp_TAIKHOAN_Delete", pa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return r;
        }
        public int DeleteNguoiGui(string cmnd)
        {
            int r = 0;
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@CMND", cmnd),

                };
                dh.ExecuteNonQuery("usp_NguoiGui_delete", pa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return r;
        }
        public int DeleteNguoiNhan(string cmnd)
        {
            int r = 0;
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@CMND", cmnd),

                };
                dh.ExecuteNonQuery("usp_NguoiNhan_delete", pa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return r;
        }
        public int DeletePhuongTien(string MaPhuongTien)
        {
            int r = 0;
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@MAPT", MaPhuongTien),

                };
                dh.ExecuteNonQuery("usp_PHUONGTIEN_delete", pa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return r;
        }
        public int DeleteShipper(string MaShipper)
        {
            int r = 0;
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@MASHIPPER", MaShipper),

                };
                dh.ExecuteNonQuery("usp_Shipper_Delete", pa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return r;
        }
        public int DeleteNhanVien(string maNV)
        {
            int r = 0;
            try
            {
                SqlParameter[] pa = new SqlParameter[]
                {
                    new SqlParameter("@MANV", maNV),

                };
                dh.ExecuteNonQuery("usp_NhanVien_Delete", pa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return r;
        }
        #endregion
    }
}
