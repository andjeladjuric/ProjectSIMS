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

namespace HospitalService.Pages
{
    /// <summary>
    /// Interaction logic for Start.xaml
    /// </summary>
    public partial class Start : Page
    {
        public Start()
        {
            InitializeComponent();
        }

        private void patient_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Patient());
        }

        private void menager_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Manager());
        }

        private void doctor_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Doctor());
        }

        private void secretary_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Secretary());
        }
    }
}
