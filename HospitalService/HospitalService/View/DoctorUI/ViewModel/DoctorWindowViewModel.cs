using HospitalService.Repositories;
using HospitalService.Service;
using HospitalService.Storage;
using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Views;
using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class DoctorWindowViewModel : ViewModelClass
    {
        private Doctor doctor;
        private DateTime date;
        private ObservableCollection<Appointment> appointments;
        private ObservableCollection<Patient> patients;
        private ObservableCollection<Medication> medicationsForApproval;
        private ObservableCollection<Medication> approvedMedications;
        private Appointment selectedAppointment;
        private Medication selectedMedication;
        public RelayCommand AddAppointmentCommand { get; set; }
        public RelayCommand EditAppointmentCommand { get; set; }
        public RelayCommand DeleteAppointmentCommand { get; set; }
        public RelayCommand RefreshAppointmentsCommand { get; set; }
        public RelayCommand ValidateCommand { get; set; }
        public RelayCommand LogOutCommand { get; set; }
        public DoctorWindowView Window { get; set; }


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

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
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
        public Appointment SelectedAppointment
        {
            get { return selectedAppointment; }
            set
            {
                selectedAppointment = value;
                OnPropertyChanged();
            }
        }

        public Medication SelectedMedication
        {
            get { return selectedMedication; }
            set
            {
                selectedMedication = value;
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

        public DoctorWindowViewModel(Doctor loggedDoctor, DoctorWindowView doctorWindow)
        {
            AddAppointmentCommand = new RelayCommand(Executed_AddAppointmentCommand,
                CanExecute_AddAppointmentCommand);
            LogOutCommand = new RelayCommand(Executed_LogOutCommand,
                CanExecute_AddAppointmentCommand);
            RefreshAppointmentsCommand = new RelayCommand(Executed_RefreshAppointmentsCommand,
              CanExecute_AddAppointmentCommand);
            EditAppointmentCommand = new RelayCommand(Executed_EditAppointmentCommand,
               CanExecute_EditAppointmentCommand);
            DeleteAppointmentCommand = new RelayCommand(Executed_DeleteAppointmentCommand,
                CanExecute_EditAppointmentCommand);
            ValidateCommand = new RelayCommand(Executed_ValidateCommand,
                CanExecute_ValidateCommand);
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
            this.Window = doctorWindow;
            this.Doctor = loggedDoctor;
            this.Appointments = new ObservableCollection<Appointment>();
            this.Patients = new ObservableCollection<Patient>();
            this.MedicationsForApproval = new ObservableCollection<Medication>();
            this.ApprovedMedications = new ObservableCollection<Medication>();
            Date = DateTime.Now;
            List<Appointment> todaysAppointments = new AppointmentsService().GetByDoctor(loggedDoctor, Date);
            List<Patient> allPatients = new PatientsRepository().GetAll(); // prebaciti na servis
            List<MedicineValidationRequest> validationRequests = new MedicineValidationStorage().GetForDoctor(Doctor.Jmbg); // servis
            List<Medication> medications = new MedicationStorage().GetForApproval(validationRequests); // servis
            List<Medication> allMedications = new MedicationStorage().GetAllApproved(); // servis
            todaysAppointments.ForEach(Appointments.Add);
            allPatients.ForEach(Patients.Add);
            medications.ForEach(MedicationsForApproval.Add);
            allMedications.ForEach(ApprovedMedications.Add);
        }

       

        public void Executed_AddAppointmentCommand(object obj)
        {
            new AddAppointmentToDoctorView(this).ShowDialog();
        }

        public void Executed_LogOutCommand(object obj)
        {
            new MainWindow().Show();
            this.Window.Close();
        }

        public void Executed_RefreshAppointmentsCommand(object obj)
        {
            Refresh();
        }

        public void Executed_DeleteAppointmentCommand(object obj)
        {
            new AppointmentsService().Delete(SelectedAppointment.Id);
            Refresh();
        }

        public bool CanExecute_AddAppointmentCommand(object obj)
        {
            return true;
        }

        public void Executed_EditAppointmentCommand(object obj)
        {
            new EditAppointmentForDoctorView(SelectedAppointment, this).ShowDialog();
        }

        public bool CanExecute_EditAppointmentCommand(object obj)
        {
            if (SelectedAppointment == null)
            {
                MessageBox.Show("Morate izabrati termin");
                return false;
            }
            else
                return true;
        }

        private void Executed_KeyDownCommandWithKey(object obj)
        {
            if (obj.Equals(Key.A))
                AddAppointmentCommand.Execute(obj);
        }

        public void Refresh()
        {
            List<Appointment> todaysAppointments = new AppointmentsService().GetByDoctor(Doctor, Date);
            Appointments = new ObservableCollection<Appointment>();
            this.MedicationsForApproval = new ObservableCollection<Medication>();
            this.ApprovedMedications = new ObservableCollection<Medication>();
            todaysAppointments.ForEach(Appointments.Add);
            List<MedicineValidationRequest> validationRequests = new MedicineValidationStorage().GetForDoctor(Doctor.Jmbg); // servis
            List<Medication> medications = new MedicationStorage().GetForApproval(validationRequests); // servis
            List<Medication> allMedications = new MedicationStorage().GetAllApproved(); // servis
            todaysAppointments.ForEach(Appointments.Add);
            medications.ForEach(MedicationsForApproval.Add);
            allMedications.ForEach(ApprovedMedications.Add);
        }

        public bool CanExecute_ValidateCommand(object obj)
        {
            return true;
        }

        public void Executed_ValidateCommand(object obj)
        {
            new MedicineValidationView(this, SelectedMedication).ShowDialog();
        }
    }

}
