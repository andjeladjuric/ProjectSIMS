using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.View.SecretaryUI
{
    public class CalendarHelper
    {
        string name = "";
        List<Tuple<string, Appointment>> appointments = new List<Tuple<string, Appointment>>();

        public string Name { get => name; set => name = value; }
        public List<Tuple<string, Appointment>> Appointments { get => appointments; set => appointments = value; }

        public CalendarHelper(string name)
        {
            Name = name;
            for (int i = 0; i < 13; i++)
            {
                appointments.Add(new Tuple<string, Appointment>("", null));
            }
        }
    }
}
