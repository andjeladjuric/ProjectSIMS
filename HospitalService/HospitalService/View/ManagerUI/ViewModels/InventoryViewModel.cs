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
                FilterCollection();
            }
        }
       
        private ICollectionView inventoryView;
        public ICollectionView InventoryView
        {
            get { return inventoryView; }
            set
            {
                inventoryView = value;
                OnPropertyChanged();
            }
        }

        private string filterId;
        public string FilterId
        {
            get { return filterId; }
            set
            {
                filterId = value;
                OnPropertyChanged();
                FilterCollection();
            }
        }

        private string filterName;
        public string FilterName
        {
            get { return filterName; }
            set
            {
                filterName = value;
                OnPropertyChanged();
                FilterCollection();
            }
        }

        private string filterSupplier;
        public string FilterSupplier
        {
            get { return filterSupplier; }
            set
            {
                filterSupplier = value;
                OnPropertyChanged();
                FilterCollection();
            }
        }
        #endregion

        #region Commands
        public MyICommand AddCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }
        public MyICommand EditCommand { get; set; }
        public MyICommand CancelSearch { get; set; }
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
            this.Frame.NavigationService.Navigate(new EditInventoryView(SelectedItem, false));
        }

        private void OnCancel()
        {
            FilterName = "";
            FilterId = "";
            FilterSupplier = "";
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

        private void FilterCollection()
        {
            if (InventoryView != null)
            {
                InventoryView.Refresh();
            }
        }

        public bool Filter(object obj)
        {
            RoomInventoryService service = new RoomInventoryService();
            return service.Filter(obj, FilterId, FilterName, FilterSupplier, SelectedType);
        }
        #endregion

        #region Constructors
        public InventoryViewModel(Frame currentFrame)
        {
            LoadInventory();
            this.Frame = currentFrame;
            this.FilterName = "";
            this.FilterId = "";
            this.FilterSupplier = "";
            InventoryView = CollectionViewSource.GetDefaultView(Inventory);
            InventoryView.Filter = new Predicate<object>(Filter);
            AddCommand = new MyICommand(OnAdd, CanExecute);
            DeleteCommand = new MyICommand(OnDelete, CanEditOrDelete);
            EditCommand = new MyICommand(OnEdit, CanEditOrDelete);
            CancelSearch = new MyICommand(OnCancel, CanExecute);
        }
        #endregion
    }
}
