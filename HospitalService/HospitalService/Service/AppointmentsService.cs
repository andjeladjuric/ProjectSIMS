using HospitalService.Model;
using HospitalService.Repositories;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HospitalService.Service
{
    class AppointmentsService
    {
        private AppointmentsRepository repository;
        public AppointmentsService()
        {
            repository = new AppointmentsRepository();
        }

        public void SetIds()
        {
            List<Appointment> appointments = repository.GetAll();
            int idCount = 0;
            for (int i = 0; i < appointments.Count; i++)
            {
                appointments[i].Id = (++idCount).ToString();
            }
            repository.SaveAll(appointments);
        }

        public string GetNextId()
        {
            List<Appointment> appointments = repository.GetAll();
            int id = appointments.Count;
            return (++id).ToString();
        }

        public Appointment getLastFinishedAppointment(Doctor doctor, Patient patient) {

            List<Appointment> appointments = repository.GetAll();
            List<Appointment> finishedAppointments = appointments.Where(appointment => appointment.doctor.Jmbg.Equals(doctor.Jmbg) && appointment.patient.Jmbg.Equals(patient.Jmbg) && appointment.EndTime < DateTime.Now).ToList();
            finishedAppointments.Sort((a1, a2) => DateTime.Compare(a1.EndTime, a2.EndTime));
            Appointment lastFinishedAppointment = finishedAppointments[finishedAppointments.Count - 1];
            return lastFinishedAppointment;

        }

        public int getNumberOfAppointmentsAfterLastFinishedSurvey(SurveyHospitalPatient lastFinishedHospitalSurvey, Patient surveyor) {

            List<Appointment> appointments = repository.GetAll();
            List<Appointment> appointmentsAfterFinishedSurvey = appointments.Where(appointment => appointment.patient.Jmbg.Equals(surveyor.Jmbg) && appointment.EndTime < DateTime.Now && appointment.EndTime > lastFinishedHospitalSurvey.ExecutionTime).ToList();
            int countOfAppointments = appointmentsAfterFinishedSurvey.Count;
            return countOfAppointments;
        }

        public List<Appointment> getNotFinishedAppointments(Patient patient) {

            repository = new AppointmentsRepository();
            List<Appointment> appointments = repository.GetAll();
            List<Appointment> notFinishedAppointment = appointments.Where(appointment => appointment.patient.Jmbg.Equals(patient.Jmbg) && appointment.StartTime >= DateTime.Now).ToList();
            return notFinishedAppointment;

        }

        public void delete(String id) {

            repository = new AppointmentsRepository();
            repository.Delete(id);
        }

        public int getNumberOfMovedAppointments(Patient patient) {

            repository = new AppointmentsRepository();
            List<Appointment> appointments = repository.GetAll();
            List<Appointment> movedAppointments = appointments.Where(appointment => appointment.patient.Jmbg.Equals(patient.Jmbg) && appointment.Status == Status.Moved).ToList();
            return movedAppointments.Count;
        }

        
    }
}
