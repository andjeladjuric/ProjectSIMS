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
using Model;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for Surveys.xaml
    /// </summary>
    public partial class Surveys : Page
    {
        public Patient patient { get; set; }
        public Surveys(Patient p)
        {
            InitializeComponent();
            patient = p;
        }

        private void ChooseDoctor(object sender, RoutedEventArgs e)
        {
            Ankete.Content = new DoctorChoice(patient,this);
        }
    }
}
