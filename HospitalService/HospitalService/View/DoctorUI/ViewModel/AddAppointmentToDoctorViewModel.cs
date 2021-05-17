using HospitalService.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class AddAppointmentToDoctorViewModel : ViewModelClass
    {
        private string nextId;
        private ObservableCollection<String> appointmentsType;
        private ObservableCollection<Patient> patients;
        private ObservableCollection<Room> rooms;

        public string NextId
        {
            get { return nextId; }
            set
            {
                nextId = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<String> AppointmentsType
        {
            get { return appointmentsType; }
            set
            {
                appointmentsType = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Patient> Patients
        {
            get { return patients; }
            set
            {
                patients = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Room> Rooms
        {
            get { return rooms; }
            set
            {
                rooms = value;
                OnPropertyChanged();
            }
        }

        public AddAppointmentToDoctorViewModel()
        {
            AppointmentsService appointmentService = new AppointmentsService();
            NextId = appointmentService.GetNextId();
            AppointmentsType = new ObservableCollection<string>();
            Enum.GetNames(typeof(AppointmentType)).ToList().ForEach(AppointmentsType.Add);
            Patients = new ObservableCollection<Patient>();
            new PatientStorage().GetAll().ForEach(Patients.Add); // servis
            new RoomFileStorage().GetAll().ForEach(Rooms.Add); // servis, slobodne sobe? ili validacija
        }

    }
}
