using HospitalService.Service;
using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Validation;
using HospitalService.View.DoctorUI.Views;
using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace HospitalService.View.DoctorUI.ViewModel
{
    class EditAppointmentForDoctorViewModel:ValidationBase
    {
        public Appointment AppointmentForEditing { get; set; }
        public DoctorWindowViewModel ParentWindow { get; set; }
        private DateTime date;
        private String startTime;
        private String endTime;
        private Room room;
        private ObservableCollection<Room> rooms;
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public EditAppointmentForDoctorView ThisWindow { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }
        public String EndTime
        {
            get { return endTime; }
            set
            {
                endTime = value;
                OnPropertyChanged("EndTime");
            }
        }

        public ObservableCollection<Room>  Rooms
        {
            get { return rooms; }
            set
            {
                rooms = value;
                OnPropertyChanged("Rooms");
            }
        }

       
        public String StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                OnPropertyChanged("StartTime");
            }
        }

        public Room Room
        {
            get { return room; }
            set
            {
                room = value;
                OnPropertyChanged("Room");
            }
        }

        public EditAppointmentForDoctorViewModel(EditAppointmentForDoctorView window, Appointment selectedAppointment, DoctorWindowViewModel parent) {
            ParentWindow = parent;
            ThisWindow = window;
            EditCommand = new RelayCommand(Executed_EditCommand,
               CanExecute_EditCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand,
               CanExecute_CancelCommand);
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
            AppointmentForEditing = selectedAppointment;
            Date = selectedAppointment.StartTime.Date;
            StartTime = selectedAppointment.StartTime.ToString("HH:mm");
            EndTime = selectedAppointment.EndTime.ToString("HH:mm");
            RoomType roomType = new RoomService().GetRoomType(AppointmentForEditing.Type);
            Rooms = new ObservableCollection<Room>();
            new RoomService().GetByType(roomType).ForEach(Rooms.Add);
           // new RoomService().GetAll().ForEach(Rooms.Add);
            Room = selectedAppointment.room;
        }

        public void Executed_EditCommand(object obj)
        {
            DateService dateService = new DateService();
            DateTime Start = dateService.CreateDateString(Date, StartTime);
            DateTime End = dateService.CreateDateString(Date,EndTime);
            AppointmentForEditing.room = Room;
            new AppointmentsService().Edit(AppointmentForEditing.Id, Start, End, Room); 
            ParentWindow.Refresh();
            ThisWindow.Close();
        }

        public bool CanExecute_EditCommand(object obj)
        {
            DateService dateService = new DateService();
            this.Validate();
            if (this.IsValid)
            {
                DateTime Start = dateService.CreateDateString(Date, StartTime);
                DateTime End = dateService.CreateDateString(Date, EndTime);
                if (new AppointmentsService().IsTaken(Start, End, AppointmentForEditing.doctor))
                {
                    MessageBox.Show("Postoji termin u izabranom periodu.");
                    return false;
                }
                if (new AppointmentsService().IsRoomTaken(Start, End, Room))
                {
                    RoomType roomType = new RoomService().GetRoomType(AppointmentForEditing.Type);
                    List<Room> available = new AppointmentsService().GetAvailableRooms(AppointmentForEditing.StartTime, AppointmentForEditing.EndTime, roomType);
                    try
                    {
                        if (available[0] != null)
                        {
                            MessageBox.Show("Odabrana soba je zauzeta. Slobodna je soba: " + available[0].Id);
                            return false;
                        }
                    }
                    catch
                    {

                        MessageBox.Show("Nema slobodnih soba za odabrano vrijeme.");
                        return false;

                    }
                }
                return true;
            }
            return false;
        }

        public void Executed_CancelCommand(object obj)
        {
            ThisWindow.Close();
        }

        public bool CanExecute_CancelCommand(object obj)
        {
            return true;
        }

        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }

        protected override void ValidateSelf()
        {
            Regex regexTime = new Regex(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$");
            if (string.IsNullOrWhiteSpace(StartTime))
            {
                this.ValidationErrors["StartT"] = "Obavezan format: 23:59";
            }
            else if (!regexTime.IsMatch(StartTime))
            {
                this.ValidationErrors["StartT"] = "Obavezan format: 23:59";
            }

            if (string.IsNullOrWhiteSpace(EndTime))
            {
                this.ValidationErrors["EndT"] = "Obavezan format: 23:59";
            }
            else if (!regexTime.IsMatch(EndTime))
            {
                this.ValidationErrors["EndT"] = "Obavezan format: 23:59";
            }
        }
    }
}
