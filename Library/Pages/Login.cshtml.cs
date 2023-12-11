using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LibraryCore.Models;
using LibraryCore.UnitOfWork;
using System.Net.Http.Headers;
using System.Text.Json;
using LibraryCore.ViewModels;
using Newtonsoft.Json.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.Pages
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient client;
        private readonly IConfiguration _configuration;

        public LoginModel(IConfiguration configuration)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public LoginViewModel User { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("User.Fullname");
            ModelState.Remove("User.Mail");
            ModelState.Remove("User.UserRole");
            

            var responseUser =
                await client.PostAsJsonAsync(
                    "http://localhost:5098/api/User/Login",User);
            if (responseUser.IsSuccessStatusCode)
            {

                string dataUser = await responseUser.Content.ReadAsStringAsync();
                JObject Object = JObject.Parse(dataUser);
                var token = Object["jwtToken"].ToString();
                HttpContext.Session.SetString("token",token);
                var claimsPrincipal = DecodeJwtToken(token);
                    // Lấy thông tin từ ClaimsPrincipal
                var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userRole = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value;
                var userName = claimsPrincipal.FindFirst("username")?.Value;


                HttpContext.Session.SetString("username", userName);
                HttpContext.Session.SetInt32("userid", int.Parse(userId));
                HttpContext.Session.SetString("role", userRole);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                var responseCartCount =
                    await client.GetAsync($"http://localhost:5098/odata/Cart?$filter=UserId eq {userId}&$count=true");
                string dataCartCount = await responseCartCount.Content.ReadAsStringAsync();
                JObject jsonObject = JObject.Parse(dataCartCount);
                var x = jsonObject["@odata.count"];
                HttpContext.Session.SetInt32("cartcount", (int)x);
                return RedirectToPage("Index");

            }

            return Page();
        }

        private ClaimsPrincipal DecodeJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"] ?? "");


            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = false,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                return claimsPrincipal as ClaimsPrincipal;
            }
            catch (SecurityTokenException)
            {
                return null;
            }
        }
    }
}
