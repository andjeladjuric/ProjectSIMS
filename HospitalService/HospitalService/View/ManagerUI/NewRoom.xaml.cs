using Model;
using System;
using System.Collections.Generic;
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
    public partial class NewRoom : Page
    {
        public Room room { get; set; }
        RoomFileStorage storage;
        DataGrid bind;
        public static List<String> roomTypes = Enum.GetNames(typeof(RoomType)).ToList();
        Regex checkName = new Regex(@"[A-Za-z]+([\s][A-Za-z]*[1-9]*)*$");
        Regex checkId = new Regex(@"[1-4]{1}[0-9]{2}[A-Za-z]{0,1}$");
        public NewRoom(DataGrid dg, RoomFileStorage st)
        {
            InitializeComponent();
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

        private void idTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!checkId.IsMatch(IDBox.Text))
            {
                label2.Visibility = Visibility.Visible;
                save.IsEnabled = false;
            }
            else
            {
                label2.Visibility = Visibility.Hidden;
            }

        }

        private void nameTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!checkName.IsMatch(NameBox.Text))
            {
                label.Visibility = Visibility.Visible;
                save.IsEnabled = false;
            }
            else
            {
                label.Visibility = Visibility.Hidden;
                save.IsEnabled = true;
            }
        }
    }
}

