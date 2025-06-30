using LSE.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LSE.Core.ServiceContract
{
    public interface IStockService
    {
        Task<StockValueResponse?> GetStockValueAsync(string ticker);
        Task<List<StockValueResponse>> GetAllStockValuesAsync();
        Task<List<StockValueResponse>> GetStockValuesAsync(IEnumerable<string> tickers);
    }
}