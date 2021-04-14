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

namespace HospitalService.View.PatientUI
{
    /// <summary>
    /// Interaction logic for AddAppointmentToPatient.xaml
    /// </summary>
    public partial class AddAppointmentToPatient : Window
    {
        public AppointmentStorage baza { get; set; }
        public DataGrid Table { get; set; }
        // public static List<String> appointmentsType = Enum.GetNames(typeof(AppointmentType)).ToList();
        public AddAppointmentToPatient(AppointmentStorage aps, DataGrid dg, RoomFileStorage sobe)
        {
            InitializeComponent();
            baza = aps;
            Table = dg;
            //AppointmentTypeBox.ItemsSource = appointmentsType;
            List<Room> r = sobe.GetAll();
            List<String> ids = new List<String>();
            Room soba;
            for (int i = 0; i < r.Count; i++)
            {
                soba = r[i];
                ids.Add(soba.Id);
            }

            RoomBox.ItemsSource = ids;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String start = StartBox.Text;
            String end = EndBox.Text;
            String date = DateBox.Text;
            String pocetak = date + " " + start + ":00";
            String kraj = date + " " + end + ":00";
            String ime = DoctorBox.Text;
            String[] name = ime.Split(' ');
            Doctor selectedDoctor = new Doctor() { Name = name[0], Surname = name[1] };
            Room selectedRoom = new Room() { Id = RoomBox.Text };
            Patient patient = new Patient() { Name = "Sladjana", Surname = "Colakovic" };

            Appointment newAppointment = new Appointment()
            {
                Id = IdBox.Text,
                StartTime = Convert.ToDateTime(pocetak),
                EndTime = Convert.ToDateTime(kraj),
                Type = AppointmentType.Pregled,
                doctor = selectedDoctor,
                room = selectedRoom,
                patient = patient

            };
            baza.Save(newAppointment);
            Table.Items.Refresh();
            this.Close();
        }
    }
}

