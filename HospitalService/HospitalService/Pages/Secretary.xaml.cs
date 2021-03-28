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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HospitalService.Windowss;
using Model;

namespace HospitalService.Pages
{
    /// <summary>
    /// Interaction logic for Secretary.xaml
    /// </summary>
    public partial class Secretary : Page
    {
        private PatientStorage storage;
        public List<Model.Patient> patients { get; set; }

        public Secretary()
        {
            InitializeComponent();
            this.DataContext = this;
            storage = new PatientStorage();
            patients = storage.GetAll();
            tableBinding.ItemsSource = patients;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddPatient addWindow = new AddPatient(storage, tableBinding);
            addWindow.Show();
            
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Model.Patient p = (Model.Patient)tableBinding.SelectedItem;
            if (p == null)
            {
                MessageBox.Show("You must select one item");
            }
            else
            {
                EditPatient windowEdit = new EditPatient(p, storage, tableBinding);
                windowEdit.Show();
             }
        }
        
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Model.Patient patient = (Model.Patient)tableBinding.SelectedItem;
            if (patient == null)
            {
                MessageBox.Show("You must select one item!");
            }
            else
            {
                storage.Delete(patient.Jmbg);
                tableBinding.Items.Refresh();
            }
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Start());
        }

    }
}
