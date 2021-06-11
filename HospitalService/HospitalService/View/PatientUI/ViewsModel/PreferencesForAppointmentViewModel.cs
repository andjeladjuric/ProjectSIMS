using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Navigation;
using HospitalService.View.PatientUI.Pages;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
  public  class PreferencesForAppointmentViewModel: ViewModelPatientClass
    {
        public bool CheckedDate { get; set; }
        public bool CheckedDoctor { get; set; }

        public bool CheckedReferral { get; set; }
        private Patient patient { get; set; }
        private PreferencesForAppointment preferencesForAppointment;
        private NavigationService navigationService;

        public RelayCommand addAppointment { get; set; }
        //public RelayCommand showReferrals { get; set; }

        private void Execute_AddAppointment(object obj) {

            if (CheckedDoctor == true)
            {
                navigationService.Navigate(new AddPatientAppointment(patient, preferencesForAppointment));

            }
            else if (CheckedDate == true)
            {
                navigationService.Navigate(new UrgentPatientAppointment(patient,preferencesForAppointment));


            } else if (CheckedReferral==true) {

                preferencesForAppointment.NavigationService.Navigate(new ReferralAppointment(patient));
            }
            else {
                MessageBox.Show("Odaberite jedan od nacina zakazivanja!");

            }

            

        }
        /*private void Execute_ShowReferrals(object obj) {

            preferencesForAppointment.NavigationService.Navigate(new ReferralAppointment(patient));
        }*/

        private bool CanExecute_Command(object obj) {
            return true;
        }

        public PreferencesForAppointmentViewModel(Patient patient, PreferencesForAppointment preferencesForAppointment, NavigationService navigationService) {
            this.patient = patient;
            this.preferencesForAppointment = preferencesForAppointment;
            this.navigationService = navigationService;
            addAppointment = new RelayCommand(Execute_AddAppointment,CanExecute_Command);
            //showReferrals = new RelayCommand(Execute_ShowReferrals,CanExecute_Command);
        
        }
    }
}
