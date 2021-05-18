using HospitalService.Service;
using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class AddAppointmentToDoctorViewModel : ViewModelClass
    {
        private string nextId;
        private ObservableCollection<String> appointmentsType;
        private ObservableCollection<Patient> patients;
        private ObservableCollection<Room> rooms;
        private DateTime date;
        private DateTime startTime;
        private DateTime endTime;
        private AppointmentType appointmentType;
        private Room room;
        private Patient patient;
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public AddAppointmentToDoctorView thisWindow { get; set; }

        public RelayCommand KeyUpCommandWithKey { get; set; }

        public string NextId
        {
            get { return nextId; }
            set
            {
                nextId = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<String> AppointmentsType
        {
            get { return appointmentsType; }
            set
            {
                appointmentsType = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Patient> Patients
        {
            get { return patients; }
            set
            {
                patients = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Room> Rooms
        {
            get { return rooms; }
            set
            {
                rooms = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
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
            get { return endTime; }
            set
            {
                endTime = value;
                OnPropertyChanged();
            }
        }

        public AppointmentType AppointmentType
        {
            get { return appointmentType; }
            set
            {
                appointmentType = value;
                OnPropertyChanged();
            }
        }

        public Room Room
        {
            get { return room; }
            set
            {
                room = value;
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

        public AddAppointmentToDoctorViewModel(AddAppointmentToDoctorView window)
        {
            thisWindow = window;
            AddCommand = new RelayCommand(Executed_AddCommand,
               CanExecute_AddCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand,
               CanExecute_CancelCommand);
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
            AppointmentsService appointmentService = new AppointmentsService();
            NextId = appointmentService.GetNextId();
            AppointmentsType = new ObservableCollection<string>();
            Enum.GetNames(typeof(AppointmentType)).ToList().ForEach(AppointmentsType.Add);
            Patients = new ObservableCollection<Patient>();
            Rooms = new ObservableCollection<Room>();
            new PatientStorage().GetAll().ForEach(Patients.Add); // servis
            new RoomFileStorage().GetAll().ForEach(Rooms.Add); 
        }
        public void Executed_AddCommand(object obj)
        {
        }

        public bool CanExecute_AddCommand(object obj)
        {
            return true;
        }

        public void Executed_CancelCommand(object obj)
        {
            thisWindow.Close();
        }

        public bool CanExecute_CancelCommand(object obj)
        {
            return true;
        }

        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }

    }
}
