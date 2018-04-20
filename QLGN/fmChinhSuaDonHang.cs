using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Text.RegularExpressions;
using log4net;

namespace QLGN
{
    public partial class fmChinhSuaDonHang : DevExpress.XtraBars.TabForm
    {
            CATSHIPDataContext db = new CATSHIPDataContext();

            ILog log = LogManager.GetLogger(typeof(fmChinhSuaDonHang));

            string madon;

            public fmChinhSuaDonHang(string _dh)
            {

                madon = _dh;

                InitializeComponent();
            }


            private static bool KiemTraSo(string text)
            {
                if (!Regex.IsMatch(text, @"^[0-9]\d*\.?[0]*$")) return false;
                return true;
            }

            private void fmChinhSuaDonHang_Load(object sender, EventArgs e)
            {

                
                    DONHANG dh = (from a in db.DONHANGs where a.MADH == madon select a).Single();



                    NGCMT.Text = dh.NGUOIGUI.CMND;
                    NGTen.Text = dh.NGUOIGUI.HOTEN;
                    NGDiaChi.Text = dh.NGUOIGUI.DIACHI;
                    NGSDT.Text = dh.NGUOIGUI.SDT;

                    NNCMT.Text = dh.NGUOINHAN.CMND;
                    NNDC.Text = dh.NGUOINHAN.DIACHI;
                    NNSDT.Text = dh.NGUOINHAN.SDT;
                    NNTEN.Text = dh.NGUOINHAN.HOTEN;


                    madh.Text = dh.MADH;
                    T2NgayGuiHang.DateTime = dh.NGAYGUI;
                    T2NhanVien.Text = dh.MANV;
                    cbTinhtrangdh.Text = dh.TTHD;
                    cbtinhtrangphi.Text = dh.PHIGH.TTPHI;

                    cbLoaihinhvantai.DataSource = from a in db.PHUONGTIENs select a.TENPT;
                    cbLoaihinhvantai.SelectedItem = dh.PHIGH.PHUONGTIEN.TENPT;

                    cbShipper.DataSource = from a in db.SHIPPERs where a.MAPT == dh.PHIGH.MAPT select a.TEN;
                    cbShipper.SelectedItem = dh.PHIGH.SHIPPER.TEN;

                    
                    T2Noidung.Text = dh.HANG.NOIDUNG;
                    T2Cao.Text = dh.HANG.CAO.ToString();
                    T2Dai.Text = dh.HANG.DAI.ToString();
                    T2Rong.Text = dh.HANG.RONG.ToString();
                    T2TrongLuong.Text = dh.HANG.TRONGLUONG.ToString();
                    T2Ghichu.Text = dh.HANG.GHICHU;

                    phuphi.Text = dh.PHIGH.PHUPHI.ToString();
                    tongtien.Text = String.Format("{0:0,0 vnđ}", dh.PHIGH.TONGPHI);
                    tienchenhlech.Text = String.Format("{0:0,0 vnđ}", dh.PHIGH.TONGPHI);
                

                    
             
            }

            //Tính phí giao hàng
            public double TinhPhi()
            {
                double tl;
                double pp;
                if (T2TrongLuong.Text == "") tl = 0; else tl = double.Parse(T2TrongLuong.Text);
                if (phuphi.Text == "") pp = 0; else pp = double.Parse(phuphi.Text);
                double sum = 0;
                try
                {
                    PHUONGTIEN pt = (from a in db.PHUONGTIENs where a.TENPT == cbLoaihinhvantai.Text select a).Single();
                    sum += tl * pt.PHIPT + pp;
                    
                    
                    tienchenhlech.Text = sum.ToString();
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }

                
                return sum;
            }


          

            private void btHuy_Click(object sender, EventArgs e)
            {
                this.Close();
            }

