using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.Validations
{
    public class NameValidation : ValidationRule
    {
        Regex checkName = new Regex(@"[A-Za-z]+([\s][A-Za-z]*[1-9]*)*$");
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (value.ToString().Equals(String.Empty))
                {
                    return new ValidationResult(false, "Polje ne sme biti prazno!");
                }
                else if (!checkName.IsMatch(value.ToString()))
                {
                    return new ValidationResult(false, "Pogrešan format!");
                }


                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Nepoznata greška!");
            }
        }
    }

    public class ItemNameValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (value.ToString().Equals(String.Empty))
                {
                    return new ValidationResult(false, "Polje ne sme biti prazno!");
                }
                
                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Nepoznata greška!");
            }
        }
    }
}
