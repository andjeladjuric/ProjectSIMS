using HospitalService.Repositories;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

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

        private void DeleteIngredientsFromMedication(string name)
        {
            MedicationIngredients ingredient;
            for (int i = 0; i < ingredients.GetAll().Count; i++)
            {
                ingredient = ingredients.GetAll()[i];
                if (ingredient.IngredientName.Equals(name))
                {
                    foreach (Medication med in medications.GetAll())
                    {
                        if (med.Ingredients.ContainsKey(name))
                        {
                            med.Ingredients.Remove(name);
                            medications.SerializeMedication();
                            break;
                        }
                    }
                }
            }
        }

        public List<Medication> GetAll() => medications.GetAll();
        public Medication GetOne(string id) => medications.getOne(id);
        public void Edit(string id, string format, MedicationType type, Dictionary<string, int> ingredients) =>
            medications.Edit(id, format, type, ingredients);
        public void AddMedication(Medication newMedication) => medications.Save(newMedication);
        public void AddIngredient(MedicationIngredients newIngredient) => ingredients.Save(newIngredient);
        public void SerializeMedication() => medications.SerializeMedication();

        #endregion
    }
}
