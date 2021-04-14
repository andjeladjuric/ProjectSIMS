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
        public MoveAppointment(Appointment a, DataGrid t, AppointmentStorage st,Patient pa)
        {
            InitializeComponent();
            this.DataContext = this;
            A = a;
            Table = t;
            baza = st;
            pacijent = pa;
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

                baza.Move(A.Id, st, et);
                Table.Items.Refresh();
                this.NavigationService.Navigate(new ViewAppointment(pacijent));

            }
            else
            {
                MessageBox.Show("More than two days between appointments!");
                
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {

            this.NavigationService.Navigate(new ViewAppointment(pacijent));
        }
    }
}
