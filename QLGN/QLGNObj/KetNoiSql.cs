using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien
{
    public class KetNoiSql
    {
        public static string con = QLGN.Properties.Settings.Default.CATSHIPConnectionString;
        public static SqlConnection cnn = new SqlConnection(con);
        ILog log = LogManager.GetLogger(typeof(KetNoiSql));

        SqlCommand cmm = new SqlCommand();
        public static DataSet ds = new DataSet();
        public static SqlDataAdapter da;
        internal int KetnoiCSDL(string query)
        {
            int check = 0;
            connect();
            try
            {
                SqlCommand com = new SqlCommand(query, cnn);
                check = com.ExecuteNonQuery();
            }
            catch (Exception ex )
            {
                check = 0;
                log.Error(ex);
            }
            disconnect();
            return check;
        }
        internal DataTable KetnoiCSDL_Load(string query)
        {
            connect();
            SqlCommand com = new SqlCommand(query, cnn);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cnn.Close();
            disconnect();
            return dt;
        }
        internal  int  Check (string query)
        {
            connect();
            SqlCommand com = new SqlCommand(query, cnn);
            Int32 i = Convert.ToInt32(com.ExecuteScalar());
            com.Dispose();
            disconnect();
            return i;
        }
        internal DataSet KetnoiCSDL_LoadDataSet(string query)
        {
            connect();
            SqlCommand com = new SqlCommand(query, cnn);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cnn.Close();
            disconnect();
            return ds;
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
    }
}
