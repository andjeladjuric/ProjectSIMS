using HospitalService.Model;
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

        public Doctor getFirstAvailableDoctor(DateTime startTime, DateTime endTime)
        {
            repository = new DoctorsRepository();
            List<Doctor> doctors = repository.GetAll();
            bool isFindAvailableDoctor = false;
            Doctor availableDoctor = new Doctor();
            for (int i = 0; i < doctors.Count; i++)
            {
                if (isDoctorAvailable(startTime, endTime, doctors[i]))
                {

                    availableDoctor = doctors[i];
                    isFindAvailableDoctor = true;
                    break;
                }
            }
            if (isFindAvailableDoctor == false)
            {
                return null;
            }
            return availableDoctor;
        }
        public bool isDoctorAvailable(DateTime startTime, DateTime endTime, Doctor doctor)
        {
            AppointmentsService appointmentsService = new AppointmentsService();
            DateService dateService = new DateService();
            List<Appointment> appointments = appointmentsService.GetAll();
            for (int i = 0; i < appointments.Count; i++)
            {
                if (dateService.IsTaken(appointments[i].StartTime, appointments[i].EndTime, startTime, endTime) && appointments[i].doctor.Jmbg.Equals(doctor.Jmbg) && appointments[i].Status != Status.Canceled)
                {
                    return false;
                }
            }
            return true;
        }



        public List<Doctor> GetAll() => repository.GetAll();
        public Doctor GetOne(string username) => repository.GetOne(username);
        public Doctor GetOneByJmbg(string jmbg) => repository.GetOneByJmbg(jmbg);
        public void Edit(Doctor doctor) => repository.Edit(doctor);
        public void Delete(String jmbg) => repository.Delete(jmbg);
        public void Save(Doctor doctor) => repository.Save(doctor);

      

    }

}
