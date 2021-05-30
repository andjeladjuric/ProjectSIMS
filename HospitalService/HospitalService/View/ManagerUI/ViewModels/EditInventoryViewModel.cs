using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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
        private Frame frame;
        private bool demoOn;
        private bool isOpen;
        #endregion

        #region Properties
        public bool IsPopupOpen
        {
            get { return isOpen; }
            set
            {
                isOpen = value;
                OnPropertyChanged();
            }
        }
        public bool DemoOn
        {
            get { return demoOn; }
            set
            {
                demoOn = value;
                OnPropertyChanged();
            }
        }
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

        #region Other Functions
        private async Task DemoIsOn(CancellationToken ct)
        {
            if (DemoOn)
            {
                InventoryService service = new InventoryService();
                RoomService rooms = new RoomService();
                ct.ThrowIfCancellationRequested();

                await Task.Delay(1500, ct);
                SelectedItem = service.GetOne(321);
                await Task.Delay(2000, ct);
                ItemId = SelectedItem.Id;
                await Task.Delay(2000, ct);
                ItemName = "Izmenjen naziv";
                await Task.Delay(2000, ct);
                Supplier = "Izmenjen proizvođač";
                await Task.Delay(2000, ct);
                Quantity = "15";
                await Task.Delay(2000, ct);
                Type = Equipment.Static;

                await Task.Delay(2000, ct);
                this.Frame.NavigationService.Navigate(new InventoryView());
                IsPopupOpen = true;
                await Task.Delay(1500, ct);
                IsPopupOpen = false;
                ManagerWindowViewModel.cts.Cancel();
                DemoOn = false;
            }
        }
        #endregion

        #region Constructors
        public EditInventoryViewModel(Frame currentFrame, Inventory item, bool demo)
        {
            this.Frame = currentFrame;
            this.SelectedItem = item;
            this.ItemId = SelectedItem.Id;
            this.ItemName = SelectedItem.Name;
            this.Supplier = SelectedItem.Supplier;
            this.Quantity = SelectedItem.Quantity.ToString();
            this.Type = SelectedItem.EquipmentType;
            this.DemoOn = demo;

            ConfirmCommand = new MyICommand(OnConfirm, CanExecute);
            CancelCommand = new MyICommand(OnCancel, CanExecute);

            CancellationTokenSource cts = ManagerWindowViewModel.cts;
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
