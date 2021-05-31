using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Repositories
{
    class PatientsRepository
    {
        private String FileLocation = @"..\..\..\Data\patients.json";
        private List<Patient> patients;

        public PatientsRepository()
        {
            patients = new List<Patient>();
            patients = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText(FileLocation));
        }

        public void SerializePatients()
        {
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(patients));
        }
        public List<Patient> GetAll()
        {
            return patients;
        }

        public Patient GetOne(String jmbg)
        {
            return patients.Find(x => x.Jmbg == jmbg);
        }

        public Patient GetOneByUsername(String username)
        {
            return patients.Find(x => x.Username == username);
        }

        public void Save(Patient newPatient)
        {
            patients.Add(newPatient);
            SerializePatients();
        }

        public void Edit(String jmbg, String username, String password, DateTime? dateOfBirth, String phone, String address, String email, PatientType patientType)
        {
            Patient patient;
            for (int i = 0; i < patients.Count; i++)
            {
                patient = patients[i];
                if (patient.Jmbg.Equals(jmbg))
                {
                    patient.PatientType = patientType;
                    patient.Username = username;
                    patient.Password = password;
                    patient.DateOfBirth = dateOfBirth;
                    patient.Address = address;
                    patient.Phone = phone;
                    patient.Email = email;
                    patients[i] = patient;
                    SerializePatients();
                    break;
                }
            }
        }

        public void Delete(String jmbg)
        {
            Patient patient = patients.Find(x => x.Jmbg == jmbg);
            patients.Remove(patient);
            SerializePatients();

        }

        public void addRecord(String jmbg, String id)
        {
            Patient patient = GetAll().Find(x => x.Jmbg == jmbg);
            patient.medicalRecordId = id;
            SerializePatients();
        }
    }
}
