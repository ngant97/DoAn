using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LHMContactCenter
{
    class GiaTriV2
    {
       

        public static Color ColorButton()
        {
            //return ColorTranslator.FromHtml("#6666ff");
            Color mau = Color.FromArgb(51, 153, 255);
            return mau;
        }

        public static Color GroupColor()
        {
            //return Color.LightCyan;
            //return Color.RoyalBlue;
            //return Color.DarkSeaGreen;
            Color mau = Color.FromArgb(51, 153, 255);
            return mau;
            //return ColorTranslator.FromHtml("#14C30D");
            //return ColorTranslator.FromHtml("#79AE92");
        }

        public static Color MauForm()
        {
            //return Color.LightCyan;
            //return Color.RoyalBlue;
            //return Color.DarkSeaGreen;
            //Color c = ColorTranslator.FromHtml("#FFCC66");
            return ColorTranslator.FromHtml("#FFFFFF");
        }

        
        public static Color RowColor()
        {
            //return Color.LightCyan;
            return ColorTranslator.FromHtml("#E0F8E0");
            //return Color.#CEF6CE;
        }
        public static Color GridRowColor()
        {
            //return Color.LightCyan;
            return ColorTranslator.FromHtml("#E0F8E0");
            //return Color.#CEF6CE;
        }

        public static Color TaskChuaThucHien()
        {
            //return Color.LightCyan;
            return Color.Tomato;
        }

        public static Color TaskDaThucHien()
        {
            //return Color.LightCyan;
            return Color.LimeGreen;
        }
        public static Color ChooseRowGridEdit()
        {
            return ColorTranslator.FromHtml("#0033FF");
        }

        public static Image GetImageFromString(string base64String)
        {
            byte[] buffer = Convert.FromBase64String(base64String);
            if (buffer != null)
            {
                ImageConverter ic = new ImageConverter();
                return ic.ConvertFrom(buffer) as Image;
            }

            return null;
        }
    }
}
