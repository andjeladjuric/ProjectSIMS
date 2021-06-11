using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Model;

namespace HospitalService.Service
{
    public class DatePriority : AddAppointmentBehaviour
    {
        private DoctorService doctorService;
        private RoomService roomService;
        private AppointmentsService appointmentsService;
        public bool addAppointment(DateTime startTime, DateTime endTime, Doctor doctor, Patient patient)
        {
            doctorService = new DoctorService();
            roomService = new RoomService();
            appointmentsService = new AppointmentsService();
            Doctor availableDoctor = doctorService.getFirstAvailableDoctor(startTime, endTime);
            if (appointmentsService.moreThanTwoAppointmentsInOneDay(startTime, patient))
            {
                MessageBox.Show("Vise od dva termina u jednom danu!");
                return false;
            }
            if (availableDoctor == null)
            {
                MessageBox.Show("Nema slobodnih doktora!");
                return false; 
            }
            Room availableRoom = roomService.getFirstAvailableRoom(startTime, endTime);
            if (availableRoom == null)
            {
                MessageBox.Show("Nema slobodnih sala!");
                return false;               
            }
            Appointment newAppointment = new Appointment() { Id = appointmentsService.GetNextId(), StartTime = startTime, EndTime = endTime, Type = AppointmentType.Pregled, doctor = availableDoctor, room = availableRoom, patient = patient };
            appointmentsService.Save(newAppointment);          
            return true;
        }
    }
}
