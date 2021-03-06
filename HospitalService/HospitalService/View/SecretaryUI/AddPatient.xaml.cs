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
using HospitalService.Service;
using HospitalService.Repositories;

namespace HospitalService.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Window
    {
        public PatientService storage { get; set; }
        public DataGrid Table { get; set; }
        public AddPatient(PatientService ps, DataGrid dg)
        {
            InitializeComponent();
            rbNereg.IsChecked = true;
            storage = ps;
            Table = dg;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            String dat = date.Text;
            int tip = 0;
            if (rbReg.IsChecked == true) tip = 1;
            if (dat == "")
            {
                Patient p2 = new Patient
                {
                    Username = username.Text,
                    Password = password.Password,
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
                new PatientsRepository().Save(p2);
            }
            else
            {
                Patient p1 = new Patient
                {
                    Username = username.Text,
                    Password = password.Password,
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
                new PatientsRepository().Save(p1);
            }

            Table.Items.Refresh();
            this.Close();

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}