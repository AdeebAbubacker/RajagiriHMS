
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RajagiriHMS.Data;
using RajagiriHMS.Models;

namespace RajagriHMS.Controllers
{
    [ApiController]
    [Route("api/lab")]
    public class LabController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LabController(AppDbContext context)
        {
            _context = context;
        }

        // PURPOSE: Doctor orders lab tests for a patient
        [Authorize(Roles = "Doctor")]
        [HttpPost("order")]
        public IActionResult OrderLabTest(CreateLabTestRequest request)
        {
            var labTest = new LabRequest
            {
                LabRequestID = Guid.NewGuid(),
                AppointmentID = request.AppointmentID,
                TestName = request.TestName,
                Result = "",
                Status = "Ordered",
                CreatedAt = DateTime.Now
            };

            _context.LabRequests.Add(labTest);
            _context.SaveChanges();

            return Ok("Lab test ordered");
        }

        // PURPOSE: Lab staff uploads test result
        [Authorize(Roles = "Lab")]
        [HttpPut("{labTestId}/result")]
        public IActionResult UploadResult(Guid labTestId, UpdateLabResultRequest request)
        {
            var test = _context.LabRequests.FirstOrDefault(l => l.LabRequestID == labTestId);
            if (test == null) return NotFound();

            test.Result = request.Result;
            test.Status = request.Status;
            _context.SaveChanges();

            return Ok("Lab result uploaded");
        }

        // PURPOSE: Doctor / Admin views lab reports
        [Authorize(Roles = "Doctor,Admin,Management")]
        [HttpGet("appointment/{appointmentId}")]
        public IActionResult GetReports(Guid appointmentId)
        {
            return Ok(_context.LabRequests
                .Where(l => l.AppointmentID == appointmentId)
                .ToList());
        }
    }
}