using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace HospitalService.View.ManagerUI.Converters
{
    public class RoomAvailabilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "true":
                    return "Dostupna";
                case "false":
                    return "Renovira se";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                if (value.Equals("Dostupna"))
                    return true;
                else if (value.Equals("Renovira se"))
                    return false;
            }

            return null;
        }
    }
}
