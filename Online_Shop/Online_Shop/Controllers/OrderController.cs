using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Shop.Dto;
using Online_Shop.Interfaces.ServiceInterfaces;
using System.Data;

namespace Online_Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderService _service;
        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpPost("CreateOrder")]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<IActionResult> CreateOrder(CreateOrderDto orderDto)
        {
            int id = int.Parse(User.Claims.First(c => c.Type == "UserId").Value);
            OrderDto order = await _service.CreateOrder(id, orderDto);
            if (order == null)
                return BadRequest();
            return Ok(order);
        }

        //GET api/order
        [HttpGet("GetAllOrders")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<IActionResult> GetAllOrders()
        {
            List<OrderDto> orders = await _service.GetAllOrders();
            if (orders == null)
                return BadRequest();
            return Ok(orders);
        }

        //GET api/order
        [HttpGet("GetCustomerDeliveredOrders")]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<IActionResult> GetCustomerDeliveredOrders()
        {
            int id = int.Parse(User.Claims.First(c => c.Type == "UserId").Value);
            List<OrderDto> orders = await _service.GetAllDeliveredOrders(id);
            if (orders == null)
                return BadRequest();
            return Ok(orders);
        }

        //GET api/order
        [HttpGet("GetSalesmanDeliveredOrders")]
        [Authorize(Roles = "SALESMAN", Policy = "VerifiedUserOnly")]
        public async Task<IActionResult> GetSalesmanDeliveredOrders()
        {
            int id = int.Parse(User.Claims.First(c => c.Type == "UserId").Value);
            List<OrderDto> orders = await _service.GetAllDeliveredOrders(id);
            if (orders == null)
                return BadRequest();
            return Ok(orders);
        }

        //GET api/order
        [HttpGet("GetCustomerInProgressOrders")]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<IActionResult> GetCustomerInProgressOrders()
        {
            int id = int.Parse(User.Claims.First(c => c.Type == "UserId").Value);
            List<OrderDto> orders = await _service.GetAllInProgressOrders(id);
            if (orders == null)
                return BadRequest();
            return Ok(orders);
        }

        //GET api/order
        [HttpGet("GetSalesmanInProgressOrders")]
        [Authorize(Roles = "SALESMAN", Policy = "VerifiedUserOnly")]
        public async Task<IActionResult> GetSalesmanInProgressOrders()
        {
            int id = int.Parse(User.Claims.First(c => c.Type == "UserId").Value);
            List<OrderDto> orders = await _service.GetAllInProgressOrders(id);
            if (orders == null)
                return BadRequest();
            return Ok(orders);
        }

        //GET api/order
        [HttpGet("DenieOrder")]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<IActionResult> DenieOrder(int id)
        {
            bool temp = await _service.DenieOrder(id);
            if (!temp)
                return BadRequest();
            return Ok();
        }
    }
}
