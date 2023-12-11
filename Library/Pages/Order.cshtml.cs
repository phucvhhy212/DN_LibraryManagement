using LibraryCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Library.Pages
{
    public class OrderModel : PageModel
    {
        private readonly HttpClient client;
        

        [BindProperty]
        public IList<Order> Orders { get; set; }


        public OrderModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public async Task<IActionResult> OnGet()
        {
            
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("Index");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

            var responseOrder =
                await client.GetAsync(
                    "http://localhost:5098/api/Order/"+ HttpContext.Session.GetInt32("userid"));
            if(responseOrder.IsSuccessStatusCode){
                string dataOrder = await responseOrder.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Orders = JsonSerializer.Deserialize<IList<Order>>(dataOrder, options);

                return Page();
            }
            else
            {
                return RedirectToPage("Index");
            }
        }
    }
}
