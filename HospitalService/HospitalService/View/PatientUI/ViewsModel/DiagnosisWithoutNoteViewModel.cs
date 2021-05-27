using System;
using System.Collections.Generic;
using System.Text;
using HospitalService.Model;
using HospitalService.View.PatientUI.Pages;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
  public  class DiagnosisWithoutNoteViewModel:ViewModelPatientClass
    {

        public RelayCommand makeNote { get; set; }
        public String Date { get; set; }
        public String PatientIllness { get; set; }
        public String PatientSymptoms { get; set; }
        public String PatientAnamnesis { get; set; }
        private Patient patient;
        private Diagnosis diagnosis;
        private DiagnosisWithoutNote diagnosisWithoutNote;
        private void Execute_MakeNote(object obj) {

            diagnosisWithoutNote.NavigationService.Navigate(new NoteForDiagnosis(patient, diagnosis));

        }
        private bool CanExecute_Command(object obj) {
            return true;
        }
        public DiagnosisWithoutNoteViewModel(Patient patient, Diagnosis diagnosis, DiagnosisWithoutNote diagnosisWithoutNote) {

            this.patient = patient;
            this.diagnosis = diagnosis;
            this.diagnosisWithoutNote = diagnosisWithoutNote;
            Date = diagnosis.Datum.ToShortDateString();
            PatientIllness = diagnosis.Illness;
            PatientSymptoms = diagnosis.Symptoms;
            PatientAnamnesis = diagnosis.Anamnesis;
            makeNote = new RelayCommand(Execute_MakeNote,CanExecute_Command);
        }
    }
}
