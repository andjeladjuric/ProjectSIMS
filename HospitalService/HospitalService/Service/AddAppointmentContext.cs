using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace HospitalService.Service
{
   public class AddAppointmentContext
    {

        private AddAppointmentBehaviour addAppointmentBehaviour;

        public void setStrategy(AddAppointmentBehaviour addAppointmentBehaviour) {
            this.addAppointmentBehaviour = addAppointmentBehaviour;
        }

        public bool addAppointment(DateTime startTime, DateTime endTime, Doctor doctor, Patient patient) {

            return addAppointmentBehaviour.addAppointment(startTime,endTime,doctor,patient);

        }
    }
}
