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
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Window
    {
        public PatientStorage storage { get; set; }
        public DataGrid Table { get; set; }
        public AddPatient(PatientStorage ps, DataGrid dg)
        {
            InitializeComponent();
            rbGost.IsChecked = true;
            storage = ps;
            Table = dg;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            String dat = date.Text;
            int tip = 0;
            if (rbStalni.IsChecked == true) tip = 1;
            if (dat == "")
            {
                Patient p2 = new Patient
                {
                    Username = username.Text,
                    Password = password.Text,
                    PatientType = (PatientType)tip,
                    Name = name.Text,
                    Surname = surname.Text,
                    Jmbg = jmbg.Text,
                    Gender = (Gender)Convert.ToInt32(cbgender.SelectedIndex),
                    Address = address.Text,
                    Email = email.Text,
                    Phone = phone.Text,
                    DateOfBirth = null
                };
                storage.Save(p2);
            }
            else
            {
                Patient p1 = new Patient
                {
                    Username = username.Text,
                    Password = password.Text,
                    PatientType = (PatientType)tip,
                    Name = name.Text,
                    Surname = surname.Text,
                    Jmbg = jmbg.Text,
                    Gender = (Gender)Convert.ToInt32(cbgender.SelectedIndex),
                    Address = address.Text,
                    Email = email.Text,
                    Phone = phone.Text,
                    DateOfBirth = (DateTime?)Convert.ToDateTime(dat)
                };
                storage.Save(p1);
            }

            Table.Items.Refresh();
            this.Close();

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void rbGost_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rbStalni_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}