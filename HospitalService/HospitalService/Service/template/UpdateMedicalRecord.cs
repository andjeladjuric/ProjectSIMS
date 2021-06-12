using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Service.template
{
    public abstract class UpdateMedicalRecord
    {
        public MedicalRecordService Service = new MedicalRecordService();

        public void UpdateRecord(String id) {
            MedicalRecord record = GetOne(id);
            MakeChanges(record);
            SaveRecord(record);
        }

        public abstract void MakeChanges(MedicalRecord record);


        public MedicalRecord GetOne(String id)
        {
            return Service.GetOne(id);
        }

        public void SaveRecord(MedicalRecord medicalRecord)
        {
            Service.UpdateRecord(medicalRecord);
        }
    }
}
