
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RajagiriHMS.Data;
using RajagiriHMS.Models;

namespace RajagriHMS.Controllers
{
    [ApiController]
    [Route("api/billing")]
    public class BillingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BillingController(AppDbContext context)
        {
            _context = context;
        }

        // PURPOSE: Billing staff generates bill after consultation
        [Authorize(Roles = "Billing")]
        [HttpPost("create")]
        public IActionResult CreateBill(CreateBillRequest request)
        {
            var bill = new Billing
            {
                BillingID = Guid.NewGuid(),
                AppointmentID = request.AppointmentID,
                TotalAmount = request.Amount,
                Status = "Unpaid",
                CreatedAt = DateTime.Now
            };

            _context.Billings.Add(bill);
            _context.SaveChanges();

            return Ok("Bill generated");
        }

        // PURPOSE: Billing staff marks bill as paid
        [Authorize(Roles = "Billing")]
        [HttpPut("{billId}/pay")]
        public IActionResult PayBill(Guid billId, UpdateBillStatusRequest request)
        {
            var bill = _context.Billings.FirstOrDefault(b => b.BillingID == billId);
            if (bill == null) return NotFound();

            bill.Status = request.Status;
            _context.SaveChanges();

            return Ok("Payment updated");
        }

        // PURPOSE: Admin & Management view billing summary
        [Authorize(Roles = "Admin,Management")]
        [HttpGet]
        public IActionResult GetAllBills()
        {
            return Ok(_context.Billings.ToList());
        }
    }
}