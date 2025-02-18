using AppLibrary.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PayPalService _payPalService;
        public PaymentController(PayPalService payPalService)
        {
            _payPalService = payPalService;
        }

        [HttpPost("create")]
        [Authorize]
        public IActionResult CreatePayment([FromBody] decimal amount)
        {
            string returnUrl = "https://localhost:7228/api/payment/success";
            string cancelUrl = "https://localhost:7228/api/payment/cancel";

            var payment = _payPalService.CreatePayment(amount, returnUrl, cancelUrl);

            var approvalUrl = payment.links.FirstOrDefault(l => l.rel == "approval_url")?.href;

            if (approvalUrl == null)
                return BadRequest("Unable to create PayPal payment.");

            return Ok(new { approvalUrl });
        }

        [HttpGet("success")]
        [AllowAnonymous]
        public IActionResult PaymentSuccess([FromQuery] string paymentId, [FromQuery] string PayerID)
        {
            return Ok($"Payment successful! Payment ID: {paymentId}, Payer ID: {PayerID}");
        }

        [HttpGet("cancel")]
        [AllowAnonymous]
        public IActionResult PaymentCancelled()
        {
            return Ok("Payment was cancelled.");
        }
    }
}