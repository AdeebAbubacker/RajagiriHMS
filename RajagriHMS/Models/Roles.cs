using System.ComponentModel.DataAnnotations;

namespace RajagiriHMS.Models
{
    public class Role
    {
        [Key]
        public Guid RoleID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
}
