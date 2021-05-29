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
            List<Appointment> appointments = appointmentsService.GetAll();
            for (int i = 0; i < appointments.Count; i++)
            {
                if ((DateTime.Compare(appointments[i].StartTime, startTime) == 0 || DateTime.Compare(appointments[i].EndTime, endTime) == 0) && appointments[i].doctor.Jmbg.Equals(doctor.Jmbg))
                {
                    return false;
                }
                else if (startTime > appointments[i].StartTime && startTime < appointments[i].EndTime && appointments[i].doctor.Jmbg.Equals(doctor.Jmbg))
                {
                    return false;
                }
                else if (endTime > appointments[i].StartTime && endTime < appointments[i].EndTime && appointments[i].doctor.Jmbg.Equals(doctor.Jmbg))
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
    }

}
