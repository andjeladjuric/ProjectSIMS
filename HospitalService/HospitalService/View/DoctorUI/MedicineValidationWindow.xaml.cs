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

namespace HospitalService.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for MedicineValidationWindow.xaml
    /// </summary>
    public partial class MedicineValidationWindow : Window
    {
        private Medication medication;
        private ObservableCollection<string> ingredients { get; set; }
        private MedicationStorage medicationStorage;
        private MedicineValidationStorage medicineValidationStorage;
        private DoctorWindow mainWindow;
        public MedicineValidationWindow(Medication medicine, DoctorWindow window)
        {
            InitializeComponent();
            mainWindow = window;
            medicationStorage = new MedicationStorage();
            medicineValidationStorage = new MedicineValidationStorage();
            medication = medicine;
            MedicationNameTB.Text = medication.MedicineName;
            MedicationFormatTB.Text = medication.Format;
            MedicationTypeTB.Text = medication.Type.ToString();
            ingredients = new ObservableCollection<string>();

            foreach (var item in medication.Ingredients)
            {
                ingredients.Add(item.Key + " " + item.Value + " mg");
            }
            IngredientsListView.ItemsSource = ingredients;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            medication.DoctorsComment = CommentTextBox.Text;
            medication.IsApproved = (bool)YesOption.IsChecked ? MedicineStatus.Approved : MedicineStatus.NotApproved;
            medicationStorage.Update(medication);
            medicineValidationStorage.Delete(medication.Id);
            mainWindow.refresh();
            this.Close();
        }
    }
}
