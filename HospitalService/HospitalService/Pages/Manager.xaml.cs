using Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace HospitalService.Pages
{
    /// <summary>
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Page
    {
        RoomFileStorage storage;
        public Manager()
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
                MessageBox.Show("You must select an item!");
            }
            else
            {
                storage.Delete(r.Id);
                tableBinding.Items.Refresh();

            }
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Start());
        }
    }
}
