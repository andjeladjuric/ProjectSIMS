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
        
        public Patient patient { get; set; }
        public PatientWindow(Patient p)
        {
            InitializeComponent();
            this.DataContext = this;
            patient = p;
            Main.Content = new ProfileView(patient);

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
            AppointmentStorage apst = new AppointmentStorage();
            List<Appointment> la = apst.GetAll();
            List<Appointment> notFinishedAppointment = la.Where(ap => ap.patient.Jmbg.Equals(patient.Jmbg) && ap.StartTime>=DateTime.Now).ToList();
           
            if (notFinishedAppointment.Count >= 5)
            {
                MessageBox.Show("Prekoracen maksimalan broj termina koje mozete da zakazete!");
            }
            else
            {

                Main.Content = new PreferencesForAppointment(patient);
            }
        }

        private void viewPrescriptions(object sender, RoutedEventArgs e)
        {
            Main.Content = new MedicalRecordWithPrescriptions(patient);
        }

        private void SurveyClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new Surveys(patient);
        }

        private void viewProfileClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new ProfileView(patient);
        }

        private void editProfileClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new EditProfile(patient);
        }

        private void viewMedicalRecordClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new MedicalRecordForPatient(patient);
        }
    }
}
