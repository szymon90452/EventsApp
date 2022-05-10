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
            GetDataFromDb();
        }

        void GetDataFromDb() {
            GetUsersFromDb();
            GetEventsFromDb();
            GetEntriesFromDb();
        }

        void GetUsersFromDb()
        {
            DatabaseConnector.StartConnection();
            String query = $"SELECT id,login,password,name,surname,DATE_FORMAT(date_of_registry, '%d-%m-%Y'),privileges FROM users;";
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

        void GetEventsFromDb()
        {
            DatabaseConnector.StartConnection();
            String query = $"SELECT id,title,description,DATE_FORMAT(date_of_event, '%d-%m-%Y'),time_of_event FROM events;";
            try
            {
                MySqlCommand cmd = new(query, DatabaseConnector.connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                eventsListView.Items.Clear();

                while (reader.Read())
                {
                    eventsListView.Items.Add(new EventItem { Id = (int)reader[0], Title = (string)reader[1], Description = (string)reader[2], DateTimeOfEvent = (string)reader[3]+" | "+reader[4] });
                }
                DatabaseConnector.CloseConnection();
            }
            catch (Exception e)
            {
                DatabaseConnector.CloseConnection();
                MessageBox.Show(e.ToString());
            }
        }

        void GetEntriesFromDb()
        {
            DatabaseConnector.StartConnection();
            String query = $"SELECT entries.id,users.name,users.surname,events.title,entries.attend_type,entries.food_type,entries.status FROM entries INNER JOIN USERS ON users.id = entries.user_id INNER JOIN events ON events.id = entries.event_id WHERE entries.status = 'rozpatrywany';";
            try
            {
                MySqlCommand cmd = new(query, DatabaseConnector.connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                entriesListView.Items.Clear();

                while (reader.Read())
                {
                    entriesListView.Items.Add(new EntryItem { Id = (int)reader[0], Name = (string)reader[1], Surname = (string)reader[2], Title = (string)reader[3], AttendType = (string)reader[4], FoodType = (string)reader[5], Status = (string)reader[6] });
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
            DatabaseConnector.StartConnection();
            String query = $"DELETE FROM Users WHERE id={userItem.Id};";
            String query2 = $"DELETE FROM Entries WHERE user_id={userItem.Id};";
            try
            {
                MySqlCommand cmd = new(query, DatabaseConnector.connection);
                MySqlCommand cmd2 = new(query2, DatabaseConnector.connection);
                cmd2.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());
            }
            DatabaseConnector.CloseConnection();
            GetUsersFromDb();
            GetEntriesFromDb();
        }

        private void DeleteEvent(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            EventItem eventItem = b.CommandParameter as EventItem;
            DatabaseConnector.StartConnection();
            String query = $"DELETE FROM Events WHERE Events.id={eventItem.Id};";
            String query2 = $"DELETE FROM Entries WHERE Entries.event_id={eventItem.Id};";
            try
            {
                MySqlCommand cmd = new(query, DatabaseConnector.connection);
                MySqlCommand cmd2 = new(query2, DatabaseConnector.connection);
                cmd2.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());
            }
            DatabaseConnector.CloseConnection();
            GetEventsFromDb();
            GetEntriesFromDb();
        }

        private void AcceptUserEntry(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            EntryItem entryItem = b.CommandParameter as EntryItem;
            DatabaseConnector.StartConnection();
            String query = $"UPDATE Entries SET status='zaakceptowany' WHERE Entries.id={entryItem.Id};";
            try
            {
                MySqlCommand cmd = new(query, DatabaseConnector.connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());
            }
            DatabaseConnector.CloseConnection();
            GetEntriesFromDb();
        }

        private void RejectUserEntry(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            EntryItem entryItem = b.CommandParameter as EntryItem;
            DatabaseConnector.StartConnection();
            String query = $"UPDATE Entries SET status='odrzucony' WHERE Entries.id={entryItem.Id};";
            try
            {
                MySqlCommand cmd = new(query, DatabaseConnector.connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());
            }
            DatabaseConnector.CloseConnection();
            GetEntriesFromDb();
        }

    }
}