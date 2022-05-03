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
            initializeCombos();
            initializeUserInfo();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            StartWindow startWindow = new StartWindow();
            startWindow.Show();
            this.Close();
        }

        void initializeCombos() 
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

        void initializeUserInfo()
        {
            nameLabel.Content = user.getName() + " " + user.getSurname();
            nickLabel.Content = user.getLogin();
        }
    }
}
