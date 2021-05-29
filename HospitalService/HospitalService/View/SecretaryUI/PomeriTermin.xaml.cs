using Model;
using HospitalService.Service;
using HospitalService.Repositories;
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
    /// Interaction logic for PomeriTermin.xaml
    /// </summary>
    public partial class PomeriTermin : Window
    {
        Appointment appointment;
        public PomeriTermin(Appointment a)
        {
            appointment = a;
            List<Room> romms = new RoomFileStorage().GetAll();

            InitializeComponent();
            sala.ItemsSource = romms;
            vreme.StartTime = new TimeSpan(8, 0, 0);
            vreme.EndTime = new TimeSpan(20, 0, 0);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (sala.SelectedItem == null || datum.Value == null || !vreme.Value.HasValue)
            {
                MessageBox.Show("Morate uneti sva polja");
                return;
            }


            DateTime date = new DateTime(datum.Value.Value.Year, datum.Value.Value.Month, datum.Value.Value.Date.Day, vreme.Value.Value.Hour, 0, 0);
            if (date < DateTime.Now)
            {
                MessageBox.Show("Datum koji ste izabrali je prosao.");
                return;
            }
            else if (date.Hour < 8 || date.Hour > 21)
            {
                MessageBox.Show("Bolnica radi od 8h do 21h");
                return;
            }
            else if (date > appointment.StartTime.AddDays(2) || date < appointment.StartTime.AddDays(-2))
            {
                MessageBox.Show("Mozete pomeriti termin samo za dva dana ");
                return;
            }

            foreach (Appointment item in new AppointmentsRepository().GetAll())
            {
                if (item.StartTime == date && item.room.Id == ((Room)sala.SelectedItem).Id)
                {
                    MessageBox.Show("Izabrani termin je zauzet, molimo vas izaberite drugi");
                    return;
                }
                else if (item.doctor.Jmbg == appointment.doctor.Jmbg && item.StartTime == date)
                {
                    MessageBox.Show("Doktor je u datom terminu zauzet");
                    return;
                }
                else if (item.patient.Jmbg == appointment.patient.Jmbg && item.StartTime == date)
                {
                    MessageBox.Show("Pacijent je u datom terminu zauzet");
                    return;
                }
            }

            new AppointmentsRepository().Edit(appointment.Id, date, date.Add(new TimeSpan(1, 0, 0)), (Room)sala.SelectedItem);
            MessageBox.Show("Uspesno promenjen termin");
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
