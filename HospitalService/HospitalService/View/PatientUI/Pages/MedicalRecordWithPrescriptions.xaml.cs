using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HospitalService.Model;
using Model;
using Storage;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for MedicalRecordWithPrescriptions.xaml
    /// </summary>
    public partial class MedicalRecordWithPrescriptions : Page
    {

        public Patient pacijent { get; set; }

        public List<Prescription> prescriptions { get; set; }

        public MedicalRecordStorage baza;

        public Timer timer;
        public Prescription p;
        public String notifikacija = "";
        public MedicalRecordWithPrescriptions(Patient p)
        {
            InitializeComponent();
            this.DataContext = this;
            pacijent = p;
            baza = new MedicalRecordStorage();
            prescriptions = baza.getByPatient(pacijent);
            tablePrescriptions.ItemsSource = prescriptions;
        }

        private void setReminder(object sender, RoutedEventArgs e)
        {
            
            p = (Prescription)tablePrescriptions.SelectedItem;
            if (p == null)
            {

                MessageBox.Show("Niste selektovali recept!");

            }
            else
            {
                
                DateTime dt = p.Start;
                timer = new System.Timers.Timer(15000); // in milliseconds - p.HowOften*3600000
                notifikacija += "Uzmite lijek" + " " + p.Medication.ToUpper() + "\n" + "Dodatne informacije: " + p.AdditionalInfos;
                DateTime et = dt.AddDays(p.HowLong);
                timer.Elapsed += (sender, e) => TimerMethod(sender, e, notifikacija, et);
                timer.Start();
               
                
            }

        }

        static void TimerMethod(object sender, ElapsedEventArgs e, string theString, DateTime et)
        {
            if (DateTime.Compare(DateTime.Now, et) < 0)
            {
                MessageBox.Show(theString);

            }
        }
    }
}
