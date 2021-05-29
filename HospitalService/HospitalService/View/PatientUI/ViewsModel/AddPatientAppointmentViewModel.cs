using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using HospitalService.Repositories;
using HospitalService.Service;
using HospitalService.View.PatientUI.Pages;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
   public class AddPatientAppointmentViewModel:ViewModelPatientClass
    {
        public String AppointmentId { get; set; }
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
        public String StartTimeAppointment { get; set; }
        public String EndTimeAppointment { get; set; }
        public RelayCommand confirmAddAppointment { get; set; }
        public RelayCommand cancelAddAppointment { get; set; }
        private DoctorService doctorService;
        private AppointmentsRepository appointmentsRepository;
        public List<Appointment> appointments { get; set; }
        public List<Room> rooms { get; set; }

        private void Execute_ConfirmAddAppointment(object obj) {


           
            DateTime startTime = Convert.ToDateTime(Date.ToShortDateString() + " " + StartTimeAppointment + ":00");
            DateTime endTime = Convert.ToDateTime(Date.ToShortDateString() + " " + EndTimeAppointment + ":00");
           



            if (!isDoctorAvailable(startTime, endTime, selectedDoctor))
            {

                MessageBox.Show("Doktor je zauzet!");
            }
            else
            {

                Room availableRoom = getFirstAvailableRoom(startTime, endTime);


                if (availableRoom == null)
                {
                    MessageBox.Show("Nema slobodnih sala!");

                }
                else
                {

                    if (moreThanTwoAppointmentsInOneDay(startTime))
                    {
                        MessageBox.Show("Vise od dva termina u jednom danu!");
                    }
                    else
                    {
                        Appointment newAppointment = new Appointment()
                        {
                            Id = AppointmentId,
                            StartTime = startTime,
                            EndTime = endTime,
                            Type = AppointmentType.Pregled,
                            doctor = selectedDoctor,
                            room = availableRoom,
                            patient = patient

                        };
                        appointmentsRepository.Save(newAppointment);
                        addPatientAppointment.NavigationService.Navigate(new ViewAppointment(patient));
                    }

                }




            }
        }

        private bool moreThanTwoAppointmentsInOneDay(DateTime startTime)
        {
            List<Appointment> la = appointmentsRepository.GetAll();
            List<Appointment> sameDateAppointments = la.Where(ap => ap.patient.Jmbg.Equals(patient.Jmbg) && startTime.ToShortDateString().Equals(ap.StartTime.ToShortDateString())).ToList();
            if (sameDateAppointments.Count > 1)
            {
                return true;
            }
            return false;
        }

        private Room getFirstAvailableRoom(DateTime startTime, DateTime endTime)
        {
            int isFindAvailableRoom = 0;
            int isRoomAvailable = 1;
            Room availableRoom = new Room();
            for (int i = 0; i < rooms.Count; i++)
            {
                isRoomAvailable = 1;
                for (int j = 0; j < appointments.Count; j++)
                {

                    if (DateTime.Compare(appointments[j].StartTime, startTime) == 0 || DateTime.Compare(appointments[j].EndTime, endTime) == 0)
                    {
                        if (appointments[j].room.Id.Equals(rooms[i].Id))
                        {
                            isRoomAvailable = 0;
                            break;
                        }

                    }
                    else if (startTime >= appointments[j].StartTime && startTime < appointments[j].EndTime)
                    {
                        if (appointments[j].room.Id.Equals(rooms[i].Id))
                        {
                            isRoomAvailable = 0;
                            break;
                        }
                    }
                    else if (endTime >= appointments[j].StartTime && endTime < appointments[j].EndTime)
                    {
                        if (appointments[j].room.Id.Equals(rooms[i].Id))
                        {
                            isRoomAvailable = 0;
                            break;
                        }
                    }

                }
                if (isRoomAvailable == 1)
                {
                    availableRoom = rooms[i];
                    isFindAvailableRoom = 1;
                    break;

                }
            }
            if (isFindAvailableRoom == 0)
            {
                return null;
            }
            return availableRoom;
        }

        private bool isDoctorAvailable(DateTime startTime, DateTime endTime, Doctor selectedDoctor)
        {

            for (int i = 0; i < appointments.Count; i++)
            {

                if ((DateTime.Compare(appointments[i].StartTime, startTime) == 0 || DateTime.Compare(appointments[i].EndTime, endTime) == 0) && appointments[i].doctor.Jmbg.Equals(selectedDoctor.Jmbg))
                {

                    return false;
                }
                else if (startTime > appointments[i].StartTime && startTime < appointments[i].EndTime && appointments[i].doctor.Jmbg.Equals(selectedDoctor.Jmbg))
                {

                    return false;

                }
                else if (endTime > appointments[i].StartTime && endTime < appointments[i].EndTime && appointments[i].doctor.Jmbg.Equals(selectedDoctor.Jmbg))
                {

                    return false;

                }

            }

            return true;
        }
        private void Execute_CancelAddAppointment(object obj) {

            addPatientAppointment.NavigationService.Navigate(new ViewAppointment(patient));
        }
        private bool CanExecute_Command(object obj) {
            return true;
        }
        private Patient patient;
        private AddPatientAppointment addPatientAppointment;
        public AddPatientAppointmentViewModel(Patient patient,AddPatientAppointment addPatientAppointment) {
            this.patient = patient;
            this.addPatientAppointment = addPatientAppointment;
            Date = DateTime.Now;
            AppointmentId = new AppointmentsService().GetNextId();
            doctorService = new DoctorService();
            List<Doctor> docttorsForAppointment = doctorService.GetAll();
            Doctors = new ObservableCollection<Doctor>();
            docttorsForAppointment.ForEach(this.Doctors.Add);
            appointmentsRepository = new AppointmentsRepository();
            appointments = appointmentsRepository.GetAll();
            RoomService roomService = new RoomService();
            rooms = roomService.GetAll();
            confirmAddAppointment = new RelayCommand(Execute_ConfirmAddAppointment,CanExecute_Command);
            cancelAddAppointment = new RelayCommand(Execute_CancelAddAppointment,CanExecute_Command);

        }
    }
}
