using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RajagiriHMS.Data;
using RajagiriHMS.Models;

namespace RajagriHMS.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // ==========================
        // 1. CREATE ROLE
        // ==========================
        [HttpPost("roles")]
        public IActionResult CreateRole([FromBody] Role role)
        {
            if (string.IsNullOrEmpty(role.RoleName))
                return BadRequest("Role name is required");

            // ✅ Auto-generate GUID
            role.RoleID = Guid.NewGuid();

            _context.Roles.Add(role);
            _context.SaveChanges();

            return Ok(new
            {
                Message = "Role created successfully",
                RoleID = role.RoleID
            });
        }


        // ==========================
        // 2. GET ALL ROLES
        // ==========================
        [HttpGet("roles")]
        public IActionResult GetRoles()
        {
            var roles = _context.Roles.ToList();
            return Ok(roles);
        }

        // ==========================
        // 3. CREATE USER (STAFF)
        // ==========================
        [HttpPost("users")]
        public IActionResult CreateUser([FromBody] User user)
        {
            user.CreatedAt = DateTime.Now;

            // NOTE: Password hashing skipped for simplicity
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new
            {
                Message = "User created successfully",
                User = user
            });
        }

        // ==========================
        // 4. GET ALL USERS
        // ==========================
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        // ==========================
        // 5. GET USER BY ID
        // ==========================
        [HttpGet("users/{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        // ==========================
        // 6. DELETE USER
        // ==========================
        [HttpDelete("users/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
                return NotFound("User not found");

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok("User deleted successfully");
        }
    }
}
