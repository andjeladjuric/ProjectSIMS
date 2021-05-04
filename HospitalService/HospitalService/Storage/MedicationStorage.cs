﻿using HospitalService.Storage;
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
            for(int i = 0; i < validationStorage.GetAll().Count; i++)
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

    }
}