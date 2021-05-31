using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.View.DoctorUI.Validation
{
    public class AppointmentValidation : ValidationBase
    {
        private string patientsName;
        private DateTime startTime;
        private DateTime endTime;
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

        public DateTime StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                OnPropertyChanged("StartTime");
            }
        }

        public DateTime EndTime
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
            if (string.IsNullOrWhiteSpace(this.patientsName))
            {
                this.ValidationErrors["Patient"] = "Izaberite pacijenta.";
            }
            if (this.room == null)
            {
                this.ValidationErrors["Room"] = "Izaberite sobu.";
            }
        }
    }
}
