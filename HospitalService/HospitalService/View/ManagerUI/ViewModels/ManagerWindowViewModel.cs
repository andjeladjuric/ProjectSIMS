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
        private Manager manager;
        public Manager Manager
        {
            get { return manager; }
            set
            {
                manager = value;
                OnPropertyChanged();
            }
        }
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

        private void OnProfile()
        {
            this.Frame.NavigationService.Navigate(new ProfileView(this.Manager));
        }

        private void OnDemo()
        {
            DemoOn = true;
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
            else if (SelectedItem == 2)
            {
                this.Frame.NavigationService.Navigate(new DoctorsView());
                CustomizeGridSize();
            }
            else if(SelectedItem == 3)
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
            /*view*/
            this.Manager = currentUser;
            this.Window = currentWindow;
            this.Frame = currentFrame;
            this.Menu = close;
            this.grid = menu;
            this.DemoOn = false;

            /*commands*/
            LogoutCommand = new MyICommand(OnLogout, CanExecute);
            ProfileCommand = new MyICommand(OnProfile, CanExecute);
            ChangePage = new MyICommand(OnChange, CanExecute);
            DemoCommand = new MyICommand(OnDemo, CanExecute);

            /*check requests*/
            RoomInventoryService service = new RoomInventoryService();
            service.CheckRequests();
            RoomRenovationService renovationService = new RoomRenovationService();
            renovationService.CheckRenovationRequests();
        }
        #endregion
    }
}
