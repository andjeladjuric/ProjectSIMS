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
    /// Interaction logic for EditPatient.xaml
    /// </summary>
    public partial class EditPatient : Window
    {
        public Model.Patient patient { get; set; }
        public PatientStorage storage { get; set; }
        public DataGrid Table { get; set; }
        public EditPatient(Patient p, PatientStorage ps, DataGrid dg)
        {
            InitializeComponent();
            patient = p;
            PatientType type = p.PatientType;
            List<String> ids = new List<String>();
            ids.Add("Guest");
            ids.Add("General");
            if (type == PatientType.Guest)
            {
                cbtype.ItemsSource = ids;
                cbtype.SelectedItem = patient.PatientType.ToString();
             }
            else
            {
                ids.Remove("Guest");
                cbtype.ItemsSource = ids;
                cbtype.SelectedItem = patient.PatientType.ToString();
            }
            storage = ps;
            Table = dg;
            editGrid.DataContext = this;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            String dat = date.Text;
            if (cbtype.Text == "General")
                storage.Edit(jmbg.Text, username.Text, password.Text, (DateTime?)Convert.ToDateTime(dat), phone.Text, address.Text, email.Text, PatientType.General);
            else
                storage.Edit(jmbg.Text, username.Text, password.Text, (DateTime?)Convert.ToDateTime(dat), phone.Text, address.Text, email.Text, PatientType.Guest);
            Table.Items.Refresh();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
