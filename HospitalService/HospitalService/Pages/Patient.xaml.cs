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
    /// Interaction logic for Patient.xaml
    /// </summary>
    public partial class Patient : Page
    {

        private int colNum = 0;
        AppointmentStorage baza;
        RoomFileStorage sobe;

        public List<Appointment> appointments
        {
            get;
            set;
        }
        public Patient()
        {
            InitializeComponent();
            this.DataContext = this;
            baza = new AppointmentStorage();
            appointments = baza.GetAll();
            sobe = new RoomFileStorage();
            tableBinding.ItemsSource = appointments;
        }

        private void odjava_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Start());
        }

        private void dodaj_Click(object sender, RoutedEventArgs e)
        {
            AddAppointmentToPatient prozorDodavanje = new AddAppointmentToPatient(baza, tableBinding, sobe);
            prozorDodavanje.Show();
        }

        private void izmjeni_Click(object sender, RoutedEventArgs e)
        {
            Appointment a = (Appointment)tableBinding.SelectedItem;
            if (a == null)
            {
                MessageBox.Show("You must select one item");
            }
            else
            {
                EditAppointmentForPatient prozorIzmjena = new EditAppointmentForPatient(a, baza, tableBinding, sobe);
                prozorIzmjena.Show();
            }
        }

        private void obrisi_Click(object sender, RoutedEventArgs e)
        {

            Appointment a = (Appointment)tableBinding.SelectedItem;
            if (a == null)
            {
                MessageBox.Show("You must select one item");
            }
            else
            {
                baza.Delete(a.Id);
                tableBinding.Items.Refresh();
            }

        }
    }
}
