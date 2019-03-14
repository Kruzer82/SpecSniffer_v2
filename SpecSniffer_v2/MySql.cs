using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace SpecSniffer_v2
{
    class MySql
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }
        private MySqlConnection _connection;
        

        public MySql(string server, string port, string user, string password, string databaseName)
        {
            Server = server;
            Port = port;
            User = user;
            Password = password;
            DatabaseName = databaseName;
            Initialize();
        }

        public void Initialize()
        {
            string connectionString = 
                $"SERVER={Server}"+
                $";PORT={Port}"+ 
                $";USERID={User}"+ 
                $";PASSWORD={Password}" + 
                $";DATABASE={DatabaseName}" +
                ";Connection Timeout=10;";

            _connection=new MySqlConnection(@connectionString);
        }

        public bool CheckConnection()
        {
            if (OpenConnection())
            {
                CloseConnection();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void NonQuery(string statement)
        {
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(statement, _connection);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }

        private bool OpenConnection()
        {


            try
            {
                _connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"MySQL error.\n" +
                                $"Error id: {ex.Number}\n" +
                                $"Error : {ex.Message}");
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                _connection.Close();
              return  true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"MySQL error.\n" +
                                $"Error : {ex.Message}");
              return  false;
            }
        }
    }
}
