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
using HospitalService.Storage;
using Model;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for DoctorChoice.xaml
    /// </summary>
    public partial class DoctorChoice : Page
    {
        public Patient patient { get; set; }
        public Doctor doctor { get; set; }
        public Surveys surveys { get; set; }
        
        public DoctorChoice(Patient p, Surveys s)
        {
            InitializeComponent();
            patient = p;
            surveys = s;
            DoctorStorage ds = new DoctorStorage();
            List<Doctor> ld = ds.GetAll();
            cbDoctors.ItemsSource = ld;
        }

        private void DoDoctorSurvey(object sender, RoutedEventArgs e)
        {
            Doctor d = (Doctor)cbDoctors.SelectedItem;
            surveys.NavigationService.Navigate(new DoctorSurvey(patient,d));
        }
    }
}
