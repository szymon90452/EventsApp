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
    /// Logika interakcji dla klasy EditEventWindow.xaml
    /// </summary>
    public partial class EditEventWindow : Window
    {
        int eventId;
        public EditEventWindow(int eventId)
        {
            InitializeComponent();
            this.eventId = eventId;
            GetEventData();
            AddTimeToCombo();
        }

        void AddTimeToCombo()
        {
            for (int i = 0; i <= 24; i++)
            { 
                hourComboBox.Items.Add(i.ToString());
            }
            hourComboBox.SelectedValue="16";

            for (int i = 0; i <= 60; i++)
            {
                minuteComboBox.Items.Add(i.ToString());
            }
            minuteComboBox.SelectedValue = "0";
        }

        void GetEventData()
        {
            DatabaseConnector.StartConnection();
            String query = $"SELECT * FROM events WHERE id={eventId};";
            try
            {
                MySqlCommand cmd = new(query, DatabaseConnector.connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    this.Title = "Wydarzenie: " + reader.GetString(1);
                    titleTextBox.Text = reader.GetString(1);
                    descriptionTextBox.Text = reader.GetString(2);
                    datePicker.SelectedDate = reader.GetDateTime(4);
                    idLabel.Content = reader.GetString(0);
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
            String title = titleTextBox.Text;
            String description = descriptionTextBox.Text;
            String date = datePicker.SelectedDate.Value.Date.Year+"-"+ datePicker.SelectedDate.Value.Date.Month+"-"+ datePicker.SelectedDate.Value.Date.Day;
            String time = hourComboBox.SelectedValue + ":" + minuteComboBox.SelectedValue + ":00";

            

            DatabaseConnector.StartConnection();
            String query = $"UPDATE Events SET title='{title}',description='{description}', date_of_event='{date}', time_of_event='{time}' WHERE id={eventId};";
            try
            {
                MySqlCommand cmd = new(query, DatabaseConnector.connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Wydarzenie " + title + " zostało zmodyfikowane pomyślnie.");
                this.Close();
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());
            }
            DatabaseConnector.CloseConnection();
        }
    }
}
