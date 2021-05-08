
using System;

namespace Model
{
    public class Appointment
    {
        public const int OPENING_HOUR = 8;
        public const int CLOSING_HOUR = 21;
        public const int MAX_DURATION = CLOSING_HOUR - OPENING_HOUR;



        public Appointment() { this.isUrgent = false; }
        public Appointment(Appointment arg)
        {
            this.StartTime = arg.StartTime;
            this.EndTime = arg.EndTime;
            this.Type = arg.Type;
            this.Status = arg.Status;
            this.room = arg.room;
            this.doctor = arg.doctor;
            this.patient = arg.patient;
            this.isUrgent = arg.isUrgent;
        }

        public bool intersect(Appointment arg)
        {
            return intersect(arg.StartTime, arg.EndTime);
        }

        public bool intersect(DateTime start, DateTime end)
        {
            if (StartTime < end && EndTime > start) { return true; }
            return false;
        }

        public void setDates(int duration)
        {
            DateTime now = DateTime.Now;
            setDates(now, duration);
        }

        public void setDates(DateTime now, int duration)
        {
          
            int startHour = now.Hour;
            int endHour = now.Hour + duration;
            if (now.Minute > 0 || now == DateTime.Now) endHour++;


            if (endHour > CLOSING_HOUR || startHour < OPENING_HOUR)
            {
                startHour = OPENING_HOUR;
                if (endHour > CLOSING_HOUR) now = now.AddDays(1);
                endHour = startHour + duration;

            }

            StartTime = new DateTime(now.Year, now.Month, now.Day, startHour, 0, 0);
            EndTime = new DateTime(now.Year, now.Month, now.Day, endHour, 0, 0);

        }
        public bool isUrgent { get; set; }
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