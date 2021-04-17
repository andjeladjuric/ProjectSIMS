using HospitalService.Storage;
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
        public RoomFileStorage sobe { get; set; }

        public DoctorStorage ds { get; set; }
        public Patient pacijent { get; set; }

        public List<Appointment> termini { get; set; }

        public List<Doctor> doktori { get; set; }
        
        public List<Room> sale { get; set; }
        public AddAppointmentToPatient(Patient pac)
        {
            InitializeComponent();


            sobe = new RoomFileStorage();
            sale = sobe.GetAll();
            baza = new AppointmentStorage();
            termini = baza.GetAll();
            pacijent = pac;
            ds = new DoctorStorage();
            doktori = ds.GetAll();
            
            DoctorBox.ItemsSource = doktori;
            
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
            DateTime st = Convert.ToDateTime(pocetak);
            DateTime et = Convert.ToDateTime(kraj);
            Doctor selectedDoctor = (Doctor)DoctorBox.SelectedItem;
            Patient patient = new Patient() { Name = pacijent.Name, Surname = pacijent.Surname, Jmbg=pacijent.Jmbg, Username=pacijent.Username,DateOfBirth=pacijent.DateOfBirth };

            int k = 1;
            for (int i = 0; i < termini.Count; i++)
            {

                if ((DateTime.Compare(termini[i].StartTime, st) == 0 || DateTime.Compare(termini[i].EndTime, et) == 0) && termini[i].doctor.Jmbg.Equals(selectedDoctor.Jmbg))
                {

                    k = 0;
                    break;
                }
                else if (st >= termini[i].StartTime && st < termini[i].EndTime && termini[i].doctor.Jmbg.Equals(selectedDoctor.Jmbg))
                {

                    k = 0;
                    break;

                }
                else if (et >= termini[i].StartTime && et < termini[i].EndTime && termini[i].doctor.Jmbg.Equals(selectedDoctor.Jmbg))
                {

                    k = 0;
                    break;

                }

            }
            if (k == 0)
            {

                MessageBox.Show("Doktor je zauzet!");
            }
            else
            {
                int f = 0;
                int v = 1;
                Room r = new Room();
                for (int i = 0; i < sale.Count; i++)
                {
                    v = 1;
                    for (int j = 0; j < termini.Count; j++)
                    {

                        if (DateTime.Compare(termini[j].StartTime, st) == 0 || DateTime.Compare(termini[j].EndTime, et) == 0)
                        {
                            if (termini[j].room.Id.Equals(sale[i].Id))
                            {
                                v = 0;
                                break;
                            }

                        }
                        else if (st >= termini[j].StartTime && st < termini[j].EndTime)
                        {
                            if (termini[j].room.Id.Equals(sale[i].Id))
                            {
                                v = 0;
                                break;
                            }
                        }
                        else if (et >= termini[j].StartTime && et < termini[j].EndTime)
                        {
                            if (termini[j].room.Id.Equals(sale[i].Id))
                            {
                                v = 0;
                                break;
                            }
                        }

                    }
                    if (v == 1)
                    {
                        r = sale[i];
                        f = 1;
                        break;

                    }
                }

                if (f == 0)
                {
                    MessageBox.Show("Nema slobodnih sala!");

                }
                else {

                    Appointment newAppointment = new Appointment()
                    {
                        Id = IdBox.Text,
                        StartTime =st,
                        EndTime = et,
                        Type = AppointmentType.Pregled,
                        doctor = selectedDoctor,
                        room = r,
                        patient = patient

                    };
                    baza.Save(newAppointment);
                    this.Close();


                }




            }

            
        }
    }
}

