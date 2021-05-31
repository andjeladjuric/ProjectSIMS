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
    /// Interaction logic for EditDoctor.xaml
    /// </summary>
    public partial class EditDoctor : Window
    {
        public Doctor doctor { get; set; }
        public DoctorService service { get; set; }
        public DataGrid Table { get; set; }

        public Doctors parent { get; set; }
        public EditDoctor(Doctor d, DoctorService ps, DataGrid dg, Doctors p)
        {
            InitializeComponent();
            doctor = d;
            service = ps;
            Table = dg;
            editGrid.DataContext = this;
            List<String> ids = new List<String>();
            ids.Add(doctor.DoctorType.ToString());
            cbtype.ItemsSource = ids;
            cbtype.SelectedItem = doctor.DoctorType.ToString();
            parent = p;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            String dat = date.Text;
            Doctor d1 = new Doctor
            {
                Name = doctor.Name,
                Surname = doctor.Surname,
                Jmbg = jmbg.Text,
                Username = username.Text,
                Password = password.Text,
                DateOfBirth = (DateTime?)Convert.ToDateTime(dat),
                Gender = doctor.Gender,
                Phone = phone.Text,
                Address = address.Text,
                Email = email.Text,
                DoctorType = doctor.DoctorType
            };

            service.Edit(d1);
            parent.refresh();
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
