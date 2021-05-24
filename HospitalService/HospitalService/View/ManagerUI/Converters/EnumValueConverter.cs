using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace HospitalService.View.ManagerUI.Converters
{
    public class EnumValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "patientroom":
                    return "Bolnička soba";
                case "storageroom":
                    return "Skladište";
                case "operatingroom":
                    return "Operaciona sala";
                case "examinationroom":
                    return "Ordinacija";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                if (value.Equals("Bolnička soba"))
                    return RoomType.PatientRoom;
                else if (value.Equals("Skladište"))
                    return RoomType.StorageRoom;
                else if (value.Equals("Operaciona sala"))
                    return RoomType.OperatingRoom;
                else if (value.Equals("Ordinacija"))
                    return RoomType.ExaminationRoom;
            }

            return null;
        }
    }
}
