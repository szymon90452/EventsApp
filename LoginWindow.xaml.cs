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
    /// Logika interakcji dla klasy LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        bool passwordIsVisible = false;
        int failedAttempt = 0;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String login,password;
            login = loginBox.Text;

            if (passwordIsVisible)
            {
                password = passwordTextBox.Text;
            }
            else
            {
                password = passwordBox.Password.ToString();
            }

            if (failedAttempt < 3)
            {
                if (DatabaseConnector.StartConnection())
                {
                    if (DatabaseOperation.UserLogin(login, password))
                    {
                        User user = DatabaseOperation.GetInfoAboutUser(login, password);
                        if (user.GetPriviledges() == "user")
                        {
                            UserWindow userWindow = new(user);
                            userWindow.Show();
                            this.Close();
                            DatabaseConnector.CloseConnection();
                        }
                        else if (user.GetPriviledges() == "administrator")
                        {
                            AdminWindow adminWindow = new(user);
                            adminWindow.Show();
                            this.Close();
                            DatabaseConnector.CloseConnection();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Nieprawidłowe dane logowania");
                        failedAttempt++;
                    }
                }
            }
            else 
            {
                MessageBox.Show("Przekroczono ilość nieudanych prób logowania");    
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //Switch from passwordBox to TextBox
            passwordTextBox.Text = passwordBox.Password.ToString();
            passwordBox.Visibility = Visibility.Hidden;
            passwordTextBox.Visibility = Visibility.Visible;
            passwordIsVisible = true;
        }

        private void Checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            //Switch from textBox to passwordBox
            passwordBox.Password = passwordTextBox.Text;
            passwordBox.Visibility = Visibility.Visible;
            passwordTextBox.Visibility = Visibility.Hidden;
            passwordIsVisible= false;
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            StartWindow startWindow = new();
            startWindow.Show();
            this.Close();
        }
    }
}
