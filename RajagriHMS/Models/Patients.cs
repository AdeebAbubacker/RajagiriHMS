using System.ComponentModel.DataAnnotations;

namespace RajagiriHMS.Models
{
    public class Patient
    {
        [Key]
        public Guid PatientID { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
