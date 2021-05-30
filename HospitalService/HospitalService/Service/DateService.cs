using HospitalService.Repositories;
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
                        if (IsBefore(startReno, renovation.Start) && IsAfter(endReno, renovation.End))
                        {
                            returnValue = false;
                            break;
                        }
                        else if (IsBefore(startReno, renovation.Start) && IsBefore(endReno, renovation.End) &&
                            IsAfter(endReno, renovation.Start))
                        {
                            returnValue = false;
                            break;
                        }
                        else if (IsAfter(startReno, renovation.Start) && IsBefore(endReno, renovation.End))
                        {
                            returnValue = false;
                            break;
                        }
                        else if (IsAfter(startReno, renovation.Start) && IsAfter(endReno, renovation.End) &&
                            IsBefore(startReno, renovation.End))
                        {
                            returnValue = false;
                            break;
                        }
                    }
                }
            }

            return returnValue;
        }

        private bool IsBefore(DateTime firstDate, DateTime secondDate)
        {
            if (DateTime.Compare(firstDate, secondDate) <= 0)
                return true;

            return false;
        }

        private bool IsAfter(DateTime firstDate, DateTime secondDate)
        {
            if (DateTime.Compare(firstDate, secondDate) >= 0)
                return true;

            return false;
        }
          
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

        public bool IsTaken(DateTime startTime1, DateTime endTime1, DateTime startTime2, DateTime endTime2)
        {

            if (DateTime.Compare(startTime1, startTime2) == 0 && DateTime.Compare(endTime1, endTime2) == 0)
            {
                return true;
            }
            else if (startTime2 > startTime1 && startTime2 < endTime1)
            {
                return true;
            }
            else if (endTime2 > startTime1 && endTime2 < endTime1)
            {
                return true;
            }

            return false;

        }
    }
}
