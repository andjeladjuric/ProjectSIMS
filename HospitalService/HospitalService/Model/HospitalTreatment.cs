using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Model
{
    public class HospitalTreatment
    {
        public DateTime StartDate { get; set; }
        public DateTime EndTime { get; set; }
        public DoctorType Department { get; set; }
        public string RoomId { get; set; }
        public int BedNumber { get; set; }
        public string Reason { get; set; }

        public HospitalTreatment() { }
    }
}
