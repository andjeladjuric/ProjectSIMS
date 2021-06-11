using System;
using System.Collections.Generic;
using System.Text;
using HospitalService.Model;
using HospitalService.Service;
using HospitalService.View.PatientUI.Pages;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
  public  class DoctorSurveyViewModel:ValidationBase
    {

        public bool NCommunicationCheched { get; set; }
        public bool DZCommunicationCheched { get; set; }
        public bool ZCommunicationCheched { get; set; }
        public bool NCourtesyCheched { get; set; }
        public bool DZCourtesyCheched { get; set; }
        public bool ZCourtesyCheched { get; set; }
        public bool NProfessionalismChecked { get; set; }
        public bool DZProfessionalismChecked { get; set; }
        public bool ZProfessionalismChecked { get; set; }
        public bool NDoctorCareChecked { get; set; }
        public bool DZDoctorCareChecked { get; set; }
        public bool ZDoctorCareChecked { get; set; }
        public bool NProvidingInformationChecked { get; set; }
        public bool DZProvidingInformationChecked { get; set; }
        public bool ZProvidingInformationChecked { get; set; }
        public bool NDevotedTimeChecked { get; set; }
        public bool DZDevotedTimeChecked { get; set; }
        public bool ZDevotedTimeChecked { get; set; }
        public String SurveyedDoctor { get; set; }
        public RelayCommand confirmDoctorSurvey { get; set; }
        public RelayCommand cancelDoctorSurvey { get; set; }
        private Patient patient;
        private Doctor doctor;
        private DoctorSurvey doctorSurvey;
        private DoctorSurveyService doctorSurveyService;

        private void Execute_ConfirmDoctorSurvey(object obj) {

            this.Validate();
            if (IsValid)
            {
                String ratingForCommunication;
                if (NCommunicationCheched == true)
                {
                    ratingForCommunication = "Nezadovoljan/na";
                }
                else if (DZCommunicationCheched == true)
                {
                    ratingForCommunication = "Djelimicno zadovoljan/na";
                }
                else
                {
                    ratingForCommunication = "Zadovoljan/na";
                }

                String ratingForCourtesy;
                if (NCourtesyCheched == true)
                {
                    ratingForCourtesy = "Nezadovoljan/na";
                }
                else if (DZCourtesyCheched == true)
                {
                    ratingForCourtesy = "Djelimicno zadovoljan/na";
                }
                else
                {
                    ratingForCourtesy = "Zadovoljan/na";
                }

                String ratingForProfessionalism;
                if (NProfessionalismChecked == true)
                {
                    ratingForProfessionalism = "Nezadovoljan/na";
                }
                else if (DZProfessionalismChecked == true)
                {
                    ratingForProfessionalism = "Djelimicno zadovoljan/na";
                }
                else
                {
                    ratingForProfessionalism = "Zadovoljan/na";
                }

                String ratingForDoctorCare;
                if (NDoctorCareChecked == true)
                {
                    ratingForDoctorCare = "Nezadovoljan/na";
                }
                else if (DZDoctorCareChecked == true)
                {
                    ratingForDoctorCare = "Djelimicno zadovoljan/na";
                }
                else
                {
                    ratingForDoctorCare = "Zadovoljan/na";
                }

                String ratingForProvidingInformation;
                if (NProvidingInformationChecked == true)
                {
                    ratingForProvidingInformation = "Nezadovoljan/na";
                }
                else if (DZProvidingInformationChecked == true)
                {
                    ratingForProvidingInformation = "Djelimicno zadovoljan/na";
                }
                else
                {
                    ratingForProvidingInformation = "Zadovoljan/na";
                }

                String ratingForDevotedTime;
                if (NDevotedTimeChecked == true)
                {
                    ratingForDevotedTime = "Nezadovoljan/na";
                }
                else if (DZDevotedTimeChecked == true)
                {
                    ratingForDevotedTime = "Djelimicno zadovoljan/na";
                }
                else
                {
                    ratingForDevotedTime = "Zadovoljan/na";
                }

                SurveyDoctorPatient newSurvey = new SurveyDoctorPatient { Communication = ratingForCommunication, Courtesy = ratingForCourtesy, Professionalism = ratingForProfessionalism, CareForPatient = ratingForDoctorCare, ProvidingInformation = ratingForProvidingInformation, DevotedTime = ratingForDevotedTime, doctor = doctor, patient = patient, ExecutionTime = DateTime.Now };
                doctorSurveyService.saveDoctorSurvey(newSurvey);
                doctorSurvey.NavigationService.Navigate(new Surveys(patient));
            }
        }
        private void Execute_CancelDoctorSurvey(object obj) {

            doctorSurvey.NavigationService.Navigate(new Surveys(patient));

        }
        private bool CanExecute_Command(object obj) {
            return true;
        }

        protected override void ValidateSelf()
        {
            if (NCommunicationCheched == false && DZCommunicationCheched == false && ZCommunicationCheched == false) {

                this.ValidationErrors["Communication"] = "Ocijenite nacin komunikacije sa pacijentom.";

            }
            if (NCourtesyCheched == false && DZCourtesyCheched == false && ZCourtesyCheched == false)
            {

                this.ValidationErrors["Courtesy"] = "Ocijenite ljubaznost doktora.";

            }
            if (NProfessionalismChecked == false && DZProfessionalismChecked == false && ZProfessionalismChecked == false)
            {

                this.ValidationErrors["Professionalism"] = "Ocijenite strucnost doktora.";

            }
            if (NDoctorCareChecked == false && DZDoctorCareChecked == false && ZDoctorCareChecked == false)
            {

                this.ValidationErrors["DoctorCare"] = "Ocijenite doktorovu brigu o pacijentu.";

            }
            if (NProvidingInformationChecked == false && DZProvidingInformationChecked == false && ZProvidingInformationChecked == false)
            {

                this.ValidationErrors["ProvidingInformation"] = "Ocijenite pruzanje informacija pacijentu.";

            }
            if (NDevotedTimeChecked == false && DZDevotedTimeChecked == false && ZDevotedTimeChecked == false)
            {

                this.ValidationErrors["DevotedTime"] = "Ocijenite vrijeme posveceno pacijentu.";

            }

        }

        public DoctorSurveyViewModel(Patient patient,Doctor doctor, DoctorSurvey doctorSurvey) {
            this.patient = patient;
            this.doctor = doctor;
            this.doctorSurvey = doctorSurvey;
            doctorSurveyService = new DoctorSurveyService();
            SurveyedDoctor=doctor.Name + " " + doctor.Surname;
            confirmDoctorSurvey = new RelayCommand(Execute_ConfirmDoctorSurvey,CanExecute_Command);
            cancelDoctorSurvey = new RelayCommand(Execute_CancelDoctorSurvey,CanExecute_Command);

        }




    }
}
