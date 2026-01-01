using System.ComponentModel.DataAnnotations;

namespace RajagiriHMS.Models
{
    public class Doctor
    {
        [Key]
        public Guid DoctorID { get; set; }
        public Guid UserID { get; set; }
        public string Specialization { get; set; }
        public string ContactNumber { get; set; }
    }
}
