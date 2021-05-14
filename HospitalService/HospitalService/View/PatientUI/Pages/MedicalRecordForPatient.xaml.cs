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
using Storage;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for MedicalRecordForPatient.xaml
    /// </summary>
    public partial class MedicalRecordForPatient : Page
    {
        public Patient Patient { get; set; }
        public MedicalRecordForPatient(Patient patient)
        {
            InitializeComponent();
            Patient = patient;
            MedicalRecordStorage mrs = new MedicalRecordStorage();
            MedicalRecord md = mrs.getOneByPatient(Patient);
            historyList.ItemsSource = md.Diagnoses;
            IdLabel.Content = md.Id;
        }

        

        private void click(object sender, MouseButtonEventArgs e)
        {
            Diagnosis d = (Diagnosis)historyList.SelectedItem;
            RecordPage.Content = new DiagnosisForPatient(d);
        }
    }
}
