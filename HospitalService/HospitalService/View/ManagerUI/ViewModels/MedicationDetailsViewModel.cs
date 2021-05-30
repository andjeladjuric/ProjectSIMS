using HospitalService.Repositories;
using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class MedicationDetailsViewModel : ViewModel
    {
        #region Fields
        private string format;
        public string Format
        {
            get { return format; }
            set
            {
                format = value;
                OnPropertyChanged();
            }
        }

        private MedicationType type;
        public MedicationType Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged();
            }
        }

        private string replacement;
        public string Replacement
        {
            get { return replacement; }
            set
            {
                replacement = value;
                OnPropertyChanged();
            }
        }

        private Medication selected;
        public Medication SelectedMedication
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> ingredients;
        public ObservableCollection<string> Ingredients
        {
            get { return ingredients; }
            set
            {
                ingredients = value;
                OnPropertyChanged();
            }
        }

        private Dictionary<string, int> currentIngredients;
        public Dictionary<string, int> CurrentIngredients
        {
            get { return currentIngredients; }
            set
            {
                currentIngredients = value;
                OnPropertyChanged();
            }
        }

        public string Comment { get; set; }
        public Frame Frame { get; set; }
        public Frame IngredientsFrame { get; set; }

        #endregion

        #region Commands
        public MyICommand EditCommand { get; set; }
        public MyICommand CancelCommand { get; set; }
        public MyICommand ResendCommand { get; set; }
        #endregion

        #region Actions
        private void OnEdit()
        {
            this.IngredientsFrame.NavigationService.Navigate(new IngredientsView(SelectedMedication.Ingredients, IngredientsFrame, Ingredients));
        }

        private void OnCancel()
        {
            SaveEditedIngredients();
            this.Frame.NavigationService.Navigate(new MedicationsView());
        }

        private void OnResend()
        {
            DoctorsRepository ds = new DoctorsRepository();
            Doctor drpetra = ds.GetOne("drpetra");
            MedicineValidationRequest validationRequest = new MedicineValidationRequest(SelectedMedication.Id, drpetra.Jmbg);
            MedicineValidationsRepository validation = new MedicineValidationsRepository();
            validation.GetAll().Add(validationRequest);
            validation.SerializeValidationRequests();
            SaveEditedIngredients();
            ChangeMedStatus();
        }

        private bool CanExecute()
        {
            return true;
        }

        private bool CanResend()
        {
            if (SelectedMedication.IsApproved.Equals(MedicineStatus.NotApproved))
                return true;

            return false;
        }
        #endregion

        #region Other Functions
        private void LoadIngredients()
        {
            Ingredients = new ObservableCollection<string>();

            foreach (var item in CurrentIngredients)
            {
                Ingredients.Add(item.Key + " " + item.Value + " mg");
            }
        }
        private void SaveEditedIngredients()
        {
            MedicationService service = new MedicationService();
            service.Edit(SelectedMedication.Id, Format, Type, SelectedMedication.Ingredients);
        }

        private void ChangeMedStatus()
        {
            MedicationService service = new MedicationService();
            foreach (Medication m in service.GetAll())
            {
                if (m.Id.Equals(SelectedMedication.Id))
                {
                    m.IsApproved = MedicineStatus.WaitingForApproval;
                    break;
                }
            }
            service.SerializeMedication();
        }
        #endregion

        #region Constructors
        public MedicationDetailsViewModel(Frame currentFrame, Frame ingredientsFrame, Medication medication)
        {
            this.Frame = currentFrame;
            this.IngredientsFrame = ingredientsFrame;
            this.SelectedMedication = medication;
            this.CurrentIngredients = SelectedMedication.Ingredients;
            this.Format = SelectedMedication.Format;
            this.Type = SelectedMedication.Type;
            this.Replacement = SelectedMedication.Replacement;
            this.Comment = SelectedMedication.DoctorsComment;
            LoadIngredients();
            CancelCommand = new MyICommand(OnCancel, CanExecute);
            EditCommand = new MyICommand(OnEdit, CanExecute);
            ResendCommand = new MyICommand(OnResend, CanResend);
        }
        #endregion
    }
}
