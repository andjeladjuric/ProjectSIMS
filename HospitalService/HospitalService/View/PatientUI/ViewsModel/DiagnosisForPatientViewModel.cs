using System;
using System.Collections.Generic;
using System.Text;
using HospitalService.Model;
using HospitalService.View.PatientUI.Pages;

namespace HospitalService.View.PatientUI.ViewsModel
{
   public class DiagnosisForPatientViewModel:ViewModelPatientClass
    {
        public RelayCommand setReminder { get; set; }
        public String Date { get; set; }
        public String PatientIllness { get; set; }
        public String PatientSymptoms { get; set; }
        public String PatientAnamnesis { get; set; }
        public String PatientNote { get; set; }
        private Diagnosis diagnosis;
        private Note note;
        private DiagnosisForPatient diagnosisForPatient;

        private void Execute_SetReminder(object obj) {

            diagnosisForPatient.NavigationService.Navigate(new NotesNotification(diagnosis, note));

        }
        private bool CanExecute_Command(object obj) {

            return true;
        }

        public DiagnosisForPatientViewModel(Diagnosis diagnosis, Note note, DiagnosisForPatient diagnosisForPatient) {
            this.diagnosis = diagnosis;
            this.note = note;
            this.diagnosisForPatient = diagnosisForPatient;
            Date = diagnosis.Datum.ToShortDateString();
            PatientIllness = diagnosis.Illness;
            PatientSymptoms = diagnosis.Symptoms;
            PatientAnamnesis = diagnosis.Anamnesis;
            PatientNote = note.noteForPatient;
            setReminder=new RelayCommand(Execute_SetReminder,CanExecute_Command);
        
        }
    }
}
