using LSE.Core.Entities;
using System.Threading.Tasks;

namespace LSE.Core.RepositoryContracts
{
    public interface ITradeRepository
    {
        Task AddAsync(Trade trade);
    }
}