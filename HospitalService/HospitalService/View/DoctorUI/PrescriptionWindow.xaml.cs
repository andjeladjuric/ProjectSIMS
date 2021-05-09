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
        private MedicalRecordDoctorWindow parentWindow;
        private MedicalRecord medicalRecord;

        public PrescriptionWindow(MedicalRecordDoctorWindow parent)
        {
            InitializeComponent();
            parentWindow = parent;
            medicalRecord = parent.MedicalRecord;
            PatientTB.Text = medicalRecord.Patient.Name + " " + medicalRecord.Patient.Surname;
            MedicationOptions.ItemsSource = new MedicationStorage().GetAllAllowed(medicalRecord.Allergies);
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            Medication medication = (Medication)MedicationOptions.SelectedItem;
            int howOften = int.Parse(HoursTB.Text);
            int howLong = int.Parse(DaysTB.Text);
            string info = InfoTB.Text;
            Prescription newPrescription = new Prescription(medication.MedicineName, howOften, howLong, info, DateTime.Now);
            medicalRecord.Prescriptions.Add(newPrescription);
            new MedicalRecordStorage().Edit(medicalRecord);
            parentWindow.Refresh();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
