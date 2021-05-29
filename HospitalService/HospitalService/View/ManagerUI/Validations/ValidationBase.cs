using HospitalService.View.ManagerUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.View.ManagerUI.Validations
{
    public abstract class ValidationBase : ViewModel
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
