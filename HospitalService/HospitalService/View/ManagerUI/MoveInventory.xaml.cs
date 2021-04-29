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
    /// Interaction logic for MoveInventory.xaml
    /// </summary>
    public partial class MoveInventory : Page, INotifyPropertyChanged 
    {
        public Room room;
        InventoryFileStorage invStorage = new InventoryFileStorage();
        RoomFileStorage roomStorage;
        public ObservableCollection<Inventory> roomInventory { get; set; }
        public List<String> roomNames { get; set; }
        RoomInventoryStorage functions = new RoomInventoryStorage();
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

        private DateTime _date;
        public DateTime Date
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

        private TimeSpan _currentTime;
        public TimeSpan currentTime
        {
            get { return _currentTime; }
            set
            {
                _currentTime = value;
                OnPropertyChanged("currentTime");
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
        public MoveInventory(Room r, ObservableCollection<Inventory> inv)
        {
            InitializeComponent();

            this.DataContext = this;

            room = r;
            roomInventory = inv;

            String source = "";
            roomStorage = new RoomFileStorage();
            roomNames = new List<string>();
            foreach (Room soba in roomStorage.GetAll())
            {
                if (soba.Id != r.Id)
                {
                    source = soba.Id + "/" + soba.Name;
                    roomNames.Add(source);
                }
            }

            currentTime = new TimeSpan(0, 0, 0);
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedIndex == -1)
                MessageBox.Show("Izaberite prostoriju!");
            else
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

                int inventoryId = Int32.Parse(IDBox.Text);

                foreach (Inventory i in roomInventory)
                {
                    if(i.Id == inventoryId)
                    {
                        if (i.EquipmentType.Equals(Equipment.Dynamic))
                        {
                            functions.AnalyzeRequests(new MovingRequests(DateTime.Now, EnteredQuantity, room.Id, sendToThisRoom.Id, inventoryId));
                        }
                        else
                        {
                            TimeSpan selectedTime = TimeSpan.ParseExact(Time, "c", null);
                            Date = Convert.ToDateTime(selectedTime + " " + datePicker.Text);
                            MovingRequests request = new MovingRequests(Date, EnteredQuantity, room.Id, sendToThisRoom.Id, inventoryId);
                            functions.StartMoving(request);

                        }
                    }
                }
            }
            newFrame.Content = new ManageRoomInventory(room);
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            TimeBox.Visibility = Visibility.Hidden;
            quantityBox.Visibility = Visibility.Hidden;
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
