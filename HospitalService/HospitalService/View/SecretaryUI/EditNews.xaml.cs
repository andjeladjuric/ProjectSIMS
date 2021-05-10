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
    /// Interaction logic for EditNews.xaml
    /// </summary>
    public partial class EditNews : Window
    {
        private NewsStorage allNewsStorage = NewsStorage.getInstance();
        private List<Patient> allPatients;
        private News selectedNewsForEditing;

        public EditNews(News selectedNews)
        {
            InitializeComponent();

            selectedNewsForEditing = selectedNews;
            setTextFieldValues(selectedNewsForEditing);
        }

        private void setTextFieldValues(News selectedNewsForEditing)
        {
            titleText.Text = selectedNewsForEditing.Title;
            publishingDate.Content = selectedNewsForEditing.PublishingDate;

            roleComboBox.ItemsSource = Enum.GetValues(typeof(Role)).Cast<Role>();
            roleComboBox.SelectedItem = selectedNewsForEditing.Roles;
            newsText.Text = selectedNewsForEditing.Content;

            allPatients = new PatientStorage().GetAll();
            foreach (Patient patient in allPatients)
            {
                specificPatientComboBox.Items.Add(patient.Name + patient.Surname);
            }

            if (selectedNewsForEditing.Roles == Role.specificniPacijent)
            {
                specificPatientComboBox.SelectedItem = selectedNewsForEditing.specificPatient.Name + selectedNewsForEditing.specificPatient.Surname;
            }
            else
            {
                specificPatientComboBox.IsEnabled = false;
            }
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            News editedNews = new News();
            editedNews.Title = titleText.Text;
            editedNews.PublishingDate = DateTime.Now.Date;
            editedNews.Roles = (Role)roleComboBox.SelectedItem;

            if ((Role)roleComboBox.SelectedItem != Role.specificniPacijent)
            {
                editedNews.specificPatient = null;
            }
            else
            {
                foreach (Patient patient in allPatients)
                {
                    if ((patient.Name + patient.Surname).Equals(specificPatientComboBox.SelectedItem.ToString()))
                    {
                        editedNews.specificPatient = patient;
                    }
                }
            }

            editedNews.Content = newsText.Text;

            allNewsStorage.Update(editedNews, selectedNewsForEditing);
            NewsWindow.getInstance().refreshListViewData();
            this.Hide();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void roleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Role)roleComboBox.SelectedItem == Role.specificniPacijent)
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
