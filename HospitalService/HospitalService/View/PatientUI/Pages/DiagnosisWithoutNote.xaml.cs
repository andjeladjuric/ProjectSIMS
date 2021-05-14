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
using Model;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for DiagnosisWithoutNote.xaml
    /// </summary>
    public partial class DiagnosisWithoutNote : Page
    {
        public Patient Patient { get; set; }
        public Diagnosis Diagnosis { get; set; }
        public DiagnosisWithoutNote(Diagnosis diagnosis, Patient patient)
        {
            InitializeComponent();
            Patient = patient;
            Diagnosis = diagnosis;
            lbDate.Content = diagnosis.Datum;
            lbIllness.Content = diagnosis.Illness;
            tbSymptoms.Text = diagnosis.Symptoms;
            tbAnamnesis.Text = diagnosis.Anamnesis;

        }

        private void makeNoteClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new NoteForDiagnosis(Patient,Diagnosis));
        }
    }
}
