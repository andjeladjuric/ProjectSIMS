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

namespace HospitalService.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for OperationWindow.xaml
    /// </summary>
    public partial class OperationWindow : Window
    {
        private Patient patient;
        private DateTime start;
        private DateTime end;

        public OperationWindow(Patient selectedPatient)
        {
            InitializeComponent();
            patient = selectedPatient;
            PatientTextBox.Text = patient.Name + " " + patient.Surname;
            OperationDate.SelectedDate = DateTime.Now;
            DepartmentOptions.ItemsSource = Enum.GetValues(typeof(DoctorType)).Cast<DoctorType>();

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            AppointmentStorage appointmentStorage = new AppointmentStorage();
            Appointment newAppointment = new Appointment()
            {
                Id = appointmentStorage.GetNextId(),
                StartTime = start,
                EndTime = end,
                Type = AppointmentType.Operacija,
                patient = patient,
                room = (Room)AvailableRooms.SelectedItem,
                doctor = (Doctor)AvailableDoctors.SelectedItem
            };
            appointmentStorage.Save(newAppointment);
            this.Close();

        }

        private void DepartmentOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            start = Convert.ToDateTime(OperationDate.Text + " " + OperationStart.Text + ":00");
            end = Convert.ToDateTime(OperationDate.Text + " " + OperationEnd.Text + ":00");
            List<Doctor> availableDoctors = new AppointmentStorage().GetAvailableDoctors((DoctorType)DepartmentOptions.SelectedItem, start, end);
            List<Room> availableRooms = new AppointmentStorage().GetAvailableRooms(RoomType.OperatingRoom, start, end);

            AvailableDoctors.ItemsSource = availableDoctors;
            AvailableDoctors.IsEnabled = true;
            AvailableRooms.ItemsSource = availableRooms;
            AvailableRooms.DisplayMemberPath = "Name";
        }
    }
}
