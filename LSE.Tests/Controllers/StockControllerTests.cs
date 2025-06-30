using LSE.API.Controllers;
using LSE.Core.DTO;
using LSE.Core.ServiceContract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class StockControllerTests
{
    private readonly Mock<IStockService> _stockServiceMock = new();
    private readonly Mock<ILogger<StockController>> _loggerMock = new();
    private readonly StockController _controller;

    public StockControllerTests()
    {
        _controller = new StockController(_stockServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetStockValue_ReturnsOk_WhenStockExists()
    {
        var response = new StockValueResponse { TickerSymbol = "INFY", AveragePrice = 100 };
        _stockServiceMock.Setup(s => s.GetStockValueAsync("INFY")).ReturnsAsync(response);

        var result = await _controller.GetStockValue("INFY");

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(response, okResult.Value);
    }

    [Fact]
    public async Task GetStockValue_ReturnsNotFound_WhenStockDoesNotExist()
    {
        _stockServiceMock.Setup(s => s.GetStockValueAsync("ABCD")).ReturnsAsync((StockValueResponse)null);

        var result = await _controller.GetStockValue("ABCD");

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetStockValue_ReturnsBadRequest_WhenTickerIsNull()
    {
        var result = await _controller.GetStockValue(null);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetAllStockValues_ReturnsOk_WhenStocksExist()
    {
        var response = new List<StockValueResponse> { new StockValueResponse() };
        _stockServiceMock.Setup(s => s.GetAllStockValuesAsync()).ReturnsAsync(response);

        var result = await _controller.GetAllStockValues();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(response, okResult.Value);
    }

    [Fact]
    public async Task GetAllStockValues_ReturnsNotFound_WhenNoStocks()
    {
        _stockServiceMock.Setup(s => s.GetAllStockValuesAsync()).ReturnsAsync((List<StockValueResponse>)null);

        var result = await _controller.GetAllStockValues();

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetStockValues_ReturnsOk_WhenStocksExist()
    {
        var tickers = new List<string> { "INFY", "ADAN" };
        var response = new List<StockValueResponse> { new StockValueResponse() };
        _stockServiceMock.Setup(s => s.GetStockValuesAsync(tickers)).ReturnsAsync(response);

        var result = await _controller.GetStockValues(tickers);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(response, okResult.Value);
    }

    [Fact]
    public async Task GetStockValues_ReturnsBadRequest_WhenTickersNull()
    {
        var result = await _controller.GetStockValues(null);

        Assert.IsType<BadRequestObjectResult>(result);
    }
}