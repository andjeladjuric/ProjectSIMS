using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class MoveInventoryViewModel : ViewModel
    {
        #region Fields
        private Inventory selectedItem;
        private string enteredTime;
        private DateTime date;
        private string quantity;
        private string selectedRoom;
        private Room room;
        #endregion

        #region Properties
        public ObservableCollection<Inventory> RoomInventory { get; set; }
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
                ConfirmCommand.RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public string EnteredTime
        {
            get { return enteredTime; }
            set
            {
                enteredTime = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }

        public string Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged();
            }
        }

        public string SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                selectedRoom = value;
                OnPropertyChanged();
            }
        }

        public List<String> Rooms { get; set; }
        #endregion

        #region Commands
        public MyICommand ConfirmCommand { get; set; }
        public MyICommand CancelCommand { get; set; }
        #endregion

        #region Actions
        private void OnConfirm()
        {
            RoomService roomService = new RoomService();
            RoomInventoryService roomInventoryService = new RoomInventoryService();
            string[] rooms = SelectedRoom.ToString().Split("/");
            string selectedId = rooms[0];

            Room sendToThisRoom = null;
            foreach (Room r in roomService.GetAll())
            {
                if (r.Id == selectedId)
                {
                    sendToThisRoom = r;
                    break;
                }
            }

            int itemId = SelectedItem.Id;
            foreach (Inventory i in RoomInventory)
            {
                if (i.Id == itemId)
                {
                    if (i.EquipmentType.Equals(Equipment.Dynamic))
                    {
                        roomInventoryService.AnalyzeRequests(new MovingRequests(DateTime.Now, Int32.Parse(Quantity), Room.Id, sendToThisRoom.Id, itemId));
                    }
                    else
                    {
                        MessageBox.Show(Date.ToString());
                        TimeSpan selectedTime = TimeSpan.ParseExact(EnteredTime, "c", null);
                        DateTime selectedDate = Convert.ToDateTime(selectedTime + " " + Date.ToString("d"));
                        MovingRequests request = new MovingRequests(selectedDate, Int32.Parse(Quantity), room.Id, sendToThisRoom.Id, itemId);
                        //roomInventoryService.StartMoving(request);
                                
                    }
                }
            }

            this.Frame.NavigationService.Navigate(new ManageRoomInventoryView(Room));
        }

        private void OnCancel()
        {
            this.Frame.NavigationService.Navigate(new ManageRoomInventoryView(Room));
        }

        private bool CanExecute()
        {
            return SelectedItem != null;
        }

        private bool CanNavigate()
        {
            return true;
        }
        #endregion

        #region Other Functions
        private void LoadRooms()
        {
            String source = "";
            RoomService roomService = new RoomService();
            Rooms = new List<string>();
            foreach (Room soba in roomService.GetAll())
            {
                if (soba.Id != room.Id)
                {
                    source = soba.Id + "/" + soba.Name;
                    Rooms.Add(source);
                }
            }
        }
        #endregion

        #region Constructors
        public MoveInventoryViewModel(Frame currentFrame, Room room, ObservableCollection<Inventory> inv)
        {
            this.Frame = currentFrame;
            this.Room = room;
            this.RoomInventory = inv;
            //this.Date = DateTime.Now;
            LoadRooms();
            ConfirmCommand = new MyICommand(OnConfirm, CanExecute);
            CancelCommand = new MyICommand(OnCancel, CanNavigate);
        }
        #endregion
    }
}
