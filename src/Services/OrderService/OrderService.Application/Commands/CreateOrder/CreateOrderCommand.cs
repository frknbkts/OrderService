using System;
using MediatR;
using OrderService.Application.DTOs;

namespace OrderService.Application.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public CreateOrderDto OrderDto { get; }

        public CreateOrderCommand(CreateOrderDto orderDto)
        {
            OrderDto = orderDto;
        }
    }
} 