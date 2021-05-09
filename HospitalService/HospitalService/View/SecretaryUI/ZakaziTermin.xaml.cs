using HospitalService.Storage;
using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ZakaziTermin.xaml
    /// </summary>
    public partial class ZakaziTermin : Window
    {
        Appointment appointment = new Appointment();


        public ZakaziTermin(DateTime dt, string roomId)
        {
            appointment.StartTime = dt;
            appointment.EndTime = dt.AddHours(1);
            appointment.room = new RoomFileStorage().GetAll().Find(x => x.Id == roomId);


            InitializeComponent();
            start.Content = appointment.StartTime.TimeOfDay.ToString();
            end.Content = appointment.EndTime.TimeOfDay.ToString();
            doktori.ItemsSource = new DoctorStorage().GetAll();
            pacijenti.ItemsSource = new PatientStorage().GetAll();
            sala.Content = roomId;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = 0;
                foreach (Appointment item in new AppointmentStorage().GetAll())
                {
                    if (id < int.Parse(item.Id))
                    {
                        id = int.Parse(item.Id);
                    }
                }
                id++;
                appointment.Id = id.ToString();
                appointment.doctor = (Doctor)doktori.SelectedItem;
                appointment.patient = (Patient)pacijenti.SelectedItem;
                appointment.Type = (AppointmentType)Enum.Parse(typeof(AppointmentType), (string)tipovi.SelectionBoxItem);


                foreach (Appointment item in new AppointmentStorage().GetAll())
                {

                    if (item.doctor.Jmbg == appointment.doctor.Jmbg && item.StartTime == appointment.StartTime)
                    {
                        MessageBox.Show("Doktor je u datom terminu zauzet");
                        return;
                    }
                    else if (item.patient.Jmbg == appointment.patient.Jmbg && item.StartTime == appointment.StartTime)
                    {
                        MessageBox.Show("Pacijent je u datom terminu zauzet");
                        return;
                    }
                }

                new AppointmentStorage().Save(appointment);
                MessageBox.Show("Uspesno ste zakazali termin.");
                this.Close();
            }
            catch { MessageBox.Show("Niste uneli sva polja"); }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void doktori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
