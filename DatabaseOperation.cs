using System;
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
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static bool userRegister(string name, string surname,string login, string password, string email)
        {
            String query = $"INSERT INTO users (name,surname,login,password,email,privileges,date_of_registry) VALUES ('{name}','{surname}','{login}','{password}','{email}','user',CURDATE());";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, DatabaseConnector.connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static User getInfoAboutUser(String login, String password)
        {
            String query = $"SELECT * FROM users WHERE login='{login}' AND password='{password}';";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, DatabaseConnector.connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    User user = new User((String)reader[1], (String)reader[2], (String)reader[3], (String)reader[5], (String)reader[6]);
                    reader.Close();
                    return user;
                }
                else 
                {
                    return new User("false");
                }

            }
            catch (Exception)
            {
                return new User("false");
            }
        }
    }
}
