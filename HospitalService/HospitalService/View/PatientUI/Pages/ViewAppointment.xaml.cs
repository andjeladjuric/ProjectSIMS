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
        AppointmentStorage appointmentStorage;
        RoomFileStorage rooms;
        public Patient patient { get; set; }

        public List<Appointment> appointments
        {
            get;
            set;
        }
        public ViewAppointment(Patient p)
        {
            InitializeComponent();
            this.DataContext = this;
            patient = p;
            appointmentStorage = new AppointmentStorage();
            appointments = appointmentStorage.getByPatient(patient);
            rooms = new RoomFileStorage();
            tableViewAppointment.ItemsSource = appointments;
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {

            Appointment a = (Appointment)tableViewAppointment.SelectedItem;
            if (a == null)
            {
                MessageBox.Show("Morate odabrati jedan termin!");
            }
            else
            { 
                appointmentStorage.Delete(a.Id);
                appointmentStorage.SetIds();
                tableViewAppointment.ItemsSource = null;
                tableViewAppointment.ItemsSource = appointmentStorage.getByPatient(patient);
                

            }
        }

        private void MoveClick(object sender, RoutedEventArgs e)
        {
            Appointment selectedAppointment = (Appointment)tableViewAppointment.SelectedItem;
            if (selectedAppointment == null)
            {
                MessageBox.Show("Morate odabrati jedan termin!");
            }
            else
            {
                List<Appointment> la = appointmentStorage.GetAll();
                List<Appointment> movedAppointments = la.Where(ap => ap.patient.Jmbg.Equals(patient.Jmbg) && ap.Status == Status.Moved).ToList();
                
                if (selectedAppointment.Status == Status.Moved || movedAppointments.Count >= 3)
                {
                    String st = "Termin je vec pomjeran, ne mozete ga pomjeriti ponovo" + "\n" + "(Moguce i da ste prekoracili dozvoljen broj pomjerenih termina!)";
                    MessageBox.Show(st);
                }
                else
                {
                    this.NavigationService.Navigate(new MoveAppointment(selectedAppointment, tableViewAppointment, appointmentStorage, patient));
                } 
            }
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            Appointment selectedAppointent = (Appointment)tableViewAppointment.SelectedItem;
            if (selectedAppointent == null)
            {
                MessageBox.Show("Morate odabrati jedan termin!");
            }
            else
            {
                EditAppointmentForPatient windowForEdit = new EditAppointmentForPatient(selectedAppointent, appointmentStorage, tableViewAppointment, rooms);
                windowForEdit.Show();
            }
        }

        private void PayClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
