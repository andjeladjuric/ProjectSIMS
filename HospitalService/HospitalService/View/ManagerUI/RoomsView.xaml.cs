using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for RoomsView.xaml
    /// </summary>
    public partial class RoomsView : Page
    {
        RoomFileStorage storage;
        public RoomsView()
        {
            InitializeComponent();
            this.DataContext = this;
            storage = new RoomFileStorage();
            List<Room> rooms = storage.GetAll();

            tableBinding.ItemsSource = rooms;

        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            newFrame.Content = new NewRoom(tableBinding, storage);
        }

        private void update_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Room r = (Room)tableBinding.SelectedItem;
            if (r == null)
            {
                MessageBox.Show("You must select an item!");
            }
            else
            {
                newFrame.Content = new RoomEdit(r, tableBinding, storage);
                //this.NavigationService.Navigate(new RoomEdit(r));
            }
        }

        private void delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Room r = (Room)tableBinding.SelectedItem;
            if (r == null)
            {
                MessageBox.Show("Morate izabrati sobu!");
            }
            else
            {
                storage.Delete(r.Id);
                tableBinding.Items.Refresh();

            }
        }

        private void inventory_Click(object sender, RoutedEventArgs e)
        {
            Room r = (Room)tableBinding.SelectedItem;
            newFrame.Content = new ManageRoomInventory(r);
        }
    }

    public class EnumValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "patientroom":
                    return "Bolnička soba";
                case "storageroom":
                    return "Skladište";
                case "operatingroom":
                    return "Operaciona sala";
                case "examinationroom":
                    return "Ordinacija";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                if (value.Equals("Bolnička soba"))
                    return RoomType.PatientRoom;
                else if (value.Equals("Skladište"))
                    return RoomType.StorageRoom;
                else if (value.Equals("Operaciona sala"))
                    return RoomType.OperatingRoom;
                else if (value.Equals("Ordinacija"))
                    return RoomType.ExaminationRoom;
            }

            return null;
        }
    }
}

