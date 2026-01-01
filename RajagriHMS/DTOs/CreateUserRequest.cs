namespace RajagriHMS.DTOs
{
    public class CreateUserRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid RoleID { get; set; }
    }
}
