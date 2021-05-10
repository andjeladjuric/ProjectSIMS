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

namespace HospitalService.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for MedicalRecordWindow.xaml
    /// </summary>
    public partial class MedicalRecordWindow : Window
    {
        private Patient patient;
        private MedicalRecordStorage store;
        public PatientStorage stor { get; set; }
        public MedicalRecord karton { get; set; }
        public MedicalRecordWindow(Patient p, PatientStorage s)
        {
            InitializeComponent();
            patient = p;
            stor = s;
            store = new MedicalRecordStorage();

            idkarton.IsEnabled = false;
            if (p.medicalRecordId != null)
            {
                karton = store.GetOne(patient.medicalRecordId);
                idkarton.Text = patient.medicalRecordId;
                dodaj.Visibility = Visibility.Hidden;
                IstorijaList.ItemsSource = karton.Diagnoses;
                TerapijaList.ItemsSource = karton.Prescriptions;
            }
            else
            {
                IstorijaList.Visibility = Visibility.Hidden;
                TerapijaList.Visibility = Visibility.Hidden;
                AlergijaList.Visibility = Visibility.Hidden;
                ist.Visibility = Visibility.Hidden;
                ter.Visibility = Visibility.Hidden;
                aler.Visibility = Visibility.Hidden;
            }
            
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddMedicalRecord addWindow = new AddMedicalRecord(store, patient, stor);
            this.Close();
            addWindow.Show();
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
