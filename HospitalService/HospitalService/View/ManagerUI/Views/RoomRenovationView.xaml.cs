using HospitalService.Storage;
using HospitalService.View.ManagerUI.ViewModels;
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

namespace HospitalService.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for RoomRenovation.xaml
    /// </summary>
    public partial class RoomRenovationView : Page
    {
        RoomRenovationViewModel currentViewModel;
        public RoomRenovationView(Room room, bool DemoOn)
        {
            InitializeComponent();
            currentViewModel = new RoomRenovationViewModel(newFrame, room, DemoOn);
            this.DataContext = currentViewModel;
        }
        //Room SelectedRoom { get; set; }
        //ObservableCollection<Appointment> appointment { get; set; }
        //public RoomRenovationView(Room room)
        //{
        //    InitializeComponent();
        //    this.DataContext = this;
        //    SelectedRoom = room;

        //    appointment = new ObservableCollection<Appointment>();
        //    AppointmentStorage storage = new AppointmentStorage();

        //    foreach (Appointment a in storage.GetAll())
        //    {
        //        if (a.StartTime >= DateTime.Now && a.room.Id.Equals(SelectedRoom.Id))
        //        {
        //            appointment.Add(a);
        //        }
        //    }

        //    tableBinding.ItemsSource = appointment;
        //    IDBox.Text = SelectedRoom.Id;
        //}

        //private void save_Click(object sender, RoutedEventArgs e)
        //{
        //    RenovationStorage renovationStorage = new RenovationStorage();
        //    DateTime startDate = Convert.ToDateTime(startPicker.Text);
        //    DateTime endDate = Convert.ToDateTime(endPicker.Text);

        //    if (CheckDateEntry(startDate, endDate) && renovationStorage.CheckExistingRenovations(SelectedRoom.Id, startDate, endDate))
        //    {
        //        renovationStorage.Save(new Renovation(SelectedRoom.Id, startDate, endDate));
        //        renovationStorage.SerializeRenovations();
        //    }

        //    startPicker.Text = "";
        //    endPicker.Text = "";
        //    newFrame.NavigationService.Navigate(new RoomsView());
        //}

        //private bool CheckDateEntry(DateTime startDate, DateTime endDate)
        //{
        //    foreach (Appointment a in new AppointmentStorage().GetAll())
        //    {
        //        if (a.room.Id.Equals(SelectedRoom.Id))
        //        {
        //            if (DateTime.Compare(startDate.Date, a.StartTime.Date) <= 0 && DateTime.Compare(endDate.Date, a.StartTime.Date) >= 0)
        //            {
        //                MessageBox.Show("U datom periodu postoje zakazani termini!");
        //                return false;
        //            }
        //        }
        //    }

        //    if (DateTime.Compare(startDate, endDate) > 0)
        //    {
        //        MessageBox.Show("Neispravan unos datuma!");
        //        return false;
        //    }

        //    return true;
        //}

        //private void cancel_Click(object sender, RoutedEventArgs e)
        //{
        //    newFrame.Content = new RoomsView();
        //}
    }
}
