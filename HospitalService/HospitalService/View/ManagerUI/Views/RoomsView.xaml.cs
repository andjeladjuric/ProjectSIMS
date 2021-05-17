using HospitalService.Storage;
using HospitalService.View.ManagerUI.ViewModels;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HospitalService.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for RoomsView.xaml
    /// </summary>
    public partial class RoomsView : Page
    {
        /*RoomFileStorage storage = new RoomFileStorage();
        InventoryFileStorage invStorage = new InventoryFileStorage();
        private RenovationStorage renovationStorage = new RenovationStorage();
        public ObservableCollection<Room> rooms { get; set; }
        public RoomsView()
        {
            InitializeComponent();
            this.DataContext = this;
            renovationStorage.CheckRenovationRequests();
            rooms = new ObservableCollection<Room>();

            foreach (Room r in storage.GetAll())
            {
                rooms.Add(r);
            }
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            newFrame.Content = new NewRoomView();
        }

        private void update_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Room r = (Room)tableBinding.SelectedItem;
            if (r == null)
            {
                MessageBox.Show("Morate izabrati stavku!");
            }
            else
                newFrame.Content = new RoomEditView(r, rooms);
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
                rooms.Remove(r);
            }
        }

        private void inventory_Click(object sender, RoutedEventArgs e)
        {
            Room r = (Room)tableBinding.SelectedItem;

            if (r != null)
            {
                newFrame.Content = new ManageRoomInventoryView(r);
            }
            else
            {
                MessageBox.Show("Morate izabrati sobu!");
            }
        }

        private void renovation_Click(object sender, RoutedEventArgs e)
        {
            Room r = (Room)tableBinding.SelectedItem;
            if (r == null)
            {
                MessageBox.Show("Morate izabrati sobu!");
            }
            else
                newFrame.Content = new RoomRenovationView(r);
        }*/

        RoomsViewModel currentViewModel;
        public RoomsView()
        {
            InitializeComponent();
            currentViewModel = new RoomsViewModel(newFrame);
            this.DataContext = currentViewModel;
        }
    }

    
}


