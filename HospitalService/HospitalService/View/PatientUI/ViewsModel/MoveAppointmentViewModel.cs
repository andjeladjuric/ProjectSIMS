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
 

        public String SelectedTime { get; set; }
        private DateTime appointmentDate { get; set; }
        private Patient patient;
        private AppointmentsService appointmentsService;
        private Appointment appointment;
        private RoomService roomService;
        private DoctorService doctorService;
        private AppointmentDetails appointmentDetails;

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

            String[] startTimeArray1 = SelectedTime.Split(" ");
            String startTimeCb = startTimeArray1[1];
            String[] startTimeArray2 = startTimeCb.Split(":");

            int endTimeCb = int.Parse(startTimeArray2[0]) + 1;
            String shortEndTime = Convert.ToString(endTimeCb);

            DateTime startTimeOfAppointment = Convert.ToDateTime(AppointmentDate.ToShortDateString() + " " + startTimeCb);
            DateTime endTimeOfAppointment = Convert.ToDateTime(AppointmentDate.ToShortDateString() + " " + shortEndTime + ":00");
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
                    appointmentDetails.NavigationService.Navigate(new ViewAppointment(patient));
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
                    appointmentDetails.NavigationService.Navigate(new ViewAppointment(patient));    
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

            appointmentDetails.NavigationService.Navigate(new AppointmentDetails(patient,appointment));
        }
        private bool CanExecute_Command(object obj) {
            return true;
        }
        public MoveAppointmentViewModel(Patient patient, Appointment appointment, AppointmentDetails appointmentDetails) {
            this.patient = patient;
            this.appointment = appointment;
            this.appointmentDetails = appointmentDetails;
            AppointmentDate = DateTime.Now;
            appointmentsService = new AppointmentsService();
            roomService = new RoomService();
            doctorService = new DoctorService();
            confirmMoveAppointment = new RelayCommand(Execute_ConfirmMoveAppointment,CanExecute_Command);
            cancelMoveAppointment = new RelayCommand(Execute_CancelMoveAppointment,CanExecute_Command);
        }
    }
}
