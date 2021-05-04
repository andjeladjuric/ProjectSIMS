using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Storage
{
    public class AppointmentStorage
    {
        private String FileLocation = @"..\..\..\Data\appointments.json";
        public List<Appointment> appointments { get; set; }

        public AppointmentStorage()
        {
            appointments = new List<Appointment>();
            appointments = JsonConvert.DeserializeObject<List<Appointment>>(File.ReadAllText(FileLocation));
            /*var doktor1 = new Doctor();
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
            return appointments;
        }

        public void Save(Appointment newAppointment)
        {
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
            Appointment a = new Appointment();
            for (int i = 0; i < appointments.Count; i++)
            {
                a = appointments[i];
                if (a.Id.Equals(id))
                {
                    break;
                }
            }

            return a;
            
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


        // Vraca listu termina za proslijedjenog doktora
        public List<Appointment> getByDoctor(Doctor d, DateTime dt)
        {
            Appointment a;
            List<Appointment> retVal = new List<Appointment>();
            for (int i = 0; i < appointments.Count; i++)
            {
                a = appointments[i];
                if (a.doctor.Jmbg.Equals(d.Jmbg) && a.StartTime.Date == dt.Date)
                {
                    retVal.Add(a);
                }
            }
            return retVal;
        }

        public List<Appointment> getByPatient(Patient p) {

            Appointment a;
            List<Appointment> retVal = new List<Appointment>();
            for (int i = 0; i < appointments.Count; i++) {
                a = appointments[i];
                if (a.patient.Jmbg.Equals(p.Jmbg)) {
                    retVal.Add(a);
                }
            }
            return retVal;
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

        public Boolean AlreadyExists(String id)
        {
            if (appointments.Find(x => x.Id == id) == null)
                return false;
            else
                return true;

        }

        public Boolean IsTaken(DateTime start, DateTime end, Doctor d)
        {
            Appointment a;
            List<Appointment> termini = this.getByDoctor(d, start);
            for (int i = 0; i < termini.Count; i++)
            {
                a = termini[i];
                if (DateTime.Compare(a.StartTime, start) == 0)
                {
                    return true;
                }
                else if (DateTime.Compare(a.StartTime, start) < 0)
                {
                    if (DateTime.Compare(a.EndTime, start) > 0)
                        return true;
                }
                else if (DateTime.Compare(a.StartTime, start) > 0 && DateTime.Compare(end, a.StartTime) > 0)
                    return true;
            }
            return false;
        }

        public void SetIds()
        {
            int j = 0;
            for (int i = 0; i < appointments.Count; i++)
            {
                appointments[i].Id = (++j).ToString();
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(appointments));
        }

        public string GetNextId()
        {
            int id = appointments.Count;
            return (++id).ToString();
        }
    }
}