using Microsoft.EntityFrameworkCore;
using OrderService.Application.Commands.CreateOrder;
using OrderService.Application.Services;
using OrderService.Domain.Events;
using OrderService.Infrastructure.Extensions;
using OrderService.Infrastructure.Messaging;
using OrderService.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5020); // http
    serverOptions.ListenAnyIP(5021, configure => configure.UseHttps()); // https
});

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson();

// Add MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly);
});

// Add Infrastructure Services
builder.Services.AddInfrastructureServices(builder.Configuration);

// Add Application Services
builder.Services.AddScoped<IOrderEventService, OrderEventService>();

// Add Event Bus
builder.Services.AddSingleton<IEventBus, KafkaEventBus>();

// Add HttpClient
builder.Services.AddHttpClient<IOrderApiClient, OrderApiClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
