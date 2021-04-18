using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for NewItem.xaml
    /// </summary>
    public partial class NewItem : Page
    {
        public Inventory item { get; set; }
        InventoryFileStorage storage;
        DataGrid bind;
        public static List<String> equipment = Enum.GetNames(typeof(Model.Equipment)).ToList();
        Regex checkName = new Regex(@"[A-Za-z]+([\s][A-Za-z]*[1-9]*)*$");
        Regex checkId = new Regex(@"[0-9]$");
        public NewItem(DataGrid dg, InventoryFileStorage st)
        {
            InitializeComponent();
            bind = dg;
            storage = st;
            comboBox.ItemsSource = equipment;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            foreach(Inventory inv in storage.GetAll())
            {
                if(inv.Id == int.Parse(IDBox.Text))
                {
                    MessageBox.Show("ID već postoji!");
                    return;
                }
            }

            item = new Inventory();
            item.EquipmentType = (Equipment)comboBox.SelectedIndex;
            item.Id = Int32.Parse(IDBox.Text);
            item.Name = NameBox.Text;
            item.Quantity = Int32.Parse(KolicinaBox.Text);

            storage.Save(item);
            bind.Items.Refresh();
            NavigationService.Navigate(new Page());
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page());
        }

        private void idTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!checkId.IsMatch(IDBox.Text))
            {
                label.Visibility = Visibility.Visible;
                save.IsEnabled = false;
            }
            else
            {
                label.Visibility = Visibility.Hidden;
                save.IsEnabled = true;
            }

        }

        private void nameTextChanged(object sender, TextChangedEventArgs e)
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
            if (!checkId.IsMatch(KolicinaBox.Text))
            {
                label3.Visibility = Visibility.Visible;
                save.IsEnabled = false;
            }
            else
            {
                label3.Visibility = Visibility.Hidden;
                save.IsEnabled = true;
            }
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
