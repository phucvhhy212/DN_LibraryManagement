using LibraryAPI.Attributes;
using LibraryCore.Models;
using LibraryCore.UnitOfWork;
using LibraryCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace LibraryAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [JwtAuthorize]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [EnableQuery]
        public IActionResult Get()
        {
            try
            {
                var carts = _unitOfWork.CartRepository.GetAll().AsQueryable();
                return Ok(carts);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        

        public IActionResult Post([FromBody]Cart cart)
        {
            try
            {
                _unitOfWork.CartRepository.Add(cart);
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        
        public IActionResult Put(int key, [FromBody]Cart cart)
        {
            try
            {
                var findCart = _unitOfWork.CartRepository.Find(key);
                findCart.BookId = cart.BookId;
                findCart.Quantity = cart.Quantity;
                findCart.UserId = cart.UserId;
                _unitOfWork.CartRepository.Update(findCart);
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        public IActionResult Delete(int key)
        {
            try
            {
                _unitOfWork.CartRepository.Delete(key);
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
