using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;
using Newtonsoft.Json;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for EditPassword.xaml
    /// </summary>
    public partial class EditPassword : Page
    {
        public EditProfile editProfile { get; set; }
        public Patient patient { get; set; }
        public EditPassword(Patient p, EditProfile ep)
        {
            InitializeComponent();
            this.DataContext = this;
            editProfile = ep;
            patient = p;
            oldPasswordPb.Password = patient.Password;
        }

        private void confirmClick(object sender, RoutedEventArgs e)
        {
            String newPassword = newPasswordPB.Password;
            String confirmPassword = confirmPb.Password;
            if (!newPassword.Equals(confirmPassword))
            {
                MessageBox.Show("Sifre se ne poklapaju");
            }
            else {
                PatientStorage ps = new PatientStorage();
                ps.Edit(patient.Jmbg, patient.Username, newPassword, patient.DateOfBirth, patient.Phone, patient.Address, patient.Email, patient.PatientType);
                editPassword(newPassword);
                editProfile.NavigationService.Navigate(new EditProfile(patient));

            }
        }

        private void editPassword(String password) {

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

        private void cancelClick(object sender, RoutedEventArgs e)
        {
            editProfile.NavigationService.Navigate(new EditProfile(patient));
        }
    }
}
