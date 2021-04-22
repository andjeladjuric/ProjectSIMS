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
        List<Inventory> roomInventory;
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

        private string _time;
        public string Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged("Time");
            }
        }

        private string _date;
        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        private int _enteredQuantity;
        public int EnteredQuantity
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
        public MoveInventory(Room r, DataGrid dg, List<Inventory> inv)
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
            quantityBox.Text = "";
            tableBinding.ItemsSource = inv;
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

            int quantity = EnteredQuantity;
            int inventoryId = int.Parse(IDBox.Text);
            String time = Time;
            String date = Date;

            foreach (Inventory stavka in roomInventory)
            {
                if (inventoryId == stavka.Id)
                {
                    if (stavka.EquipmentType == Equipment.Dynamic)
                    {
                        if (stavka.Quantity == quantity)
                        {
                            roomInventory.Remove(stavka);
                            room.inventory.Remove(stavka.Id);
                            roomStorage.editRoom(room);

                            if (sendToThisRoom.inventory.ContainsKey(stavka.Id))
                            {
                                sendToThisRoom.inventory[stavka.Id] += quantity;
                                File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(roomStorage.GetAll()));
                            }
                            else
                            {
                                sendToThisRoom.inventory.Add(stavka.Id, quantity);
                                File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(roomStorage.GetAll()));
                            }
                        }
                        else if (quantity > stavka.Quantity)
                            MessageBox.Show("Ne postoji dovoljno stavki za premeštanje!");
                        else
                        {
                            stavka.Quantity = stavka.Quantity - quantity;
                            room.inventory[stavka.Id] = stavka.Quantity;
                            roomStorage.editRoom(room);

                            if (sendToThisRoom.inventory.ContainsKey(stavka.Id))
                            {
                                sendToThisRoom.inventory[stavka.Id] += quantity;
                                File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(roomStorage.GetAll()));
                            }
                            else
                            {
                                sendToThisRoom.inventory.Add(stavka.Id, quantity);
                                File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(roomStorage.GetAll()));
                            }
                        }

                        //newFrame.Content = new ManageRoomInventory(room);
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
            NavigationService.Navigate(new Page());
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
