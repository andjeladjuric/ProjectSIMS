using HospitalService.Storage;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalService.View.ManagerUI
{
    /// <summary>
    /// Interaction logic for ChangeInventoryQuantity.xaml
    /// </summary>
    public partial class ChangeInventoryQuantity : Page, INotifyPropertyChanged
    {
        public Room room;
        InventoryFileStorage invStorage = new InventoryFileStorage();
        RoomFileStorage roomStorage;
        public ObservableCollection<Inventory> roomInventory { get; set; }

        private Inventory _i;
        public Inventory selectedInv
        {
            get { return _i; }
            set
            {
                _i = value;
                OnPropertyChanged("selectedInv");
            }
        }

        private string _enteredQuantity;
        public string EnteredQuantity
        {
            get { return _enteredQuantity; }
            set
            {
                _enteredQuantity = value;
                OnPropertyChanged("EnteredQuantity");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ChangeInventoryQuantity(Room r, ObservableCollection<Inventory> inv)
        {
            InitializeComponent();
            this.DataContext = this;

            room = r;

            roomInventory = new ObservableCollection<Inventory>();
            foreach(Inventory i in inv)
            {
                if (i.EquipmentType == Equipment.Dynamic)
                    roomInventory.Add(i);
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            quantityBox.Visibility = Visibility.Hidden;
            newFrame.Content = new ManageRoomInventory(room);
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            int quantity = Int32.Parse(quantityBox.Text);

            Inventory item;
            for (int i = 0; i < invStorage.GetAll().Count; i++)
            {
                item = invStorage.GetAll()[i];
                if (item.Id == selectedInv.Id)
                {
                    if (item.Quantity == quantity)
                    {
                        invStorage.GetAll().RemoveAt(i);
                        break;
                    }
                    else
                    {
                        item.Quantity -= quantity;
                        break;
                    }
                }
            }

            RoomInventoryStorage f = new RoomInventoryStorage();
            RoomInventory ri = f.GetRoomInventoryByIds(room.Id, selectedInv.Id);

            if (ri.Quantity == quantity)
            {
                f.GetAll().Remove(ri);
            }
            else
            {
                ri.Quantity -= quantity;
            }

            for (int i = 0; i < roomInventory.Count; i++)
            {
                item = roomInventory[i];
                if (item.Id == selectedInv.Id)
                {
                    if (item.Quantity == quantity)
                    {
                        roomInventory.RemoveAt(i);
                        break;
                    }
                    else
                    {
                        item.Quantity -= quantity;
                        break;
                    }
                }
            }

            f.SerializeRoomInventory();
            File.WriteAllText(@"..\..\..\Data\inventory.json", JsonConvert.SerializeObject(invStorage.GetAll()));


            newFrame.Content = new ManageRoomInventory(room);
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string id = searchId.Text.ToLower().Trim();
            string name = searchName.Text.ToLower().Trim();
            string supplier = searchSupplier.Text.ToLower().Trim();
            RoomInventoryStorage roomInv = new RoomInventoryStorage();
            RoomFileStorage rooms = new RoomFileStorage();
            List<Inventory> filtered = new List<Inventory>();

            if (id != "" || name != "" || supplier != "")
            {
                foreach (Inventory i in roomInventory)
                {
                    if (i.Name.ToLower().Contains(name) && i.Id.ToString().Contains(id)
                        && i.Supplier.ToLower().Contains(supplier))
                    {
                        filtered.Add(i);
                    }
                }
                tableBinding.ItemsSource = filtered;
            }
            else
            {
                tableBinding.ItemsSource = roomInventory;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Popup.IsPopupOpen = false;
            searchId.Text = "";
            searchName.Text = "";
            searchSupplier.Text = "";
            tableBinding.ItemsSource = roomInventory;
        }
    }
}
