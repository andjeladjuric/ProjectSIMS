using HospitalService.View.PatientUI.Pages;
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

namespace HospitalService.View.PatientUI
{
    /// <summary>
    /// Interaction logic for PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        
        public Patient patient { get; set; }
        public PatientWindow(Patient pac)
        {
            InitializeComponent();
            this.DataContext = this;
            patient = pac;
            
        }

        private void ViewAppointmentsClick(object sender, RoutedEventArgs e)
        {
            
            Main.Content = new ViewAppointment(patient);
        }

        private void logOutClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void addAppointmentClick(object sender, RoutedEventArgs e)
        {
            
            Main.Content = new PreferencesForAppointment(patient);
        }

        private void viewPrescriptions(object sender, RoutedEventArgs e)
        {
            Main.Content = new MedicalRecordWithPrescriptions(patient);
        }
    }
}
