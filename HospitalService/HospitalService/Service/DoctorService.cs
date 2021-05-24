using HospitalService.Repositories;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Service
{
    public class DoctorService
    {
        private DoctorsRepository repository;
        public DoctorService()
        {
            repository = new DoctorsRepository();
        }
        public List<Doctor> GetByDepartment(DoctorType department)
        {
            List<Doctor> foundDoctors = new List<Doctor>();
            List<Doctor> doctors = GetAll();
            foreach (Doctor doctor in doctors)
            {
                if (doctor.DoctorType.Equals(department))
                    foundDoctors.Add(doctor);
            }
            return foundDoctors;
        }

        public List<Doctor> GetAll() => repository.GetAll();
        public Doctor GetOne(string username) => repository.GetOne(username);
        public Doctor GetOneByJmbg(string jmbg) => repository.GetOneByJmbg(jmbg);
        public void Edit(Doctor doctor) => repository.Edit(doctor);
    }

}
