using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public  class VacationViewModel : ViewModelClass
    {
        private DateTime startDate;
        private DateTime endDate;
        private bool isSickLeave;
        public VacationView Window { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand ApplyCommand { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged();
            }
        }

        public bool IsSickLeave
        {
            get { return isSickLeave; }
            set
            {
                isSickLeave = value;
                OnPropertyChanged();
            }
        }

        public VacationViewModel(VacationView window)
        {
            this.Window = window;
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now;
            this.IsSickLeave = false;
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
            ApplyCommand = new RelayCommand(Executed_ApplyCommand,
              CanExecute_ApplyCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand,
             CanExecute_CancelCommand);
        }

        public bool CanExecute_CancelCommand(object obj)
        {
            return true;
        }
        public void Executed_CancelCommand(object obj)
        {
            this.Window.Close();
        }

        public bool CanExecute_ApplyCommand(object obj)
        {
            if(DateTime.Compare(StartDate, EndDate) > 0 || DateTime.Compare(StartDate, EndDate) == 0)
            {
                MessageBox.Show("Pogrešan odabir datuma.");
                return false;
            }
            else
                return true;
        }
        public void Executed_ApplyCommand(object obj)
        {
            this.Window.Close();
        }
        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }

    }
}
