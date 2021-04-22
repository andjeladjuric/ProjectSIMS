using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.Validations
{
    public class RoomNumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex checkId = new Regex(@"[1-4]{1}[0-9]{2}[A-Za-z]{0,1}$");

            try
            {
                if(value.ToString().Equals(String.Empty))
                {
                    return new ValidationResult(false, "Polje ne sme biti prazno!");
                }
                else if (!checkId.IsMatch(value.ToString()))
                {
                    return new ValidationResult(false, "Pogrešan format!");
                }

                RoomFileStorage storage = new RoomFileStorage();
                foreach(Room r in storage.GetAll())
                {
                    if (value.ToString().Equals(r.Id))
                        return new ValidationResult(false, "ID već postoji u bazi!");
                }

                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Nepoznata greška!");
            }
        }
    }

    public class ItemIdValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                int i;
                if (value.ToString().Equals(String.Empty))
                {
                    return new ValidationResult(false, "Polje ne sme biti prazno!");
                }
                else if (!Int32.TryParse(value.ToString(), out i))
                {
                    return new ValidationResult(false, "Pogrešan format!");
                }

                InventoryFileStorage invStorage = new InventoryFileStorage();
                foreach (Inventory inv in invStorage.GetAll())
                {
                    if (inv.Id == Int32.Parse(value.ToString()))
                        return new ValidationResult(false, "ID već postoji u bazi!");
                }

                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Nepoznata greška!");
            }
        }
    }
}
