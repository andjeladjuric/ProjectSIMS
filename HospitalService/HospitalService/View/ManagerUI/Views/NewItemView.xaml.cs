using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
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
    /// Interaction logic for NewItem.xaml
    /// </summary>
    public partial class NewItemView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _itemId;
        public string ItemId
        {
            get
            {
                return _itemId;
            }
            set
            {
                if (value != _itemId)
                {
                    _itemId = value;
                    OnPropertyChanged("ItemId");
                }
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

        private string _quantity;
        public string ItemQuantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (value != _quantity)
                {
                    _quantity = value;
                    OnPropertyChanged("ItemQuantity");
                }
            }
        }

        public Inventory item { get; set; }
        InventoryFileStorage storage;
        ObservableCollection<Inventory> invList { get; set; }

        public NewItemView(ObservableCollection<Inventory> inv, InventoryFileStorage st)
        {
            InitializeComponent();
            this.DataContext = this;
            invList = inv;
            storage = st;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            item = new Inventory();
            item.EquipmentType = (Equipment)comboBox.SelectedIndex;
            item.Id = Int32.Parse(IDBox.Text);
            item.Name = NameBox.Text;
            item.Quantity = Int32.Parse(QuantityBox.Text);
            item.Supplier = suppBox.Text;

            storage.Save(item);
            invList.Add(item);
            newFrame.NavigationService.Navigate(new InventoryView());
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            IDBox.Visibility = Visibility.Hidden;
            NameBox.Visibility = Visibility.Hidden;
            QuantityBox.Visibility = Visibility.Hidden;
            suppBox.Visibility = Visibility.Hidden;
            newFrame.NavigationService.Navigate(new InventoryView());
        }
    }

    public class InventoryTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "static":
                    return "Statička";
                case "dynamic":
                    return "Dinamička";
            }

            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                if (value.Equals("Statička"))
                    return Equipment.Static;
                else if (value.Equals("Dinamička"))
                    return Equipment.Dynamic;
            }

            return null;
        }
    }
}
