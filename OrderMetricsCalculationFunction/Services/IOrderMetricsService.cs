using System.Threading.Tasks;

namespace OrderMetricsCalculationFunction.Services
{
    public interface IOrderMetricsService
    {
        Task CalculateAndStoreMetricsAsync();
    }
}
