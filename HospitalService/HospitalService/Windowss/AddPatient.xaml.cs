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

namespace HospitalService.Windowss
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
            storage = ps;
            Table = dg;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            String dat = date.Text;
            if (dat == "")
            {
                Model.Patient p2 = new Model.Patient
                {
                    Username = username.Text,
                    Password = password.Text,
                    PatientType = (PatientType)Convert.ToInt32(cbtype.SelectedIndex),
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
                Model.Patient p1 = new Model.Patient
                {
                    Username = username.Text,
                    Password = password.Text,
                    PatientType = (PatientType)Convert.ToInt32(cbtype.SelectedIndex),
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

        
    }
}
