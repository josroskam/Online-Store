using Azure.Data.Tables;
using OrderMetricsCalculationFunction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMetricsCalculationFunction.Repository
{
    public class OrderMetricsRepository : IOrderMetricsRepository
    {
        private readonly TableClient _tableClient;

        public OrderMetricsRepository(string connectionString, string tableName)
        {
            _tableClient = new TableClient(connectionString, tableName);
            _tableClient.CreateIfNotExists();
        }

        public async Task SaveMetricsAsync(OrderMetrics metrics)
        {
            await _tableClient.AddEntityAsync(metrics);
        }
    }
}
