using HospitalService.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.Validations
{
    public class DateTimeValidation : ValidationRule
    {
        public TimeWrapper Wrapper { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex checkTime = new Regex(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");
            var time = value.ToString().Trim();
            DateService dateService = new DateService();

            try
            {
                if (time.Equals(String.Empty))
                {
                    return new ValidationResult(false, "Polje ne sme biti prazno!");
                }
                
                if (!checkTime.IsMatch(time))
                {
                    return new ValidationResult(false, "Pogrešan format!");
                }

                var enteredTime = Wrapper.Time.Add(TimeSpan.ParseExact(value.ToString(), "c", null));

                if (DateTime.Compare(enteredTime, DateTime.Now) <= 0)
                {
                    return new ValidationResult(false, "Vreme koje ste uneli je prošlo!");
                }

                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Nepoznata greška!");
            }

        }
    }

    public class TimeWrapper : DependencyObject
    {
        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(DateTime), typeof(TimeWrapper),
            null);

        public DateTime Time
        {
            get => (DateTime)GetValue(TimeProperty);
            set => SetValue(TimeProperty, value);
        }
    }

    public class TimeProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new TimeProxy();
        }

        public object Data
        {
            get => (object)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(object), typeof(TimeProxy), 
            new PropertyMetadata(null));
    }
}
