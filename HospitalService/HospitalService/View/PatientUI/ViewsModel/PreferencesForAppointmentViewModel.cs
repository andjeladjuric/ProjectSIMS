using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using HospitalService.View.PatientUI.Pages;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
  public  class PreferencesForAppointmentViewModel: ViewModelPatientClass
    {
        public bool CheckedYes { get; set; }
        public bool CheckedNo { get; set; }
        private Patient patient { get; set; }
        private PreferencesForAppointment preferencesForAppointment;

        public RelayCommand addAppointment { get; set; }

        private void Execute_AddAppointment(object obj) {

            if (CheckedNo == true)
            {
                preferencesForAppointment.NavigationService.Navigate(new AddPatientAppointment(patient));
                /*AddAppointmentToPatient addAppointmentWindow = new AddAppointmentToPatient(patient);
                addAppointmentWindow.Show();*/
            }
            else if (CheckedYes == true)
            {

                UrgentAppointment urgentAppointmentWindow = new UrgentAppointment(patient);
                urgentAppointmentWindow.Show();

            }
            else {
                MessageBox.Show("Odaberite jedan od nacina zakazivanja!");
            
            }

            

        }

        private bool CanExecute_Command(object obj) {
            return true;
        }

        public PreferencesForAppointmentViewModel(Patient patient, PreferencesForAppointment preferencesForAppointment) {
            this.patient = patient;
            this.preferencesForAppointment = preferencesForAppointment;
            addAppointment = new RelayCommand(Execute_AddAppointment,CanExecute_Command);
        
        }
    }
}
