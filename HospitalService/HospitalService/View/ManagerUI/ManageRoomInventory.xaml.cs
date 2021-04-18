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
    public partial class ManageRoomInventory : Page, INotifyPropertyChanged
    {
        InventoryFileStorage invStorage = new InventoryFileStorage();
        RoomFileStorage roomStorage = new RoomFileStorage();
       // ObservableCollection<Inventory> roomInventory = new ObservableCollection<Inventory>();
        Room r;

        private ObservableCollection<Inventory> _roomInv;
        public ObservableCollection<Inventory> roomInventory
        {
            get { return _roomInv; }
            set { _roomInv = value; OnPropertyChanged("roomInventory"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ManageRoomInventory(Room room)
        {
            InitializeComponent();

            this.DataContext = this; 
            r = room;
            IDBox.Text = r.Id;
            invStorage.analyzeRequests();

            roomInventory = new ObservableCollection<Inventory>();

            foreach (int i in r.inventory.Keys)
            {
                foreach(Inventory inv in invStorage.GetAll())
                {
                    if(i == inv.Id)
                    {
                        inv.Quantity = r.inventory[i];
                        roomInventory.Add(inv);
                    }
                }
            }

            tableBinding.Items.Refresh();

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
