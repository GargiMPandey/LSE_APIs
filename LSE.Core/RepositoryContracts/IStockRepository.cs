using LSE.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LSE.Core.RepositoryContracts
{
    public interface IStockRepository
    {
        Task<Stock?> GetByTickerAsync(string ticker);
        Task<List<Stock>> GetAllAsync();
        Task<List<Stock>> GetByTickersAsync(IEnumerable<string> tickers);
        Task AddAsync(Stock stock);
    }
}