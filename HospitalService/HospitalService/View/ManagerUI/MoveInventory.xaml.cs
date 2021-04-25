using HospitalService.View.ManagerUI.Logic;
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
        FunctionsForRoomInventory functions = new FunctionsForRoomInventory();
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

                //int quantity = EnteredQuantity);
                int inventoryId = Int32.Parse(IDBox.Text);

                /*if (selectedInv.EquipmentType == Equipment.Dynamic)
                {
                    functions.AnalyzeRequests(new MovingRequests(DateTime.Now, EnteredQuantity, room.Id, sendToThisRoom.Id, inventoryId));
                }
                else
                {
                    TimeSpan selectedTime = TimeSpan.ParseExact(Time, "c", null);
                    Date = Date.Add(selectedTime);
                    MovingRequests request = new MovingRequests(Date, EnteredQuantity, room.Id, sendToThisRoom.Id, selectedInv.Id);
                    functions.StartMoving(request);
                }*/

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
                            //Date = Date.Add(selectedTime);
                            MovingRequests request = new MovingRequests();
                            request.movingTime = Date;
                            request.quantity = EnteredQuantity;
                            request.moveFromThisRoom = room.Id;
                            request.sendToThisRoom = sendToThisRoom.Id;
                            request.inventoryId = inventoryId;
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
    }
}
