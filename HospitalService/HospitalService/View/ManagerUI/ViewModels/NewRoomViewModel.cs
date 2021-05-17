using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class NewRoomViewModel : ViewModel
    {
        #region Fields
        private string roomId;
        private string roomName;
        private RoomType roomType;
        private Frame frame;
        #endregion

        #region Properties
        public string RoomId
        {
            get { return roomId; }
            set
            {
                roomId = value;
                OnPropertyChanged();
            }
        }

        public string RoomName
        {
            get { return roomName; }
            set
            {
                roomName = value;
                OnPropertyChanged();
            }
        }

        public RoomType RoomType
        {
            get { return roomType; }
            set
            {
                roomType = value;
                OnPropertyChanged();
            }
        }

        public Frame Frame
        {
            get { return frame; }
            set
            {
                frame = value;
            }
        }
        #endregion

        #region Commands
        public MyICommand AddCommand { get; set; }
        public MyICommand CancelCommand { get; set; }

        #endregion

        #region Actions
        private void OnAdd()
        {
            RoomService roomService = new RoomService();
            roomService.AddRoom(new Room(RoomType, RoomId, RoomName, true));
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

        #region Constructors
        public NewRoomViewModel(Frame frame)
        {
            AddCommand = new MyICommand(OnAdd, CanExecute);
            CancelCommand = new MyICommand(OnCancel, CanExecute);
            this.Frame = frame;
        }
        #endregion
    }
}
