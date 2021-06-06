using HospitalService.Service;
using HospitalService.View.ManagerUI.Validations;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class MoveInventoryViewModel : ValidationBase
    {
        #region Fields
        private CancellationTokenSource cts = new CancellationTokenSource();
        private Inventory selectedItem;
        private string enteredTime;
        private DateTime date;
        private string quantity;
        private Room selectedRoom;
        private Room room;
        private bool demoOn;
        private bool isOpen;
        #endregion

        #region Properties
        private bool warning;
        public bool Warning
        {
            get { return warning; }
            set
            {
                warning = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Inventory> RoomInventory { get; set; }
        public bool IsPopupOpen
        {
            get { return isOpen; }
            set
            {
                isOpen = value;
                OnPropertyChanged();
            }
        }

        public Frame Frame { get; set; }

        public bool DemoOn
        {
            get { return demoOn; }
            set
            {
                demoOn = value;
                OnPropertyChanged();
            }
        }
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

        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                selectedRoom = value;
                OnPropertyChanged();
            }
        }

        private int selectedType;
        public int SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;
                OnPropertyChanged();
            }
        }

        public List<String> Rooms { get; set; }
        #endregion

        #region Commands
        public MyICommand ConfirmCommand { get; set; }
        public MyICommand CancelCommand { get; set; }
        public MyICommand CancelSearch { get; set; }
        public MyICommand StopDemo { get; set; }
        #endregion

        #region Actions
        private void OnStop()
        {
            cts.Cancel();
            Warning = true;
            DemoOn = false;
            this.Frame.NavigationService.Navigate(new RoomsView());
        }
        private void OnConfirm()
        {
            RoomService roomService = new RoomService();
            RoomInventoryService roomInventoryService = new RoomInventoryService();
            InventoryService inventoryService = new InventoryService();
            this.Validate();

            Room sendToThisRoom = null;
            foreach (Room r in roomService.GetAll())
            {
                if (r.Id == SelectedRoom.Id)
                {
                    sendToThisRoom = r;
                    break;
                }
            }

            if (IsValid)
            {
                Inventory item = inventoryService.GetOne(SelectedItem.Id);
                if (item.EquipmentType.Equals(Equipment.Dynamic))
                {
                    roomInventoryService.AnalyzeRequests(new MovingRequests(DateTime.Now, Int32.Parse(Quantity), Room.Id, SelectedRoom.Id, item.Id));
                }
                else
                {
                    TimeSpan selectedTime = TimeSpan.ParseExact(EnteredTime, "c", null);
                    DateTime selectedDate = Convert.ToDateTime(selectedTime + " " + Date.ToString("d"));
                    MovingRequests request = new MovingRequests(selectedDate, Int32.Parse(Quantity), room.Id, SelectedRoom.Id, item.Id);
                    roomInventoryService.StartMoving(request);

                }

                string one = Room.Id + "/" + Room.Name;
                string two = SelectedRoom.Id + "/" + SelectedRoom.Name;
                this.Frame.NavigationService.Navigate(new TransferItemView(one, two, false));
            }
        }

        private void OnCancel()
        {
            string one = Room.Id + "/" + Room.Name;
            string two = SelectedRoom.Id + "/" + SelectedRoom.Name;
            MessageBox.Show(one + " " + two);
            this.Frame.NavigationService.Navigate(new TransferItemView(one, two, false));
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

        private async Task DemoIsOn(CancellationToken ct)
        {
            if (DemoOn)
            {
                InventoryService service = new InventoryService();
                MessageViewModel.Message = "Završena treća funkcionalnost \n Sledi - izmena inventara";
                RoomService rooms = new RoomService();
                ct.ThrowIfCancellationRequested();

                await Task.Delay(2000, ct);
                EnteredTime = "15:30";
                await Task.Delay(2000, ct);
                Date = DateTime.Today;
                await Task.Delay(2000, ct);
                Quantity = "5";

                await Task.Delay(2000, ct);
                this.Frame.NavigationService.Navigate(new InventoryView());
                IsPopupOpen = true;
                await Task.Delay(1500, ct);
                IsPopupOpen = false;
                this.Frame.NavigationService.Navigate(new EditInventoryView(service.GetOne(321), DemoOn));
            }
        }

        private bool CheckExistingMovingRequests(Room moveFrom, int quantity)
        {
            RoomInventoryService service = new RoomInventoryService();
            foreach (MovingRequests mr in service.LoadRequests())
            {
                if (mr.moveFromThisRoom.Equals(moveFrom.Id) && mr.inventoryId == SelectedItem.Id)
                {
                    RoomInventory item = service.GetRoomInventoryByIds(moveFrom.Id, mr.inventoryId);
                    int temp = item.Quantity - mr.quantity;
                    if(quantity > temp)
                        return false;
                }
            }

            return true;
        }

        private int GetAvailableNumber(Room moveFrom)
        {
            RoomInventoryService service = new RoomInventoryService();
            int temp = 0;
            foreach (MovingRequests mr in service.LoadRequests())
            {
                if (mr.moveFromThisRoom.Equals(moveFrom.Id) && mr.inventoryId == SelectedItem.Id)
                {
                    RoomInventory item = service.GetRoomInventoryByIds(moveFrom.Id, mr.inventoryId);
                    temp = item.Quantity - mr.quantity;
                    return temp;
                }
            }

            return temp;
        }

        protected override void ValidateSelf()
        {
            if (!CheckExistingMovingRequests(Room, Int32.Parse(Quantity)))
            {
                this.ValidationErrors["Quantity"] = "Samo " + GetAvailableNumber(Room) + " stavki je dostupno za transfer!" +
                    "\nUnesite manju količinu!";
            }
        }
        #endregion

        #region Constructors
        public MoveInventoryViewModel(Frame currentFrame, Room moveFrom, Room sendHere, Inventory selectedItem, bool demo)
        {
            /*view*/
            this.Frame = currentFrame;
            this.Room = moveFrom;
            this.SelectedRoom = sendHere;
            this.SelectedItem = selectedItem;
            this.Date = DateTime.Today;
            this.DemoOn = demo;

            /*commands*/
            ConfirmCommand = new MyICommand(OnConfirm, CanNavigate);
            CancelCommand = new MyICommand(OnCancel, CanNavigate);
            StopDemo = new MyICommand(OnStop, CanExecute);
            try
            {
                DemoIsOn(cts.Token);
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Greška!");
            }
        }
        #endregion
    }
}
