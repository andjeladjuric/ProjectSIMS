using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Inventory> invList { get; set; }
        public EditInventory(Inventory i, ObservableCollection<Inventory> inv, InventoryFileStorage st)
        {
            InitializeComponent();
            item = i;
            invList = inv;
            storage = st;
            this.DataContext = this;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            Equipment tip = (Equipment)comboBox.SelectedIndex;
            int id = Int32.Parse(IDBox.Text);
            string neki = NameBox.Text;
            int kol = Int32.Parse(KolicinaBox.Text);

            storage.Edit(id, neki, tip, kol);
            NavigationService.Navigate(new Page());
            //bind.Items.Refresh();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page());
        }
    }
}
