using HospitalService.Storage;
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
    /// Interaction logic for AddAllergieWindow.xaml
    /// </summary>
    public partial class AddAllergieWindow : Window
    {
        private List<MedicationIngredients> Ingredients;
        private MedicalRecordDoctorWindow parent;
        private MedicalRecord patient;
        public AddAllergieWindow(MedicalRecord selected, MedicalRecordDoctorWindow window)
        {
            InitializeComponent();
            patient = selected;
            parent = window;
            Ingredients = new IngredientStorage().GetAll();
            IngredientsView.ItemsSource = Ingredients;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            MedicationIngredients newAllergie = (MedicationIngredients)IngredientsView.SelectedItem;
            if (newAllergie == null)
                MessageBox.Show("Morate izabrati sastojak.");
            else if (patient.AlreadyExists(newAllergie))
                MessageBox.Show("Izabrani sastojak vec postoji kao alergija.");
            else
            {
                patient.Allergies.Add(newAllergie);
                new MedicalRecordStorage().Edit(patient);
                parent.Refresh();
                this.Close();
            }

        }
    }
}
