using HospitalService.Service;
using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HospitalService.View.DoctorUI.ViewModel
{
    class AboutMedicationViewModel : ViewModelClass
    {
        public Medication Medication { get; set; }
        public ObservableCollection<Medication> Replacements { get; set; }
        public ObservableCollection<string> Ingredients { get; set; }
        public DoctorWindowViewModel ParentWindow { get; set; }
        public AboutMedicationView ThisWindow { get; set; }
        public RelayCommand ApplyCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }
        private Medication replacement;

        public Medication Replacement
        {
            get { return replacement; }
            set
            {
                replacement = value;
                OnPropertyChanged();
            }
        }

        public AboutMedicationViewModel(Medication selectedMedication, AboutMedicationView window, DoctorWindowViewModel parent)
        {
            this.Medication = selectedMedication;
            this.ThisWindow = window;
            this.ParentWindow = parent;
            this.Replacement = new MedicationService().GetOne(selectedMedication.Replacement); 
            this.Replacements = new ObservableCollection<Medication>();
            new MedicationService().GetAll().ForEach(Replacements.Add); 
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
                return true;
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
            Medication.Replacement = Replacement.Id;
            new MedicationService().UpdateMedication(Medication); 
            ParentWindow.Refresh();
            ThisWindow.Close();
        }

        private void Executed_KeyDownCommandWithKey(object obj)
        {
            
        }
    }
}
