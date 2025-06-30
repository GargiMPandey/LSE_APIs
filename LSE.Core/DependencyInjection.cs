using Microsoft.Extensions.DependencyInjection;
using LSE.Core.ServiceContract;
using LSE.Core.Services;

namespace LSE.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {

            services.AddScoped<IBrokerService, BrokerService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<ITradeService, TradeService>();

            return services;
        }
    }
}
