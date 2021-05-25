using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class RoomRenovationViewModel : ViewModel
    {
        #region Fields
        private Room selected;
        public Room SelectedRoom
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged();
            }
        }

        private DateTime start;
        public DateTime Start
        {
            get { return start; }
            set
            {
                start = value;
                OnPropertyChanged();
            }
        }

        private DateTime end;
        public DateTime End
        {
            get { return end; }
            set
            {
                end = value;
                OnPropertyChanged();
            }
        }

        private string newId;
        public string NewID
        {
            get { return newId; }
            set
            {
                newId = value;
                OnPropertyChanged();
            }
        }

        private string newName;
        public string NewName
        {
            get { return newName; }
            set
            {
                newName = value;
                OnPropertyChanged();
            }
        }

        private string newSize;
        public string NewSize
        {
            get { return newSize; }
            set
            {
                newSize = value;
                OnPropertyChanged();
            }
        }
        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged();
            }
        }

        private string secondRoom;
        public string SecondRoom
        {
            get { return secondRoom; }
            set
            {
                secondRoom = value;
                OnPropertyChanged();
            }
        }

        private RoomType newType;
        public RoomType NewType
        {
            get { return newType; }
            set
            {
                newType = value;
                OnPropertyChanged();
            }
        }
        public List<string> Rooms { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }
        public Frame Frame { get; set; }
        #endregion

        #region Commands
        public MyICommand ConfirmCommand { get; set; }
        public MyICommand CancelCommand { get; set; }
        #endregion

        #region Action
        private void OnConfirm()
        {
            RoomRenovationService renovationService = new RoomRenovationService();
            MessageBox.Show(Start.ToString());
            MessageBox.Show(End.ToString());
            string[] rooms = SecondRoom.ToString().Split("/");
            string selectedId = rooms[0];

            if (CheckDateEntry(Start, End) && renovationService.CheckExistingRenovations(SelectedRoom.Id, Start, End) &&
                renovationService.CheckAppointmentsForDate(Start, End, SelectedRoom.Id))
            {
                if (IsChecked && renovationService.CheckAppointmentsForDate(Start, End, selectedId))
                    renovationService.Save(new Renovation(SelectedRoom.Id, Start, End, RenovationType.Merge, selectedId, NewID, NewType, NewName));
                else
                    renovationService.Save(new Renovation(SelectedRoom.Id, Start, End, RenovationType.Split, NewID, newType, NewName, Double.Parse(NewSize)));
                
                renovationService.SerializeRenovations();
            }

            renovationService.CheckRenovationRequests();
            this.Frame.NavigationService.Navigate(new RoomsView());
        }

        private void OnCancel()
        {
            this.Frame.NavigationService.Navigate(new RoomsView());
        }

        private bool CanExecute()
        {
            return true;
        }
        #endregion

        #region Other Functions
        private bool CheckDateEntry(DateTime startDate, DateTime endDate)
        {
            if (DateTime.Compare(startDate.Date, endDate.Date) > 0)
            {
                MessageBox.Show("Neispravan unos datuma!");
                return false;
            }

            return true;
        }

        private void LoadRooms()
        {
            String source = "";
            RoomService roomService = new RoomService();
            Rooms = new List<string>();
            foreach (Room soba in roomService.GetAll())
            {
                if (soba.Id != SelectedRoom.Id)
                {
                    source = soba.Id + "/" + soba.Name;
                    Rooms.Add(source);
                }
            }
        }

        private void LoadAppointments()
        {
            Appointments = new ObservableCollection<Appointment>();
            AppointmentStorage storage = new AppointmentStorage();

            foreach (Appointment a in storage.GetAll())
            {
                if (a.StartTime >= DateTime.Now && a.room.Id.Equals(SelectedRoom.Id))
                {
                    Appointments.Add(a);
                }
            }
        }
        #endregion

        #region Constructors
        public RoomRenovationViewModel(Frame currentFrame, Room room)
        {
            this.Frame = currentFrame;
            this.SelectedRoom = room;
            this.Start = DateTime.Now;
            this.End = DateTime.Now;
            this.IsChecked = false;
            LoadAppointments();
            LoadRooms();
            ConfirmCommand = new MyICommand(OnConfirm, CanExecute);
            CancelCommand = new MyICommand(OnCancel, CanExecute);

        }
        #endregion
    }
}
