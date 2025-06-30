using LSE.Core.DTO;
using LSE.Core.Entities;
using LSE.Core.RepositoryContracts;
using LSE.Core.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

public class TradeServiceTests
{
    private readonly Mock<IStockRepository> _stockRepoMock = new();
    private readonly Mock<ITradeRepository> _tradeRepoMock = new();
    private readonly TradeService _service;

    public TradeServiceTests()
    {
        _service = new TradeService(_stockRepoMock.Object, _tradeRepoMock.Object);
    }

    [Fact]
    public async Task AddTradeAsync_AddsTrade_WhenStockExists()
    {
        var request = new TradeRequest { TickerSymbol = "INFY", Price = 100, NumberOfShares = 1, BrokerId = Guid.NewGuid().ToString() };
        var stock = new Stock { Id = 1, TickerSymbol = "INFY" };
        _stockRepoMock.Setup(r => r.GetByTickerAsync("INFY")).ReturnsAsync(stock);

        await _service.AddTradeAsync(request);

        _tradeRepoMock.Verify(r => r.AddAsync(It.IsAny<Trade>()), Times.Once);
    }

    [Fact]
    public async Task AddTradeAsync_AddsStock_WhenStockDoesNotExist()
    {
        var request = new TradeRequest { TickerSymbol = "AIML", Price = 100, NumberOfShares = 1, BrokerId = Guid.NewGuid().ToString() };
        _stockRepoMock.Setup(r => r.GetByTickerAsync("AIML")).ReturnsAsync((Stock)null);

        await _service.AddTradeAsync(request);

        _stockRepoMock.Verify(r => r.AddAsync(It.IsAny<Stock>()), Times.Once);
        _tradeRepoMock.Verify(r => r.AddAsync(It.IsAny<Trade>()), Times.Once);
    }
}