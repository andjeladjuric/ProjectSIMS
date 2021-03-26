using System;
using System.Collections.Generic;

namespace Model
{
    public class AppointmentStorage
    {
        private String FileLocation;
        public List<Appointment> appointments { get; set; }

        public List<Appointment> GetAll()
        {
            // TODO: implement
            return null;
        }

        public void Save(Appointment newAppointment)
        {
            // TODO: implement
        }

        public Appointment GetOne(String id)
        {
            // TODO: implement
            return null;
        }

        public void Edit(String id, DateTime startTime, DateTime endTime, Room room)
        {
            // TODO: implement
        }

        public void Delete(String id)
        {
            // TODO: implement
        }
    }
}