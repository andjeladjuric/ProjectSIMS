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
using HospitalService.View.PatientUI.ViewsModel;
using Model;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for UrgentPatientAppointment.xaml
    /// </summary>
    public partial class UrgentPatientAppointment : Page
    {
        private UrgentPatientAppointmentViewModel viewModel;
        public UrgentPatientAppointment(Patient patient, PreferencesForAppointment preferencesForAppointment)
        {

            InitializeComponent();
            viewModel = new UrgentPatientAppointmentViewModel(patient,preferencesForAppointment);
            this.DataContext = viewModel;
        }
    }
}
