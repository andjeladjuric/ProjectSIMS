using HospitalService.Model;
using HospitalService.Repositories;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HospitalService.Service
{
    class AppointmentsService
    {
        private AppointmentsRepository repository;
        public AppointmentsService()
        {
            repository = new AppointmentsRepository();
        }
        public void SetIds()
        {
            List<Appointment> appointments = repository.GetAll();
            int idCount = 0;
            for (int i = 0; i < appointments.Count; i++)
            {
                appointments[i].Id = (++idCount).ToString();
            }
            repository.SaveAll(appointments);
        }

        public string GetNextId()
        {
            List<Appointment> appointments = repository.GetAll();
            int id = appointments.Count;
            return (++id).ToString();
        }

        public Appointment getLastFinishedAppointment(Doctor doctor, Patient patient) {

            List<Appointment> appointments = repository.GetAll();
            List<Appointment> finishedAppointments = appointments.Where(appointment => appointment.doctor.Jmbg.Equals(doctor.Jmbg) && appointment.patient.Jmbg.Equals(patient.Jmbg) && appointment.EndTime < DateTime.Now).ToList();
            finishedAppointments.Sort((a1, a2) => DateTime.Compare(a1.EndTime, a2.EndTime));
            Appointment lastFinishedAppointment = finishedAppointments[finishedAppointments.Count - 1];
            return lastFinishedAppointment;

        }

        public int getNumberOfAppointmentsAfterLastFinishedSurvey(SurveyHospitalPatient lastFinishedHospitalSurvey, Patient surveyor) {

            List<Appointment> appointments = repository.GetAll();
            List<Appointment> appointmentsAfterFinishedSurvey = appointments.Where(appointment => appointment.patient.Jmbg.Equals(surveyor.Jmbg) && appointment.EndTime < DateTime.Now && appointment.EndTime > lastFinishedHospitalSurvey.ExecutionTime).ToList();
            int countOfAppointments = appointmentsAfterFinishedSurvey.Count;
            return countOfAppointments;
        }

        public List<Appointment> getNotFinishedAppointments(Patient patient) {

            repository = new AppointmentsRepository();
            List<Appointment> appointments = repository.GetAll();
            List<Appointment> notFinishedAppointment = appointments.Where(appointment => appointment.patient.Jmbg.Equals(patient.Jmbg) && appointment.StartTime >= DateTime.Now).ToList();
            return notFinishedAppointment;

        }

        public List<Appointment> getAppointmentsByDate(Patient patient, DateTime date) {
            repository = new AppointmentsRepository();
            List<Appointment> appointments = repository.GetAll();
            List<Appointment> appointmentsForSelectedDate = new List<Appointment>();
            appointmentsForSelectedDate= appointments.Where(appointment => appointment.patient.Jmbg.Equals(patient.Jmbg) && appointment.StartTime.Date == date.Date && appointment.Status!=Status.Canceled).ToList();
            return appointmentsForSelectedDate;


        }

        public int getNumberOfCanceledAppointments(Patient patient) {

            
            repository = new AppointmentsRepository();
            List<Appointment> appointments = repository.GetAll();
            List<Appointment> canceledAppointments = new List<Appointment>();
            canceledAppointments = appointments.Where(appointment => appointment.patient.Jmbg.Equals(patient.Jmbg) && appointment.Status==Status.Canceled).ToList();
            return canceledAppointments.Count;

        }

        public int getNumberOfMovedAppointments(Patient patient) {

            repository = new AppointmentsRepository();
            List<Appointment> appointments = repository.GetAll();
            List<Appointment> movedAppointments = appointments.Where(appointment => appointment.patient.Jmbg.Equals(patient.Jmbg) && appointment.Status == Status.Moved).ToList();
            return movedAppointments.Count;
        }

        

        public List<Appointment> GetByDoctor(Doctor doctor, DateTime date)
        {
            Appointment appointment;
            List<Appointment> appointments = repository.GetAll();
            List<Appointment> appointmentsForSelectedDate = new List<Appointment>();
            for (int i = 0; i < appointments.Count; i++)
            {
                appointment = appointments[i];
                if (appointment.doctor.Jmbg.Equals(doctor.Jmbg) && appointment.StartTime.Date == date.Date)
                {
                    appointmentsForSelectedDate.Add(appointment);
                }
            }
            return appointmentsForSelectedDate;
        }

        public void Delete(String AppointmentID)
        {
            repository.Delete(AppointmentID);
            SetIds();
        }

        public void DeletePatientAppointment(String AppointmentID)
        {
            
            repository.DeletePatientAppointment(AppointmentID);
            SetIds();
        }

        public void AddAppointment(Appointment newAppointment) => repository.Save(newAppointment);
       

        public Boolean IsTaken(DateTime start, DateTime end, Doctor doctor)
        {
            Appointment appointment;
            List<Appointment> appointments = this.GetByDoctor(doctor, start);
            for (int i = 0; i < appointments.Count; i++)
            {
                appointment = appointments[i];
                if (new DateService().ExsitstsAtTime(appointment, start, end))
                    return true;
            }
            return false;
        }

        public Boolean IsRoomTaken(DateTime start, DateTime end, Room room)
        {
            Appointment appointment;
            List<Appointment> appointments = GetForRoom(room.Id);
            for (int i = 0; i < appointments.Count; i++)
            {
                appointment = appointments[i];
                if (new DateService().ExsitstsAtTime(appointment, start, end))
                    return true;
            }
            return false;
        }


        public void Save(Appointment appointment) {
            repository = new AppointmentsRepository();
            repository.Save(appointment);
        }
        public int getNumberOfSameDateAppointments(Patient patient, DateTime startTime) {
            repository = new AppointmentsRepository();
            List<Appointment> la = repository.GetAll();
            List<Appointment> sameDateAppointments = la.Where(appointment => appointment.patient.Jmbg.Equals(patient.Jmbg) && startTime.ToShortDateString().Equals(appointment.StartTime.ToShortDateString()) && appointment.Status!=Status.Canceled).ToList();
            return sameDateAppointments.Count;
        }
        public List<Appointment> GetAll() {

            repository = new AppointmentsRepository();
            List<Appointment> appointments = repository.GetAll();
            return appointments;
        
        }
        public void Move(String id, DateTime st, DateTime et, Room r) {
            repository = new AppointmentsRepository();
            repository.Move(id,st,et,r);
        }

        public List<Appointment> GetForRoom(string roomId)
        {
            Appointment appointment;
            List<Appointment> appointments = repository.GetAll();
            List<Appointment> appointmentsForSelectedRoom = new List<Appointment>();
            for (int i = 0; i < appointments.Count; i++)
            {
                appointment = appointments[i];
                if (appointment.room.Id.Equals(roomId))
                {
                    appointmentsForSelectedRoom.Add(appointment);
                }
            }
            return appointmentsForSelectedRoom;
        }

        public List<Room> GetAvailableRooms(DateTime start, DateTime end, RoomType roomType)
        {
            List<Room> allRooms = new RoomService().GetByType(roomType);
            List<Room> availableRooms = new List<Room>();
            foreach (Room room in allRooms)
                if (!IsRoomTaken(start, end, room))
                    availableRooms.Add(room);
            return availableRooms;
        }

        public void Edit(String id, DateTime startTime, DateTime endTime, Room room) => repository.Edit(id, startTime, endTime, room);
        
        public List<Appointment> getByPatient(Patient p)
        {

            Appointment a;
            List<Appointment> retVal = new List<Appointment>();
            List<Appointment> appointments = repository.GetAll();
            for (int i = 0; i < appointments.Count; i++)
            {
                a = appointments[i];
                if (a.patient.Jmbg.Equals(p.Jmbg))
                {
                    retVal.Add(a);
                }
            }
            return retVal;
        }

        private bool checkPatient(Patient patient, Appointment arg)
        {
            List<Appointment> appoints = new AppointmentsService().getByPatient(patient);
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
                foreach (Appointment appoint in new AppointmentsRepository().GetAll())
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
            List<Doctor> doktori = new DoctorService().GetByDepartment(oblast);
            AppointmentsService storage = new AppointmentsService();
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
            AppointmentsService storage = new AppointmentsService();
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
                repository.Save(appArray[i]);
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
