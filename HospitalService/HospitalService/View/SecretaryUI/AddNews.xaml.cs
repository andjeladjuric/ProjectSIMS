using Model;
using Storage;
using System;
using System.Collections.Generic;
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

namespace HospitalService.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for AddNews.xaml
    /// </summary>
    public partial class AddNews : Window
    {
        
        private List<Patient> allPatients;

        public AddNews()
        {
            InitializeComponent();
            

            //publishingDate.Content = DateTime.Now.ToShortDateString();
            targetGroupComboBox.ItemsSource = Enum.GetValues(typeof(Role)).Cast<Role>();
            allPatients = new PatientStorage().GetAll();
            foreach (Patient patient in allPatients)
            {
                specificPatientComboBox.Items.Add(patient.Name + patient.Surname);
            }
            specificPatientComboBox.IsEnabled = false;

        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            News newOne = new News();
            newOne.Title = titleText.Text;
            IFormatProvider provider = null;
            newOne.PublishingDate = DateTime.Now;
            newOne.Roles = (Role)targetGroupComboBox.SelectedItem;

            if ((Role)targetGroupComboBox.SelectedItem != Role.specificniPacijent)
            {
                newOne.specificPatient = null;
            }
            else
            {
                foreach (Patient patient in allPatients)
                {
                    if ((patient.Name + patient.Surname).Equals(specificPatientComboBox.SelectedItem.ToString()))
                    {
                        newOne.specificPatient = patient;
                    }
                }
            }

            newOne.Content = newsText.Text;

            NewsStorage.getInstance().Save(newOne);
            NewsWindow.getInstance().refreshListViewData();
            this.Hide();

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void targetGroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (targetGroupComboBox.SelectedItem.ToString().Equals(Role.specificniPacijent.ToString()))
            {
                specificPatientComboBox.IsEnabled = true;
            }
            else
            {
                specificPatientComboBox.IsEnabled = false;
            }
        }

    }
}
