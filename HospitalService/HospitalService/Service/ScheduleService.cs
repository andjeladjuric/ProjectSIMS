using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Service
{
    class ScheduleService
    {
        private AppointmentsService AppointmentsService;
        private RoomService RoomService;
        private DateService DateService;
        private RoomRenovationService RoomRenovationService;

        public ScheduleService() {

                AppointmentsService = new AppointmentsService();
                RoomService = new RoomService();
                DateService = new DateService();
                RoomRenovationService = new RoomRenovationService();
    }

        public Boolean IsDoctorTaken(DateTime start, DateTime end, Doctor doctor)
        {
            Appointment appointment;
            List<Appointment> appointments = AppointmentsService.GetByDoctor(doctor, start);
            for (int i = 0; i < appointments.Count; i++)
            {
                appointment = appointments[i];
                if (DateService.ExsitstsAtTime(appointment, start, end))
                    return true;
            }
            return false;
        }

        public Boolean IsRoomTaken(DateTime start, DateTime end, Room room)
        {
            Appointment appointment;
            List<Appointment> appointments = AppointmentsService.GetForRoom(room.Id);
            for (int i = 0; i < appointments.Count; i++)
            {
                appointment = appointments[i];
                if (DateService.ExsitstsAtTime(appointment, start, end))
                    return true;
            }
            return false;
        }

        private Boolean IsPatientTaken(Patient patient, Appointment arg)
        {
            List<Appointment> appoints = AppointmentsService.getByPatient(patient);
            foreach (Appointment appoint in appoints)
            {
                if (arg.intersect(appoint))
                {
                    return false;
                }
            }
            return true;
        }


        public Boolean CheckAppointmentsForDate(DateTime startDate, DateTime endDate, string roomId)
        {
            foreach (Appointment a in AppointmentsService.GetAll())
            {
                if (a.room.Id.Equals(roomId))
                {
                    if (DateTime.Compare(startDate.Date, a.StartTime.Date) <= 0 &&
                        DateTime.Compare(endDate.Date, a.StartTime.Date) >= 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public Boolean IsRoomRenovating(DateTime appointmentStart, DateTime appointmentEnd, Room room)
        {
            List<Renovation> renovationRequests = RoomRenovationService.GetAll();

            if (renovationRequests.Count != 0)
            {
                foreach (Renovation renovation in renovationRequests)
                {
                    if (renovation.RoomId.Equals(room.Id))
                    {
                        if (DateTime.Compare(renovation.Start, appointmentStart) <= 0 && DateTime.Compare(renovation.End, appointmentStart) >= 0
                            && DateTime.Compare(renovation.Start, appointmentEnd) <= 0 && DateTime.Compare(renovation.End, appointmentEnd) >= 0)
                            return true;
                        else
                            return false;
                    }
                }
            }

            return false;
        }
    }
}
