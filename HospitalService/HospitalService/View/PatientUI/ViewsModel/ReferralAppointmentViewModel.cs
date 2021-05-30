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
   public class ReferralAppointmentViewModel:ViewModelPatientClass
    {

        private Referral selectedPatientReferral;
        private ObservableCollection<Referral> patientReferrals;
        public ObservableCollection<Referral> PatientReferrals
        {
            get { return patientReferrals; }
            set
            {
                patientReferrals = value;
                OnPropertyChanged();
            }
        }


        public Referral SelectedPatientReferral
        {
            get { return selectedPatientReferral; }
            set
            {
                selectedPatientReferral = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand showDetails { get; set; }
        private NavigationService navigationService;
        private ReferralAppointment referralAppointment;
        private Patient patient;
        private MedicalRecordService medicalRecordService;
        private void Execute_ShowDiagnosis(object obj) {
            navigationService.Navigate(new ReferralAppointmentDetails(patient,SelectedPatientReferral,referralAppointment));
        }
        private bool CanExecute_Command(object obj) {
            return true;
        }
        public ReferralAppointmentViewModel(Patient patient,NavigationService navigationService,ReferralAppointment referralAppointment) {
            this.patient = patient;
            this.navigationService = navigationService;
            this.referralAppointment = referralAppointment;
            medicalRecordService = new MedicalRecordService();
            List<Referral> referralsForPatient = medicalRecordService.GetReferrals(patient);
            PatientReferrals = new ObservableCollection<Referral>();
            referralsForPatient.ForEach(PatientReferrals.Add);
            showDetails = new RelayCommand(Execute_ShowDiagnosis,CanExecute_Command);
        
        }
    }
}
