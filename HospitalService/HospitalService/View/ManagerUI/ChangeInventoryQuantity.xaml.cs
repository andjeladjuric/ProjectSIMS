using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for ChangeInventoryQuantity.xaml
    /// </summary>
    public partial class ChangeInventoryQuantity : Page, INotifyPropertyChanged
    {
        public Room room;
        InventoryFileStorage invStorage = new InventoryFileStorage();
        RoomFileStorage roomStorage;
        public ObservableCollection<Inventory> roomInventory { get; set; }

        private Inventory _i;
        public Inventory selectedInv
        {
            get { return _i; }
            set
            {
                _i = value;
                OnPropertyChanged("selectedInv");
            }
        }

        private string _enteredQuantity;
        public string EnteredQuantity
        {
            get { return _enteredQuantity; }
            set
            {
                _enteredQuantity = value;
                OnPropertyChanged("EnteredQuantity");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ChangeInventoryQuantity(Room r, ObservableCollection<Inventory> inv)
        {
            InitializeComponent();
            this.DataContext = this;

            room = r;
            roomInventory = inv;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            quantityBox.Visibility = Visibility.Hidden;
            newFrame.Content = new ManageRoomInventory(room);
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            int quantity = Int32.Parse(quantityBox.Text);
        }
    }
}
