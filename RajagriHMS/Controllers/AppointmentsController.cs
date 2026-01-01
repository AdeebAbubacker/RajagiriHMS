using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RajagiriHMS.Data;
using RajagiriHMS.Models;
using RajagriHMS.DTOs;

namespace RajagriHMS.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppointmentsController(AppDbContext context)
        {
            _context = context;
        }

        // PURPOSE: Front Office books appointment using an available slot
        [Authorize(Roles = "FrontOffice")]
        [HttpPost]
        public IActionResult CreateAppointment(CreateAppointmentRequest request)
        {
            var slot = _context.Slots
                .FirstOrDefault(s => s.SlotID == request.SlotID && s.Status == "Available");

            if (slot == null)
                return BadRequest("Slot not available");

            var appointment = new Appointment
            {
                AppointmentID = Guid.NewGuid(),
                PatientID = request.PatientID,
                DoctorID = request.DoctorID,
                SlotID = request.SlotID,
                Status = "Scheduled",
                CreatedAt = DateTime.Now
            };

            slot.Status = "Booked";

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return Ok("Appointment booked");
        }

        // PURPOSE: Doctor updates appointment status (Checked-in, Completed)
        [Authorize(Roles = "Doctor,FrontOffice")]
        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(Guid id, UpdateAppointmentStatusRequest request)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.AppointmentID == id);
            if (appointment == null) return NotFound();

            appointment.Status = request.Status;
            _context.SaveChanges();

            return Ok("Status updated");
        }

        // PURPOSE: Admin & Management view all appointments
        [Authorize(Roles = "Admin,Management,FrontOffice")]
        [HttpGet]
        public IActionResult GetAllAppointments()
        {
            return Ok(_context.Appointments.ToList());
        }

        // PURPOSE: Front Office cancels appointment
        [Authorize(Roles = "FrontOffice")]
        [HttpDelete("{id}")]
        public IActionResult CancelAppointment(Guid id)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.AppointmentID == id);
            if (appointment == null) return NotFound();

            appointment.Status = "Cancelled";
            _context.SaveChanges();

            return Ok("Appointment cancelled");
        }
    }
}