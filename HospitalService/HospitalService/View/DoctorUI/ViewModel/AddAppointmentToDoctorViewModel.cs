using HospitalService.Service;
using HospitalService.Storage;
using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Views;
using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class AddAppointmentToDoctorViewModel : ViewModelClass
    {
        private string nextId;
        private bool isEnabled;
        private string patientsName;
        private Appointment newAppointment;
        private ObservableCollection<String> appointmentsType;
        private ObservableCollection<Room> rooms;
        private DateTime date;
        private DateTime startTime;
        private DateTime endTime;
        private AppointmentType appointmentType;
        private Room room;
        private Patient patient;
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand FindCommand { get; set; }
        public RelayCommand GetRoomsCommand { get; set; }

        public AddAppointmentToDoctorView thisWindow { get; set; }
        public DoctorWindowViewModel DoctorWindow { get; set; }
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

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged();
            }
        }


        public string PatientsName
        {
            get { return patientsName; }
            set
            {
                patientsName = value;
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

        public Frame Frame { get; set; }

        public AddAppointmentToDoctorViewModel(AddAppointmentToDoctorView window, DoctorWindowViewModel doctorWindow, Frame currentFrame)
        {
            this.Frame = currentFrame;
            IsEnabled = false;
            DoctorWindow = doctorWindow;
            thisWindow = window;
            Date = DateTime.Today.Date;
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            AddCommand = new RelayCommand(Executed_AddCommand,
               CanExecute_AddCommand);
            FindCommand = new RelayCommand(Executed_FindCommand, CanExecute_CancelCommand);
            GetRoomsCommand = new RelayCommand(Executed_GetRoomsCommand, CanExecute_CancelCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand,
               CanExecute_CancelCommand);
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);

            NextId = new AppointmentsService().GetNextId();
            AppointmentsType = new ObservableCollection<string>();
            Enum.GetNames(typeof(AppointmentType)).ToList().ForEach(AppointmentsType.Add);
            newAppointment = new Appointment();
            Rooms = new ObservableCollection<Room>();
            new RoomFileStorage().GetAll().ForEach(Rooms.Add);
        }
        public void Executed_AddCommand(object obj)
        {
            String start = Date.ToString("MM/dd/yyyy") + " " + StartTime.ToString("HH: mm");
            String end = Date.ToString("MM/dd/yyyy") + " " + EndTime.ToString("HH: mm");
            newAppointment.StartTime = Convert.ToDateTime(start);
            newAppointment.EndTime = Convert.ToDateTime(end);
            newAppointment.Id = NextId;
            newAppointment.Type = AppointmentType;
            newAppointment.room = Room;
            newAppointment.patient = Patient;
            newAppointment.doctor = DoctorWindow.Doctor;
            new AppointmentsService().AddAppointment(newAppointment);
            DoctorWindow.Refresh();
            thisWindow.Close();
        }

        public bool CanExecute_AddCommand(object obj)
        {
            String start = Date.ToString("MM/dd/yyyy") + " " + StartTime.ToString("HH: mm");
            String end = Date.ToString("MM/dd/yyyy") + " " + EndTime.ToString("HH: mm");
            newAppointment.StartTime = Convert.ToDateTime(start);
            newAppointment.EndTime = Convert.ToDateTime(end);
            Doctor doctor = DoctorWindow.Doctor;

            if (StartTime == null || EndTime == null || AppointmentsType == null || Room == null || Patient == null)
            {
                MessageBox.Show("Sva polja  moraju biti popunjena!");
                return false;
            } else if (new AppointmentsService().IsTaken(newAppointment.StartTime, newAppointment.EndTime, doctor))
            {
                MessageBox.Show("Termin je vec zauzet"); // Ispitati da li je soba slobodna i da li je pacijent slobodan?
                return false;
            }
            return true;
        }

        public void Executed_CancelCommand(object obj)
        {
            thisWindow.Close();
        }

        public void Executed_GetRoomsCommand(object obj)
        {
             RoomType roomType;
             if (AppointmentType == AppointmentType.Pregled)
                 roomType = RoomType.ExaminationRoom;
             else
                 roomType = RoomType.OperatingRoom;

             Rooms = new ObservableCollection<Room>();
             new RoomFileStorage().getByType(roomType).ForEach(Rooms.Add); // prebaciti u servis
            IsEnabled = true;
        }

        public void Executed_FindCommand(object obj)
        {
            this.Frame.NavigationService.Navigate(new FindPatientView(this, Frame));
        }

        public bool CanExecute_CancelCommand(object obj)
        {
            return true;
        }

        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }

        public void SelectPatient(Patient patient)
        {
            this.Patient = patient;
            PatientsName = Patient.Name + " " + Patient.Surname;
        }

    }
}
