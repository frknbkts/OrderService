using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain.Events;
using OrderService.Domain.Repositories;
using OrderService.Infrastructure.Persistence;
using OrderService.Infrastructure.Repositories;
using OrderService.Infrastructure.Messaging;

namespace OrderService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<OrderDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("OrderDb")));

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddSingleton<IEventBus, KafkaEventBus>();

            return services;
        }
    }
} 