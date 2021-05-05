using HospitalService.Storage;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace HospitalService.View.ManagerUI
{
    /// <summary>
    /// Interaction logic for Ingredients.xaml
    /// </summary>
    public partial class Ingredients : Window, INotifyPropertyChanged
    {
        private IngredientStorage ingredientStorage = new IngredientStorage();
        public ObservableCollection<MedicationIngredients> ingredients { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private MedicationIngredients _i;
        public MedicationIngredients selectedIng
        {
            get { return _i; }
            set
            {
                _i = value;
                OnPropertyChanged("selectedIng");
            }
        }

        public Dictionary<string, int> ing { get; set; }
        public ListBox lb { get; set; }
        public Ingredients(Dictionary<string, int> dict, ListBox box)
        {
            InitializeComponent();
            this.DataContext = this;
            ing = dict;
            lb = box;
            ingredients = new ObservableCollection<MedicationIngredients>();
            foreach (MedicationIngredients i in ingredientStorage.GetAll())
            {
                ingredients.Add(i);
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            string name = ingredientBox.Text;
            int quantity = Int32.Parse(quantityBox.Text);

            ing.Add(name, quantity);
            quantityBox.Text = "";
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            List<string> items = new List<string>();
            foreach (var item in ing)
            {
                items.Add(item.Key + " " + item.Value + " mg");
            }

            lb.ItemsSource = items;
            Close();
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            MedicationIngredients m = (MedicationIngredients)Validation.SelectedItem;
            if (m == null)
            {
                MessageBox.Show("Morate izabrati sobu!");
            }
            else
            {
                ingredientStorage.Delete(m.IngredientName);
                ingredients.Remove(m);
            }
        }

        private void addNewIngredient_Click(object sender, RoutedEventArgs e)
        {
            newFrame.Content = new AddIngredient(ingredients);
        }
    }
}
