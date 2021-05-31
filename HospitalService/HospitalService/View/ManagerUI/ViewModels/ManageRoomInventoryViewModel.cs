using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

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
        public Frame Frame { get; set; }
        #endregion

        #region Commands
        public MyICommand MoveInventoryCommand { get; set; }
        public MyICommand ChangeQuantityCommand { get; set; }
        public MyICommand CancelSearch { get; set; }
        #endregion

        #region Actions 
        private void OnMoveInventory()
        {
            this.Frame.NavigationService.Navigate(new MoveInventoryView(SelectedRoom, Inventory, false));
        }

        private void OnChangeQuantity()
        {
            this.Frame.NavigationService.Navigate(new ChangeInventoryQuantityView(SelectedRoom, Inventory));
        }

        private void OnCancel()
        {
            FilterName = "";
            FilterId = "";
            FilterSupplier = "";
        }

        private bool CanExecute()
        {
            return true;
        }
        #endregion

        #region Other Functions
        private void LoadRoomInventory()
        {
            RoomInventoryService service = new RoomInventoryService();
            Inventory = service.LoadRoomInventory(SelectedRoom);
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
            return service.Filter(obj, FilterId, FilterName, FilterSupplier, -1);
        }
        #endregion

        #region Constructors
        public ManageRoomInventoryViewModel(Frame frame, Room selected)
        {
            /*view*/
            this.Frame = frame;
            this.SelectedRoom = selected;
            LoadRoomInventory();

            /*search filters*/
            this.FilterName = "";
            this.FilterId = "";
            this.FilterSupplier = "";
            InventoryView = CollectionViewSource.GetDefaultView(Inventory);
            InventoryView.Filter = new Predicate<object>(Filter);

            /*commands*/
            ChangeQuantityCommand = new MyICommand(OnChangeQuantity, CanExecute);
            MoveInventoryCommand = new MyICommand(OnMoveInventory, CanExecute);
            CancelSearch = new MyICommand(OnCancel, CanExecute);


        }
        #endregion
    }
}
