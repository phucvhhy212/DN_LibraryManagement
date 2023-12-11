using LibraryCore.Models;
using LibraryCore.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using LibraryCore.ViewModels;

namespace Library.Pages.Admin
{
    public class BookManagementModel : PageModel
    {
        private readonly HttpClient client;

        public BookManagementModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public IList<Category> Categories { get; set; }
        public IList<LibraryCore.Models.Book> Books { get; set; }
        public IList<Author> Authors { get; set; }
        

        [BindProperty]
        public AddBookViewModel Book { get; set; }

        [Required]
        public IFormFile Image { get; set; }
        public async Task OnGet()
        {

            var responseCategory =
                await client.GetAsync(
                    "http://localhost:5098/api/Category?$select=CategoryId,CategoryName");
            string dataCategory = await responseCategory.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Categories = JsonSerializer.Deserialize<List<Category>>(dataCategory, options);


            var responseBook =
                await client.GetAsync(
                    "http://localhost:5098/api/Book?$select=BookId,Rate,Publisher,PublicationDate,Introduction,Description,Status,Quantity,Image,Title&$expand=Category");
            string dataBook = await responseBook.Content.ReadAsStringAsync();
            
            Books = JsonSerializer.Deserialize<List<LibraryCore.Models.Book>>(dataBook, options);

            var responseAuthor =
                await client.GetAsync(
                    "http://localhost:5098/api/Author?$select=AuthorId,Fullname");
            string dataAuthor = await responseAuthor.Content.ReadAsStringAsync();
            Authors = JsonSerializer.Deserialize<List<Author>>(dataAuthor, options);
        }

        public async Task<IActionResult> OnPostEditBook()
        {
            UpdateBookViewModel b = new UpdateBookViewModel();
            b.Title = Request.Form["title"];
            b.Description = Request.Form["description"];
            b.CategoryId = int.Parse(Request.Form["category"]);
            b.Publisher = Request.Form["publisher"];
            b.PublicationDate = DateTime.Parse(Request.Form["date"]);
            b.Quantity = int.Parse(Request.Form["quantity"]);
            var file = Request.Form.Files["newimage"];

            if (file != null && file.Length > 0)
            {
                var fileName = file.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "books", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                b.Image = "/img/books/" + fileName;
            }
            b.Rate = int.Parse(Request.Form["rate"]);
            b.Introduction = Request.Form["introduction"];
            b.Description = Request.Form["description"];
            b.Status = Request.Form["status"] == "1";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var responseUpdate = await client.PutAsJsonAsync($"http://localhost:5098/api/Book?key={int.Parse(Request.Form["id"])}", b);
            return RedirectToPage();

        }

        public async Task<IActionResult> OnPostAddBook()
        {
            var fileName = Image.FileName;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "books", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                Image.CopyTo(stream);
            }
            Book.Image = "/img/books/" + fileName;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            await client.PostAsJsonAsync("http://localhost:5098/api/Book", Book);
            return RedirectToPage();
        }
    }
}
