using System;
using System.Collections.Generic;

namespace Model
{
    public class PatientStorage
    {
        private String FileLocation;

        public List<Patient> GetAll()
        {
            // TODO: implement
            return null;
        }

        public Patient GetOne(String jmbg)
        {
            // TODO: implement
            return null;
        }

        public void Save(Patient newPatient)
        {
            // TODO: implement
        }

        public void Edit(String jmbg, String username, String password, DateTime dateOfBirth, String phone, String address, String email, PatientType patientType)
        {
            // TODO: implement
        }

        public void Delete(String jmbg)
        {
            // TODO: implement
        }

    }
}