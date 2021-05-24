using HospitalService.View.DoctorUI.Commands;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class FindMedicationViewModel : ViewModelClass
    {
        private ObservableCollection<Medication> medications;
        private Medication selectedMedication;

        public RelayCommand CancelCommand { get; set; }
        public RelayCommand ApplyCommand { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }
        public PrescriptionViewModel ParentWindow { get; set; }
        public Frame Frame { get; set; }


        public ObservableCollection<Medication> Medications
        {
            get { return medications; }
            set
            {
                medications = value;
                OnPropertyChanged();
            }
        }
        public Medication SelectedMedication
        {
            get { return selectedMedication; }
            set
            {
                selectedMedication = value;
                OnPropertyChanged();
            }
        }

        public FindMedicationViewModel(PrescriptionViewModel window, Frame frame)
        {
            this.Frame = frame;
            ParentWindow = window;
            Medications = new ObservableCollection<Medication>();
            new MedicationStorage().GetAll().ForEach(Medications.Add); // servis, na sta nije alergican
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
            ApplyCommand = new RelayCommand(Executed_ApplyCommand,
              CanExecute_ApplyCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand,
            CanExecute_CancelCommand);
        }

        public bool CanExecute_ApplyCommand(object obj)
        {
            if (selectedMedication == null)
            {
                MessageBox.Show("Morate izabrati lijek.");
                return false;
            }
            else
                return true;
        }

        public void Executed_ApplyCommand(object obj)
        {
            ParentWindow.SelectMedication(SelectedMedication);
            this.Frame.Content = null;
        }


        public bool CanExecute_CancelCommand(object obj)
        {
                return true;
        }

        public void Executed_CancelCommand(object obj)
        {
            this.Frame.Content = null;
        }

        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }
    }
}

