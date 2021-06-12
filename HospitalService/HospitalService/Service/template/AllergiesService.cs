using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Service.template
{
    public class AllergiesService : UpdateMedicalRecord
    {
        private MedicationIngredients allergie;

        public AllergiesService(MedicationIngredients newAllergie) {
            allergie = newAllergie;
        }
        public override void MakeChanges(MedicalRecord record)
        {
            record.Allergies.Add(allergie);
        }
    }
}
