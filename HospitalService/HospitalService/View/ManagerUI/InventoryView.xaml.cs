using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for InventoryView.xaml
    /// </summary>
    public partial class InventoryView : Page
    {
        InventoryFileStorage storage = new InventoryFileStorage();
        public ObservableCollection<Inventory> InventoryList { get; set; }
        public InventoryView()
        {
            InitializeComponent();
            this.DataContext = this;
            InventoryList = new ObservableCollection<Inventory>();

            foreach (Inventory i in storage.GetAll())
            {
                InventoryList.Add(i);
            }
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            newFrame.Content = new NewItem(InventoryList, storage);
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            Inventory item = (Inventory)tableBinding.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("Morate izabrati stavku!");
            }
            else
            {
                newFrame.Content = new EditInventory(item, InventoryList, storage);
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            Inventory item = (Inventory)tableBinding.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("Izaberite stavku!");
            }
            else
            {
                storage.Delete(item.Id);
                InventoryList.Remove(item);
                tableBinding.Items.Refresh();

            }
        }
    }
}

