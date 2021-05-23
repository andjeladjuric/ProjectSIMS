using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace HospitalService.View.ManagerUI.Converters
{
    public class InventoryTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "static":
                    return "Statička";
                case "dynamic":
                    return "Dinamička";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                if (value.Equals("Statička"))
                    return Equipment.Static;
                else if (value.Equals("Dinamička"))
                    return Equipment.Dynamic;
            }

            return null;
        }
    }
}
