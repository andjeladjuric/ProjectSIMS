
using System;

namespace Model
{
    public class Appointment
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public AppointmentType Type { get; set; }
        public String Id { get; set; }
        public Status Status { get; set; }

        public Room room { get; set; }
        public Doctor doctor { get; set; }
        public Patient patient { get; set; }


     

    }
}