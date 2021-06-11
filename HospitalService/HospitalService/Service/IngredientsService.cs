using HospitalService.Repositories;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Service
{
    class IngredientsService
    {
        private IngredientsRepository repository;
        public IngredientsService()
        {
            repository = new IngredientsRepository();
        }

        public List<MedicationIngredients> GetNewAllergens(List<MedicationIngredients> allergies)
        {
            List<MedicationIngredients> allergens = new List<MedicationIngredients>();
            foreach (MedicationIngredients ingredient in GetAll())
                if (allergies.Find(x => x.IngredientName == ingredient.IngredientName) == null)
                    allergens.Add(ingredient);
            return allergens;
        }

        public List<MedicationIngredients> GetAll() => repository.GetAll();
        public MedicationIngredients GetOne(string name) => repository.GetOne(name);
        public void Save(MedicationIngredients newIngredient) => repository.Save(newIngredient);
        public void Delete(string name) => repository.Delete(name);


    }
}
