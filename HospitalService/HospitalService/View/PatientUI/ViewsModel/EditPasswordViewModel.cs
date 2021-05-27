using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using HospitalService.Service;
using HospitalService.View.PatientUI.Pages;
using Model;
using Newtonsoft.Json;

namespace HospitalService.View.PatientUI.ViewsModel
{
   public class EditPasswordViewModel:ViewModelPatientClass
    {
        public RelayCommand confirmEditPassword { get; set; }
        public RelayCommand cancelEditPassword { get; set; }
        private Patient patient;
        private PasswordBox oldPasswordPb;
        private PasswordBox newPasswordPB;
        private PasswordBox confirmPb;
        private PatientService patientService;
        private EditProfile editProfile;


        private void Execute_ConfirmEditPassword(object obj) {

            String newPassword = newPasswordPB.Password;
            String confirmPassword = confirmPb.Password;
            if (!newPassword.Equals(confirmPassword))
            {
                MessageBox.Show("Sifre se ne poklapaju");
            }
            else
            {
                
                patientService.Edit(patient.Jmbg, patient.Username, newPassword, patient.DateOfBirth, patient.Phone, patient.Address, patient.Email, patient.PatientType);
                editPassword(newPassword);
                editProfile.NavigationService.Navigate(new EditProfile(patient));

            }

        }

        private void editPassword(String password)
        {

            String FileLocation = @"..\..\..\Data\users.json";
            List<Account> users = new List<Account>();
            users = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText(FileLocation));
            Account a;
            for (int i = 0; i < users.Count; i++)
            {
                a = users[i];
                if (a.Username.Equals(patient.Username))
                {
                    a.Password = password;
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(users,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));

                    break;
                }
            }

        }

        private void Execute_CancelEditPassword(object obj) {

            editProfile.NavigationService.Navigate(new EditProfile(patient));
        }
        private bool CanExecute_Command(object obj) {
            return true;
        }
        public EditPasswordViewModel(Patient patient, PasswordBox oldPasswordPb, PasswordBox newPasswordPB, PasswordBox confirmPb, EditProfile editProfile) {

            this.patient = patient;
            this.oldPasswordPb = oldPasswordPb;
            this.newPasswordPB = newPasswordPB;
            this.confirmPb = confirmPb;
            this.editProfile = editProfile;
            oldPasswordPb.Password = patient.Password;
            patientService = new PatientService();
            confirmEditPassword = new RelayCommand(Execute_ConfirmEditPassword,CanExecute_Command);
            cancelEditPassword = new RelayCommand(Execute_CancelEditPassword,CanExecute_Command);
        
        }
    }
}
