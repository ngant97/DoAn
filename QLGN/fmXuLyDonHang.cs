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

namespace QLGN
{
    public partial class fmXuLyDonHang : DevExpress.XtraBars.TabForm
    {
        string madh;
        CATSHIPDataContext db = new CATSHIPDataContext();

        public fmXuLyDonHang(string _madh)
        {
            madh = _madh;
            InitializeComponent();
        }

        private void fmXemChiTietDonHang_Load(object sender, EventArgs e)
        {
            DONHANG dh = (from a in db.DONHANGs where a.MADH == madh select a).Single();
            ChonShipper.DataSource = (from a in db.SHIPPERs where a.MAPT == dh.PHIGH.MAPT select a.TEN);
            ChonShipper.Text = "Chọn Shipper";


            labelMaDonHang.Text = dh.MADH;
            ng.Text = dh.NGUOIGUI.HOTEN;
            nn.Text = dh.NGUOINHAN.HOTEN;
            dcnn.Text = dh.NGUOINHAN.DIACHI;
            dcng.Text = dh.NGUOIGUI.DIACHI;

            if (dh.TTHD == "Chờ vận chuyển") LoadChoGiaoHang();
            if (dh.TTHD == "Đang trì hoãn") LoadTriHoan();
            else
            {
                SHIPPER sp = (from a in db.SHIPPERs where dh.PHIGH.MASHIPPER == a.MASHIPPER select a).SingleOrDefault();
                if (dh.TTHD == "Đang giao hàng") LoadDangGiaoHang(sp.TEN);
                if (dh.TTHD == "Chờ nhận hàng") LoadChoNhanHang(sp.TEN);
                if (dh.TTHD == "Đang hoàn trả") LoadDangHoanTra(sp.TEN);
                if (dh.TTHD == "Đã hủy") LoadDaHuy(sp.TEN);
                if (dh.TTHD == "Giao hàng thành công") LoadGiaoHangThanhCong(sp.TEN);

            }
        }

        void LoadChoGiaoHang()
        {
            //Hiển thị

            btDaGiaoHang.Enabled = false;
            btTriHoan.Enabled = true;
            labelTinhTrang.Text = "Chờ vận chuyển";
            labelChoGiaoHang.Text = "CHỜ VẬN CHUYỂN";

            //Ẩn
            ttShipper.Visible = false;

            lineDangGiaoHang.Visible = false;
            paneDangGiaoHang.Visible = false;

            lineDangHoanTra.Visible = false;
            panelDangHoanTra.Visible = false;

            lineDaHuy.Visible = false;
            panelDaHuy.Visible = false;

            lineGiaoHangThanhCong.Visible = false;
            lineGiaoHangThanhCong2.Visible = false;
            paneGiaoHangThanhCong.Visible = false;
        }

        void LoadTriHoan() 
        {
            //Hiển thị
            ttShipper.Visible = false;

            btDaGiaoHang.Enabled = false;
            btTriHoan.Enabled = false;
            labelTinhTrang.Text = "Đang trì hoãn";
            labelChoGiaoHang.Text = "CHỜ VẬN CHUYỂN";

            //Ẩn
            lineDangGiaoHang.Visible = false;
            paneDangGiaoHang.Visible = false;

            lineDangHoanTra.Visible = false;
            panelDangHoanTra.Visible = false;

            lineDaHuy.Visible = false;
            panelDaHuy.Visible = false;

            lineGiaoHangThanhCong.Visible = false;
            lineGiaoHangThanhCong2.Visible = false;
            paneGiaoHangThanhCong.Visible = false;
        }

        void LoadDangGiaoHang(string tensp)
        {
            //CÀI ĐẶT
            ChonShipper.Enabled = false;
            ChonShipper.Text = tensp;
            //Hiển thị1
            ttShipper.Visible = true;

            labelTinhTrang.Text = "Đang giao hàng";
            labelChoGiaoHang.Text = "ĐÃ LẤY HÀNG";
            labelDangGiaoHang.Text = "ĐANG GIAO HÀNG";

            btDaGiaoHang.Visible = false;
            btTriHoan.Visible = false;

            lineDangGiaoHang.Visible = true;
            paneDangGiaoHang.Visible = true;

            btDaNhanHang.Enabled = true;
            btChoNhanHang.Enabled = true;
            btKhongNhanHang.Enabled = true;

            //Ẩn
            lineDangHoanTra.Visible = false;
            panelDangHoanTra.Visible = false;

            lineDaHuy.Visible = false;
            panelDaHuy.Visible = false;

            lineGiaoHangThanhCong.Visible = false;
            lineGiaoHangThanhCong2.Visible = false;
            paneGiaoHangThanhCong.Visible = false;
        }

