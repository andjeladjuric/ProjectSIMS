using System;
using System.Collections.Generic;
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

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for EditProfile.xaml
    /// </summary>
    public partial class EditProfile : Page
    {
        public Patient patient { get; set; }
        public EditProfile(Patient p)
        {
            InitializeComponent();
            this.DataContext = this;
            patient = p;
            if (patient.Gender == Gender.Female)
            {
                genderLabel.Content = "Zenski";
            }
            else
            {
                genderLabel.Content = "Muski";
            }
            String[] dob = patient.DateOfBirth.ToString().Split(" ");

            dobLabel.Content = dob[0];
            jmbgLabel.Content = patient.Jmbg;
            addressTb.Text = patient.Address;
            emailTb.Text = patient.Email;
            phoneTb.Text = patient.Phone;
        }

        private void confirmClick(object sender, RoutedEventArgs e)
        {
            PatientStorage ps = new PatientStorage();
            String newAddress = addressTb.Text;
            String newEmail = emailTb.Text;
            String newPhone = phoneTb.Text;
            ps.Edit(patient.Jmbg, patient.Username, patient.Password, patient.DateOfBirth, newPhone, newAddress, newEmail, patient.PatientType);
            Patient p = ps.GetOneByUsername(patient.Username);
            this.NavigationService.Navigate(new ProfileView(p));
        }

        private void cancelClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ProfileView(patient));
        }

        private void editPasswordClick(object sender, RoutedEventArgs e)
        {
            PasswordEdit.Content = new EditPassword(patient,this);
        }
    }
}
