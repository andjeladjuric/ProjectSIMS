using HospitalService.Storage;
using HospitalService.View.ManagerUI.ViewModels;
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

namespace HospitalService.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for IngredientsView.xaml
    /// </summary>
    public partial class IngredientsView : Page
    {
        IngredientsViewModel currentViewModel;

        public IngredientsView(Dictionary<string, int> CurrentIngredients, Frame currentFrame)
        {
            InitializeComponent();
            currentViewModel = new IngredientsViewModel(currentFrame, quantityFrame, CurrentIngredients);
            this.KeepAlive = true;
            this.DataContext = currentViewModel;
        }

        //private IngredientStorage ingredientStorage = new IngredientStorage();
        //public ObservableCollection<MedicationIngredients> ingredients { get; set; }
        //public ObservableCollection<string> currentIngredients { get; set; }
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void OnPropertyChanged(string name)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(name));
        //    }
        //}
        //private MedicationIngredients _i;
        //public MedicationIngredients selectedIng
        //{
        //    get { return _i; }
        //    set
        //    {
        //        _i = value;
        //        OnPropertyChanged("selectedIng");
        //    }
        //}

        //public Dictionary<string, int> ing { get; set; }
        //public ListBox lb { get; set; }
        //public IngredientsView(Dictionary<string, int> dict)
        //{
        //    InitializeComponent();
        //    this.DataContext = this;
        //    ing = dict;

        //    ingredients = new ObservableCollection<MedicationIngredients>();
        //    foreach (MedicationIngredients i in ingredientStorage.GetAll())
        //    {
        //        ingredients.Add(i);
        //    }

        //    AddIngredientsToList();
        //}

        //private void AddIngredientsToList()
        //{
        //    currentIngredients = new ObservableCollection<string>();
        //    foreach (var item in ing)
        //    {
        //        currentIngredients.Add(item.Key + " " + item.Value + " mg");
        //    }
        //}

        //private void add_Click(object sender, RoutedEventArgs e)
        //{
        //    int quantity = Int32.Parse(quantityBox.Text);
        //    MedicationIngredients m = (MedicationIngredients)Validation.SelectedItem;
        //    ing.Add(m.IngredientName, quantity);
        //    AddIngredientsToList();
        //    currentMedIngredients.ItemsSource = currentIngredients;
        //    quantityBox.Text = "";
        //}

        //private void cancel_Click(object sender, RoutedEventArgs e)
        //{
        //    List<string> items = new List<string>();
        //    foreach (var item in ing)
        //    {
        //        items.Add(item.Key + " " + item.Value + " mg");
        //    }

        //    lb.ItemsSource = items;
        //    // navigate back
        //}

        //private void remove_Click(object sender, RoutedEventArgs e)
        //{
        //    MedicationIngredients m = (MedicationIngredients)Validation.SelectedItem;
        //    if (m == null)
        //    {
        //        MessageBox.Show("Morate izabrati sastojak!");
        //    }
        //    else
        //    {
        //        if (ing.ContainsKey(m.IngredientName))
        //        {
        //            ing.Remove(m.IngredientName);
        //            AddIngredientsToList();
        //            currentMedIngredients.ItemsSource = currentIngredients;
        //        }
        //        DeleteIngredientsFromMedication(m);
        //        ingredientStorage.Delete(m.IngredientName);
        //        ingredients.Remove(m);
        //    }
        //}

        //private static void DeleteIngredientsFromMedication(MedicationIngredients m)
        //{
        //    MedicationStorage medStorage = new MedicationStorage();
        //    Medication med;
        //    for (int i = 0; i < medStorage.GetAll().Count; i++)
        //    {
        //        med = medStorage.GetAll()[i];
        //        if (med.Ingredients.ContainsKey(m.IngredientName))
        //        {
        //            med.Ingredients.Remove(m.IngredientName);
        //            medStorage.SerializeMedication();
        //        }
        //    }
        //}

        //private void addNewIngredient_Click(object sender, RoutedEventArgs e)
        //{
        //    newFrame.Content = new AddIngredientView(ingredients, newFrame);
        //}

        //private void removeIng_Click(object sender, RoutedEventArgs e)
        //{
        //    MedicationIngredients m = (MedicationIngredients)currentMedIngredients.SelectedItem;
        //    if (m == null)
        //    {
        //        MessageBox.Show("Morate izabrati sastojak!");
        //    }
        //    else
        //    {
        //        ing.Remove(m.IngredientName);
        //        AddIngredientsToList();
        //        currentMedIngredients.ItemsSource = currentIngredients;
        //        DeleteIngredientsFromMedication(m);
        //    }
        //}

        //private void currentMedIngredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    string i = (string)currentMedIngredients.SelectedItem;
        //    if (i != null)
        //    {
        //        string[] split = i.Split(" ");
        //        quantityBox.Text = split[1];
        //    }
        //}

        //private void Validation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    currentMedIngredients.UnselectAll();
        //    quantityBox.Text = "";
        //}

        //private void editIng_Click(object sender, RoutedEventArgs e)
        //{
        //    string i = (string)currentMedIngredients.SelectedItem;
        //    if (i != null)
        //    {
        //        string[] split = i.Split(" ");
        //        ing[split[0]] = Int32.Parse(quantityBox.Text);
        //    }
        //    AddIngredientsToList();
        //    currentMedIngredients.ItemsSource = currentIngredients;
        //    quantityBox.Text = "";
        //}
    }
}
