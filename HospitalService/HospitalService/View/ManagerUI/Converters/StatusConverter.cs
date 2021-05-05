using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace HospitalService.View.ManagerUI.Converters
{
    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "approved":
                    return "Odobren";
                case "notapproved":
                    return "Nije odobren";
                case "waitingforapproval":
                    return "Na validaciji";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                if (value.Equals("Odobren"))
                    return MedicineStatus.Approved;
                else if (value.Equals("Nije odobren"))
                    return MedicineStatus.NotApproved;
                else if (value.Equals("Na validaciji"))
                    return MedicineStatus.WaitingForApproval;
            }

            return null;
        }
    }
}
