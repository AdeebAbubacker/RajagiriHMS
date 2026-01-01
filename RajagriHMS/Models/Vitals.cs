using System.ComponentModel.DataAnnotations;

namespace RajagiriHMS.Models
{
    public class Vital
    {
        [Key]
        public Guid VitalsID { get; set; }
        public Guid AppointmentID { get; set; }
        public decimal Temperature { get; set; }
        public string BP { get; set; }
        public int Pulse { get; set; }
        public string Notes { get; set; }
        public DateTime RecordedAt { get; set; }
    }
}
