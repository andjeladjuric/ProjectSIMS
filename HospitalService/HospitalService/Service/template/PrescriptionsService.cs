using HospitalService.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Service.template
{
    public class PrescriptionsService : UpdateMedicalRecord
    {
        private Prescription prescription;

        public PrescriptionsService(Prescription newPrescription) {
            prescription = newPrescription;
        }

        public override void MakeChanges(MedicalRecord record)
        {
            record.Prescriptions.Add(prescription);
        }
    }
}
