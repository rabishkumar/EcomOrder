using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces;
using OrderService.Application.UseCases;
using OrderService.Infrastructure.Messaging;
using OrderService.Infrastructure.Persistence;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Register application services
        services.AddScoped<CreateOrderUseCase>();

        // Register infrastructure services
        services.AddDbContext<OrderDbContext>(options =>
            options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=EcomOrder;Trusted_Connection=True;"));
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IEventPublisher, EventBridgePublisher>();

        // Add controllers
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
