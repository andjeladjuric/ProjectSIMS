using HospitalService.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class AboutTreatmentViewModel : ViewModelClass
    {
        private string roomId;
        private int bedNum;
        private string reason;

        public string RoomId
        {
            get { return roomId; }
            set
            {
                roomId = value;
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

        public AboutTreatmentViewModel(HospitalTreatment treatment)
        {
            this.RoomId = treatment.RoomId;
            this.BedNum = treatment.BedNumber;
            this.Reason = treatment.Reason;
        }
    }
}
