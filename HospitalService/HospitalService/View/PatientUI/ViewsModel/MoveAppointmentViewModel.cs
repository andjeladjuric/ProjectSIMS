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
        private AppointmentsService appointmentsService;
        private Appointment appointment;
        private RoomService roomService;
        private DoctorService doctorService;
        private MoveAppointment moveAppointment;

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
                if (!doctorService.isDoctorAvailable(startTimeOfAppointment, endTimeOfAppointment,appointment.doctor))
                {
                    MessageBox.Show("Doktor je zauzet!");
                    return;
                }
                if (roomService.isCurrentRoomAvailable(startTimeOfAppointment, endTimeOfAppointment,appointment.room))
                {                 
                    appointmentsService.Move(appointment.Id, startTimeOfAppointment, endTimeOfAppointment, appointment.room);
                    moveAppointment.NavigationService.Navigate(new ViewAppointment(patient));
                }
                else
                {
                    Room availableRoom = roomService.getFirstAvailableRoom(startTimeOfAppointment, endTimeOfAppointment);
                    if (availableRoom == null)
                    {
                        MessageBox.Show("Nema slobodnih sala!");
                        return;
                    }
                    appointmentsService.Move(appointment.Id, startTimeOfAppointment, endTimeOfAppointment, availableRoom);
                    moveAppointment.NavigationService.Navigate(new ViewAppointment(patient));    
                }               
            }
            else
            {
                MessageBox.Show("Vise od dva dana izmedju termina!");
            }

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
            appointmentsService = new AppointmentsService();
            roomService = new RoomService();
            doctorService = new DoctorService();
            confirmMoveAppointment = new RelayCommand(Execute_ConfirmMoveAppointment,CanExecute_Command);
            cancelMoveAppointment = new RelayCommand(Execute_CancelMoveAppointment,CanExecute_Command);
        }
    }
}
