using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class DoctorsViewModel : ViewModel
    {
        #region Fields and Properties
        private ObservableCollection<Doctor> doctors;
        public ObservableCollection<Doctor> Doctors
        {
            get { return doctors; }
            set
            {
                doctors = value;
                OnPropertyChanged();
            }
        }

        private Doctor selectedDoctor;
        public Doctor SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged();
            }
        }

        public Frame Frame { get; set; }
        #endregion

        #region Commands
        public MyICommand GenerateCommand { get; set; }
        #endregion

        #region Actions
        private void OnGenerate()
        {
            this.Frame.NavigationService.Navigate(new DoctorsReport(SelectedDoctor));
        }

        private bool CanExecute()
        {
            return SelectedDoctor != null;
        }
        #endregion

        #region Other Functions
        private void LoadDoctors()
        {
            Doctors = new ObservableCollection<Doctor>();
            DoctorService service = new DoctorService();

            foreach(Doctor doctor in service.GetAll())
            {
                Doctors.Add(doctor);
            }
        }
        #endregion

        #region Constructors
        public DoctorsViewModel(Frame currentFrame)
        {
            this.Frame = currentFrame;
            LoadDoctors();
            GenerateCommand = new MyICommand(OnGenerate, CanExecute);
        }
        #endregion
    }
}
