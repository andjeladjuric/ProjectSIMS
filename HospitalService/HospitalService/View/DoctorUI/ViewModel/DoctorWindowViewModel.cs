using HospitalService.Repositories;
using HospitalService.Service;
using HospitalService.Storage;
using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class DoctorWindowViewModel : ViewModelClass
    {
        private Doctor doctor;
        private ObservableCollection<Appointment> appointments;
        private ObservableCollection<Patient> patients;
        private ObservableCollection<Medication> medicationsForApproval;
        private ObservableCollection<Medication> approvedMedications;
        public RelayCommand AddAppointmentCommand { get; set; }

        public RelayCommand KeyUpCommandWithKey { get; set; }

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
            AddAppointmentCommand = new RelayCommand(Executed_AddAppointmentCommand,
                CanExecute_AddAppointmentCommand);
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
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

        public void Executed_AddAppointmentCommand(object obj)
        {
            new AddAppointmentToDoctorView().ShowDialog();
        }

        public bool CanExecute_AddAppointmentCommand(object obj)
        {
            return true;
        }

        private void Executed_KeyDownCommandWithKey(object obj)
        {
            if (obj.Equals(Key.A))
                AddAppointmentCommand.Execute(obj);
        }
    }
}
