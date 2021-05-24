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
    /// Interaction logic for DoctorChoice.xaml
    /// </summary>
    public partial class DoctorChoice : Page
    {
        private AppointmentsService appointmentService;
        private DoctorSurveyService doctorSurveyService;
        public Patient surveyorPatient { get; set; }
        public Surveys pageShowingSurveys { get; set; }
        
        public DoctorChoice(Patient patient, Surveys pageSurveys)
        {
            InitializeComponent();
            surveyorPatient = patient;
            pageShowingSurveys = pageSurveys;
            DoctorStorage doctorStorage = new DoctorStorage();
            List<Doctor> surveyedDoctors = doctorStorage.GetAll();
            cbSurveyedDoctors.ItemsSource = surveyedDoctors;
            appointmentService = new AppointmentsService();
            doctorSurveyService = new DoctorSurveyService();
        }

        private void DoDoctorSurvey(object sender, RoutedEventArgs e)
        {
            Doctor chosenDoctor = (Doctor)cbSurveyedDoctors.SelectedItem;
            Appointment lastFinishedAppointment = appointmentService.getLastFinishedAppointment(chosenDoctor,surveyorPatient);
            SurveyDoctorPatient lastFinishedSurvey = doctorSurveyService.getLastFinishedDoctorSurvey(chosenDoctor,surveyorPatient);

            if (lastFinishedSurvey.ExecutionTime > lastFinishedAppointment.EndTime && lastFinishedSurvey.ExecutionTime < DateTime.Now)
            {

                pageShowingSurveys.NavigationService.Navigate(new LastFinishedSurvey(chosenDoctor,lastFinishedSurvey));
                
            }
            else {

                pageShowingSurveys.NavigationService.Navigate(new DoctorSurvey(surveyorPatient, chosenDoctor));

            }

        }
    }
}
