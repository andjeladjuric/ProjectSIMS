using HospitalService.Model;
using HospitalService.Storage;
using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Views;
using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class HospitalTreatmentViewModel : ViewModelClass
    {
        private DateTime startDate;
        private DateTime endDate;
        private DoctorType selectedDepartment;
        private Room selectedRoom;
        private int bedNum;
        private string reason;
        private bool isEnabled;
        private bool enabled;
        private ObservableCollection<Room> rooms;
        public ObservableCollection<string> Departments { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
        public MedicalRecordViewModel ParentWindow { get; set; }
        public HospitalTreatmentView Window { get; set; }
        public string PatientsName { get; set; }
        public RelayCommand ApplyCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand GetRoomsCommand { get; set; }
        public RelayCommand GetBedsCommand { get; set; }
        public RelayCommand BedCommand { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }

        public ObservableCollection<Room> Rooms
        {
            get { return rooms; }
            set
            {
                rooms = value;
                OnPropertyChanged();
            }

        }

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged();
            }

        }
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged();
            }

        }
        public DoctorType SelectedDepartment
        {
            get { return selectedDepartment; }
            set
            {
                selectedDepartment = value;
                OnPropertyChanged();
            }

        }
        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                selectedRoom = value;
                OnPropertyChanged();
            }

        }
        public int BedNum
        {
            get { return bedNum; }
            set
            {
                bedNum = value;
                OnPropertyChanged();
            }

        }
        public string Reason
        {
            get { return reason; }
            set
            {
                reason = value;
                OnPropertyChanged();
            }

        }
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged();
            }

        }
        public bool Enabled
        {
            get { return enabled; }
            set
            {
                enabled = value;
                OnPropertyChanged();
            }

        }

        public HospitalTreatmentViewModel(MedicalRecordViewModel parent, HospitalTreatmentView window)
        {
            this.Window = window;
            this.ParentWindow = parent;
            this.MedicalRecord = parent.MedicalRecord;
            this.PatientsName = MedicalRecord.Patient.Name + " " + MedicalRecord.Patient.Surname;
            this.Departments = new ObservableCollection<string>();
            Enum.GetNames(typeof(DoctorType)).ToList().ForEach(Departments.Add);
            this.IsEnabled = false;
            this.Enabled = false;
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now;
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
            ApplyCommand = new RelayCommand(Executed_ApplyCommand,
              CanExecute_ApplyCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand,
            CanExecute_CancelCommand);
            GetRoomsCommand = new RelayCommand(Executed_GetRoomsCommand,
           CanExecute_GetRoomsCommand);
            GetBedsCommand = new RelayCommand(Executed_GetBedsCommand,
            CanExecute_GetBedsCommand);
            BedCommand = new RelayCommand(Executed_BedCommand,
          CanExecute_BedCommand);
        }

        public bool CanExecute_CancelCommand(object obj)
        {
            return true;
        }

        public void Executed_CancelCommand(object obj)
        {
            this.Window.Close();
        }

        public bool CanExecute_ApplyCommand(object obj)
        {
            if (SelectedRoom == null || BedNum == 0 || Reason == null)
            {
                MessageBox.Show("Obavezna polja nisu popunjena.");
                return false;
            }
            return true;
        }

        public void Executed_ApplyCommand(object obj)
        {
            HospitalTreatment newHospitalTreatment = new HospitalTreatment()
            {
                StartDate = this.StartDate,
                EndTime = this.EndDate,
                Department = this.SelectedDepartment,
                RoomId = this.SelectedRoom.Id,
                BedNumber = this.BedNum,
                Reason = this.Reason
            };
            this.MedicalRecord.HospitalTreatments.Add(newHospitalTreatment);
            new MedicalRecordStorage().Edit(MedicalRecord);
            this.ParentWindow.Refresh();
            this.Window.Close();
        }

        public bool CanExecute_GetRoomsCommand(object obj)
        {
            return true;
        }

        public void Executed_GetRoomsCommand(object obj)
        {
            this.Rooms = new ObservableCollection<Room>();
            new RoomFileStorage().getByType(RoomType.PatientRoom).ForEach(Rooms.Add);
            this.IsEnabled = true;
        }

        public bool CanExecute_GetBedsCommand(object obj)
        {
            return true;
        }

        public void Executed_GetBedsCommand(object obj)
        {
            this.Enabled = true;
        }

        public bool CanExecute_BedCommand(object obj)
        {
            return true;
        }

        public void Executed_BedCommand(object obj)
        {
            this.BedNum = new RoomInventoryStorage().GetNextAvailableBed(SelectedRoom.Id);
            if (BedNum == 0)
                MessageBox.Show("Nema slobodnih kreveta za izabranu sobu.");
        }
        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }
    }
}
