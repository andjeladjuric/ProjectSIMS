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
using HospitalService.Storage;
using Model;
using Storage;

namespace HospitalService.View.PatientUI
{
    /// <summary>
    /// Interaction logic for UrgentAppointment.xaml
    /// </summary>
    public partial class UrgentAppointment : Window
    {
        public Patient pacijent { get; set; }

        public List<Appointment> termini { get; set; }
        public List<Doctor> doktori { get; set; }
        public List<Room> sale { get; set; }

        public AppointmentStorage bazaTermina { get; set; }
        public UrgentAppointment(Patient pac)

        {
            InitializeComponent();
            this.DataContext = this;
            pacijent = pac;
            bazaTermina = new AppointmentStorage();
            termini = bazaTermina.GetAll();
            DoctorStorage bazaDoktora = new DoctorStorage();
            doktori = bazaDoktora.GetAll();
            RoomFileStorage bazaSala = new RoomFileStorage();
            sale = bazaSala.GetAll();
            
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            String start = tb1.Text;
            String end = tb2.Text;
            String date = dp.Text;
            String pocetak = date + " " + start + ":00";
            String kraj = date + " " + end + ":00";
            DateTime st = Convert.ToDateTime(pocetak);
            DateTime et = Convert.ToDateTime(kraj);

            int k = 0;
            int s = 1;
            Doctor d = new Doctor();
            for (int i = 0; i < doktori.Count; i++) {
                s = 1;
                for (int j = 0; j < termini.Count; j++) {

                    if (DateTime.Compare(termini[j].StartTime, st) == 0 || DateTime.Compare(termini[j].EndTime, et) == 0)
                    {
                        if (termini[j].doctor.Jmbg.Equals(doktori[i].Jmbg))
                        {
                           s = 0;
                           break;
                        }
                        
                    }
                    else if (st >= termini[j].StartTime && st < termini[j].EndTime)
                    {
                        if (termini[j].doctor.Jmbg.Equals(doktori[i].Jmbg))
                        {
                            s = 0;
                            break;
                        }      
                    }
                    else if (et >= termini[j].StartTime && et < termini[j].EndTime) {
                        if (termini[j].doctor.Jmbg.Equals(doktori[i].Jmbg))
                        {
                            s = 0;
                            break;
                        }       
                    }
                               
                }
                if (s == 1)
                {
                    d = doktori[i];
                    k = 1;
                    break;
                }
            }

            if (k == 0)
            {
                MessageBox.Show("Nema slobodnog doktora!");

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
                else
                {

                    int brojTermina = termini.Count;
                    int idTerm = brojTermina + 1;
                    String id = idTerm.ToString();

                    Appointment newAppointment = new Appointment()
                    {
                        Id = id,
                        StartTime = st,
                        EndTime = et,
                        Type = AppointmentType.Pregled,
                        doctor = d,
                        room = r,
                        patient = pacijent

                    };
                    bazaTermina.Save(newAppointment);
                    this.Close();

                }
            }




        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
