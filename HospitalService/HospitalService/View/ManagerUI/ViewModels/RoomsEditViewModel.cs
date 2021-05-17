using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class RoomsEditViewModel : ViewModel
    {
        #region Fields
        private string roomName;
        private RoomType roomType;
        private bool isFree;
        private Room selectedRoom;
        private Frame frame;
        #endregion

        #region Properties
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

        public bool IsFree
        {
            get { return isFree; }
            set
            {
                isFree = value;
                OnPropertyChanged();
            }
        }

        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                selectedRoom = value;
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
        public MyICommand ConfirmCommand { get; set; }
        public MyICommand CancelCommand { get; set; }
        #endregion

        #region Actions
        private void OnConfirm()
        {
            RoomService roomService = new RoomService();
            roomService.Edit(SelectedRoom.Id, RoomName, RoomType, IsFree);
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
        public RoomsEditViewModel(Frame frame)
        {
            ConfirmCommand = new MyICommand(OnConfirm, CanExecute);
            CancelCommand = new MyICommand(OnConfirm, CanExecute);
        }
        #endregion
    }
}
