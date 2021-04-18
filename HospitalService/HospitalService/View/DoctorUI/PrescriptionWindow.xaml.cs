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
    /// Interaction logic for PrescriptionWindow.xaml
    /// </summary>
    public partial class PrescriptionWindow : Window
    {
        public MedicalRecordDoctorWindow medicalRecordWindow { get; set; }

        public PrescriptionWindow(MedicalRecordDoctorWindow mr)
        {
            InitializeComponent();
            medicalRecordWindow = mr;
            PatientTB.Text = mr.Karton.Patient.Name + " " + mr.Karton.Patient.Surname;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecordStorage baza = new MedicalRecordStorage();
            string medication = MedicationTB.Text;
            int howOften = int.Parse(HoursTB.Text);
            int howLong = int.Parse(DaysTB.Text);
            string info = InfoTB.Text;
            Prescription prescription = new Prescription(medication, howOften, howLong, info, DateTime.Now);
            MedicalRecord mr = medicalRecordWindow.Karton;
            mr.Prescriptions.Add(prescription);
            baza.Edit(mr);
            medicalRecordWindow.Refresh();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
