using HospitalService.Model;
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
using System.Windows.Shapes;

namespace HospitalService.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        public MedicalRecordDoctorWindow Karton { get; set; }

        public HistoryWindow(MedicalRecordDoctorWindow mr)
        {
            InitializeComponent();
            Karton = mr;
            DiagnosisTable.ItemsSource = Karton.Karton.Diagnoses;
        }

        private void AddDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            DiagnosisWindow diagnosisWindow = new DiagnosisWindow(this);
            diagnosisWindow.ShowDialog();
        }

        public void refresh()
        {
            DiagnosisTable.ItemsSource = Karton.Karton.Diagnoses;
            DiagnosisTable.Items.Refresh();
            Karton.Refresh();
        }

        private void Anamnesis_Click(object sender, RoutedEventArgs e)
        {
            Diagnosis d = (Diagnosis)DiagnosisTable.SelectedItem;
            if (d == null)
            {
                MessageBox.Show("Morate izabrati dijagnozu.");
            }
            else
            {
                AnamnesisWindow anamnesisWindow = new AnamnesisWindow(d, this);
                anamnesisWindow.ShowDialog();
            }
        }
    }
}
