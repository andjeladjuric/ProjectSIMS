using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class RenovationDTO
    {
        public DateTime Start { get; set; }
        public string Type { get; set; }

        public RenovationDTO() { }

        public RenovationDTO(DateTime start, string type)
        {
            Start = start;
            Type = type;
        }
    }
}