        void LoadChoNhanHang(string tensp)
        {
            ttShipper.Visible = true;

            ChonShipper.Enabled = false;
            ChonShipper.Text = tensp;
            //Hiển thị
            labelTinhTrang.Text = "Chờ nhận hàng";
            btDaGiaoHang.Visible = false;
            btTriHoan.Visible = false;
            labelChoGiaoHang.Text = "ĐÃ LẤY HÀNG";
            labelDangGiaoHang.Text = "ĐANG GIAO HÀNG";

            lineDangGiaoHang.Visible = true;
            paneDangGiaoHang.Visible = true;

            btDaNhanHang.Enabled = true;
            btChoNhanHang.Enabled = false;
            btKhongNhanHang.Enabled = true;

            //Ẩn
            lineDangHoanTra.Visible = false;
            panelDangHoanTra.Visible = false;

            lineDaHuy.Visible = false;
            panelDaHuy.Visible = false;

            lineGiaoHangThanhCong.Visible = false;
            lineGiaoHangThanhCong2.Visible = false;
            paneGiaoHangThanhCong.Visible = false;
        }


        void LoadDangHoanTra(string tensp)
        {
            ChonShipper.Enabled = false;
            ChonShipper.Text = tensp;
            //Hiển thị
            labelTinhTrang.Text = "Đang hoàn trả";
            labelChoGiaoHang.Text = "ĐÃ LẤY HÀNG";
            labelDangGiaoHang.Text = "ĐÃ GIAO HÀNG";

            ttShipper.Visible = true;
            btDaGiaoHang.Visible = false;
            btTriHoan.Visible = false;

            lineDangGiaoHang.Visible = true;
            paneDangGiaoHang.Visible = true;

            btDaNhanHang.Enabled = false;
            btChoNhanHang.Enabled = false;
            btKhongNhanHang.Enabled = false;

            lineDangHoanTra.Visible = true;
            panelDangHoanTra.Visible = true;

            btDaNhanLai.Enabled = true;

            //Ẩn
            lineDaHuy.Visible = false;
            panelDaHuy.Visible = false;

            lineGiaoHangThanhCong.Visible = false;
            lineGiaoHangThanhCong2.Visible = false;
            paneGiaoHangThanhCong.Visible = false;
        }


        void LoadDaHuy(string tensp)
        {
            ChonShipper.Enabled = false;
            ChonShipper.Text = tensp;
            //Hiển thị
            labelTinhTrang.Text = "Đã hủy";
            labelChoGiaoHang.Text = "ĐÃ LẤY HÀNG";
            labelDangGiaoHang.Text = "ĐÃ GIAO HÀNG";
            labelDangHoanTra.Text = "ĐÃ HOÀN TRẢ";

            ttShipper.Visible = true;
            btDaGiaoHang.Visible = false;
            btTriHoan.Visible = false;

            lineDangGiaoHang.Visible = true;
            paneDangGiaoHang.Visible = true;

            btDaNhanHang.Enabled = false;
            btChoNhanHang.Enabled = false;
            btKhongNhanHang.Enabled = false;

            lineDangHoanTra.Visible = true;
            panelDangHoanTra.Visible = true;

            btDaNhanLai.Enabled = false;

            lineDaHuy.Visible = true;
            panelDaHuy.Visible = true;

            //Ẩn
            lineGiaoHangThanhCong.Visible = false;
            lineGiaoHangThanhCong2.Visible = false;
            paneGiaoHangThanhCong.Visible = false;
        }

