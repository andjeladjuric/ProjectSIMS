using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class MedicineValidationRequest
    {
        public string MedicineId { get; set; }
        public string doctorsJMBG { get; set; }

        public MedicineValidationRequest(string medicineId, string doctorsJMBG)
        {
            MedicineId = medicineId;
            this.doctorsJMBG = doctorsJMBG;
        }

        public MedicineValidationRequest()
        {
        }
    }
}
