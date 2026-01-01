using System.ComponentModel.DataAnnotations;

namespace RajagiriHMS.Models
{
    public class InsuranceClaim
    {
        [Key]
        public Guid ClaimID { get; set; }
        public Guid AppointmentID { get; set; }
        public string InsuranceProvider { get; set; }
        public string Status { get; set; } // Pending, Approved, Rejected
        public DateTime CreatedAt { get; set; }
    }
}
