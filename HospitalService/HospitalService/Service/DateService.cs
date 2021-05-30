using HospitalService.Repositories
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace HospitalService.Service
{
    public class DateService
    {
        public DateService() { }

        public DateTime CreateDate(DateTime date, DateTime time)
        {
            String createdDate = date.ToString("MM/dd/yyyy") + " " + time.ToString("HH: mm");
            return Convert.ToDateTime(createdDate);
        }

        public bool CheckExistingRenovations(string roomId, DateTime startReno, DateTime endReno)
        {
            RenovationsRepository renovations = new RenovationsRepository();
            List<Renovation> renovationRequests = renovations.GetAll();
            bool returnValue = true;

            if (renovations != null && renovationRequests.Count != 0)
            {
                foreach (Renovation renovation in renovationRequests)
                {
                    if (renovation.RoomId.Equals(roomId))
                    {
                        if (IsFirstBefore(startReno, renovation.Start) && IsFirstAfter(endReno, renovation.End))
                        {
                            returnValue = false;
                            break;
                        }
                        else if (IsFirstBefore(startReno, renovation.Start) && IsFirstBefore(endReno, renovation.End) &&
                            IsFirstAfter(endReno, renovation.Start))
                        {
                            returnValue = false;
                            break;
                        }
                        else if (IsFirstAfter(startReno, renovation.Start) && IsFirstBefore(endReno, renovation.End))
                        {
                            returnValue = false;
                            break;
                        }
                        else if (IsFirstAfter(startReno, renovation.Start) && IsFirstAfter(endReno, renovation.End) &&
                            IsFirstBefore(startReno, renovation.End))
                        {
                            returnValue = false;
                            break;
                        }
                    }
                }
            }

            return returnValue;
        }

        private bool IsFirstBefore(DateTime firstDate, DateTime secondDate)
        {
            if (DateTime.Compare(firstDate, secondDate) <= 0)
                return true;

            return false;
        }

        private bool IsFirstAfter(DateTime firstDate, DateTime secondDate)
        {
            if (DateTime.Compare(firstDate, secondDate) >= 0)
                return true;

            return false;
          
        public bool ExsitstsAtTime(Appointment appointment, DateTime start, DateTime end)
        {
            if (DateTime.Compare(appointment.StartTime, start) == 0)
                return true;
            else if (DateTime.Compare(appointment.StartTime, start) < 0)
            {
                if (DateTime.Compare(appointment.EndTime, start) > 0)
                    return true;
            }
            else if (DateTime.Compare(appointment.StartTime, start) > 0 && DateTime.Compare(end, appointment.StartTime) > 0)
                return true;
            
             return false;
        }
    }
}
