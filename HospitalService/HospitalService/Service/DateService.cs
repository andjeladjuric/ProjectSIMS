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
            String  createdDate= date.ToString("MM/dd/yyyy") + " " + time.ToString("HH: mm");
            return Convert.ToDateTime(createdDate);
        }
    }
}
