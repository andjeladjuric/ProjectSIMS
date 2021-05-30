using HospitalService.Model;
using HospitalService.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class ReportWindowViewModel : ViewModelClass
    {
        public MedicalRecord MedicalRecord { get; set; }
        public string PatientsName { get; set; }
        public string TimePeriod { get; set; }
        private ObservableCollection<Diagnosis> diagnoses;

        public ObservableCollection<Diagnosis> Diagnoses
        {
            get { return diagnoses; }
            set
            {
                diagnoses = value;
                OnPropertyChanged();
            }
        }

        public ReportWindowViewModel(MedicalRecord record, DateTime start, DateTime end)
        {
            this.MedicalRecord = record;
            this.PatientsName = record.Patient.Name + " " + record.Patient.Surname;
            this.TimePeriod = start.ToString("dd.MM.yyyy") + " - " + end.ToString("dd.MM.yyyy");
            List<Diagnosis> foundDiagnoses = new MedicalRecordService().GetForTimePeriod(MedicalRecord.Id, start, end);
            Diagnoses = new ObservableCollection<Diagnosis>();
            foundDiagnoses.ForEach(Diagnoses.Add);
        }
    }
}
