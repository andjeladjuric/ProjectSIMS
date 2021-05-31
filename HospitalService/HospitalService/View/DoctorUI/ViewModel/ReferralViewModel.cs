using HospitalService.Model;
using HospitalService.Service;
using HospitalService.Storage;
using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Validation;
using HospitalService.View.DoctorUI.Views;
using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace HospitalService.View.DoctorUI.ViewModel
{
    class ReferralViewModel : ValidationBase
    {
        public MedicalRecord MedicalRecord { get; set; }
        public string PatientsName { get; set; }
        public ObservableCollection<string> Departments { get; set; }
        public ReferralView ThisWindow { get; set; }
        private ObservableCollection<Doctor> doctors;
        private Doctor selectedDoctor;
        private DoctorType selectedDepartment;
        private bool isUrgent;
        private bool isEnabled;
        private string reason;

        public RelayCommand ApplyCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand GetDoctorsCommand { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }

        public ObservableCollection<Doctor> Doctors
        {
            get { return doctors; }
            set
            {
                doctors = value;
                OnPropertyChanged("Doctors");
            }
        }

        public Doctor SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged("SelectedDoctor");
            }
        }

        public DoctorType SelectedDepartment
        {
            get { return selectedDepartment; }
            set
            {
                selectedDepartment = value;
                OnPropertyChanged("SelectedDepartment");
            }
        }
        public bool IsUrgent
        {
            get { return isUrgent; }
            set
            {
                isUrgent = value;
                OnPropertyChanged("IsUrgent");
            }
        }
        public  bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }
        public string Reason
        {
            get { return reason; }
            set
            {
                reason = value;
                OnPropertyChanged("Reason");
            }
        }

        public ReferralViewModel(MedicalRecord medicalRecord, ReferralView window)
        {
            ThisWindow = window;
            this.MedicalRecord = medicalRecord;
            this.PatientsName = medicalRecord.Patient.Name + " " + medicalRecord.Patient.Surname;
            this.Departments = new ObservableCollection<string>();
            Enum.GetNames(typeof(DoctorType)).ToList().ForEach(Departments.Add);
            this.IsEnabled = false;
            this.IsUrgent = false;
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
            ApplyCommand = new RelayCommand(Executed_ApplyCommand,
              CanExecute_ApplyCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand,
             CanExecute_CancelCommand);
            GetDoctorsCommand = new RelayCommand(Executed_GetDoctorsCommand,
             CanExecute_GetDoctorsCommand);
        }

        public bool CanExecute_CancelCommand(object obj)
        {
            return true;
        }
        public bool CanExecute_ApplyCommand(object obj)
        {
            this.Validate();
            if (this.IsValid)
            {
                return true;
            }else
                return false;
        }
        public bool CanExecute_GetDoctorsCommand(object obj)
        {
            return true;
        }
        public void Executed_ApplyCommand(object obj)
        {

            Referral newReferral = new Referral()
            {
                DateOfIssue = DateTime.Now,
                Specialization = SelectedDepartment,
                Doctor = SelectedDoctor,
                IsUrgent = IsUrgent,
                Reason = Reason
            };
            MedicalRecord.Referrals.Add(newReferral);
            new MedicalRecordService().UpdateRecord(MedicalRecord); 
            ThisWindow.Close();
        
        }

        public void Executed_CancelCommand(object obj)
        {
            ThisWindow.Close();
        }

        public void Executed_GetDoctorsCommand(object obj)
        {
            this.Doctors = new ObservableCollection<Doctor>();
            List<Doctor> doctors = new DoctorService().GetByDepartment(SelectedDepartment); 
            doctors.ForEach(Doctors.Add);
            IsEnabled = true;
        }

        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }

        protected override void ValidateSelf()
        {
            if (this.SelectedDoctor == null)
                this.ValidationErrors["Doctor"] = "Obavezno polje";
            if (string.IsNullOrWhiteSpace(this.Reason))
                this.ValidationErrors["Reason"] = "Obavezno polje";
        }
    }
}
