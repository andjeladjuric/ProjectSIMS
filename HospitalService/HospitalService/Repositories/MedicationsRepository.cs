using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Repositories
{
    class MedicationsRepository
    {
        private String FileLocation = @"..\..\..\Data\meds.json";
        private List<Medication> meds;

        public MedicationsRepository()
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
                    SerializeMedication();
                    break;
                }
            }
        }

        public void Edit(string id, string format, MedicationType type, Dictionary<string, int> ingredients)
        {
            Medication m;
            for (int i = 0; i < meds.Count; i++)
            {
                m = meds[i];
                if (m.Id.Equals(id))
                {
                    m.Format = format;
                    m.Type = type;
                    m.Ingredients = ingredients;
                    SerializeMedication();
                    break;
                }
            }
        }

        public Medication getOne(string id)
        {
            List<Medication> medications = JsonConvert.DeserializeObject<List<Medication>>(File.ReadAllText(FileLocation));
            return medications.Find(x => x.Id == id);
        }

        public void Update(Medication updatedMedication)
        {
            for (int i = 0; i < meds.Count; i++)
                if (meds[i].Id.Equals(updatedMedication.Id))
                    meds[i] = updatedMedication;
            SerializeMedication();
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
    }
}

