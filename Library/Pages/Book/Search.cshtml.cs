using LibraryCore.Models;
using LibraryCore.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Library.Pages.Book
{
    public class SearchModel : PageModel
    {
        private readonly HttpClient client;

        public SearchModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public IList<LibraryCore.Models.Book> Books { get; set; }
        public IList<Category> Categories { get; set; }

        public async Task OnGet(int? cid, int? pages,string? name)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var responseCategory =
                await client.GetAsync(
                    "http://localhost:5098/api/Category?$select=CategoryId,CategoryName,Image&$expand=Books");
            string dataCategory = await responseCategory.Content.ReadAsStringAsync();
            Categories = JsonSerializer.Deserialize<List<Category>>(dataCategory, options);
            var responseBook =
                await client.GetAsync(
                    "http://localhost:5098/api/Book?$select=CategoryId,BookId,Rate,Quantity,Image,Title,Description&$expand=Author($select=Fullname)");
            string dataBook = await responseBook.Content.ReadAsStringAsync();
            var all = JsonSerializer.Deserialize<List<LibraryCore.Models.Book>>(dataBook, options);
            if (cid.HasValue)
            {
                all = all.Where(x => x.CategoryId == cid).ToList();
            }
            if (!pages.HasValue)
            {
                pages = 1;
            }

            if (!String.IsNullOrEmpty(name))
            {
                all = all.Where(x =>
                    x.Title.Contains(name) ||x.Description.Contains(name)).ToList();
            }

            ViewData["idValue"] = cid;
            ViewData["pages"] = pages;
            ViewData["search"] = name;
            ViewData["countPage"] = Math.Ceiling( all.Count/3d);
            Books = all.Skip((pages.Value-1) * 3).Take(3).ToList();
        }
    }
}
