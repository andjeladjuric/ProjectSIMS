using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using HospitalService.Service;
using HospitalService.View.PatientUI.Pages;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
    public class ViewAppointmentViewModel:ValidationBase
    {
        public RelayCommand showAppointments { get; set; }
       

        private ViewAppointment viewAppointments;


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
        private Appointment selectedAppointment;
        private ObservableCollection<Appointment> appointments;
        public ObservableCollection<Appointment> Appointments
        {
            get { return appointments; }
            set
            {
                appointments = value;
                OnPropertyChanged();
            }
        }


        public Appointment SelectedAppointment
        {
            get { return selectedAppointment; }
            set
            {
                selectedAppointment = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand showAppointmentDetails { get; set; }


        private AppointmentsService appointmentService;
        private Patient patient;


        private void Execute_ShowAppointmentDetails(object obj) {
            this.Validate();
            if (IsValid)
            {
                
                viewAppointments.NavigationService.Navigate(new AppointmentDetails(patient, SelectedAppointment));
                
            }
        }
        private void Execute_ShowAppointments(object obj)
        {
            DateTime selectedDate = Date;
            appointmentService=new AppointmentsService();
            List<Appointment> todaysAppointments = appointmentService.getAppointmentsByDate(patient, selectedDate);
            this.Appointments = new ObservableCollection<Appointment>();
            todaysAppointments.ForEach(this.Appointments.Add);
            


        }


        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        protected override void ValidateSelf()
        {
            if (SelectedAppointment==null) {
                this.ValidationErrors["Start"] = "Odaberite jedan termin.";
            }
            else if (SelectedAppointment.StartTime <= DateTime.Now) {
                this.ValidationErrors["Start"] = "Ne mozete otkazati ili pomjeriti termin koji je prosao.";
            }
        }

        public ViewAppointmentViewModel(Patient patient, ViewAppointment viewAppointments) {

            Date = DateTime.Now;
            this.viewAppointments = viewAppointments;
            appointmentService = new AppointmentsService();
            this.patient = patient;
            

            
            this.Appointments = new ObservableCollection<Appointment>();
            List<Appointment> todaysAppointments = appointmentService.getAppointmentsByDate(patient,Date);
            todaysAppointments.ForEach(Appointments.Add);
            showAppointments = new RelayCommand(Execute_ShowAppointments,CanExecute_Command);
           
            showAppointmentDetails = new RelayCommand(Execute_ShowAppointmentDetails,CanExecute_Command);

            


        }
    }
}
