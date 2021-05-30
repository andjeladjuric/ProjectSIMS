using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Service
{
    public class DateService
    {
        public DateService() { }

        public DateTime CreateDate(DateTime date, DateTime time)
        {
            String createdDate = date.ToString("MM/dd/yyyy") + " " + time.ToString("HH: mm");
            return Convert.ToDateTime(createdDate);
        }

        public bool ExsitstsAtTime(Appointment appointment, DateTime start, DateTime end)
        {
            if (DateTime.Compare(appointment.StartTime, start) == 0)
                return true;
            else if (DateTime.Compare(appointment.StartTime, start) < 0)
            {
                if (DateTime.Compare(appointment.EndTime, start) > 0)
                    return true;
            }
            else if (DateTime.Compare(appointment.StartTime, start) > 0 && DateTime.Compare(end, appointment.StartTime) > 0)
                return true;
            
             return false;
        }
    }
}
