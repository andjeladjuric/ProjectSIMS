using HospitalService.Model;
using HospitalService.Repositories;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Service
{
    public class MedicalRecordService
    {
        private MedicalRecordsRepository repository;
        public MedicalRecordService()
        {
            repository = new MedicalRecordsRepository();
        }

        public MedicalRecord GetOneByPatient(Patient patient)
        {

            MedicalRecord medicalRecordOfPatient = new MedicalRecord();
            List<MedicalRecord> records = GetAll();

            for (int i = 0; i < records.Count; i++)
            {

                medicalRecordOfPatient = records[i];

                if (patient.Jmbg.Equals(medicalRecordOfPatient.Patient.Jmbg))
                {
                    break;
                }

            }
            return medicalRecordOfPatient;

        }

        public List<Prescription> GetPrescriptions(Patient patient)
        {

            MedicalRecord record;
            List<Prescription> prescriptions = new List<Prescription>();
            List<MedicalRecord> records = GetAll();

            for (int i = 0; i < records.Count; i++)
            {

                record = records[i];
                if (record.Patient.Jmbg.Equals(patient.Jmbg))
                {

                    prescriptions = records[i].Prescriptions;
                    break;

                }

            }

            return prescriptions;

        }
        public List<Referral> GetReferrals(Patient patient) {

            MedicalRecord record;
            List<Referral> referrals = new List<Referral>();
            List<MedicalRecord> records = GetAll();

            for (int i = 0; i < records.Count; i++)
            {
                record = records[i];
                if (record.Patient.Jmbg.Equals(patient.Jmbg))
                {
                    referrals = records[i].Referrals;
                    break;
                }
            }
            return referrals;
        }

        public int TakenBeds(string roomId)
        {
            int currentlyTakenBeds = 0;
            List<MedicalRecord> records = GetAll();
            foreach (MedicalRecord record in records)
            {
                foreach (HospitalTreatment treatment in record.HospitalTreatments)
                {
                    if (treatment.RoomId.Equals(roomId))
                        if (DateTime.Compare(treatment.EndTime, DateTime.Now) > 0)
                            currentlyTakenBeds++;
                }
            }
            return currentlyTakenBeds;
        }

        public void AddNewRecord(MedicalRecord record) => repository.Save(record);
        public List<MedicalRecord> GetAll() => repository.GetAll();
        public MedicalRecord GetOne(string Id) => repository.GetOne(Id);
        public void Delete(string Id) => repository.Delete(Id);
        public void UpdateRecord(MedicalRecord record) => repository.Edit(record);
    }
}
