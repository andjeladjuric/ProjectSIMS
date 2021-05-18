﻿using HospitalService.Repositories;
using Model;
using System;
using System.Collections.Generic;
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

        public List<Appointment> GetByDoctor(Doctor doctor, DateTime date)
        {
            Appointment appointment;
            List<Appointment> appointments = repository.GetAll();
            List<Appointment> appointmentsForSelectedDate = new List<Appointment>();
            for (int i = 0; i < appointments.Count; i++)
            {
                appointment = appointments[i];
                if (appointment.doctor.Jmbg.Equals(doctor.Jmbg) && appointment.StartTime.Date == date.Date)
                {
                    appointmentsForSelectedDate.Add(appointment);
                }
            }
            return appointmentsForSelectedDate;
        }
    }
}