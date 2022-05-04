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
        int eventId = 0;
        String eventTitle;

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

        private void ComboEventTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EventInfoChange();
        }

        void EventInfoChange()
        {
            eventTitle = (string)comboEventTitle.SelectedItem.ToString();
            DatabaseConnector.StartConnection();
            String query = $"SELECT * FROM Events WHERE title='{eventTitle}';";
            try
            {
                MySqlCommand cmd = new(query, DatabaseConnector.connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    eventId = (int)reader[0];
                    labelDescription.Text = (string)reader[2];
                    labelDateTime.Content = reader[3] + " | " + reader[4];
                }

                DatabaseConnector.CloseConnection();
            }
            catch (Exception)
            {
                DatabaseConnector.CloseConnection();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String attendType = (string)comboAttendType.SelectedItem.ToString();
            String foodType = (string)comboFoodType.SelectedItem.ToString();

            DatabaseConnector.StartConnection();
            if (DatabaseOperation.SendEntry(eventId, attendType, foodType, user))
            {
                MessageBox.Show("Zgłoszenie na udział w " + eventTitle + " zostało wysłane.");
                DatabaseConnector.CloseConnection();
            }
            else 
            {
                MessageBox.Show("Coś poszło nie tak.");
                DatabaseConnector.CloseConnection();
            }

            comboAttendType.SelectedIndex = 0;
            comboEventTitle.SelectedIndex = 0;
            comboFoodType.SelectedIndex = 0;
        }
    }
}
