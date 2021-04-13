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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalService.View.ManagerUI
{
    /// <summary>
    /// Interaction logic for InventoryView.xaml
    /// </summary>
    public partial class InventoryView : Page
    {
        InventoryFileStorage storage;
        public InventoryView()
        {
            InitializeComponent();

            storage = new InventoryFileStorage();
            List<Inventory> inventoryList = storage.GetAll();
            this.DataContext = this;
            tableBinding.ItemsSource = inventoryList;
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            newFrame.Content = new NewItem(tableBinding, storage);
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
                newFrame.Content = new EditInventory(item, tableBinding, storage);
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
                tableBinding.Items.Refresh();

            }
        }
    }
}

