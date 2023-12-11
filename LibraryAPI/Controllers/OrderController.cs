using LibraryAPI.Attributes;
using LibraryCore.Models;
using LibraryCore.UnitOfWork;
using LibraryCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 3)]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.OrderRepository.GetAllOrdersNav());
        }

        [JwtAuthorize]
        [HttpGet("{userId:int}")]
        //[Produces("application/xml")]
        public IActionResult Get(int userId)
        {
            var acceptHeader = Request.Headers["Accept"];
            try
            {
                var res = _unitOfWork.OrderRepository.GetOrderByUserId(userId);
                
                if (acceptHeader.Contains("application/json"))
                {
                    var f = res.Select(o =>
                        new
                        {

                            Address = o.Address,
                            Total = o.Total,
                            OrderDate = o.OrderDate,
                            Status = o.Status,
                            OrderId = o.OrderId,
                            OrderDetails = o.OrderDetails.Select(od => new
                            {
                                Book = new
                                {
                                    Price = od.Book.Price,

                                    Title = od.Book.Title,
                                    Image = od.Book.Image,
                                    Category = new { CategoryName = od.Book.Category.CategoryName },
                                },
                                Quantity = od.Quantity,
                                Price = od.Book.Price
                            }).ToList()

                        }).ToList();
                    return Ok(f);

                }
                else
                {
                    var f = res.Select(o =>
                        new CustomResponseOrder
                        {

                            Address = o.Address,
                            Total = o.Total,
                            OrderDate = o.OrderDate,
                            Status = o.Status,
                            OrderId = o.OrderId,
                            OrderDetails = o.OrderDetails.Select(od => new CustomResponseOrderDetail()
                            {

                                Book = new Book
                                {
                                    Price = od.Book.Price,

                                    Title = od.Book.Title,
                                    Image = od.Book.Image,
                                    Category = new Category { CategoryName = od.Book.Category.CategoryName },
                                },
                                Quantity = od.Quantity,
                                Price = od.Book.Price.Value
                            }).ToList()

                        }).ToList();
                    return Ok(f);

                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [JwtAuthorize]
        [HttpPost]
        public IActionResult AddOrder(AddOrderViewModel order)
        {
            try
            {
                Order o = new Order
                {
                    UserId = order.UserId,
                    Address = order.Address,
                    OrderDate = order.OrderDate,
                    Total = order.Total,
                    Status = order.Status
                };
                _unitOfWork.OrderRepository.Add(o);
                _unitOfWork.SaveChange();
                return Ok(o);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [JwtAuthorize]
        [HttpPut]
        public IActionResult UpdateOrder(int key, UpdateOrderViewModel order)
        {
            try
            {
                Order o = _unitOfWork.OrderRepository.Find(key);
                o.Address = order.Address;
                o.Status = order.Status;
                _unitOfWork.OrderRepository.Update(o);
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
