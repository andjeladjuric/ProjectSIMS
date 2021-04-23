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
            invStorage.analyzeRequests();

            roomInventory = new ObservableCollection<Inventory>();
            foreach (Inventory i in invStorage.getInventoryForRoom(r))
            {
                roomInventory.Add(i);
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
    }
}
