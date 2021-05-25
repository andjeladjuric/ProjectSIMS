using HospitalService.Repositories;
using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class RoomsViewModel : ViewModel
    {
        #region Fields
        private ObservableCollection<Room> rooms;
        public ObservableCollection<Room> Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }

        private Frame frame;
        public Frame Frame
        {
            get { return frame; }
            set { frame = value; }
        }

        private Room selectedRoom;
        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set 
            { 
                selectedRoom = value; 
                DeleteCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                OpenInventoryCommand.RaiseCanExecuteChanged();
                OpenRenovationCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Commands
        public MyICommand AddCommand { get; set; }
        public MyICommand EditCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }
        public MyICommand OpenInventoryCommand { get; set; }
        public MyICommand OpenRenovationCommand { get; set; }
        #endregion

        #region Constructors
        public RoomsViewModel(Frame currentFrame)
        {
            RoomRenovationService service = new RoomRenovationService();
            service.CheckRenovationRequests();
            LoadRooms();
            AddCommand = new MyICommand(OnAdd, CanAddRoom);
            DeleteCommand = new MyICommand(OnDelete, CanExecute);
            EditCommand = new MyICommand(OnEdit, CanExecute);
            OpenRenovationCommand = new MyICommand(OpenRenovationView, CanExecute);
            OpenInventoryCommand = new MyICommand(OpenInventoryView, CanExecute);
            this.Frame = currentFrame;

        }

        private void LoadRooms()
        {
            RoomService roomService = new RoomService();
            RoomRenovationService service = new RoomRenovationService();
            service.CheckRenovationRequests();
            Rooms = new ObservableCollection<Room>();

            foreach (Room r in roomService.GetAll())
            {
                Rooms.Add(r);
            }
        }

        #endregion

        #region Actions
        public void OnAdd()
        {
            this.Frame.NavigationService.Navigate(new NewRoomView());
        }

        public void OnDelete()
        {
            RoomService roomService = new RoomService();
            roomService.DeleteRoom(selectedRoom.Id);
            Rooms.Remove(SelectedRoom);
        }

        public void OnEdit()
        {
            this.Frame.NavigationService.Navigate(new RoomEditView(SelectedRoom, Rooms));
        }

        public void OpenInventoryView()
        {
            this.Frame.NavigationService.Navigate(new ManageRoomInventoryView(SelectedRoom));
        }

        public void OpenRenovationView()
        {
            this.Frame.NavigationService.Navigate(new RoomRenovationView(SelectedRoom));
        }

        private bool CanExecute()
        {
            return SelectedRoom != null;
        }

        private bool CanAddRoom()
        {
            return true;
        }

        #endregion

    }
}
