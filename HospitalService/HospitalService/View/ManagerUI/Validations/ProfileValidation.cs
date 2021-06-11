using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.Validations
{
    public class ProfileValidation : ValidationRule
    {
        Regex checkName = new Regex(@"([\p{L}]+[\s]*[\p{L}]*[0-9]{1,3}(,[\s][\p{L}]+([\s][\p{L}]+)+)*$)");
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

    public class PhoneValidation : ValidationRule
    {
        Regex checkName = new Regex(@"[0-9]{3}/[0-9]{3,5}-[0-9]{3,5}$");
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

    public class EmailValidation : ValidationRule
    {
        Regex checkName = new Regex(@"[^@]+@[^\\.]+\\..+$");
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
}
