using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Model
{
    public class Prescription
    {
        public string Medication { get; set; } // Treba napraviti bazu lijekova
        public int HowOften { get; set; }
        public int HowLong { get; set; }
        public string AdditionalInfos { get; set; }
        public DateTime start { get; set; }

        public Prescription(string m, int a, int b, string i, DateTime dt)
        {
            Medication = m;
            HowOften = a;
            HowLong = b;
            AdditionalInfos = i;
            start = dt;
        }
    }
}
