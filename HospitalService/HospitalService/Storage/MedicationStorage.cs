using HospitalService.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Model
{
    public class MedicationStorage
    {
        private String FileLocation = @"..\..\..\Data\meds.json";
        private List<Medication> meds;

        public MedicationStorage()
        {
            meds = new List<Medication>();
            meds = JsonConvert.DeserializeObject<List<Medication>>(File.ReadAllText(FileLocation));
        }

        public List<Medication> GetAll()
        {
            return meds;
        }
        public void SerializeMedication()
        {
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(meds));
        }

        public void Save(Medication newMedication)
        {
            meds.Add(newMedication);
            SerializeMedication();
        }

        public void Delete(string medicineId)
        {
            Medication m;
            for (int i = 0; i < meds.Count; i++)
            {
                m = meds[i];
                if (m.Id.Equals(medicineId))
                {
                    meds.RemoveAt(i);
                    RemoveValidationRequest(medicineId);
                    SerializeMedication();
                    break;
                }
            }
        }

        private void RemoveValidationRequest(string medicineId)
        {
            MedicineValidationRequest validationRequest;
            MedicineValidationStorage validationStorage = new MedicineValidationStorage();
            for (int i = 0; i < validationStorage.GetAll().Count; i++)
            {
                validationRequest = validationStorage.GetAll()[i];
                if (validationRequest.MedicineId.Equals(medicineId))
                {
                    validationStorage.GetAll().RemoveAt(i);
                    validationStorage.SerializeValidationRequests();
                    break;
                }
            }
        }

        public Medication getOne(string id)
        {
            List<Medication> medications = JsonConvert.DeserializeObject<List<Medication>>(File.ReadAllText(FileLocation));
            return medications.Find(x => x.Id == id);
        }

        public List<Medication> GetForApproval(List<MedicineValidationRequest> validationRequests)
        {
            List<Medication> medicationsForApproval = new List<Medication>();
            foreach (MedicineValidationRequest request in validationRequests)
                medicationsForApproval.Add(meds.Find(x => x.Id == request.MedicineId));
            return medicationsForApproval;
        }

        public void Update(Medication updatedMedication)
        {
            for (int i = 0; i < meds.Count; i++)
                if (meds[i].Id.Equals(updatedMedication.Id))
                    meds[i] = updatedMedication;
            SerializeMedication();
        }

        public List<Medication> GetAllApproved()
        {
            List<Medication> approvedMeds = new List<Medication>();
            foreach (Medication medication in meds)
                if (medication.IsApproved == MedicineStatus.Approved)
                    approvedMeds.Add(medication);
            return approvedMeds;
        }

        public void AddIngredient(string name, int grams, String medicationId)
        {
            meds.Find(x => x.Id == medicationId).Ingredients.Add(name, grams);
            SerializeMedication();
        }

        public void DeleteIngredient(string name, String medicationId)
        {
            meds.Find(x => x.Id == medicationId).Ingredients.Remove(name);
            SerializeMedication();
        }

        public List<Medication> GetAllAllowed(List<MedicationIngredients> allergies)
        {
            List<Medication> alloweMedications = new List<Medication>();
            foreach (Medication medication in meds)
            {
                if (medication.IsAllergen(allergies) == false)
                    alloweMedications.Add(medication);
            }
            return alloweMedications;
        }
    }
}
