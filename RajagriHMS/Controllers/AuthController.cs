using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RajagiriHMS.Data;
using RajagiriHMS.DTOs;
using RajagiriHMS.Models;
using RajagriHMS.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RajagriHMS.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // PURPOSE: Login for Admin, FrontOffice, Doctor, etc.
        [HttpPost("login")]
        public IActionResult Login(LoginRequestsDTO request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null) return Unauthorized();

            var role = _context.Roles.First(r => r.RoleID == user.RoleID);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Role, role.RoleName)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"])
                ),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Role = role.RoleName
            });
        }

        // PURPOSE: Admin creates internal users (Doctor, FrontOffice, etc.)
        [Authorize(Roles = "Admin,FrontOffice")]
        [HttpPost("admin/create-user")]
        public IActionResult CreateUser(CreateUserRequest request)
        {
            var user = new User
            {
                UserID = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = request.Password,
                RoleID = request.RoleID,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User created");
        }

        // PURPOSE: Front Office registers patients
        [Authorize(Roles = "FrontOffice")]
        [HttpPost("frontoffice/register-patient")]
        public IActionResult RegisterPatient(CreatePatientRequest request)
        {
            var patient = new Patient
            {
                PatientID = Guid.NewGuid(),
                FullName = request.FullName,
                Gender = request.Gender,
                DOB = request.DOB,
                Phone = request.Phone,
                Email = request.Email,
                Address = request.Address,
                CreatedAt = DateTime.Now
            };

            _context.Patients.Add(patient);
            _context.SaveChanges();

            return Ok("Patient registered");
        }
    }
}