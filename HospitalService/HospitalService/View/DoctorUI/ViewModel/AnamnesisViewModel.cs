using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class AnamnesisViewModel : ViewModelClass
    {
        private string anamnesis;

        public string Anamnesis
        {
            get { return anamnesis; }
            set
            {
                anamnesis = value;
                OnPropertyChanged();
            }

        }

        public AnamnesisViewModel(string anamnesis)
        {
            this.Anamnesis = anamnesis;
        }
    }
}
