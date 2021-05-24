using HospitalService.Service;
using HospitalService.View.DoctorUI.Commands;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalService.View.DoctorUI.ViewModel
{
    class FindPatientViewModel : ViewModelClass
    {
        private ObservableCollection<Patient> patients;
        private Patient patient;

        public RelayCommand SelectCommand { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }
        public AddAppointmentToDoctorViewModel ParentWindow { get; set; }
        public Frame Frame { get; set; }


        public ObservableCollection<Patient> Patients
        {
            get { return patients; }
            set
            {
                patients = value;
                OnPropertyChanged();
            }
        }
        public Patient Patient
        {
            get { return patient; }
            set
            {
                patient = value;
                OnPropertyChanged();
            }
        }

        public FindPatientViewModel(AddAppointmentToDoctorViewModel window, Frame frame)
        {
            this.Frame = frame;
            ParentWindow = window;
            Patients = new ObservableCollection<Patient>();
            new PatientService().GetAll().ForEach(Patients.Add); 
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
            SelectCommand = new RelayCommand(Executed_SelectCommand,
              CanExecute_SelectCommand);
        }

        public bool CanExecute_SelectCommand(object obj)
        {
            if (Patient == null)
            {
                MessageBox.Show("Morate izabrati jednog pacijenta");
                return false;
            }
            else
                return true;
        }

        public void Executed_SelectCommand(object obj)
        {
            ParentWindow.SelectPatient(Patient);
            this.Frame.Content = null;
        }

        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }
    }
}
