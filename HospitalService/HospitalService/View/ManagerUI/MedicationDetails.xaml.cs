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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
