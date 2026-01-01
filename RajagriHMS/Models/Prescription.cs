using System.ComponentModel.DataAnnotations;

namespace RajagiriHMS.Models
{
    public class Prescription
    {
        [Key]
        public Guid PrescriptionID { get; set; }
        public Guid AppointmentID { get; set; }
        public string MedicineName { get; set; }
        public string Dosage { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; } // Pending, Dispensed
        public DateTime CreatedAt { get; set; }
    }
}
