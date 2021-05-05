using HospitalService.View.ManagerUI.Validations;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private string _roomName;
        public string RoomName
        {
            get
            {
                return _roomName;
            }
            set
            {
                if (value != _roomName)
                {
                    _roomName = value;
                    OnPropertyChanged("RoomName");
                }
            }
        }

        public Room room { get; set; }
        RoomFileStorage storage;
        ObservableCollection<Room> bind { get; set; }

        public NewRoom(ObservableCollection<Room> r, RoomFileStorage st)
        {
            InitializeComponent();
            this.DataContext = this;

            bind = r;
            storage = st;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            room = new Room();
            room.Type = (RoomType)comboBox.SelectedIndex;
            room.Id = IDBox.Text;
            room.Name = NameBox.Text;
            room.IsFree = true;
            storage.Save(room);
            bind.Add(room);
            newFrame.NavigationService.Navigate(new RoomsView());
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            IDBox.Visibility = Visibility.Hidden;
            NameBox.Visibility = Visibility.Hidden;
            newFrame.NavigationService.Navigate(new RoomsView());
        }
    }
}

