using System.ComponentModel.DataAnnotations;

namespace RajagiriHMS.DTOs
{
    public class LoginRequestsDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
