using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Model
{
    public class Holidays
    {
        public HolidaysType Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Doctor Doctor { get; set; }

    }
}
