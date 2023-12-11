using LibraryCore.Models;
using LibraryCore.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using LibraryCore.ViewModels;

namespace Library.Pages.Admin
{
    public class OrderManagementModel : PageModel
    {
        private readonly HttpClient client;

        public OrderManagementModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        public IList<Order> Orders { get; set; }

        public async Task OnGet()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

            var responseOrder = await client.GetAsync("http://localhost:5098/api/Order?$expand=User,OrderDetails");
            string dataBook = await responseOrder.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Orders = JsonSerializer.Deserialize<List<Order>>(dataBook, options);
        }

        public async Task<IActionResult> OnGetOrderDetails(int orderId)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

            var responseOrder = await client.GetAsync($"http://localhost:5098/api/Order?$expand=OrderDetails($expand=Book($expand=Category))&$filter=OrderId eq {orderId}");
            string dataBook = await responseOrder.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var order = JsonSerializer.Deserialize<List<Order>>(dataBook, options).First();
            

            if (order == null)
            {
                return NotFound();
            }
            return Partial("_OrderDetailsPartial", order.OrderDetails);
        }

        public async Task<IActionResult> OnPostEditOrder()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

            int orderId = int.Parse(Request.Form["id"]);
            string address = Request.Form["address"];
            string status = Request.Form["status"];
            UpdateOrderViewModel o = new UpdateOrderViewModel
            {
                Address = address,
                Status = status
            };
            var responseOrder = await client.PutAsJsonAsync($"http://localhost:5098/api/Order?key={orderId}",o);

            return RedirectToPage();

        }
    }
}
