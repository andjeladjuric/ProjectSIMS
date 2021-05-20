using HospitalService.Storage;
using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class UrgentOperationViewModel : ViewModelClass
    {
        public MedicalRecord MedicalRecord { get; set; }
        public Patient Patient { get; set; }
        public string PatientsName { get; set; }
        public ObservableCollection<string> Departments { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }
        public UrgentOperationView ThisWindow { get; set; }
        private ObservableCollection<Doctor> doctors;
        private Doctor selectedDoctor;
        private DoctorType selectedDepartment;
        private Room selectedRoom;
        private bool isEnabled;
        private DateTime selectedDate;
        private DateTime startTime;
        private DateTime endTime;

        public RelayCommand ApplyCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand GetDoctorsCommand { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }

        public ObservableCollection<Doctor> Doctors
        {
            get { return doctors; }
            set
            {
                doctors = value;
                OnPropertyChanged();
            }
        }

        public Doctor SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged();
            }
        }


        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                selectedRoom = value;
                OnPropertyChanged();
            }
        }

        public DoctorType SelectedDepartment
        {
            get { return selectedDepartment; }
            set
            {
                selectedDepartment = value;
                OnPropertyChanged();
            }
        }
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged();
            }
        }
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                OnPropertyChanged();
            }
        }

        public DateTime EndTime
        {
            get { return selectedDate; }
            set
            {
                endTime = value;
                OnPropertyChanged();
            }
        }

        public UrgentOperationViewModel(MedicalRecord medicalRecord, UrgentOperationView window)
        {
            ThisWindow = window;
            this.MedicalRecord = medicalRecord;
            this.PatientsName = medicalRecord.Patient.Name + " " + medicalRecord.Patient.Surname;
            this.Departments = new ObservableCollection<string>();
            Enum.GetNames(typeof(DoctorType)).ToList().ForEach(Departments.Add);
            this.IsEnabled = false;
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
            ApplyCommand = new RelayCommand(Executed_ApplyCommand,
              CanExecute_ApplyCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand,
             CanExecute_CancelCommand);
            GetDoctorsCommand = new RelayCommand(Executed_GetDoctorsCommand,
             CanExecute_GetDoctorsCommand);
        }

        public bool CanExecute_CancelCommand(object obj)
        {
            return true;
        }
        public bool CanExecute_ApplyCommand(object obj)
        {
            if (SelectedRoom == null || SelectedDoctor == null)
            {
                MessageBox.Show("Sva polja moraju biti popunjena.");
                return false;
            }
            else
                return true;
        }
        public bool CanExecute_GetDoctorsCommand(object obj)
        {
            return true;
        }
        public void Executed_ApplyCommand(object obj)
        {

            ThisWindow.Close();

        }

        public void Executed_CancelCommand(object obj)
        {
            ThisWindow.Close();
        }

        public void Executed_GetDoctorsCommand(object obj)
        {
            this.Doctors = new ObservableCollection<Doctor>();
            List<Doctor> doctors = new DoctorStorage().GetByDepartment(SelectedDepartment);
            doctors.ForEach(Doctors.Add);
            IsEnabled = true;
        }

        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }
    }
}
