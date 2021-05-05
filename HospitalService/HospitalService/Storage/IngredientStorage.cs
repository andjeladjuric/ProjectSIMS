using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Storage
{
    public class IngredientStorage
    {
        private string FileLocation = @"..\..\..\Data\ingredients.json";
        private List<MedicationIngredients> ingredients;

        public IngredientStorage()
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
            MedicationIngredients m;
            MedicationStorage medStorage = new MedicationStorage();
            for (int i = 0; i < ingredients.Count; i++)
            {
                m = ingredients[i];
                if (m.IngredientName.Equals(name))
                {
                    foreach (Medication med in medStorage.GetAll())
                    {
                        if (med.Ingredients.ContainsKey(name)){
                            med.Ingredients.Remove(name);
                            medStorage.SerializeMedication();
                            break;
                        }
                    }
                    ingredients.RemoveAt(i);
                    SerializeIngredients();
                    break;
                }
            }
        }
    }
}
