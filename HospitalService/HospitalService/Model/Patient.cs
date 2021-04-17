
namespace Model
{
    public class Patient : User
    {
        public PatientType PatientType { get; set; }
        public string medicalRecordId { get; set; }
    }
}