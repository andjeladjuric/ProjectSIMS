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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalService.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for AddIngredientView.xaml
    /// </summary>
    public partial class AddIngredientView : Page
    {
        ObservableCollection<MedicationIngredients> ingredients { get; set; }
        public AddIngredientView(ObservableCollection<MedicationIngredients> i)
        {
            InitializeComponent();
            ingredients = i;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page());
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            string name = ingredientBox.Text;
            IngredientStorage storage = new IngredientStorage();
            MedicationIngredients newIngredient = new MedicationIngredients();
            newIngredient.IngredientName = name;

            foreach (MedicationIngredients m in storage.GetAll())
            {
                if (name.Equals(m.IngredientName))
                {
                    MessageBox.Show("Sastojak već postoji!");
                    return;
                }
            }

            storage.Save(newIngredient);
            storage.SerializeIngredients();
            ingredients.Add(newIngredient);
            NavigationService.Navigate(new Page());
        }
    }
}