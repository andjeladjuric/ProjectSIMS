using HospitalService.Repositories;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Service
{
    public class MedicineValidationService
    {
        private MedicineValidationsRepository repository;
        public MedicineValidationService()
        {
            repository = new MedicineValidationsRepository();
        }
        public List<MedicineValidationRequest> GetForDoctor(String jmbg)
        {
            List<MedicineValidationRequest> validationRequests = new List<MedicineValidationRequest>();
            List<MedicineValidationRequest> allRequests = repository.GetAll();
            foreach (MedicineValidationRequest request in allRequests)
            {
                if (request.doctorsJMBG.Equals(jmbg))
                    validationRequests.Add(request);
            }
            return validationRequests;
        }

        public void Delete(string Id) => repository.Delete(Id);
    }
}
