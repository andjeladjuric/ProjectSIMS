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
        List<Inventory> roomInventory;
        Room r;
        public ManageRoomInventory(Room room)
        {
            InitializeComponent();

            this.DataContext = this; 
            r = room;
            IDBox.Text = r.Id;
            invStorage.analyzeRequests();

            roomInventory = new List<Inventory>();

            foreach (var item in r.inventory)
            {
                foreach (Inventory inv in invStorage.GetAll())
                {
                    if (item.Key == inv.Id)
                    {
                        roomInventory.Add(new Inventory { Id = item.Key, Quantity = item.Value, Name = inv.Name, EquipmentType = inv.EquipmentType });
                    }
                }
            }

            tableBinding.ItemsSource = roomInventory;
        }

        private void Premesti_Click(object sender, RoutedEventArgs e)
        {
            newFrame.Content = new MoveInventory(r, tableBinding, roomInventory);
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            newFrame.Content = new RoomsView();
        }
    }
}
