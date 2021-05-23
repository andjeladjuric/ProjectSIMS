using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class ManageRoomInventoryViewModel : ViewModel
    {
        #region Fields
        private ObservableCollection<Inventory> inventory;
        public ObservableCollection<Inventory> Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        private Room selectedRoom;
        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set 
            { 
                selectedRoom = value;
                OnPropertyChanged();
            }
        }
        public Frame Frame { get; set; }
        #endregion

        #region Commands
        public MyICommand MoveInventoryCommand { get; set; }
        public MyICommand ChangeQuantityCommand { get; set; }
        #endregion

        #region Actions 
        private void OnMoveInventory()
        {
            this.Frame.NavigationService.Navigate(new MoveInventoryView(SelectedRoom, Inventory));
        }

        private void OnChangeQuantity()
        {
            this.Frame.NavigationService.Navigate(new ChangeInventoryQuantityView(SelectedRoom, Inventory));
        }

        private bool CanExecute()
        {
            return true;
        }
        #endregion

        #region Other Functions
        private void LoadRoomInventory()
        {
            Inventory = new ObservableCollection<Inventory>();
            RoomInventoryService service = new RoomInventoryService();
            InventoryService inventoryService = new InventoryService();

            foreach (RoomInventory item in service.GetAll())
            {
                if (item.RoomId.Equals(SelectedRoom.Id))
                {
                    foreach (Inventory i in inventoryService.GetAll())
                    {
                        if (item.ItemId == i.Id)
                        {
                            Inventory.Add(new Inventory(item.ItemId, i.Name, i.EquipmentType, item.Quantity, i.Supplier));
                            break;
                        }
                    }
                }
            }
        }
        #endregion

        #region Constructors
        public ManageRoomInventoryViewModel(Frame frame, Room selected)
        {
            this.Frame = frame;
            this.SelectedRoom = selected;
            LoadRoomInventory();
            this.ChangeQuantityCommand = new MyICommand(OnChangeQuantity, CanExecute);
            this.MoveInventoryCommand = new MyICommand(OnMoveInventory, CanExecute);


        }
        #endregion
    }
}
