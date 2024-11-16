using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using OrderMetricsCalculationFunction.Models;
using OrderMetricsCalculationFunction.Services;

public class OrderMetricsFunction
{
    private readonly IOrderMetricsService _metricsService;

    public OrderMetricsFunction(IOrderMetricsService metricsService)
    {
        _metricsService = metricsService;
    }

    [Function("CalculateOrderMetrics")]
    public async Task Run([TimerTrigger("0 0 23 * * *")] TimerInfo timer, ILogger log)
    {
        log.LogInformation("Starting order metrics calculation...");
        await _metricsService.CalculateAndStoreMetricsAsync();
        log.LogInformation("Order metrics calculation completed.");
    }
}
