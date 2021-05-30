using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Navigation;
using HospitalService.Model;
using HospitalService.Service;
using HospitalService.View.PatientUI.Pages;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
  public  class SurveysViewModel:ViewModelPatientClass
    {

        public RelayCommand doHospitalSurvey { get; set; }

        public RelayCommand doDoctorSurvey { get; set; }
        private Patient patient;
        private NavigationService navigationService;
        private Surveys surveys;
        private HospitalSurveyService hospitalSurveyService;
        private AppointmentsService appointmentsService;

        private void Execute_DoctorSurvey(object obj) {

            this.navigationService.Navigate(new DoctorChoice(patient,surveys));
        }

        private void Execute_HospitalSurvey(object obj) {


            SurveyHospitalPatient lastFinishedHospitalSurvey = hospitalSurveyService.getLastFinishedSurvey(patient);
            int appointments = appointmentsService.getNumberOfAppointmentsAfterLastFinishedSurvey(lastFinishedHospitalSurvey, patient);
            int daysApart = (int)(DateTime.Now - lastFinishedHospitalSurvey.ExecutionTime).TotalDays;

            if (isSurveyAvailable(appointments, daysApart))
            {
                surveys.NavigationService.Navigate(new HospitalSurvey(patient));


            }
            else
            {
                surveys.NavigationService.Navigate(new LastFinishedHospitalSurvey(lastFinishedHospitalSurvey));

            }


        }

        private static bool isSurveyAvailable(int appointments, int daysApart)
        {
            return appointments >= 4 || daysApart >= 91;
        }

        private bool CanExecute_Command(object obj) {

            return true;
        }
        public SurveysViewModel(Patient patient,NavigationService navigationService, Surveys surveys) {
            this.patient = patient;
            this.navigationService = navigationService;
            this.surveys = surveys;
            hospitalSurveyService = new HospitalSurveyService();
            appointmentsService = new AppointmentsService();
            doDoctorSurvey = new RelayCommand(Execute_DoctorSurvey,CanExecute_Command);
            doHospitalSurvey = new RelayCommand(Execute_HospitalSurvey,CanExecute_Command);


        }
    }
}
