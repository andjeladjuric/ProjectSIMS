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
    /// Interaction logic for MoveInventory.xaml
    /// </summary>
    public partial class MoveInventory : Page, INotifyPropertyChanged 
    {
        public Room room;
        InventoryFileStorage invStorage = new InventoryFileStorage();
        RoomFileStorage roomStorage;
       // ObservableCollection<Inventory> roomInventory;
        public DataGrid bind;
        public MovingRequests m = new MovingRequests();
        public List<MovingRequests> requests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));

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
        public MoveInventory(Room r, DataGrid dg, ObservableCollection<Inventory> inv)
        {
            InitializeComponent();

            this.DataContext = this;

            room = r;
            bind = dg;
            roomInventory = inv;

            List<String> roomNames = new List<string>();
            String source = "";
            roomStorage = new RoomFileStorage();

            foreach (Room soba in roomStorage.GetAll())
            {
                if (soba.Id != r.Id)
                {
                    source = soba.Id + "/" + soba.Name;
                    roomNames.Add(source);
                }
            }

            comboBox.ItemsSource = roomNames;
            //tableBinding.ItemsSource = inv;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            string[] splitId = comboBox.SelectedItem.ToString().Split("/");
            string selectedId = splitId[0];

            Room sendToThisRoom = null;
            foreach (Room r in roomStorage.GetAll())
            {
                if (r.Id == selectedId)
                {
                    sendToThisRoom = r;
                    break;
                }
            }

            int quantity = int.Parse(quantityBox.Text);
            int inventoryId = int.Parse(IDBox.Text);
            String time = TimeBox.Text;
            String date = datePicker.Text;

            foreach (Inventory i in invStorage.GetAll())
            {
                if (inventoryId == i.Id)
                {
                    if (i.EquipmentType == Equipment.Dynamic)
                    {
                        invStorage.moveDynamicInventory(sendToThisRoom, room, quantity, inventoryId, roomInventory, roomStorage);
                        break;
                    }
                    else
                    {
                        m.inventoryId = inventoryId;
                        m.moveFromThisRoom = room.Id;
                        m.sendToThisRoom = sendToThisRoom.Id;
                        m.movingTime = Convert.ToDateTime(time + " " + date);
                        m.quantity = quantity;
                        m.isDone = false;
                        requests.Add(m);
                        File.WriteAllText(@"..\..\..\Data\requests.json", JsonConvert.SerializeObject(requests));
                        break;
                    }

                }
            }       

            newFrame.Content = new ManageRoomInventory(room);
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            newFrame.Content = new ManageRoomInventory(room);
        }

        private void selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedInv.EquipmentType == Equipment.Dynamic)
            {
                datePicker.IsEnabled = false;
                TimeBox.IsEnabled = false;
            }
            else
            {
                datePicker.IsEnabled = true;
                TimeBox.IsEnabled = true;
            }
        }
    }
}
