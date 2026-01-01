using System.ComponentModel.DataAnnotations;

namespace RajagiriHMS.Models
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Guid RoleID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
