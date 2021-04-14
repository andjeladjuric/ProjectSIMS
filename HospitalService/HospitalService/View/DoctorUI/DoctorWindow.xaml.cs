using Model;
using Storage;
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
        AppointmentStorage baza;
        RoomFileStorage sobe;
        private Doctor doctor;
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
            appointments = baza.GetAll();
            sobe = new RoomFileStorage();
            tableBinding.ItemsSource = appointments;
        }

        private void odjava_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void dodaj_Click(object sender, RoutedEventArgs e)
        {
            AddAppointmentToDoctor prozorDodavanje = new AddAppointmentToDoctor(baza, tableBinding, sobe);
            prozorDodavanje.Show();
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

        private void izmjeni_Click(object sender, RoutedEventArgs e)
        {
            Appointment a = (Appointment)tableBinding.SelectedItem;
            if (a == null)
            {
                MessageBox.Show("You must select one item");
            }
            else
            {
                EditAppointmentForDoctor prozorIzmjena = new EditAppointmentForDoctor(a, baza, tableBinding, sobe);
                prozorIzmjena.Show();
            }
        }
    }
}
