using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Repositories
{
    class IngredientsRepository
    {
        private string FileLocation = @"..\..\..\Data\ingredients.json";
        private List<MedicationIngredients> ingredients;

        public IngredientsRepository()
        {
            ingredients = new List<MedicationIngredients>();
            ingredients = JsonConvert.DeserializeObject<List<MedicationIngredients>>(File.ReadAllText(FileLocation));
        }

        public void SerializeIngredients()
        {
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(ingredients));
        }

        public List<MedicationIngredients> GetAll()
        {
            return ingredients;
        }

        public void Save(MedicationIngredients newIngredient)
        {
            ingredients.Add(newIngredient);
            SerializeIngredients();
        }

        public void Delete(string name)
        {
            MedicationIngredients ingredient;
            for (int i = 0; i < ingredients.Count; i++)
            {
                ingredient = ingredients[i];
                if (ingredient.IngredientName.Equals(name))
                {
                    ingredients.RemoveAt(i);
                    SerializeIngredients();
                    break;
                }
            }
        }
    }
}

