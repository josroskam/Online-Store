using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMetricsCalculationFunction.Models
{
    public class OrderMetrics : ITableEntity
    {
        public string PartitionKey { get; set; } = "OrderMetrics";
        public string RowKey { get; set; } = Guid.NewGuid().ToString(); // Unique identifier for each metric entry
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public int TotalOrders { get; set; }
        public int ProcessedOrders { get; set; }
        public string AverageProcessingTime { get; set; }

        public ETag ETag { get; set; }  // Required by ITableEntity
        DateTimeOffset? ITableEntity.Timestamp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
