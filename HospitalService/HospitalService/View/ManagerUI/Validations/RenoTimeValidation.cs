using HospitalService.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.Validations
{
    public class RenoTimeValidation : ValidationRule
    {
        public RoomWrapper Wrapper { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateService dateService = new DateService();

            try
            {
                if (value.ToString().Equals(String.Empty))
                {
                    return new ValidationResult(false, "Polje ne sme biti prazno!");
                }

                if (!dateService.CheckExistingRenovations(Wrapper.FirstRoom.Id, Wrapper.StartDate, Convert.ToDateTime(value)))
                {
                    return new ValidationResult(false, "Postoji zakazano renoviranje \n u ovom periodu!");
                }

                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Nepoznata greška!");
            }

        }
    }

    public class RoomWrapper : DependencyObject
    {
        public static readonly DependencyProperty FirstRoomProperty = DependencyProperty.Register("FirstRoom", typeof(Room), typeof(RoomWrapper),
            null);

        public Room FirstRoom
        {
            get => (Room)GetValue(FirstRoomProperty);
            set => SetValue(FirstRoomProperty, value);
        }

        public static readonly DependencyProperty StartDateProperty = DependencyProperty.Register("StartDate", typeof(DateTime), typeof(RoomWrapper),
            null);

        public DateTime StartDate
        {
            get => (DateTime)GetValue(StartDateProperty);
            set => SetValue(StartDateProperty, value);
        }
    }

    public class RoomProxy : Freezable
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

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(object), typeof(RoomProxy),
            new PropertyMetadata(null));
    }
}
