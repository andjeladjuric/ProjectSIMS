using System;
using System.Collections.Generic;
using System.Text;
using HospitalService.Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
    
   public class LastFinishedHospitalSurveyViewModel: ViewModelPatientClass
    {

        public String Date { get; set; }

        public String StaffExpertise { get; set; }
        public String StaffCourtesy { get; set; }
        public String WaitingForReception { get; set; }
        public String Hygiene { get; set; }
        public String QualityOfService { get; set; }
        public String ServicePrices { get; set; }
        public LastFinishedHospitalSurveyViewModel(SurveyHospitalPatient lastFinishedSurvey)
        {

            Date = lastFinishedSurvey.ExecutionTime.ToShortDateString();
            StaffExpertise = lastFinishedSurvey.StaffExpertise;
            StaffCourtesy = lastFinishedSurvey.StaffCourtesy;
            WaitingForReception = lastFinishedSurvey.WaitingForReception;
            Hygiene = lastFinishedSurvey.RoomHygiene;
            QualityOfService = lastFinishedSurvey.QualityOfService;
            ServicePrices = lastFinishedSurvey.ServicePrices;


        }
    }
}
