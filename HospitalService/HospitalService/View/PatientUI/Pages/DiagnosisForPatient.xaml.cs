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

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for DiagnosisForPatient.xaml
    /// </summary>
    public partial class DiagnosisForPatient : Page
    {
        public Diagnosis chosenDiagnosis { get; set; }
        public Note noteOfChosenDiagnosis { get; set; }
       
        public DiagnosisForPatient(Diagnosis diagnosis, Note note)
        {
            InitializeComponent();
            lbDate.Content = diagnosis.Datum.ToShortDateString();
            lbIllness.Content = diagnosis.Illness;
            tbSymptoms.Text = diagnosis.Symptoms;
            tbAnamnesis.Text = diagnosis.Anamnesis;
            tbNotes.Text = note.noteForPatient;
            chosenDiagnosis = diagnosis;
            noteOfChosenDiagnosis = note;
           
        }

        private void setReminder(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new NotesNotification(chosenDiagnosis,noteOfChosenDiagnosis));
        }
    }
}
