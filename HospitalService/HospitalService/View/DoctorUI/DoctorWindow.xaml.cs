using HospitalService.Storage;
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

namespace HospitalService.View.DoctorUI
{
   
    public partial class DoctorWindow : Window
    {
        private int colNum = 0;
        public AppointmentStorage baza { get; set; }
        public RoomFileStorage sobe { get; set; }
        public PatientStorage patientsBase { get; set; }
        public MedicalRecordStorage Records { get; set; }
        public Doctor doctor { get; set; }
        public List<Appointment> appointments { get; set; }
        public List<MedicineValidationRequest> validationRequests { get; set; }
        public List<Medication> medicationsForApproval { get; set; }

        public DoctorWindow(Doctor d)
        {
            InitializeComponent();
            this.DataContext = this;
            patientsBase = new PatientStorage();
            List<Patient> patients = patientsBase.GetAll();
            baza = new AppointmentStorage();
            doctor = d;
            appointments = baza.getByDoctor(doctor, DateTime.Now);
            sobe = new RoomFileStorage();
            Records = new MedicalRecordStorage();
            validationRequests = new MedicineValidationStorage().GetForDoctor(d.Jmbg);
            medicationsForApproval = new MedicationStorage().GetForApproval(validationRequests);

            AppointmentsTable.ItemsSource = appointments;
            PatientsTable.ItemsSource = patients;
            ForApprovalListView.ItemsSource = medicationsForApproval;
            datePicker.SelectedDate = DateTime.Now.Date;
            ApprovedMedsListView.ItemsSource = new MedicationStorage().GetAllApproved();
        }

        public void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.refresh();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void AddAppointment_Click(object sender, RoutedEventArgs e)
        {
            AddAppointmentToDoctor addAppointmentWindow = new AddAppointmentToDoctor(this);
            addAppointmentWindow.Show();
        }

        public void refresh()
        {
            appointments = baza.getByDoctor(doctor, (DateTime)datePicker.SelectedDate);
            AppointmentsTable.ItemsSource = appointments;
            AppointmentsTable.Items.Refresh();
            ForApprovalListView.ItemsSource = new MedicationStorage().GetForApproval(new MedicineValidationStorage().GetForDoctor(doctor.Jmbg));
            ForApprovalListView.Items.Refresh();
            ApprovedMedsListView.ItemsSource = new MedicationStorage().GetAllApproved();
            ApprovedMedsListView.Items.Refresh();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Appointment a = (Appointment)AppointmentsTable.SelectedItem;
            if (a == null)
            {
                MessageBox.Show("Morate izabrati termin.");
            }
            else
            { 
                var Result = MessageBox.Show("Da li ste sigurni da želite da obrišete termin?", "Brisanje termina", MessageBoxButton.YesNo);
                if (Result == MessageBoxResult.Yes)
                {
                    baza.Delete(a.Id);
                    baza.SetIds();
                    this.refresh();
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Appointment a = (Appointment)AppointmentsTable.SelectedItem;
            if (a == null)
            {
                MessageBox.Show("Morate izabrati termin.");
            }
            else
            {
                EditAppointmentForDoctor editAppointmentWindow = new EditAppointmentForDoctor(a, this);
                editAppointmentWindow.Show();
            }
        }

        private void MedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            Patient p = (Patient)PatientsTable.SelectedItem;
            if (p == null)
            {
                MessageBox.Show("Morate izabrati pacijenta.");
            }
            else
            {
                MedicalRecord mr = Records.GetOne(p.medicalRecordId);
                MedicalRecordDoctorWindow medicalRecordWindow = new MedicalRecordDoctorWindow(mr);
                medicalRecordWindow.ShowDialog();
            }
        }

        private void ForApprovalListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MedicineValidationWindow medicineValidationWindow = new MedicineValidationWindow((Medication)ForApprovalListView.SelectedItem, this);
            medicineValidationWindow.ShowDialog();
        }

        private void ApprovedMedsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AboutMedicationWindow medicationDetails = new AboutMedicationWindow((Medication)ApprovedMedsListView.SelectedItem);
            medicationDetails.ShowDialog();
        }
    }
}
