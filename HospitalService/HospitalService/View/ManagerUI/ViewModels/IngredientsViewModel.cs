using HospitalService.Repositories;
using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class IngredientsViewModel : ViewModel
    {
        #region Fields
        private ObservableCollection<string> previousCollection; 
        public ObservableCollection<string> PreviouseCollection
        { get { return previousCollection; } set { previousCollection = value; OnPropertyChanged(); } }
        public CollectionView currentView { get; set; }
        public CollectionView allIngredientsView { get; set; }
        private ObservableCollection<MedicationIngredients> ingredients;
        public ObservableCollection<MedicationIngredients> Ingredients
        {
            get { return ingredients; }
            set
            {
                ingredients = value;
                OnPropertyChanged();
            }
        }
        private MedicationIngredients selectedIngredient;
        public MedicationIngredients SelectedIngredient
        {
            get { return selectedIngredient; }
            set
            {
                selectedIngredient = value;
                DeleteIngredientCommand.RaiseCanExecuteChanged();
                AddIngredientToMedCommand.RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        private string quantity;
        public string Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged();
            }
        }

        public Dictionary<string, int> IngredientsForMed { get; set; }
        private ObservableCollection<string> currentIngredients;
        public ObservableCollection<string> CurrentIngredients
        {
            get { return currentIngredients; }
            set
            {
                currentIngredients = value;
                OnPropertyChanged();
            }
        }
        public Frame Frame { get; set; }
        public Frame IngFrame { get; set; }
        private string selected;
        public string SelectedIngAndQuantity
        {
            get { return selected; }
            set
            {
                selected = value;
                EditIngredientCommand.RaiseCanExecuteChanged();
                DeleteIngredientFromMedCommand.RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public MyICommand AddIngredientToMedCommand { get; set; }
        public MyICommand EditIngredientCommand { get; set; }
        public MyICommand DeleteIngredientFromMedCommand { get; set; }
        public MyICommand CancelCommand { get; set; }
        public MyICommand AddNewIngredientCommand { get; set; }
        public MyICommand DeleteIngredientCommand { get; set; }
        public MyICommand SelectionChangedCommand { get; set; }
        public MyICommand IngredientChangedCommand { get; set; }
        #endregion

        #region Actions
        private void OnAddToMed()
        {
            int enteredQuantity = Int32.Parse(Quantity);
            IngredientsForMed.Add(SelectedIngredient.IngredientName, enteredQuantity);
            AddIngredientToMedication();
            currentView.Refresh();
            Quantity = "";
        }

        private void OnAddNewIngredient()
        {
            this.IngFrame.NavigationService.Navigate(new AddIngredientView(Ingredients, IngFrame));
        }

        private void OnDeleteIngredient()
        {
            MedicationService service = new MedicationService();
            if (IngredientsForMed.ContainsKey(SelectedIngredient.IngredientName))
            {
                IngredientsForMed.Remove(SelectedIngredient.IngredientName);
                AddIngredientToMedication();
            }
            DeleteIngredientsFromMedication(SelectedIngredient.IngredientName);
            service.DeleteIngredient(SelectedIngredient.IngredientName);
            Ingredients.Remove(SelectedIngredient);
            allIngredientsView.Refresh();

        }

        private void OnRemoveIngredientFromMed()
        {
            string[] parts = SelectedIngAndQuantity.Split(" ");
            IngredientsForMed.Remove(parts[0]);
            CurrentIngredients.Remove(SelectedIngAndQuantity);
            //AddIngredientToMedication();
            DeleteIngredientsFromMedication(parts[0]);
            Quantity = "";
            currentView.Refresh();
        }

        private void OnEdit()
        {
            string[] parts = SelectedIngAndQuantity.Split(" ");
            IngredientsForMed[parts[0]] = Int32.Parse(Quantity);
            AddIngredientToMedication();
            Quantity = "";
        }

        private void OnCancel()
        {
            this.PreviouseCollection.Clear();
            foreach (string ing in CurrentIngredients)
            {
                this.previousCollection.Add(ing);
            }

            this.Frame.Content = null;
        }

        public bool CanExecute()
        {
            return SelectedIngredient != null;
        }

        public bool CanRemoveOrEdit()
        {
            return SelectedIngAndQuantity != null;
        }

        public bool CanAddOrCancel()
        {
            return true;
        }

        private void OnSelectionChanged()
        {
            if (SelectedIngAndQuantity != null)
            {
                string[] parts = SelectedIngAndQuantity.Split(" ");
                Quantity = parts[1];
            }
        }

        private void OnIngredientChanged()
        {
            if (SelectedIngredient != null)
            {
                SelectedIngAndQuantity = null;
                Quantity = "";
            }
        }

        #endregion

        #region Other Functions
        private void AddIngredientToMedication()
        {
            CurrentIngredients = new ObservableCollection<string>();
            foreach (var ingredient in IngredientsForMed)
            {
                CurrentIngredients.Add(ingredient.Key + " " + ingredient.Value + " mg");
            }
        }

        private void DeleteIngredientsFromMedication(string ingName)
        {
            MedicationService service = new MedicationService();
            Medication med;
            for (int i = 0; i < service.GetAll().Count; i++)
            {
                med = service.GetAll()[i];
                if (med.Ingredients.ContainsKey(ingName))
                {
                    med.Ingredients.Remove(ingName);
                    service.SerializeMedication();
                }
            }
        }
         private void LoadAllIngredients()
        {
            Ingredients = new ObservableCollection<MedicationIngredients>();
            IngredientsRepository repo = new IngredientsRepository();

            foreach(MedicationIngredients m in repo.GetAll())
            {
                Ingredients.Add(m);
            }
        }
        
        #endregion

        #region Constructors
        public IngredientsViewModel(Frame currentFrame, Frame quantityFrame, Dictionary<string, int> MedIngredients, ObservableCollection<string> view)
        {
            this.Frame = currentFrame;
            this.IngFrame = quantityFrame;
            this.IngredientsForMed = MedIngredients;
            this.previousCollection = view;
            LoadAllIngredients();
            AddIngredientToMedication();
            this.currentView = (CollectionView)CollectionViewSource.GetDefaultView(CurrentIngredients);
            this.allIngredientsView = (CollectionView)CollectionViewSource.GetDefaultView(Ingredients);
            AddIngredientToMedCommand = new MyICommand(OnAddToMed, CanAddOrCancel);
            AddNewIngredientCommand = new MyICommand(OnAddNewIngredient, CanAddOrCancel);
            DeleteIngredientCommand = new MyICommand(OnDeleteIngredient, CanExecute);
            DeleteIngredientFromMedCommand = new MyICommand(OnRemoveIngredientFromMed, CanRemoveOrEdit);
            EditIngredientCommand = new MyICommand(OnEdit, CanRemoveOrEdit);
            CancelCommand = new MyICommand(OnCancel, CanAddOrCancel);
            SelectionChangedCommand = new MyICommand(OnSelectionChanged, CanAddOrCancel);
            IngredientChangedCommand = new MyICommand(OnIngredientChanged, CanAddOrCancel);
        }
        #endregion
    }
}
