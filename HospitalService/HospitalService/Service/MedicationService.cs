using HospitalService.Repositories;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace HospitalService.Service
{
    public class MedicationService
    {
        #region Fields

        private MedicationsRepository medications;
        private IngredientsRepository ingredients;
        private MedicineValidationsRepository validationRequests;

        #endregion

        #region Conctructors

        public MedicationService()
        {
            medications = new MedicationsRepository();
            ingredients = new IngredientsRepository();
            validationRequests = new MedicineValidationsRepository();
        }

        #endregion

        #region Functions 

        public void DeleteMedication(string medicineId)
        {
            RemoveValidationRequest(medicineId);
            medications.Delete(medicineId);
        }

        private void RemoveValidationRequest(string medicineId)
        {
            MedicineValidationRequest request;
            for (int i = 0; i < validationRequests.GetAll().Count; i++)
            {
                request = validationRequests.GetAll()[i];
                if (request.MedicineId.Equals(medicineId))
                {
                    validationRequests.GetAll().RemoveAt(i);
                    validationRequests.SerializeValidationRequests();
                    break;
                }
            }
        }

        public void DeleteIngredient(string name)
        {
            DeleteIngredientsFromMedication(name);
            ingredients.Delete(name);
        }

        public void DeleteIngredientsFromMedication(string name)
        {
            Medication med;
            for (int j = 0; j < medications.GetAll().Count; j++)
            {
                med = medications.GetAll()[j];
                if (med.Ingredients.ContainsKey(name))
                {
                    med.Ingredients.Remove(name);
                    UpdateMedication(med);
                }
            }
        }

        public List<Medication> GetAllApproved()
        {
            List<Medication> approvedMeds = new List<Medication>();
            List<Medication> allMeds = GetAll();
            foreach (Medication medication in allMeds)
                if (medication.IsApproved == MedicineStatus.Approved)
                    approvedMeds.Add(medication);
            return approvedMeds;
        }

        public List<Medication> GetForApproval(List<MedicineValidationRequest> validationRequests)
        {
            List<Medication> medicationsForApproval = new List<Medication>();
            List<Medication> allMeds = GetAll();
            foreach (MedicineValidationRequest request in validationRequests)
                medicationsForApproval.Add(allMeds.Find(x => x.Id == request.MedicineId));
            return medicationsForApproval;
        }

        public List<Medication> GetAllAllowed(List<MedicationIngredients> allergies)
        {
            List<Medication> allowedMedications = new List<Medication>();
            List<Medication> allMeds = GetAll();
            foreach (Medication medication in allMeds)
            {
                if (!medication.IsAllergen(allergies))
                    allowedMedications.Add(medication);
            }
            return allowedMedications;
        }

        public List<Medication> GetAll() => medications.GetAll();
        public Medication GetOne(string id) => medications.getOne(id);
        public void Edit(string id, string format, MedicationType type, Dictionary<string, int> ingredients) =>
            medications.Edit(id, format, type, ingredients);
        public void AddMedication(Medication newMedication) => medications.Save(newMedication);
        public void AddIngredient(MedicationIngredients newIngredient) => ingredients.Save(newIngredient);
        public void SerializeMedication() => medications.SerializeMedication();
        public void UpdateMedication(Medication medication) => medications.Update(medication);

        #endregion
    }
}
