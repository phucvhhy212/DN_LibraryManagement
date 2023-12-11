using AutoMapper;
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
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            var acceptHeader = Request.Headers["Accept"];

            try
            {
                var x = _unitOfWork.BookRepository.GetAll().AsQueryable();
                return Ok(x);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPut]
        [JwtAuthorize]
        public IActionResult Update(int key, UpdateBookViewModel b)
        {
            try
            {
                Book book = _unitOfWork.BookRepository.Find(key);
                book.CategoryId = b.CategoryId;
                book.Title = b.Title;
                book.Description = b.Description;
                book.Image = b.Image ?? book.Image;
                book.Introduction = b.Introduction;
                book.Publisher = b.Publisher;
                book.Rate = b.Rate;
                book.Quantity = b.Quantity;
                book.PublicationDate = b.PublicationDate;
                book.Status = b.Status;
                _unitOfWork.BookRepository.Update(book);
                _unitOfWork.SaveChange();
                return Ok();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [JwtAuthorize]
        [HttpPost]
        public IActionResult Add(AddBookViewModel book)
        {
            try
            {
                Book b = _mapper.Map<Book>(book);
                _unitOfWork.BookRepository.Add(b);
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
