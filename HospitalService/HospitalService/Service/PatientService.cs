using HospitalService.Repositories;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Service
{
    public class PatientService
    {
        private PatientsRepository repository;
        public PatientService()
        {
            repository = new PatientsRepository();
        }

        public List<Patient> GetAll() => repository.GetAll();
        public Patient GetOneByUsername(string username) => repository.GetOneByUsername(username);
        public Patient GetOne(string jmbg) => repository.GetOne(jmbg);
        public void Delete(string jmbg) => repository.Delete(jmbg);
        public void AddNew(Patient doctor) => repository.Save(doctor);
        public void Edit(String jmbg, String username, String password, DateTime? dateOfBirth, String phone, String address, String email, PatientType patientType) => repository.Edit(jmbg, username, password, dateOfBirth, phone, address, email, patientType);
    }
}
