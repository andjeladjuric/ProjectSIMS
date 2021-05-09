using System;
using System.Collections.Generic;
using System.Text;
using Model;
using Storage;
namespace HospitalService.View.SecretaryUI
{
    class SortNode
    {
        public SortNode(Appointment app)
        {
            this.appointment = app;
            startNext = new AppointmentStorage().findNextAvailable(app).StartTime;
        }
        public Appointment appointment;

        
        public DateTime startNext;
    }
}
