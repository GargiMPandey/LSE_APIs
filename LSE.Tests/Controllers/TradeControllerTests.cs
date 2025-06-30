using LSE.API.Controllers;
using LSE.Core.DTO;
using LSE.Core.ServiceContract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

public class TradeControllerTests
{
    private readonly Mock<ITradeService> _tradeServiceMock = new();
    private readonly Mock<ILogger<TradeController>> _loggerMock = new();
    private readonly TradeController _controller;

    public TradeControllerTests()
    {
        _controller = new TradeController(_tradeServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task PostTrade_ReturnsCreated_WhenValid()
    {
        var request = new TradeRequest();
        _tradeServiceMock.Setup(s => s.AddTradeAsync(request)).Returns(Task.CompletedTask);

        var result = await _controller.PostTrade(request);

        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public async Task PostTrade_ReturnsBadRequest_WhenNull()
    {
        var result = await _controller.PostTrade(null);

        Assert.IsType<BadRequestObjectResult>(result);
    }
}