using Newtonsoft.Json;
using Storage;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
    public class PatientStorage
    {
        private String FileLocation = @"..\..\..\Data\patients.json";
        private List<Patient> patients;

        public PatientStorage()
        {
            patients = new List<Patient>();
            /*Patient p1 = new Patient
            {
                Username = null,
                Password = null,
                PatientType = PatientType.Guest,
                Name = "Marko",
                Surname = "Jankovic",
                Jmbg = "0534023456712",
                Gender = Gender.Male,
                Address = null,
                Email = null,
                Phone = null,
                DateOfBirth = null

            };

            
            Patient p2 = new Patient
            {
                Username = "sladjana",
                Password = "123456",
                PatientType = PatientType.General,
                Name = "Sladjana",
                Surname = "Colakovic",
                Jmbg = "0101000234567",
                Gender = Gender.Female,
                Address = "Balzakova 23",
                Email = "sladjanac@gmail.com",
                Phone = "0614563456",
                DateOfBirth = new DateTime(1999, 1, 1)
            };
            patients.Add(p1);
            patients.Add(p2);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(patients));
           */

            patients = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText(FileLocation));
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
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(patients));
        }

        public void Edit(String jmbg, String username, String password, DateTime? dateOfBirth, String phone, String address, String email, PatientType patientType)
        {
            Patient patient = patients.Find(x => x.Jmbg == jmbg);
            patient.PatientType = patientType;
            patient.Username = username;
            patient.Password = password;
            patient.DateOfBirth = dateOfBirth;
            patient.Address = address;
            patient.Phone = phone;
            patient.Email = email;
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(patients));
        }
        
        public void Delete(String jmbg)
        {
            Patient patient = patients.Find(x => x.Jmbg == jmbg);
            patients.Remove(patient);
            new MedicalRecordStorage().Delete(patient.medicalRecordId);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(patients));

        }

        public void addRecord(String jmbg, String id)
        {
            Patient patient = patients.Find(x => x.Jmbg == jmbg);
            patient.medicalRecordId = id;
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(patients));
        }
    }
}