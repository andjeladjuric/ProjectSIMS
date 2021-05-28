using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using HospitalService.Repositories;
using HospitalService.Service;
using HospitalService.View.PatientUI.Pages;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
   public class MoveAppointmentViewModel:ViewModelPatientClass
    {
        public String StartTimeAppointment { get; set; }
        public String EndTimeAppointment { get; set; }
        private DateTime appointmentDate { get; set; }
        private Patient patient;
        private Appointment appointment;
        private AppointmentsService appointmentService;
        private RoomService roomService;
        private List<Room> rooms;
        private MoveAppointment moveAppointment;
        private List<Appointment> appointments;
        private AppointmentsRepository appointmentRepository;

        public DateTime AppointmentDate
        {
            get { return appointmentDate; }
            set
            {
                appointmentDate = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand confirmMoveAppointment { get; set; }
        public RelayCommand cancelMoveAppointment { get; set; }
        private void Execute_ConfirmMoveAppointment(object obj) {

            
            DateTime startTimeOfAppointment = Convert.ToDateTime(AppointmentDate.ToShortDateString() + " " + StartTimeAppointment + ":00");
            DateTime endTimeOfAppointment = Convert.ToDateTime(AppointmentDate.ToShortDateString() + " " + EndTimeAppointment + ":00");
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

                        appointmentRepository.Move(appointment.Id, startTimeOfAppointment, endTimeOfAppointment, appointment.room);

                        moveAppointment.NavigationService.Navigate(new ViewAppointment(patient));


                    }
                    else
                    {
                        Room availableRoom = getFirstAvailableRoom(startTimeOfAppointment, endTimeOfAppointment);
                        if (availableRoom == null)
                        {
                            MessageBox.Show("Nema slobodnih sala!");

                        }
                        else
                        {

                            appointmentRepository.Move(appointment.Id, startTimeOfAppointment, endTimeOfAppointment, availableRoom);
                           
                            moveAppointment.NavigationService.Navigate(new ViewAppointment(patient));

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
            for (int i = 0; i < rooms.Count; i++)
            {
                isRoomAvailable = 1;
                for (int j = 0; j < appointments.Count; j++)
                {

                    if (DateTime.Compare(appointments[j].StartTime, startTimeOfAppointment) == 0 || DateTime.Compare(appointments[j].EndTime, endTimeOfAppointment) == 0)
                    {
                        if (appointments[j].room.Id.Equals(rooms[i].Id))
                        {
                            isRoomAvailable = 0;
                            break;
                        }

                    }
                    else if (startTimeOfAppointment >= appointments[j].StartTime && startTimeOfAppointment < appointments[j].EndTime)
                    {
                        if (appointments[j].room.Id.Equals(rooms[i].Id))
                        {
                            isRoomAvailable = 0;
                            break;
                        }
                    }
                    else if (endTimeOfAppointment >= appointments[j].StartTime && endTimeOfAppointment < appointments[j].EndTime)
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

        private bool isCurrentRoomOfAppointmentAvailable(DateTime startTimeOfAppointment, DateTime endTimeOfAppointment)
        {

            for (int i = 0; i < appointments.Count; i++)
            {

                if ((DateTime.Compare(appointments[i].StartTime, startTimeOfAppointment) == 0 || DateTime.Compare(appointments[i].EndTime, endTimeOfAppointment) == 0) && appointments[i].room.Id.Equals(appointment.room.Id))
                {

                    return false;
                }
                else if (startTimeOfAppointment > appointments[i].StartTime && startTimeOfAppointment < appointments[i].EndTime && appointments[i].room.Id.Equals(appointment.room.Id))
                {

                    return false;

                }
                else if (endTimeOfAppointment > appointments[i].StartTime && endTimeOfAppointment < appointments[i].EndTime && appointments[i].room.Id.Equals(appointment.room.Id))
                {

                    return false;

                }

            }

            return true;
        }

        private bool isDoctorAvailable(DateTime startTimeOfAppointment, DateTime endTimeOfAppointment)
        {

            for (int i = 0; i < appointments.Count; i++)
            {

                if ((DateTime.Compare(appointments[i].StartTime, startTimeOfAppointment) == 0 || DateTime.Compare(appointments[i].EndTime, endTimeOfAppointment) == 0) && appointments[i].doctor.Jmbg.Equals(appointment.doctor.Jmbg))
                {

                    return false;
                }
                else if (startTimeOfAppointment > appointments[i].StartTime && startTimeOfAppointment < appointments[i].EndTime && appointments[i].doctor.Jmbg.Equals(appointment.doctor.Jmbg))
                {

                    return false;

                }
                else if (endTimeOfAppointment > appointments[i].StartTime && endTimeOfAppointment < appointments[i].EndTime && appointments[i].doctor.Jmbg.Equals(appointment.doctor.Jmbg))
                {

                    return false;

                }

            }
            return true;


        }
        private bool isLessThanTwoDaysBetween(DateTime startTimeOfAppointment)
        {
            return (startTimeOfAppointment.Date - appointment.StartTime.Date).TotalDays <= 2;
        }
        private void Execute_CancelMoveAppointment(object obj) {

            moveAppointment.NavigationService.Navigate(new ViewAppointment(patient));
        }
        private bool CanExecute_Command(object obj) {
            return true;
        }
        public MoveAppointmentViewModel(Patient patient, Appointment appointment, MoveAppointment moveAppointment) {
            this.patient = patient;
            this.appointment = appointment;
            this.moveAppointment = moveAppointment;
            AppointmentDate = DateTime.Now;
            appointmentService = new AppointmentsService();
            roomService = new RoomService();
            appointmentRepository = new AppointmentsRepository();
            appointments = appointmentRepository.GetAll();
            rooms = roomService.GetAll();
            confirmMoveAppointment = new RelayCommand(Execute_ConfirmMoveAppointment,CanExecute_Command);
            cancelMoveAppointment = new RelayCommand(Execute_CancelMoveAppointment,CanExecute_Command);
        }
    }
}
