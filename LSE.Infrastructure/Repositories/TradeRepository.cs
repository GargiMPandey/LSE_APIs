using LSE.Core.Entities;
using LSE.Core.RepositoryContracts;
using LSE.Infrastructure.DBContext;
using System.Threading.Tasks;

namespace LSE.Infrastructure.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly LSEDBContext _context;
        public TradeRepository(LSEDBContext context) => _context = context;

        public async Task AddAsync(Trade trade)
        {
            await _context.Trade.AddAsync(trade);
            await _context.SaveChangesAsync();
        }
    }
}