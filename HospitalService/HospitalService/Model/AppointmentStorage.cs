using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
    public class AppointmentStorage
    {
        private String FileLocation = @"..\..\..\Images\appointments.json";
        public List<Appointment> appointments { get; set; }

        public AppointmentStorage()
        {
            appointments = new List<Appointment>();
            appointments = JsonConvert.DeserializeObject<List<Appointment>>(File.ReadAllText(FileLocation));
           /* var doktor1 = new Doctor();
            doktor1.Name = "Petra";
            doktor1.Surname = "Jovic";
            var pacijent1 = new Patient();
            pacijent1.Name = "Sladjana";
            pacijent1.Surname = "Colakovic";
            var soba1 = new Room();
            soba1.Id = "101-o";
            var termin1 = new Appointment();
            termin1.Id = "1";
            termin1.doctor = doktor1;
            termin1.patient = pacijent1;
            termin1.room = soba1;
            appointments.Add(termin1);*/
        }

        public List<Appointment> GetAll()
        {
            // TODO: implement
            return appointments;
        }

        public void Save(Appointment newAppointment)
        {
            // TODO: implement
            appointments.Add(newAppointment);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(appointments,
                       new JsonSerializerSettings()
                       {
                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                       }));
        }

        public Appointment GetOne(String id)
        {
            // TODO: implement
            return null;
        }

        public void Edit(String id, DateTime startTime, DateTime endTime, Room room)
        {
            Appointment a;
            for (int i = 0; i < appointments.Count; i++)
            {
                a = appointments[i];
                if (a.Id.Equals(id))
                {
                    a.StartTime = startTime;
                    a.EndTime = endTime;
                    a.room = room;
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
            Appointment a;
            for (int i = 0; i < appointments.Count; i++)
            {
                a = appointments[i];
                if (a.Id.Equals(id))
                {
                    appointments.RemoveAt(i);
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(appointments,
                       new JsonSerializerSettings()
                       {
                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                       }));
                    break;
                }
            }
        }
    }
}