using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad
{
    class DatabaseManager
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;" +
            "port=3306;" +
            "username=root;" +
            "password=0000;" +
            "database=Sklad");

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public MySqlConnection GetConnection { get { return connection; } }
    }
}