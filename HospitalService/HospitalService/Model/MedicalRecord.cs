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
        public List<MedicationIngredients> Allergies { get; set; }
        public List<HospitalTreatment> HospitalTreatments { get; set; }

        public MedicalRecord() {
            Diagnoses = new List<Diagnosis>();
            Prescriptions = new List<Prescription>();
            Referrals = new List<Referral>();
            Allergies = new List<MedicationIngredients>();
            HospitalTreatments = new List<HospitalTreatment>();
        }

        public MedicalRecord(string s, List<Diagnosis> ds, List<Prescription> ps)
        {
            Id = s;
            Diagnoses = ds;
            Prescriptions = ps;
        }


        public bool AlreadyExists(MedicationIngredients allergie)
        {
            for (int i = 0; i < Allergies.Count; i++)
            {
                if (Allergies[i].IngredientName.Equals(allergie.IngredientName))
                    return true;
            }
            return false;
        }


        public void EditTreatment(HospitalTreatment treatment)
        {
            for(int i = 0; i < HospitalTreatments.Count; i++)
            {
                if (HospitalTreatments[i] == treatment)
                {
                    HospitalTreatments[i] = treatment;
                    break;
                }
            }
        }

    }
}
