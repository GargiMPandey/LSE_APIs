using LSE.Core.DTO;
using LSE.Core.Entities;
using LSE.Core.RepositoryContracts;
using LSE.Core.ServiceContract;
using System;
using System.Threading.Tasks;

namespace LSE.Core.Services
{
    public class TradeService : ITradeService
    {
        private readonly IStockRepository _stockRepository;
        private readonly ITradeRepository _tradeRepository;

        public TradeService(IStockRepository stockRepository, ITradeRepository tradeRepository)
        {
            _stockRepository = stockRepository;
            _tradeRepository = tradeRepository;
        }

        public async Task AddTradeAsync(TradeRequest request)
        {
            var stock = await _stockRepository.GetByTickerAsync(request.TickerSymbol.ToUpper());
            if (stock == null)
            {
                stock = new Stock { TickerSymbol = request.TickerSymbol.ToUpper() };
                await _stockRepository.AddAsync(stock);
            }

            var trade = new Trade
            {
                StockId = stock.Id,
                Price = request.Price,
                NumberOfShares = request.NumberOfShares,
                BrokerId = Guid.Parse(request.BrokerId),
                Timestamp = DateTime.UtcNow
            };

            await _tradeRepository.AddAsync(trade);
        }
    }
}