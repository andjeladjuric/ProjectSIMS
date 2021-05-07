using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;
using Storage;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for MoveAppointment.xaml
    /// </summary>
    public partial class MoveAppointment : Page
    {

        public Appointment Appointment { get; set; }
        public DataGrid TableOfPatientAppointments { get; set; }
        public AppointmentStorage AppointmentStorage { get; set; }
        public Patient Patient { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Room> Rooms { get; set; }
        public MoveAppointment(Appointment appointment, DataGrid appointmentTable, AppointmentStorage appointmentStorage, Patient patient)
        {
            InitializeComponent();
            this.DataContext = this;
            Appointment = appointment;
            TableOfPatientAppointments = appointmentTable;
            AppointmentStorage = appointmentStorage;
            Patient = patient;
            Appointments = AppointmentStorage.GetAll();
            RoomFileStorage bazaSala = new RoomFileStorage();
            Rooms = bazaSala.GetAll();
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            String timeOfStart = tb1.Text;
            String timeOfEnd = tb2.Text;
            String appointmentDate = dp.Text;
            DateTime startTimeOfAppointment = Convert.ToDateTime(appointmentDate + " " + timeOfStart + ":00");
            DateTime endTimeOfAppointment = Convert.ToDateTime(appointmentDate + " " + timeOfEnd + ":00");
            if (isLessThanTwoDaysBetween(startTimeOfAppointment))
            {
               
                if (!isDoctorAvailable(startTimeOfAppointment, endTimeOfAppointment))
                {

                    MessageBox.Show("Doktor je zauzet!");
                }
                else
                {
                  
                    if (isCurrentRoomOfAppointmentAvailable(startTimeOfAppointment, endTimeOfAppointment))
                    {

                        AppointmentStorage.Move(Appointment.Id, startTimeOfAppointment, endTimeOfAppointment, Appointment.room);
                        TableOfPatientAppointments.Items.Refresh();
                        this.NavigationService.Navigate(new ViewAppointment(Patient));


                    }
                    else
                    {
                        Room availableRoom= getFirstAvailableRoom(startTimeOfAppointment, endTimeOfAppointment);
                        if (availableRoom==null)
                        {
                            MessageBox.Show("Nema slobodnih sala!");

                        }
                        else
                        {

                            AppointmentStorage.Move(Appointment.Id, startTimeOfAppointment, endTimeOfAppointment, availableRoom);
                            TableOfPatientAppointments.Items.Refresh();
                            this.NavigationService.Navigate(new ViewAppointment(Patient));

                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vise od dva dana izmedju termina!");

            }
        }

        private Room getFirstAvailableRoom(DateTime startTimeOfAppointment, DateTime endTimeOfAppointment)
        {
            int isFindAvailableRoom = 0;
            int isRoomAvailable = 1;
            Room availableRoom = new Room();
            for (int i = 0; i < Rooms.Count; i++)
            {
                isRoomAvailable = 1;
                for (int j = 0; j < Appointments.Count; j++)
                {

                    if (DateTime.Compare(Appointments[j].StartTime, startTimeOfAppointment) == 0 || DateTime.Compare(Appointments[j].EndTime, endTimeOfAppointment) == 0)
                    {
                        if (Appointments[j].room.Id.Equals(Rooms[i].Id))
                        {
                            isRoomAvailable = 0;
                            break;
                        }

                    }
                    else if (startTimeOfAppointment >= Appointments[j].StartTime && startTimeOfAppointment < Appointments[j].EndTime)
                    {
                        if (Appointments[j].room.Id.Equals(Rooms[i].Id))
                        {
                            isRoomAvailable = 0;
                            break;
                        }
                    }
                    else if (endTimeOfAppointment >= Appointments[j].StartTime && endTimeOfAppointment < Appointments[j].EndTime)
                    {
                        if (Appointments[j].room.Id.Equals(Rooms[i].Id))
                        {
                            isRoomAvailable = 0;
                            break;
                        }
                    }

                }
                if (isRoomAvailable == 1)
                {
                    availableRoom = Rooms[i];
                    isFindAvailableRoom = 1;
                    break;

                }
            }
            if (isFindAvailableRoom == 0) {
                return null;
            }
            return availableRoom;
        }

        private bool isCurrentRoomOfAppointmentAvailable(DateTime startTimeOfAppointment, DateTime endTimeOfAppointment)
        {
           
            for (int i = 0; i < Appointments.Count; i++)
            {

                if ((DateTime.Compare(Appointments[i].StartTime, startTimeOfAppointment) == 0 || DateTime.Compare(Appointments[i].EndTime, endTimeOfAppointment) == 0) && Appointments[i].room.Id.Equals(Appointment.room.Id))
                {

                    return false;
                }
                else if (startTimeOfAppointment > Appointments[i].StartTime && startTimeOfAppointment < Appointments[i].EndTime && Appointments[i].room.Id.Equals(Appointment.room.Id))
                {

                    return false;

                }
                else if (endTimeOfAppointment > Appointments[i].StartTime && endTimeOfAppointment < Appointments[i].EndTime && Appointments[i].room.Id.Equals(Appointment.room.Id))
                {

                    return false;

                }

            }

            return true;
        }

        private bool isDoctorAvailable(DateTime startTimeOfAppointment, DateTime endTimeOfAppointment)
        {
           
            for (int i = 0; i < Appointments.Count; i++)
            {

                if ((DateTime.Compare(Appointments[i].StartTime, startTimeOfAppointment) == 0 || DateTime.Compare(Appointments[i].EndTime, endTimeOfAppointment) == 0) && Appointments[i].doctor.Jmbg.Equals(Appointment.doctor.Jmbg))
                {

                    return false;
                }
                else if (startTimeOfAppointment > Appointments[i].StartTime && startTimeOfAppointment < Appointments[i].EndTime && Appointments[i].doctor.Jmbg.Equals(Appointment.doctor.Jmbg))
                {

                    return false;

                }
                else if (endTimeOfAppointment > Appointments[i].StartTime && endTimeOfAppointment < Appointments[i].EndTime && Appointments[i].doctor.Jmbg.Equals(Appointment.doctor.Jmbg))
                {

                    return false;

                }

            }
            return true;

           
        }

        private bool isLessThanTwoDaysBetween(DateTime startTimeOfAppointment)
        {
            return (startTimeOfAppointment.Date - Appointment.StartTime.Date).TotalDays <= 2;
        }

        

        private void CancelClick(object sender, RoutedEventArgs e)
        {

            this.NavigationService.Navigate(new ViewAppointment(Patient));
        }
    }
}
