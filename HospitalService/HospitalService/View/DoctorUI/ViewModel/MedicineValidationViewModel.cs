using HospitalService.Service;
using HospitalService.Storage;
using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Validation;
using HospitalService.View.DoctorUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace HospitalService.View.DoctorUI.ViewModel
{
    class MedicineValidationViewModel : ValidationBase
    {
        private bool isApproved;
        private string doctorsComment;
        public Medication Medication { get; set; }
        public DoctorWindowViewModel ParentWindow { get; set; }
        public MedicineValidationView ThisWindow { get; set; }
        public ObservableCollection<string> Ingredients { get; set; }
        public RelayCommand ApplyCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }

        public bool IsApproved
        {
            get { return isApproved; }
            set
            {
                isApproved = value;
                OnPropertyChanged("IsApproved");
            }
        }

        public string DoctorsComment
        {
            get { return doctorsComment; }
            set
            {
                doctorsComment = value;
                OnPropertyChanged("DoctorsComment");
            }
        }

        public MedicineValidationViewModel(Medication selectedMedication, MedicineValidationView window, DoctorWindowViewModel parent)
        {
            this.Medication = selectedMedication;
            this.IsApproved = false;
            this.ThisWindow = window;
            this.ParentWindow = parent;
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
            ApplyCommand = new RelayCommand(Executed_ApplyCommand,
              CanExecute_ApplyCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand,
             CanExecute_CancelCommand);
            this.Ingredients = new ObservableCollection<string>();
            foreach (var item in Medication.Ingredients)
            {
                Ingredients.Add(item.Key + " " + item.Value + " mg");
            }
        }

        public bool CanExecute_ApplyCommand(object obj)
        {
            this.Validate();
            if (this.IsValid)
            {
                return true;
            }
            else
                return false;
        }

        public void Executed_CancelCommand(object obj)
        {
            this.ThisWindow.Close();
        }

        public bool CanExecute_CancelCommand(object obj)
        {
                return true;
        }

        public void Executed_ApplyCommand(object obj)
        {
            Medication.DoctorsComment = DoctorsComment;
            Medication.IsApproved = IsApproved ? MedicineStatus.Approved : MedicineStatus.NotApproved;
            new MedicationService().UpdateMedication(Medication); 
            new MedicineValidationService().Delete(Medication.Id); 
            ParentWindow.Refresh();
            ThisWindow.Close();
        }

        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }

        protected override void ValidateSelf()
        {
            if (this.isApproved == false || this.DoctorsComment == "")
                this.ValidationErrors["DoctorsComment"] = "Navedite razlog neodobravanja";
           
        }
    }
}
