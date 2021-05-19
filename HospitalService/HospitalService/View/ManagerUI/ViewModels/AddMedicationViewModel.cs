﻿using HospitalService.Repositories;
using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class AddMedicationViewModel : ViewModel
    {
        #region Fields
        private string medicationId;
        private string medicationName;
        private string medicationFormat;
        private MedicationType type;
        private MedicineStatus status;
        private string doctor;
        #endregion

        #region Properties
        public string MedicationId
        {
            get { return medicationId; }
            set
            {
                medicationId = value;
                OnPropertyChanged();
            }
        }
        public string MedicationName
        {
            get { return medicationName; }
            set
            {
                medicationName = value;
                OnPropertyChanged();
            }
        }
        public string MedicationFormat
        {
            get { return medicationFormat; }
            set
            {
                medicationFormat = value;
                OnPropertyChanged();
            }
        }

        public MedicationType Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged();
            }
        }

        public MedicineStatus Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }
        
        public string Doctor
        {
            get { return doctor; }
            set { doctor = value; OnPropertyChanged(); }
        }

        public List<String> Doctors { get; set; }
        public Frame Frame { get; set; }

        public Dictionary<string, int> Ingredients { get; set; }
        #endregion

        #region Commands
        public MyICommand ConfirmCommand { get; set; }
        public MyICommand AddIngredientCommand {get; set;}
        public MyICommand CancelCommand { get; set; }
        #endregion

        #region Actions
        private void OnConfirm()
        {
            MedicationService medicationService = new MedicationService();
            string[] generateId = new Guid().ToString().Split("-");
            MedicationId = generateId[0];
            medicationService.AddMedication(new Medication(MedicationId, MedicationName, Status, Type, Ingredients, MedicationFormat));
            CreateValidationRequest();
            this.Frame.NavigationService.Navigate(new MedicationsView());
        }

        private void OnCancel()
        {
            this.Frame.NavigationService.Navigate(new MedicationsView());
        }

        private void OnAddIngredients()
        {
            Ingredients = new Dictionary<string, int>();
            IngredientsView ing = new IngredientsView(Ingredients);
            ing.ShowDialog();
        }

        private bool CanExecute()
        {
            return true;
        }
        #endregion

        #region Other Functions
        private void LoadDoctors()
        {
            Doctors = new List<string>();
            DoctorsRepository repository = new DoctorsRepository();
            foreach (Doctor d in repository.GetAll())
            {
                Doctors.Add(d.Name + " " + d.Surname);
            }
        }

        private void CreateValidationRequest()
        {
            string[] selectedDoctor = Doctor.ToString().Split(" ");
            string docName = selectedDoctor[0];
            string docLastName = selectedDoctor[1];
            string docJmbg = "";

            DoctorsRepository repository = new DoctorsRepository();
            foreach (Doctor d in repository.GetAll())
            {
                if (d.Name.Equals(docName) && d.Surname.Equals(docLastName))
                {
                    docJmbg = d.Jmbg;
                }
            }

            MedicineValidationRequest validationRequest = new MedicineValidationRequest(MedicationId, docJmbg);
            MedicineValidationsRepository validations = new MedicineValidationsRepository();
            validations.GetAll().Add(validationRequest);
            validations.SerializeValidationRequests();
        }
        #endregion

        #region Constructors
        public AddMedicationViewModel(Frame currentFrame)
        {
            this.Frame = currentFrame;
            LoadDoctors();
            ConfirmCommand = new MyICommand(OnConfirm, CanExecute);
            CancelCommand = new MyICommand(OnCancel, CanExecute);
            AddIngredientCommand = new MyICommand(OnAddIngredients, CanExecute);
        }
        #endregion
    }
}
