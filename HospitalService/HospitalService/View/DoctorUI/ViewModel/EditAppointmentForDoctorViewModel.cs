using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HospitalService.View.DoctorUI.ViewModel
{
    class EditAppointmentForDoctorViewModel:ViewModelClass
    {
        private Appointment appointmentForEditing;
        private Room room;
        private DateTime startTime;
        public ObservableCollection<Room> Rooms { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public EditAppointmentForDoctorView ThisWindow { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }

        public Appointment AppointmentForEditing
        {
            get { return appointmentForEditing; }
            set
            {
                appointmentForEditing = value;
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
        public DateTime StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                OnPropertyChanged();
            }
        }

        public EditAppointmentForDoctorViewModel(EditAppointmentForDoctorView window, Appointment selectedAppointment) {
            ThisWindow = window;
            EditCommand = new RelayCommand(Executed_EditCommand,
               CanExecute_EditCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand,
               CanExecute_CancelCommand);
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
            AppointmentForEditing = selectedAppointment;
            StartTime = selectedAppointment.StartTime;
            Room = selectedAppointment.room;
            Rooms = new ObservableCollection<Room>();
            new RoomFileStorage().GetAll().ForEach(Rooms.Add);
        }

        public void Executed_EditCommand(object obj)
        {

           // new AppointmentStorage().Save(newAppointment);
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
