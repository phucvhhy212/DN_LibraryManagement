
using LibraryCore.Models;
using LibraryCore.UnitOfWork;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Library.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient client;

        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        public IList<Category> Categories { get; set; }
        public IList<LibraryCore.Models.Book> Book { get; set; }
        public async Task OnGet()
        {

            var responseBook =
                await client.GetAsync(
                    "http://localhost:5098/api/Book?$select=BookId,Rate,Quantity,Image,Title&$expand=Author($select=Fullname)");
            string dataBook = await responseBook.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Book = JsonSerializer.Deserialize<List<LibraryCore.Models.Book>>(dataBook, options);


            var responseCategory =
                await client.GetAsync(
                    "http://localhost:5098/api/Category?$select=CategoryId,CategoryName,Image&$expand=Books");
            string dataCategory = await responseCategory.Content.ReadAsStringAsync();
            Categories = JsonSerializer.Deserialize<List<Category>>(dataCategory, options);

        }
    }
}