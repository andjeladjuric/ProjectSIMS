using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Storage
{
    public class DoctorStorage
    {
        private String FileLocation = @"..\..\..\Data\doctors.json";
        private List<Doctor> doctors;

        public DoctorStorage()
        {
            doctors = new List<Doctor>();
            doctors = JsonConvert.DeserializeObject<List<Doctor>>(File.ReadAllText(FileLocation));
        }
        public List<Doctor> GetAll()
        {
            return doctors;
        }

        public Doctor GetOne(String username)
        {
            return doctors.Find(x => x.Username == username);
        }

        public List<Doctor> GetByDepartment(DoctorType department)
        {
            List<Doctor> foundDoctors = new List<Doctor>();
            foreach(Doctor doctor in doctors)
            {
                if (doctor.DoctorType.Equals(department))
                    foundDoctors.Add(doctor);
            }
            return foundDoctors;
        }

    }
}
