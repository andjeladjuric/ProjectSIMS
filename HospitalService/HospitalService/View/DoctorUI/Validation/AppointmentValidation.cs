using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HospitalService.View.DoctorUI.Validation
{
    public class AppointmentValidation : ValidationBase
    {
        private string patientsName;
        private String startTime;
        private String endTime;
        private Room room;
        private Patient patient;

        public string PatientsName
        {
            get { return patientsName; }
            set
            {
                patientsName = value;
                OnPropertyChanged("PatientsName");
            }
        }

        public String StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                OnPropertyChanged("StartTime");
            }
        }

        public String EndTime
        {
            get { return endTime; }
            set
            {
                endTime = value;
                OnPropertyChanged("EndTime");
            }
        }


        public Room Room
        {
            get { return room; }
            set
            {
                room = value;
                OnPropertyChanged("Room");
            }
        }

        public Patient Patient
        {
            get { return patient; }
            set
            {
                patient = value;
                OnPropertyChanged("Patient");
            }
        }

        public AppointmentValidation() { }

        protected override void ValidateSelf()
        {
            Regex regexTime = new Regex(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$");
            if (string.IsNullOrWhiteSpace(this.patientsName))
            {
                this.ValidationErrors["Patient"] = "Izaberite pacijenta.";
            }
            if (this.room == null)
            {
                this.ValidationErrors["Room"] = "Izaberite sobu.";
            }
            if (string.IsNullOrWhiteSpace(StartTime))
            {
                this.ValidationErrors["StartTime"] = "Obavezan format: 23:59";
            }
            else if(!regexTime.IsMatch(StartTime)) {
                this.ValidationErrors["StartTime"] = "Obavezan format: 23:59";
            }

            if (string.IsNullOrWhiteSpace(EndTime))
            {
                this.ValidationErrors["EndTime"] = "Obavezan format: 23:59";
            }
            else if (!regexTime.IsMatch(EndTime))
            {
                this.ValidationErrors["EndTime"] = "Obavezan format: 23:59";
            }
        }
    }
}
