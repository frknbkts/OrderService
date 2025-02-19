using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Commands.CreateOrder;
using OrderService.Application.DTOs;
using OrderService.Application.Queries.GetOrderById;
using OrderService.Application.Queries.GetUserOrders;

namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            var command = new CreateOrderCommand(createOrderDto);
            var orderId = await _mediator.Send(command);
            return Ok(orderId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(Guid id)
        {
            var query = new GetOrderByIdQuery(id);
            var order = await _mediator.Send(query);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetUserOrders(Guid userId)
        {
            var query = new GetUserOrdersQuery(userId);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }
    }
} 