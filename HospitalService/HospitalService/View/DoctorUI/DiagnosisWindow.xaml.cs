using HospitalService.Model;
using Model;
using Storage;
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
    /// Interaction logic for DiagnosisWindow.xaml
    /// </summary>
    public partial class DiagnosisWindow : Window
    {
        public HistoryWindow prozor { get; set; }
        public DiagnosisWindow(HistoryWindow hw)
        {
            InitializeComponent();
            prozor = hw;
            PatientTB.Text = prozor.Karton.Karton.Patient.Name + " " + prozor.Karton.Karton.Patient.Surname;
            DateBox.SelectedDate = DateTime.Now;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecordStorage baza = new MedicalRecordStorage();
            string illness = DiagnosisTB.Text;
            string symptoms = SymptomsTB.Text;
            string anamnesis = AnamnesisTB.Text;
            Diagnosis diagnosis = new Diagnosis(illness, symptoms, (DateTime)DateBox.SelectedDate, anamnesis);
            MedicalRecord mr = prozor.Karton.Karton;
            mr.Diagnoses.Add(diagnosis);
            baza.Edit(mr);
            prozor.refresh();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
