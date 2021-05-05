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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HospitalService.Model;
using HospitalService.Storage;
using Model;
using Storage;

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
            AppointmentStorage s= new AppointmentStorage();
            List<Appointment> a = s.GetAll();
            List<Appointment> filteredAppointments = a.Where(ap => ap.doctor.Jmbg.Equals(d.Jmbg) && ap.patient.Jmbg.Equals(patient.Jmbg) && ap.EndTime < DateTime.Now).ToList();
            filteredAppointments.Sort((t1, t2) => DateTime.Compare(t1.EndTime, t2.EndTime));

            Appointment lastFinished = filteredAppointments[filteredAppointments.Count - 1];

            DoctorSurveyStorage dss = new DoctorSurveyStorage();
            List<SurveyDoctorPatient> sur = dss.GetAll();
            List<SurveyDoctorPatient> filteredSurveys = sur.Where(su => su.doctor.Jmbg.Equals(d.Jmbg) && su.patient.Jmbg.Equals(patient.Jmbg)).ToList();
            filteredSurveys.Sort((a1, a2) => DateTime.Compare(a1.ExecutionTime, a2.ExecutionTime));

            SurveyDoctorPatient lastSurvey = filteredSurveys[filteredSurveys.Count - 1];

            if (lastSurvey.ExecutionTime > lastFinished.EndTime && lastSurvey.ExecutionTime < DateTime.Now)
            {

                surveys.NavigationService.Navigate(new LastFinishedSurvey(d,lastSurvey));
                
            }
            else {

                surveys.NavigationService.Navigate(new DoctorSurvey(patient, d));

            }

        }
    }
}
