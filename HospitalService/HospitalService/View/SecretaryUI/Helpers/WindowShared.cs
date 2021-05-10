using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.View.SecretaryUI
{
    class WindowSharedData
    {
        public WindowSharedData(Appointment argApp, DoctorType argType, int argDuration)
        {
            this.appointment = argApp;
            this.type = argType;
            this.duration = argDuration;
        }
        public Appointment appointment;
        public DoctorType type;
        public int duration;
    }
}
