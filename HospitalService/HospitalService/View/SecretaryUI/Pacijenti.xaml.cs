using Model;
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
    /// Interaction logic for SecretaryWindow.xaml
    /// </summary>
    public partial class Pacijenti : Window
    {
        private PatientStorage storage;
        private Secretary secretary;
        public List<Patient> patients { get; set; }

        public Pacijenti()
        {
            InitializeComponent();
            this.DataContext = this;
           
            storage = new PatientStorage();
            patients = storage.GetAll();
            tableBinding.ItemsSource = patients;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddPatient addWindow = new AddPatient(storage, tableBinding);
            addWindow.Show();

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Patient p = (Patient)tableBinding.SelectedItem;
            if (p == null)
            {
                MessageBox.Show("You must select one item");
            }
            else
            {
                EditPatient windowEdit = new EditPatient(p, storage, tableBinding);
                windowEdit.Show();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)tableBinding.SelectedItem;
            if (patient == null)
            {
                MessageBox.Show("You must select one item!");
            }
            else
            {
                storage.Delete(patient.Jmbg);
                tableBinding.Items.Refresh();
            }
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }


        private void MedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)tableBinding.SelectedItem;
            if (patient == null)
            {
                MessageBox.Show("You must select one item!");
            }
            else
            {
                MedicalRecordWindow record = new MedicalRecordWindow(patient, storage);
                record.Show();
            }
        }

        private void Appointments_Click(object sender, RoutedEventArgs e)
        {
            Appointments appointments = new Appointments();
            appointments.ShowDialog();

        }

    }
}
