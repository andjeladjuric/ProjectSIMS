using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace HospitalService.Model
{
   public class Note
    {
        public String noteForPatient { get; set; }
        public Diagnosis diagnosis { get; set; }
        public Patient patient { get; set; }
    }
}
