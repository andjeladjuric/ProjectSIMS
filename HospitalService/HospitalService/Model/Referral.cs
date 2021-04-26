using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Model
{
    class Referral
    {
        public DateTime DateOfIssue { get; set; }
        public DoctorType Specialization { get; set; }
        public Doctor Doctor { get; set; }
        public bool Urgent { get; set; }

        public Referral() { }
    }
}
