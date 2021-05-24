using HospitalService.View.ManagerUI.ViewModels;
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
    /// Interaction logic for EditInventoryView.xaml
    /// </summary>
    public partial class EditInventoryView : Page
    {
        EditInventoryViewModel currentViewModel;

        public EditInventoryView(Inventory selectedItem, ObservableCollection<Inventory> inventories)
        {
            InitializeComponent();
            currentViewModel = new EditInventoryViewModel(newFrame, inventories, selectedItem);
            this.DataContext = currentViewModel;
        }
    }
}
