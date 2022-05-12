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
    /// Logika interakcji dla klasy AddEventWindow.xaml
    /// </summary>
    public partial class AddEventWindow : Window
    {
        public AddEventWindow()
        {
            InitializeComponent();
            AddTimeToCombo();
        }

        void AddTimeToCombo()
        {
            for (int i = 0; i <= 24; i++)
            {
                hourComboBox.Items.Add(i.ToString());
            }
            hourComboBox.SelectedValue = "16";

            for (int i = 0; i <= 60; i++)
            {
                minuteComboBox.Items.Add(i.ToString());
            }
            minuteComboBox.SelectedValue = "0";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String title = titleTextBox.Text;
            String description = descriptionTextBox.Text;
            String date = datePicker.SelectedDate.Value.Date.Year + "-" + datePicker.SelectedDate.Value.Date.Month + "-" + datePicker.SelectedDate.Value.Date.Day;
            String time = hourComboBox.SelectedValue + ":" + minuteComboBox.SelectedValue + ":00";

            DatabaseConnector.StartConnection();
            try
            {
                String query = $"INSERT INTO events (title,description,date_of_event,time_of_event) VALUES ('{title}','{description}','{date}','{time}');";
                try
                {
                    MySqlCommand cmd = new(query, DatabaseConnector.connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show($"Wydarzenie {title} zostało dodane do bazy.");
                    this.Close();
                }
                catch (Exception)
                {
                    //MessageBox.Show(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            DatabaseConnector.CloseConnection();
        }
    }
}
