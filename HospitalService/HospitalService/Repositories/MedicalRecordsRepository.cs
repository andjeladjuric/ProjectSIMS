using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Repositories
{
    class MedicalRecordsRepository
    {
        private String FileLocation = @"..\..\..\Data\medicalrecords.json";
        public List<MedicalRecord> records { get; set; }

        public MedicalRecordsRepository()
        {
            records = new List<MedicalRecord>();
            records = JsonConvert.DeserializeObject<List<MedicalRecord>>(File.ReadAllText(FileLocation));
        }

        public void SerializeMedicalRecords()
        {
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(records));
        }

        public void Save(MedicalRecord newRecord)
        {
            records.Add(newRecord);
            SerializeMedicalRecords();
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
            SerializeMedicalRecords();
        }

        public void Edit(MedicalRecord medicalRecord)
        {
            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].Id == medicalRecord.Id)
                {
                    records[i] = medicalRecord;
                    break;
                }
            }
            SerializeMedicalRecords();
        }
    }
}

