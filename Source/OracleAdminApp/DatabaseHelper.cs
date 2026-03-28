using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Windows.Forms;

namespace OracleSecurityAdmin
{
    public class DatabaseHelper
    {
        private OracleConnection connection;
        private string connectionString;

        public void BuildConnectionString(string host, string port, string serviceName, string username, string password, bool isSysDba = false)
        {
            connectionString = $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={serviceName})));User Id={username};Password={password};";

            if (isSysDba)
            {
                connectionString += "DBA Privilege=SYSDBA;";
            }
        }

        public bool Connect()
        {
            try
            {
                connection = new OracleConnection(connectionString);
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void Disconnect()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (connection == null)
                {
                    connection = new OracleConnection(connectionString);
                }

                if (connection.State != ConnectionState.Open) connection.Open();

                using (OracleCommand cmd = new OracleCommand(query, connection))
                {
                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }

        public bool ExecuteNonQuery(string query)
        {
            try
            {
                if (connection == null)
                {
                    connection = new OracleConnection(connectionString);
                }

                if (connection.State != ConnectionState.Open) connection.Open();

                using (OracleCommand cmd = new OracleCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực thi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}