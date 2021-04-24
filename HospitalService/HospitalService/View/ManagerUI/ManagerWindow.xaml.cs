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

namespace HospitalService.View.ManagerUI
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        private Manager manager;
        private InventoryFileStorage invStorage = new InventoryFileStorage();
        public ManagerWindow(Manager m)
        {
            InitializeComponent();
            manager = m;
            invStorage.AnalyzeRequests();
        }

        private void openButtonClick(object sender, RoutedEventArgs e)
        {
            OpenMenuButton.Visibility = Visibility.Collapsed;
            CloseMenuButton.Visibility = Visibility.Visible;
        }

        private void closeButtonClick(object sender, RoutedEventArgs e)
        {
            OpenMenuButton.Visibility = Visibility.Visible;
            CloseMenuButton.Visibility = Visibility.Collapsed;
        }

        private void Notification_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewMenu != null && MainFrame != null)
            {
                if (RoomsPage.IsSelected)
                {
                    if (GridMenu.Width == 200)
                    {
                        CloseMenuButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    }

                    MainFrame.Content = new RoomsView();
                }
                else if (InventoryPage.IsSelected)
                {
                    if (GridMenu.Width == 200)
                    {
                        CloseMenuButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    }

                    MainFrame.Content = new InventoryView();
                }
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
