using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using OrderService.Services;
using OrderService.Repository;

var builder = WebApplication.CreateBuilder(args);

// Load Cosmos DB configuration from appsettings.json
var cosmosDbConfig = builder.Configuration.GetSection("CosmosDb");
string connectionString = cosmosDbConfig["ConnectionString"];
string databaseName = cosmosDbConfig["DatabaseName"];
string containerName = cosmosDbConfig["ContainerName"];

// Register services to the container
builder.Services.AddScoped<IOrderService, OrderService.Services.OrderService>();
builder.Services.AddScoped<IOrderRepository>(provider =>
    new OrderRepository(connectionString, databaseName, containerName));  // Register OrderRepository with Cosmos DB settings

builder.Services.AddControllers();  // Register controllers

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
