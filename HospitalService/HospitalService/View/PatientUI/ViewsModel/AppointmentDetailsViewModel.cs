using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Navigation;
using HospitalService.Service;
using HospitalService.View.PatientUI.Pages;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
    public class AppointmentDetailsViewModel : ViewModelPatientClass
    {
        public String Doctor { get; set; }
        public String Date { get; set; }
        public String Time { get; set; }
        public String Duration { get; set; }
        public String Room { get; set; }
        public String ServiceType { get; set; }
        private Appointment appointment;
        private AppointmentDetails appointmentDetails;
        private AppointmentsService appointmentService;
        private Patient patient;
        private NavigationService navigationService;
        public RelayCommand deleteAppointment { get; set; }

        public RelayCommand moveAppointment { get; set; }

        private void Execute_DeleteAppointment(object obj)
        {

            
            if (appointmentService.getNumberOfCanceledAppointments(patient) >= 2)
            {
                MessageBox.Show("Prekoracili ste broj termina za otkazivanje!");
            }
            else
            {
                appointmentService.DeletePatientAppointment(appointment.Id);
                appointmentService = new AppointmentsService();
                appointmentDetails.NavigationService.Navigate(new ViewAppointment(patient));

            }

        }

        private void Execute_MoveAppointment(object obj)
        {
            
            int movedAppointments = appointmentService.getNumberOfMovedAppointments(patient);
            if (appointment.Status == Status.Moved || movedAppointments >= 3)
            {
                String messageForMoving = "Termin je vec pomjeran, ne mozete ga pomjeriti ponovo" + "\n" + "(Moguce i da ste prekoracili dozvoljen broj pomjerenih termina!)";
                MessageBox.Show(messageForMoving);
            }
            else
            {
                navigationService.Navigate(new MoveAppointment(appointment, patient,appointmentDetails));
            }
            

        }
        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        public AppointmentDetailsViewModel(Patient patient,Appointment appointment, AppointmentDetails appointmentDetails, NavigationService navigationService) {
            this.appointment = appointment;
            this.patient = patient;
            this.appointmentDetails = appointmentDetails;
            this.navigationService = navigationService;
            Doctor = appointment.doctor.Name + " " + appointment.doctor.Surname;
            Date = appointment.StartTime.ToShortDateString();
            Time = appointment.StartTime.ToShortTimeString();
            TimeSpan difference = appointment.EndTime.Subtract(appointment.StartTime);
            double totalHours = difference.TotalHours;
            Duration = Convert.ToString(totalHours) + "h";
            Room = appointment.room.Id;
            ServiceType = appointment.Type.ToString();
            appointmentService = new AppointmentsService();
            deleteAppointment = new RelayCommand(Execute_DeleteAppointment, CanExecute_Command);
            moveAppointment = new RelayCommand(Execute_MoveAppointment, CanExecute_Command);

        }
    }
}
