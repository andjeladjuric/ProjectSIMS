using HospitalService.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Service.template
{
    class DiagnosesService : UpdateMedicalRecord
    {
        private Diagnosis diagnosis;

        public DiagnosesService(Diagnosis addedDiagnosis) {
            diagnosis = addedDiagnosis;
        }

        public override void MakeChanges(MedicalRecord record)
        {
            record.Diagnoses.Add(diagnosis);
        }
    }
}
