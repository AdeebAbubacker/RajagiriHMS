using System.ComponentModel.DataAnnotations;

namespace RajagiriHMS.Models
{
    public class LabRequest
    {
        [Key]
        public Guid LabRequestID { get; set; }
        public Guid AppointmentID { get; set; }
        public string TestName { get; set; }
        public string Result { get; set; }
        public string Status { get; set; } // Pending, Completed
        public DateTime CreatedAt { get; set; }
    }
    public class CreateLabTestRequest
    {
        public Guid AppointmentID { get; set; }
        public string TestName { get; set; }
    }

    public class UpdateLabResultRequest
    {
        public string Result { get; set; }
        public string Status { get; set; } // Completed
    }
}
