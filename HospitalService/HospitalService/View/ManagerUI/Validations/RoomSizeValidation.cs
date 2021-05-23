using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.Validations
{
    public class RoomSizeValidation : ValidationRule
    {
        public double Min { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (value.ToString().Equals(String.Empty))
                    return new ValidationResult(false, "Polje ne sme biti prazno!");
                else if (double.Parse(value.ToString()) < Min)
                    return new ValidationResult(false, "Ne možete uneti 0!");
                else
                    return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Samo brojevi!");
            }
        }
    }

    public class FloorValidation : ValidationRule
    {
        public int Min { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (value.ToString().Equals(String.Empty))
                    return new ValidationResult(false, "Polje ne sme biti prazno!");
                else if (int.Parse(value.ToString()) < Min)
                    return new ValidationResult(false, "Ne možete uneti 0!");
                else
                    return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Samo celi brojevi!");
            }
        }
    }
}
