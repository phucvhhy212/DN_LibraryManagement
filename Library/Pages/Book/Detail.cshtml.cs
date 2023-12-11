using LibraryCore.Models;
using LibraryCore.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Library.Pages
{
    public class DetailModel : PageModel
    {
        public LibraryCore.Models.Book Book { get; set; }

        private readonly HttpClient client;
        public DetailModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        public async Task OnGet(int id)
        {
            var responseBook =
                await client.GetAsync(
                    $"http://localhost:5098/api/Book?$select=BookId,Introduction,Publisher,Description,PublicationDate,Rate,Quantity,Image,Title&filter=BookId eq {id}");
            string dataBook = await responseBook.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Book = JsonSerializer.Deserialize<List<LibraryCore.Models.Book>>(dataBook, options).First();
        }
    }
}
