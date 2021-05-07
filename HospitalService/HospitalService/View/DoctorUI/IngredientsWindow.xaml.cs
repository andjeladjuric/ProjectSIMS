using HospitalService.Storage;
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

namespace HospitalService.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for IngredientsWindow.xaml
    /// </summary>
    public partial class IngredientsWindow : Window
    {
        private List<MedicationIngredients> ingredients;
        private AboutMedicationWindow AboutMedicationWindow;
        public IngredientsWindow(AboutMedicationWindow window)
        { 
            InitializeComponent();
            ingredients = new IngredientStorage().GetAll();
            IngredientsView.ItemsSource = ingredients;
            AboutMedicationWindow = window;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MedicationIngredients ingredient = (MedicationIngredients)IngredientsView.SelectedItem;
            new MedicationStorage().AddIngredient(ingredient.IngredientName, Int32.Parse(GramsTextBox.Text), AboutMedicationWindow.medication.Id);
            AboutMedicationWindow.RefreshView();
            this.Close();
        }
    }
}
