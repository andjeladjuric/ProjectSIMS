
using HospitalService.Model;
using System.Collections.Generic;
using System;

namespace Model
{
    public class Doctor : User
    {
        public DoctorType DoctorType { get; set; }

        public List<Holidays> Holidays { get; set; }

        public int FreeDaysNum { get; set; }
        
        
    }
}