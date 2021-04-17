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
    /// Interaction logic for EditInventory.xaml
    /// </summary>
    public partial class EditInventory : Page
    {
        public Inventory item { get; set; }
        InventoryFileStorage storage;
        DataGrid bind;
        public static List<String> equipment = Enum.GetNames(typeof(Equipment)).ToList();
        Regex checkName = new Regex(@"[A-Za-z]+([\s][A-Za-z]*[1-9]*)*$");
        Regex checkQuantity = new Regex(@"[0-9]$");
        public EditInventory(Inventory i, DataGrid dg, InventoryFileStorage st)
        {
            InitializeComponent();
            item = i;
            bind = dg;
            storage = st;
            EditGrid.DataContext = this;
            comboBox.ItemsSource = equipment;
            comboBox.SelectedItem = item.EquipmentType.ToString();
            IDBox.Text = item.Id.ToString();
            NameBox.Text = item.Name;
            KolicinaBox.Text = item.Quantity.ToString();

            tableBinding.ItemsSource = storage.GetAll();
            tableBinding.SelectedItem = storage.getOne(item.Id);
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            Equipment tip = (Equipment)comboBox.SelectedIndex;
            int id = Int32.Parse(IDBox.Text);
            string neki = NameBox.Text;
            int kol = Int32.Parse(KolicinaBox.Text);

            storage.Edit(id, neki, tip, kol);
            NavigationService.Navigate(new Page());
            bind.Items.Refresh();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page());
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!checkName.IsMatch(NameBox.Text))
            {
                label2.Visibility = Visibility.Visible;
                save.IsEnabled = false;
            }
            else
            {
                label2.Visibility = Visibility.Hidden;
            }
        }

        private void KolicinaBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!checkQuantity.IsMatch(KolicinaBox.Text))
            {
                label3.Visibility = Visibility.Visible;
                save.IsEnabled = false;
            }
            else
            {
                label3.Visibility = Visibility.Hidden;
                if (label2.Visibility == Visibility.Hidden)
                    save.IsEnabled = true;
            }
        }
    }
}
