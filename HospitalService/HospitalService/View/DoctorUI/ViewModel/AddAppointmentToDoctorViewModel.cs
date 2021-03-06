using HospitalService.Service;
using HospitalService.Storage;
using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Validation;
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
        private ScheduleService ScheduleService;
        private AppointmentValidation appointment;
        private AppointmentType appointmentType;
        private string nextId;
        private Appointment newAppointment;
        private ObservableCollection<String> appointmentsType;
        private ObservableCollection<Room> rooms;
        private DateTime date;
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



        public AppointmentValidation Appointment
        {
            get { return appointment; }
            set
            {
                appointment = value;
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

        public AppointmentType AppointmentType
        {
            get { return appointmentType; }
            set
            {
                appointmentType = value;
                OnPropertyChanged("AppointmentType");
            }
        }

        public Frame Frame { get; set; }

        public AddAppointmentToDoctorViewModel(AddAppointmentToDoctorView window, DoctorWindowViewModel doctorWindow, Frame currentFrame)
        {
            ScheduleService = new ScheduleService();
            Appointment = new AppointmentValidation();
            this.Frame = currentFrame;
            this.AppointmentType = AppointmentType.Pregled;
            DoctorWindow = doctorWindow;
            thisWindow = window;
            Date = DateTime.Today.Date;
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
            new RoomService().GetByType(RoomType.ExaminationRoom).ForEach(Rooms.Add);
        }
        public void Executed_AddCommand(object obj)
        {
            DateService dateService = new DateService();
            newAppointment.StartTime = dateService.CreateDateString(Date,Appointment.StartTime);
            newAppointment.EndTime = dateService.CreateDateString(Date, Appointment.EndTime);
            newAppointment.Id = NextId;
            newAppointment.Type = AppointmentType;
            newAppointment.room = Appointment.Room;
            newAppointment.patient = Appointment.Patient;
            newAppointment.doctor = DoctorWindow.Doctor;
            new AppointmentsService().AddAppointment(newAppointment);
            DoctorWindow.Date = Date;
            DoctorWindow.Refresh();
            thisWindow.Close();
        }

        public bool CanExecute_AddCommand(object obj)
        {
            DateService dateService = new DateService();
 
            Doctor doctor = DoctorWindow.Doctor;
            Appointment.Validate();
            if (Appointment.IsValid)
            {
                newAppointment.StartTime = dateService.CreateDateString(Date, Appointment.StartTime);
                newAppointment.EndTime = dateService.CreateDateString(Date, Appointment.EndTime);
                if (DateTime.Compare(newAppointment.StartTime, DateTime.Now) <= 0)
                {
                    MessageBox.Show("Odabrano vrijeme je prošlo.");
                    return false;
                }

                if (ScheduleService.IsDoctorTaken(newAppointment.StartTime, newAppointment.EndTime, doctor))
                {
                    MessageBox.Show("Imate termin u izabranim periodu.");
                    return false;
                }
                if (ScheduleService.IsRoomTaken(newAppointment.StartTime, newAppointment.EndTime, Appointment.Room))
                {
                     MessageBox.Show("Odabrana soba je zauzeta.");
                    return false;
                 
                }
                if (ScheduleService.IsPatientTaken(Appointment.Patient, newAppointment))
                {
                    MessageBox.Show("Pacijent je zauzet u odabranom terminu.");
                    return false;

                }

                if (ScheduleService.IsRoomRenovating(newAppointment.StartTime, newAppointment.EndTime, Appointment.Room))
                {
                    MessageBox.Show("Odabrana soba se trenutno renovira.");
                    return false;

                }
                return true;
            }
            else
                return false;
        }

        public void Executed_CancelCommand(object obj)
        {
            thisWindow.Close();
        }

        public void Executed_GetRoomsCommand(object obj)
        {
            RoomType roomType = new RoomService().GetRoomType(AppointmentType);
            Rooms = new ObservableCollection<Room>();
            new RoomService().GetByType(roomType).ForEach(Rooms.Add);
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
            this.Appointment.Patient = patient;
            Appointment.PatientsName = Appointment.Patient.Name + " " + Appointment.Patient.Surname;
        }

    }
}
