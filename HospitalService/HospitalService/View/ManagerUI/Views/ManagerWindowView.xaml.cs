using HospitalService.Storage;
using HospitalService.View.ManagerUI.ViewModels;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HospitalService.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindowView : Window
    {
        ManagerWindowViewModel currentViewModel;
        public ManagerWindowView(Manager m)
        {
            InitializeComponent();
            currentViewModel = new ManagerWindowViewModel(this, MainFrame, CloseMenuButton, m);
            this.DataContext = currentViewModel;
        }

        //private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (ListViewMenu != null && MainFrame != null)
        //    {
        //        if (RoomsPage.IsSelected)
        //        {
        //            if (GridMenu.Width == 200)
        //            {
        //                CloseMenuButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //            }

        //            MainFrame.Content = new RoomsView();
        //        }
        //        else if (InventoryPage.IsSelected)
        //        {
        //            if (GridMenu.Width == 200)
        //            {
        //                CloseMenuButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //            }

        //            MainFrame.Content = new InventoryView();
        //        }
        //        else if (MedicationsPage.IsSelected)
        //        {
        //            if (GridMenu.Width == 200)
        //            {
        //                CloseMenuButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //            }

        //            MainFrame.Content = new MedicationsView();
        //        }
        //    }
        //}
    }
}
