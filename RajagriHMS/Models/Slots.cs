using System.ComponentModel.DataAnnotations;

namespace RajagriHMS.Models
{
    public class Slot
    {
        [Key]
        public Guid SlotID { get; set; }

        public Guid DoctorID { get; set; }

        public DateTime SlotDate { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string Status { get; set; } // Available, Booked, Blocked

        public DateTime CreatedAt { get; set; }
    }
}