            private void btLuu_Click(object sender, EventArgs e)
            {
                if (CheckNull() == false)
                {
                    
                        DONHANG dh = (from a in db.DONHANGs where a.MADH == madon select a).Single();
                        //update
                        dh.NGUOIGUI.HOTEN = NGTen.Text;
                        dh.NGUOIGUI.DIACHI = NGDiaChi.Text;
                        dh.NGUOIGUI.SDT = NGSDT.Text;

                        dh.NGUOINHAN.DIACHI = NNDC.Text;
                        dh.NGUOINHAN.SDT = NNSDT.Text;
                        dh.NGUOINHAN.HOTEN = NNTEN.Text ;

                        dh.PHIGH.MAPT = (from a in db.PHUONGTIENs where a.TENPT == cbLoaihinhvantai.Text select a.MAPT).Single();

                        try
                        {
                            SHIPPER sp = (from a in db.SHIPPERs where a.MAPT == dh.PHIGH.MAPT && a.TEN == cbShipper.SelectedItem select a).SingleOrDefault();
                            MessageBox.Show("Tên: " + sp.TEN + "\nMã: " + sp.MASHIPPER);
                            dh.PHIGH.MASHIPPER = sp.MASHIPPER;
                            
                        }
                        catch (Exception er)
                        {
                            MessageBox.Show("sai");
                        }
                        dh.PHIGH.PHUPHI = int.Parse(phuphi.Text);
                        dh.NGAYGUI = T2NgayGuiHang.DateTime;
                        dh.TTHD = cbTinhtrangdh.Text;
                        dh.PHIGH.TTPHI = cbtinhtrangphi.Text;

                        dh.PHIGH.TONGPHI = TinhPhi();

                        dh.HANG.NOIDUNG = T2Noidung.Text;
                        dh.HANG.CAO = Convert.ToDouble(T2Cao.Text);
                        dh.HANG.DAI = Convert.ToDouble(T2Dai.Text);
                        dh.HANG.RONG = Convert.ToDouble(T2Rong.Text);
                        dh.HANG.TRONGLUONG = Convert.ToDouble(T2TrongLuong.Text);
                        dh.HANG.GHICHU = T2Ghichu.Text;


                        db.SubmitChanges();

                        XtraMessageBox.Show("Sửa đơn hàng " + dh.MADH + " thành công!", "Sửa thành công!");

                  
                }
                
            }

            private void txtPhuPhi_TextChanged(object sender, EventArgs e)
            {

            }

            private void groupControl2_Paint(object sender, PaintEventArgs e)
            {

            }

            private void T2TrongLuong_TextChanged(object sender, EventArgs e)
            {
                if (T2TrongLuong.Text != "" && !KiemTraSo(T2TrongLuong.Text)) { ktTL.Visible = true; btLuuDonHang.Enabled = false; }
                else
                {
                    ktTL.Visible = false;
                    btLuuDonHang.Enabled = true;
                    TinhPhi();
                }
            }

            private void phuphi_EditValueChanged(object sender, EventArgs e)
            {
                 //Kiểm tra textbox
                if (phuphi.Text != "" && !KiemTraSo(phuphi.Text)) { ktPhuPhi.Visible = true; btLuuDonHang.Enabled = false; }
                else
                {
                    ktPhuPhi.Visible = false;
                    btLuuDonHang.Enabled = true;
                    TinhPhi();
                }
            }

            private void cbLoaihinhvantai_SelectedIndexChanged(object sender, EventArgs e)
            {
                
                try
                {
                    PHUONGTIEN pt = (from a in db.PHUONGTIENs where a.TENPT == cbLoaihinhvantai.SelectedItem select a).Single();
                    cbShipper.DataSource = from a in db.SHIPPERs where a.MAPT == pt.MAPT select a.TEN;
                }
                catch (Exception)
                {
                    
                    throw;
                }
                
            }

            private void NGSDT_TextChanged(object sender, EventArgs e)
            {
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
                if (T2Dai.Text != "" && !KiemTraSo(T2Dai.Text)) { KTDRC.Visible = true; btLuuDonHang.Enabled = false; }
                else
                {
                    KTDRC.Visible = false;
                    btLuuDonHang.Enabled = true;
                }
            }

            private void T2Rong_TextChanged(object sender, EventArgs e)
            {
                if (T2Rong.Text != "" && !KiemTraSo(T2Rong.Text)) { KTDRC.Visible = true; btLuuDonHang.Enabled = false; }
                else
                {
                    KTDRC.Visible = false;
                    btLuuDonHang.Enabled = true;
                }
            }

            private void T2Cao_TextChanged(object sender, EventArgs e)
            {
                if (T2Cao.Text != "" && !KiemTraSo(T2Cao.Text)) { KTDRC.Visible = true; btLuuDonHang.Enabled = false; }
                else { KTDRC.Visible = false; btLuuDonHang.Enabled = true; }
            }


            bool CheckNull()
            {
                if (NGCMT.Text == "" || NGSDT.Text == "" || NGTen.Text == "" || NGDiaChi.Text == "")
                {
                    XtraMessageBox.Show("Thiếu thông tin người gửi! Vui lòng kiểm tra lại.");
                    return true;
                }

                if (NNCMT.Text == "" || NNSDT.Text == "" || NNTEN.Text == "" || NNDC.Text == "")
                {
                    XtraMessageBox.Show("Thiếu thông tin người nhận! Vui lòng kiểm tra lại.");
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
               
                if (T2Dai.Text == "" || T2Rong.Text == "" || T2Cao.Text == "")
                {
                    XtraMessageBox.Show("Thiếu loại kích thước hàng hóa! Vui lòng kiểm tra lại.");
                    return true;
                }
                return false;
            }
       
    }
}
