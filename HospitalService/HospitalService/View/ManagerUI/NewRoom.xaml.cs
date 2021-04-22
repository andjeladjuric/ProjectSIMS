using HospitalService.View.ManagerUI.Validations;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for NewRoom.xaml
    /// </summary>
    public partial class NewRoom : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _roomId;
        public string RoomId
        {
            get
            {
                return _roomId;
            }
            set
            {
                if (value != _roomId)
                {
                    _roomId = value;
                    OnPropertyChanged("RoomId");
                }
            }
        }

        private string _name;
        public string roomName
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("roomName");
                }
            }
        }

        public Room room { get; set; }
        RoomFileStorage storage;
        DataGrid bind;
        public static List<String> roomTypes = Enum.GetNames(typeof(RoomType)).ToList();

        public NewRoom(DataGrid dg, RoomFileStorage st)
        {
            InitializeComponent();
            this.DataContext = this;

            bind = dg;
            storage = st;
            comboBox.ItemsSource = roomTypes;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            room = new Room();
            room.Type = (RoomType)comboBox.SelectedIndex;
            room.Id = IDBox.Text;
            room.Name = NameBox.Text;
            room.inventory = new Dictionary<int, int>();
            if ((bool)available.IsChecked)
            {
                room.IsFree = true;
            }
            else
            {
                room.IsFree = false;
            }

            storage.Save(room);
            bind.Items.Refresh();
            NavigationService.Navigate(new Page());
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page());
        }
    }
}

