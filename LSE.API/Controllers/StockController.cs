using LSE.Core.ServiceContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LSE.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly ILogger<StockController> _logger;

        public StockController(IStockService stockService, ILogger<StockController> logger)
        {
            _stockService = stockService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the stock value for a given ticker symbol.
        /// </summary>
        /// <param name="ticker">The ticker symbol of the stock.</param>
        /// <returns>The current value of the stock.</returns>
        [HttpGet("{ticker}")]
        public async Task<IActionResult> GetStockValue(string ticker)
        {
            if (string.IsNullOrWhiteSpace(ticker))
            {
                return BadRequest("Ticker must be provided.");
            }

            try
            {
                var value = await _stockService.GetStockValueAsync(ticker);
                if (value == null) return NotFound();
                return Ok(value);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Failed to get stock by ticker symbol: {ticker}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves all stock values from the system.
        /// </summary>
        /// <returns>The current value of all the stocks.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllStockValues()
        {
            try
            {
                var values = await _stockService.GetAllStockValuesAsync();
                if (values == null)
                {
                    return NotFound();
                }
                return Ok(values);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Failed to get all stock values.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves stock values for the specified list of ticker symbols.
        /// </summary>
        /// <param name="tickers">A list of stock ticker symbols for which to retrieve values.</param>
        /// <returns>Current Stock values for the specified tickers.</returns>
        [HttpPost("range")]
        public async Task<IActionResult> GetStockValues([FromBody] List<string> tickers)
        {
            if (tickers == null || tickers.Count == 0)
            {
                return BadRequest("Tickers list must be provided and not empty.");
            }

            try
            {
                var values = await _stockService.GetStockValuesAsync(tickers);
                if (values == null)
                {
                    return NotFound();
                }
                return Ok(values);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Failed to get stocks by ticker symbol: {tickers}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}