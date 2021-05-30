using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class ManagerWindowViewModel : ViewModel
    {
        #region Fields
        public static CancellationTokenSource cts = new CancellationTokenSource();
        public Manager Manager { get; set; }
        public Grid grid { get; set; }
        public Window Window { get; set; }
        public Frame Frame { get; set; }
        public Button Menu { get; set; }
        private bool demo;
        public bool DemoOn
        {
            get { return demo; }
            set
            {
                demo = value;
                OnPropertyChanged();
            }
        }
        private int selected;
        public int SelectedItem
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged();
            }
        }

        private bool isOpen;
        public bool IsPopupOpen
        {
            get { return isOpen; }
            set
            {
                isOpen = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public MyICommand NotificationCommand { get; set; }
        public MyICommand ProfileCommand { get; set; }
        public MyICommand LogoutCommand { get; set; }
        public MyICommand DemoCommand { get; set; }
        public MyICommand HelpCommand { get; set; }
        public MyICommand ChangePage { get; set; }
        public MyICommand StopDemo { get; set; }

        #endregion

        #region Actions
        private void OnLogout()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Window.Close();
        }

        private void OnStop()
        {
            cts.Cancel();
            MessageBox.Show("Demo zavrsen");
            DemoOn = false;
            this.Frame.NavigationService.Navigate(new RoomsView());
        }

        private void OnDemo()
        {
            DemoOn = true;
            IsPopupOpen = true;
            this.Frame.NavigationService.Navigate(new NewRoomView(DemoOn));
        }

        private void OnChange()
        {
            if(SelectedItem == 0)
            {
                this.Frame.NavigationService.Navigate(new RoomsView());
                CustomizeGridSize();
            }
            else if(SelectedItem == 1)
            {
                this.Frame.NavigationService.Navigate(new InventoryView());
                CustomizeGridSize();
            }
            else if(SelectedItem == 4)
            {
                this.Frame.NavigationService.Navigate(new MedicationsView());
                CustomizeGridSize();
            }
        }

        private void CustomizeGridSize()
        {
            if(grid.Width == 200)
                Menu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private bool CanExecute()
        {
            return true;
        }
        #endregion

        #region Constructors
        public ManagerWindowViewModel(Window currentWindow, Frame currentFrame, Button close, Grid menu, Manager currentUser)
        {
            this.Manager = currentUser;
            this.Window = currentWindow;
            this.Frame = currentFrame;
            this.Menu = close;
            this.grid = menu;
            this.DemoOn = false;

            RoomRenovationService service = new RoomRenovationService();
            service.CheckRenovationRequests();
            LogoutCommand = new MyICommand(OnLogout, CanExecute);
            ChangePage = new MyICommand(OnChange, CanExecute);
            DemoCommand = new MyICommand(OnDemo, CanExecute);
            StopDemo = new MyICommand(OnStop, CanExecute);
        }
        #endregion
    }
}
