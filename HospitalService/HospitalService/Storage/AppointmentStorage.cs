using HospitalService.Storage;
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

        public List<Room> GetAvailableRooms(RoomType roomType, DateTime startTime, DateTime endTime)
        {
            List<Room> availableRooms = new RoomFileStorage().getByType(roomType);
            foreach(Appointment appointment in appointments) 
            {
                bool pom = false;
                if (DateTime.Compare(appointment.StartTime, startTime) == 0)
                {
                    pom = true;
                }
                else if (DateTime.Compare(appointment.StartTime, startTime) < 0)
                {
                    if (DateTime.Compare(appointment.EndTime, startTime) > 0)
                        pom = true;

                }
                else if (DateTime.Compare(appointment.StartTime, startTime) > 0 && DateTime.Compare(endTime, appointment.StartTime) > 0)
                    pom = true;

                if (pom)
                {
                    for (int i = 0; i < availableRooms.Count; i++)
                        if (availableRooms[i] == appointment.room)
                            availableRooms.RemoveAt(i);
                }
                   
            }
            return availableRooms;
        }

        public List<Doctor> GetAvailableDoctors(DoctorType doctorType, DateTime startTime, DateTime endTime)
        {
            List<Doctor> availableDoctors = new DoctorStorage().GetByDepartment(doctorType);
            for (int i = 0; i < availableDoctors.Count; i++)
            {
                if (IsTaken(startTime, endTime, availableDoctors[i]))
                    availableDoctors.RemoveAt(i);
            }
            return availableDoctors;
        }
        
        private bool checkPatient(Patient patient, Appointment arg)
        {
            List<Appointment> appoints = new AppointmentStorage().getByPatient(patient);
            foreach (Appointment appoint in appoints)
            {
                if (arg.intersect(appoint))
                {
                    return false;
                }
            }
            return true;
        }

        
        private Room findFreeRoom(Appointment arg)
        {
            Room ret = null;
            foreach (Room room in new RoomFileStorage().GetAll())
            {
                ret = room;
                foreach (Appointment appoint in new AppointmentStorage().GetAll())
                {
                    if (arg.intersect(appoint) && appoint.room.Id.Equals(room.Id))
                    {
                        ret = null;
                        break;
                    }
                }
                if (ret != null) break;
            }

            return ret;
        }


        private Doctor findFreeDoctor(Appointment appointment, DoctorType oblast)
        {
            List<Doctor> doktori = new DoctorStorage().GetByType(oblast);
            AppointmentStorage storage = new AppointmentStorage();
            foreach (Doctor doc in doktori)
            {
                if (!storage.IsTaken(appointment.StartTime, appointment.EndTime, doc))
                    return doc;
            }
            return null;
        }

        
        public Appointment createAppointment(Appointment appointment, Patient patient, DoctorType oblast)
        {
            if (appointment == null || patient == null) return null;

            AppointmentStorage storage = new AppointmentStorage();
            Room room = null;
            Doctor doctor = null;
            if (!checkPatient(patient, appointment)) return null;
            doctor = findFreeDoctor(appointment, oblast);
            room = findFreeRoom(appointment);
            if (doctor == null || room == null) return null;

            appointment.patient = patient;
            appointment.doctor = doctor;
            appointment.room = room;
            appointment.isUrgent = true;

            return new Appointment(appointment);
        }

        public Appointment createAppointment(Appointment app, DoctorType oblast)
        {
            if (app == null) return null;
            if (app.patient == null) return null;
            return createAppointment(app, app.patient, oblast);
        }

        public Appointment createAppointment(Appointment app)
        {
            if (app == null) return null;
            if (app.doctor == null) return null;
            return createAppointment(app, app.doctor.DoctorType);
        }

        
        public void storeAppointment(Appointment appointment)
        {
            if (appointment == null) return;
            int length = (appointment.EndTime - appointment.StartTime).Hours;
            Appointment[] appArray = new Appointment[length];
            for (int i = 0; i < length; i++)
            {
                appArray[i] = new Appointment(appointment);
                appArray[i].StartTime = appArray[i].StartTime.AddHours(i);
                appArray[i].EndTime = appArray[i].StartTime.AddHours(1);
                appArray[i].Id = GetNextId();
                Save(appArray[i]);
            }
        }

        
        public Appointment findNextAvailable(Appointment arg)
        {
            Appointment temp = new Appointment(arg);
            Appointment app = null;
            while (app == null)
            {
                app = createAppointment(temp);
                temp.setDates(temp.StartTime.AddHours(1), temp.EndTime.Hour - temp.StartTime.Hour);
            }
            return app;
        }


    }
}