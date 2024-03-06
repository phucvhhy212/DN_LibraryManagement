using LibraryCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Library.Pages
{
    public class PaymentStatusModel : PageModel
    {

        public VnPaymentResponseModel responseVnPay { get; set; }
        public IActionResult OnGet()
        {
            var response = TempData["vnPayResponse"] as string;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            responseVnPay = JsonSerializer.Deserialize<VnPaymentResponseModel>(response, options);
            return Page();
        }
    }
}
