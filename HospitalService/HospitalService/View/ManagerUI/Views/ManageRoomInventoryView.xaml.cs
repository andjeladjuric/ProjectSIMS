using HospitalService.Storage;
using HospitalService.View.ManagerUI.ViewModels;
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

namespace HospitalService.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for ManageRoomInventory.xaml
    /// </summary>
    public partial class ManageRoomInventoryView : Page
    {
        ManageRoomInventoryViewModel currentViewModel;

        public ManageRoomInventoryView(Room room)
        {
            InitializeComponent();
            currentViewModel = new ManageRoomInventoryViewModel(newFrame, room);
            this.DataContext = currentViewModel;
        }
    }
}
