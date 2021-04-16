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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;
using Storage;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for MoveAppointment.xaml
    /// </summary>
    public partial class MoveAppointment : Page
    {

        public Appointment A { get; set; }
        public DataGrid Table { get; set; }

        public AppointmentStorage baza { get; set; }

        public Patient pacijent { get; set; }

        public List<Appointment> termini { get; set; }

        public List<Room> sale { get; set; }
        public MoveAppointment(Appointment a, DataGrid t, AppointmentStorage st,Patient pa)
        {
            InitializeComponent();
            this.DataContext = this;
            A = a;
            Table = t;
            baza = st;
            pacijent = pa;
            termini = baza.GetAll();
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
            if ((st.Date - A.StartTime.Date).TotalDays <= 2)
            {
                int k = 1;
                for (int i = 0; i < termini.Count; i++) {

                    if ((DateTime.Compare(termini[i].StartTime, st) == 0 || DateTime.Compare(termini[i].EndTime, et) == 0) && termini[i].doctor.Jmbg.Equals(A.doctor.Jmbg)) {

                        k = 0;
                        break;
                    }
                    else if (st >= termini[i].StartTime && st < termini[i].EndTime && termini[i].doctor.Jmbg.Equals(A.doctor.Jmbg)) {

                        k = 0;
                        break;

                    } else if (et >= termini[i].StartTime && et < termini[i].EndTime && termini[i].doctor.Jmbg.Equals(A.doctor.Jmbg)) {

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
                    int l = 1;
                    for (int i = 0; i < termini.Count; i++)
                    {

                        if ((DateTime.Compare(termini[i].StartTime, st) == 0 || DateTime.Compare(termini[i].EndTime, et) == 0) && termini[i].room.Id.Equals(A.room.Id))
                        {

                            l = 0;
                            break;
                        }
                        else if (st >= termini[i].StartTime && st < termini[i].EndTime && termini[i].room.Id.Equals(A.room.Id))
                        {

                            l = 0;
                            break;

                        }
                        else if (et >= termini[i].StartTime && et < termini[i].EndTime && termini[i].room.Id.Equals(A.room.Id))
                        {

                            l = 0;
                            break;

                        }

                    }

                    if (l == 1)
                    {

                        baza.Move(A.Id, st, et, A.room);
                        Table.Items.Refresh();
                        this.NavigationService.Navigate(new ViewAppointment(pacijent));


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

                            baza.Move(A.Id, st, et,r);
                            Table.Items.Refresh();
                            this.NavigationService.Navigate(new ViewAppointment(pacijent));

                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vise od dva dana izmedju termina!");
                
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {

            this.NavigationService.Navigate(new ViewAppointment(pacijent));
        }
    }
}
