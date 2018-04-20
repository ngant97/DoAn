
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LHMContactCenter
{
    public class SqlDependencyHelper : IDisposable
    {
        private string ConnectionString = null;
        private string command;
        private Action Act;

        public SqlDependencyHelper(Action obj, string cmd)
        {
            ConnectionString = QLGN.Properties.Settings.Default.CATSHIPConnectionString;
            Act = obj;
            command = cmd;
            // Start listening notification
            SqlDependency.Stop(ConnectionString);
            SqlDependency.Start(ConnectionString);
        }

        public void loadData()
        {
            // Connect to DB, create subscriber
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(command, con);
                SqlDependency de = new SqlDependency(cmd);

                // This event will run when receive message from DB
                de.OnChange += new OnChangeEventHandler(de_OnChange);
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                // Run action that you want to do on changed
                if (Act != null)
                    Act.Invoke();
            }
        }

        private void de_OnChange(object sender, SqlNotificationEventArgs e)
        {
            SqlDependency de = sender as SqlDependency;
            de.OnChange -= de_OnChange;
            loadData();
        }

        public void Dispose()
        {
            // Unregister the notification subscription for the current instance.
            SqlDependency.Stop(ConnectionString);
        }
    }
}
