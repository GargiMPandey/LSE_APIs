using LSE.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSE.Core.RepositoryContracts
{
    public interface IBrokerRepository
    {
        Task<Broker?> AddBroker(Broker broker);
        Task<Broker?> GetBrokerByEmailId(string emailId, string? password);
    }
}
