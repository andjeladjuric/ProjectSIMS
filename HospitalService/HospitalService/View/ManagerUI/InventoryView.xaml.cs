using HospitalService.Storage;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for InventoryView.xaml
    /// </summary>
    public partial class InventoryView : Page
    {
        InventoryFileStorage storage = new InventoryFileStorage();
        public ObservableCollection<Inventory> InventoryList { get; set; }
        public InventoryView()
        {
            InitializeComponent();
            this.DataContext = this;
            InventoryList = new ObservableCollection<Inventory>();

            foreach (Inventory i in storage.GetAll())
            {
                InventoryList.Add(i);
            }
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            newFrame.Content = new NewItem(InventoryList, storage);
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            Inventory item = (Inventory)tableBinding.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("Morate izabrati stavku!");
            }
            else
            {
                newFrame.Content = new EditInventory(item, InventoryList, storage);
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            Inventory item = (Inventory)tableBinding.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("Izaberite stavku!");
            }
            else
            {
                storage.Delete(item.Id);
                InventoryList.Remove(item);
                tableBinding.ItemsSource = InventoryList;
                InventoryType.SelectedIndex = -1;
                tableBinding.Items.Refresh();
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(InventoryType.SelectedIndex != -1)
            {
                ObservableCollection<Inventory> filtered = new ObservableCollection<Inventory>();

                if(InventoryType.SelectedIndex == 0)
                {
                    filtered = InventoryList;
                }
                else if(InventoryType.SelectedIndex == 1)
                {
                    foreach(Inventory i in InventoryList)
                    {
                        if (i.EquipmentType.Equals(Equipment.Static))
                            filtered.Add(i);
                    }
                }
                else
                {
                    foreach (Inventory i in InventoryList)
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
                foreach (Inventory i in InventoryList)
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
                tableBinding.ItemsSource = InventoryList;
            }
        }

        private List<Inventory> GetInventoryForRoom(string roomId)
        {
            List<Inventory> inv = new List<Inventory>();
            RoomInventoryStorage roomInv = new RoomInventoryStorage();
            foreach (RoomInventory r in roomInv.GetAll())
            {
                if (r.RoomId.Equals(roomId))
                {
                    foreach(Inventory i in storage.GetAll())
                    {
                        if (r.ItemId == i.Id)
                            inv.Add(i);
                    }
                }
                    
            }

            return inv;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Popup.IsPopupOpen = false;
            searchId.Text = "";
            searchName.Text = "";
            searchSupplier.Text = "";
            tableBinding.ItemsSource = InventoryList;
        }
    }
}

