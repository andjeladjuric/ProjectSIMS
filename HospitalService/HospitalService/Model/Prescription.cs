using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Model
{
    public class Prescription
    {
        public string Medication { get; set; } 
        public int HowOften { get; set; }
        public int HowLong { get; set; }
        public string AdditionalInfos { get; set; }
        public DateTime Start { get; set; }

        public Prescription(string medicationName, int howOften, int howLong, string info, DateTime startDate )
        {
            Medication = medicationName;
            HowOften = howOften;
            HowLong = howLong;
            AdditionalInfos = info;
            Start = startDate;
        }
    }
}
