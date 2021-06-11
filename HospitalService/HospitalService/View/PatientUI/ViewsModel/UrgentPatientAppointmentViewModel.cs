using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using HospitalService.Repositories;
using HospitalService.Service;
using HospitalService.View.PatientUI.Pages;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
    public class UrgentPatientAppointmentViewModel:ValidationBase
    {

        private DateTime date { get; set; }
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }
        public String SelectedTime { get; set; }
        public RelayCommand confirmAddAppointment { get; set; }
        public RelayCommand cancelAddAppointment { get; set; }
        private Patient patient;
        private PreferencesForAppointment preferencesForAppointment;
        private AddAppointmentContext addAppointmentContext;


        private void Execute_ConfirmAddAppointment(object obj) {
            this.Validate();
            if (IsValid)
            {
                String startTimeCb = SelectedTime.Split(" ")[1];
                int endTimeCb = int.Parse(startTimeCb.Split(":")[0]) + 1;

                DateTime startTime = Convert.ToDateTime(Date.ToShortDateString() + " " + startTimeCb);
                DateTime endTime = Convert.ToDateTime(Date.ToShortDateString() + " " + Convert.ToString(endTimeCb) + ":00");
         
                addAppointmentContext.setStrategy(new DatePriority());
                if (addAppointmentContext.addAppointment(startTime, endTime, null, patient))
                {
                    preferencesForAppointment.NavigationService.Navigate(new ViewAppointment(patient));
                }
            }
                   
        }

        private void Execute_CancelAddAppointment(object obj)
        {
            preferencesForAppointment.NavigationService.Navigate(new PreferencesForAppointment(patient));

        }
        private bool CanExecute_Command(object obj) {
            return true;
        }

        protected override void ValidateSelf()
        {
            DateTime startTime = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(SelectedTime))
            {
                String[] startTimeArray1 = SelectedTime.Split(" ");
                String startTimeCb = startTimeArray1[1];
                startTime = Convert.ToDateTime(Date.ToShortDateString() + " " + startTimeCb);
            }
            if (Date.Date < DateTime.Now.Date)
            {
                this.ValidationErrors["Date"] = "Datum koji birate je prosao.";
            }
            if (string.IsNullOrWhiteSpace(SelectedTime))
            {
                this.ValidationErrors["Start"] = "Odaberite vrijeme termina.";
            }
            else if (startTime <= DateTime.Now)
            {
                this.ValidationErrors["Start"] = "Termin koji birate je prosao.";
            }
        }

        public UrgentPatientAppointmentViewModel(Patient patient, PreferencesForAppointment preferencesForAppointment) {
            this.patient = patient;
            this.preferencesForAppointment = preferencesForAppointment;
            Date = DateTime.Now;
            addAppointmentContext = new AddAppointmentContext();          
            confirmAddAppointment = new RelayCommand(Execute_ConfirmAddAppointment, CanExecute_Command);
            cancelAddAppointment = new RelayCommand(Execute_CancelAddAppointment,CanExecute_Command);
        
        }
    }
}
