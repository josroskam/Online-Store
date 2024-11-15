using Microsoft.AspNetCore.Mvc;
using ProductService.Repository;
using ReviewService.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services to the container
builder.Services.AddScoped<IReviewService, ReviewService.Services.ReviewService>();
builder.Services.AddControllers();  // Register controllers

builder.Services.AddScoped<IProductRepository>(provider =>
    new ProductRepository(builder.Configuration.GetConnectionString("CosmosDb")));

// Configure Kestrel to listen on all network interfaces (0.0.0.0) and port 80
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);  // Listens on all network interfaces on port 80
});

var app = builder.Build();

// Add authorization middleware (if needed)
app.UseAuthorization();

// Map controllers to handle incoming HTTP requests
app.MapControllers();

// Run the application
app.Run();
