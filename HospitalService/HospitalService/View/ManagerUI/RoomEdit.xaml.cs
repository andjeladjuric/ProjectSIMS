using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private string _name;
        public string roomName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("roomName");
            }
        }
        public Room room { get; set; }
        RoomFileStorage storage;
        public ObservableCollection<Room> roomList { get; set; }
        public RoomEdit(Room r, ObservableCollection<Room> rooms, RoomFileStorage st)
        {
            InitializeComponent();
            this.DataContext = this;
            room = r;
            roomList = rooms;
            storage = st;

            roomName = room.Name;
        }

        private void saveClick(object sender, RoutedEventArgs e)
        {
            RoomType tip = (RoomType)comboBox.SelectedIndex;
            string id = room.Id;
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
            newFrame.NavigationService.Navigate(new RoomsView());
        }

        private void cancelClick(object sender, RoutedEventArgs e)
        {
            NameBox.Visibility = Visibility.Hidden;
            newFrame.NavigationService.Navigate(new RoomsView());
        }
    }
}


