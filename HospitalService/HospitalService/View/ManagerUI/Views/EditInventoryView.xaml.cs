using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace HospitalService.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for EditInventory.xaml
    /// </summary>
    public partial class EditInventoryView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _itemName;
        public string ItemName
        {
            get
            {
                return _itemName;
            }
            set
            {
                if (value != _itemName)
                {
                    _itemName = value;
                    OnPropertyChanged("ItemName");
                }
            }
        }

        private string _enteredQuantity;
        public string EnteredQuantity
        {
            get
            {
                return _enteredQuantity;
            }
            set
            {
                if (value != _enteredQuantity)
                {
                    _enteredQuantity = value;
                    OnPropertyChanged("EnteredQuantity");
                }
            }
        }

        private string _supplier;
        public string Supplier
        {
            get
            {
                return _supplier;
            }
            set
            {
                if (value != _supplier)
                {
                    _supplier = value;
                    OnPropertyChanged("Supplier");
                }
            }
        }
        public Inventory item { get; set; }
        InventoryFileStorage storage;
        public ObservableCollection<Inventory> invList { get; set; }
        public EditInventoryView(Inventory i, ObservableCollection<Inventory> inv)
        {
            InitializeComponent();
            item = i;
            invList = inv;
            this.DataContext = this;

            EnteredQuantity = item.Quantity.ToString();
            ItemName = item.Name;
            Supplier = item.Supplier;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            Equipment tip = (Equipment)comboBox.SelectedIndex;
            int id = Int32.Parse(IDBox.Text);
            string neki = NameBox.Text;
            int kol = Int32.Parse(KolicinaBox.Text);
            string supplier = suppBox.Text;

            storage.Edit(id, neki, tip, kol, supplier);
            newFrame.NavigationService.Navigate(new InventoryView());
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            NameBox.Visibility = Visibility.Hidden;
            KolicinaBox.Visibility = Visibility.Hidden;
            suppBox.Visibility = Visibility.Hidden;
            newFrame.NavigationService.Navigate(new InventoryView());
        }
    }
}
