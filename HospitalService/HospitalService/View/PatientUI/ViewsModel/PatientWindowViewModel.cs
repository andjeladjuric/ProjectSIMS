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
    public class PatientWindowViewModel:ViewModelPatientClass
    {
        private NavigationService navigationService;
        private AppointmentsService appointmentService;
        public Patient patient { get; set; }

        private Window patientWindow {get; set;}
        public RelayCommand openViewProfile { get; set; }
        public RelayCommand openEditProfile { get; set; }

        public RelayCommand openAddAppointment { get; set; }
        public RelayCommand openViewAppointment { get; set; }
        public RelayCommand openViewMedicalRecord { get; set; }

        public RelayCommand openViewPrescriptions { get; set; }

        public RelayCommand openSurvey { get; set; }

        public RelayCommand openMainWindow { get; set; }

        private void Execute_NavigateToMainWindow(object obj)
        {
            new MainWindow().Show();
            patientWindow.Close();
        }

        private void Execute_NavigateToViewProfile(object obj)
        {
            this.navigationService.Navigate(
                new ProfileView(patient));
        }

        private void Execute_NavigateToEditProfile(object obj)
        {
            this.navigationService.Navigate(
                new EditProfile(patient));
        }

        private void Execute_NavigateToAddAppointment(object obj) {

            List<Appointment> notFinishedAppointment = appointmentService.getNotFinishedAppointments(patient);

            if (notFinishedAppointment.Count >= 5)
            {
                MessageBox.Show("Prekoracen maksimalan broj termina koje mozete da zakazete!");
            }
            else
            {

                this.navigationService.Navigate(new AddAppointmentToPatient(patient));
            }

            this.navigationService.Navigate(new PreferencesForAppointment(patient));
        }
        private void Execute_NavigateToViewAppointment(object obj)
        {

            this.navigationService.Navigate(new ViewAppointment(patient));
        }

        private void Execute_NavigateToViewMedicalRecord(object obj)
        {

            this.navigationService.Navigate(new MedicalRecordForPatient(patient));
        }
        private void Execute_NavigateToPrescriptions(object obj)
        {

            this.navigationService.Navigate(new MedicalRecordWithPrescriptions(patient));
        }

        private void Execute_NavigateToSurvey(object obj)
        {

            this.navigationService.Navigate(new Surveys(patient));
        }




        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }
        public PatientWindowViewModel(NavigationService navigationService,Patient patient, Window patientWindow) {
            this.patient = patient;
            this.navigationService = navigationService;
            appointmentService = new AppointmentsService();
            this.patientWindow = patientWindow;
            this.navigationService.Navigate(
                new ProfileView(patient));
            this.openViewProfile = new RelayCommand(Execute_NavigateToViewProfile,CanExecute_NavigateCommand);
            this.openEditProfile = new RelayCommand(Execute_NavigateToEditProfile,CanExecute_NavigateCommand);
            this.openAddAppointment = new RelayCommand(Execute_NavigateToAddAppointment, CanExecute_NavigateCommand);
            openViewAppointment = new RelayCommand(Execute_NavigateToViewAppointment,CanExecute_NavigateCommand);
            openViewMedicalRecord = new RelayCommand(Execute_NavigateToViewMedicalRecord, CanExecute_NavigateCommand);
            openViewPrescriptions = new RelayCommand(Execute_NavigateToPrescriptions,CanExecute_NavigateCommand);
            openSurvey = new RelayCommand(Execute_NavigateToSurvey,CanExecute_NavigateCommand);
            openMainWindow = new RelayCommand(Execute_NavigateToMainWindow,CanExecute_NavigateCommand);



        }
    }
}
