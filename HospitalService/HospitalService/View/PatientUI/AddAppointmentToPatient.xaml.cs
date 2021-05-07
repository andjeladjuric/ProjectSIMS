using HospitalService.Storage;
using Model;
using Storage;
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

namespace HospitalService.View.PatientUI
{
    /// <summary>
    /// Interaction logic for AddAppointmentToPatient.xaml
    /// </summary>
    public partial class AddAppointmentToPatient : Window
    {
        public AppointmentStorage appointmentStorage { get; set; }
        public RoomFileStorage roomStorage { get; set; }

        public DoctorStorage doctorStorage { get; set; }
        public Patient patient { get; set; }

        public List<Appointment> appointments { get; set; }

        public List<Doctor> doctors { get; set; }
        
        public List<Room> rooms { get; set; }
        public AddAppointmentToPatient(Patient p)
        {
            InitializeComponent();
            roomStorage = new RoomFileStorage();
            rooms = roomStorage.GetAll();
            appointmentStorage = new AppointmentStorage();
            appointments = appointmentStorage.GetAll();
            patient = p;
            doctorStorage = new DoctorStorage();
            doctors = doctorStorage.GetAll(); 
            DoctorBox.ItemsSource = doctors;
            IdBox.Text = appointmentStorage.GetNextId();


        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            String start = StartBox.Text;
            String end = EndBox.Text;
            String date = DateBox.Text;
            DateTime startTime = Convert.ToDateTime(date + " " + start + ":00");
            DateTime endTime = Convert.ToDateTime(date + " " + end + ":00");
            Doctor selectedDoctor = (Doctor)DoctorBox.SelectedItem;
            

            
            if (!isDoctorAvailable(startTime, endTime, selectedDoctor))
            {

                MessageBox.Show("Doktor je zauzet!");
            }
            else
            {
                
                Room availableRoom= getFirstAvailableRoom(startTime, endTime);
                

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
                        Appointment newAppointment = new Appointment()
                        {
                            Id = IdBox.Text,
                            StartTime = startTime,
                            EndTime = endTime,
                            Type = AppointmentType.Pregled,
                            doctor = selectedDoctor,
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

        private bool isDoctorAvailable(DateTime startTime, DateTime endTime, Doctor selectedDoctor)
        {
            
            for (int i = 0; i < appointments.Count; i++)
            {

                if ((DateTime.Compare(appointments[i].StartTime, startTime) == 0 || DateTime.Compare(appointments[i].EndTime, endTime) == 0) && appointments[i].doctor.Jmbg.Equals(selectedDoctor.Jmbg))
                {

                    return false;
                }
                else if (startTime > appointments[i].StartTime && startTime < appointments[i].EndTime && appointments[i].doctor.Jmbg.Equals(selectedDoctor.Jmbg))
                {

                    return false;

                }
                else if (endTime > appointments[i].StartTime && endTime < appointments[i].EndTime && appointments[i].doctor.Jmbg.Equals(selectedDoctor.Jmbg))
                {

                    return false;

                }

            }

            return true;
        }
    }
}

