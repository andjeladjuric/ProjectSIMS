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

namespace HospitalService.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        private int colNum = 0;
        public AppointmentStorage baza { get; set; }
        public RoomFileStorage sobe { get; set; }
        public  Doctor doctor { get; set; }
        public List<Appointment> appointments

        {
            get;
            set;
        }
        public DoctorWindow(Doctor d)
        {
            InitializeComponent();
            this.DataContext = this;
            baza = new AppointmentStorage();
            doctor = d;
            appointments = baza.getByDoctor(doctor, DateTime.Now);
            sobe = new RoomFileStorage();

            AppointmentsTable.ItemsSource = appointments;
            datePicker.SelectedDate = DateTime.Now.Date;
        }

        public void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            appointments = baza.getByDoctor(doctor, (DateTime)datePicker.SelectedDate);
            AppointmentsTable.ItemsSource = appointments;
            AppointmentsTable.Items.Refresh();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void AddAppointment_Click(object sender, RoutedEventArgs e)
        {
            AddAppointmentToDoctor prozorDodavanje = new AddAppointmentToDoctor(this);
            prozorDodavanje.Show();
        }

        public void refresh()
        {
            appointments = baza.getByDoctor(doctor, (DateTime)datePicker.SelectedDate);
            AppointmentsTable.ItemsSource = appointments;
            AppointmentsTable.Items.Refresh();
        }
    }
}
