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
        public static bool UserLogin(string login, string password) 
        {
            String query = $"SELECT * FROM users WHERE login='{login}' AND password='{password}';";
            try
            {
                MySqlCommand cmd = new(query, DatabaseConnector.connection);
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

        public static bool UserRegister(string name, string surname,string login, string password, string email, string priviledges="user")
        {
            bool loginExists = false;
            String checkquery = $"SELECT * FROM users where login='{login}';";
            try
            {
                MySqlCommand cmd = new(checkquery, DatabaseConnector.connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    reader.Close();
                    loginExists = true;
                }
                else
                {
                    reader.Close();
                    loginExists = false;
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());
                return false;
            }

            if (loginExists)
            {
                MessageBox.Show("Już istnieje użytkownik z taką nazwą!");
                return false;
            }
            else 
            {
                String query = $"INSERT INTO users (name,surname,login,password,email,privileges,date_of_registry) VALUES ('{name}','{surname}','{login}','{password}','{email}','{priviledges}',CURDATE());";
                try
                {
                    MySqlCommand cmd = new(query, DatabaseConnector.connection);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    //MessageBox.Show(ex.ToString());
                    return false;
                }
            }
        }

        public static User GetInfoAboutUser(String login, String password)
        {
            String query = $"SELECT * FROM users WHERE login='{login}' AND password='{password}';";
            try
            {
                MySqlCommand cmd = new(query, DatabaseConnector.connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    User user = new((int)reader[0],(String)reader[1], (String)reader[2], (String)reader[3], (String)reader[5], (String)reader[6]);
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

        public static bool SendEntry(int eventId, String attendType, String foodType, User user) 
        {
            String query = $"INSERT INTO Entries(user_id,event_id,attend_type,food_type,status) VALUES ({user.GetId()},{eventId}, '{attendType}', '{foodType}' ,'rozpatrywany');";
            try
            {
                MySqlCommand cmd = new(query, DatabaseConnector.connection);
                cmd.ExecuteNonQuery();
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
