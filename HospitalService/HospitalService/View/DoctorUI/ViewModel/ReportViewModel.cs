using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class ReportViewModel : ViewModelClass
    {
        public ReportView Window { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
        public string PatientsName { get; set; }
        private DateTime startDate;
        private DateTime endDate;
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand ApplyCommand { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged();
            }

        }

        public ReportViewModel(MedicalRecord record, ReportView window)
        {
            this.MedicalRecord = record;
            this.Window = window;
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now;
            this.PatientsName = record.Patient.Name + " " + record.Patient.Surname;
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
            CancelCommand = new RelayCommand(Executed_CancelCommand,
              CanExecute_CancelCommand);
            ApplyCommand = new RelayCommand(Executed_ApplyCommand,
            CanExecute_ApplyCommand);
        }

        public bool CanExecute_CancelCommand(object obj)
        {
            return true;
        }

        public void Executed_CancelCommand(object obj)
        {
            this.Window.Close();
        }

        public bool CanExecute_ApplyCommand(object obj)
        {
            return true;
        }

        public void Executed_ApplyCommand(object obj)
        {
            new ReportWindowView(MedicalRecord, StartDate, EndDate).ShowDialog();
            this.Window.Close();
        }
        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }





    }
}
