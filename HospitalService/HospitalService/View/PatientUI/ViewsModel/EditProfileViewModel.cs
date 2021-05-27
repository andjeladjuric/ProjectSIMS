using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Navigation;
using HospitalService.Service;
using HospitalService.View.PatientUI.Pages;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
   public class EditProfileViewModel:ViewModelPatientClass
    {
        public String PatientGender { get; set; }
        public String PatientDateOfBirth { get; set; }
        public String PatientJmbg { get; set; }
        public String PatientAddress { get; set; }
        public String PatientEmail { get; set; }

        public String PatientPhone { get; set; }
        public RelayCommand editPassword { get; set; }
        public RelayCommand confirmEdit { get; set; }

        public RelayCommand cancelEdit { get; set; }

        private Patient patient;
        private NavigationService navService;
        private PatientService patientService;
        private EditProfile editProfile;

        private void Execute_ConfirmEdit(object obj) {

            patientService.Edit(patient.Jmbg, patient.Username, patient.Password, patient.DateOfBirth, PatientPhone, PatientAddress, PatientEmail, patient.PatientType);
            Patient p = patientService.GetOneByUsername(patient.Username);
            editProfile.NavigationService.Navigate(new ProfileView(p));

        }
        private void Execute_CancelEdit(object obj) {

            editProfile.NavigationService.Navigate(new ProfileView(patient));
        }
        private void Execute_EditPassword(object obj) {
            this.navService.Navigate(new EditPassword(patient, editProfile));
        }
        private bool CanExecute_Command(object obj) {
            return true;
        }
        public EditProfileViewModel(Patient patient, NavigationService navService, EditProfile editProfile) {
            this.patient = patient;
            this.navService = navService;
            this.editProfile = editProfile;
            patientService = new PatientService();
            if (patient.Gender == Gender.Female)
            {
                PatientGender = "Zenski";
            }
            else
            {
                PatientGender = "Muski";
            }
            String[] dob = patient.DateOfBirth.ToString().Split(" ");

            PatientDateOfBirth= dob[0];
            PatientJmbg = patient.Jmbg;
            PatientAddress = patient.Address;
            PatientEmail = patient.Email;
            PatientPhone = patient.Phone;
            confirmEdit = new RelayCommand(Execute_ConfirmEdit,CanExecute_Command);
            cancelEdit = new RelayCommand(Execute_CancelEdit,CanExecute_Command);
            editPassword = new RelayCommand(Execute_EditPassword,CanExecute_Command);

        }
    }
}
