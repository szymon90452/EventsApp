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
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String login,password;
            login = loginBox.Text;
            password = passwordBox.Password.ToString();
            if (DatabaseOperation.userLogin(login, password))
            {
                MessageBox.Show("Zalogowany");
                //Zmień okno na panel
            }
            else 
            {
                MessageBox.Show("Nieprawidłowe dane logowania");
            }
        }
    }
}
