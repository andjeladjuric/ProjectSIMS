using Model;
using Storage;
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
    /// Interaction logic for AddMedicalRecord.xaml
    /// </summary>
    public partial class AddMedicalRecord : Window
    {
        public Patient patient { get; set; }
        public MedicalRecordStorage store { get; set; }
        public PatientStorage stor { get; set; }
        public AddMedicalRecord(MedicalRecordStorage mrs, Patient p, PatientStorage ps)
        {
            InitializeComponent();
            store = mrs;
            patient = p;
            stor = ps;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String Id = id.Text;
            store.Save(new MedicalRecord{ Id = Id, Patient = patient });
            stor.addRecord(patient.Jmbg, Id);
            this.Close();
        }
    }
}
