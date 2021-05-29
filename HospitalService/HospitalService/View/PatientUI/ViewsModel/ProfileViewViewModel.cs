using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
    public class ProfileViewViewModel:ViewModelPatientClass
    {
        public String PatientGender { get; set; }

        public String DateOfBirth { get; set; }
        public String Jmbg { get; set; }

        public String Address { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Patient { get; set; }
        public ProfileViewViewModel(Patient patient) {


            if (patient.Gender == Gender.Female)
            {
                PatientGender = "Zenski";
            }
            else
            {
                PatientGender = "Muski";
            }
            String[] dob = patient.DateOfBirth.ToString().Split(" ");

            DateOfBirth = dob[0];
            Jmbg = patient.Jmbg;
            Address = patient.Address;
            Email = patient.Email;
            Phone = patient.Phone;
            Patient = patient.Name + " " + patient.Surname;
        }
    }
}
