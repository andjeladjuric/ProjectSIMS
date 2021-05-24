﻿using HospitalService.Model;
using HospitalService.Service;
using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Views;
using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class PrescriptionViewModel : ViewModelClass
    {
        private Medication selectedMedication;
        private int hours;
        private int days;
        private DateTime selectedDate;
        private string info;

        public Frame Frame { get; set; }
        public string PatientsName { get; set; }
        public PrescriptionView Window { get; set; }
        public MedicalRecordViewModel ParentWindow { get; set; }
        public RelayCommand AddMedicationCommand { get; set; }
        public RelayCommand ApplyCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }


        public Medication SelectedMedication
        {
            get { return selectedMedication; }
            set
            {
                selectedMedication = value;
                OnPropertyChanged();
            }
        }

        public int Hours
        {
            get { return hours; }
            set
            {
                hours = value;
                OnPropertyChanged();
            }
        }

        public int Days
        {
            get { return days; }
            set
            {
                days = value;
                OnPropertyChanged();
            }
        }

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged();
            }
        }

        public string Info
        {
            get { return info; }
            set
            {
                info = value;
                OnPropertyChanged();
            }
        }

        public PrescriptionViewModel(PrescriptionView window, MedicalRecordViewModel parent, Frame frame)
        {
            this.Frame = frame;
            this.ParentWindow = parent;
            this.Window = window;
            this.PatientsName = ParentWindow.MedicalRecord.Patient.Name + " "  + ParentWindow.MedicalRecord.Patient.Surname;
            this.SelectedDate = DateTime.Now;
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
            ApplyCommand = new RelayCommand(Executed_ApplyCommand,
              CanExecute_ApplyCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand,
             CanExecute_CancelCommand);
            AddMedicationCommand = new RelayCommand(Executed_AddMedicationCommand,
            CanExecute_AddMedicationCommand);
        }

        public bool CanExecute_CancelCommand(object obj)
        {
            return true;
        }

        public bool CanExecute_AddMedicationCommand(object obj)
        {
            return true;
        }
        public bool CanExecute_ApplyCommand(object obj)
        {
            if (SelectedMedication == null || Hours == 0 || Days == 0)
            {
                MessageBox.Show("Obavezna polja nisu popunjena.");
                return false;
            }
            else
                return true;
        }

        public void Executed_ApplyCommand(object obj)
        {
            Prescription newPrescription = new Prescription(SelectedMedication.MedicineName, Hours, Days, Info, SelectedDate);
            MedicalRecord medicalRecord = this.ParentWindow.MedicalRecord;
            medicalRecord.Prescriptions.Add(newPrescription);
            new MedicalRecordService().UpdateRecord(medicalRecord); 
            this.ParentWindow.Refresh();
            this.Window.Close();

        }

        public void Executed_CancelCommand(object obj)
        {
            this.Window.Close();
        }

        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }

        public void Executed_AddMedicationCommand(object obj)
        {
            this.Frame.NavigationService.Navigate( new FindMedicationView(this, Frame));
        }

        public void SelectMedication(Medication medication)
        {
            this.SelectedMedication = medication;
        }
    }
}
