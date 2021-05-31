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
        private string jmbg;
        public string Jmbg
        {
            get { return jmbg; }
            set
            {
                jmbg = value;
                OnPropertyChanged();
            }
        }

        private DateTime? birthday;
        public DateTime? Birthday
        {
            get { return birthday; }
            set
            {
                birthday = value;
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
        public MyICommand EditProfile { get; set; }
        #endregion

        #region Actions
        #endregion

        #region Constructors
        public ProfileViewModel(Frame currentFrame, Manager currentManager)
        {
            this.Manager = currentManager;
            this.Frame = currentFrame;
            this.Jmbg = Manager.Jmbg;
            this.Birthday = Manager.DateOfBirth;
            this.Address = Manager.Address;
            this.Phone = Manager.Phone;
            this.Mail = Manager.Email;

        }
        #endregion
    }
}
