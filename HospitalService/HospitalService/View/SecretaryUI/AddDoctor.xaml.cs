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
    /// Interaction logic for AddDoctor.xaml
    /// </summary>
    public partial class AddDoctor : Window
    {
        public DoctorService service { get; set; }
        public DataGrid Table { get; set; }
        public Doctors parent { get; set; }
        public AddDoctor(DoctorService ds, DataGrid dg, Doctors window)
        {
            InitializeComponent();
            service = ds;
            Table = dg;
            parent = window;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            String dat = date.Text;
            int tip = 0;
            if (rbKar.IsChecked == true) tip = 0;
            else if (rbDer.IsChecked == true) tip = 1;
            else if (rbNeu.IsChecked == true) tip = 2;
            else if (rbPor.IsChecked == true) tip = 3;
            else if (rbPed.IsChecked == true) tip = 4;
            else if (rbHir.IsChecked == true) tip = 5;

            Doctor d1 = new Doctor
            {
                Username = username.Text,
                Password = password.Password,
                DoctorType = (DoctorType)tip,
                Name = name.Text,
                Surname = surname.Text,
                Jmbg = jmbg.Text,
                Gender = (Gender)Convert.ToInt32(cbgender.SelectedIndex),
                Address = address.Text,
                Email = email.Text,
                Phone = phone.Text,
                DateOfBirth = (DateTime?)Convert.ToDateTime(dat),
                FreeDaysNum = 30
            };
            
            service.Save(d1);
            parent.refresh();
            this.Close();
            
        }
    }
}
