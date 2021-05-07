using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace HospitalService.View.ManagerUI.Converters
{
    public class DaysOfTheWeekConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "monday":
                    return "Ponedeljak";
                case "tuesday":
                    return "Utorak";
                case "wednesday":
                    return "Sreda";
                case "thursday":
                    return "Četvrtak";
                case "friday":
                    return "Petak";
                case "saturday":
                    return "Subota";
                case "sunday":
                    return "Nedelja";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                if (value.Equals("Ponedeljak"))
                    return "Monday";
                else if (value.Equals("Utorak"))
                    return "Tuesday";
                else if (value.Equals("Sreda"))
                    return "Wednesday";
                else if (value.Equals("Četvrtak"))
                    return "Thursday";
                else if (value.Equals("Petak"))
                    return "Friday";
                else if (value.Equals("Subota"))
                    return "Saturday";
                else if (value.Equals("Nedelja"))
                    return "Sunday";
            }

            return null;
        }
    }
}
