using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class ProfileViewModel : ViewModel
    {
        #region Fields
        public Frame Frame { get; set; }
        private Manager manager;
        public Manager Manager
        {
            get { return manager;}
            set
            {
                manager = value;
                OnPropertyChanged();
            }
        }
        
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public MyICommand EditProfile { get; set; }
        #endregion

        #region Actions
        private void OnEdit()
        {
            this.Frame.NavigationService.Navigate(new EditProfileView(manager));
        }
        #endregion

        #region Constructors
        public ProfileViewModel(Frame currentFrame, Manager currentManager)
        {
            this.Manager = currentManager;
            this.Frame = currentFrame;
            this.Name = Manager.Name + " " + Manager.Surname;
            EditProfile = new MyICommand(OnEdit);
        }
        #endregion
    }
}
