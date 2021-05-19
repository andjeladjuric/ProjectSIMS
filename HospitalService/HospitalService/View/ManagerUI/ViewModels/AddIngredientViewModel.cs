using HospitalService.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class AddIngredientViewModel :ViewModel
    {
        #region Fields
        private string ingredient;
        public string Ingredient
        {
            get { return ingredient; }
            set
            {
                ingredient = value;
                OnPropertyChanged();
            }
        }

        public Frame Frame { get; set; }
        public ObservableCollection<MedicationIngredients> AllIngredients { get; set; }
        #endregion

        #region Commands
        public MyICommand ConfirmCommand { get; set; }
        public MyICommand CancelCommand { get; set; }
        #endregion

        #region Actions
        private void OnConfirm()
        {
            MedicationService medicationService = new MedicationService();
            medicationService.AddIngredient(new MedicationIngredients(Ingredient));
            AllIngredients.Add(new MedicationIngredients(Ingredient));
            this.Frame.Content = null;
        }

        private void OnCancel()
        {
            this.Frame.Content = null;
        }

        private bool CanExecute()
        {
            return true;
        }
        #endregion

        #region Constructors
        public AddIngredientViewModel(Frame currentFrame, ObservableCollection<MedicationIngredients> i)
        {
            this.Frame = currentFrame;
            this.AllIngredients = i;
            ConfirmCommand = new MyICommand(OnConfirm, CanExecute);
            CancelCommand = new MyICommand(OnCancel, CanExecute);
        }
        #endregion
    }
}
