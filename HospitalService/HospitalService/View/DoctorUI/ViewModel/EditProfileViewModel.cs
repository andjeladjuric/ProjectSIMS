using HospitalService.Service;
using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Validation;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class EditProfileViewModel : ValidationBase
    {
        public ProfileViewModel ProfileWindow { get; set; }
        private Frame frame;
        private Doctor doctor;
        private string name;
        private string type;
        private string address;
        private string phone;
        private string email;
        public RelayCommand KeyUpCommandWithKey { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand ApplyCommand { get; set; }

        public Doctor Doctor
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged("Doctor");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }

        public Frame Frame
        {
            get { return frame; }
            set
            {
                frame = value;
                OnPropertyChanged("Frame");
            }
        }

        public EditProfileViewModel(Doctor doctor, Frame frame, ProfileViewModel profileView)
        {
            this.ProfileWindow = profileView;
            this.Frame = frame;
            this.Doctor = doctor;
            this.Name = "dr " + doctor.Name + " " + doctor.Surname;
            this.Type = doctor.DoctorType.ToString();
            this.Address = doctor.Address;
            this.Phone = doctor.Phone;
            this.Email = doctor.Email;
            CancelCommand = new RelayCommand(Executed_CancelCommand,
              CanExecute_CancelCommand);
            ApplyCommand = new RelayCommand(Executed_ApplyCommand,
              CanExecute_ApplyCommand);
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
        }

        public void Executed_CancelCommand(object obj)
        {
            this.Frame.Content = null;
        }

        public bool CanExecute_CancelCommand(object obj)
        {
            return true;
        }

        public void Executed_ApplyCommand(object obj)
        {
            this.Doctor.Address = Address;
            this.Doctor.Phone = Phone;
            this.Doctor.Email = Email;
            new DoctorService().Edit(Doctor);
            this.ProfileWindow.Doctor = Doctor;
            this.Frame.Content = null;
        }

        public bool CanExecute_ApplyCommand(object obj)
        {
            this.Validate();
            if(this.IsValid)
                return true;
            else
                return false;
        }

        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }

        protected override void ValidateSelf()
        {
            Regex regexNum = new Regex(@"0[0-9]{2}\/[0-9]{3}-[0-9]{4}$");
            Regex regexMail = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            if (string.IsNullOrWhiteSpace(Address))
                this.ValidationErrors["DoctorsAddress"] = "Obavezno polje";
            if (!regexNum.IsMatch(Phone))
                this.ValidationErrors["DoctorsPhone"] = "Obavezan format: 012/345-6789";
            if (!regexMail.IsMatch(Email))
                this.ValidationErrors["DoctorsEmail"] = "Obavezan format: example@mail.com";

        }
    }
} 