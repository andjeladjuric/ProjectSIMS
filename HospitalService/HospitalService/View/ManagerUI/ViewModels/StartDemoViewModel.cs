using HospitalService.View.ManagerUI.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class StartDemoViewModel : ViewModel
    {
        #region Fields
        public bool DemoOn { get; set; }
        public UserControl PopUp { get; set; }
        #endregion

        #region Commands
        public MyICommand OkayCommand { get; set; }
        public MyICommand CancelCommand { get; set; }
        #endregion

        #region Actions
        private void OnOkay()
        {
            DemoOn = true;
            PopUp.Visibility = Visibility.Hidden;
        }

        private void OnCancel()
        {
            DemoOn = false;
            PopUp.Visibility = Visibility.Hidden;
        }

        private bool CanNavigate()
        {
            return true;
        }
        #endregion

        #region Constructors
        public StartDemoViewModel(UserControl control)
        {
            this.DemoOn = false;
            this.PopUp = control;
            OkayCommand = new MyICommand(OnOkay, CanNavigate);
            CancelCommand = new MyICommand(OnCancel, CanNavigate);
        }
        #endregion
    }
}
