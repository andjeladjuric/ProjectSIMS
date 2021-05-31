using HospitalService.Repositories;
using HospitalService.Service;
using HospitalService.Storage;
using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Views;
using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace HospitalService.View.DoctorUI.ViewModel
{
    class AddAllergieViewModel : ViewModelClass
    {
        public Frame Frame { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
        public ObservableCollection<MedicationIngredients> Ingredients { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand ApplyCommand { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }
        private MedicationIngredients selectedIngredient;

        public MedicationIngredients SelectedIngredient
        {
            get { return selectedIngredient; }
            set
            {
                selectedIngredient = value;
                OnPropertyChanged();
            }
        }

        public AddAllergieViewModel(Frame frame, MedicalRecord medicalRecord)
        {
            this.Frame = frame;
            this.MedicalRecord = medicalRecord;
            this.Ingredients = new ObservableCollection<MedicationIngredients>();
            List<MedicationIngredients> allergens = new IngredientsService().GetNewAllergens(medicalRecord.Allergies);
            allergens.ForEach(Ingredients.Add); 
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
            this.Frame.NavigationService.Navigate(new AllergiesView(MedicalRecord.Allergies, Frame, MedicalRecord));
        }
        public bool CanExecute_ApplyCommand(object obj)
        {
            if (SelectedIngredient == null)
            {
                MessageBox.Show("Morate izabrati sastojak.");
                return false;
            }
            return true;
        }

        public void Executed_ApplyCommand(object obj)
        {
            this.MedicalRecord.Allergies.Add(SelectedIngredient);
            new MedicalRecordService().UpdateRecord(MedicalRecord); 
            this.Frame.NavigationService.Navigate(new AllergiesView(MedicalRecord.Allergies, Frame, MedicalRecord));
        }

        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }
    }
}
