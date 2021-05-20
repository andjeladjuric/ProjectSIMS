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

        public void deleteAllergie(MedicationIngredients allergie)
        {
            for (int i = 0; i < Allergies.Count; i++)
                if (Allergies[i].IngredientName.Equals(allergie.IngredientName))
                {
                    Allergies.RemoveAt(i);
                    break;
                }
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

    }
}
