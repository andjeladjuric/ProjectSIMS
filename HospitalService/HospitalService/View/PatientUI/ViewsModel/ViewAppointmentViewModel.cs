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
    public class ViewAppointmentViewModel:ViewModelPatientClass
    {
        public RelayCommand showAppointments { get; set; }
        public RelayCommand deleteAppointment { get; set; }

        public RelayCommand moveAppointment { get; set; }
        public RelayCommand editAppointment { get; set; }

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




        private AppointmentsService appointmentService;
        private Patient patient;

        private void Execute_ShowAppointments(object obj)
        {
            DateTime selectedDate = Date;
            List<Appointment> todaysAppointments = appointmentService.getAppointmentsByDate(patient, selectedDate);
            this.Appointments = new ObservableCollection<Appointment>();
            todaysAppointments.ForEach(this.Appointments.Add);
            


        }

        private void Execute_DeleteAppointment(object obj) {

            
            if (SelectedAppointment == null)
            {
                MessageBox.Show("Morate odabrati jedan termin!");
            }
            else
            {
                appointmentService.Delete(SelectedAppointment.Id);
                
                List<Appointment> todaysAppointments = appointmentService.getAppointmentsByDate(patient, Date);
                this.Appointments = new ObservableCollection<Appointment>();
                todaysAppointments.ForEach(Appointments.Add);
                


            }

        }

        private void Execute_MoveAppointment(object obj) {


            
            if (SelectedAppointment == null)
            {
                MessageBox.Show("Morate odabrati jedan termin!");
            }
            else
            {
                int movedAppointments = appointmentService.getNumberOfMovedAppointments(patient);
                if (SelectedAppointment.Status == Status.Moved || movedAppointments >= 3)
                {
                    String messageForMoving = "Termin je vec pomjeran, ne mozete ga pomjeriti ponovo" + "\n" + "(Moguce i da ste prekoracili dozvoljen broj pomjerenih termina!)";
                    MessageBox.Show(messageForMoving);
                }
                else
                {
                    viewAppointments.NavigationService.Navigate(new MoveAppointment(SelectedAppointment, patient));
                }
            }

        }



        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        public ViewAppointmentViewModel(Patient patient, ViewAppointment viewAppointments) {

            Date = DateTime.Now;
            this.viewAppointments = viewAppointments;
            appointmentService = new AppointmentsService();
            this.patient = patient;
            

            
            this.Appointments = new ObservableCollection<Appointment>();
            List<Appointment> todaysAppointments = appointmentService.GetByPatient(patient,Date);
            todaysAppointments.ForEach(Appointments.Add);
            showAppointments = new RelayCommand(Execute_ShowAppointments,CanExecute_Command);
            deleteAppointment = new RelayCommand(Execute_DeleteAppointment,CanExecute_Command);
            moveAppointment = new RelayCommand(Execute_MoveAppointment,CanExecute_Command);
            

            


        }
    }
}
