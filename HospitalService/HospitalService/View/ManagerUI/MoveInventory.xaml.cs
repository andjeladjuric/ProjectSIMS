using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        InventoryFileStorage invStorage;
        RoomFileStorage roomStorage;
        List<Inventory> roomInventory;
        public DataGrid bind;

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
            //grid.DataContext = this;

            List<String> roomNames = new List<string>();
            String source = "";
            roomStorage = new RoomFileStorage();

            foreach(Room soba in roomStorage.GetAll())
            {
                if (soba.Id != r.Id)
                {
                    source = soba.Id + "/" + soba.Name;
                    roomNames.Add(source);
                }
            }

            comboBox.ItemsSource = roomNames;
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

            int quantity = int.Parse(quantityBox.Text);

            if (tableBinding.SelectedItem != null)
            {
                foreach(Inventory stavka in roomInventory)
                {
                    if(stavka.Id == int.Parse(IDBox.Text))
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

                        newFrame.Content = new ManageRoomInventory(room);
                        break;
                    }
                }
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            newFrame.Content = new ManageRoomInventory(room);
        }
    }
}
