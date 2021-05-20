using HospitalService.Model;
using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Views;
using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class MedicalRecordViewModel : ViewModelClass
    {
        public Patient Patient { get; set; }
        public Frame AllergiesFrame { get; set; }
        public string PatientName { get; set; }
        private bool male;
        private Frame anamnesisFrame;
        private Diagnosis selectedDiagnosis;
        public bool Famele { get; set; }
        private MedicalRecord medicalRecord;
        private ObservableCollection<Diagnosis> diagnoses;
        private ObservableCollection<Prescription> prescriptions;
        public RelayCommand ReferralCommand { get; set; }
        public RelayCommand TreatmentCommand { get; set; }
        public RelayCommand DiagnosisCommand { get; set; }
        public RelayCommand ShowAnamnesisCommand { get; set; }
        public RelayCommand PrescriptionCommand { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }

        public ObservableCollection<Diagnosis> Diagnoses
        {
            get { return diagnoses; }
            set
            {
                diagnoses = value;
                OnPropertyChanged();
            }

        }

        public ObservableCollection<Prescription> Prescriptions
        {
            get { return prescriptions; }
            set
            {
                prescriptions = value;
                OnPropertyChanged();
            }

        }

        public Diagnosis SelectedDiagnosis
        {
            get { return selectedDiagnosis; }
            set
            {
                selectedDiagnosis = value;
                OnPropertyChanged();
            }

        }

        public bool Male
        {
            get { return male; }
            set
            {
                male = value;
                OnPropertyChanged();
            }

        }

        public Frame AnamnesisFrame
        {
            get { return anamnesisFrame; }
            set
            {
                anamnesisFrame = value;
                OnPropertyChanged();
            }

        }

        public MedicalRecord MedicalRecord
        {
            get { return medicalRecord; }
            set
            {
                medicalRecord = value;
                OnPropertyChanged();
            }
        }

        public MedicalRecordViewModel(Frame frame, Patient selected, Frame anamnesisFrame) 
        {
            this.AllergiesFrame = frame;
            this.AnamnesisFrame = anamnesisFrame;
            this.Patient = selected;
            this.PatientName = selected.Name + " " + selected.Surname;
            if (selected.Gender.Equals(Gender.Female))
            {
                Male = false;
            }
            else {
                Male = true;
            }
            this.MedicalRecord = new MedicalRecordStorage().GetOne(Patient.medicalRecordId);
            this.Diagnoses = new ObservableCollection<Diagnosis>();
            this.Prescriptions = new ObservableCollection<Prescription>();
            this.MedicalRecord.Diagnoses.ForEach(Diagnoses.Add);
            this.MedicalRecord.Prescriptions.ForEach(Prescriptions.Add);
            this.AllergiesFrame.NavigationService.Navigate(new AllergiesView(MedicalRecord.Allergies,AllergiesFrame, MedicalRecord));
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
            ReferralCommand = new RelayCommand(Executed_ReferralCommand,
              CanExecute_ReferralCommand);
            DiagnosisCommand = new RelayCommand(Executed_DiagnosisCommand,
            CanExecute_DiagnosisCommand);
            TreatmentCommand = new RelayCommand(Executed_TreatmentCommand,
           CanExecute_TreatmentCommand);
            ShowAnamnesisCommand = new RelayCommand(Executed_ShowAnamnesisCommand,
            CanExecute_ShowAnamnesisCommand);
            PrescriptionCommand = new RelayCommand(Executed_PrescriptionCommand,
          CanExecute_PrescriptionCommand);
        }

        public bool CanExecute_TreatmentCommand(object obj)
        {
            return true;
        }

        public void Executed_TreatmentCommand(object obj)
        {
            new HospitalTreatmentView(this).ShowDialog();
        }

        public bool CanExecute_PrescriptionCommand(object obj)
        {
            return true;
        }

        public void Executed_PrescriptionCommand(object obj)
        {
            new PrescriptionView(this).ShowDialog();
        }

        public bool CanExecute_ReferralCommand(object obj)
        {
            return true;
        }

        public void Executed_ReferralCommand(object obj)
        {
            new ReferralView(MedicalRecord).ShowDialog();
        }

        public bool CanExecute_ShowAnamnesisCommand(object obj)
        {
            return true;
        }

        public void Executed_ShowAnamnesisCommand(object obj)
        {
            this.AnamnesisFrame.NavigationService.Navigate(new AnamnesisView(SelectedDiagnosis.Anamnesis));
        }

        public bool CanExecute_DiagnosisCommand(object obj)
        {
            return true;
        }

        public void Executed_DiagnosisCommand(object obj)
        {
            new DiagnosisView(this).ShowDialog();
        }
        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }

        public void Refresh()
        {
            this.MedicalRecord = new MedicalRecordStorage().GetOne(Patient.medicalRecordId);
            this.Diagnoses = new ObservableCollection<Diagnosis>();
            this.MedicalRecord.Diagnoses.ForEach(Diagnoses.Add);
            this.Prescriptions = new ObservableCollection<Prescription>();
            this.MedicalRecord.Prescriptions.ForEach(Prescriptions.Add);
        }
    }
}
