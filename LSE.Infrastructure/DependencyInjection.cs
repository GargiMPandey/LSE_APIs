using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSE.Core.RepositoryContracts;
using Microsoft.Extensions.DependencyInjection;
using LSE.Infrastructure.Repositories;
using LSE.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LSE.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("LSEConnection")!;

            services.AddDbContext<LSEDBContext>(options =>
                options.UseMySQL(connectionString)); 

            services.AddScoped<IBrokerRepository, BrokerRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<ITradeRepository, TradeRepository>();
            return services;
        }   
    }
}
