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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for PreferencesForAppointment.xaml
    /// </summary>
    public partial class PreferencesForAppointment : Page
    {

        public Patient Patient { get; set; }
        public PreferencesForAppointment(Patient patient)
        {
            InitializeComponent();
            this.DataContext = this;
            Patient = patient;
        }

        private void PreferenceClick(object sender, RoutedEventArgs e)
        {
            if (No.IsChecked == true)
            {
                AddAppointmentToPatient aw = new AddAppointmentToPatient(Patient);
                aw.Show();
            }

            if (Yes.IsChecked == true)
            {
                UrgentAppointment uaw = new UrgentAppointment(Patient);
                uaw.Show();
            }
        }
    }
}
