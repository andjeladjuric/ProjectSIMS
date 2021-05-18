using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Views;
using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HospitalService.View.DoctorUI.ViewModel
{
    class EditAppointmentForDoctorViewModel:ViewModelClass
    {
        public Appointment AppointmentForEditing { get; set; }
        public DoctorWindowViewModel ParentWindow { get; set; }
        private DateTime date;
        private DateTime startTime;
        private DateTime endTime;
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

        public ObservableCollection<Room>  Rooms
        {
            get { return rooms; }
            set
            {
                rooms = value;
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

        public Room Room
        {
            get { return room; }
            set
            {
                room = value;
                OnPropertyChanged();
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
            StartTime = selectedAppointment.StartTime;
            EndTime = selectedAppointment.EndTime;
            Room = selectedAppointment.room;
            Rooms = new ObservableCollection<Room>();
            new RoomFileStorage().GetAll().ForEach(Rooms.Add);
        }

        public void Executed_EditCommand(object obj)
        {
            String start = Date.ToString("MM/dd/yyyy") + " " + StartTime.ToString("HH: mm");
            String end = Date.ToString("MM/dd/yyyy") + " " + EndTime.ToString("HH: mm");
            AppointmentForEditing.StartTime = Convert.ToDateTime(start);
            AppointmentForEditing.EndTime = Convert.ToDateTime(end);
            AppointmentForEditing.room = Room;
            new AppointmentStorage().Edit(AppointmentForEditing.Id, Convert.ToDateTime(start), Convert.ToDateTime(end), Room);
            ParentWindow.Refresh();
            ThisWindow.Close();
        }

        public bool CanExecute_EditCommand(object obj)
        {
            return true;
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

    }
}
