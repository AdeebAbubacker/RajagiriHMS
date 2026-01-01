using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RajagiriHMS.Data;
using RajagriHMS.DTOs;
using RajagriHMS.Models;

namespace RajagriHMS.Controllers
{
    [ApiController]
    [Route("api/slots")]
    public class SlotsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SlotsController(AppDbContext context)
        {
            _context = context;
        }

        // PURPOSE: Front Office creates slots for doctors
        [Authorize(Roles = "FrontOffice")]
        [HttpPost]
        public IActionResult CreateSlot(CreateSlotRequest request)
        {
            var slot = new Slot
            {
                SlotID = Guid.NewGuid(),
                DoctorID = request.DoctorID,
                SlotDate = request.SlotDate.Date,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Status = "Available",
                CreatedAt = DateTime.Now
            };

            _context.Slots.Add(slot);
            _context.SaveChanges();

            return Ok("Slot created");
        }

        // PURPOSE: View available slots for a doctor
        // Used by: FrontOffice, Patient
        [Authorize]
        [HttpGet("doctor/{doctorId}")]
        public IActionResult GetAvailableSlots(Guid doctorId, DateTime date)
        {
            var slots = _context.Slots
                .Where(s =>
                    s.DoctorID == doctorId &&
                    s.SlotDate == date.Date &&
                    s.Status == "Available")
                .ToList();

            return Ok(slots);
        }

        // PURPOSE: Block a slot (doctor unavailable)
        // Used by: Admin, FrontOffice
        [Authorize(Roles = "Admin,FrontOffice")]
        [HttpPut("{slotId}/block")]
        public IActionResult BlockSlot(Guid slotId)
        {
            var slot = _context.Slots.FirstOrDefault(s => s.SlotID == slotId);
            if (slot == null) return NotFound();

            slot.Status = "Blocked";
            _context.SaveChanges();

            return Ok("Slot blocked");
        }
    }
}