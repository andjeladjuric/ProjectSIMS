using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class NewRoomViewModel : ViewModel
    {
        #region Fields
        private string roomId;
        private string roomName;
        private string roomFloor;
        private string roomSize;
        private RoomType roomType;
        private Frame frame;
        #endregion

        #region Properties
        private bool demoOn;
        public bool DemoOn
        {
            get { return demoOn; }
            set 
            { 
                demoOn = value;
                OnPropertyChanged();
            }
        }
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

        public string RoomFloor
        {
            get { return roomFloor; }
            set
            {
                roomFloor = value;
                OnPropertyChanged();
            }
        }

        public string RoomSize
        {
            get { return roomSize; }
            set
            {
                roomSize = value;
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
            roomService.AddRoom(new Room(RoomType, RoomId, RoomName, Double.Parse(RoomSize), Int32.Parse(RoomFloor), true));
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
        private async Task DemoIsOn()
        {
            if (DemoOn)
            {
                RoomService rooms = new RoomService();

                await Task.Delay(1500);
                RoomId = "403a";
                await Task.Delay(2000);
                RoomName = "Operaciona sala";
                await Task.Delay(2000);
                RoomFloor = "3";
                await Task.Delay(2000);
                RoomSize = "35.4";
                await Task.Delay(2000);
                RoomType = RoomType.OperatingRoom;
                await Task.Delay(2000);
                OnAdd();

                await Task.Delay(2000);
                MessageBox.Show("Počinje DEMO za sledeću funkcionalnost - Rukovanje inventarom");
                await Task.Delay(1500);
                this.Frame.NavigationService.Navigate(new ManageRoomInventoryView(rooms.GetOne("105")));
            }
        }
        #endregion

        #region Constructors
        public NewRoomViewModel(Frame frame, bool demo)
        {
            AddCommand = new MyICommand(OnAdd, CanExecute);
            CancelCommand = new MyICommand(OnCancel, CanExecute);
            this.Frame = frame;
            this.DemoOn = demo;
            DemoIsOn();
        }
        #endregion
    }
}
