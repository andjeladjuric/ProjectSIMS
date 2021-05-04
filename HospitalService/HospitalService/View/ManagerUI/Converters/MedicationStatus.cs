using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace HospitalService.View.ManagerUI.Converters
{
    public class MedicationStatus : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "antibiotic":
                    return "Antibiotik";
                case "antiseptic":
                    return "Antiseptik";
                case "antidepressant":
                    return "Antidepresiv";
                case "painkiller":
                    return "Protiv bolova";
                case "supplement":
                    return "Suplement";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                if (value.Equals("Antibiotik"))
                    return MedicationType.Antibiotic;
                else if (value.Equals("Antiseptik"))
                    return MedicationType.Antiseptic;
                else if (value.Equals("Antidepresiv"))
                    return MedicationType.Antidepressant;
                else if (value.Equals("Protiv bolova"))
                    return MedicationType.Painkiller;
                else if (value.Equals("Suplement"))
                    return MedicationType.Supplement;
            }

            return null;
        }
    }
}
