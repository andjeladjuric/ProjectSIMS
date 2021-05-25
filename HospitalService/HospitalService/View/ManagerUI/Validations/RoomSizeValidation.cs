using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
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

    public class RoomRenovationValidation : ValidationRule
    {
        public double Max { get; set; }
        public MaxSizeWrapper Wrapper { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (value.ToString().Equals(String.Empty))
                    return new ValidationResult(false, "Polje ne sme biti prazno!");
                if (Wrapper != null)
                {
                    if (double.Parse(value.ToString()) > Wrapper.Max)
                        return new ValidationResult(false, "Kvadratura mora biti manja!");
                }

                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Samo brojevi!");
            }
        }
    }

    public class MaxSizeWrapper : DependencyObject
    {
        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register("Max", typeof(double),
            typeof(MaxSizeWrapper), new FrameworkPropertyMetadata(double.MaxValue));

        public double Max
        {
            get { return (double)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }
    }

    public class MaxSizeProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new MaxSizeProxy();
        }

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(object),
            typeof(MaxSizeProxy), new PropertyMetadata(null));
    }

}
