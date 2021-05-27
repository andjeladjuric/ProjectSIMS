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
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Data;
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
        private ObservableCollection<News> news;
        private Appointment selectedAppointment;
        private Medication selectedMedication;
        private Patient selectedPatient;
        private News selectedNews;
        private string _filterString;
        private ICollectionView _dataGridCollection;
        public RelayCommand AddAppointmentCommand { get; set; }
        public RelayCommand RemoveNewsCommand { get; set; }
        public RelayCommand ShowNewsCommand { get; set; }
        public RelayCommand OpenRecordCommand { get; set; }
        public RelayCommand EditAppointmentCommand { get; set; }
        public RelayCommand DeleteAppointmentCommand { get; set; }
        public RelayCommand RefreshAppointmentsCommand { get; set; }
        public RelayCommand AboutMedicationCommand { get; set; }
        public RelayCommand ValidateCommand { get; set; }
        public RelayCommand LogOutCommand { get; set; }
        public DoctorWindowView Window { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }

        public ICollectionView DataGridCollection
        {
            get { return _dataGridCollection; }
            set { _dataGridCollection = value;
                OnPropertyChanged();
            }
        }

        public string FilterString
        {
            get { return _filterString; }
            set
            {
                _filterString = value;
                OnPropertyChanged();
                FilterCollection();
            }
        }

        public Doctor Doctor
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged();
            }
        }

        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                selectedPatient = value;
                OnPropertyChanged();
            }
        }

        public News SelectedNews
        {
            get { return selectedNews; }
            set
            {
                selectedNews = value;
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

        public ObservableCollection<News> News
        {
            get { return news; }
            set
            {
                news = value;
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
            OpenRecordCommand = new RelayCommand(Executed_OpenRecordCommand,
               CanExecute_OpenRecordCommand);
            AboutMedicationCommand = new RelayCommand(Executed_AboutMedicationCommand,
               CanExecute_AboutMedicationCommand);
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
            RemoveNewsCommand = new RelayCommand(Executed_RemoveNewsCommand,
               CanExecute_RemoveNewsCommand);
            ShowNewsCommand = new RelayCommand(Executed_ShowNewsCommand,
               CanExecute_ShowNewsCommand);
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
            this.Window = doctorWindow;
            this.Doctor = loggedDoctor;
            this.Appointments = new ObservableCollection<Appointment>();
            this.Patients = new ObservableCollection<Patient>();
            this.MedicationsForApproval = new ObservableCollection<Medication>();
            this.ApprovedMedications = new ObservableCollection<Medication>();
            this.News = new ObservableCollection<News>();
            Date = DateTime.Now;
            List<Appointment> todaysAppointments = new AppointmentsService().GetByDoctor(loggedDoctor, Date);
            List<Patient> allPatients = new PatientService().GetAll(); 
            List<MedicineValidationRequest> validationRequests = new MedicineValidationService().GetForDoctor(Doctor.Jmbg); 
            List<Medication> medications = new MedicationService().GetForApproval(validationRequests); 
            List<Medication> allMedications = new MedicationService().GetAllApproved(); 
            List<News> news = new NewsService().GetForRole(Role.doktori); 
            news.ForEach(News.Add);
            todaysAppointments.ForEach(Appointments.Add);
            allPatients.ForEach(Patients.Add);
            medications.ForEach(MedicationsForApproval.Add);
            allMedications.ForEach(ApprovedMedications.Add);
            DataGridCollection = CollectionViewSource.GetDefaultView(Patients);
            DataGridCollection.Filter = new Predicate<object>(Filter);
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

        public bool CanExecute_RemoveNewsCommand(object obj)
        {
            return true;
        }

        public void Executed_RemoveNewsCommand(object obj)
        {
            this.News = new ObservableCollection<News>();
        }

        public bool CanExecute_ShowNewsCommand(object obj)
        {
            return true;
        }

        public void Executed_ShowNewsCommand(object obj)
        {
            new NewsView(SelectedNews).ShowDialog();
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
          //  if (obj.Equals(Key.A))
            //    AddAppointmentCommand.Execute(obj);
        }

        public void Refresh()
        {
            List<Appointment> todaysAppointments = new AppointmentsService().GetByDoctor(Doctor, Date);
            Appointments = new ObservableCollection<Appointment>();
            this.MedicationsForApproval = new ObservableCollection<Medication>();
            this.ApprovedMedications = new ObservableCollection<Medication>();
            todaysAppointments.ForEach(Appointments.Add);
            List<MedicineValidationRequest> validationRequests = new MedicineValidationService().GetForDoctor(Doctor.Jmbg);
            List<Medication> medications = new MedicationService().GetForApproval(validationRequests);
            List<Medication> allMedications = new MedicationService().GetAllApproved();
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

        public bool CanExecute_AboutMedicationCommand(object obj)
        {
            return true;
        }

        public void Executed_AboutMedicationCommand(object obj)
        {
            new AboutMedicationView(this, SelectedMedication).ShowDialog();
        }
        public bool CanExecute_OpenRecordCommand(object obj)
        {
            if(SelectedPatient == null)
            {
                MessageBox.Show("Morate izabrati pacijenta.");
                return false;
            }else
                return true;
        }

        public void Executed_OpenRecordCommand(object obj)
        {
            new MedicalRecordView(SelectedPatient).ShowDialog();
        }

        public bool Filter(object obj)
        {
            var data = obj as Patient;
            if (data != null)
            {
                if (!string.IsNullOrEmpty(_filterString))
                {
                    return data.Name.ToLower().Contains(_filterString.ToLower().Trim()) || data.Surname.ToLower().Contains(_filterString.ToLower().Trim()) || data.Jmbg.Contains(_filterString.Trim()) || data.medicalRecordId.Contains(_filterString.Trim());
                }
                return true;
            }
            return false;
        }

        private void FilterCollection()
        {
            if (_dataGridCollection != null)
            {
                _dataGridCollection.Refresh();
            }
        }
        public void SetFilter(Predicate<object> filter)
        {
            if (DataGridCollection.CurrentItem != null && !filter(DataGridCollection.CurrentItem))
            {
                DataGridCollection = null;
                OnPropertyChanged("DataGridCollection");
            }
            DataGridCollection.Filter = filter;
            if (DataGridCollection.CurrentItem == null && !DataGridCollection.IsEmpty)
                DataGridCollection.MoveCurrentToFirst();
        }
    }


}
