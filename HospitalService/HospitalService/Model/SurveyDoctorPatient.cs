using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace HospitalService.Model
{
   public class SurveyDoctorPatient
    {

        public String Communication { get; set; }
        public String Professionalism { get; set; }
        public String Courtesy { get; set; }
        public String CareForPatient { get; set; }
        public String ProvidingInformation { get; set; }
        public String DevotedTime { get; set; }
        public DateTime ExecutionTime { get; set; }
        public Patient patient { get; set; }
        public Doctor doctor { get; set; }
    }
}
