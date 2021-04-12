using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HospitalService.View.ManagerUI
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        RoomFileStorage storage;
        private Manager manager;
        public ManagerWindow(Manager m)
        {
            InitializeComponent();
            this.DataContext = this;
            storage = new RoomFileStorage();
            manager = m;
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
            new MainWindow().Show();
            this.Close();
        }
    }
}
