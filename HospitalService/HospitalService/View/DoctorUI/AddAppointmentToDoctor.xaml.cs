using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public partial class AddAppointmentToDoctor : Window
    {
        public DoctorWindow DoctorWindow { get; set; }
        public AppointmentStorage baza { get; set; }
        public PatientStorage patientStorage { get; set; }
        public List<Patient> Patients { get; set; }
        public static List<String> appointmentsType = Enum.GetNames(typeof(AppointmentType)).ToList();

        public AddAppointmentToDoctor(DoctorWindow dw)
        {
            DoctorWindow = dw;
            baza = dw.baza;
            patientStorage = new PatientStorage();
            Patients = patientStorage.GetAll();
            InitializeComponent();
            AppointmentTypeBox.ItemsSource = appointmentsType;
            List<Room> r = dw.sobe.GetAll();
            List<String> ids = new List<String>();
            Room soba;
            for (int i = 0; i < r.Count; i++)
            {
                soba = r[i];
                ids.Add(soba.Id);
            }

            RoomBox.ItemsSource = ids;
            DateBox.SelectedDate = DateTime.Today;
            PatientBox.ItemsSource = Patients;

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            String start = StartBox.Text;
            String end = EndBox.Text;
            String date = DateBox.Text;
            String pocetak = date + " " + start + ":00";
            String kraj = date + " " + end + ":00";
            String ime = PatientBox.Text;
            String[] name = ime.Split(' ');
            Patient selectedPatient = (Patient)PatientBox.SelectedItem;
            Room selectedRoom = new Room() { Id = RoomBox.Text }; // dodaji iz baze ili cuvaj samo id?
            Doctor defaultDoctor = DoctorWindow.doctor;
            if (baza.AlreadyExists(IdBox.Text))
                MessageBox.Show("Termin vec postoji.");
            else if (baza.IsTaken(Convert.ToDateTime(pocetak), Convert.ToDateTime(kraj), DoctorWindow.doctor))
                MessageBox.Show("Termin je zauzet.");
            else
            {
                Appointment newAppointment = new Appointment()
                {
                    Id = IdBox.Text,
                    StartTime = Convert.ToDateTime(pocetak),
                    EndTime = Convert.ToDateTime(kraj),
                    Type = (AppointmentType)AppointmentTypeBox.SelectedIndex,
                    patient = selectedPatient,
                    room = selectedRoom,
                    doctor = defaultDoctor
                };
                baza.Save(newAppointment);
                DoctorWindow.refresh();
                this.Close();
            }
        }
    }
}
