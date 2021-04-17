using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Model
{
    public class Diagnosis
    {
        public string Illness { get; set; }
        public string Symptoms { get; set; }
        public DateTime Datum { get; set; }
        public string Anamnesis { get; set; }

        public Diagnosis(string i, string s, DateTime d, string a)
        {
            Illness = i;
            Symptoms = s;
            Datum = d;
            Anamnesis = a;
        }
    }
}
