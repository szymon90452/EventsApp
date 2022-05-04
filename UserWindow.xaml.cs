using MySql.Data.MySqlClient;
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

namespace EventsApp
{
    /// <summary>
    /// Logika interakcji dla klasy UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public User user;
        public UserWindow(User user)
        {
            InitializeComponent();
            this.user = user;
            InitializeCombos();
            InitializeUserInfo();
            InitializeComboEvent();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            StartWindow startWindow = new();
            startWindow.Show();
            this.Close();
        }

        void InitializeCombos() 
        {
            comboAttendType.Items.Add("Słuchacz");
            comboAttendType.Items.Add("Autor");
            comboAttendType.Items.Add("Sponsor");
            comboAttendType.Items.Add("Organizator");
            comboAttendType.SelectedIndex = 0;
            comboFoodType.Items.Add("Bez preferencji");
            comboFoodType.Items.Add("Wegetariańskie");
            comboFoodType.Items.Add("Bez glutenu");
            comboFoodType.SelectedIndex = 0;
        }

        void InitializeUserInfo()
        {
            nameLabel.Content = user.GetName() + " " + user.GetSurname();
            nickLabel.Content = user.GetLogin();
        }

        void InitializeComboEvent()
        {
            DatabaseConnector.StartConnection();
            String query = $"SELECT * FROM Events;";
            try
            {
                MySqlCommand cmd = new(query, DatabaseConnector.connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) 
                {
                    comboEventTitle.Items.Add((string)reader[1]);
                }
                comboEventTitle.SelectedIndex = 1;
                EventInfoChange();

                DatabaseConnector.CloseConnection();
            }
            catch (Exception)
            {
                DatabaseConnector.CloseConnection();
            }
        }

        private void comboEventTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EventInfoChange();
        }

        void EventInfoChange()
        {
            String eventTitle = (string)comboEventTitle.SelectedItem.ToString();
            DatabaseConnector.StartConnection();
            String query = $"SELECT * FROM Events WHERE title='{eventTitle}';";
            try
            {
                MySqlCommand cmd = new(query, DatabaseConnector.connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    labelDescription.Content = reader[2];
                    labelDateTime.Content = reader[3] + " | " + reader[4];
                }

                DatabaseConnector.CloseConnection();
            }
            catch (Exception)
            {
                DatabaseConnector.CloseConnection();
            }
        }

    }
}
