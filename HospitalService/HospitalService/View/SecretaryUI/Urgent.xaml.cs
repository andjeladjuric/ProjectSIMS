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
using HospitalService.Storage;
using Model;
using Storage;

namespace HospitalService.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for Urgent.xaml
    /// </summary>
    public partial class Urgent : Window
    {


        public Appointment appointment = null;
        public Urgent()
        {
            InitializeComponent();
            rbStalni.IsChecked = true;
            initOblasti();

        }

        private void refreshPatients() { cbStalni.ItemsSource = null; cbStalni.ItemsSource = new PatientStorage().GetAll(); }



       
        private void rbGost_Checked(object sender, RoutedEventArgs e)
        {
            gridForGuests.Visibility = Visibility.Visible;
            cbStalni.IsEnabled = false;
        }

        
        private void rbStalni_Checked(object sender, RoutedEventArgs e)
        {
            refreshPatients();
            gridForGuests.Visibility = Visibility.Collapsed;
            cbStalni.IsEnabled = true;
        }



        private void initOblasti()
        {
            string[] oblasti = { "Kardiologija", "Dermatologija", "Neurologija", "Porodicni lekari", "Pedijatrija", "Hirurgija" };
            foreach(string item in oblasti) {
                cbOblast.Items.Add(item);
            }

        }


        private void Zakazi_Click(object sender, RoutedEventArgs e)
        {

            if (!checkArgs()) return;

            Patient patient = null;
            if (rbStalni.IsChecked == true)
            {
                patient = (Patient)cbStalni.SelectedItem;
            }
            else
            {
                patient = readNewPatient();
                if (patient == null) return;

            }

            DoctorType oblast = (DoctorType)cbOblast.SelectedIndex;
            AppointmentType tip = (AppointmentType)Enum.Parse(typeof(AppointmentType), (string)cbTip.SelectionBoxItem);
            int duration = Int16.Parse(txtDuration.Text);

           
            appointment = new Appointment();
            appointment.setDates(duration);
            appointment.Status = Status.Active;
            appointment.Type = tip;
            appointment.patient = patient;

        
            WindowSharedData shData = new WindowSharedData(new Appointment(appointment), oblast, duration);

            Appointment ret = null;

        
            if ((ret = new AppointmentStorage().createAppointment(appointment, patient, oblast)) == null)
            {
                if ((appointment.StartTime.AddHours(1)).Hour < Appointment.CLOSING_HOUR)
                {
                    appointment.setDates(appointment.StartTime.AddHours(1), duration);
                    ret = new AppointmentStorage().createAppointment(appointment, patient, oblast);
                }

            }

            if (ret==null)
            {
                Izlistaj novi = new Izlistaj
                {
                    DataContext = shData
                };
                MessageBox.Show("Ne postoji slobodan termin u narednih pola sata/sat!\n" +
                    "odaberite koje termine želite da pomerite, i kliknite dugme 'Zakaži' kada izvršite željena pomeranja!", "Obaveštenje");
                novi.ShowDialog();

            }
            else
            {
                new AppointmentStorage().storeAppointment(ret);
                MessageBox.Show("Uspesno ste zakazali hitni termin!\n" + "Vreme: " + appointment.StartTime, "Potvrda");
            }

        }


        private bool checkArgs()
        {
            if (rbStalni.IsChecked == true)
            {
                if (cbStalni.SelectedIndex == -1)
                {
                    MessageBox.Show("Odaberite pacijenta!", "Greska");
                    return false;

                }
            }
            else
            {
                if (tbName.Text == "" || tbSurname.Text == "" || tbJMBG.Text == "")
                {
                    MessageBox.Show("Popunite podatke za gosta!", "Greška!");
                    return false;
                }
            }

            if (cbOblast.SelectedIndex == -1)
            {
                MessageBox.Show("Odaberite oblast!", "Greska");
                return false;
            }
            if (cbTip.SelectedIndex == -1)
            {
                MessageBox.Show("Odaberite tip posete!", "Greska");
                return false;
            }
            if (!Int16.TryParse(txtDuration.Text, out short i) || i > Appointment.MAX_DURATION)
            {
                MessageBox.Show("Nije broj broj sati!", "Greska");
                return false;
            }
            return true;
        }

        
        private Patient readNewPatient()
        {
            Patient patient = new Patient();
            patient.Name = tbName.Text;
            patient.Surname = tbSurname.Text;
            patient.Gender = (Gender)Convert.ToInt32(cbGender.SelectedIndex);
            patient.Jmbg = tbJMBG.Text;
            patient.PatientType = 0;
            if ((new PatientStorage().GetOne(patient.Jmbg)) != null)
            {
                tbJMBG.Text = ""; MessageBox.Show("Već postoji stalni pacijent sa ovim matičnim brojem!"); return null;
            }
            new PatientStorage().Save(patient);
            return patient;
        }







       









    }
}
