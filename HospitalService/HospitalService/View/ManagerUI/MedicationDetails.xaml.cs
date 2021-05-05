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

namespace HospitalService.View.ManagerUI
{
    /// <summary>
    /// Interaction logic for MedicationDetails.xaml
    /// </summary>
    public partial class MedicationDetails : Window
    {
        public Medication medication { get; set; }
        private ObservableCollection<string> items {get; set;}
        public MedicationDetails(Medication m)
        {
            InitializeComponent();
            this.DataContext = this;
            medication = m;

            formatBox.Text = m.Format;
            items = new ObservableCollection<string>();

            foreach (var item in m.Ingredients)
            {
                items.Add(item.Key + " " + item.Value + " mg");
            }

            Validation.ItemsSource = items;

            if (medication.IsApproved == MedicineStatus.NotApproved)
                resend.IsEnabled = true;
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
                    m.IsApproved = MedicineStatus.WaitingForApproval;
                    m.Ingredients = medication.Ingredients;
                    break;
                }
            }
            medStorage.SerializeMedication();
        }

        private void resend_Click(object sender, RoutedEventArgs e)
        {
            MedicineValidationRequest validationRequest = new MedicineValidationRequest(medication.Id, "0101000234567");
            MedicineValidationStorage validationStorage = new MedicineValidationStorage();
            validationStorage.GetAll().Add(validationRequest);
            validationStorage.SerializeValidationRequests();
            SerializeEditedIngredients();
            Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Ingredients i = new Ingredients(medication.Ingredients, Validation);
            i.ShowDialog();
        }
    }
}
