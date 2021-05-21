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

            if (CheckDateEntry(Start, End) && renovationService.CheckExistingRenovations(SelectedRoom.Id, Start, End))
            {
                renovationService.Save(new Renovation(SelectedRoom.Id, Start, End));
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
            foreach (Appointment a in new AppointmentStorage().GetAll())
            {
                if (a.room.Id.Equals(SelectedRoom.Id))
                {
                    if (DateTime.Compare(startDate.Date, a.StartTime.Date) <= 0 && DateTime.Compare(endDate.Date, a.StartTime.Date) >= 0)
                    {
                        MessageBox.Show("U datom periodu postoje zakazani termini!");
                        return false;
                    }
                }
            }

            if (DateTime.Compare(startDate.Date, endDate.Date) > 0)
            {
                MessageBox.Show("Neispravan unos datuma!");
                return false;
            }

            return true;
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
            LoadAppointments();
            ConfirmCommand = new MyICommand(OnConfirm, CanExecute);
            CancelCommand = new MyICommand(OnCancel, CanExecute);

        }
        #endregion
    }
}
