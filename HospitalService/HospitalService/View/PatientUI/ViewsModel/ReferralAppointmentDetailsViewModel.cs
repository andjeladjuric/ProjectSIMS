using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using HospitalService.Model;
using HospitalService.Service;
using HospitalService.View.PatientUI.Pages;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
    public class ReferralAppointmentDetailsViewModel:ValidationBase
    {
        
       public String SelectedTime { get; set; }
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
        private Doctor selectedDoctor;
        private ObservableCollection<Doctor> doctors;
        public ObservableCollection<Doctor> Doctors
        {
            get { return doctors; }
            set
            {
                doctors = value;
                OnPropertyChanged();
            }
        }
        public Doctor SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand confirmAddAppointment { get; set; }
        public RelayCommand cancelAddAppointment { get; set; }
        private Referral referral;
        private ReferralAppointment referralAppointment;
        private DoctorService doctorService;
        private AppointmentsService appointmentsService;
        private RoomService roomService;
        private Patient patient;

        private void Execute_ConfirmAddAppointment(object obj)
        {
            this.Validate();
            if (IsValid) { 
            
                String[] startTimeArray1 = SelectedTime.Split(" ");
                String startTimeCb = startTimeArray1[1];
                String[] startTimeArray2 = startTimeCb.Split(":");

                int endTimeCb = int.Parse(startTimeArray2[0]) + 1;
                String shortEndTime = Convert.ToString(endTimeCb);

                DateTime startTime = Convert.ToDateTime(Date.ToShortDateString() + " " + startTimeCb);
                DateTime endTime = Convert.ToDateTime(Date.ToShortDateString() + " " + shortEndTime + ":00");

                if (!doctorService.isDoctorAvailable(startTime, endTime, SelectedDoctor))
                {
                    MessageBox.Show("Doktor je zauzet!");
                    return;
                }
                Room availableRoom = roomService.getFirstAvailableRoom(startTime, endTime);
                if (availableRoom == null)
                {
                    MessageBox.Show("Nema slobodnih sala!");
                    return;
                }
                if (moreThanTwoAppointmentsInOneDay(startTime))
                {
                    MessageBox.Show("Vise od dva termina u jednom danu!");
                    return;
                }
                Appointment newAppointment = new Appointment() { Id = appointmentsService.GetNextId(), StartTime = startTime, EndTime = endTime, Type = AppointmentType.Pregled, doctor = SelectedDoctor, room = availableRoom, patient = patient };
                appointmentsService.Save(newAppointment);
                referralAppointment.NavigationService.Navigate(new ViewAppointment(patient));
            }
        }

        private bool moreThanTwoAppointmentsInOneDay(DateTime startTime)
        {
            int sameDateAppointments = appointmentsService.getNumberOfSameDateAppointments(patient, startTime);
            if (sameDateAppointments > 1)
            {
                return true;
            }
            return false;
        }

        private void Execute_CancelAddAppointment(object obj)
        {

            referralAppointment.NavigationService.Navigate(new ReferralAppointment(patient));
        }
        private bool CanExecute_Command(object obj)
        {
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
            if (SelectedDoctor == null)
            {
                this.ValidationErrors["Doctor"] = "Odaberite doktora.";
            }
        }

        public ReferralAppointmentDetailsViewModel(Patient patient,Referral referral, ReferralAppointment referralAppointment) {

            this.referral = referral;
            this.referralAppointment = referralAppointment;
            this.patient = patient;
            Date = DateTime.Now;
            appointmentsService = new AppointmentsService();
            roomService = new RoomService();
            
            doctorService = new DoctorService();
            Doctor doctorForAppointment = referral.Doctor;
            List<Doctor> doctorsForAppointment = new List<Doctor>();
            doctorsForAppointment.Add(doctorForAppointment);
            Doctors = new ObservableCollection<Doctor>();
            doctorsForAppointment.ForEach(Doctors.Add);

            confirmAddAppointment = new RelayCommand(Execute_ConfirmAddAppointment, CanExecute_Command);
            cancelAddAppointment = new RelayCommand(Execute_CancelAddAppointment, CanExecute_Command);
        }
    }
}
