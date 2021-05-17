using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Repositories
{
    class DoctorsRepository
    {
        private String FileLocation = @"..\..\..\Data\doctors.json";
        private List<Doctor> doctors;

        public DoctorsRepository()
        {
            doctors = new List<Doctor>();
            doctors = JsonConvert.DeserializeObject<List<Doctor>>(File.ReadAllText(FileLocation));
        }

        public void SerializeDoctors()
        {
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(doctors));
        }

        public List<Doctor> GetAll()
        {
            return doctors;
        }

        public Doctor GetOne(String username)
        {
            return doctors.Find(x => x.Username == username);
        }

        public void Edit(Doctor editedDoctor)
        {
            Doctor doctor;
            for (int i = 0; i < doctors.Count; i++)
            {
                doctor = doctors[i];
                if (doctor.Jmbg.Equals(editedDoctor.Jmbg))
                {
                    doctors[i] = editedDoctor;
                    SerializeDoctors();
                    break;
                }
            }
        }
    }
}
