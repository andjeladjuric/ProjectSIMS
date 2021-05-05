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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalService.View.ManagerUI
{
    /// <summary>
    /// Interaction logic for AddMedication.xaml
    /// </summary>
    public partial class AddMedication : Page, INotifyPropertyChanged
    {
        private MedicationStorage medStorage = new MedicationStorage();
        Dictionary<string, int> dictionary = new Dictionary<string, int>();
        List<string> ing = new List<string>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _name;
        public string MedName
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("MedName");
                }
            }
        }

        private string _format;
        public string MedForm
        {
            get
            {
                return _format;
            }
            set
            {
                if (value != _format)
                {
                    _format = value;
                    OnPropertyChanged("MedForm");
                }
            }
        }

        public AddMedication()
        {
            InitializeComponent();
            this.DataContext = this;
            List<string> doctors = new List<string>();
            DoctorStorage storage = new DoctorStorage();

            foreach(Doctor d in storage.GetAll())
            {
                doctors.Add(d.Name + " " + d.Surname);
            }

            doctorsBox.ItemsSource = doctors;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            string id = random.Next(1000).ToString();
            string name = NameBox.Text;
            MedicationType type = (MedicationType)comboBox.SelectedItem;

            string[] selectedDoctor = doctorsBox.Text.Split(" ");
            string docName = selectedDoctor[0];
            string docLastName = selectedDoctor[1];
            string docJmbg = "";

            DoctorStorage storage = new DoctorStorage();
            foreach (Doctor d in storage.GetAll())
            {
                if (d.Name.Equals(docName) && d.Surname.Equals(docLastName))
                {
                    docJmbg = d.Jmbg;
                }
            }

            Medication newMed = new Medication();
            newMed.Id = id;
            newMed.MedicineName = name;
            newMed.Type = type;
            newMed.Ingredients = dictionary;
            newMed.Format = formatBox.Text;
            medStorage.Save(newMed);

            MedicineValidationRequest validationRequest = new MedicineValidationRequest(id, docJmbg);
            MedicineValidationStorage validationStorage = new MedicineValidationStorage();
            validationStorage.GetAll().Add(validationRequest);
            validationStorage.SerializeValidationRequests();

            newFrame.Content = new MedicationsView();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            NameBox.Visibility = Visibility.Hidden;
            newFrame.Content = new MedicationsView();
        }

        private void Sastojci_Click(object sender, RoutedEventArgs e)
        {
            Ingredients i = new Ingredients(dictionary, Validation);
            i.ShowDialog();
        }

        private void NameBox_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
