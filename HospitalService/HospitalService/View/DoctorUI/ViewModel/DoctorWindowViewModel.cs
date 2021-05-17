using HospitalService.Repositories;
using HospitalService.Service;
using HospitalService.Storage;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class DoctorWindowViewModel : ViewModelClass
    {
        private Doctor doctor;
        private ObservableCollection<Appointment> appointments;
        private ObservableCollection<Patient> patients;
        private ObservableCollection<Medication> medicationsForApproval;
        private ObservableCollection<Medication> approvedMedications;

        public Doctor Doctor
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Appointment> Appointments
        {
            get { return appointments; }
            set
            {
                appointments = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Patient> Patients
        {
            get { return patients; }
            set
            {
                patients = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Medication> MedicationsForApproval
        {
            get { return medicationsForApproval; }
            set
            {
                medicationsForApproval = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Medication> ApprovedMedications
        {
            get { return approvedMedications; }
            set
            {
                approvedMedications = value;
                OnPropertyChanged();
            }
        }

        public DoctorWindowViewModel(Doctor loggedDoctor)
        {
            this.Doctor = loggedDoctor;
            this.Appointments = new ObservableCollection<Appointment>();
            this.Patients = new ObservableCollection<Patient>();
            this.MedicationsForApproval = new ObservableCollection<Medication>();
            this.ApprovedMedications = new ObservableCollection<Medication>();
            List<Appointment> todaysAppointments = new AppointmentsService().GetByDoctor(loggedDoctor, DateTime.Now);
            List<Patient> allPatients = new PatientsRepository().GetAll(); // prebaciti na servis
            List<MedicineValidationRequest> validationRequests = new MedicineValidationStorage().GetForDoctor(Doctor.Jmbg); // servis
            List<Medication> medications = new MedicationStorage().GetForApproval(validationRequests); // servis
            List<Medication> allMedications = new MedicationStorage().GetAllApproved(); // servis
            todaysAppointments.ForEach(Appointments.Add);
            allPatients.ForEach(Patients.Add);
            medications.ForEach(MedicationsForApproval.Add);
            allMedications.ForEach(ApprovedMedications.Add);
        }

        public void DateChanged(DateTime date)
        {
            List<Appointment> todaysAppointments = new AppointmentsService().GetByDoctor(Doctor, date);
            todaysAppointments.ForEach(Appointments.Add);
        }

    }
}
