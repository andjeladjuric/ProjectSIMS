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

        private string start;
        public string Start
        {
            get { return start; }
            set
            {
                start = value;
                OnPropertyChanged();
            }
        }

        private string end;
        public string End
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
            DateTime StartDate = Convert.ToDateTime(Start + DateTime.Now.ToString("HH:mm:ss"));
            MessageBox.Show(StartDate.ToString());
            DateTime EndDate = Convert.ToDateTime(End + "23:59:59");
            MessageBox.Show(EndDate.ToString());

            if (CheckDateEntry(StartDate, EndDate) && renovationService.CheckExistingRenovations(SelectedRoom.Id, StartDate, EndDate))
            {
                renovationService.Save(new Renovation(SelectedRoom.Id, StartDate, EndDate));
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
            LoadAppointments();
            ConfirmCommand = new MyICommand(OnConfirm, CanExecute);
            CancelCommand = new MyICommand(OnCancel, CanExecute);

        }
        #endregion
    }
}
