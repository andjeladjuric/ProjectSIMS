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
        private bool demoOn;
        private bool isOpen;
        private CancellationTokenSource cts = new CancellationTokenSource();
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
                MessageViewModel.Message = "Završena četvrta funkcionalnost \n Sledi - dodavanje lekova";
                ct.ThrowIfCancellationRequested();

                await Task.Delay(1500, ct);
                SelectedItem = service.GetOne(321);
                await Task.Delay(2000, ct);
                ItemId = SelectedItem.Id;
                await Task.Delay(2000, ct);
                ItemName = "Izmenjen naziv";
                await Task.Delay(2000, ct);
                Quantity = "15";
                await Task.Delay(2000, ct);
                Supplier = "Izmenjen proizvođač";
                await Task.Delay(2000, ct);
                Type = Equipment.Static;

                await Task.Delay(2000, ct);
                this.Frame.NavigationService.Navigate(new InventoryView());
                IsPopupOpen = true;
                await Task.Delay(2000, ct);
                IsPopupOpen = false;
                this.Frame.NavigationService.Navigate(new MedicationsView());
                await Task.Delay(3000, ct);
                this.Frame.NavigationService.Navigate(new AddMedicationView(DemoOn));
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
