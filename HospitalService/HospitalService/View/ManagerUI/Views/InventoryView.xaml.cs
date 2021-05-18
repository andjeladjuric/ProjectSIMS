using HospitalService.Storage;
using HospitalService.View.ManagerUI.ViewModels;
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

namespace HospitalService.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for InventoryView.xaml
    /// </summary>
    public partial class InventoryView : Page
    {
        InventoryViewModel currentViewModel;
        public InventoryView()
        {
            InitializeComponent();
            currentViewModel = new InventoryViewModel(newFrame);
            this.DataContext = currentViewModel;
        }

        /*private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InventoryType.SelectedIndex != -1)
            {
                ObservableCollection<Inventory> filtered = new ObservableCollection<Inventory>();

                if (InventoryType.SelectedIndex == 0)
                {
                    filtered = InventoryList;
                }
                else if (InventoryType.SelectedIndex == 1)
                {
                    foreach (Inventory i in InventoryList)
                    {
                        if (i.EquipmentType.Equals(Equipment.Static))
                            filtered.Add(i);
                    }
                }
                else
                {
                    foreach (Inventory i in InventoryList)
                    {
                        if (i.EquipmentType.Equals(Equipment.Dynamic))
                            filtered.Add(i);
                    }
                }

                tableBinding.ItemsSource = filtered;
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string id = searchId.Text.ToLower().Trim();
            string name = searchName.Text.ToLower().Trim();
            string supplier = searchSupplier.Text.ToLower().Trim();
            RoomInventoryStorage roomInv = new RoomInventoryStorage();
            RoomFileStorage rooms = new RoomFileStorage();
            List<Inventory> filtered = new List<Inventory>();

            if (id != "" || name != "" || supplier != "")
            {
                foreach (Inventory i in InventoryList)
                {
                    if (i.Name.ToLower().Contains(name) && i.Id.ToString().Contains(id)
                        && i.Supplier.ToLower().Contains(supplier))
                    {
                        filtered.Add(i);
                    }
                }
                tableBinding.ItemsSource = filtered;
            }
            else
            {
                tableBinding.ItemsSource = InventoryList;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Popup.IsPopupOpen = false;
            searchId.Text = "";
            searchName.Text = "";
            searchSupplier.Text = "";
        }*/
    }
}

