using HospitalService.Model;
using HospitalService.Service;
using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Validation;
using HospitalService.View.DoctorUI.Views;
using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class PrescriptionViewModel : ValidationBase
    {
        private Medication selectedMedication;
        private string hours;
        private string days;
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
                OnPropertyChanged("SelectedMedication");
            }
        }

        public string Hours
        {
            get { return hours; }
            set
            {
                hours = value;
                OnPropertyChanged("Hours");
            }
        }

        public string Days
        {
            get { return days; }
            set
            {
                days = value;
                OnPropertyChanged("Days");
            }
        }

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        public string Info
        {
            get { return info; }
            set
            {
                info = value;
                OnPropertyChanged("Info");
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
            this.Validate();
            if (this.IsValid)
            {
                return true;
            }
            else
                return false;
        }

        public void Executed_ApplyCommand(object obj)
        {
            Prescription newPrescription = new Prescription(SelectedMedication.MedicineName, Int32.Parse(Hours), Int32.Parse(Days), Info, SelectedDate);
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

        protected override void ValidateSelf()
        {
            Regex regex = new Regex("^[0-9]+$");
            if (SelectedMedication == null)
                this.ValidationErrors["Medication"] = "Izaberite lijek";
            if (string.IsNullOrWhiteSpace(Hours) || !regex.IsMatch(Hours))
                this.ValidationErrors["Hours"] = "Unesite broj";
            if (string.IsNullOrWhiteSpace(Days) || !regex.IsMatch(Days))
                this.ValidationErrors["Days"] = "Unesite broj";
        }
    }
}
