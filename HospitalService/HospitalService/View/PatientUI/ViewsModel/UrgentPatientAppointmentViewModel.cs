using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using HospitalService.Repositories;
using HospitalService.Service;
using HospitalService.View.PatientUI.Pages;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
    public class UrgentPatientAppointmentViewModel:ViewModelPatientClass
    {

        private DateTime date { get; set; }
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }
        public String StartTimeAppointment { get; set; }

        public String EndTimeAppointment { get; set; }
        public RelayCommand confirmAddAppointment { get; set; }
        public RelayCommand cancelAddAppointment { get; set; }
        private Patient patient;
        private UrgentPatientAppointment urgentPatientAppointment;
        private List<Appointment> appointments;
        private List<Doctor> doctors;
        private List<Room> rooms;
        private AppointmentsRepository appointmentRepository;
        private void Execute_ConfirmAddAppointment(object obj) {

            
            DateTime startTime = Convert.ToDateTime(Date.ToShortDateString() + " " + StartTimeAppointment + ":00");
            DateTime endTime = Convert.ToDateTime(Date.ToShortDateString() + " " + EndTimeAppointment + ":00");
            Doctor availableDoctor = getFirtsAvailableDoctor(startTime, endTime);

            if (availableDoctor == null)
            {
                MessageBox.Show("Nema slobodnog doktora!");

            }
            else
            {
                Room availableRoom = getFirstAvailableRoom(startTime, endTime);

                if (availableRoom == null)
                {
                    MessageBox.Show("Nema slobodnih sala!");

                }
                else
                {


                    if (moreThanTwoAppointmentsInOneDay(startTime))
                    {
                        MessageBox.Show("Vise od dva termina u jednom danu!");
                    }
                    else
                    {
                        int appointmentId = appointments.Count + 1;
                        Appointment newAppointment = new Appointment()
                        {
                            Id = appointmentId.ToString(),
                            StartTime = startTime,
                            EndTime = endTime,
                            Type = AppointmentType.Pregled,
                            doctor = availableDoctor,
                            room = availableRoom,
                            patient = patient

                        };
                        appointmentRepository.Save(newAppointment);
                        urgentPatientAppointment.NavigationService.Navigate(new ViewAppointment(patient));
                    }

                }
            }


        }
        private bool moreThanTwoAppointmentsInOneDay(DateTime startTime)
        {
            List<Appointment> la = appointmentRepository.GetAll();
            List<Appointment> sameDateAppointments = la.Where(ap => ap.patient.Jmbg.Equals(patient.Jmbg) && startTime.ToShortDateString().Equals(ap.StartTime.ToShortDateString())).ToList();
            if (sameDateAppointments.Count > 1)
            {
                return true;
            }
            return false;
        }

        private Room getFirstAvailableRoom(DateTime startTime, DateTime endTime)
        {
            int isFindAvailableRoom = 0;
            int isRoomAvailable = 1;
            Room availableRoom = new Room();
            for (int i = 0; i < rooms.Count; i++)
            {
                isRoomAvailable = 1;
                for (int j = 0; j < appointments.Count; j++)
                {

                    if (DateTime.Compare(appointments[j].StartTime, startTime) == 0 || DateTime.Compare(appointments[j].EndTime, endTime) == 0)
                    {
                        if (appointments[j].room.Id.Equals(rooms[i].Id))
                        {
                            isRoomAvailable = 0;
                            break;
                        }

                    }
                    else if (startTime >= appointments[j].StartTime && startTime < appointments[j].EndTime)
                    {
                        if (appointments[j].room.Id.Equals(rooms[i].Id))
                        {
                            isRoomAvailable = 0;
                            break;
                        }
                    }
                    else if (endTime >= appointments[j].StartTime && endTime < appointments[j].EndTime)
                    {
                        if (appointments[j].room.Id.Equals(rooms[i].Id))
                        {
                            isRoomAvailable = 0;
                            break;
                        }
                    }

                }
                if (isRoomAvailable == 1)
                {
                    availableRoom = rooms[i];
                    isFindAvailableRoom = 1;
                    break;

                }
            }
            if (isFindAvailableRoom == 0)
            {
                return null;

            }
            return availableRoom;
        }

        private Doctor getFirtsAvailableDoctor(DateTime startTime, DateTime endTime)
        {
            int isFindAvailableDoctor = 0;
            int isDoctorAvailable = 1;
            Doctor availableDoctor = new Doctor();
            for (int i = 0; i < doctors.Count; i++)
            {
                isDoctorAvailable = 1;
                for (int j = 0; j < appointments.Count; j++)
                {

                    if (DateTime.Compare(appointments[j].StartTime, startTime) == 0 || DateTime.Compare(appointments[j].EndTime, endTime) == 0)
                    {
                        if (appointments[j].doctor.Jmbg.Equals(doctors[i].Jmbg))
                        {
                            isDoctorAvailable = 0;
                            break;
                        }

                    }
                    else if (startTime > appointments[j].StartTime && startTime < appointments[j].EndTime)
                    {
                        if (appointments[j].doctor.Jmbg.Equals(doctors[i].Jmbg))
                        {
                            isDoctorAvailable = 0;
                            break;
                        }
                    }
                    else if (endTime > appointments[j].StartTime && endTime < appointments[j].EndTime)
                    {
                        if (appointments[j].doctor.Jmbg.Equals(doctors[i].Jmbg))
                        {
                            isDoctorAvailable = 0;
                            break;
                        }
                    }

                }
                if (isDoctorAvailable == 1)
                {
                    availableDoctor = doctors[i];
                    isFindAvailableDoctor = 1;
                    break;
                }
            }
            if (isFindAvailableDoctor == 0)
            {
                return null;

            }
            return availableDoctor;
        }
        private void Execute_CancelAddAppointment(object obj)
        {
            urgentPatientAppointment.NavigationService.Navigate(new ViewAppointment(patient));

        }
        private bool CanExecute_Command(object obj) {
            return true;
        }

        public UrgentPatientAppointmentViewModel(Patient patient, UrgentPatientAppointment urgentPatientAppointment) {
            this.patient = patient;
            this.urgentPatientAppointment = urgentPatientAppointment;
            Date = DateTime.Now;
            appointmentRepository = new AppointmentsRepository();
            appointments = appointmentRepository.GetAll();
            DoctorService doctorService = new DoctorService();
            doctors = doctorService.GetAll();
            RoomService roomService = new RoomService();
            rooms = roomService.GetAll();
            confirmAddAppointment = new RelayCommand(Execute_ConfirmAddAppointment, CanExecute_Command);
            cancelAddAppointment = new RelayCommand(Execute_CancelAddAppointment,CanExecute_Command);
        
        }
    }
}
