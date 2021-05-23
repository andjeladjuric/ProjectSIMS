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
    public class MedicationsViewModel : ViewModel
    {
        #region Fields
        private ObservableCollection<Medication> medications;
        public ObservableCollection<Medication> Medications
        {
            get { return medications; }
            set
            {
                medications = value;
            }
        }

        private Medication medication;
        public Medication Medication
        {
            get { return medication; }
            set
            {
                medication = value;
                DeleteCommand.RaiseCanExecuteChanged();
                DetailsCommand.RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public Frame Frame { get; set; }
        #endregion

        #region Commands
        public MyICommand AddCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }
        public MyICommand DetailsCommand { get; set; }
        #endregion

        #region Actions
        private void OnAdd()
        {
            this.Frame.NavigationService.Navigate(new AddMedicationView());
        }

        private void OnDelete()
        {
            MedicationService medicationService = new MedicationService();
            medicationService.DeleteMedication(Medication.Id);
            Medications.Remove(Medication);
        }

        private void OnDetails()
        {
            this.Frame.NavigationService.Navigate(new MedicationDetailsView(Medication, Frame));
        }

        private bool CanExecute()
        {
            return Medication != null;
        }

        private bool CanNavigate()
        {
            return true;
        }
        #endregion

        #region Other Functions
        private void LoadMedications()
        {
            Medications = new ObservableCollection<Medication>();
            MedicationService medicationService = new MedicationService();

            foreach(Medication m in medicationService.GetAll())
            {
                Medications.Add(m);
            }
        }
        #endregion

        #region Constructors
        public MedicationsViewModel(Frame currentFrame)
        {
            this.Frame = currentFrame;
            LoadMedications();
            AddCommand = new MyICommand(OnAdd, CanNavigate);
            DeleteCommand = new MyICommand(OnDelete, CanExecute);
            DetailsCommand = new MyICommand(OnDetails, CanExecute);
        }
        #endregion
    }
}
