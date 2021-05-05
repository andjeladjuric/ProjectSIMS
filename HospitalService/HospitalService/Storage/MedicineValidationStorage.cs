using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Storage
{
    public class MedicineValidationStorage
    {
        private string FileLocation = @"..\..\..\Data\validationRequests.json";
        private List<MedicineValidationRequest> requests;

        public MedicineValidationStorage()
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
    }
}
