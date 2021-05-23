using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace HospitalService.View.DoctorUI.ViewModel
{
    class AllergiesViewModel : ViewModelClass
    {
        public MedicalRecord MedicalRecord { get; set; }
        private ObservableCollection<MedicationIngredients> allergies;
        public RelayCommand AddCommand { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }
        public Frame Frame { get; set; }

        public ObservableCollection<MedicationIngredients> Allergies
        {
            get { return allergies; }
            set
            {
                allergies = value;
                OnPropertyChanged();
            }
        }

        public AllergiesViewModel(List<MedicationIngredients> ingredients, Frame frame, MedicalRecord record)
        {
            this.MedicalRecord = record;
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
            AddCommand = new RelayCommand(Executed_AddCommand,
              CanExecute_AddCommand);
            this.Frame = frame;
            this.Allergies = new ObservableCollection<MedicationIngredients>();
            ingredients.ForEach(Allergies.Add);
        }

        public bool CanExecute_AddCommand(object obj)
        {
            return true;
        }

        public void Executed_AddCommand(object obj)
        {
            this.Frame.NavigationService.Navigate(new AddAllergieView(Frame, MedicalRecord));
        }

        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }
    }
}
