using HospitalService.Service;
using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Views;
using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

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
            RoomType roomType = new RoomService().GetRoomType(AppointmentForEditing.Type);
            Rooms = new ObservableCollection<Room>();
            new RoomService().GetByType(roomType).ForEach(Rooms.Add);
           // new RoomService().GetAll().ForEach(Rooms.Add);
            Room = selectedAppointment.room;
        }

        public void Executed_EditCommand(object obj)
        {
            DateService dateService = new DateService();
            DateTime Start = dateService.CreateDate(Date, StartTime);
            DateTime End = dateService.CreateDate(Date,EndTime);
            AppointmentForEditing.room = Room;
            new AppointmentsService().Edit(AppointmentForEditing.Id, Start, End, Room); 
            ParentWindow.Refresh();
            ThisWindow.Close();
        }

        public bool CanExecute_EditCommand(object obj)
        {
            DateService dateService = new DateService();
            DateTime Start = dateService.CreateDate(Date, StartTime);
            DateTime End = dateService.CreateDate(Date, EndTime);
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
