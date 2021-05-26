using HospitalService.View.ManagerUI.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class StartDemoViewModel : ViewModel
    {
        #region Fields
        public Frame Frame { get; set; }
        public bool DemoOn { get; set; }
        #endregion

        #region Commands
        public MyICommand OkayCommand { get; set; }
        public MyICommand CancelCommand { get; set; }
        #endregion

        #region Actions
        private void OnOkay()
        {
            DemoOn = true;
            this.Frame.NavigationService.Navigate(new NewRoomView(DemoOn));
        }

        private void OnCancel()
        {
            DemoOn = false;
            this.Frame.NavigationService.GoBack();
        }

        private bool CanNavigate()
        {
            return true;
        }
        #endregion

        #region Constructors
        public StartDemoViewModel(Frame newFrame)
        {
            this.Frame = newFrame;
            this.DemoOn = false;
            OkayCommand = new MyICommand(OnOkay, CanNavigate);
            CancelCommand = new MyICommand(OnCancel, CanNavigate);
        }
        #endregion
    }
}
