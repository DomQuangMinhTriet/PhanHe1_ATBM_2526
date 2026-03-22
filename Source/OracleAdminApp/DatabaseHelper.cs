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

        // Hàm khởi tạo chuỗi kết nối
        public void BuildConnectionString(string host, string port, string serviceName, string username, string password, bool isSysDba = false)
        {
            // Cú pháp kết nối chuẩn của Oracle Managed Data Access
            connectionString = $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={serviceName})));User Id={username};Password={password};";

            if (isSysDba)
            {
                connectionString += "DBA Privilege=SYSDBA;";
            }
        }

        // Hàm mở kết nối để test lúc đăng nhập
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

        // Hàm ngắt kết nối
        public void Disconnect()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        // Hàm thực thi lệnh SELECT và trả về DataTable (Dùng để đổ dữ liệu lên DataGridView)
        public DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                // Thêm dòng này để khởi tạo connection nếu nó bị null
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

        // Hàm thực thi lệnh INSERT, UPDATE, DELETE, DDL, DCL (Tạo user, Cấp quyền...)
        public bool ExecuteNonQuery(string query)
        {
            try
            {
                // Thêm dòng này để khởi tạo connection nếu nó bị null
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