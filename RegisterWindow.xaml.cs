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
        //Regex match flag for validation
        bool correctName = false;
        bool correctSurname = false;
        bool correctLogin = false;
        bool correctPassword = false;
        bool correctEmail = false;


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

            if (textBoxNotEmpty())
            {
                if (regexCheck())
                {
                    if (DatabaseConnector.StartConnection())
                    {
                        if (DatabaseOperation.UserRegister(name, surname, login, password, email))
                        {
                            MessageBox.Show("Pomyślnie zarejestrowano.");
                            DatabaseConnector.CloseConnection();
                            LoginWindow loginWindow = new LoginWindow();
                            loginWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Coś poszło nie tak.");
                        }
                    }
                }
            }

        }

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if( (new Regex(@"^[A-Za-z]+$")).IsMatch(nameTextBox.Text))
            {
                nameTextBox.Foreground = Brushes.Black;
                correctName = true;
            }
            else
            {
                nameTextBox.Foreground = Brushes.Red;
                correctName = false;
            }
        }

        private void surnameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((new Regex(@"^[A-Za-z]+$")).IsMatch(surnameTextBox.Text))
            {
                surnameTextBox.Foreground = Brushes.Black;
                correctSurname = true;
            }
            else
            {
                surnameTextBox.Foreground = Brushes.Red;
                correctSurname = false;
            }
        }

        private void loginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((new Regex(@"^(?=.*[A-Za-z0-9]$)[A-Za-z][A-Za-z\d.-]{0,19}$")).IsMatch(loginTextBox.Text))
            {
                loginTextBox.Foreground = Brushes.Black;
                correctLogin = true;
            }
            else
            {
                loginTextBox.Foreground = Brushes.Red;
                correctLogin = false;
            }
        }

        private void emailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$")).IsMatch(emailTextBox.Text))
            {
                emailTextBox.Foreground = Brushes.Black;
                correctEmail = true;
            }
            else
            {
                emailTextBox.Foreground = Brushes.Red;
                correctEmail = false;
            }
        }

        private void passwordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if ((new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$")).IsMatch(passwordTextBox.Password.ToString()) && 
                (passwordTextBox.Password.ToString() == repeatPasswordTextBox.Password.ToString()))
            {
                passwordTextBox.Foreground = Brushes.Black;
                repeatPasswordTextBox.Foreground = Brushes.Black;
                correctPassword = true;
            }
            else
            {
                passwordTextBox.Foreground = Brushes.Red;
                repeatPasswordTextBox.Foreground = Brushes.Red;
                correctPassword = false;
            }
        }

        private void repeatPasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if ((new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$")).IsMatch(repeatPasswordTextBox.Password.ToString()) &&
                (passwordTextBox.Password.ToString() == repeatPasswordTextBox.Password.ToString()))
            {
                passwordTextBox.Foreground = Brushes.Black;
                repeatPasswordTextBox.Foreground = Brushes.Black;
                correctPassword = true;
            }
            else
            {
                passwordTextBox.Foreground = Brushes.Red;
                repeatPasswordTextBox.Foreground = Brushes.Red;
                correctPassword = false;
            }
        }

        bool regexCheck() 
        {
            String msg = "";
            bool isCorrect = true;
            if (!correctName)
            {
                isCorrect = false;
                msg += "Pole imienia może zawierać tylko litery \n";
            }
            if (!correctSurname)
            {
                isCorrect = false;
                msg += "Pole nazwiska może zawierać tylko litery \n";
            }
            if (!correctLogin)
            {
                isCorrect = false;
                msg += "Pole nazwy użytkownika może zawierać tylko litery i liczby. \n";
            }
            if (!correctPassword)
            {
                isCorrect = false;
                msg += "Pole hasła musi zawierać przynajmniej jedną literę i jedną cyfrę. \n";
                msg += "Pole hasła musi zawierać conajmniej 8 znaków. \n";
                if (passwordTextBox.Password.ToString() != repeatPasswordTextBox.Password.ToString())
                {
                    msg += "Hasła nie są takie same. \n";
                }
            }
            if (!correctEmail)
            {
                isCorrect = false;
                msg += "Podany email jest niepoprawny. \n";
            }
            if (isCorrect)
            {
                return true;
            }
            else
            {
                MessageBox.Show(msg);
                return false;
            }
        }

        bool textBoxNotEmpty()
        {
            String msg = "";
            bool isNotEmpty = true;
            if (nameTextBox.Text == "")
            {
                msg += "Pole imienia jest puste. \n";
                isNotEmpty = false;
            }
            if (surnameTextBox.Text == "")
            {
                msg += "Pole nazwiska jest puste. \n";
                isNotEmpty = false;
            }
            if (loginTextBox.Text == "")
            {
                msg += "Pole nazwy użytkownika jest puste. \n";
                isNotEmpty = false;
            }
            if ((passwordTextBox.Password.ToString() == "") || (repeatPasswordTextBox.Password.ToString() == ""))
            {
                msg += "Pola haseł są puste. \n";
                isNotEmpty = false;
            }
            if (emailTextBox.Text == "")
            {
                msg += "Pole email jest puste. \n";
                isNotEmpty = false;
            }
            if (!isNotEmpty)
            {
                MessageBox.Show(msg);
                return false;
            }
            else 
            {
                return true;
            }

        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            StartWindow startWindow = new StartWindow();
            startWindow.Show();
            this.Close();
        }
    }
}