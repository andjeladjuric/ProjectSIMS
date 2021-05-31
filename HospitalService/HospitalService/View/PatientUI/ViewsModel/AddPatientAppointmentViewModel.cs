using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using HospitalService.Repositories;
using HospitalService.Service;
using HospitalService.View.PatientUI.Pages;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
   public class AddPatientAppointmentViewModel:ViewModelPatientClass
    {
        public String AppointmentId { get; set; }
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
        private Doctor selectedDoctor;
        private ObservableCollection<Doctor> doctors;
        public ObservableCollection<Doctor> Doctors
        {
            get { return doctors; }
            set
            {
                doctors = value;
                OnPropertyChanged();
            }
        }
        public Doctor SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged();
            }
        }
        public String StartTimeAppointment { get; set; }
        public String EndTimeAppointment { get; set; }
        public RelayCommand confirmAddAppointment { get; set; }
        public RelayCommand cancelAddAppointment { get; set; }
        private DoctorService doctorService;        
        private AppointmentsService appointmentsService;
        private RoomService roomService;
        private Patient patient;
        private AddPatientAppointment addPatientAppointment;
        private void Execute_ConfirmAddAppointment(object obj) {
          
            DateTime startTime = Convert.ToDateTime(Date.ToShortDateString() + " " + StartTimeAppointment + ":00");
            DateTime endTime = Convert.ToDateTime(Date.ToShortDateString() + " " + EndTimeAppointment + ":00");
           
            if (!doctorService.isDoctorAvailable(startTime, endTime, SelectedDoctor))
            {
                MessageBox.Show("Doktor je zauzet!");
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
                Appointment newAppointment = new Appointment() { Id = AppointmentId, StartTime = startTime, EndTime = endTime, Type = AppointmentType.Pregled, doctor = SelectedDoctor, room = availableRoom, patient = patient };                       
                appointmentsService.Save(newAppointment);
                addPatientAppointment.NavigationService.Navigate(new ViewAppointment(patient));            
        }

        private bool moreThanTwoAppointmentsInOneDay(DateTime startTime)
        {
            int sameDateAppointments = appointmentsService.getNumberOfSameDateAppointments(patient,startTime);
            if (sameDateAppointments > 1)
            {
                return true;
            }
            return false;
        }
      
        private void Execute_CancelAddAppointment(object obj) {

            addPatientAppointment.NavigationService.Navigate(new ViewAppointment(patient));
        }
        private bool CanExecute_Command(object obj) {
            return true;
        }
        
        public AddPatientAppointmentViewModel(Patient patient,AddPatientAppointment addPatientAppointment) {
            this.patient = patient;
            this.addPatientAppointment = addPatientAppointment;
            Date = DateTime.Now;
            appointmentsService = new AppointmentsService();
            roomService = new RoomService();
            AppointmentId = appointmentsService.GetNextId();
            doctorService = new DoctorService();
            List<Doctor> docttorsForAppointment = doctorService.GetAll();
            Doctors = new ObservableCollection<Doctor>();
            docttorsForAppointment.ForEach(this.Doctors.Add);
            confirmAddAppointment = new RelayCommand(Execute_ConfirmAddAppointment,CanExecute_Command);
            cancelAddAppointment = new RelayCommand(Execute_CancelAddAppointment,CanExecute_Command);

        }
    }
}
