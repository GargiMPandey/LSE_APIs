using LSE.Core.DTO;
using System.Threading.Tasks;

namespace LSE.Core.ServiceContract
{
    public interface ITradeService
    {
        Task AddTradeAsync(TradeRequest request);
    }
}