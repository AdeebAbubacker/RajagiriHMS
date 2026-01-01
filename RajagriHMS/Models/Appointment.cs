using System.ComponentModel.DataAnnotations;

namespace RajagiriHMS.Models
{
    public class Appointment
    {
        [Key]
        public Guid AppointmentID { get; set; }
        public Guid PatientID { get; set; }
        public Guid DoctorID { get; set; }
        public Guid SlotID { get; set; }
        public string Status { get; set; } // Booked, Checked-in, Completed, Cancelled
        public DateTime CreatedAt { get; set; }
    }
}
