using LibraryCore.Models;
using LibraryCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace Library.Pages
{
    public class CartModel : PageModel
    {
        private readonly HttpClient client;
        private IServiceScopeFactory _serviceScopeFactory;

        public CartModel(IServiceScopeFactory serviceScopeFactory)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            _serviceScopeFactory = serviceScopeFactory;
        }
        [BindProperty]
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();

        [BindProperty]
        public string Address { get; set; }
        [BindProperty]
        public double Total { get; set; }
        public async Task<IActionResult> OnGet()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("Index");
            }
            var responseCart =
                await client.GetAsync(
                    $"http://localhost:5098/odata/Cart?$filter=UserId eq {HttpContext.Session.GetInt32("userid").Value}&$expand=Book($expand=Category)");
            string dataBook = await responseCart.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(dataBook);
            JArray values = (JArray)jsonObject["value"];

            foreach (JToken value in values)
            {
                // Tạo đối tượng Cart từ dữ liệu JSON
                Cart cart = new Cart
                {
                    CartId = (int)value["CartId"],
                    UserId = (int)value["UserId"],
                    BookId = (int)value["BookId"],
                    Quantity = (int)value["Quantity"],
                    Book = new LibraryCore.Models.Book
                    {
                        BookId = (int)value["Book"]["BookId"],
                        Title = (string)value["Book"]["Title"],
                        CategoryId = (int)value["Book"]["CategoryId"],
                        AuthorId = (int)value["Book"]["AuthorId"],
                        Publisher = (string)value["Book"]["Publisher"],
                        PublicationDate = (DateTime)value["Book"]["PublicationDate"],
                        Quantity = (int)value["Book"]["Quantity"],
                        Image = (string)value["Book"]["Image"],
                        Rate = (double)value["Book"]["Rate"],
                        Status = (bool)value["Book"]["Status"],
                        Introduction = (string)value["Book"]["Introduction"],
                        Description = (string)value["Book"]["Description"],
                        Price = (double)value["Book"]["Price"]
                    }
                };

                // Thêm Cart vào collection
                Carts.Add(cart);
            }
            return Page();
        }

        public async Task<IActionResult> OnGetDeleteCart(int cartId)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("Index");
            }
            await client.DeleteAsync(
                $"http://localhost:5098/odata/Cart/" + cartId);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetAddCart(int bookId)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("Index");
            }


            var responseCart =
                await client.GetAsync(
                    "http://localhost:5098/odata/Cart?" + "$filter=BookId eq " + bookId + " and UserId eq " + HttpContext.Session.GetInt32("userid").Value);
            string dataCart = await responseCart.Content.ReadAsStringAsync();

            JObject jsonObject = JObject.Parse(dataCart);
            JToken values = (JToken)jsonObject["value"];
            Cart? cart;
            if (!values.Any())
                cart = null;
            else
                cart = new Cart
                {
                    CartId = (int)values[0]["CartId"],
                    Quantity = (int)values[0]["Quantity"],
                    BookId = (int)values[0]["BookId"],
                    UserId = (int)values[0]["UserId"]
                };


            //Cart cart = _unitOfWork.CartRepository.FindByBookIdAndUserId(bookId, HttpContext.Session.GetInt32("userid").Value);
            if (cart != null)
            {
                cart.Quantity += 1;
                await client.PutAsJsonAsync($"http://localhost:5098/odata/Cart({cart.CartId})", cart);
            }
            else
            {
                Cart c = new Cart
                {
                    BookId = bookId,
                    Quantity = 1,
                    UserId = HttpContext.Session.GetInt32("userid").Value
                };

                var responseAddNewCart = await client.PostAsJsonAsync("http://localhost:5098/odata/Cart", c);
                var responseCartCount = await client.GetAsync($"http://localhost:5098/odata/Cart?$filter=UserId eq {HttpContext.Session.GetInt32("userid").Value}&$count=true");
                string dataCartCount = await responseCartCount.Content.ReadAsStringAsync();
                JObject jObject = JObject.Parse(dataCartCount);
                var x = jObject["@odata.count"];
                HttpContext.Session.SetInt32("cartcount", (int)x);

            }
            return RedirectToPage("Index");

        }

        public async Task<IActionResult> OnGetAddQuantity(int cartId)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

            var responseCart = await client.GetAsync($"http://localhost:5098/odata/Cart?$filter=CartId eq {cartId}&$expand=Book");

            string dataCart = await responseCart.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(dataCart);
            JToken values = (JToken)jsonObject["value"];
            Cart? cart;
            if (!values.Any())
                cart = null;
            else
                cart = new Cart
                {
                    CartId = (int)values[0]["CartId"],
                    Quantity = (int)values[0]["Quantity"],
                    BookId = (int)values[0]["BookId"],
                    UserId = (int)values[0]["UserId"],

                };

            if (cart != null)
            {
                cart.Quantity += 1;
                await client.PutAsJsonAsync($"http://localhost:5098/odata/Cart({cart.CartId})", cart);
                cart.Book = new LibraryCore.Models.Book
                {
                    BookId = (int)values[0]["Book"]["BookId"],
                    Title = (string)values[0]["Book"]["Title"],
                    CategoryId = (int)values[0]["Book"]["CategoryId"],
                    AuthorId = (int)values[0]["Book"]["AuthorId"],
                    Publisher = (string)values[0]["Book"]["Publisher"],
                    PublicationDate = (DateTime)values[0]["Book"]["PublicationDate"],
                    Quantity = (int)values[0]["Book"]["Quantity"],
                    Image = (string)values[0]["Book"]["Image"],
                    Rate = (double)values[0]["Book"]["Rate"],
                    Status = (bool)values[0]["Book"]["Status"],
                    Introduction = (string)values[0]["Book"]["Introduction"],
                    Description = (string)values[0]["Book"]["Description"],
                    Price = (double)values[0]["Book"]["Price"]
                };
                return new JsonResult(cart);
            }

            return Page();
        }

        public async Task<IActionResult> OnGetMinusQuantity(int cartId)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

            var responseCart = await client.GetAsync($"http://localhost:5098/odata/Cart?$filter=CartId eq {cartId}&$expand=Book");
            string dataCart = await responseCart.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(dataCart);
            JToken values = (JToken)jsonObject["value"];
            Cart? cart;
            if (!values.Any())
                cart = null;
            else
                cart = new Cart
                {
                    CartId = (int)values[0]["CartId"],
                    Quantity = (int)values[0]["Quantity"],
                    BookId = (int)values[0]["BookId"],
                    UserId = (int)values[0]["UserId"],

                };

            if (cart != null)
            {
                if (cart.Quantity == 1)
                {
                    await client.DeleteAsync(
                        $"http://localhost:5098/odata/Cart/" + cartId);
                    var responseCartCount = await client.GetAsync($"http://localhost:5098/odata/Cart?$filter=UserId eq {HttpContext.Session.GetInt32("userid").Value}&$count=true");
                    string dataCartCount = await responseCartCount.Content.ReadAsStringAsync();
                    JObject jObject = JObject.Parse(dataCartCount);
                    var x = jObject["@odata.count"];
                    HttpContext.Session.SetInt32("cartcount", (int)x);
                    cart.Book = new LibraryCore.Models.Book
                    {
                        BookId = (int)values[0]["Book"]["BookId"],
                        Title = (string)values[0]["Book"]["Title"],
                        CategoryId = (int)values[0]["Book"]["CategoryId"],
                        AuthorId = (int)values[0]["Book"]["AuthorId"],
                        Publisher = (string)values[0]["Book"]["Publisher"],
                        PublicationDate = (DateTime)values[0]["Book"]["PublicationDate"],
                        Quantity = (int)values[0]["Book"]["Quantity"],
                        Image = (string)values[0]["Book"]["Image"],
                        Rate = (double)values[0]["Book"]["Rate"],
                        Status = (bool)values[0]["Book"]["Status"],
                        Introduction = (string)values[0]["Book"]["Introduction"],
                        Description = (string)values[0]["Book"]["Description"],
                        Price = (double)values[0]["Book"]["Price"]
                    };

                    cart.Quantity = 0;
                    return new JsonResult(cart);
                }
                else
                {
                    cart.Quantity -= 1;
                    await client.PutAsJsonAsync($"http://localhost:5098/odata/Cart({cart.CartId})", cart);
                    cart.Book = new LibraryCore.Models.Book
                    {
                        BookId = (int)values[0]["Book"]["BookId"],
                        Title = (string)values[0]["Book"]["Title"],
                        CategoryId = (int)values[0]["Book"]["CategoryId"],
                        AuthorId = (int)values[0]["Book"]["AuthorId"],
                        Publisher = (string)values[0]["Book"]["Publisher"],
                        PublicationDate = (DateTime)values[0]["Book"]["PublicationDate"],
                        Quantity = (int)values[0]["Book"]["Quantity"],
                        Image = (string)values[0]["Book"]["Image"],
                        Rate = (double)values[0]["Book"]["Rate"],
                        Status = (bool)values[0]["Book"]["Status"],
                        Introduction = (string)values[0]["Book"]["Introduction"],
                        Description = (string)values[0]["Book"]["Description"],
                        Price = (double)values[0]["Book"]["Price"]
                    };
                    return new JsonResult(cart);
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnGetVnPayReturn()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var builder = new UriBuilder("http://localhost:5098/api/Payment");
            var query = HttpUtility.ParseQueryString(builder.Query);
            foreach (var (key,item) in Request.Query)
            {
                query[key] = item;
            }
            builder.Query = query.ToString();
            var responseVnPay = await client.GetAsync(builder.ToString());
            string dataResponse = await responseVnPay.Content.ReadAsStringAsync();

            VnPaymentResponseModel o = JsonSerializer.Deserialize<VnPaymentResponseModel>(dataResponse, options);
            TempData["vnPayResponse"] = JsonSerializer.Serialize(o);
            if (responseVnPay.IsSuccessStatusCode)
            {
                Address = TempData["address"] as string;
                Total = o.Amount;
                await ClearCartAndCreateOrder();
            }
            
            return RedirectToPage("PaymentStatus");
        }


        public async Task<IActionResult> OnPost(string? payment)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            // if customer choose to checkout using VNPAY
            if (payment == "VNPAY")
            {
                TempData["address"] = Address;

                var model = new VnPaymentRequestModel()
                {
                    Amount = Total,
                    Description = $"VNPAY checkout for user {HttpContext.Session.GetInt32("username").Value}",
                    FullName = "HPV",
                    PaymentId = HttpContext.Session.GetInt32("userid").Value + DateTime.Now.Ticks.ToString(),
                };
                var responseVnPay = await client.PostAsJsonAsync("http://localhost:5098/api/Payment", model);
                if (responseVnPay.IsSuccessStatusCode)
                {
                    string dataBook = await responseVnPay.Content.ReadAsStringAsync();
                    JObject jObject = JObject.Parse(dataBook);

                    return Redirect((string)jObject["redirectUrl"]);
                }
                return RedirectToPage("Index");

            }
            else
            {
                ClearCartAndCreateOrder();

                return RedirectToPage("Index");
            }
        }

        private async Task ClearCartAndCreateOrder()
        {
            // else
            var responseCart =
                await client.GetAsync(
                    $"http://localhost:5098/odata/Cart?$filter=UserId eq {HttpContext.Session.GetInt32("userid").Value}&$expand=Book($expand=Category)");
            string dataBook = await responseCart.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(dataBook);
            JArray values = (JArray)jsonObject["value"];

            foreach (JToken value in values)
            {
                // Tạo đối tượng Cart từ dữ liệu JSON
                Cart cart = new Cart
                {
                    CartId = (int)value["CartId"],
                    UserId = (int)value["UserId"],
                    BookId = (int)value["BookId"],
                    Quantity = (int)value["Quantity"],
                    Book = new LibraryCore.Models.Book
                    {
                        BookId = (int)value["Book"]["BookId"],
                        Title = (string)value["Book"]["Title"],
                        CategoryId = (int)value["Book"]["CategoryId"],
                        AuthorId = (int)value["Book"]["AuthorId"],
                        Publisher = (string)value["Book"]["Publisher"],
                        PublicationDate = (DateTime)value["Book"]["PublicationDate"],
                        Quantity = (int)value["Book"]["Quantity"],
                        Image = (string)value["Book"]["Image"],
                        Rate = (double)value["Book"]["Rate"],
                        Status = (bool)value["Book"]["Status"],
                        Introduction = (string)value["Book"]["Introduction"],
                        Description = (string)value["Book"]["Description"],
                        Price = (double)value["Book"]["Price"]
                    }
                };

                // Add cart's items into collection
                Carts.Add(cart);
            }

            // Create a new Order
            AddOrderViewModel order = new AddOrderViewModel
            {
                UserId = HttpContext.Session.GetInt32("userid").Value,
                Address = Address,
                OrderDate = DateTime.Now,
                Total = Total,
                Status = "Pending"
            };
            var responseAddOrder = await client.PostAsJsonAsync(
                $"http://localhost:5098/api/Order", order);
            if (responseAddOrder.IsSuccessStatusCode)
            {
                string dataUser = await responseAddOrder.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var o = JsonSerializer.Deserialize<Order>(dataUser, options);
                foreach (var item in Carts)
                {
                    var responseAddOrderDetail = await client.PostAsJsonAsync(
                        $"http://localhost:5098/odata/OrderDetail", new AddOrderDetailViewModel
                        {
                            OrderId = o.OrderId,
                            BookId = item.BookId,
                            Quantity = item.Quantity
                        });

                    var responseClearCart =
                        await client.DeleteAsync($"http://localhost:5098/odata/Cart/{item.CartId}");
                }


                var responseCartCount = await client.GetAsync(
                    $"http://localhost:5098/odata/Cart?$filter=UserId eq {HttpContext.Session.GetInt32("userid").Value}&$count=true");
                string dataCartCount = await responseCartCount.Content.ReadAsStringAsync();
                JObject jObject = JObject.Parse(dataCartCount);
                var x = jObject["@odata.count"];
                HttpContext.Session.SetInt32("cartcount", (int)x);

            }
        }
    }
}
