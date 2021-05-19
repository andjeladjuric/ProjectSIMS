using HospitalService.Service;
using HospitalService.View.PatientUI.Pages;
using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private AppointmentsService appointmentService;
        public Patient Patient { get; set; }
        public PatientWindow(Patient patient)
        {
            InitializeComponent();
            this.DataContext = this;
            Patient = patient;
            Main.Content = new ProfileView(Patient);
            appointmentService = new AppointmentsService();

        }

        private void ViewAppointmentsClick(object sender, RoutedEventArgs e)
        {
            
            Main.Content = new ViewAppointment(Patient);
        }

        private void logOutClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void addAppointmentClick(object sender, RoutedEventArgs e)
        {
            
            List<Appointment> notFinishedAppointment = appointmentService.getNotFinishedAppointments(Patient);
           
            if (notFinishedAppointment.Count >= 5)
            {
                MessageBox.Show("Prekoracen maksimalan broj termina koje mozete da zakazete!");
            }
            else
            {

                Main.Content = new PreferencesForAppointment(Patient);
            }
        }

        private void viewPrescriptions(object sender, RoutedEventArgs e)
        {
            Main.Content = new MedicalRecordWithPrescriptions(Patient);
        }

        private void SurveyClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new Surveys(Patient);
        }

        private void viewProfileClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new ProfileView(Patient);
        }

        private void editProfileClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new EditProfile(Patient);
        }

        private void viewMedicalRecordClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new MedicalRecordForPatient(Patient);
        }
    }
}
