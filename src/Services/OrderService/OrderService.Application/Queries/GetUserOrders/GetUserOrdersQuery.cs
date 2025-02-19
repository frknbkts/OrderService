using System;
using System.Collections.Generic;
using MediatR;
using OrderService.Application.DTOs;

namespace OrderService.Application.Queries.GetUserOrders
{
    public class GetUserOrdersQuery : IRequest<IEnumerable<OrderDto>>
    {
        public Guid UserId { get; }

        public GetUserOrdersQuery(Guid userId)
        {
            UserId = userId;
        }
    }
} 