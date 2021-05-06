using HospitalService.Storage;
using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HospitalService.View.ManagerUI
{
    /// <summary>
    /// Interaction logic for RoomRenovation.xaml
    /// </summary>
    public partial class RoomRenovation : Page
    {
        Room SelectedRoom { get; set; }
        ObservableCollection<Appointment> appointment { get; set; }
        public RoomRenovation(Room room)
        {
            InitializeComponent();
            this.DataContext = this;
            SelectedRoom = room;

            appointment = new ObservableCollection<Appointment>();
            AppointmentStorage storage = new AppointmentStorage();

            foreach (Appointment a in storage.GetAll())
            {
                if (a.StartTime >= DateTime.Now && a.room.Id.Equals(SelectedRoom.Id))
                {
                    appointment.Add(a);
                }
            }

            tableBinding.ItemsSource = appointment;
            IDBox.Text = SelectedRoom.Id;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            RenovationStorage renovationStorage = new RenovationStorage();
            DateTime startDate = Convert.ToDateTime(startPicker.Text);
            DateTime endDate = Convert.ToDateTime(endPicker.Text);

            foreach (Appointment a in new AppointmentStorage().GetAll())
            {
                if (a.StartTime.Date == startDate || a.StartTime.Date == endDate || a.EndTime.Date == startDate || a.EndTime.Date == endDate)
                {
                    MessageBox.Show("U datom periodu postoje zakazani termini!");
                    break;
                }
            }

            renovationStorage.Save(new Renovation(SelectedRoom.Id, startDate, endDate));
            renovationStorage.SerializeRenovations();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            newFrame.Content = new RoomsView();
        }
    }
}
