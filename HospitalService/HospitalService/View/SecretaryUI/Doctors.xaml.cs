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
    /// Interaction logic for Doctors.xaml
    /// </summary>
    public partial class Doctors : Window
    {
        private DoctorService service;
        public List<Doctor> doctors { get; set; }
        public Doctors()
        {
            InitializeComponent();
            this.DataContext = this;

            service = new DoctorService();
            doctors = service.GetAll();
            tableBinding.ItemsSource = doctors;
        }

        public void refresh()
        {
            tableBinding.Items.Refresh();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddDoctor addWindow = new AddDoctor(service, tableBinding, this);
            addWindow.Show();
            
            
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Doctor d = (Doctor)tableBinding.SelectedItem;
            if (d == null)
            {
                MessageBox.Show("You must select one item");
            }
            else
            {
                EditDoctor windowEdit = new EditDoctor(d, service, tableBinding, this);
                windowEdit.Show();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Doctor doctor = (Doctor)tableBinding.SelectedItem;
            if (doctor == null)
            {
                MessageBox.Show("Niste selektovali doktora!");
            }
            else
            {
                service.Delete(doctor.Jmbg);
                tableBinding.Items.Refresh();
            }
        }

        private void Holidays_Click(object sender, RoutedEventArgs e)
        {
            Doctor doctor = (Doctor)tableBinding.SelectedItem;
            
            if (doctor == null)
            {
                MessageBox.Show("You must select one item!");
            }
            else
            {
                HolidaysW hWindow = new HolidaysW(doctor, service, this
                    );
                hWindow.Show();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
