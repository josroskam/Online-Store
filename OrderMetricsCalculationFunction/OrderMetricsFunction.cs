using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using OrderMetricsCalculationFunction.Models;

public class OrderMetricsFunction
{
    private readonly TableClient _tableClient;

    public OrderMetricsFunction()
    {
        // Initialize TableClient to connect to Azure Table Storage
        string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
        string tableName = "OrderMetrics";
        _tableClient = new TableClient(connectionString, tableName);

        // Ensure the table is created
        _tableClient.CreateIfNotExists();
    }

    [FunctionName("CalculateOrderMetrics")]
    public async Task Run([TimerTrigger("0 0 23 * * *")] TimerInfo timer, ILogger log)
    {
        log.LogInformation("Calculating order metrics...");

        // Example calculation logic for metrics
        var metrics = new OrderMetrics
        {
            TotalOrders = 100, // Placeholder values
            ProcessedOrders = 90,
            AverageProcessingTime = "1h 30m"
        };

        // Insert metrics into Table Storage
        await _tableClient.AddEntityAsync(metrics);

        log.LogInformation("Order metrics successfully saved to Azure Table Storage.");
    }
}
