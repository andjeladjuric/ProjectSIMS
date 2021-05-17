using HospitalService.Storage;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HospitalService.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for MedicationDetails.xaml
    /// </summary>
    public partial class MedicationDetailsView : Window
    {
        public Medication medication { get; set; }
        private ObservableCollection<string> items { get; set; }
        DataGrid dg { get; set; }
        public MedicationDetailsView(Medication m, DataGrid grid)
        {
            InitializeComponent();
            this.DataContext = this;
            medication = m;
            dg = grid;
            items = new ObservableCollection<string>();

            foreach (var item in m.Ingredients)
            {
                items.Add(item.Key + " " + item.Value + " mg");
            }

            Validation.ItemsSource = items;

            if (medication.IsApproved == MedicineStatus.NotApproved)
                resend.IsEnabled = true;

            if (medication.IsApproved == MedicineStatus.Approved)
            {
                formatBox.IsReadOnly = true;
                comboBox.IsHitTestVisible = false;
                Edit.Visibility = Visibility.Hidden;
            }

            if (medication.Replacement != null)
            {
                Medication med = new MedicationStorage().getOne(medication.Replacement);
                alternativeBox.Text = med.MedicineName;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SerializeEditedIngredients();
            Close();
        }

        private void SerializeEditedIngredients()
        {
            MedicationStorage medStorage = new MedicationStorage();
            foreach (Medication m in medStorage.GetAll())
            {
                if (m.Id.Equals(medication.Id))
                {
                    m.Ingredients = medication.Ingredients;
                    m.Format = formatBox.Text;
                    if (comboBox.SelectedIndex != -1)
                        m.Type = (MedicationType)comboBox.SelectedItem;
                    break;
                }
            }
            medStorage.SerializeMedication();
        }

        private void ChangeMedStatus()
        {
            MedicationStorage medStorage = new MedicationStorage();
            foreach (Medication m in medStorage.GetAll())
            {
                if (m.Id.Equals(medication.Id))
                {
                    m.IsApproved = MedicineStatus.WaitingForApproval;
                    break;
                }
            }
            medStorage.SerializeMedication();
        }

        private void resend_Click(object sender, RoutedEventArgs e)
        {
            DoctorStorage ds = new DoctorStorage();
            Doctor drpetra = ds.GetOne("drpetra");
            MedicineValidationRequest validationRequest = new MedicineValidationRequest(medication.Id, drpetra.Jmbg);
            MedicineValidationStorage validationStorage = new MedicineValidationStorage();
            validationStorage.GetAll().Add(validationRequest);
            validationStorage.SerializeValidationRequests();
            SerializeEditedIngredients();
            ChangeMedStatus();
            MedicationStorage medStorage = new MedicationStorage();
            dg.ItemsSource = medStorage.GetAll();
            Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            IngredientsView i = new IngredientsView(medication.Ingredients, Validation);
            i.ShowDialog();
        }
    }
}
