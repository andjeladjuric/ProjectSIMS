using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class ProfileViewModel : ViewModelClass
    {
        public ProfileView Window { get; set; }
        private Doctor doctor;
        private string name;
        private string type;
        private Frame frame;
        public RelayCommand KeyUpCommandWithKey { get; set; }
        public RelayCommand EditProfileCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }

        public Doctor Doctor
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged();
            }
        }

        public Frame Frame
        {
            get { return frame; }
            set
            {
                frame = value;
                OnPropertyChanged();
            }
        }

        public ProfileViewModel(Doctor doctor, Frame frame, ProfileView window)
        {
            this.Window = window;
            this.Frame = frame;
            this.Doctor = doctor;
            this.Name = "dr " + doctor.Name + " " + doctor.Surname;
            this.Type = doctor.DoctorType.ToString();
            EditProfileCommand = new RelayCommand(Executed_EditProfileCommand,
              CanExecute_EditProfileCommand);
            CloseCommand = new RelayCommand(Executed_CloseCommand,
             CanExecute_EditProfileCommand);
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
        }

        public void Executed_EditProfileCommand(object obj)
        {
            this.Frame.NavigationService.Navigate(new EditProfileView(Doctor, Frame));
        }

        public void Executed_CloseCommand(object obj)
        {
            this.Window.Close();
        }

        public bool CanExecute_EditProfileCommand(object obj)
        {
            return true;
        }

        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }
    }
}
