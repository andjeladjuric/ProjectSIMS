
using HospitalService.Model;
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

        public MedicalRecord getOneByPatient(Patient patient) {

            MedicalRecord medicalRecordOfPatient=new MedicalRecord();

            for (int i = 0; i < records.Count; i++) {

                medicalRecordOfPatient = records[i];

                if (patient.Jmbg.Equals(medicalRecordOfPatient.Patient.Jmbg)) {
                    break;
                }  
            
            }
            return medicalRecordOfPatient;
        
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


        public List<Prescription> getByPatient(Patient p) {

            MedicalRecord md;
            List<Prescription> rec = new List<Prescription>();

            for (int i = 0; i < records.Count; i++) {

                md = records[i];
                if (md.Patient.Jmbg.Equals(p.Jmbg)) {

                    rec = records[i].Prescriptions;
                    break;

                }

            }

            return rec;

        }

     public int TakenBeds(string roomId)
        {
            int currentlyTakenBeds = 0;
            foreach(MedicalRecord record in records)
            {
                foreach(HospitalTreatment treatment in record.HospitalTreatments)
                {
                    if (treatment.RoomId.Equals(roomId))
                        if (DateTime.Compare(treatment.EndTime, DateTime.Now) > 0)
                            currentlyTakenBeds++;
                }
            }
            return currentlyTakenBeds;
        }

        public void ExtendTreatment(string MedicalRecordId, HospitalTreatment treatment)
        {
            for(int i = 0; i < records.Count; i++)
            {
                if (records[i].Equals(MedicalRecordId))
                {
                    records[i].EditTreatment(treatment);
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(records));
                    break;

                }
            }
        }

    }
}
