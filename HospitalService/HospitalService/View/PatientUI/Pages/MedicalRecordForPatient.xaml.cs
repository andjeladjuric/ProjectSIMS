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
using HospitalService.Service;
using HospitalService.Storage;
using Model;
using Storage;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for MedicalRecordForPatient.xaml
    /// </summary>
    public partial class MedicalRecordForPatient : Page
    {
        private NotesService notesService;
        public Patient Patient { get; set; }
        public MedicalRecordForPatient(Patient patient)
        {
            InitializeComponent();
            Patient = patient;
            MedicalRecordStorage mrs = new MedicalRecordStorage();
            MedicalRecord md = mrs.getOneByPatient(Patient);
            historyList.ItemsSource = md.Diagnoses;
            IdLabel.Content = md.Id;
            notesService = new NotesService();
        }

        

        private void showDiagnosisDetails(object sender, MouseButtonEventArgs e)
        {
            Diagnosis selectedDiagnosis = (Diagnosis)historyList.SelectedItem;
            Note note = notesService.getNoteByPatient(Patient,selectedDiagnosis);
            if (note != null)
            {
                RecordPage.Content = new DiagnosisForPatient(selectedDiagnosis,note);
            }
            else {
                RecordPage.Content = new DiagnosisWithoutNote(selectedDiagnosis,Patient);
            }
        }
    }
}
