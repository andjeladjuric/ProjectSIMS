using HospitalService.Service;
using HospitalService.View.DoctorUI.Commands;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class FindMedicationViewModel : ViewModelClass
    {
        private ObservableCollection<Medication> medications;
        private Medication selectedMedication;

        private string _filterString;
        private ICollectionView _dataGridCollection;

        public RelayCommand CancelCommand { get; set; }
        public RelayCommand ApplyCommand { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }
        public PrescriptionViewModel ParentWindow { get; set; }
        public Frame Frame { get; set; }

        public ICollectionView DataGridCollection
        {
            get { return _dataGridCollection; }
            set
            {
                _dataGridCollection = value;
                OnPropertyChanged();
            }
        }

        public string FilterString
        {
            get { return _filterString; }
            set
            {
                _filterString = value;
                OnPropertyChanged();
                FilterCollection();
            }
        }


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
            MedicalRecord record = ParentWindow.ParentWindow.MedicalRecord;
            new MedicationService().GetAllAllowed(record.Allergies).ForEach(Medications.Add);
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
            ApplyCommand = new RelayCommand(Executed_ApplyCommand,
              CanExecute_ApplyCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand,
            CanExecute_CancelCommand);
            DataGridCollection = CollectionViewSource.GetDefaultView(Medications);
            DataGridCollection.Filter = new Predicate<object>(Filter);
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

        public bool Filter(object obj)
        {
            var data = obj as Medication;
            if (data != null)
            {
                if (!string.IsNullOrEmpty(_filterString))
                {
                    return data.MedicineName.ToLower().Contains(_filterString.ToLower().Trim());
                }
                return true;
            }
            return false;
        }

        private void FilterCollection()
        {
            if (_dataGridCollection != null)
            {
                _dataGridCollection.Refresh();
            }
        }
    }
}

