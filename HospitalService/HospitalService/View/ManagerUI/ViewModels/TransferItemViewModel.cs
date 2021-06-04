using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class TransferItemViewModel : ViewModel
    {
        #region Fields
        private RoomInventoryService roomInventoryService;
        private Inventory selectedItem;
        private string selectedFirstRoom;
        private string selectedSecondRoom;
        private Room room;
        private int index;
        #endregion

        #region Properties
        public int SelectedIndex
        {
            get { return index; }
            set
            {
                index = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> rooms;
        public ObservableCollection<string> Rooms
        {
            get { return rooms; }
            set
            {
                rooms = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> otherRooms;
        public ObservableCollection<string> OtherRooms
        {
            get { return otherRooms; }
            set
            {
                otherRooms = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Inventory> inventory;
        public ObservableCollection<Inventory> RoomInventory
        {
            get { return inventory; }
            set
            {
                inventory = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Inventory> secondInventory;
        public ObservableCollection<Inventory> SecondRoomInventory
        {
            get { return secondInventory; }
            set
            {
                secondInventory = value;
                OnPropertyChanged();
            }
        }

        public Frame Frame { get; set; }
        public Room Room
        {
            get { return room; }
            set
            {
                room = value;
                OnPropertyChanged();
            }
        }
        public Inventory SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                SendRequest.RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }
        public string SelectedFirstRoom
        {
            get { return selectedFirstRoom; }
            set
            {
                selectedFirstRoom = value;
                OnPropertyChanged();
            }
        }

        public string SelectedSecondRoom
        {
            get { return selectedSecondRoom; }
            set
            {
                selectedSecondRoom = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public MyICommand SendRequest { get; set; }
        public MyICommand ChangeInventory { get; set; }
        public MyICommand ChangeSecondInventory { get; set; }
        #endregion

        #region Actions
        private void OnSendRequest()
        {
            RoomService roomService = new RoomService();
            string[] rooms = SelectedFirstRoom.ToString().Split("/");
            string selectedId = rooms[0];
            Room moveFrom = roomService.GetOne(selectedId);

            string[] rooms2 = SelectedSecondRoom.ToString().Split("/");
            string selectedId2 = rooms2[0];
            Room sendHere = roomService.GetOne(selectedId2);

            this.Frame.NavigationService.Navigate(new MoveInventoryView(moveFrom, sendHere, SelectedItem, false));
        }

        private void OnChangeFirst()
        {
            String source = "";
            RoomService roomService = new RoomService();
            OtherRooms = new ObservableCollection<string>();
            foreach (Room soba in roomService.GetAll())
            {
                source = soba.Id + "/" + soba.Name;
                if (!SelectedFirstRoom.Equals(source))
                    OtherRooms.Add(source);
            }

            SelectedIndex = -1;
            LoadInventory();
        }

        private void OnChangeSecond()
        {
            LoadSecondInventory();
        }

        private bool CanExecute()
        {
            if (SelectedItem != null)
                return true;
            return false;
        }

        private bool CanExecuteSelectionChanged()
        {
            return true;
        }
        #endregion

        #region Other Functions
        private void LoadRooms()
        {
            String source = "";
            RoomService roomService = new RoomService();
            Rooms = new ObservableCollection<string>();
            foreach (Room soba in roomService.GetAll())
            {
                source = soba.Id + "/" + soba.Name;
                Rooms.Add(source);
            }
        }

        private void LoadInventory()
        {
            RoomService roomService = new RoomService();
            RoomInventory = new ObservableCollection<Inventory>();

            string[] rooms = SelectedFirstRoom.ToString().Split("/");
            string selectedId = rooms[0];
            Room room = roomService.GetOne(selectedId);

            RoomInventory = roomInventoryService.LoadRoomInventory(room);
        }

        private void LoadSecondInventory()
        {
            RoomService roomService = new RoomService();
            SecondRoomInventory = new ObservableCollection<Inventory>();

            if (SelectedIndex != -1)
            {
                string[] rooms = SelectedSecondRoom.ToString().Split("/");
                string selectedId = rooms[0];
                Room room = roomService.GetOne(selectedId);

                SecondRoomInventory = roomInventoryService.LoadRoomInventory(room);
            }
        }

        #endregion

        #region Constructors
        public TransferItemViewModel(Frame currentFrame, string SelectedFirst, string SelectedSecond)
        {
            /*check requests*/
            roomInventoryService = new RoomInventoryService();
            roomInventoryService.CheckRequests();

            /*view*/
            this.Frame = currentFrame;
            this.SelectedFirstRoom = SelectedFirst;
            this.SelectedSecondRoom = SelectedSecond;
            if (SelectedFirst != null && SelectedSecond != null && SelectedFirst != "" && SelectedSecond != "")
            {
                LoadInventory();
                LoadSecondInventory();
            }

            LoadRooms();

            /*commands*/
            SendRequest = new MyICommand(OnSendRequest, CanExecute);
            ChangeInventory = new MyICommand(OnChangeFirst, CanExecuteSelectionChanged);
            ChangeSecondInventory = new MyICommand(OnChangeSecond, CanExecuteSelectionChanged);

        }
        #endregion
    }
}
