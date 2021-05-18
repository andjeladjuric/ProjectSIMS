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
using HospitalService.Service;
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
        private HospitalSurveyService hospitalSurveyService;
        private AppointmentsService appointmentsService;
        public Patient surveyor { get; set; }
        public Surveys(Patient patient)
        {
            InitializeComponent();
            surveyor = patient;
            hospitalSurveyService = new HospitalSurveyService();
            appointmentsService = new AppointmentsService();
        }

        private void ChooseDoctor(object sender, RoutedEventArgs e)
        {
            SurveyFrame.Content = new DoctorChoice(surveyor,this);
        }

        private void DoHospitalSurvey(object sender, RoutedEventArgs e)
        {

            SurveyHospitalPatient lastFinishedHospitalSurvey = hospitalSurveyService.getLastFinishedSurvey(surveyor);
            int appointments = appointmentsService.getNumberOfAppointmentsAfterLastFinishedSurvey(lastFinishedHospitalSurvey, surveyor);
            int daysApart = (int)(DateTime.Now - lastFinishedHospitalSurvey.ExecutionTime).TotalDays;

            if (isSurveyAvailable(appointments, daysApart))
            {
                this.NavigationService.Navigate(new HospitalSurvey(surveyor));


            }
            else
            {
                this.NavigationService.Navigate(new LastFinishedHospitalSurvey(lastFinishedHospitalSurvey));

            }


        }

        private static bool isSurveyAvailable(int appointments, int daysApart)
        {
            return appointments >= 4 || daysApart >= 91;
        }

    }
}
