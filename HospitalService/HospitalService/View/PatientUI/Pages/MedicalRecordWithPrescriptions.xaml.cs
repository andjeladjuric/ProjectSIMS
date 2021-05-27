using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
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
using HospitalService.View.PatientUI.ViewsModel;
using Model;
using Storage;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for MedicalRecordWithPrescriptions.xaml
    /// </summary>
    public partial class MedicalRecordWithPrescriptions : Page
    {

        /* public Patient Patient { get; set; }
         public List<Prescription> prescriptions { get; set; }

         public MedicalRecordStorage medicalRecordStorage;

         public Timer medicationReminder;
         public Prescription prescription;
         public String notification = "";*/
        private PatientPrescriptionsViewModel viewModel;
        public MedicalRecordWithPrescriptions(Patient patient)
        {
            InitializeComponent();
            viewModel = new PatientPrescriptionsViewModel(patient);
            this.DataContext = viewModel;
            /*Patient = patient;
            medicalRecordStorage = new MedicalRecordStorage();
            prescriptions = medicalRecordStorage.getByPatient(Patient);
            tablePrescriptions.ItemsSource = prescriptions;*/
        }

       /* private void setMedicationReminder(object sender, RoutedEventArgs e)
        {
            
            prescription = (Prescription)tablePrescriptions.SelectedItem;
            if (prescription == null)
            {

                MessageBox.Show("Niste selektovali recept!");

            }
            else
            {
                
                DateTime prescriptionStartDate = prescription.Start;
                medicationReminder = new System.Timers.Timer(15000); // in milliseconds - p.HowOften*3600000
                notification += "Uzmite lijek" + " " + prescription.Medication.ToUpper() + "\n" + "Dodatne informacije: " + prescription.AdditionalInfos;
                DateTime prescriptionExpiryDate = prescriptionStartDate.AddDays(prescription.HowLong);
                medicationReminder.Elapsed += (sender, e) => showNotification(sender, e, notification, prescriptionExpiryDate);
                medicationReminder.Start();
               
                
            }

        }

        static void showNotification(object sender, ElapsedEventArgs e, string theString, DateTime et)
        {
            if (DateTime.Compare(DateTime.Now, et) < 0)
            {
                MessageBox.Show(theString);

            }
        }*/
    }
}
