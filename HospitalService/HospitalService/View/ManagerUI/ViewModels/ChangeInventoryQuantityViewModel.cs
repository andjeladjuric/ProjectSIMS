using HospitalService.Repositories;
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
    public class ChangeInventoryQuantityViewModel : ViewModel
    {
        #region Fields
        private Inventory selectedItem;
        private Room room;
        private string quantity;
        #endregion

        #region Properties
        public ObservableCollection<Inventory> Inventory { get; set; }
        public ObservableCollection<Inventory> DynamicInventory { get; set; }
        public Frame Frame { get; set; }
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

        public Room Room
        {
            get { return room; }
            set
            {
                room = value;
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
        #endregion

        #region Commands
        public MyICommand ConfirmCommand { get; set; }
        public MyICommand CancelCommand { get; set; }
        #endregion

        #region Actions
        private void OnConfirm()
        {
            int enteredQuantity = Int32.Parse(Quantity);
            InventoryQuantityService service = new InventoryQuantityService();
            service.RemoveUsedItems(enteredQuantity, SelectedItem, Room);
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
        private void LoadInventory()
        {
            DynamicInventory = new ObservableCollection<Inventory>();
            foreach (Inventory i in Inventory)
            {
                if (i.EquipmentType == Equipment.Dynamic)
                    DynamicInventory.Add(i);
            }
        }
        #endregion

        #region Constructors
        public ChangeInventoryQuantityViewModel(Frame currentFrame, Room selectedRoom, ObservableCollection<Inventory> inventories)
        {
            this.Frame = currentFrame;
            this.Room = selectedRoom;
            this.Inventory = inventories;
            LoadInventory();
            ConfirmCommand = new MyICommand(OnConfirm, CanExecute);
            CancelCommand = new MyICommand(OnCancel, CanNavigate);
        }
        #endregion
    }
}