        void LoadGiaoHangThanhCong(string tensp)
        {
            ChonShipper.Enabled = false;
            ChonShipper.Text = tensp;
            //Hiển thị
            labelTinhTrang.Text = "Giao hàng thành công";
            labelChoGiaoHang.Text = "ĐÃ LẤY HÀNG";
            labelDangGiaoHang.Text = "ĐÃ GIAO HÀNG";

            btDaGiaoHang.Visible = false;
            btTriHoan.Visible = false;

            lineDangGiaoHang.Visible = true;
            paneDangGiaoHang.Visible = true;

            ttShipper.Visible = true;
            btDaNhanHang.Enabled = false;
            btChoNhanHang.Enabled = false;

            btKhongNhanHang.Enabled = false;

            lineGiaoHangThanhCong.Visible = true;
            lineGiaoHangThanhCong2.Visible = true;
            paneGiaoHangThanhCong.Visible = true;

            //Ẩn
            lineDangHoanTra.Visible = false;
            panelDangHoanTra.Visible = false;

            lineDaHuy.Visible = false;
            panelDaHuy.Visible = false;

            
        }
    

    //CHỨC NĂNG BUTTOM

        private void btDaGiaoHang_Click(object sender, EventArgs e)
        {
            if (ChonShipper.SelectedItem == null) XtraMessageBox.Show("Vui lòng chọn Shipper!", "Lỗi!");
            else
            {
                try
                {
                    DONHANG dh = (from a in db.DONHANGs where a.MADH == madh select a).Single();
                    SHIPPER sp = (from a in db.SHIPPERs where a.MAPT == dh.PHIGH.MAPT && a.TEN == ChonShipper.Text select a).Single();
                    XtraMessageBox.Show("Chuyển đơn hàng cho " + sp.TEN + " thành công!", "Thông báo");

                    dh.TTHD = "Đang giao hàng";
                    dh.PHIGH.MASHIPPER = sp.MASHIPPER;
                    db.SubmitChanges();

                    fmXemChiTietDonHang_Load(sender, e);
                }
                catch (Exception)
                {

                    throw;
                }
            }
           
        }

        private void btTriHoan_Click(object sender, EventArgs e)
        {
            DONHANG dh = (from a in db.DONHANGs where a.MADH == madh select a).Single();
            dh.TTHD = "Đang trì hoãn";
            db.SubmitChanges();
            LoadTriHoan();
        }

        private void btDaNhanHang_Click(object sender, EventArgs e)
        {
            DONHANG dh = (from a in db.DONHANGs where a.MADH == madh select a).Single();
            dh.TTHD = "Giao hàng thành công";
            db.SubmitChanges();
            fmXemChiTietDonHang_Load(sender, e);
           
        }

        private void btChoNhanHang_Click(object sender, EventArgs e)
        {
            DONHANG dh = (from a in db.DONHANGs where a.MADH == madh select a).Single();
            dh.TTHD = "Chờ nhận hàng";
            db.SubmitChanges();

            fmXemChiTietDonHang_Load(sender, e);
        }

        private void btKhongNhanHang_Click(object sender, EventArgs e)
        {
            DONHANG dh = (from a in db.DONHANGs where a.MADH == madh select a).Single();
            dh.TTHD = "Đang hoàn trả";
            db.SubmitChanges();

            fmXemChiTietDonHang_Load(sender, e);
        }

        private void btDaNhanLai_Click(object sender, EventArgs e)
        {

            DONHANG dh = (from a in db.DONHANGs where a.MADH == madh select a).Single();
            dh.TTHD = "Đã hủy";
            db.SubmitChanges();

            fmXemChiTietDonHang_Load(sender, e);
        }

        private void labelChoGiaoHang_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btDaGiaoHang.Enabled = true;
        }

        private void pnChoGiaoHang_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btHangHoa_Click(object sender, EventArgs e)
        {
            try
            {
                DONHANG dh = (from a in db.DONHANGs where a.MADH == madh select a).Single();
                fmXemHangHoa f = new fmXemHangHoa(dh.MADH, 1);
                f.ShowDialog();
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private void btNguoiGui_Click(object sender, EventArgs e)
        {
            try
            {
                DONHANG dh = (from a in db.DONHANGs where a.MADH == madh select a).Single();
                fmXemHangHoa f = new fmXemHangHoa(dh.MADH, 2);

                f.ShowDialog();
            }
            catch (Exception)
            {
                
                throw;
            }
            

        }

        private void btNguoiNhan_Click(object sender, EventArgs e)
        {
            try
            {
                DONHANG dh = (from a in db.DONHANGs where a.MADH == madh select a).Single();
                fmXemHangHoa f = new fmXemHangHoa(dh.MADH, 3);
                f.ShowDialog();
            }
            catch (Exception)
            {
                
                throw;
            }
            

        }





       
    }
}