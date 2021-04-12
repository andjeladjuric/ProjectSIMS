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

namespace HospitalService.View.PatientUI
{
    /// <summary>
    /// Interaction logic for PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        private int colNum = 0;
        AppointmentStorage baza;
        RoomFileStorage sobe;
        Model.Patient pacijent;

        public List<Appointment> appointments
        {
            get;
            set;
        }
        public PatientWindow(Model.Patient pac)
        {
            InitializeComponent();
            this.DataContext = this;
            baza = new AppointmentStorage();
            appointments = baza.GetAll();
            sobe = new RoomFileStorage();
            pacijent = pac;
            tableBinding.ItemsSource = appointments;
        }

        private void odjava_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
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
