using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class MessageViewModel : ViewModel
    {
        #region Fields
        public bool DemoOn { get; set; }
        public UserControl PopUp { get; set; }

        private string mes;
        public static string Message { get; set; }
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

        private bool CanNavigate()
        {
            return true;
        }
        #endregion

        public void SetMessage(string text)
        {
            Message = text;
        }

        #region Constructors
        public MessageViewModel()
        {
            this.DemoOn = false;
            Message = "";
            OkayCommand = new MyICommand(OnOkay, CanNavigate);
        }
        #endregion
    }
}
