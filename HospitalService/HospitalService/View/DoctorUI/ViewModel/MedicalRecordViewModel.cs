using HospitalService.View.DoctorUI.Views;
using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace HospitalService.View.DoctorUI.ViewModel
{
    class MedicalRecordViewModel : ViewModelClass
    {
        public Patient Patient { get; set; }
        public Frame AllergiesFrame { get; set; }
        public string PatientName { get; set; }
        private bool male;
        public bool Famele { get; set; }
        private MedicalRecord medicalRecord;

        public bool Male
        {
            get { return male; }
            set
            {
                male = value;
                OnPropertyChanged();
            }

        }

        public MedicalRecord MedicalRecord
        {
            get { return medicalRecord; }
            set
            {
                medicalRecord = value;
                OnPropertyChanged();
            }
        }

        public MedicalRecordViewModel(Frame frame, Patient selected) 
        {
            this.AllergiesFrame = frame;
            this.Patient = selected;
            this.PatientName = selected.Name + " " + selected.Surname;
            if (selected.Gender.Equals(Gender.Female))
            {
                Male = false;
            }
            else {
                Male = true;
            }
            this.MedicalRecord = new MedicalRecordStorage().GetOne(Patient.medicalRecordId);
            this.AllergiesFrame.NavigationService.Navigate(new AllergiesView(MedicalRecord.Allergies,AllergiesFrame, MedicalRecord));
        }
    }
}
