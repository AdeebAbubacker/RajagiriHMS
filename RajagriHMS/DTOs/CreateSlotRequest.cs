namespace RajagriHMS.DTOs
{
    public class CreateSlotRequest
    {
        public Guid DoctorID { get; set; }
        public DateTime SlotDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
