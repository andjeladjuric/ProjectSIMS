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

namespace HospitalService.View.ManagerUI
{
    /// <summary>
    /// Interaction logic for MedicationsView.xaml
    /// </summary>
    public partial class MedicationsView : Page
    {
        private MedicationStorage medStorage = new MedicationStorage();
        public ObservableCollection<Medication> meds { get; set; }
        public MedicationsView()
        {
            InitializeComponent();
            this.DataContext = this;

            meds = new ObservableCollection<Medication>();
            foreach (Medication m in medStorage.GetAll())
            {
                meds.Add(m);
            }

        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            newFrame.Content = new AddMedication();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            Medication m = (Medication)tableBinding.SelectedItem;
            if (m == null)
            {
                MessageBox.Show("Morate izabrati lek!");
            }
            else
            {
                medStorage.Delete(m.Id);
                meds.Remove(m);
            }
        }

        private void tableBinding_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Medication med = (Medication)tableBinding.SelectedItem;
            MedicationDetails m = new MedicationDetails(med, tableBinding);
            m.ShowDialog();
        }
    }
}
