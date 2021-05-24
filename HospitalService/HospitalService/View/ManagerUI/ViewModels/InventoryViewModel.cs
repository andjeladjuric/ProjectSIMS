using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class InventoryViewModel : ViewModel
    {
        #region Fields
        private ObservableCollection<Inventory> inventory;
        public ObservableCollection<Inventory> Inventory
        {
            get { return inventory; }
            set
            {
                inventory = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Inventory> filtered;
        public ObservableCollection<Inventory> Filtered
        {
            get { return filtered; }
            set
            {
                filtered = value;
                OnPropertyChanged();
            }
        }

        private Frame frame;
        public Frame Frame
        {
            get { return frame; }
            set { frame = value; }
        }

        private Inventory selectedItem;
        public Inventory SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                DeleteCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
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

        private string itemId;
        public string ItemId
        {
            get { return itemId; }
            set
            {
                itemId = value;
                OnPropertyChanged();
            }
        }

        private string itemName;
        public string ItemName
        {
            get { return itemName; }
            set
            {
                itemName = value;
                OnPropertyChanged();
            }
        }

        private string supplier;
        public string Supplier
        {
            get { return supplier; }
            set
            {
                supplier = value;
                OnPropertyChanged();
            }
        }
        public CollectionView MainView { get; set; }
        #endregion

        #region Commands
        public MyICommand AddCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }
        public MyICommand EditCommand { get; set; }
        public MyICommand FilterTypeCommand { get; set; }
        public MyICommand SearchCommand { get; set; }
        #endregion

        #region Actions
        private void OnAdd()
        {
            this.Frame.NavigationService.Navigate(new NewItemView());
        }

        private void OnDelete()
        {
            InventoryService inventoryService = new InventoryService();
            inventoryService.Delete(SelectedItem.Id);
            Inventory.Remove(SelectedItem);
        }

        private void OnEdit()
        {
            this.Frame.NavigationService.Navigate(new EditInventoryView(SelectedItem, Inventory));
        }

        private void OnFilter()
        {
            InventoryService inv = new InventoryService();
            Filtered = new ObservableCollection<Inventory>();
            LoadInventory();
            if (SelectedType != -1)
            {
                if (SelectedType == 0)
                {
                    Filtered = Inventory;
                }
                else if (SelectedType == 1)
                {
                    foreach (Inventory i in inv.GetAll())
                    {
                        if (i.EquipmentType.Equals(Equipment.Static))
                            Filtered.Add(i);
                    }
                }
                else
                {
                    foreach (Inventory i in inv.GetAll())
                    {
                        if (i.EquipmentType.Equals(Equipment.Dynamic))
                            Filtered.Add(i);
                    }
                }

                Inventory = Filtered;
                MainView.Refresh();
            }
        }

        private void OnSearch()
        {
            LoadInventory();
            Filtered = new ObservableCollection<Inventory>();
            if (ItemId.ToLower().Trim() != "" || ItemName.ToLower().Trim() != "" || Supplier.ToLower().Trim() != "")
            {
                foreach (Inventory i in Inventory)
                {
                    if (i.Name.ToLower().Contains(ItemName) && i.Id.ToString().Contains(ItemId)
                        && i.Supplier.ToLower().Contains(Supplier))
                    {
                        Filtered.Add(i);
                    }
                }

                Inventory = Filtered;
                MainView.Refresh();
            }
            else
            {
                LoadInventory();
                MainView.Refresh();
            }
        }

        private bool CanEditOrDelete()
        {
            return SelectedItem != null;
        }

        private bool CanExecute()
        {
            return true;
        }
        #endregion

        #region Other functions
        private void LoadInventory()
        {
            InventoryService inv = new InventoryService();
            Inventory = new ObservableCollection<Inventory>();
            foreach (Inventory i in inv.GetAll())
            {
               Inventory.Add(i);
            }
        }
        #endregion

        #region Constructors
        public InventoryViewModel(Frame currentFrame)
        {
            LoadInventory();
            this.Frame = currentFrame;
            this.ItemName = "";
            this.ItemId = "";
            this.Supplier = "";
            MainView = (CollectionView)CollectionViewSource.GetDefaultView(Inventory);
            AddCommand = new MyICommand(OnAdd, CanExecute);
            DeleteCommand = new MyICommand(OnDelete, CanEditOrDelete);
            EditCommand = new MyICommand(OnEdit, CanEditOrDelete);
            FilterTypeCommand = new MyICommand(OnFilter, CanExecute);
            SearchCommand = new MyICommand(OnSearch, CanExecute);
        }
        #endregion
    }
}
