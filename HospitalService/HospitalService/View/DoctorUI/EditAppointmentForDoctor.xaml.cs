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
    public partial class EditAppointmentForDoctor : Window
    {
        public Appointment a { get; set; }
        public AppointmentStorage baza { get; set; }
        public DoctorWindow DoctorWindow { get; set; }

        public EditAppointmentForDoctor(Appointment ap, DoctorWindow dw)
        {
            InitializeComponent();
            DoctorWindow = dw;
            a = ap;
            baza = dw.baza;
            editGrid.DataContext = this;
            AppointmentDate.SelectedDate = a.StartTime.Date;
            IdTB.Text = a.Id;
            startTB.Text = a.StartTime.ToShortTimeString();
            endTB.Text = a.EndTime.ToShortTimeString();
            List<Room> r = dw.sobe.GetAll();
            List<String> ids = new List<String>();
            Room soba;
            for (int i = 0; i < r.Count; i++)
            {
                soba = r[i];
                ids.Add(soba.Id);
            }
            comboBox.ItemsSource = ids;
            comboBox.SelectedItem = a.room.Id;

        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String start = startTB.Text;
                String end = endTB.Text;
                String date = AppointmentDate.Text;
                String pocetak = date + " " + start;
                String kraj = date + " " + end;
                a.StartTime = Convert.ToDateTime(pocetak);
                a.EndTime = Convert.ToDateTime(kraj);
            }
            catch (Exception) { }
            a.room.Id = comboBox.Text;
            baza.Edit(a.Id, a.StartTime, a.EndTime, a.room);
            DoctorWindow.refresh();
            this.Close();

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}