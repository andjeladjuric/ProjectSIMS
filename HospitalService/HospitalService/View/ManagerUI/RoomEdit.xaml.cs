using Model;
using System;
using System.Collections.Generic;
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
    public partial class RoomEdit : Page
    {
        public Room room { get; set; }
        RoomFileStorage storage;
        public static List<String> roomTypes = Enum.GetNames(typeof(Model.RoomType)).ToList();
        public DataGrid bind;
        Regex checker = new Regex(@"[A-Za-z]+([\s][A-Za-z]*[1-9]*)*$");
        public RoomEdit(Room r, DataGrid dg, RoomFileStorage st)
        {
            InitializeComponent();
            room = r;
            bind = dg;
            storage = st;
            grid.DataContext = this;
            comboBox.ItemsSource = roomTypes;
            comboBox.SelectedItem = room.Type.ToString();
            IDBox.Text = room.Id;
            NameBox.Text = room.Name;

            if (room.IsFree)
                available.IsChecked = true;
            else
                notAvailable.IsChecked = true;

            tableBinding.ItemsSource = storage.GetAll();
            tableBinding.SelectedItem = storage.getOne(IDBox.Text);
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

        private void textChanged(object sender, TextChangedEventArgs e)
        {
            if (!checker.IsMatch(NameBox.Text))
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


