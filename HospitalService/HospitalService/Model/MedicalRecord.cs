using HospitalService.Model;
using Model;
using System;
using System.Collections.Generic;

namespace Model
{
    public class MedicalRecord
    {
        public String Id { get; set; }
        public Patient Patient { get; set; }
        public List<Diagnosis> Diagnoses { get; set; }
        public List<Prescription> Prescriptions { get; set; }

        public MedicalRecord() { }

        public MedicalRecord(string s, List<Diagnosis> ds, List<Prescription> ps)
        {
            Id = s;
            Diagnoses = ds;
            Prescriptions = ps;
        }

    }
}
