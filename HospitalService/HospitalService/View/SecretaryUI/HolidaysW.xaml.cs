using HospitalService.Model;
using HospitalService.Repositories;
using HospitalService.Service;
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

namespace HospitalService.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for Holidays.xaml
    /// </summary>

    public partial class HolidaysW : Window
    {
        public Doctor Doctor { get; set; }
        public DoctorService DrService { get; set; }
        public Doctors Window { get; set; }

        public HolidaysW(Doctor d, DoctorService ds, Doctors parent)
        {
            InitializeComponent();
            Doctor = d;
            DrService = ds;
            datekraj.Visibility = Visibility.Hidden;
            kraj.Visibility = Visibility.Hidden;
            datepocetak.Visibility = Visibility.Hidden;
            pocetak.Visibility = Visibility.Hidden;
            Window = parent;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void rbSd_Checked(object sender, RoutedEventArgs e)
        {
            pocetak.Content = "Izaberi datum: ";
            datepocetak.Visibility = Visibility.Visible;
            pocetak.Visibility = Visibility.Visible;
            datekraj.Visibility = Visibility.Hidden;
            kraj.Visibility = Visibility.Hidden;
        }

        private void rbGo_Checked(object sender, RoutedEventArgs e)
        {
            pocetak.Content = "Izaberi datum pocetka: ";
            kraj.Content = "Izaberi datum završetka: ";
            pocetak.Visibility = Visibility.Visible;
            kraj.Visibility = Visibility.Visible;
            datepocetak.Visibility = Visibility.Visible;
            datekraj.Visibility = Visibility.Visible;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (rbGo.IsChecked == true)
            {
               
                DateTime startDate = datepocetak.SelectedDate.Value.Date;
                if (startDate < DateTime.Now)
                {
                    MessageBox.Show("Datum koji ste izabrali je prosao.");
                    return;
                }
                DateTime endDate = datekraj.SelectedDate.Value.Date;
                if (endDate < startDate)
                {
                    MessageBox.Show("Pogrešan unos.");
                    return;
                }

                TimeSpan duration = endDate - startDate;
                int numberOfDays = duration.Days;


                if (numberOfDays <= Doctor.FreeDaysNum)
                {
                    Holidays h = new Holidays
                    {
                        Type = Model.HolidaysType.GodisnjiOdmor,
                        Doctor = Doctor,
                        Start = startDate,
                        End = endDate
                    };
                    if (Doctor.Holidays == null)
                    {
                        Doctor.Holidays = new List<Holidays>();
                        Doctor.Holidays.Add(h);
                    }

                    Doctor.Holidays.Add(h);
                    Doctor.FreeDaysNum -= numberOfDays;
                    Window.refresh();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Doktor je iskoristio sve slobodne dane");
                }
            }

            else if (rbSd.IsChecked == true)
            {
                DateTime startDate = datepocetak.SelectedDate.Value.Date;
                if (startDate < DateTime.Now)
                {
                    MessageBox.Show("Datum koji ste izabrali je prosao.");
                    return;
                }
                DateTime endDate = datepocetak.SelectedDate.Value.Date;
                if (endDate < startDate)
                {
                    MessageBox.Show("Pogrešan unos.");
                    return;
                }

                if (1 <= Doctor.FreeDaysNum)
                {
                    Holidays h = new Holidays
                    {
                        Type = Model.HolidaysType.SlobodanDan,
                        Doctor = Doctor,
                        Start = startDate,
                        End = endDate
                    };
                    if (Doctor.Holidays == null)
                    {
                        Doctor.Holidays = new List<Holidays>();
                        Doctor.Holidays.Add(h);
                    }

                    Doctor.Holidays.Add(h);
                    Doctor.FreeDaysNum --;
                    Window.refresh();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Doktor je iskoristio sve slobodne dane");
                }
            }

        }

        private void FreeDays_Click(object sender, RoutedEventArgs e)
        {
            new FreeDaysView(Doctor).Show();
        }

    }
}
