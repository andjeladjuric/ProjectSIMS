using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.Validations
{
    public class DateTimeValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex checkTime = new Regex(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");

            try
            {
                if (value.ToString().Equals(String.Empty))
                {
                    return new ValidationResult(false, "Polje ne sme biti prazno!");
                }
                else if (!checkTime.IsMatch(value.ToString()))
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
