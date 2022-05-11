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
    /// Logika interakcji dla klasy EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        int userId;
        bool passwordVisible;
        public EditUserWindow(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            SetComboValues();
            GetUserData();
        }

        void SetComboValues()
        {
            privilegesComboBox.Items.Add("user");
            privilegesComboBox.Items.Add("administrator");
        }

        void GetUserData() 
        {
            DatabaseConnector.StartConnection();
            String query = $"SELECT * FROM users WHERE id={userId};";
            try
            {
                MySqlCommand cmd = new(query, DatabaseConnector.connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    this.Title = "Konto: "+reader.GetString(3);
                    loginTextBox.Text = reader.GetString(3);
                    nameTextBox.Text = reader.GetString(1);
                    surnameTextBox.Text = reader.GetString(2);
                    emailTextBox.Text = reader.GetString(5);
                    passwordBox.Password = reader.GetString(4);
                    idLabel.Content = reader.GetString(0);
                    dateLabel.Content = reader.GetString(7);
                    if (reader.GetString(6) == "administrator")
                    {
                        privilegesComboBox.SelectedIndex = 1;
                    }
                    else
                    {
                        privilegesComboBox.SelectedIndex = 0;
                    }
                    reader.Close();
                }
                else
                {
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            DatabaseConnector.CloseConnection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String login = loginTextBox.Text;
            String name = nameTextBox.Text;
            String surname = surnameTextBox.Text;
            String email = emailTextBox.Text;
            String password;
            String privileges = (string)privilegesComboBox.SelectedItem.ToString();

            if (passwordVisible)
            {
                password = passwordTextBox.Text;
            }
            else
            {
                password = passwordBox.Password;
            }

            DatabaseConnector.StartConnection();
            String query = $"UPDATE Users SET login='{login}',name='{name}', surname='{surname}', email='{email}', password='{password}', privileges='{privileges}' WHERE id={userId};";
            try
            {
                MySqlCommand cmd = new(query, DatabaseConnector.connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Użytkownik "+login+" został zmodyfikowany pomyślnie.");
                this.Close();
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());
            }
            DatabaseConnector.CloseConnection();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            passwordTextBox.Text = passwordBox.Password;
            passwordTextBox.Visibility = Visibility.Visible;
            passwordBox.Visibility = Visibility.Hidden;
            passwordVisible = true;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            passwordBox.Password = passwordTextBox.Text;
            passwordTextBox.Visibility = Visibility.Hidden;
            passwordBox.Visibility = Visibility.Visible;
            passwordVisible = false;
        }
    }
}
