using System;
using System.Collections.Generic;
using System.Text;
using HospitalService.View.PatientUI.ViewsModel;

namespace HospitalService.View.PatientUI
{
  public abstract  class ValidationBase: ViewModelPatientClass
    {
        public ValidationErrors ValidationErrors { get; set; }
        public bool IsValid { get; private set; }

        protected ValidationBase()
        {
            this.ValidationErrors = new ValidationErrors();
        }

        protected abstract void ValidateSelf();

        public void Validate()
        {
            this.ValidationErrors.Clear();
            this.ValidateSelf();
            this.IsValid = this.ValidationErrors.IsValid;
            this.OnPropertyChanged("IsValid");
            this.OnPropertyChanged("ValidationErrors");
        }
    }
}
