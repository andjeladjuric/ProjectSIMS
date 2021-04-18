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


        public SecretaryWindow(Secretary s)
        {
            InitializeComponent();
            this.DataContext = this;
            
        }

        
       

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }


        private void Appointments_Click(object sender, RoutedEventArgs e)
        {
            Appointments appointments = new Appointments();
            appointments.ShowDialog();

        }

        private void Patients_Click(object sender, RoutedEventArgs e)
        {
            Pacijenti patients = new Pacijenti();
            patients.Show();
        }
    }
}
