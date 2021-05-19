using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class ManagerWindowViewModel
    {
        #region Fields
        public Manager Manager { get; set; }
        public Window Window { get; set; }
        #endregion

        #region Commands
        public MyICommand NotificationCommand { get; set; }
        public MyICommand ProfileCommand { get; set; }
        public MyICommand LogoutCommand { get; set; }
        public MyICommand HelpCommand { get; set; }
        #endregion

        #region Actions
        private void OnLogout()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Window.Close();
        }

        private bool CanExecute()
        {
            return true;
        }
        #endregion

        #region Constructors
        public ManagerWindowViewModel(Window currentWindow, Manager currentUser)
        {
            this.Manager = currentUser;
            this.Window = currentWindow;
            LogoutCommand = new MyICommand(OnLogout, CanExecute);
        }
        #endregion
    }
}
