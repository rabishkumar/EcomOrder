// using Microsoft.AspNetCore.HttpOverrides;

// var builder = WebApplication.CreateBuilder(args);

// // Force Kestrel to listen on all interfaces (important for Kubernetes)
// builder.WebHost.ConfigureKestrel(options =>
// {
//     options.ListenAnyIP(80); // Bind to port 80
// });

// // Add services to the DI container
// builder.Services.AddControllers();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Enable Forwarded Headers Middleware (Needed for Ingress Controllers)
// app.UseForwardedHeaders(new ForwardedHeadersOptions
// {
//     ForwardedHeaders = ForwardedHeaders.All
// });

// // Enable Swagger for API testing (Optional)
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// // Enable Routing & Controllers
// app.UseRouting();
// app.UseAuthorization();
// app.MapControllers();

// // Handle graceful shutdown (important for Kubernetes)
// app.Lifetime.ApplicationStopping.Register(() =>
// {
//     Console.WriteLine("Application is shutting down gracefully...");
// });

// app.Run();
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Create the app
var app = builder.Build();

// Enable Swagger in development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use middleware
app.UseCors("AllowAll");
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Run the application
app.Run();
