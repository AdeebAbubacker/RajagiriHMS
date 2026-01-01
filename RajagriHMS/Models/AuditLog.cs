using System.ComponentModel.DataAnnotations;

namespace RajagiriHMS.Models
{
    public class AuditLog
    {
        [Key]
        public Guid LogID { get; set; }
        public Guid UserID { get; set; }
        public string Action { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
