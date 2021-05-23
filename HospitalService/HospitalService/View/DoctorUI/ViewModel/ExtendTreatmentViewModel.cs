using HospitalService.Model;
using HospitalService.View.DoctorUI.Commands;
using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class ExtendTreatmentViewModel : ViewModelClass
    {
        public MedicalRecordViewModel ParentWindow { get; set; }
        public Frame Frame { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
        public HospitalTreatment HospitalTreatent { get; set; }
        private DateTime selectedDate;
        public RelayCommand ApplyCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged();
            }

        }

        public ExtendTreatmentViewModel(MedicalRecordViewModel parent)
        {
            this.ParentWindow = parent;
            this.Frame = parent.EditTreatmentFrame;
            this.MedicalRecord = parent.MedicalRecord;
            this.HospitalTreatent = parent.SelectedTreatment;
            this.SelectedDate = HospitalTreatent.EndTime;
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

        public bool CanExecute_ApplyCommand(object obj)
        {
               if(DateTime.Compare(SelectedDate, HospitalTreatent.StartDate) < 0)
            {
                MessageBox.Show("Nije moguće da kraj bude prije početka.");
                return false;
            }
               return true;
        }

        public void Executed_ApplyCommand(object obj)
        {
            this.HospitalTreatent.EndTime = SelectedDate;
            this.MedicalRecord.EditTreatment(HospitalTreatent);
            new MedicalRecordStorage().Edit(MedicalRecord);
            this.ParentWindow.Refresh();
            this.Frame.Content = null;
        }

        public void Executed_CancelCommand(object obj)
        {
            this.Frame.Content = null;
        }

        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }
}
}
