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
using Storage;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for ViewAppointment.xaml
    /// </summary>
    public partial class ViewAppointment : Page
    {
        private int colNum = 0;
        AppointmentStorage baza;
        RoomFileStorage sobe;
        public Patient patient { get; set; }

        public List<Appointment> appointments
        {
            get;
            set;
        }
        public ViewAppointment(Patient pac)
        {
            InitializeComponent();
            this.DataContext = this;
            patient = pac;
            baza = new AppointmentStorage();
            appointments = baza.getByPatient(patient);
            sobe = new RoomFileStorage();
            tableViewAppointment.ItemsSource = appointments;
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {

            Appointment a = (Appointment)tableViewAppointment.SelectedItem;
            if (a == null)
            {
                MessageBox.Show("You must select one item");
            }
            else
            { 
                baza.Delete(a.Id);
                baza.SetIds();
                tableViewAppointment.ItemsSource = null;
                tableViewAppointment.ItemsSource = baza.getByPatient(patient);
                

            }
        }

        private void MoveClick(object sender, RoutedEventArgs e)
        {
            Appointment a = (Appointment)tableViewAppointment.SelectedItem;
            if (a == null)
            {
                MessageBox.Show("You must select an item");
            }
            else
            {

                this.NavigationService.Navigate(new MoveAppointment(a, tableViewAppointment, baza, patient));
                   
            }
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            Appointment a = (Appointment)tableViewAppointment.SelectedItem;
            if (a == null)
            {
                MessageBox.Show("You must select one item");
            }
            else
            {
                EditAppointmentForPatient prozorIzmjena = new EditAppointmentForPatient(a, baza, tableViewAppointment, sobe);
                prozorIzmjena.Show();
            }
        }

        private void PayClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
