using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    /// Interaction logic for RoomEdit.xaml
    /// </summary>
    public partial class RoomEdit : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _id;
        public string roomId
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged("roomId");
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
        public static List<String> roomTypes = Enum.GetNames(typeof(RoomType)).ToList();
        public DataGrid bind;
        public RoomEdit(Room r, DataGrid dg, RoomFileStorage st)
        {
            InitializeComponent();
            room = r;
            bind = dg;
            storage = st;
            this.DataContext = this;
            comboBox.ItemsSource = roomTypes;
            comboBox.SelectedItem = room.Type.ToString();
            roomId = room.Id;
            roomName = room.Name;

            if (room.IsFree)
                available.IsChecked = true;
            else
                notAvailable.IsChecked = true;

            tableBinding.ItemsSource = storage.GetAll();
            tableBinding.SelectedItem = storage.getOne(roomId);
        }

        private void saveClick(object sender, RoutedEventArgs e)
        {
            RoomType tip = (RoomType)comboBox.SelectedIndex;
            string id = IDBox.Text;
            string neki = NameBox.Text;
            Boolean slobodan;
            if ((bool)available.IsChecked)
            {
                slobodan = true;
            }
            else
            {
                slobodan = false;
            }

            storage.Edit(id, neki, tip, slobodan);
            NavigationService.Navigate(new Page());
            bind.Items.Refresh();
        }

        private void cancelClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page());
        }
    }
}


