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
using HospitalService.Service;
using HospitalService.View.PatientUI.ViewsModel;
using Model;
using Storage;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for ViewAppointment.xaml
    /// </summary>
    public partial class ViewAppointment : Page
    {
        //private AppointmentsService appointmentsService;
        //AppointmentStorage appointmentStorage;
        //RoomFileStorage rooms;
       // public Patient Patient { get; set; }

        /*public List<Appointment> appointments
        {
            get;
            set;
        }*/

        private ViewAppointmentViewModel viewModel;
        public ViewAppointment(Patient patient)
        {
            InitializeComponent();
            viewModel = new ViewAppointmentViewModel(patient,this);
            this.DataContext = viewModel;
            //appointmentsService = new AppointmentsService();
            //Patient = patient;
            //appointmentStorage = new AppointmentStorage();
            //appointments = appointmentsService.getNotFinishedAppointments(Patient);
            //rooms = new RoomFileStorage();
            //tableViewAppointment.ItemsSource = appointments;
        }

        /*private void DeleteClick(object sender, RoutedEventArgs e)
        {

            Appointment selctedAppointment = (Appointment)tableViewAppointment.SelectedItem;
            if (selctedAppointment == null)
            {
                MessageBox.Show("Morate odabrati jedan termin!");
            }
            else
            { 
                appointmentsService.delete(selctedAppointment.Id);
                appointmentsService.SetIds();
                tableViewAppointment.ItemsSource = null;
                tableViewAppointment.ItemsSource = appointmentsService.getNotFinishedAppointments(Patient);
                

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
                int movedAppointments = appointmentsService.getNumberOfMovedAppointments(Patient);
                if (selectedAppointment.Status == Status.Moved || movedAppointments >= 3)
                {
                    String messageForMoving = "Termin je vec pomjeran, ne mozete ga pomjeriti ponovo" + "\n" + "(Moguce i da ste prekoracili dozvoljen broj pomjerenih termina!)";
                    MessageBox.Show(messageForMoving);
                }
                else
                {
                    this.NavigationService.Navigate(new MoveAppointment(selectedAppointment, tableViewAppointment, appointmentStorage, Patient));
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

        }*/
    }
}
