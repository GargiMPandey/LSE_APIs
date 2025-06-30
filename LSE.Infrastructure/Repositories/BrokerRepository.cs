using LSE.Core.Entities;
using LSE.Core.RepositoryContracts;
using LSE.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSE.Infrastructure.Repositories
{
    public class BrokerRepository : IBrokerRepository
    {
        private readonly LSEDBContext _dbcontext;
        public BrokerRepository(LSEDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        async Task<Broker?> IBrokerRepository.AddBroker(Broker broker)
        {
            _dbcontext.Broker.Add(broker);
            await _dbcontext.SaveChangesAsync();            
            return broker;
        }

       async Task<Broker?> IBrokerRepository.GetBrokerByEmailId(string emailId, string? password)
        {

          var brokerlist =  await _dbcontext.Broker.ToListAsync();
            return brokerlist.FirstOrDefault(b => b.EmailId == emailId && (password == null || b.Password == password));
        }
    }
}
