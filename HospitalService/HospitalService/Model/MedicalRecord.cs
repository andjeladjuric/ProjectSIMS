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
        public List<Referral> Referrals { get; set; }

        public MedicalRecord() {
            Diagnoses = new List<Diagnosis>();
            Prescriptions = new List<Prescription>(); ;
        }

        public MedicalRecord(string s, List<Diagnosis> ds, List<Prescription> ps)
        {
            Id = s;
            Diagnoses = ds;
            Prescriptions = ps;
        }

        public List<Diagnosis> editDignosis(Diagnosis d)
        {
            Diagnosis diagnosis;
            for (int i = 0; i < Diagnoses.Count; i++)
            {
                diagnosis = Diagnoses[i];
                if (diagnosis.Illness.Equals(d.Illness) && DateTime.Compare(diagnosis.Datum, d.Datum) == 0)
                {
                    Diagnoses[i] = d;
                    break;
                }
            }
            return Diagnoses;
        }

    }
}
