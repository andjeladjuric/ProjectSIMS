using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class DoctorsReportViewModel : ViewModel
    {
        #region Fields
        public Doctor SelectedDoctor { get; set; }
        public Frame Frame { get; set; }
        private string doctor;
        public string DoctorsName
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged();
            }
        }
        private string patientsName;
        public string PatientsName
        {
            get { return patientsName; }
            set
            {
                patientsName = value;
                OnPropertyChanged();
            }
        }

        private DateTime start;
        public DateTime StartDate
        {
            get { return start; }
            set
            {
                start = value;
                OnPropertyChanged();
            }
        }

        private DateTime end;
        public DateTime EndDate
        {
            get { return end; }
            set
            {
                end = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Appointment> appointments;
        public ObservableCollection<Appointment> Appointments
        {
            get { return appointments; }
            set
            {
                appointments = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public MyICommand SelectionChanged { get; set; }
        public MyICommand GenerateReport { get; set; }
        public MyICommand CancelCommand { get; set; }
        #endregion

        #region Actions
        private void OnDateChanged()
        {
            AppointmentsService service = new AppointmentsService();
            Appointments = new ObservableCollection<Appointment>();

            foreach (Appointment a in service.GetAll())
            {
                if (a.doctor.Jmbg.Equals(SelectedDoctor.Jmbg) && a.StartTime >= StartDate && a.EndTime <= EndDate)
                {
                    a.patient.FullName = a.patient.Name + " " + a.patient.Surname;
                    Appointments.Add(a);
                }
            }
        }

        private void OnCancel()
        {
            this.Frame.NavigationService.Navigate(new DoctorsView());
        }
        
        private bool CanExecute()
        {
            return true;
        }
        #endregion

        #region Constructors
        public DoctorsReportViewModel(Doctor selected, Frame currentFrame)
        {
            this.Frame = currentFrame;
            this.SelectedDoctor = selected;
            this.DoctorsName = "Plan rada, Dr " + SelectedDoctor.Name + " " + SelectedDoctor.Surname;
            this.StartDate = DateTime.Today;
            this.EndDate = DateTime.Today;

            SelectionChanged = new MyICommand(OnDateChanged, CanExecute);
        }
        #endregion
    }
}
