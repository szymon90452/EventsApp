using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace EventsApp
{
    /// <summary>
    /// Logika interakcji dla klasy AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {

        User user;
        public AdminWindow(User user)
        {
            InitializeComponent();
            this.user = user;
            GetUsersFromDb();
        }

        void GetUsersFromDb()
        {
            DatabaseConnector.StartConnection();
            String query = $"SELECT id,login,password,name,surname,DATE_FORMAT(date_of_registry, '%d-%m-%Y'),privileges FROM users GROUP BY privileges;";
            try
            {
                MySqlCommand cmd = new(query, DatabaseConnector.connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                usersListView.Items.Clear();

                while (reader.Read())
                {
                    usersListView.Items.Add(new UserItem { Id = (int)reader[0], Login = (string)reader[1], Name= (string)reader[3], Surname=(string)reader[4], DateOfRegistry= (string)reader[5], Privileges = (string)reader[6]});
                }
                DatabaseConnector.CloseConnection();

            }
            catch (Exception e)
            {
                DatabaseConnector.CloseConnection();
                MessageBox.Show(e.ToString());
            }
        }

        private void EditUser(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            UserItem userItem = b.CommandParameter as UserItem;
            //Display other window with edit of current user
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            UserItem userItem = b.CommandParameter as UserItem;
            //Delete procedure + refresh list
        }

    }
}
