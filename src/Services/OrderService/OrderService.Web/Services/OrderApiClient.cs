using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using OrderService.Application.DTOs;

namespace OrderService.Web.Services
{
    public class OrderApiClient : IOrderApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly ILogger<OrderApiClient> _logger;

        public OrderApiClient(HttpClient httpClient, IConfiguration configuration, ILogger<OrderApiClient> logger)
        {
            _httpClient = httpClient;
            var baseUrl = configuration["ApiSettings:BaseUrl"] ?? throw new ArgumentNullException("ApiSettings:BaseUrl is not configured");
            _httpClient.BaseAddress = new Uri(baseUrl);
            _logger = logger;
            
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(Guid userId)
        {
            try
            {
                _logger.LogInformation("Getting orders for user {UserId}", userId);
                var response = await _httpClient.GetAsync($"/api/Orders/user/{userId}");
                response.EnsureSuccessStatusCode();
                
                var content = await response.Content.ReadAsStringAsync();
                var orders = JsonSerializer.Deserialize<IEnumerable<OrderDto>>(content, _jsonOptions);
                _logger.LogInformation("Successfully retrieved {Count} orders for user {UserId}", orders?.Count() ?? 0, userId);
                return orders ?? Enumerable.Empty<OrderDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting orders for user {UserId}", userId);
                throw;
            }
        }

        public async Task<OrderDto?> GetOrderByIdAsync(Guid orderId)
        {
            try
            {
                _logger.LogInformation("Getting order {OrderId}", orderId);
                var response = await _httpClient.GetAsync($"/api/Orders/{orderId}");
                
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;
                    
                response.EnsureSuccessStatusCode();
                
                var content = await response.Content.ReadAsStringAsync();
                var order = JsonSerializer.Deserialize<OrderDto>(content, _jsonOptions);
                _logger.LogInformation("Successfully retrieved order {OrderId}", orderId);
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting order {OrderId}", orderId);
                throw;
            }
        }

        public async Task<Guid> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            try
            {
                _logger.LogInformation("Creating new order for user {UserId}", createOrderDto.UserId);
                var json = JsonSerializer.Serialize(createOrderDto, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("/api/Orders", content);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Failed to create order. Status: {StatusCode}, Response: {Response}", 
                        response.StatusCode, errorContent);
                    throw new HttpRequestException($"Failed to create order. Status: {response.StatusCode}, Response: {errorContent}");
                }
                
                var responseContent = await response.Content.ReadAsStringAsync();
                var orderId = JsonSerializer.Deserialize<Guid>(responseContent, _jsonOptions);
                _logger.LogInformation("Successfully created order {OrderId}", orderId);
                return orderId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order for user {UserId}", createOrderDto.UserId);
                throw;
            }
        }
    }
} 