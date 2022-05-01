using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace EventsApp
{
    public static class DatabaseConnector
    {
        public static MySqlConnection connection;
        public static bool startConnection()
        {
            try
            {
                string connectionString = "SERVER=localhost;DATABASE=eventsdatabase;UID=root;PASSWORD=;";
                connection = new MySqlConnection(connectionString);
                connection.Open();
                return true;
            }
            catch (Exception ex) { 
                MessageBox.Show(ex.ToString());
                return false;
            }
            
        }

        public static bool closeConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
    }
}
