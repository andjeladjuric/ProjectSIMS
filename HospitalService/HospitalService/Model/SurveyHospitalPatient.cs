using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace HospitalService.Model
{
    public class SurveyHospitalPatient
    {

        public String StaffExpertise { get; set; }
        public String StaffCourtesy { get; set; }
        public String WaitingForReception { get; set; }
        public String RoomHygiene { get; set; }
        public String QualityOfService { get; set; }
        public String ServicePrices { get; set; }
        public Patient patient { get; set; }
        public DateTime ExecutionTime { get; set; }

    }
}
