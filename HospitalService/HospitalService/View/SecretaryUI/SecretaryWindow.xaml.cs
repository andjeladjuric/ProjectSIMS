using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HospitalService.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for SecretaryWindow.xaml
    /// </summary>
    public partial class SecretaryWindow : Window
    {

        public string Datum { get; set; }
        public SecretaryWindow(Secretary s)
        {
            InitializeComponent();
            this.DataContext = this;
            
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {

            btnMenu.Visibility = System.Windows.Visibility.Hidden;
            btnBack.Visibility = System.Windows.Visibility.Visible;
            MenuRectangle.Visibility = System.Windows.Visibility.Visible;
            MenuItems.Visibility = System.Windows.Visibility.Visible;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {


            btnMenu.Visibility = System.Windows.Visibility.Visible;
            btnBack.Visibility = System.Windows.Visibility.Hidden;
            MenuRectangle.Visibility = System.Windows.Visibility.Collapsed;
            MenuItems.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Account_Click(object sender, RoutedEventArgs e) { }


        private void Appointments_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new DayAppointments((DateTime)cldSample.SelectedDate).Show();

            }
            catch { MessageBox.Show("Izaberite datum."); }
        }
        private void Patients_Click(object sender, RoutedEventArgs e)
        {
            new Pacijenti().ShowDialog();
        }

        private void Appointments_Selected(object sender, RoutedEventArgs e)
        {
        }

        private void Urgent_Selected(object sender, RoutedEventArgs e)
        {
            new Urgent().ShowDialog();
        }

        private void Patients_Selected(object sender, RoutedEventArgs e)
        {
            new Pacijenti().ShowDialog();
        }


        private void News_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void AddNews_Selected(object sender, RoutedEventArgs e)
        {
            new Doctors().ShowDialog();
        }

        private void Info_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void Help_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void Logout_Selected(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

    }
}
