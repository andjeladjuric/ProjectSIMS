using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace HospitalService.Service
{
    public interface AddAppointmentBehaviour
    {
        public bool addAppointment(DateTime startTime, DateTime endTime, Doctor doctor, Patient patient);
    }
}
