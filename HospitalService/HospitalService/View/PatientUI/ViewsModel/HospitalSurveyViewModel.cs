using System;
using System.Collections.Generic;
using System.Text;
using HospitalService.Model;
using HospitalService.Service;
using HospitalService.View.PatientUI.Pages;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
    public class HospitalSurveyViewModel : ViewModelPatientClass
    {
        public bool NStaffExpertiseChecked { get; set; }
        public bool DZStaffExpertiseChecked { get; set; }
        public bool ZStaffExpertiseChecked { get; set; }
        public bool NCourtesyChecked { get; set; }
        public bool DZCourtesyChecked { get; set; }
        public bool ZCourtesyChecked { get; set; }
        public bool NTimelinessChecked { get; set; }
        public bool DZTimelinessChecked { get; set; }
        public bool ZTimelinessChecked { get; set; }
        public bool NHygieneChecked { get; set; }
        public bool DZHygieneChecked { get; set; }
        public bool ZHygieneChecked { get; set; }
        public bool NQualityOfServiceChecked { get; set; }
        public bool DZQualityOfServiceChecked { get; set; }
        public bool ZQualityOfServiceChecked { get; set; }
        public bool NServicePricesChecked { get; set; }
        public bool DZServicePricesChecked { get; set; }
        public bool ZServicePricesChecked { get; set; }
        public RelayCommand confirmHospitalSurvey { get; set; }
        public RelayCommand cancelHospitalSurvey { get; set; }
        private Patient patient;
        private HospitalSurvey hospitalSurvey;
        private HospitalSurveyService hospitalSurveyService;

        private void Execute_ConfirmHospitalSurvey(object obj) {

            String ratingForStaffExpertise;
            if (NStaffExpertiseChecked == true)
            {
                ratingForStaffExpertise = "Nezadovoljan/na";
            }
            else if (DZStaffExpertiseChecked == true)
            {
                ratingForStaffExpertise = "Djelimicno zadovoljan/na";
            }
            else
            {
                ratingForStaffExpertise = "Zadovoljan/na";
            }

            String ratingForCourtesy;
            if (NCourtesyChecked == true)
            {
                ratingForCourtesy = "Nezadovoljan/na";
            }
            else if (DZCourtesyChecked == true)
            {
                ratingForCourtesy = "Djelimicno zadovoljan/na";
            }
            else
            {
                ratingForCourtesy = "Zadovoljan/na";
            }

            String ratingForTimeliness;
            if (NTimelinessChecked == true)
            {
                ratingForTimeliness = "Nezadovoljan/na";
            }
            else if (DZTimelinessChecked == true)
            {
                ratingForTimeliness = "Djelimicno zadovoljan/na";
            }
            else
            {
                ratingForTimeliness = "Zadovoljan/na";
            }

            String ratingForHygiene;
            if (NHygieneChecked == true)
            {
                ratingForHygiene = "Nezadovoljan/na";
            }
            else if (DZHygieneChecked == true)
            {
                ratingForHygiene = "Djelimicno zadovoljan/na";
            }
            else
            {
                ratingForHygiene = "Zadovoljan/na";
            }

            String ratingForQualityOfService;
            if (NQualityOfServiceChecked == true)
            {
                ratingForQualityOfService = "Nezadovoljan/na";
            }
            else if (DZQualityOfServiceChecked == true)
            {
                ratingForQualityOfService = "Djelimicno zadovoljan/na";
            }
            else
            {
                ratingForQualityOfService = "Zadovoljan/na";
            }

            String ratingForServicePrices;
            if (NServicePricesChecked == true)
            {
                ratingForServicePrices = "Nezadovoljan/na";
            }
            else if (DZServicePricesChecked == true)
            {
                ratingForServicePrices = "Djelimicno zadovoljan/na";
            }
            else
            {
                ratingForServicePrices = "Zadovoljan/na";
            }

            SurveyHospitalPatient newSurvey = new SurveyHospitalPatient { StaffExpertise = ratingForStaffExpertise, StaffCourtesy = ratingForCourtesy, WaitingForReception = ratingForTimeliness, RoomHygiene = ratingForHygiene, QualityOfService = ratingForQualityOfService, ServicePrices = ratingForServicePrices, patient = patient, ExecutionTime = DateTime.Now };
            hospitalSurveyService.saveHospitalSurvey(newSurvey);
            hospitalSurvey.NavigationService.Navigate(new Surveys(patient));

        }
        private void Execute_CancelHospitalSurvey(object obj) {

            hospitalSurvey.NavigationService.Navigate(new Surveys(patient));
        }
        private bool CanExecute_Command(object obj) {
            return true;
        }
        public HospitalSurveyViewModel(Patient patient,HospitalSurvey hospitalSurvey) {
            this.patient = patient;
            this.hospitalSurvey = hospitalSurvey;
            hospitalSurveyService = new HospitalSurveyService();
            confirmHospitalSurvey = new RelayCommand(Execute_ConfirmHospitalSurvey,CanExecute_Command);
            cancelHospitalSurvey = new RelayCommand(Execute_CancelHospitalSurvey, CanExecute_Command);

        }
    }
}
