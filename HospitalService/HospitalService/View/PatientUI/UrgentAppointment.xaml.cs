using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HospitalService.Storage;
using Model;
using Storage;

namespace HospitalService.View.PatientUI
{
    /// <summary>
    /// Interaction logic for UrgentAppointment.xaml
    /// </summary>
    public partial class UrgentAppointment : Window
    {
        public Patient patient { get; set; }

        public List<Appointment> appointments { get; set; }
        public List<Doctor> doctors { get; set; }
        public List<Room> rooms { get; set; }

        public AppointmentStorage appointmentStorage { get; set; }
        public UrgentAppointment(Patient p)

        {
            InitializeComponent();
            this.DataContext = this;
            patient = p;
            appointmentStorage = new AppointmentStorage();
            appointments = appointmentStorage.GetAll();
            DoctorStorage bazaDoktora = new DoctorStorage();
            doctors = bazaDoktora.GetAll();
            RoomFileStorage bazaSala = new RoomFileStorage();
            rooms = bazaSala.GetAll();
            
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            String start = tb1.Text;
            String end = tb2.Text;
            String date = dp.Text;
            DateTime startTime = Convert.ToDateTime(date + " " + start + ":00");
            DateTime endTime = Convert.ToDateTime(date + " " + end + ":00");
            Doctor availableDoctor= getFirtsAvailableDoctor(startTime, endTime);
            
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
                        int appointmentId = appointments.Count+1;
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
                        appointmentStorage.Save(newAppointment);
                        this.Close();
                    }

                }
            }




        }
        private bool moreThanTwoAppointmentsInOneDay(DateTime startTime)
        {
            List<Appointment> la = appointmentStorage.GetAll();
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

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
