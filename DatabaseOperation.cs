﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;

namespace EventsApp
{
    public static class DatabaseOperation
    {
        public static bool userLogin(string login, string password) 
        {
            String query = $"SELECT * FROM users WHERE login='{login}' AND password='{password}';";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, DatabaseConnector.connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    reader.Close();
                    return true;
                }
                else
                {
                    reader.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static bool userRegister(string login, string password)
        {
            String query = $"INSERT;";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, DatabaseConnector.connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    reader.Close();
                    return true;
                }
                else
                {
                    reader.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
    }
}