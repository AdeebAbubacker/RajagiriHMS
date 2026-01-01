using System.ComponentModel.DataAnnotations;

namespace RajagiriHMS.Models
{
    public class PharmacyDispense
    {
        [Key]
        public Guid DispenseID { get; set; }
        public Guid PrescriptionID { get; set; }
        public DateTime DispenseDate { get; set; }
        public string Status { get; set; }
    }
}
