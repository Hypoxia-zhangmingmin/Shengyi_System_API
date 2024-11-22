using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shengyi_WebAPI.Models.Out;
using Shengyi_WebAPI.Services.PaymentStatus;

namespace Shengyi_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentStatusController : ControllerBase
    {
        private readonly IPaymentStatusService _paymentStatusService;
        public PaymentStatusController(IPaymentStatusService paymentStatusService)
        {
            _paymentStatusService = paymentStatusService;
        }
        [HttpGet("all")]
        public async Task<object> GetAll()
        {
            var result =await _paymentStatusService.GetAll();
            return Ok(new OutputModel<object>().Success(result));
        }
    }
}
