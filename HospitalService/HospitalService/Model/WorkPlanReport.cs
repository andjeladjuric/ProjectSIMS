using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Model
{
    public class WorkPlanReport
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        Doctor SelectedDoctor { get; set; }
    }
}
