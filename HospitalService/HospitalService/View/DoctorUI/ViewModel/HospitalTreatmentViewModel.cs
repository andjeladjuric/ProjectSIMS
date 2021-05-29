using HospitalService.Model;
using HospitalService.Service;
using HospitalService.Storage;
using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Validation;
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
    public class HospitalTreatmentViewModel : ValidationBase
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
                OnPropertyChanged("Rooms");
            }

        }

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged("StartDate");
            }

        }
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged("EndDate");
            }

        }
        public DoctorType SelectedDepartment
        {
            get { return selectedDepartment; }
            set
            {
                selectedDepartment = value;
                OnPropertyChanged("SelectedDepartment");
            }

        }
        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                selectedRoom = value;
                OnPropertyChanged("SelectedRoom");
            }

        }
        public int BedNum
        {
            get { return bedNum; }
            set
            {
                bedNum = value;
                OnPropertyChanged("BedNum");
            }

        }
        public string Reason
        {
            get { return reason; }
            set
            {
                reason = value;
                OnPropertyChanged("Reason");
            }

        }
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }

        }
        public bool Enabled
        {
            get { return enabled; }
            set
            {
                enabled = value;
                OnPropertyChanged("Enabled");
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
            this.Validate();
            if (this.IsValid)
            {
                if(DateTime.Compare(StartDate, EndDate) >= 0)
                {
                    MessageBox.Show("Pogrešan unos datuma.");
                    return false;
                }else
                    return true;
            }
            return false;
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
            new MedicalRecordService().UpdateRecord(MedicalRecord); 
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
            new RoomService().GetByType(RoomType.PatientRoom).ForEach(Rooms.Add);
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
            this.BedNum = new RoomInventoryService().GetNextAvailableBed(SelectedRoom.Id);
            if (BedNum == 0)
                MessageBox.Show("Nema slobodnih kreveta za izabranu sobu.");
        }
        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }

        protected override void ValidateSelf()
        {
            if (SelectedRoom == null)
                this.ValidationErrors["SelectedRoom"] = "Obavezno polje";
            if (BedNum == 0)
                this.ValidationErrors["BedNum"] = "Obavezno polje";
            if (string.IsNullOrWhiteSpace(this.reason))
                this.ValidationErrors["TreatmentReason"] = "Obavezno polje";
        }
    }
}
