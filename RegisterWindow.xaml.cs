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
using System.Text.RegularExpressions;

namespace EventsApp
{
    /// <summary>
    /// Logika interakcji dla klasy RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String name, surname, login, password, repeatPassword, email;
            name = nameTextBox.Text;
            surname = surnameTextBox.Text;
            login = loginTextBox.Text;
            password = passwordTextBox.Password.ToString();
            repeatPassword = repeatPasswordTextBox.Password.ToString();
            email = emailTextBox.Text;

            if (password == repeatPassword)
            {
                if (DatabaseOperation.userRegister(name, surname, login, password, email))
                {
                    MessageBox.Show("Pomyślnie zarejestrowano.");
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();
                    this.Close();
                }
                else 
                {
                    MessageBox.Show("Coś poszło nie tak.");
                }

            }
            else
            {
                MessageBox.Show("Hasła muszą być takie same.");
            }

        }

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if( (new Regex(@"^[A-Za-z]+$")).IsMatch(nameTextBox.Text))
            {
                nameTextBox.Foreground = Brushes.Black;
            }
            else
            {
                nameTextBox.Foreground = Brushes.Red;
            }
        }

        private void surnameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((new Regex(@"^[A-Za-z]+$")).IsMatch(surnameTextBox.Text))
            {
                surnameTextBox.Foreground = Brushes.Black;
            }
            else
            {
                surnameTextBox.Foreground = Brushes.Red;
            }
        }

        private void loginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((new Regex(@"^(?=.*[A-Za-z0-9]$)[A-Za-z][A-Za-z\d.-]{0,19}$")).IsMatch(loginTextBox.Text))
            {
                loginTextBox.Foreground = Brushes.Black;
            }
            else
            {
                loginTextBox.Foreground = Brushes.Red;
            }
        }

        private void emailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9]
                (?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]
                {0,61}[a-zA-Z0-9])?)*$")).IsMatch(emailTextBox.Text))
            {
                emailTextBox.Foreground = Brushes.Black;
            }
            else
            {
                emailTextBox.Foreground = Brushes.Red;
            }
        }

        private void passwordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if ((new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$")).IsMatch(passwordTextBox.Password.ToString()) && 
                (passwordTextBox.Password.ToString() == repeatPasswordTextBox.Password.ToString()))
            {
                passwordTextBox.Foreground = Brushes.Black;
                repeatPasswordTextBox.Foreground = Brushes.Black;
            }
            else
            {
                passwordTextBox.Foreground = Brushes.Red;
                repeatPasswordTextBox.Foreground = Brushes.Red;
            }
        }

        private void repeatPasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if ((new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$")).IsMatch(repeatPasswordTextBox.Password.ToString()) &&
                (passwordTextBox.Password.ToString() == repeatPasswordTextBox.Password.ToString()))
            {
                passwordTextBox.Foreground = Brushes.Black;
                repeatPasswordTextBox.Foreground = Brushes.Black;
            }
            else
            {
                passwordTextBox.Foreground = Brushes.Red;
                repeatPasswordTextBox.Foreground = Brushes.Red;
            }
        }
    }
}
