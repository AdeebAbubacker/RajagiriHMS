using System.ComponentModel.DataAnnotations;

namespace RajagiriHMS.Models
{
    public class Billing
    {
        [Key]
        public Guid BillingID { get; set; }
        public Guid AppointmentID { get; set; }
        public decimal ConsultationCharge { get; set; }
        public decimal LabCharge { get; set; }
        public decimal MedicineCharge { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } // Paid, Unpaid
        public DateTime CreatedAt { get; set; }
    }
    public class CreateBillRequest
    {
        public Guid AppointmentID { get; set; }
        public decimal Amount { get; set; }
    }

    public class UpdateBillStatusRequest
    {
        public string Status { get; set; } // Paid
    }
}
