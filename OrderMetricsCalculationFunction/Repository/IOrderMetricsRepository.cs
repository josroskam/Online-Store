using OrderMetricsCalculationFunction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMetricsCalculationFunction.Repository
{
    public interface IOrderMetricsRepository
    {
        Task SaveMetricsAsync(OrderMetrics metrics);
    }
}
