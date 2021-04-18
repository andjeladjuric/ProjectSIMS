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
    /// Interaction logic for DayAppointments.xaml
    /// </summary>
    public partial class DayAppointments : Window
    {
        public BindingList<CalendarHelper> ch { get; set; }
        DateTime dt;
        public DayAppointments(DateTime dt)
        {
            this.dt = dt;
            RefreshTable(dt);
            InitializeComponent();
            DataContext = this;
            datum.Content = dt.Date.ToShortDateString();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CalendarHelper o1 = (CalendarHelper)calendar.SelectedCells[0].Item;
                int index = calendar.SelectedCells[0].Column.DisplayIndex - 1;
                if (o1.Appointments[index].Item1 != "")
                {
                    Appointment a = o1.Appointments[index].Item2;
                    MessageBoxResult m = MessageBox.Show($"Datum: {a.StartTime.Date.ToShortDateString()}\nPocetak: {a.StartTime.TimeOfDay}\nKraj: {a.EndTime.TimeOfDay}\nSala: {o1.Name}\nTip: {a.Type}\nDoktor: {a.doctor.Name} {a.doctor.Surname}\nPacijent: {a.patient.Name} {a.patient.Surname}", "Zelite Pomeriti Termin?", MessageBoxButton.YesNo);
                    if (m == MessageBoxResult.Yes)
                    {
                        new PomeriTermin(a).ShowDialog();
                        this.Close();

                    }

                }
                else { MessageBox.Show("Termin je prazan!"); }
            }
            catch { this.Close(); }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //zakazi

            CalendarHelper o1 = (CalendarHelper)calendar.SelectedCells[0].Item;
            int index = calendar.SelectedCells[0].Column.DisplayIndex - 1;
            DateTime start = new DateTime(dt.Year, dt.Month, dt.Date.Day, index + 8, 0, 0);
            if (start < DateTime.Now)
            {
                MessageBox.Show("Datum koji ste izabrali je prosao.");
                return;
            }
            else if (o1.Appointments[index].Item1 != "")
            {
                MessageBox.Show("Termin je zauzet");
                return;
            }

            new ZakaziTermin(start, o1.Name).ShowDialog();

            this.Close();
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            CalendarHelper o1 = (CalendarHelper)calendar.SelectedCells[0].Item;
            int index = calendar.SelectedCells[0].Column.DisplayIndex - 1;
            if (o1.Appointments[index].Item1 == "")
            {
                MessageBox.Show("Termin je prazan, ne moze se obrisati");
                return;
            }
            Appointment a = o1.Appointments[index].Item2;
            new AppointmentStorage().Delete(a.Id);
            MessageBox.Show("Uspesno ste obrisali termin");
            this.Close();
        }

        private void RefreshTable(DateTime dt)
        {
            List<Appointment> temp = new AppointmentStorage().GetAll();
            Dictionary<string, List<Appointment>> dict = new Dictionary<string, List<Appointment>>();
            ch = new BindingList<CalendarHelper>();

            foreach (Room room in new RoomFileStorage().GetAll())
            {
                dict[room.Id] = new List<Appointment>();
            }

            foreach (Appointment item in temp)
            {
                if (dt.Date == item.StartTime.Date)
                {
                    dict[item.room.Id].Add(item);
                }
            }

            foreach (KeyValuePair<string, List<Appointment>> item in dict)
            {
                CalendarHelper tmp = new CalendarHelper(item.Key);

                foreach (Appointment appointment in item.Value)
                {
                    int idx = appointment.StartTime.Hour - 8;
                    tmp.Appointments[idx] = new Tuple<string, Appointment>("☑", appointment);
                }
                ch.Add(tmp);
            }
        }

    }
}
