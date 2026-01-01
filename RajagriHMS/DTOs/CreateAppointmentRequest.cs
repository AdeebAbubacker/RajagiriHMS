namespace RajagriHMS.DTOs
{
    public class CreateAppointmentRequest
    {
        public Guid PatientID { get; set; }
        public Guid DoctorID { get; set; }
        public Guid SlotID { get; set; }
    }
}
