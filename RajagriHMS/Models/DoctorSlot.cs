using System.ComponentModel.DataAnnotations;

namespace RajagiriHMS.Models
{
    public class DoctorSlot
    {
        [Key]
        public Guid SlotID { get; set; }
        public Guid DoctorID { get; set; }
        public DateTime SlotDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsBooked { get; set; }
    }
}
