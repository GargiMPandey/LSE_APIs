using LSE.Core.DTO;
using LSE.Core.Entities;
using LSE.Core.RepositoryContracts;
using LSE.Core.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class StockServiceTests
{
    private readonly Mock<IStockRepository> _repoMock = new();
    private readonly StockService _service;

    public StockServiceTests()
    {
        _service = new StockService(_repoMock.Object);
    }

    [Fact]
    public async Task GetStockValueAsync_ReturnsNull_WhenNoStock()
    {
        _repoMock.Setup(r => r.GetByTickerAsync("ABCD")).ReturnsAsync((Stock)null);

        var result = await _service.GetStockValueAsync("ABCD");

        Assert.Null(result);
    }

    [Fact]
    public async Task GetStockValueAsync_ReturnsValue_WhenStockExists()
    {
        var stock = new Stock { TickerSymbol = "INFY", Trades = new List<Trade> { new Trade { Price = 100 } } };
        _repoMock.Setup(r => r.GetByTickerAsync("INFY")).ReturnsAsync(stock);

        var result = await _service.GetStockValueAsync("INFY");

        Assert.NotNull(result);
        Assert.Equal("INFY", result.TickerSymbol);
        Assert.Equal(100, result.AveragePrice);
    }

    [Fact]
    public async Task GetAllStockValuesAsync_ReturnsList()
    {
        var stocks = new List<Stock>
        {
            new Stock { TickerSymbol = "INFY", Trades = new List<Trade> { new Trade { Price = 100 } } }
        };
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(stocks);

        var result = await _service.GetAllStockValuesAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetStockValuesAsync_ReturnsList()
    {
        var stocks = new List<Stock>
        {
            new Stock { TickerSymbol = "INFY", Trades = new List<Trade> { new Trade { Price = 100 } } },
            new Stock { TickerSymbol = "INFYY", Trades = new List<Trade> { new Trade { Price = 100 } } }
        };
        _repoMock.Setup(r => r.GetByTickersAsync(It.IsAny<IEnumerable<string>>())).ReturnsAsync(stocks);

        var result = await _service.GetStockValuesAsync(new[] { "INFY" , "INFYY" });

        Assert.All(result, stock =>
        {
            Assert.NotNull(stock);
            Assert.Contains(stock.TickerSymbol, new[] { "INFY", "INFYY" });
        });
    }
}