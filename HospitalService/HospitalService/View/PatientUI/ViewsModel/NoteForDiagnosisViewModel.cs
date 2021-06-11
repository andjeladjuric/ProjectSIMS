using System;
using System.Collections.Generic;
using System.Text;
using HospitalService.Model;
using HospitalService.Service;
using HospitalService.View.PatientUI.Pages;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
    
   public class NoteForDiagnosisViewModel:ValidationBase
    {
        public String PatientNotes { get; set; }
        public RelayCommand confirmMakeNote { get; set; }
        public RelayCommand cancelMakeNote { get; set; }
        private Patient patient;
        private Diagnosis diagnosis;
        private NoteForDiagnosis noteForDiagnosis;
        private NotesService notesService;


        private void Execute_ConfirmMakeNote(object obj) {

            this.Validate();
            if (IsValid)
            {
                Note newNote = new Note { noteForPatient = PatientNotes, patient = patient, diagnosis = diagnosis };
                notesService.saveNote(newNote);
                noteForDiagnosis.NavigationService.Navigate(new DiagnosisForPatient(diagnosis, newNote));
            }

        }
        private void Execute_CancelMakeNote(object obj) {

            noteForDiagnosis.NavigationService.Navigate(new DiagnosisWithoutNote(diagnosis, patient));
        }
        private bool CanExecute_Command(object obj) {
            return true;
        }

        protected override void ValidateSelf()
        {
            if (string.IsNullOrWhiteSpace(PatientNotes)) {
                this.ValidationErrors["Note"] = "Unesite biljesku.";
            }
        }

        public NoteForDiagnosisViewModel(Patient patient, Diagnosis diagnosis, NoteForDiagnosis noteForDiagnosis) {

            this.patient = patient;
            this.diagnosis = diagnosis;
            this.noteForDiagnosis = noteForDiagnosis;
            notesService = new NotesService();
            confirmMakeNote = new RelayCommand(Execute_ConfirmMakeNote,CanExecute_Command);
            cancelMakeNote = new RelayCommand(Execute_CancelMakeNote,CanExecute_Command);
        }
    }
}
