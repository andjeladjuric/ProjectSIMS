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
    public class EditInventoryViewModel : ViewModel
    {
        #region Fields
        private int itemId;
        private string itemName;
        private string supplier;
        private string quantity;
        private Equipment type;
        private Inventory item;
        private ObservableCollection<Inventory> inventory;
        private Frame frame;
        #endregion

        #region Properties
        public int ItemId
        {
            get { return itemId; }
            set
            {
                itemId = value;
                OnPropertyChanged();
            }
        }

        public string ItemName
        {
            get { return itemName; }
            set
            {
                itemName = value;
                OnPropertyChanged();
            }
        }

        public string Supplier
        {
            get { return supplier; }
            set
            {
                supplier = value;
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

        public Equipment Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged();
            }
        }

        public Frame Frame { get; set; }
        public Inventory SelectedItem
        {
            get { return item; }
            set
            {
                item = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Inventory> Inventory { get; set; }
        #endregion

        #region Commands
        public MyICommand ConfirmCommand { get; set; }
        public MyICommand CancelCommand { get; set; }
        #endregion

        #region Actions
        private void OnConfirm()
        {
            InventoryService inventoryService = new InventoryService();
            inventoryService.Edit(ItemId, ItemName, Type, Int32.Parse(Quantity), Supplier);
            this.Frame.NavigationService.Navigate(new InventoryView());
        }

        private void OnCancel()
        {
            this.Frame.NavigationService.Navigate(new InventoryView());
        }

        private bool CanExecute()
        {
            return true;
        }
        #endregion

        #region Constructors
        public EditInventoryViewModel(Frame currentFrame, ObservableCollection<Inventory> table, Inventory item)
        {
            this.Frame = currentFrame;
            this.Inventory = table;
            this.SelectedItem = item;
            this.ItemId = SelectedItem.Id;
            this.ItemName = SelectedItem.Name;
            this.Supplier = SelectedItem.Supplier;
            this.Quantity = SelectedItem.Quantity.ToString();
            this.Type = SelectedItem.EquipmentType;

            ConfirmCommand = new MyICommand(OnConfirm, CanExecute);
            CancelCommand = new MyICommand(OnCancel, CanExecute);
        }
        #endregion
    }
}
