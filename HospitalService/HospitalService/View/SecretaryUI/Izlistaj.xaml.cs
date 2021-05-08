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
using Storage;
using Model;

namespace HospitalService.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for Izlistaj.xaml
    /// </summary>
    public partial class Izlistaj : Window
    {
        public Izlistaj()
        {
            InitializeComponent();
            refreshView();
        }

        private void refreshView()
        {
            lvAppointments.ItemsSource = null;

            List<Appointment> temp = new AppointmentStorage().GetAll();
            List<Appointment> items = new List<Appointment>();
            List<SortNode> sNodes = new List<SortNode>();

           

            foreach (Appointment app in temp)
            {
                if (!app.isUrgent && app.StartTime > DateTime.Now)
                {
                    sNodes.Add(new SortNode(app));
                }
            }

            
            sNodes.Sort((x, y) => x.startNext.CompareTo(y.startNext));

            
            foreach(SortNode node in sNodes)
            {
                items.Add(node.appointment);
            }
            lvAppointments.ItemsSource = items;
        }

        private void bPomeri_Click(object sender, RoutedEventArgs e)
        {
            Appointment app = (Appointment)lvAppointments.SelectedItem;
            Appointment ret = new AppointmentStorage().findNextAvailable(app);
            if (ret != null)
            {
                MessageBox.Show("Novi vreme početka termina: " + ret.StartTime, "Uspešno pomeranje");
                AppointmentStorage storage = new AppointmentStorage();
                storage.Edit(app.Id, ret.StartTime, ret.EndTime, ret.room);
                refreshView();
            }
            else
                MessageBox.Show("Termin ne može da se pomeri!", "Greška");


        }

        private void bZakazi_Click(object sender, RoutedEventArgs e)
        {
            WindowSharedData shData = (WindowSharedData)this.DataContext;
            Appointment appointment = shData.appointment;
            DoctorType oblast = shData.type;
            Appointment ret = null;


            
            while(ret == null)
            {
                ret = new AppointmentStorage().createAppointment(appointment, oblast);
                if (ret!=null) break;
                else
                    appointment.setDates(appointment.StartTime.AddHours(1), shData.duration);

            }
            if(ret!=null)
            {
                new AppointmentStorage().storeAppointment(ret);
                MessageBox.Show("Uspešno ste zakazali hitni termin!\n" + "Vreme: " + appointment.StartTime, "Potvrda");
            }
            else
                MessageBox.Show("Nemoguće je zakazati termin!\n" + "Vreme: " + appointment.StartTime, "Nemoguće");
            this.Close();
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
