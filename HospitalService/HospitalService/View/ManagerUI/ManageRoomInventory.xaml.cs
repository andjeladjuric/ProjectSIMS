using HospitalService.Storage;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for ManageRoomInventory.xaml
    /// </summary>
    public partial class ManageRoomInventory : Page
    {
        InventoryFileStorage invStorage = new InventoryFileStorage();
        RoomFileStorage roomStorage = new RoomFileStorage();
        public ObservableCollection<Inventory> roomInventory { get; set; }
        Room r;
        public ManageRoomInventory(Room room)
        {
            InitializeComponent();

            this.DataContext = this; 
            r = room;
            IDBox.Text = r.Id;
            //invStorage.analyzeRequests();

            roomInventory = new ObservableCollection<Inventory>();
            RoomInventoryStorage f = new RoomInventoryStorage();

            foreach(RoomInventory ri in f.GetAll())
            {
                if (ri.RoomId.Equals(r.Id))
                {
                    foreach(Inventory i in invStorage.GetAll())
                    {
                        if(ri.ItemId == i.Id)
                        {
                            roomInventory.Add(new Inventory(ri.ItemId, i.Name, i.EquipmentType, ri.Quantity, i.Supplier));
                            break;
                        }
                    }
                }
            }
        }

        private void Premesti_Click(object sender, RoutedEventArgs e)
        {
            newFrame.Content = new MoveInventory(r, roomInventory);
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            newFrame.Content = new RoomsView();
        }

        private void changeQuantity_Click(object sender, RoutedEventArgs e)
        {
            newFrame.Content = new ChangeInventoryQuantity(r, roomInventory);
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InventoryType.SelectedIndex != -1)
            {
                ObservableCollection<Inventory> filtered = new ObservableCollection<Inventory>();

                if (InventoryType.SelectedIndex == 0)
                {
                    filtered = roomInventory;
                }
                else if (InventoryType.SelectedIndex == 1)
                {
                    foreach (Inventory i in roomInventory)
                    {
                        if (i.EquipmentType.Equals(Equipment.Static))
                            filtered.Add(i);
                    }
                }
                else
                {
                    foreach (Inventory i in roomInventory)
                    {
                        if (i.EquipmentType.Equals(Equipment.Dynamic))
                            filtered.Add(i);
                    }
                }

                tableBinding.ItemsSource = filtered;
            }
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
