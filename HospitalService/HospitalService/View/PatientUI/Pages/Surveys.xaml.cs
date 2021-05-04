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
    /// Interaction logic for Surveys.xaml
    /// </summary>
    public partial class Surveys : Page
    {
        public Patient patient { get; set; }
        public Surveys(Patient p)
        {
            InitializeComponent();
            patient = p;
        }

        private void ChooseDoctor(object sender, RoutedEventArgs e)
        {
            Ankete.Content = new DoctorChoice(patient,this);
        }

        private void DoHospitalSurvey(object sender, RoutedEventArgs e)
        {
            

            HospitalSurveyStorage dss = new HospitalSurveyStorage();
            List<SurveyHospitalPatient> sur = dss.GetAll();
            List<SurveyHospitalPatient> filteredSurveys = sur.Where(su => su.patient.Jmbg.Equals(patient.Jmbg)).ToList();
            filteredSurveys.Sort((a1, a2) => DateTime.Compare(a1.ExecutionTime, a2.ExecutionTime));

            SurveyHospitalPatient lastSurvey = filteredSurveys[filteredSurveys.Count - 1];

            AppointmentStorage s = new AppointmentStorage();
            List<Appointment> a = s.GetAll();
            List<Appointment> filteredAppointments = a.Where(ap => ap.patient.Jmbg.Equals(patient.Jmbg) && ap.EndTime < DateTime.Now && ap.EndTime > lastSurvey.ExecutionTime).ToList();
            int appointments = filteredAppointments.Count;


            int daysApart = (int)(DateTime.Now - lastSurvey.ExecutionTime).TotalDays;
            
            if (appointments >= 4 || daysApart >= 91)
            {
                this.NavigationService.Navigate(new HospitalSurvey(patient));
               
                               
            }
            else {
                this.NavigationService.Navigate(new LastFinishedHospitalSurvey(lastSurvey));

            }


        }
    }
}
