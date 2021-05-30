using Model;
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
using System.Windows.Shapes;

namespace HospitalService.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for FreeDaysView.xaml
    /// </summary>
    public partial class FreeDaysView : Window
    {
        public Doctor doctor { get; set; }
        public FreeDaysView(Doctor d)
        {
            InitializeComponent();
            doctor = d;
            lvDays.ItemsSource = doctor.Holidays;

        }


        public void refreshListViewData()
        {
            lvDays.ItemsSource = null;
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
