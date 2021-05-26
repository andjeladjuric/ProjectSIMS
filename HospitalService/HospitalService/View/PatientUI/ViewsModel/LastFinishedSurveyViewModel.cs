using System;
using System.Collections.Generic;
using System.Text;
using HospitalService.Model;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
   public class LastFinishedSurveyViewModel:ViewModelPatientClass
    {

        public String Date { get; set; }

        public String Doctor { get; set; }

        public String Communication { get; set; }

        public String Courtesy { get; set; }

        public String Professionalism { get; set; }

        public String CareForPatient { get; set; }

        public String ProvidingInformation { get; set; }

        public String DevotedTime { get; set; }
        public LastFinishedSurveyViewModel(Doctor doctor, SurveyDoctorPatient lastFinishedSurvey) {

            Date = lastFinishedSurvey.ExecutionTime.ToShortDateString();
            Doctor = doctor.Name + " " + doctor.Surname;
            Communication = lastFinishedSurvey.Communication;
            Courtesy = lastFinishedSurvey.Courtesy;
            Professionalism = lastFinishedSurvey.Professionalism;
            CareForPatient = lastFinishedSurvey.CareForPatient;
            ProvidingInformation = lastFinishedSurvey.ProvidingInformation;
            DevotedTime = lastFinishedSurvey.DevotedTime;
            
        
        }
    }
}
