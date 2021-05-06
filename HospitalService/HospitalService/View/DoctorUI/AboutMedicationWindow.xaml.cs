﻿using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Interaction logic for AboutMedicationWindow.xaml
    /// </summary>
    public partial class AboutMedicationWindow : Window
    {
        private Medication medication;
        private ObservableCollection<string> ingredients;

        public AboutMedicationWindow(Medication selected)
        {
            InitializeComponent();
            medication = selected;
            MedicationNameTB.Text = medication.MedicineName;
            MedicationFormatTB.Text = medication.Format;
            MedicationTypeOptions.ItemsSource = Enum.GetValues(typeof(MedicationType)).Cast<MedicationType>();
            MedicationTypeOptions.SelectedItem = medication.Type;
            ReplacementOptions.ItemsSource = new MedicationStorage().GetAllApproved();
            if (medication.Replacement != null)
                ReplacementOptions.SelectedItem = new MedicationStorage().getOne(medication.Replacement);
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
            medication.MedicineName = MedicationNameTB.Text;
            medication.Format = MedicationFormatTB.Text;
            medication.Type = (MedicationType)MedicationTypeOptions.SelectedItem;
            Medication med = (Medication)ReplacementOptions.SelectedItem;
            medication.Replacement = med.Id;
            new MedicationStorage().Update(medication);
            this.Close();
        }
    }
}