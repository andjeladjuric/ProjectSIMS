﻿
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Storage
{
    public class MedicalRecordStorage
    {
        private String FileLocation = @"..\..\..\Data\medicalrecords.json";
        public List<MedicalRecord> records { get; set; }

        public MedicalRecordStorage()
        {
            records = new List<MedicalRecord>();
            //File.WriteAllText(FileLocation, JsonConvert.SerializeObject(records));
            records = JsonConvert.DeserializeObject<List<MedicalRecord>>(File.ReadAllText(FileLocation));
        }

        public void Save(MedicalRecord newRecord)
        {
            records.Add(newRecord);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(records));
        }

        public List<MedicalRecord> GetAll()
        {
            return records;
        }

        public MedicalRecord GetOne(String id)
        {
            return records.Find(x => x.Id == id);
        }


        public void Delete(String id)
        {
            MedicalRecord record = records.Find(x => x.Id == id);
            records.Remove(record);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(records));
        }

        public void Edit(MedicalRecord mr)
        {
            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].Id == mr.Id)
                {
                    records[i] = mr;
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(records));
        }

    }
}