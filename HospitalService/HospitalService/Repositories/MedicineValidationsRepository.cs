using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Repositories
{
    class MedicineValidationsRepository
    {
        private string FileLocation = @"..\..\..\Data\validationRequests.json";
        private List<MedicineValidationRequest> requests;

        public MedicineValidationsRepository()
        {
            requests = new List<MedicineValidationRequest>();
            requests = JsonConvert.DeserializeObject<List<MedicineValidationRequest>>(File.ReadAllText(FileLocation));
        }

        public void SerializeValidationRequests()
        {
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(requests));
        }

        public List<MedicineValidationRequest> GetAll()
        {
            return requests;
        }

        public void Delete(String medicineId)
        {
            for (int i = 0; i < requests.Count; i++)
                if (requests[i].MedicineId.Equals(medicineId))
                    requests.RemoveAt(i);
            SerializeValidationRequests();
        }
    }
}
}
