using LSE.Core.DTO;
using LSE.Core.RepositoryContracts;
using LSE.Core.ServiceContract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSE.Core.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        public StockService(IStockRepository stockRepository) => _stockRepository = stockRepository;

        public async Task<StockValueResponse?> GetStockValueAsync(string ticker)
        {
            var stock = await _stockRepository.GetByTickerAsync(ticker.ToUpper());
            if (stock == null || !stock.Trades.Any()) return null;
            return new StockValueResponse
            {
                TickerSymbol = stock.TickerSymbol,
                AveragePrice = stock.Trades.Average(t => t.Price)
            };
        }

        public async Task<List<StockValueResponse>> GetAllStockValuesAsync()
        {
            var stocks = await _stockRepository.GetAllAsync();
            return stocks
                .Where(s => s.Trades.Any())
                .Select(s => new StockValueResponse
                {
                    TickerSymbol = s.TickerSymbol,
                    AveragePrice = s.Trades.Average(t => t.Price)
                }).ToList();
        }

        public async Task<List<StockValueResponse>> GetStockValuesAsync(IEnumerable<string> tickers)
        {
            var stocks = await _stockRepository.GetByTickersAsync(tickers);
            return stocks
                .Where(s => s.Trades.Any())
                .Select(s => new StockValueResponse
                {
                    TickerSymbol = s.TickerSymbol,
                    AveragePrice = s.Trades.Average(t => t.Price)
                }).ToList();
        }
    }
}