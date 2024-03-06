using LibraryAPI.Attributes;
using LibraryAPI.Services;
using LibraryCore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IVnPayService _service;

        public PaymentController(IVnPayService service)
        {
            _service = service;
        }

        // Call VnPay API with order's information
        [HttpPost]
        [JwtAuthorize]
        public IActionResult PostVnPayCheckout(VnPaymentRequestModel model)
        {
            var url = _service.CreatePaymentUrl(HttpContext, model);
            return Ok(new {redirectUrl = url});
        }

        [HttpGet]
        [JwtAuthorize]
        public IActionResult GetVnPayResponse()
        {
            var response = _service.PaymentExecute(Request.Query);
            if (response == null || response.VnPayResponseCode != "00")
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
