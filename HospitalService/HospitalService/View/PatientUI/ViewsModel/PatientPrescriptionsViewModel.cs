using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Timers;
using System.Windows;
using HospitalService.Model;
using HospitalService.Service;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
   public class PatientPrescriptionsViewModel:ViewModelPatientClass
    {

        private ObservableCollection<Prescription> prescriptions;
        public ObservableCollection<Prescription> Prescriptions
        {
            get { return prescriptions; }
            set
            {
                prescriptions = value;
                OnPropertyChanged();
            }
        }
        private Prescription selectedPrescription;
        public Prescription SelectedPrescription
        {
            get { return selectedPrescription; }
            set
            {
                selectedPrescription = value;
                OnPropertyChanged();
            }
        }
        private MedicalRecordService medicalRecordService;

        public RelayCommand setReminder { get; set; }
        private Patient patient;
        private Timer medicationReminder;
        
        private String notification = "";

        private void Execute_SetReminder(object obj) {
            
            if (SelectedPrescription == null)
            {

                MessageBox.Show("Niste selektovali recept!");

            }
            else
            {

                DateTime prescriptionStartDate = SelectedPrescription.Start;
                medicationReminder = new System.Timers.Timer(15000); // in milliseconds - p.HowOften*3600000
                notification += "Uzmite lijek" + " " + SelectedPrescription.Medication.ToUpper() + "\n" + "Dodatne informacije: " + SelectedPrescription.AdditionalInfos;
                DateTime prescriptionExpiryDate = prescriptionStartDate.AddDays(SelectedPrescription.HowLong);
                medicationReminder.Elapsed += (sender, e) => showNotification(sender, e, notification, prescriptionExpiryDate);
                medicationReminder.Start();


            }

        }
        private bool CanExecute_Command(object obj) {
            return true;
        }

        static void showNotification(object sender, ElapsedEventArgs e, string theString, DateTime et)
        {
            if (DateTime.Compare(DateTime.Now, et) < 0)
            {
                MessageBox.Show(theString);

            }
        }
        public PatientPrescriptionsViewModel(Patient patient) {
            this.patient = patient;
            medicalRecordService = new MedicalRecordService();
            List<Prescription> patientPrescriptions = medicalRecordService.GetPrescriptions(patient);
            this.Prescriptions = new ObservableCollection<Prescription>();
            patientPrescriptions.ForEach(this.Prescriptions.Add);
            setReminder = new RelayCommand(Execute_SetReminder, CanExecute_Command);

        }
    }
}
