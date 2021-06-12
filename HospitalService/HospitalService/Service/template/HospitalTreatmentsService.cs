using HospitalService.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Service.template
{
    public class HospitalTreatmentsService : UpdateMedicalRecord
    {
        private HospitalTreatment hospitalTreatment;

        public HospitalTreatmentsService(HospitalTreatment newHospitalTreatment) {
            hospitalTreatment = newHospitalTreatment;
        }
        public override void MakeChanges(MedicalRecord record)
        {
            record.HospitalTreatments.Add(hospitalTreatment);
        }
    }
}
