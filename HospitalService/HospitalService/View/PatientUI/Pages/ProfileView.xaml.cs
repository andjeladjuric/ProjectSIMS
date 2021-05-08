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
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : Page
    {
        public ProfileView(Patient patient)
        {
            InitializeComponent();
            this.DataContext = this;
            if (patient.Gender == Gender.Female)
            {
                genderLabel.Content = "Zenski";
            }
            else {
                genderLabel.Content = "Muski";
            }
            String [] dob= patient.DateOfBirth.ToString().Split(" ");
            
            dobLabel.Content = dob[0];
            jmbgLabel.Content = patient.Jmbg;
            addressLabel.Content = patient.Address;
            emailLabel.Content = patient.Email;
            phoneLabel.Content = patient.Phone;
            patientLabel.Text = patient.Name + " " + patient.Surname;
        }
    }
}
