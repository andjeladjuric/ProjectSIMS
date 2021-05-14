using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HospitalService.Model;
using HospitalService.Storage;
using Model;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for NoteForDiagnosis.xaml
    /// </summary>
    public partial class NoteForDiagnosis : Page
    {
        public Patient Patient { get; set; }
        public Diagnosis Diagnosis { get; set; }
        public NoteForDiagnosis(Patient patient, Diagnosis diagnosis)
        {
            InitializeComponent();
            Patient = patient;
            Diagnosis = diagnosis;
        }

        private void confirmClick(object sender, RoutedEventArgs e)
        {
            NotesStorage notesStorage = new NotesStorage();
            String note = tbNotes.Text;
            Note newNote = new Note {noteForPatient=note, patient=Patient, diagnosis=Diagnosis };
            notesStorage.Save(newNote);
            this.NavigationService.Navigate(new DiagnosisForPatient(Diagnosis, newNote));


        }

        private void cancelClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DiagnosisWithoutNote(Diagnosis,Patient));
        }
    }
}
