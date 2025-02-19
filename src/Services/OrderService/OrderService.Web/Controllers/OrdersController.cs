using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.DTOs;
using OrderService.Web.Services;

namespace OrderService.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderApiClient _orderApiClient;
        private readonly Guid _defaultUserId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"); // Demo user ID

        public OrdersController(IOrderApiClient orderApiClient)
        {
            _orderApiClient = orderApiClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var orders = await _orderApiClient.GetUserOrdersAsync(_defaultUserId);
                return View(orders);
            }
            catch (Exception ex)
            {
                // Log the error
                ModelState.AddModelError("", "Error retrieving orders: " + ex.Message);
                return View(Array.Empty<OrderDto>());
            }
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var order = await _orderApiClient.GetOrderByIdAsync(id);
                if (order == null)
                    return NotFound();

                return View(order);
            }
            catch (Exception ex)
            {
                // Log the error
                return NotFound("Error retrieving order: " + ex.Message);
            }
        }

        public IActionResult Create()
        {
            return View(new CreateOrderDto
            {
                UserId = _defaultUserId,
                OrderItems = new List<OrderItemDto>(),
                ShippingAddress = new AddressDto()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderDto createOrderDto)
        {
            if (!ModelState.IsValid)
                return View(createOrderDto);

            try
            {
                var orderId = await _orderApiClient.CreateOrderAsync(createOrderDto);
                return RedirectToAction(nameof(Details), new { id = orderId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating order: " + ex.Message);
                return View(createOrderDto);
            }
        }
    }
} 