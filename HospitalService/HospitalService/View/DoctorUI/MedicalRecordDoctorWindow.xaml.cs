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
    /// Interaction logic for MedicalRecordDoctorWindow.xaml
    /// </summary>
    public partial class MedicalRecordDoctorWindow : Window
    {
        public MedicalRecord Karton { get; set; }

        public MedicalRecordDoctorWindow(MedicalRecord md)
        {
            InitializeComponent();
            Karton = md;
            MedicalRecordNum.Text = md.Id;
            IstorijaList.ItemsSource = md.Diagnoses;
            TerapijaList.ItemsSource = md.Prescriptions;
            AllergiesListView.ItemsSource = md.Allergies;
            NameLbl.Content = md.Patient.Name + " " + md.Patient.Surname;
            DateOfBirthTB.Text = md.Patient.DateOfBirth.HasValue ? md.Patient.DateOfBirth.Value.ToString("MM/dd/yyyy") : " ";
            JmbgTB.Text = md.Patient.Jmbg;
            AddressTB.Text = md.Patient.Address;
            ContactTB.Text = md.Patient.Phone;

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            HistoryWindow historyWindow = new HistoryWindow(this);
            historyWindow.ShowDialog();

        }

        private void Prescription_Click(object sender, RoutedEventArgs e)
        {
             PrescriptionWindow prescriptionWindow = new PrescriptionWindow(this);
             prescriptionWindow.ShowDialog();
        }

        public void Refresh()
        {
            IstorijaList.ItemsSource = Karton.Diagnoses;
            TerapijaList.ItemsSource = Karton.Prescriptions;
            AllergiesListView.ItemsSource = Karton.Allergies;
            IstorijaList.Items.Refresh();
            TerapijaList.Items.Refresh();
            AllergiesListView.Items.Refresh();
        }

        private void Referral_Click(object sender, RoutedEventArgs e)
        {
            ReferralWindow referralWindow = new ReferralWindow(this);
            referralWindow.ShowDialog();
        }

        private void UrgentOperation_Click(object sender, RoutedEventArgs e)
        {
            OperationWindow newOperationWindow = new OperationWindow(Karton.Patient);
            newOperationWindow.ShowDialog();
        }

        private void AddAllergie_Click(object sender, RoutedEventArgs e)
        {
            AddAllergieWindow addAllergie = new AddAllergieWindow(Karton, this);
            addAllergie.ShowDialog();
        }

        private void DeleteAllergie_Click(object sender, RoutedEventArgs e)
        {
            MedicationIngredients allergie = (MedicationIngredients)AllergiesListView.SelectedItem;
            if(allergie == null)
                MessageBox.Show("Morate izabrati sastojak.");
            else
            {
                Karton.deleteAllergie(allergie);
                new MedicalRecordStorage().Edit(Karton);
                Refresh();
            }
        }
    }
}
