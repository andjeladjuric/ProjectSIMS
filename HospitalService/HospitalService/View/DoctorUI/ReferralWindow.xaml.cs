using HospitalService.Model;
using HospitalService.Storage;
using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HospitalService.View.DoctorUI
{
   
    public partial class ReferralWindow : Window
    {
        private Patient patient;
        private List<Doctor> doctors;
        private MedicalRecord activeMedicalRecord;


        public ReferralWindow(MedicalRecordDoctorWindow medicalRecord)
        {
            InitializeComponent();
            patient = medicalRecord.Karton.Patient;
            activeMedicalRecord = medicalRecord.Karton;
            PatientTB.Text = patient.Name + " " + patient.Surname;
            DepartmentOptions.ItemsSource = Enum.GetValues(typeof(DoctorType)).Cast<DoctorType>();
            DateOfIssueDP.SelectedDate = DateTime.Now;
        }

        private void DepartmentOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            doctors = new DoctorStorage().GetByDepartment((DoctorType)DepartmentOptions.SelectedItem);
            DoctorOptions.IsEnabled = true;
            DoctorOptions.ItemsSource = doctors;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecordStorage medicalRecords = new MedicalRecordStorage();
            Referral newReferral = new Referral()
            {
                DateOfIssue = (DateTime)DateOfIssueDP.SelectedDate,
                Specialization = (DoctorType)DepartmentOptions.SelectedItem,
                Doctor = (Doctor)DoctorOptions.SelectedItem,
                Urgent = (bool)YesButton.IsChecked ? true : false,
                Reason = ReasonTextBox.Text
            };
            activeMedicalRecord.Referrals.Add(newReferral);
            medicalRecords.Edit(activeMedicalRecord);
            this.Close();

        }
    }
}
