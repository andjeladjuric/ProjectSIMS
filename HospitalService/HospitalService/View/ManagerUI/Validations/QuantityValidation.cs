using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.Validations
{
    public class QuantityValidation : ValidationRule
    {
        public int Min { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (value.ToString().Equals(String.Empty))
                    return new ValidationResult(false, "Polje ne sme biti prazno!");
                else if (int.Parse(value.ToString()) < Min)
                    return new ValidationResult(false, "Količina ne može biti 0!");
                else
                    return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Nepoznata greška!");
            }
        }
    }

    public class MoveQuantityValidation : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (value.ToString().Equals(String.Empty))
                    return new ValidationResult(false, "Polje ne sme biti prazno!");
                else if (int.Parse(value.ToString()) < Min)
                    return new ValidationResult(false, "Količina ne može biti 0!");
                else if (int.Parse(value.ToString()) > Max)
                    return new ValidationResult(false, "Unesite manju količinu!");
                else
                    return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Nepoznata greška!");
            }
        }
    }
}
