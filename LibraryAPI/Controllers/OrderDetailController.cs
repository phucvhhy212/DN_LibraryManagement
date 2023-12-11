using AutoMapper;
using LibraryAPI.Attributes;
using LibraryCore.Migrations;
using LibraryCore.UnitOfWork;
using LibraryCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using OrderDetail = LibraryCore.Models.OrderDetail;

namespace LibraryAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderDetailController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;

            _mapper = mapper;
        }

        
        [EnableQuery]
        public IActionResult Get()
        {
            try
            {
                return Ok(_unitOfWork.OrderDetailRepository.GetAll().AsQueryable());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [JwtAuthorize]
        
        public IActionResult Post([FromBody] AddOrderDetailViewModel addOrderDetailViewModel)
        {
            try
            {
                var orderDetail = _mapper.Map<OrderDetail>(addOrderDetailViewModel);
                _unitOfWork.OrderDetailRepository.Add(orderDetail);
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
