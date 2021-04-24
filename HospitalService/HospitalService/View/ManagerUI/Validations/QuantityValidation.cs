using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
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
        public MaxInventoryWrapper Wrapper { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (value.ToString().Equals(String.Empty))
                    return new ValidationResult(false, "Polje ne sme biti prazno!");
                
                if (Int32.Parse(value.ToString()) < Min)
                    return new ValidationResult(false, "Količina ne može biti 0!");

                if (Wrapper != null)
                {
                    if (Int32.Parse(value.ToString()) > Wrapper.Max)
                        return new ValidationResult(false, "Unesite manju količinu!");
                }

                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Samo celi brojevi!");
            }
        }
    }

    public class MaxInventoryWrapper : DependencyObject
    {
        public static readonly DependencyProperty MaxInventoryProperty = DependencyProperty.Register("Max", typeof(int),
            typeof(MaxInventoryWrapper), new FrameworkPropertyMetadata(0));

        public int Max
        {
            get { return (int)GetValue(MaxInventoryProperty); }
            set { SetValue(MaxInventoryProperty, value); }
        }
    }

    public class MaxInventoryProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new MaxInventoryProxy();
        }

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(object),
            typeof(MaxInventoryProxy), new PropertyMetadata(null));
    }

    public class EditQuantityValidation : ValidationRule
    {
        public MinInventoryWrapper Wrapper { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (value.ToString().Equals(String.Empty))
                    return new ValidationResult(false, "Polje ne sme biti prazno!");

                if (Wrapper != null)
                {
                    if (Int32.Parse(value.ToString()) < Wrapper.Min)
                        return new ValidationResult(false, "Količinu možete smanjiti iz sobe!");
                }

                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Samo celi brojevi!");
            }
        }
    }

    public class MinInventoryWrapper : DependencyObject
    {
        public static readonly DependencyProperty MinInventoryProperty = DependencyProperty.Register("Min", typeof(int),
            typeof(MinInventoryWrapper), new FrameworkPropertyMetadata(0));

        public int Min
        {
            get { return (int)GetValue(MinInventoryProperty); }
            set { SetValue(MinInventoryProperty, value); }
        }
    }

    public class MinInventoryProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new MinInventoryProxy();
        }

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(object),
            typeof(MinInventoryProxy), new PropertyMetadata(null));
    }
}
