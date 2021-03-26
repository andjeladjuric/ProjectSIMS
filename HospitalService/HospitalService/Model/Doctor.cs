
namespace Model
{
    public class Doctor : User
    {
        public DoctorType DoctorType { get; set; }
        public System.Collections.ArrayList appintments;

        public System.Collections.ArrayList GetAppintments()
        {
            if (appintments == null)
                appintments = new System.Collections.ArrayList();
            return appintments;
        }

        public void SetAppintments(System.Collections.ArrayList newAppintments)
        {
            RemoveAllAppintments();
            foreach (Appointment oAppointment in newAppintments)
                AddAppintments(oAppointment);
        }

        public void AddAppintments(Appointment newAppointment)
        {
            if (newAppointment == null)
                return;
            if (this.appintments == null)
                this.appintments = new System.Collections.ArrayList();
            if (!this.appintments.Contains(newAppointment))
            {
                this.appintments.Add(newAppointment);
                newAppointment.SetDoctor(this);
            }
        }

        public void RemoveAppintments(Appointment oldAppointment)
        {
            if (oldAppointment == null)
                return;
            if (this.appintments != null)
                if (this.appintments.Contains(oldAppointment))
                {
                    this.appintments.Remove(oldAppointment);
                    oldAppointment.SetDoctor((Doctor)null);
                }
        }

        public void RemoveAllAppintments()
        {
            if (appintments != null)
            {
                System.Collections.ArrayList tmpAppintments = new System.Collections.ArrayList();
                foreach (Appointment oldAppointment in appintments)
                    tmpAppintments.Add(oldAppointment);
                appintments.Clear();
                foreach (Appointment oldAppointment in tmpAppintments)
                    oldAppointment.SetDoctor((Doctor)null);
                tmpAppintments.Clear();
            }
        }

    }
}