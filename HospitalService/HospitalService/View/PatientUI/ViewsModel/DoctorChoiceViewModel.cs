using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using HospitalService.Model;
using HospitalService.Service;
using HospitalService.View.PatientUI.Pages;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
   public class DoctorChoiceViewModel:ValidationBase
    {

        private Doctor selectedDoctor;
        private ObservableCollection<Doctor> doctors;
        public ObservableCollection<Doctor> Doctors
        {
            get { return doctors; }
            set
            {
                doctors = value;
                OnPropertyChanged();
            }
        }
        private Surveys surveys;
        private Patient patient;
        private AppointmentsService appointmentService;
        private DoctorSurveyService doctorSurveyService;
        private DoctorService doctorService;
        public RelayCommand doDoctorSurvey { get; set; }

        public Doctor SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged();
            }
        }

        private void Execute_DoctorSurvey(object obj) {

            this.Validate();
            if (IsValid)
            {
                Appointment lastFinishedAppointment = appointmentService.getLastFinishedAppointment(SelectedDoctor, patient);
                SurveyDoctorPatient lastFinishedSurvey = doctorSurveyService.getLastFinishedDoctorSurvey(SelectedDoctor, patient);

                if (lastFinishedSurvey.ExecutionTime > lastFinishedAppointment.EndTime && lastFinishedSurvey.ExecutionTime < DateTime.Now)
                {

                    surveys.NavigationService.Navigate(new LastFinishedSurvey(SelectedDoctor, lastFinishedSurvey));

                }
                else
                {

                    surveys.NavigationService.Navigate(new DoctorSurvey(patient, SelectedDoctor));

                }
            }

        }
        private bool CanExecute_Command(object obj) {

            return true;
        
        }

        protected override void ValidateSelf()
        {
            if (SelectedDoctor == null) {
                this.ValidationErrors["Doctor"] = "Odaberite doktora.";
            }
        }

        public DoctorChoiceViewModel(Patient patient,Surveys surveys) {
            this.surveys = surveys;
            this.patient = patient;
            appointmentService = new AppointmentsService();
            doctorSurveyService = new DoctorSurveyService();
            doctorService = new DoctorService();
            List<Doctor> doctorsForSurvey = doctorService.GetAll();
            Doctors = new ObservableCollection<Doctor>();
            doctorsForSurvey.ForEach(Doctors.Add);
            doDoctorSurvey = new RelayCommand(Execute_DoctorSurvey,CanExecute_Command);


        }
    }
}
