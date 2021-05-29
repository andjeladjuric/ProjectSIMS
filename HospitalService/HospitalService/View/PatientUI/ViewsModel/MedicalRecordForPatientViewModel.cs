using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Navigation;
using HospitalService.Model;
using HospitalService.Service;
using HospitalService.View.PatientUI.Pages;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
   public class MedicalRecordForPatientViewModel:ViewModelPatientClass
    {

        private Diagnosis selectedDiagnosis;
        private ObservableCollection<Diagnosis> diagnosis;
        public ObservableCollection<Diagnosis> Diagnosis
        {
            get { return diagnosis; }
            set
            {
                diagnosis = value;
                OnPropertyChanged();
            }
        }


        public Diagnosis SelectedDiagnosis
        {
            get { return selectedDiagnosis; }
            set
            {
                selectedDiagnosis = value;
                OnPropertyChanged();
            }
        }

        public String IdMedicalRecord { get; set; }

        private MedicalRecordService medicalRecordService;
        public RelayCommand showDiagnosis { get; set; }
        private Patient patient;
        private NotesService notesService;
        private NavigationService navService;

        private void Execute_ShowDiagnosis(object obj) {
           
            Note note = notesService.getNoteByPatient(patient, SelectedDiagnosis);
            if (note != null)
            {
                navService.Navigate(new DiagnosisForPatient(SelectedDiagnosis, note));
                
            }
            else
            {
                navService.Navigate(new DiagnosisWithoutNote(selectedDiagnosis, patient));
                
            }
        }

        private bool CanExecute_Command(object obj) {
            return true;
        }
        public MedicalRecordForPatientViewModel(Patient patient, NavigationService navService) {

            this.patient = patient;
            this.navService = navService;
            medicalRecordService = new MedicalRecordService();
            MedicalRecord medicalRecord = medicalRecordService.GetOneByPatient(patient);
            List<Diagnosis> patientDiagnosis = medicalRecord.Diagnoses;
            Diagnosis = new ObservableCollection<Diagnosis>();
            patientDiagnosis.ForEach(Diagnosis.Add);
            notesService = new NotesService();
            showDiagnosis = new RelayCommand(Execute_ShowDiagnosis,CanExecute_Command);
            IdMedicalRecord = medicalRecord.Id;



        }
    }
}
