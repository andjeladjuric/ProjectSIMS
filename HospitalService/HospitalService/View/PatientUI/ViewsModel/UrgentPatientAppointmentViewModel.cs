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
        private DoctorService doctorService;
        private RoomService roomService;
        private AppointmentsService appointmentsService;

        private void Execute_ConfirmAddAppointment(object obj) {
         
            DateTime startTime = Convert.ToDateTime(Date.ToShortDateString() + " " + StartTimeAppointment + ":00");
            DateTime endTime = Convert.ToDateTime(Date.ToShortDateString() + " " + EndTimeAppointment + ":00");
            Doctor availableDoctor = doctorService.getFirstAvailableDoctor(startTime, endTime);

            if (availableDoctor == null)
            {
                MessageBox.Show("Nema slobodnog doktora!");
                return;
            }           
            Room availableRoom = roomService.getFirstAvailableRoom(startTime, endTime);
            if (availableRoom == null)
            {
                MessageBox.Show("Nema slobodnih sala!");
                return;
            }
            if (moreThanTwoAppointmentsInOneDay(startTime))
            {
                MessageBox.Show("Vise od dva termina u jednom danu!");
                return;
            }
            Appointment newAppointment = new Appointment() { Id = appointmentsService.GetNextId(), StartTime = startTime, EndTime = endTime, Type = AppointmentType.Pregled, doctor = availableDoctor, room = availableRoom, patient = patient };
            appointmentsService.Save(newAppointment);
            urgentPatientAppointment.NavigationService.Navigate(new ViewAppointment(patient));
                   
        }
        private bool moreThanTwoAppointmentsInOneDay(DateTime startTime)
        {
            int sameDateAppointments = appointmentsService.getNumberOfSameDateAppointments(patient, startTime);
            if (sameDateAppointments > 1)
            {
                return true;
            }
            return false;
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
            doctorService = new DoctorService();
            roomService = new RoomService();
            appointmentsService = new AppointmentsService();           
            confirmAddAppointment = new RelayCommand(Execute_ConfirmAddAppointment, CanExecute_Command);
            cancelAddAppointment = new RelayCommand(Execute_CancelAddAppointment,CanExecute_Command);
        
        }
    }
}
