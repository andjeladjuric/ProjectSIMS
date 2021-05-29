using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Repositories
{
    class AppointmentsRepository
    {
        private String FileLocation = @"..\..\..\Data\appointments.json";
        public List<Appointment> appointments { get; set; }

        public AppointmentsRepository()
        {
            appointments = new List<Appointment>();
            appointments = JsonConvert.DeserializeObject<List<Appointment>>(File.ReadAllText(FileLocation));
        }

        public void SerializeAppointments()
        {
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(appointments));
        }

        public void SaveAll(List<Appointment> allAppointments)
        {
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(allAppointments));
        }

        public List<Appointment> GetAll()
        {
            return appointments;
        }

        public void Save(Appointment newAppointment)
        {
            appointments.Add(newAppointment);
            SerializeAppointments();
        }

        public Appointment GetOne(String id)
        {
            Appointment appointment = new Appointment();
            for (int i = 0; i < appointments.Count; i++)
            {
                appointment = appointments[i];
                if (appointment.Id.Equals(id))
                {
                    break;
                }
            }

            return appointment;

        }

        public void Edit(String id, DateTime startTime, DateTime endTime, Room room)
        {
            Appointment appointment;
            for (int i = 0; i < appointments.Count; i++)
            {
                appointment = appointments[i];
                if (appointment.Id.Equals(id))
                {
                    appointment.StartTime = startTime;
                    appointment.EndTime = endTime;
                    appointment.room = room;
                    SerializeAppointments();

                    break;
                }
            }
        }
        public void Move(String id, DateTime st, DateTime et, Room r)
        {
            Appointment a;
            for (int i = 0; i < appointments.Count; i++)
            {
                a = appointments[i];
                if (a.Id.Equals(id))
                {
                    a.StartTime = st;
                    a.EndTime = et;
                    a.room = r;
                    a.Status = Status.Moved;
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(appointments,
                       new JsonSerializerSettings()
                       {
                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                       }));
                    break;
                }
            }


        }

        public void Delete(String id)
        {
            Appointment appointment;
            for (int i = 0; i < appointments.Count; i++)
            {
                appointment = appointments[i];
                if (appointment.Id.Equals(id))
                {
                    appointments.RemoveAt(i);
                    SerializeAppointments();
                    break;
                }
            }
        }
    }
}
