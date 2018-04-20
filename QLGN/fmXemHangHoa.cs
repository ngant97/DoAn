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
using log4net;

namespace QLGN
{
    public partial class fmXemHangHoa : DevExpress.XtraBars.TabForm
    {
        string madon;

        ILog log = LogManager.GetLogger(typeof(fmXemHangHoa));

        public fmXemHangHoa(string _madon,int i)
        {
            madon = _madon;
            InitializeComponent();
            if (i == 1) tabFormControl1.SelectedPage = tabFormPage1;
            if (i == 2) tabFormControl1.SelectedPage = tabFormPage2;

        }

        CATSHIPDataContext db = new CATSHIPDataContext();

        private void fmXemHangHoa_Load(object sender, EventArgs e)
        {
            try
            {
                DONHANG dh = (from a in db.DONHANGs where a.MADH == madon select a).Single();
                mahang.Text = dh.MAHANG;
                noidung.Text = dh.HANG.NOIDUNG;
                chieucao.Text = dh.HANG.CAO.ToString();
                chieudai.Text = dh.HANG.DAI.ToString();
                chieurong.Text = dh.HANG.RONG.ToString();
                trongluong.Text = dh.HANG.TRONGLUONG.ToString();

                madonhang.Text = dh.MADH;
                nhanvien.Text = dh.NHANVIEN.TEN;
                ngaygui.Text = String.Format("{0:MM/dd/yyyy}", dh.NGAYGUI);
                ttdh.Text = dh.TTHD;
                ttp.Text = dh.PHIGH.TTPHI;

                cmtng.Text = dh.NGUOIGUI.CMND;
                tenng.Text = dh.NGUOIGUI.HOTEN;
                dcng.Text = dh.NGUOIGUI.DIACHI;
                sdtng.Text = dh.NGUOIGUI.SDT;

                cmtnn.Text = dh.NGUOINHAN.CMND;
                tennn.Text = dh.NGUOINHAN.HOTEN;
                dcnn.Text = dh.NGUOINHAN.DIACHI;
                sdtnn.Text = dh.NGUOINHAN.SDT;
            }
            catch (Exception er)
            {
                log.Error(er);
                XtraMessageBox.Show("Không xem được thông tin!","Lỗi!");
            }
         
        }

        private void ttp_Click(object sender, EventArgs e)
        {


        }

        private void tabFormContentContainer2_Click(object sender, EventArgs e)
        {

        }

        private void trongluong_Click(object sender, EventArgs e)
        {

        }
    }
}