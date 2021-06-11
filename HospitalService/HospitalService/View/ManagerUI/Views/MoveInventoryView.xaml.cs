using HospitalService.Storage;
using HospitalService.View.ManagerUI.ViewModels;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for MoveInventoryView.xaml
    /// </summary>
    public partial class MoveInventoryView : Page
    {
        MoveInventoryViewModel currentViewModel;

        public MoveInventoryView(Room room, Room sendHere, Inventory SelectedItem, bool demo)
        {
            InitializeComponent();
            currentViewModel = new MoveInventoryViewModel(newFrame, room, sendHere, SelectedItem, demo);
            this.DataContext = currentViewModel;
        }
    }
}
