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
using System.Windows.Input;

namespace HospitalService.View.DoctorUI.ViewModel
{
    class FindPatientViewModel : ViewModelClass
    {
        private ObservableCollection<Patient> patients;
        private Patient patient;
        private string _filterString;
        private ICollectionView _dataGridCollection;

        public RelayCommand SelectCommand { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }
        public AddAppointmentToDoctorViewModel ParentWindow { get; set; }
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


        public ObservableCollection<Patient> Patients
        {
            get { return patients; }
            set
            {
                patients = value;
                OnPropertyChanged();
            }
        }
        public Patient Patient
        {
            get { return patient; }
            set
            {
                patient = value;
                OnPropertyChanged();
            }
        }

        public FindPatientViewModel(AddAppointmentToDoctorViewModel window, Frame frame)
        {
            this.Frame = frame;
            ParentWindow = window;
            Patients = new ObservableCollection<Patient>();
            new PatientService().GetAll().ForEach(Patients.Add); 
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
            SelectCommand = new RelayCommand(Executed_SelectCommand,
              CanExecute_SelectCommand);
            DataGridCollection = CollectionViewSource.GetDefaultView(Patients);
            DataGridCollection.Filter = new Predicate<object>(Filter);
        }

        public bool CanExecute_SelectCommand(object obj)
        {
            if (Patient == null)
            {
                MessageBox.Show("Morate izabrati jednog pacijenta");
                return false;
            }
            else
                return true;
        }

        public void Executed_SelectCommand(object obj)
        {
            ParentWindow.SelectPatient(Patient);
            this.Frame.Content = null;
        }

        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }

        public bool Filter(object obj)
        {
            var data = obj as Patient;
            if (data != null)
            {
                if (!string.IsNullOrEmpty(_filterString))
                {
                    return data.Name.ToLower().Contains(_filterString.ToLower().Trim()) || data.Surname.ToLower().Contains(_filterString.ToLower().Trim()) || data.Jmbg.Contains(_filterString.Trim()) || data.medicalRecordId.Contains(_filterString.Trim());
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
