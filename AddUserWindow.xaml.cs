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
    /// Logika interakcji dla klasy AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        bool passwordVisible;
        public AddUserWindow()
        {
            InitializeComponent();
            SetComboValues();
        }

        void SetComboValues()
        {
            privilegesComboBox.Items.Add("user");
            privilegesComboBox.Items.Add("administrator");
            privilegesComboBox.SelectedIndex = 0;
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
            if (DatabaseOperation.UserRegister(name, surname, login, password, email, privileges))
            {
                MessageBox.Show("Użytkownik " + login + " został dodany pomyślnie do bazy.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Coś poszło nie tak.");
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
