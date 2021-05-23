using HospitalService.Model;
using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Views;
using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class DiagnosisViewModel : ViewModelClass
    {
        public MedicalRecord MedicalRecord { get; set; }
        public string PatientsName { get; set; }
        public DiagnosisView Window { get; set; }
        public MedicalRecordViewModel ParentWindow { get; set; }
        private string diagnosis;
        private string symptoms;
        private string anamnesis;
        public RelayCommand ApplyCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }


        public string Diagnosis
        {
            get { return diagnosis; }
            set
            {
                diagnosis = value;
                OnPropertyChanged();
            }
        }

        public string Symptoms
        {
            get { return symptoms; }
            set
            {
                symptoms = value;
                OnPropertyChanged();
            }
        }

        public string Anamnesis
        {
            get { return anamnesis; }
            set
            {
                anamnesis = value;
                OnPropertyChanged();
            }
        }

        public DiagnosisViewModel(DiagnosisView window, MedicalRecordViewModel parent)
        {
            this.Window = window;
            this.ParentWindow = parent;
            this.MedicalRecord = parent.MedicalRecord;
            this.PatientsName = MedicalRecord.Patient.Name + " " + MedicalRecord.Patient.Surname;
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
            ApplyCommand = new RelayCommand(Executed_ApplyCommand,
              CanExecute_ApplyCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand,
             CanExecute_CancelCommand);
        }

        public bool CanExecute_CancelCommand(object obj)
        {
            return true;
        }
        public bool CanExecute_ApplyCommand(object obj)
        {
            if (Diagnosis == null || Symptoms == null || Anamnesis == null)
            {
                MessageBox.Show("Sva polja moraju biti popunjena.");
                return false;
            }
            else
                return true;
        }
       
        public void Executed_ApplyCommand(object obj)
        {
            Diagnosis newDiagnosis = new Diagnosis()
            {
                Illness = this.Diagnosis,
                Symptoms = this.Symptoms,
                Datum = DateTime.Now.Date,
                Anamnesis = this.Anamnesis
            };
            this.MedicalRecord.Diagnoses.Add(newDiagnosis);
            new MedicalRecordStorage().Edit(MedicalRecord); // prebaciti u servis
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
    }
}
