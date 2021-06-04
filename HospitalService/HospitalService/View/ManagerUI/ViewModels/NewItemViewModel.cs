using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class NewItemViewModel : ViewModel
    {
        #region Fields
        private int itemId;
        private string itemName;
        private string supplier;
        private Equipment type;
        private string quantity;
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

        public Equipment Type
        {
            get { return type; }
            set
            {
                type = value;
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

        public Frame Frame { get; set; }
        #endregion

        #region Commands
        public MyICommand ConfirmCommand { get; set; }
        public MyICommand CancelCommand { get; set; }
        #endregion

        #region Actions
        private void OnConfirm()
        {
            InventoryService inventoryService = new InventoryService();
            inventoryService.AddInventoryItem(new Inventory(ItemId, ItemName, Type, Int32.Parse(Quantity), Supplier));
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

        private int GenerateId()
        {
            Random rand = new Random();
            InventoryService service = new InventoryService();
            List<Int32> ids = service.GetAllIds();

            Int32 curValue = rand.Next(1, 100000);
            while (ids.Exists(value => value == curValue))
            {
                curValue = rand.Next(1, 100000);
            }

            return curValue;
        }

        #region Constructors
        public NewItemViewModel(Frame frame)
        {
            this.Frame = frame;
            this.ItemId = GenerateId();
            ConfirmCommand = new MyICommand(OnConfirm, CanExecute);
            CancelCommand = new MyICommand(OnCancel, CanExecute);
        }
        #endregion
    }
}
