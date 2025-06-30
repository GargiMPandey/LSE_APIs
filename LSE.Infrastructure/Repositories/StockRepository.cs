using LSE.Core.Entities;
using LSE.Core.RepositoryContracts;
using LSE.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSE.Infrastructure.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly LSEDBContext _context;
        public StockRepository(LSEDBContext context) => _context = context;

        public async Task<Stock?> GetByTickerAsync(string ticker)
        {
            return await _context.Stock.Include(s => s.Trades).FirstOrDefaultAsync(s => s.TickerSymbol == ticker);
        }



        public async Task<List<Stock>> GetAllAsync() 
        {
           return  await _context.Stock.Include(s => s.Trades).ToListAsync();
        }


        public async Task<List<Stock>> GetByTickersAsync(IEnumerable<string> tickers) 
        {
            return await _context.Stock.Include(s => s.Trades)
                .Where(s => tickers.Contains(s.TickerSymbol.ToUpper())).ToListAsync();

        }

        public async Task AddAsync(Stock stock)
        {
            await _context.Stock.AddAsync(stock);
            await _context.SaveChangesAsync();
        }
    }
}