using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisrInsuranceOrderManagment.Domain.Entities;
using MisrInsuranceOrderManagment.DTOs;
using MisrInsuranceOrderManagment.Errors;
using MisrInsuranceOrderManagment.Infrastructure;
using MisrInsuranceOrderManagment.Services;

namespace MisrInsuranceOrderManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
 
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto createOrderDto)
        {
            var order = new Order
            {
                CustomerID = createOrderDto.CustomerID,
                OrderItems = createOrderDto.OrderItems
                        .Select(oi => new OrderItem { ProductID = oi.ProductID, Quantity = oi.Quantity })
                        .ToList()
            };

            var createdOrder = await _orderService.CreateOrderAsync(order);
            return createdOrder == null ? BadRequest(new ApiResponse(400, "An error occured during the creation of the order"))
                : Ok(order);

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
             return order != null? Ok(order) : BadRequest(new ApiResponse(404, "Order not found"));
             
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var updateOrder = await _orderService.UpdateOrderStatusAsync(id, status);
            return Ok(updateOrder != null ? updateOrder : new ApiResponse(400));
        }
    }
}
