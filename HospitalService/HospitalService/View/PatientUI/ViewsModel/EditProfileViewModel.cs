using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Navigation;
using HospitalService.Service;
using HospitalService.View.PatientUI.Pages;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
   public class EditProfileViewModel: ValidationBase
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
            this.Validate();
            if (IsValid)
            {

                patientService.Edit(patient.Jmbg, patient.Username, patient.Password, patient.DateOfBirth, PatientPhone, PatientAddress, PatientEmail, patient.PatientType);
                patientService = new PatientService();
                Patient p = patientService.GetOneByUsername(patient.Username);
                editProfile.NavigationService.Navigate(new ProfileView(p));
            }

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

        protected override void ValidateSelf()
            
        {
            Regex checkAddress = new Regex(@"[A-Za-z]+[\s][1-9]*[,][\s]?[A-Za-z]+");
            Regex checkEmail = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            Regex checkPhone = new Regex(@"[0-9]+");

            if (string.IsNullOrWhiteSpace(this.PatientAddress))
            {
                this.ValidationErrors["Address"] = "Adresa je obavezna.";
            } else if (!checkAddress.IsMatch(this.PatientAddress)) {
                this.ValidationErrors["Address"] = "Nije dobar format. Format je ULICA BROJ, GRAD";

            }
            if (string.IsNullOrWhiteSpace(this.PatientEmail)) {
                this.ValidationErrors["Email"] = "E-mail je obavezan.";
            }
            else if (!checkEmail.IsMatch(this.PatientEmail))
            {
                this.ValidationErrors["Email"] = "Nije dobar format.";

            }
            if (string.IsNullOrWhiteSpace(this.PatientPhone)) {

                this.ValidationErrors["Phone"] = "Broj telefona je obavezan.";

            } else if (!checkPhone.IsMatch(this.PatientPhone)) {
                this.ValidationErrors["Phone"] = "Nije dobar format.";
            }
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
