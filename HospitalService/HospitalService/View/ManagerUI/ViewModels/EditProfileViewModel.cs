using HospitalService.Repositories;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class EditProfileViewModel : ViewModel
    {
        #region Fields
        public Frame Frame { get; set; }
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

        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged();
            }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged();
            }
        }

        private string mail;
        public string Mail
        {
            get { return mail; }
            set
            {
                mail = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public MyICommand ConfirmCommand { get; set; }
        public MyICommand CancelCommand { get; set; }
        #endregion

        #region Actions
        private void OnCancel()
        {
            this.Frame.NavigationService.Navigate(new ProfileView(Manager));
        }

        private void OnEdit()
        {
            ManagersRepository repo = new ManagersRepository();
            this.Manager.Address = Address;
            this.Manager.Phone = Phone;
            this.Manager.Email = Mail;
            repo.EditManager(Manager);
            this.Frame.NavigationService.Navigate(new ProfileView(Manager));
        }
        #endregion

        #region Constructors
        public EditProfileViewModel(Frame currentFrame, Manager user)
        {
            this.Frame = currentFrame;
            this.Manager = user;
            this.Name = Manager.Name + " " + Manager.Surname;
            this.Address = user.Address;
            this.Phone = user.Phone;
            this.Mail = user.Email;

            ConfirmCommand = new MyICommand(OnEdit);
            CancelCommand = new MyICommand(OnCancel);
        }
        #endregion
    }
}
