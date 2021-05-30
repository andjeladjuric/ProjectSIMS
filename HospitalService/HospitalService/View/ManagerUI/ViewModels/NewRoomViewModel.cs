using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class NewRoomViewModel : ViewModel
    {
        #region Fields
        private CancellationTokenSource cts = new CancellationTokenSource();
        private string roomId;
        private string roomName;
        private string roomFloor;
        private string roomSize;
        private RoomType roomType;
        private Frame frame;
        #endregion

        #region Properties
        private bool isOpen;
        public bool IsPopupOpen
        {
            get { return isOpen; }
            set
            {
                isOpen = value;
                OnPropertyChanged();
            }
        }
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
        private async Task DemoIsOn(CancellationToken ct)
        {
            if (DemoOn)
            {
                RoomService rooms = new RoomService();
                MessageViewModel.Message = "Završena prva funkcionalnost \n Sledi - renoviranje prostorije";
                ct.ThrowIfCancellationRequested();

                await Task.Delay(1500, ct);
                RoomId = "403a";
                await Task.Delay(2000, ct);
                RoomName = "Operaciona sala";
                await Task.Delay(2000, ct);
                RoomFloor = "3";
                await Task.Delay(2000, ct);
                RoomSize = "35.4";
                await Task.Delay(2000, ct);
                RoomType = RoomType.OperatingRoom;
                await Task.Delay(2000, ct);

                await Task.Delay(2000, ct);
                IsPopupOpen = true;
                await Task.Delay(1500, ct);
                IsPopupOpen = false;
                this.Frame.NavigationService.Navigate(new RoomsView());
                await Task.Delay(1500, ct);
                this.Frame.NavigationService.Navigate(new RoomRenovationView(rooms.GetOne("330"), DemoOn));
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
            cts = ManagerWindowViewModel.cts;
            try
            {
               DemoIsOn(cts.Token);
            }
            catch(OperationCanceledException)
            {
                MessageBox.Show("Greška!");
            }
        }
        #endregion
    }
}
